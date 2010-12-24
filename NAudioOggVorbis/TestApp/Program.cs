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
using NAudio.Wave;
using BigMansStuff.NAudio.Ogg;

namespace BigMansStuff.NAudio.Ogg
{
    class Program
    {
        static void Main(string[] args)
        {
            IWavePlayer waveOutDevice;
            WaveStream mainOutputStream;
            string fileName = @"..\..\Audio\test.ogg";

            Console.WriteLine("Initiailizing NAudio");
            try
            {
                waveOutDevice = new DirectSoundOut(50);
            }
            catch (Exception driverCreateException)
            {
                Console.WriteLine(String.Format("{0}", driverCreateException.Message));
                return;
            }

            mainOutputStream = CreateInputStream(fileName);
            try
            {
                waveOutDevice.Init(mainOutputStream);
            }
            catch (Exception initException)
            {
                Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            Console.WriteLine("NAudio Total Time: " + (mainOutputStream as WaveChannel32).TotalTime);

            Console.WriteLine("Playing Ogg Vorbis..");

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();


            Console.ReadKey();

            Console.WriteLine("Seeking to new time: 00:01:00..");

            (mainOutputStream as WaveChannel32).CurrentTime = new TimeSpan(0, 1, 0);

            Console.ReadKey();

            waveOutDevice.Stop();

            Console.WriteLine("Finished..");

            mainOutputStream.Dispose();

            waveOutDevice.Dispose();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            WaveStream readerStream = null;

            if (fileName.EndsWith(".wav"))
            {
                readerStream = new WaveFileReader(fileName);
            }
            else if (fileName.EndsWith(".ogg"))
            {
                readerStream = new OggVorbisFileReader(fileName);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }


            // Provide PCM conversion if needed
            if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
            {
                readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                readerStream = new BlockAlignReductionStream(readerStream);
            }

            // Provide conversion to 16 bits if needed
            if (readerStream.WaveFormat.BitsPerSample != 16)
            {
                var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                16, readerStream.WaveFormat.Channels);
                readerStream = new WaveFormatConversionStream(format, readerStream);
            }

            inputStream = new WaveChannel32(readerStream);

            return inputStream;
        }
    }
}
