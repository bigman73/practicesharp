using System;

namespace Wav2Flac
{
    static class ConsoleProgress
    {
        #region Fields
        private static int lastProgress = -1;
        private static float lastPercent = 0;

        private static bool reset = true;
        private static bool operationCanceled = false;
        #endregion

        #region Properties
        public static bool Canceled
        {
            get { return operationCanceled; }
            set { operationCanceled = value; Update(); }
        }
        #endregion

        #region Methods
        public static void Reset()
        {
            reset = true;
            operationCanceled = false;
        }

        private static void Update()
        {
            Update(lastPercent);
        }

        public static void Update(int currentPosition, int maximum)
        {
            if (currentPosition < 0 || maximum < 1)
                throw new ArgumentOutOfRangeException("Values must be positive!");

            if (currentPosition > maximum)
                throw new ArgumentOutOfRangeException("currentPosition", "currentPosition must be less than, or equal to maximum!");

            Update((float)currentPosition / maximum);
        }

        public static void Update(long currentPosition, long maximum)
        {
            if (currentPosition < 0 || maximum < 1)
                throw new ArgumentOutOfRangeException("Values must be positive!");

            if (currentPosition > maximum)
                throw new ArgumentOutOfRangeException("currentPosition", "currentPosition must be less than, or equal to maximum!");

            Update((float)currentPosition / maximum);
        }

        public static void Update(float percent)
        {
            // Reserved for '[' ']', a space and percent text
            const int reserved = 2 + 6;

            // Sanity check
            if (percent < 0 || percent > 1)
                throw new ArgumentOutOfRangeException("percent", "Invalid progress value!");

            // Calculate the number of dashes and white spaces we need.
            int capacity = Console.BufferWidth;
            int dashes = (int)(percent * (capacity - reserved));
            int spaces = capacity - reserved - dashes;

            lastPercent = percent;

            // Only update the progress bar when there is a full percent change
            // Otherwise, there might be performance hits
            if (lastProgress != dashes || operationCanceled || reset)
            {
                string progressText = string.Format(
                    "[{0}{1}] {2}",

                    new string('-', dashes),
                    new string(' ', spaces),
                    operationCanceled ? "Abort" : string.Format("{0,5:P0}", percent));

                // Clear the last line, update text
                if (Console.CursorTop > 0 && !reset)
                    Console.CursorTop--;
                Console.Write(progressText);

                // Save the new state
                lastProgress = dashes;
                reset = false;
            }
        }
        #endregion
    }
}