using System;
using System.Collections.Generic;
using System.Text;

namespace BigMansStuff.PracticeSharp.Core
{
    /// <summary>
    /// TimeStretchProfileManager - Central manager (singleton) of Time Stretch Profiles
    /// </summary>
    static class TimeStretchProfileManager
    {
        public static void Initialize()
        {
            TimeStretchProfiles = new Dictionary<string, TimeStretchProfile>();

            // TODO: Load all profiles from an XML file
            TimeStretchProfile profile = new TimeStretchProfile();
            profile.Id = "AutomaticAA";
            profile.UseAAFilter = true;
            profile.AAFilterLength = 128;
            profile.SeekWindow = 0;
            profile.Sequence = 0;
            profile.Overlap = 0;
            profile.Description = "Automatic with AA Filter";
            TimeStretchProfiles.Add(profile.Id, profile);

            profile = new TimeStretchProfile();
            profile.Id = "Practice#_RockPop";
            profile.UseAAFilter = true;
            profile.AAFilterLength = 192;
            profile.SeekWindow = 90;
            profile.Sequence = 30;
            profile.Overlap = 30;
            profile.Description = "Rock/Pop 1 with Strong AA Filter";
            TimeStretchProfiles.Add(profile.Id, profile);
            DefaultProfile = profile;


            profile = new TimeStretchProfile();
            profile.Id = "Practice#_RockPop2";
            profile.UseAAFilter = true;
            profile.AAFilterLength = 64;
            profile.SeekWindow = 120;
            profile.Sequence = 60;
            profile.Overlap = 60;
            profile.Description = "Rock/Pop 2 with Soft AA Filter";
            TimeStretchProfiles.Add(profile.Id, profile);

            profile = new TimeStretchProfile();
            profile.Id = "Practice#_SpeechAA";
            profile.UseAAFilter = true;
            profile.AAFilterLength = 128;
            profile.SeekWindow = 40;
            profile.Sequence = 10;
            profile.Overlap = 10;
            profile.Description = "Speech 1 with AA Filter";
            TimeStretchProfiles.Add(profile.Id, profile);

            profile = new TimeStretchProfile();
            profile.Id = "Practice#_Speech";
            profile.UseAAFilter = false;
            profile.AAFilterLength = 128;
            profile.SeekWindow = 30;
            profile.Sequence = 8;
            profile.Overlap = 8;
            profile.Description = "Speech 2 w/o AA Filter";
            TimeStretchProfiles.Add(profile.Id, profile);
        }

        public static TimeStretchProfile DefaultProfile {get; private set;}

        public static Dictionary<string, TimeStretchProfile> TimeStretchProfiles;
    }
}
