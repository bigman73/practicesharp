using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Wav2Flac
{
    class WavWriter : IDisposable
    {
        #region Fields
        private Stream output;
        BinaryWriter writer;

        private long audioDataBytes = 0;

        private long footerFieldPos1 = 0;
        private long footerFieldPos2 = 0;

        private const uint WaveHeaderSize = 38;
        private const uint WaveFormatSize = 18;

        private bool hasHeader = false;
        #endregion

        #region Methods
        public WavWriter(string output)
            : this(File.Create(output))
        {
        }

        public WavWriter(Stream output)
        {
            this.output = output;
            this.writer = new BinaryWriter(output);
        }

        public void Dispose()
        {
            if (this.output != null)
            {
                this.WriteFooter();
                this.output.Dispose();
            }

            this.output = null;
        }

        public void WriteHeader(int sampleRate, int bitDepth, int channels)
        {
            WaveHeader format = new WaveHeader(sampleRate, bitDepth, channels);

            writer.Write(new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' });
            footerFieldPos1 = output.Position; writer.Write(WaveHeaderSize);
            writer.Write(new byte[] { (byte)'W', (byte)'A', (byte)'V', (byte)'E' });
            writer.Write(new byte[] { (byte)'f', (byte)'m', (byte)'t', (byte)' ' });

            writer.Write(WaveFormatSize);
            writer.Write(format.wFormatTag);
            writer.Write(format.nChannels);
            writer.Write(format.nSamplesPerSec);
            writer.Write(format.nAvgBytesPerSec);
            writer.Write(format.nBlockAlign);
            writer.Write(format.wBitsPerSample);
            writer.Write(format.cbSize);

            writer.Write(new byte[] { (byte)'d', (byte)'a', (byte)'t', (byte)'a' });
            footerFieldPos2 = output.Position; writer.Write((uint)audioDataBytes);

            hasHeader = true;
        }

        private void WriteFooter()
        {
            output.Position = footerFieldPos1; writer.Write((uint)audioDataBytes + WaveHeaderSize);
            output.Position = footerFieldPos2; writer.Write((uint)audioDataBytes);
        }

        public void WriteInt16(int value)
        {
            int bytes = sizeof(short);
            int bits = bytes * 8;

            writer.Write((short)value);

            audioDataBytes += bytes;
        }

        public void WriteInt24(int value)
        {
            int bytes = sizeof(int) - sizeof(byte);

            writer.Write((byte)((value >> 0 * 8) & 0xFF));
            writer.Write((byte)((value >> 1 * 8) & 0xFF));
            writer.Write((byte)((value >> 2 * 8) & 0xFF));

            audioDataBytes += bytes;
        }
        #endregion

        #region Properties
        public long AudioDataBytes
        {
            get { return audioDataBytes; }
            set { audioDataBytes += value; }
        }

        public bool HasHeader
        {
            get { return hasHeader; }
        }
        #endregion
    }

    #region Native
    [StructLayout(LayoutKind.Sequential)]
    class WaveHeader
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;

        public WaveHeader(int rate, int bits, int channels)
        {
            wFormatTag = 1; // 1 = PCM 2 = Float
            nChannels = (short)channels;
            nSamplesPerSec = rate;
            wBitsPerSample = (short)bits;
            cbSize = 0;

            nBlockAlign = (short)(channels * (bits / 8));
            nAvgBytesPerSec = nSamplesPerSec * nBlockAlign;
        }
    }
    #endregion
}
