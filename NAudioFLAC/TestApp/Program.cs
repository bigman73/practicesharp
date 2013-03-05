using System;
using NAudio.Wave;
using BigMansStuff.NAudio.FLAC;

namespace BigMansStuff.NAudio.FLAC
{
    class Program
    {
        static void Main(string[] args)
        {
            IWavePlayer waveOutDevice;
            WaveStream mainOutputStream;
            string fileName = @"test.flac";

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

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Playing FLAC..");
            Console.ResetColor();

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.WriteLine("Hit key to reposition..");
            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Seeking to new time: 00:00:10..");
            (mainOutputStream as WaveChannel32).CurrentTime = new TimeSpan(0, 0, 10);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
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

        private static WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            WaveStream readerStream = null;

            if (fileName.EndsWith(".wav"))
            {
                readerStream = new WaveFileReader(fileName);
            }
            else if (fileName.EndsWith(".flac"))
            {
                readerStream = new FLACFileReader(fileName);
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
