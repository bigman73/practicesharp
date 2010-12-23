using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wav2Flac
{
    class FlacReader : IDisposable
    {
        #region Api
        const string Dll = "LibFlac";

        [DllImport(Dll)]
        static extern IntPtr FLAC__stream_decoder_new();

        [DllImport(Dll)]
        static extern bool FLAC__stream_decoder_finish(IntPtr context);

        [DllImport(Dll)]
        static extern bool FLAC__stream_decoder_delete(IntPtr context);

        [DllImport(Dll)]
        static extern int FLAC__stream_decoder_init_file(IntPtr context, string filename, WriteCallback write, MetadataCallback metadata, ErrorCallback error, IntPtr userData);

        [DllImport(Dll)]
        static extern bool FLAC__stream_decoder_process_single(IntPtr context);

        [DllImport(Dll)]
        static extern bool FLAC__stream_decoder_process_until_end_of_stream(IntPtr context);

        [DllImport(Dll)]
        static extern long FLAC__stream_decoder_get_total_samples(IntPtr context);

        // Callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void WriteCallback(IntPtr context, IntPtr frame, IntPtr buffer, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void ErrorCallback(IntPtr context, DecodeError status, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void MetadataCallback(IntPtr context, IntPtr metadata, IntPtr userData);

        private const int FlacMaxChannels = 8;

        struct FlacFrame
        {
            public FrameHeader Header;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FlacMaxChannels)]
            public FlacSubFrame[] Subframes;
            public FrameFooter Footer;
        }

        struct FrameHeader
        {
            public int BlockSize;
            public int SampleRate;
            public int Channels;
            public int ChannelAssignment;
            public int BitsPerSample;
            public FrameNumberType NumberType;
            public long FrameOrSampleNumber;
            public byte Crc;
        }

        struct FlacSubFrame
        {
            public SubframeType Type;
            public IntPtr Data;
            public int WastedBits;
        }

        struct FrameFooter
        {
            public ushort Crc;
        }

        enum FrameNumberType
        {
            Frame,
            Sample
        }

        enum SubframeType
        {
            Constant,
            Verbatim,
            Fixed,
            LPC
        }

        enum DecodeError
        {
            LostSync,
            BadHeader,
            FrameCrcMismatch,
            UnparsableStream
        }
        #endregion

        #region Fields
        private IntPtr context;
        private Stream stream;
        private BinaryReader reader;
        private WavWriter writer;

        private int inputBitDepth;
        private int inputChannels;
        private int inputSampleRate;

        private int[] samples;
        private float[] samplesChannel;

        private long processedSamples = 0;
        private long totalSamples = -1;

        private WriteCallback write;
        private MetadataCallback metadata;
        private ErrorCallback error;
        #endregion

        #region Methods
        public FlacReader(string input, WavWriter output)
        {
            if (output == null)
                throw new ArgumentNullException("WavWriter");

            stream = File.OpenRead(input);
            reader = new BinaryReader(stream);
            writer = output;

            context = FLAC__stream_decoder_new();

            if (context == IntPtr.Zero)
                throw new ApplicationException("FLAC: Could not initialize stream decoder!");

            write = new WriteCallback(Write);
            metadata = new MetadataCallback(Metadata);
            error = new ErrorCallback(Error);

            if (FLAC__stream_decoder_init_file(context,
                                               input, write, metadata, error,
                                               IntPtr.Zero) != 0)
                throw new ApplicationException("FLAC: Could not open stream for reading!");
        }

        public void Dispose()
        {
            if (context != IntPtr.Zero)
            {
                Check(
                    FLAC__stream_decoder_finish(context),
                    "finalize stream decoder");

                Check(
                    FLAC__stream_decoder_delete(context),
                    "dispose of stream decoder instance");

                context = IntPtr.Zero;
            }
        }

        public void Close()
        {
            Dispose();
        }

        private void Check(bool result, string operation)
        {
            if (!result)
                throw new ApplicationException(string.Format("FLAC: Could not {0}!", operation));
        }
        #endregion

        #region Callbacks
        private void Write(IntPtr context, IntPtr frame, IntPtr buffer, IntPtr userData)
        {
            FlacFrame f = (FlacFrame)Marshal.PtrToStructure(frame, typeof(FlacFrame));

            int samplesPerChannel = f.Header.BlockSize;

            inputBitDepth = f.Header.BitsPerSample;
            inputChannels = f.Header.Channels;
            inputSampleRate = f.Header.SampleRate;

            if (!writer.HasHeader)
                writer.WriteHeader(inputSampleRate, inputBitDepth, inputChannels);

            if (totalSamples < 0)
                totalSamples = FLAC__stream_decoder_get_total_samples(context);

            if(samples == null) samples = new int[samplesPerChannel * inputChannels];
            if (samplesChannel == null) samplesChannel = new float[inputChannels];

            for (int i = 0; i < inputChannels; i++)
            {
                IntPtr pChannelBits = Marshal.ReadIntPtr(buffer, i * IntPtr.Size);

                Marshal.Copy(pChannelBits, samples, i * samplesPerChannel, samplesPerChannel);
            }

            // For each channel, there are BlockSize number of samples, so let's process these.
            for (int i = 0; i < samplesPerChannel; i++)
            {
                for (int c = 0; c < inputChannels; c++)
                {
                    int v = samples[i + c * samplesPerChannel];

                    switch (inputBitDepth / 8)
                    {
                        case sizeof(short): // 16-bit
                            writer.WriteInt16(v);
                            break;

                        case sizeof(int) - sizeof(byte): // 24-bit
                            writer.WriteInt24(v);
                            break;

                        default:
                             throw new NotSupportedException("Input FLAC bit depth is not supported!");
                    }
                }

                processedSamples += 1;
            }

            // Show progress
            if (totalSamples > 0)
                ConsoleProgress.Update(processedSamples, totalSamples);
        }

        private void Metadata(IntPtr context, IntPtr metadata, IntPtr userData)
        {
            // TODO
        }

        private void Error(IntPtr context, DecodeError status, IntPtr userData)
        {
            throw new ApplicationException(string.Format("FLAC: Could not decode frame: {0}!", status));
        }

        public void Process()
        {
            //    while (reader.BaseStream.Position < reader.BaseStream.Length)
            //        Check(
            //            FLAC__stream_decoder_process_single(context),
            //            "process single");

            Check(
                FLAC__stream_decoder_process_until_end_of_stream(context),
                "process until eof");
        }
        #endregion
    }
}
