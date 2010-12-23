using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Wav2Flac
{
    class WavReader : IDisposable
    {
        #region Fields
        private Stream input;
        private WaveFormat format;

        private int uRiffHeader;
        private int uRiffHeaderSize;

        private int uWaveHeader;

        private int uFmtHeader;
        private int uFmtHeaderSize;

        private int uDataHeader;
        private int nTotalAudioBytes;
        #endregion

        #region Properties
        public Stream InputStream
        {
            get { return input; }
        }

        public int Channels
        {
            get { return format.nChannels; }
        }

        public int SampleRate
        {
            get { return format.nSamplesPerSec; }
        }

        public int BitDepth
        {
            get { return format.wBitsPerSample; }
        }

        public int Bitrate
        {
            get { return BitDepth * SampleRate * Channels; }
        }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromSeconds(nTotalAudioBytes * 8 / Bitrate); }
        }
        #endregion

        #region Methods
        public WavReader(string input)
            : this(File.OpenRead(input))
        {
        }

        public WavReader(Stream input)
        {
            BinaryReader reader = new BinaryReader(input);

            // Ensure there are enough bytes to read
            if (input.Length < Marshal.SizeOf(typeof(WaveFormat)))
                throw new ApplicationException("Input stream is too short (< wave header size)!");

            this.input = input;

            // Ensure this is a correct WAVE file to avoid unnecessary reading, processing, etc.
            uRiffHeader = reader.ReadInt32();
            uRiffHeaderSize = reader.ReadInt32();
            uWaveHeader = reader.ReadInt32();

            if (uRiffHeader != 0x46464952 /* RIFF */ ||
                uWaveHeader != 0x45564157 /* WAVE */)
                throw new ApplicationException("Invalid WAVE header!");

            // Read all WAVE chunks
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int type = reader.ReadInt32();
                int size = reader.ReadInt32();

                long last = reader.BaseStream.Position;

                switch (type)
                {
                    case 0x61746164: /* data */
                        uDataHeader = type;
                        nTotalAudioBytes = size;
                        break;

                    case 0x20746d66: /* fmt  */
                        uFmtHeader = type;
                        uFmtHeaderSize = size;

                        format.wFormatTag = reader.ReadInt16();
                        format.nChannels = reader.ReadInt16();
                        format.nSamplesPerSec = reader.ReadInt32();
                        format.nAvgBytesPerSec = reader.ReadInt32();
                        format.nBlockAlign = reader.ReadInt16();
                        format.wBitsPerSample = reader.ReadInt16();
                        format.cbSize = reader.ReadInt16();
                        break;
                }

                if (uDataHeader == 0) // Do not skip the 'data' chunk size
                    reader.BaseStream.Position = last + size;
                else
                    break;
            }

            // Ensure that samples are integers (e.g. not floating-point numbers)
            if (format.wFormatTag != 1) // 1 = PCM 2 = Float
                throw new ApplicationException("Format tag " + format.wFormatTag + " is not supported!");

            // Ensure that samples are 16 or 24-bit
            if (format.wBitsPerSample != 16 && format.wBitsPerSample != 24)
                throw new ApplicationException(format.wBitsPerSample + " bits per sample is not supported by FLAC!");
        }

        public void Dispose()
        {
            if (this.input != null)
                this.input.Dispose();

            this.input = null;
        }
        #endregion
    }

    #region Native
    struct WaveFormat
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
    }
    #endregion
}
