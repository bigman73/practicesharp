using System;
using System.Collections.Generic;
using System.Text;

namespace BigMansStuff.PracticeSharp.Core
{
    /// <summary>
    /// Time Stretching Profile for tuning the SoundTouch algorithm
    /// </summary>
    /// <see cref="http://www.surina.net/article/time-and-pitch-scaling.html"/>
    public class TimeStretchProfile
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public bool UseAAFilter {get; set;}
        public int AAFilterLength {get; set;}
        public int Overlap {get; set;}
        public int Sequence {get; set;}
        public int SeekWindow { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
