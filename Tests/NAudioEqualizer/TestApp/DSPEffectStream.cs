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
using System.Collections.Generic;
using System.Text;
using NAudio.Wave;

namespace BigMansStuff.NAudio.Tests
{
    /// <summary>
    /// A simple DSP Effect Stream that allows applying one DSPEffect to the source wave stream
    /// </summary>
    public class DSPEffectStream: WaveStream
    {
        public WaveStream SourceStream { get; private set; }
        public DSPEffect ActiveDSPEffect { get; private set; }

        public DSPEffectStream(WaveStream sourceStream, DSPEffect dspEffect)
        {
            SourceStream = sourceStream;
            ActiveDSPEffect = dspEffect;
        }

        public override WaveFormat WaveFormat
        {
            get { return SourceStream.WaveFormat; }
        }

        public override long Length
        {
            get { return SourceStream.Length; }
        }

        public override long Position
        {
            get { return SourceStream.Position; }
            set { SourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = SourceStream.Read(buffer, offset, count);

            if (ActiveDSPEffect.Enabled)
            {
                if (SourceStream.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
                {
                    ByteAndFloatsConverter convertInputBuffer = new ByteAndFloatsConverter { Bytes = buffer };
                    ProcessDataIeeeFloat(convertInputBuffer, offset, bytesRead);
                }
                else
                {
                    // Do not process other types of streams
                }
            }

            return bytesRead;
        }

        /// <summary>
        /// Process the data (Ieee Floats) by applying the DSP Effect to each sample
        /// </summary>
        /// <param name="convertInputBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        private void ProcessDataIeeeFloat(ByteAndFloatsConverter convertInputBuffer, int offset, int count)
        {
            int index = 0;
            int sampleCount = count / ( sizeof( float ) );
            float sampleLeft;
            float sampleRight;
            while (index < sampleCount)
            {
                sampleLeft = convertInputBuffer.Floats[index];
                sampleRight = convertInputBuffer.Floats[index+1];

                if (sampleRight > 1)
                {
                    sampleRight = sampleRight + 0;
                }

                // Apply the DSP effect to the samples
                ActiveDSPEffect.Sample(ref sampleLeft, ref sampleRight);

                convertInputBuffer.Floats[index] = sampleLeft;
                convertInputBuffer.Floats[index + 1] = sampleRight;

                index += 2;
            }
        }
    }
}
