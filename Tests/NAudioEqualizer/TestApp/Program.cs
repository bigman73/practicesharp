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
using BigMansStuff.NAudio.Tests;
using NAudio.Wave;

namespace BigMansStuff.NAudio.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            IWavePlayer waveOutDevice;
            WaveStream mainOutputStream;
            WaveChannel32 waveChannel;
            string fileName = @"No One's Gonna Love You.mp3";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Initiailizing NAudio");
            Console.ResetColor();
            try
            {
                waveOutDevice = new DirectSoundOut(50);
            }
            catch (Exception driverCreateException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(String.Format("{0}", driverCreateException.Message));
                return;
            }

            mainOutputStream = CreateInputStream(fileName, out waveChannel);
            try
            {
                waveOutDevice.Init(mainOutputStream);
            }
            catch (Exception initException)
            {
                Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            Console.WriteLine("NAudio Total Time: " + waveChannel.TotalTime);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Note: Please use good speakers or headphones, it is hard to notice equalizer changes with cheap/crappy laptop speakers...!");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Playing File - Equalizer set to all Zero..");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seeking to new time: 00:01:00..");
            waveChannel.CurrentTime = new TimeSpan(0, 1, 0);
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hit key for next demo..");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Playing File - Equalizer set to Full Treble/Fully Supressed Med..");
            waveOutDevice.Pause();
            m_eqEffect.HiGainFactor.Value = m_eqEffect.HiGainFactor.Maximum;
            m_eqEffect.MedGainFactor.Value = m_eqEffect.MedGainFactor.Minimum;
            m_eqEffect.LoGainFactor.Value = 0;
            m_eqEffect.OnFactorChanges();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seeking to new time: 00:01:00..");
            waveChannel.CurrentTime = new TimeSpan(0, 1, 0);
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hit key for next demo..");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Playing File - Equalizer set to Full Bass..");
            waveOutDevice.Pause();
            m_eqEffect.HiGainFactor.Value = 0;
            m_eqEffect.MedGainFactor.Value = 0;
            m_eqEffect.LoGainFactor.Value = m_eqEffect.LoGainFactor.Maximum;
            m_eqEffect.OnFactorChanges();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seeking to new time: 00:01:00..");
            waveChannel.CurrentTime = new TimeSpan(0, 1, 0);
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hit key for next demo..");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Playing File - Equalizer set to Full Bass and Full Treble..");
            waveOutDevice.Pause();
            m_eqEffect.HiGainFactor.Value = m_eqEffect.HiGainFactor.Maximum;
            m_eqEffect.MedGainFactor.Value = 0;
            m_eqEffect.LoGainFactor.Value = m_eqEffect.LoGainFactor.Maximum;
            m_eqEffect.OnFactorChanges();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seeking to new time: 00:01:00..");
            waveChannel.CurrentTime = new TimeSpan(0, 1, 0);
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hit key for next demo..");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Playing File - Equalizer set to Full Med, Fully Supressed Bass and Treble..");
            waveOutDevice.Pause();
            m_eqEffect.HiGainFactor.Value = m_eqEffect.HiGainFactor.Minimum;
            m_eqEffect.MedGainFactor.Value = m_eqEffect.MedGainFactor.Maximum;
            m_eqEffect.LoGainFactor.Value = m_eqEffect.LoGainFactor.Minimum;
            m_eqEffect.OnFactorChanges();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Seeking to new time: 00:01:00..");
            waveChannel.CurrentTime = new TimeSpan(0, 1, 0);
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.WriteLine("Hit key to stop..");
            Console.ResetColor();
            Console.ReadKey();

            waveOutDevice.Stop();

            Console.WriteLine("Finished..");

            mainOutputStream.Dispose();

            waveOutDevice.Dispose();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static WaveStream CreateInputStream(string fileName, out WaveChannel32 waveChannel)
        {
            WaveStream readerStream = null;

            if (fileName.EndsWith(".wav"))
            {
                readerStream = new WaveFileReader(fileName);
            }
            else if (fileName.EndsWith(".mp3"))
            {
                readerStream = new Mp3FileReader(fileName);
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

            waveChannel = new WaveChannel32(readerStream);
            
            // **************************************************************************************
            // Chain the DSP Equalizer Effect - DSP Equalizer must come AFTER WaveChannel32 because it
            //  was coded to expect Ieee Float samples, NOT PCM samples
            m_eqEffect = new EqualizerEffect();
            m_eqEffect.OnFactorChanges();

            readerStream = new DSPEffectStream(waveChannel, m_eqEffect);
            // **************************************************************************************

            return readerStream;
        }

        private static EqualizerEffect m_eqEffect;
    }
}
