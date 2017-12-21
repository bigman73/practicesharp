#region © Copyright 2010 Yuval Naveh, Practice Sharp. LGPL.
/* Practice Sharp
 
    © Copyright 2010, Yuval Naveh.
     All rights reserved.
 
    This file is part of Practice Sharp.

    Practice Sharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Practice Sharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser Public License for more details.

    You should have received a copy of the GNU Lesser Public License
    along with Practice Sharp.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace BigMansStuff.PracticeSharp.Core
{
     public enum InputChannelsModes {Left, Both, Right};

    /// <summary>
    /// Data class for containing preset values
    /// </summary>
    public class PresetData
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PresetData()
        {
            Reset();
        }

        #endregion

        #region Properties

        public float Volume { get; set; }
        public float Tempo { get; set; }
        public float Pitch { get; set; }
        public TimeSpan CurrentPlayTime { get; set; }
        public TimeSpan StartMarker { get; set; }
        public TimeSpan EndMarker { get; set; }
        public TimeSpan Cue { get; set; }
        public bool Loop { get; set; }
        public string Description { get; set; }

        public float LoEqValue { get; set; }
        public float MedEqValue { get; set; }
        public float HiEqValue { get; set; }

        public TimeStretchProfile TimeStretchProfile { get; set; }

        public bool RemoveVocals { get; set; }

        public InputChannelsModes InputChannelsMode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets the preset settings to Defaults
        /// </summary>
        public void Reset()
        {
            Volume = Properties.Settings.Default.DefaultVolume;
            Tempo = DefaultTempo;
            Pitch = DefaultPitch;
            CurrentPlayTime = TimeSpan.Zero;
            StartMarker = TimeSpan.Zero;
            EndMarker = TimeSpan.Zero;
            Cue = TimeSpan.Zero;
            Loop = false;
            Description = string.Empty;
            RemoveVocals = false;
            InputChannelsMode = InputChannelsModes.Both;

            LoEqValue = DefaultLoEq;
            MedEqValue = DefaultMedEq;
            HiEqValue = DefaultHiEq;

            TimeStretchProfile = TimeStretchProfileManager.DefaultProfile;
        }
        #endregion

        #region Constants

        public const float DefaultTempo = 1.0f; // 1 = 100%/regular speed
        public const int DefaultPitch = 0; // 0 = Regular pitch, in semi-tones

        public const float DefaultLoEq = 0;
        public const float DefaultMedEq = 0;
        public const float DefaultHiEq = 0;

        #endregion
    }
}
