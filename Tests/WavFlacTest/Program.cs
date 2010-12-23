using System;
using System.IO;

namespace Wav2Flac
{
    class Program
    {
        #region Entrypoint
        static void Main(string[] args)
        {
            // Test files are to be placed in the 'wav' folder.
            // The output FLAC files will be crated in 'flac'.

            // If you are missing the test WAV files, please download the following and place in 'wav':
            // Username: HD     Password: 2L
            // http://www.lindberg.no/hires/test/2L50SACD_tr1_96k_stereo.wav
            // http://www.lindberg.no/hires/test/2L50SACD_tr01_multi_48.wav
            // http://www.lindberg.no/hires/test/2L53SACD_04_stereo_96k.wav
            // Courtesy of 2L http://www.2l.no/hires/index.html

//            Test("2L50SACD_tr1_96k_stereo.wav", "B.Britten: Simple Symphony, Op. 4 - Boisterous Bourrée"); //        2.0 96/24
//            Test("2L50SACD_tr01_multi_48.wav", "B.Britten: Simple Symphony, Op. 4 - Boisterous Bourrée"); //         5.1 48/24
//            Test("2L53SACD_04_stereo_96k.wav", "J. Haydn: String Quartet in D, Op. 76, No. 5 - Finale - Presto"); // 2.0 96/24
//            Test("FlacTest.flac", "FLAC Decode Test"); // Can be mono/stereo/5.1/7.1 and either 16- or 24-bit
            Test("1.flac", "FLAC Decode Test"); // Can be mono/stereo/5.1/7.1 and either 16- or 24-bit

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        #endregion

        #region Test Cases
        static void Test(string input, string text)
        {
            try
            {
                Console.WriteLine("Running encode: {0}", text);
                Console.WriteLine();

                Run(input, text);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("{0} encoded successfully!", input);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        static void Run(string input, string text)
        {
            ConsoleProgress.Reset();

            bool isInputFlac = Path.GetExtension(input).ToUpperInvariant() == ".FLAC";

            if (isInputFlac)
            {
                // FLAC -> WAV
                string inputFile = Path.Combine("flac", input);
                string outputFile = Path.Combine("wav", Path.ChangeExtension(input, ".wav"));

                if (!File.Exists(inputFile))
                    throw new ApplicationException("Input file " + inputFile + " cannot be found!");

                using (WavWriter wav = new WavWriter(outputFile))
                using (FlacReader flac = new FlacReader(inputFile, wav))
                    flac.Process();
            }
            else
            {
                // WAV -> FLAC
                string inputFile = Path.Combine("wav", input);
                string outputFile = Path.Combine("flac", Path.ChangeExtension(input, ".flac"));

                if (!File.Exists(inputFile))
                    throw new ApplicationException("Input file " + inputFile + " cannot be found!");

                using (WavReader wav = new WavReader(inputFile))
                {
                    using (FlacWriter flac = new FlacWriter(File.Create(outputFile), wav.BitDepth, wav.Channels, wav.SampleRate))
                    {
                        // Buffer for 1 second's worth of audio data
                        byte[] buffer = new byte[1 * wav.Bitrate / 8];
                        int bytesRead;

                        do
                        {
                            ConsoleProgress.Update(wav.InputStream.Position, wav.InputStream.Length);

                            bytesRead = wav.InputStream.Read(buffer, 0, buffer.Length);
                            flac.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);

                        // Finished!
                    }
                }
            }
        }
        #endregion
    }
}
