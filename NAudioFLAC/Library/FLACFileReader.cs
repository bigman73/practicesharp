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
            // Open the flac file for reading through a binary reader
            m_stream = File.OpenRead(flacFileName);
            m_reader = new BinaryReader(m_stream);
            // Create the FLAC decoder
            m_decoderContext = LibFLACSharp.FLAC__stream_decoder_new();

            if (m_decoderContext == IntPtr.Zero)
                throw new ApplicationException("FLAC: Could not initialize stream decoder!");

            // Create call back delegates
            m_writeCallback = new LibFLACSharp.Decoder_WriteCallback(FLAC_WriteCallback);
            m_metadataCallback = new LibFLACSharp.Decoder_MetadataCallback(FLAC_MetadataCallback);
            m_errorCallback = new LibFLACSharp.Decoder_ErrorCallback(FLAC_ErrorCallback);

            // Initialize the FLAC decoder
            if (LibFLACSharp.FLAC__stream_decoder_init_file(m_decoderContext,
                                               flacFileName, m_writeCallback, m_metadataCallback, m_errorCallback,
                                               IntPtr.Zero) != 0)
                throw new ApplicationException("FLAC: Could not open stream for reading!");

            // Process the meta-data (but not the audio frames) so we can prepare the NAudio wave format
            FLACCheck(
                LibFLACSharp.FLAC__stream_decoder_process_until_end_of_metadata(m_decoderContext), 
                "Could not process until end of metadata");
            
            // Initialize NAudio wave format
            m_waveFormat = new WaveFormat(m_flacStreamInfo.SampleRate, m_flacStreamInfo.BitsPerSample, m_flacStreamInfo.Channels);
        }

        #endregion

        #region Overrides - Implement logic which is specific to FLAC

        /// <summary>
        /// This is the length in bytes of data available to be read out from the Read method
        /// (i.e. the decompressed FLAC length)
        /// n.b. this may return 0 for files whose length is unknown
        /// </summary>
        public override long Length
        {
            get
            {
                // Note: Workaround to fix NAudio calculation of position (which takes channels into account) and FLAC (which ignores channels for sample position)
                return (long) m_totalSamples * m_waveFormat.BlockAlign;
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
        /// Reads decompressed PCM data from our FLAC file into the NAudio playback sample buffer
        /// </summary>
        /// <remarks>
        /// 1. The original code did not stop on end of stream. tomislavtustonic applied a fix using FLAC__stream_decoder_get_state. <seealso cref="https://code.google.com/p/practicesharp/issues/detail?id=14"/>
        /// </remarks>
        public override int Read(byte[] playbackSampleBuffer, int offset, int numBytes)
        {
            int flacBytesCopied = 0;

            lock (m_repositionLock)
            {
                m_NAudioSampleBuffer = playbackSampleBuffer;
                m_playbackBufferOffset = offset;

                // If there are still samples in the flac buffer, use them first before reading the next FLAC frame
                if (m_flacSampleIndex > 0)
                {
                    flacBytesCopied = CopyFlacBufferToNAudioBuffer();
                }
                var decoderState = LibFLACSharp.FLAC__stream_decoder_get_state(m_decoderContext);
                // Keep reading flac packets until enough bytes have been copied
                while (flacBytesCopied < numBytes)
                {
                    // Read the next PCM bytes from the FLAC File into the sample buffer
                    FLACCheck(
                            LibFLACSharp.FLAC__stream_decoder_process_single(m_decoderContext),
                            "process single");
                    decoderState = LibFLACSharp.FLAC__stream_decoder_get_state(m_decoderContext);
                    if (decoderState == LibFLACSharp.StreamDecoderState.EndOfStream)
                        break;
                    else
                        flacBytesCopied += CopyFlacBufferToNAudioBuffer();
                }
            }
            return flacBytesCopied;
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
                for (int channel = 0; channel < m_flacStreamInfo.Channels && !nAudioBufferFull; channel++)
                {
                    int sample = m_flacSamples[m_flacSampleIndex + channel * m_samplesPerChannel];

                    switch (m_flacStreamInfo.BitsPerSample)
                    {
                        case 16: // 16-bit
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)(sample);
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)(sample >> 8);

                            nAudioBufferFull = m_playbackBufferOffset >= m_NAudioSampleBuffer.Length;

                            break;

                        case 24: // 24-bit
                            // Note: Code contributed by Mathew1800, https://code.google.com/p/practicesharp/issues/detail?id=16#c2
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)((sample >>  0) & 0xFF);
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)((sample >>  8) & 0xFF);
                            m_NAudioSampleBuffer[m_playbackBufferOffset++] = (byte)((sample >> 16) & 0xFF);

                            nAudioBufferFull = m_playbackBufferOffset >= m_NAudioSampleBuffer.Length;
                            break;

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

            // Return number of actual bytes copied
            int bytesCopied = m_playbackBufferOffset - startPlaybackBufferOffset;
            return bytesCopied;
        }

        #endregion

        #region libFlac Callbacks

        //private 

        /// <summary>
        /// FLAC Write Call Back - libFlac notifies back on a frame that was read from the source file and written as a frame
        /// </summary>
        /// <param name="context"></param>
        /// <param name="frame"></param>
        /// <param name="buffer"></param>
        /// <param name="clientData"></param>
        private void FLAC_WriteCallback(IntPtr context, IntPtr frame, IntPtr buffer, IntPtr clientData)
        {
            // Read the FLAC Frame into a memory samples buffer (m_flacSamples)
            LibFLACSharp.FlacFrame flacFrame = (LibFLACSharp.FlacFrame)Marshal.PtrToStructure(frame, typeof(LibFLACSharp.FlacFrame));

            if (m_flacSamples == null)
            {
                // First time - Create Flac sample buffer
                m_samplesPerChannel = flacFrame.Header.BlockSize;
                m_flacSamples = new int[m_samplesPerChannel * m_flacStreamInfo.Channels];
                m_flacSampleIndex = 0;
            }

            // Iterate on all channels, copy the unmanaged channel bits (samples) to the a managed samples array
            for (int inputChannel = 0; inputChannel < m_flacStreamInfo.Channels; inputChannel++)
            {
                // Get pointer to channel bits, for the current channel
                IntPtr pChannelBits = Marshal.ReadIntPtr(buffer, inputChannel * IntPtr.Size);

                // Copy the unmanaged bits to managed memory
                Marshal.Copy(pChannelBits, m_flacSamples, inputChannel * m_samplesPerChannel, m_samplesPerChannel);
            }
        }

        /// <summary>
        /// FLAC Meta Call Back - libFlac notifies about one (or more) Meta frames.
        /// Note that there could be many types of Meta Frames but by default only the StreamInfo meta frame is returned
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <param name="userData"></param>
        private void FLAC_MetadataCallback(IntPtr context, IntPtr metadata, IntPtr userData)
        {
            LibFLACSharp.FLACMetaData flacMetaData = (LibFLACSharp.FLACMetaData)Marshal.PtrToStructure(metadata, typeof(LibFLACSharp.FLACMetaData));

            if (flacMetaData.MetaDataType == LibFLACSharp.FLACMetaDataType.StreamInfo)
            {
                GCHandle pinnedStreamInfo = GCHandle.Alloc(flacMetaData.Data, GCHandleType.Pinned);
                try
                {
                    m_flacStreamInfo = (LibFLACSharp.FLACStreamInfo)Marshal.PtrToStructure(
                        pinnedStreamInfo.AddrOfPinnedObject(),
                        typeof(LibFLACSharp.FLACStreamInfo));
                    m_totalSamples = ( long ) ( m_flacStreamInfo.TotalSamplesHi << 32 ) + ( long ) m_flacStreamInfo.TotalSamplesLo;
                }
                finally
                {
                    pinnedStreamInfo.Free();
                }
            }
        }

        /// <summary>
        /// FLAC Error Call Back - libFlac notifies about a decoding error
        /// </summary>
        /// <param name="context"></param>
        /// <param name="status"></param>
        /// <param name="userData"></param>
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

        private LibFLACSharp.FLACStreamInfo m_flacStreamInfo;
        private int m_samplesPerChannel;
        private long m_totalSamples = 0;

        private int[] m_flacSamples;
        private int m_flacSampleIndex;

        private byte[] m_NAudioSampleBuffer;
        private int m_playbackBufferOffset;

        private LibFLACSharp.Decoder_WriteCallback m_writeCallback;
        private LibFLACSharp.Decoder_MetadataCallback m_metadataCallback;
        private LibFLACSharp.Decoder_ErrorCallback m_errorCallback;
        #endregion
    }
}