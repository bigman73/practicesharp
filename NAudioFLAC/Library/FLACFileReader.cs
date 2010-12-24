#region © Copyright 2010 Yuval Naveh. MIT.
/* Copyright (c) 2010, Yuval Naveh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using NAudio.Wave;
using System.Runtime.InteropServices;

namespace BigMansStuff.NAudio.FLAC
{
    /// <summary>
    /// NAudio reader for FLAC files
    /// </summary>
    /// <remarks>
    /// Written By Yuval Naveh, based a .NET/C# Interop wrapper by Stanimir Stoyanov - http://stoyanoff.info/blog/2010/07/26/decoding-flac-audio-files-in-c/
    /// using libFlac - http://flac.sourceforge.net
    /// </remarks>
    public class FLACFileReader : WaveStream
    {
        #region Constructors

        /// <summary>Constructor - Supports opening a FLAC file</summary>
        public FLACFileReader(string flacFileName) 
        {
            m_stream = File.OpenRead(flacFileName);
            m_reader = new BinaryReader(m_stream);
            m_decoderContext = LibFLACSharp.FLAC__stream_decoder_new();

            if (m_decoderContext == IntPtr.Zero)
                throw new ApplicationException("FLAC: Could not initialize stream decoder!");

            m_writeCallback = new LibFLACSharp.Decoder_WriteCallback(FLAC_WriteCallback);
            m_metadataCallback = new LibFLACSharp.Decoder_MetadataCallback(FLAC_MetadataCallback);
            m_errorCallback = new LibFLACSharp.Decoder_ErrorCallback(FLAC_ErrorCallback);

            m_inputBitDepth = -1;

            if (LibFLACSharp.FLAC__stream_decoder_init_file(m_decoderContext,
                                               flacFileName, m_writeCallback, m_metadataCallback, m_errorCallback,
                                               IntPtr.Zero) != 0)
                throw new ApplicationException("FLAC: Could not open stream for reading!");
            // m_waveFormat = m_wmaStream.Format;

            // Process the meta-data (but not the audio frames) so we can prepare the NAudio wave format
            FLACCheck(
                LibFLACSharp.FLAC__stream_decoder_process_until_end_of_metadata(m_decoderContext), 
                "Could not process until end of metadata");

            m_dryRun = true;
            try
            {
                // Read first block/or fix meta data retrieval
                FLACCheck(
               LibFLACSharp.FLAC__stream_decoder_process_single(m_decoderContext),
               "Could not process single");

                // Reset position to beginning
                FLACCheck(LibFLACSharp.FLAC__stream_decoder_seek_absolute(m_decoderContext, 0), "Could not seek absolute");
            }
            finally
            {
                m_dryRun = false;
            }

            m_inputSampleRate = LibFLACSharp.FLAC__stream_decoder_get_sample_rate(m_decoderContext);
            m_inputBitDepth = LibFLACSharp.FLAC__stream_decoder_get_bits_per_sample(m_decoderContext);
            m_inputChannels = LibFLACSharp.FLAC__stream_decoder_get_channels(m_decoderContext);
            m_totalSamples = LibFLACSharp.FLAC__stream_decoder_get_total_samples(m_decoderContext);
            
            m_waveFormat = new WaveFormat(m_inputSampleRate, m_inputBitDepth, m_inputChannels);
        }
        


        #endregion

        #region Overrides - Implement logic which is specific to FLAC

        /// <summary>
        /// This is the length in bytes of data available to be read out from the Read method
        /// (i.e. the decompressed WMA length)
        /// n.b. this may return 0 for files whose length is unknown
        /// </summary>
        public override long Length
        {
            get
            {
                // Note: Workaround to fix NAudio calculation of position (which takes channels into account) and FLAC (which ignores channels for sample position)
                return m_totalSamples * m_waveFormat.BlockAlign;
            }
        }

        /// <summary>
        /// <see cref="WaveStream.WaveFormat"/>
        /// </summary>
        public override WaveFormat WaveFormat
        {
            get { return m_waveFormat; }
        }

        /// <summary>
        /// <see cref="Stream.Position"/>
        /// </summary>
        public override long Position
        {
            get
            {
                long flacPosition = 0;
                lock (m_repositionLock)
                {
                    FLACCheck(LibFLACSharp.FLAC__stream_decoder_get_decode_position(m_decoderContext, ref flacPosition), "Could not get decoder position");
                }
                // Note: Workaround to fix NAudio calculation of position (which takes channels into account) and FLAC (which ignores channels for sample position)
                return flacPosition * m_waveFormat.Channels;
            }
            set
            {
                lock (m_repositionLock)
                {
                    m_flacSampleIndex = 0;
                    
                    // Note: Workaround to fix NAudio calculation of position (which takes channels and block align into account) and FLAC (which ignores channels for sample position)
                    long flacPosition = value / m_waveFormat.BlockAlign; 
                    FLACCheck(LibFLACSharp.FLAC__stream_decoder_seek_absolute(m_decoderContext, flacPosition ), "Could not seek absolute");
                }
            }
        }

        /// <summary>
        /// Reads decompressed PCM data from our WMA file.
        /// </summary>
        public override int Read(byte[] playbackSampleBuffer, int offset, int numBytes)
        {
            // Console.WriteLine("CurrentTime: " + CurrentTime + "," );
            lock (m_repositionLock)
            {
                m_NAudioSampleBuffer = playbackSampleBuffer;
                m_playbackBufferOffset = offset;
                
                int flacBytesCopied = 0;
                // If there are still samples in the flac buffer, use them first before reading the next FLAC frame
                if (m_flacSampleIndex > 0)
                {
                    flacBytesCopied = CopyFlacBufferToNAudioBuffer();
                }

                // Keep reading flac packets until enough bytes have been copied
                while (flacBytesCopied < numBytes)
                {
                    // Read PCM bytes from the FLAC File into the sample buffer
                    FLACCheck(
                            LibFLACSharp.FLAC__stream_decoder_process_single(m_decoderContext),
                            "process single");

                    flacBytesCopied += CopyFlacBufferToNAudioBuffer();
                }
            }
            
            return numBytes;
        }

        #endregion 

        #region Private Methods

        /// <summary>
        /// Helper utility function - Checks the result of a libFlac function by throwing an exception if the result was false
        /// </summary>
        /// <param name="result"></param>
        /// <param name="operation"></param>
        private void FLACCheck(bool result, string operation)
        {
            if (!result)
                 throw new ApplicationException(string.Format("FLAC: Could not {0}!", operation));
        }

        /// <summary>
        /// Copies the Flac buffer samples to the NAudio buffer
        /// This method is an "Adapter" between the two different buffers and is the key functionality 
        ///   that enables NAudio to play FLAC frames
        /// The Flac buffer has a different length and structure (i.e. all samples from channel 0, all samples from channel 1)
        ///   than the NAudio samples buffer which has a interleaved structure (e.g sample 1 from channel 0, then sample 1 from channel 1 then sample 2 channel from Channel 1 etc.)
        /// </summary>
        /// <returns></returns>
        private int CopyFlacBufferToNAudioBuffer()
        {
            int startPlaybackBufferOffset = m_playbackBufferOffset;
            bool nAudioBufferFull = m_playbackBufferOffset >= m_NAudioSampleBuffer.Length;

            // For each channel, there are BlockSize number of samples, so let's process these.
            for (; m_flacSampleIndex < m_samplesPerChannel && !nAudioBufferFull; m_flacSampleIndex++)
            {
                for (int channel = 0; channel < m_inputChannels && !nAudioBufferFull; channel++)
                {
                    int sample = m_flacSamples[m_flacSampleIndex + channel * m_samplesPerChannel];

                    switch (m_inputBitDepth)
                    {
                        case 16: // 16-bit
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)(sample);
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)(sample >> 8);

                            nAudioBufferFull = m_playbackBufferOffset >= m_NAudioSampleBuffer.Length;

                            break;

        //                case 24: // 24-bit
                            // TODO: Write to buffer
          //                  break;

                        default:
                            throw new NotSupportedException("Input FLAC bit depth is not supported!");
                    }
                }
            }

            // Flac buffer has been exhausted, reset the buffer sample index so it starts from the beginning
            if (m_flacSampleIndex >= m_samplesPerChannel)
            {
                m_flacSampleIndex = 0;
            }

            int bytesCopied = m_playbackBufferOffset - startPlaybackBufferOffset;
            return bytesCopied;
        }

        #endregion

        #region Callbacks


        private void FLAC_WriteCallback(IntPtr context, IntPtr frame, IntPtr buffer, IntPtr clientData)
        {
            if ( m_dryRun )
            {
                return;
            }

            try
            {
                // Read the FLAC Frame into a memory samples buffer (m_flacSamples)

                LibFLACSharp.FlacFrame flacFrame = (LibFLACSharp.FlacFrame)Marshal.PtrToStructure(frame, typeof(LibFLACSharp.FlacFrame));
                // Console.WriteLine("FLAC_WriteCallback: " + flacFrame.Header.FrameOrSampleNumber);

                if (m_flacSamples == null)
                {
                    // First time - Create Flac sample buffer
                    m_samplesPerChannel = flacFrame.Header.BlockSize;
                    m_flacSamples = new int[m_samplesPerChannel * m_inputChannels];
                    m_flacSampleIndex = 0;
                }

                // Iterate on all channels, copy the unmanaged channel bits (samples) to the a managed samples array
                for (int inputChannel = 0; inputChannel < m_inputChannels; inputChannel++)
                {
                    // Get pointer to channel bits, for the current channel
                    IntPtr pChannelBits = Marshal.ReadIntPtr(buffer, inputChannel * IntPtr.Size);

                    // Copy the unmanaged bits to managed memory
                    Marshal.Copy(pChannelBits, m_flacSamples, inputChannel * m_samplesPerChannel, m_samplesPerChannel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in WriteCallBack: " + ex.ToString());
            }
        }

        private void FLAC_MetadataCallback(IntPtr context, IntPtr metadata, IntPtr userData)
        {
            Console.WriteLine("FLAC_MetadataCallback");
            LibFLACSharp.FLACMetaData flacMetaData = (LibFLACSharp.FLACMetaData)Marshal.PtrToStructure(metadata, typeof(LibFLACSharp.FLACMetaData));

            if (flacMetaData.MetaDataType == LibFLACSharp.FLACMetaDataType.StreamInfo  )
            {
           /*     var t = Marshal.SizeOf(typeof(LibFLACSharp.FLACStreamInfo));
                GCHandle pinnedData = GCHandle.Alloc(flacMetaData.Data, GCHandleType.Pinned);
                m_flacStreamInfo = (LibFLACSharp.FLACStreamInfo)Marshal.PtrToStructure(
                    pinnedData.AddrOfPinnedObject(),
                    typeof(LibFLACSharp.FLACStreamInfo));
                
                pinnedData.Free();
             */   
            }
        }
        
        private void FLAC_ErrorCallback(IntPtr context, LibFLACSharp.DecodeError status, IntPtr userData)
        {
            throw new ApplicationException(string.Format("FLAC: Could not decode frame: {0}!", status));
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposes this WaveStream
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_decoderContext != IntPtr.Zero)
                {
                    FLACCheck(
                        LibFLACSharp.FLAC__stream_decoder_finish(m_decoderContext),
                        "finalize stream decoder");

                    FLACCheck(
                        LibFLACSharp.FLAC__stream_decoder_delete(m_decoderContext),
                        "dispose of stream decoder instance");

                    m_decoderContext = IntPtr.Zero;
                }

                if (m_stream != null)
                {
                    m_stream.Close();
                    m_stream.Dispose();
                    m_stream = null;
                }

                if (m_reader != null)
                {
                    m_reader.Close();
                    m_reader = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Private Members

        private WaveFormat m_waveFormat;
        private object m_repositionLock = new object();

        private IntPtr m_decoderContext;
        private Stream m_stream;
        private BinaryReader m_reader;
        
        private int m_inputBitDepth;
        private int m_inputChannels;
        private int m_inputSampleRate;
        private int m_samplesPerChannel;

        private int[] m_flacSamples;
        private int m_flacSampleIndex;

        private byte[] m_NAudioSampleBuffer;
        private int m_playbackBufferOffset;

        private long m_totalSamples = -1;

        private LibFLACSharp.Decoder_WriteCallback m_writeCallback;
        private LibFLACSharp.Decoder_MetadataCallback m_metadataCallback;
        private LibFLACSharp.Decoder_ErrorCallback m_errorCallback;

        private bool m_dryRun;
        #endregion
    }
}



