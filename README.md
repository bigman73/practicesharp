# practicesharp
A playback practice tool for musicians that allows slowing down, changing pitch, defining presets and loops on music files.

Current stable release: 2.0.6
![alt screenshot](https://github.com/bigman73/practicesharp/blob/master/Docs/Practice%23_ScreenShot.png)

# Features
* Playback of the following formats: MP3, WAV, WMA, AIFF, Ogg Vorbis, FLAC and M4A
* Speed change - The ability to slow down or speed up music without changing its pitch
* Pitch change - The ability to change the pitch of music up or down without changing its speed. New! (1.4.6) - Accurate half semi-tone increments
* Loops - Start and End Marker with Cue (waiting a few seconds before the loop starts, useful to get hands back into position)
* Persistent Presets - The biggest difference currently between Practice# and the other tools out there. In fact, that is why this tool was written by me in the first place. When you practice some song over and over for a while it is extremely useful to have some presets of your practice habits for this song. For example: One preset can be for the intro and one for the verse or two presets can be for the solo - one regular speed and one slowed down to 75% with a cue of 2 seconds.
* Cross Windows Platform - Runs on all latest Windows Operating systems, Tested on Windows XP Pro SP3 32 bit, Vista Home Premium 32 Bit, Windows 7 Pro 64 bit and Windows 10.
* 3 Band Equalizer
* Voice Suppression
* Input channel selection (<NEW> - Version 2.0.1.0) - Select if the left, right or both channels are used from the input file. Very useful for double mono practice files that have for example drums on the left channel and guitar or bass on the right channel (common in instrument teaching books)


# Usage
A youtube video that demonstrates Practice# usage:

[![Practice# demo of usage](https://img.youtube.com/vi/rqWr7BJjhx8/0.jpg)](https://www.youtube.com/watch?v=rqWr7BJjhx8)

# Installation
## Pre-conditions
* Latest Windows OS - XP, Vista, 7, 10
* .NET Framework 4.5 (the MSI installer doesn't install .NET Framework 4.5)
* Visual Studio Express for Desktop 2012+

## Installation steps
1. Download the Practice# MSI installer to your local machine - https://github.com/bigman73/practicesharp/tree/master/Installer/MSI
2. Run it
3. That's it!

## WMA Support
As of Sep 2015 - naudio.wma has become an official Nuget project under NAudio open source project
https://github.com/naudio/NAudio.Wma

# Code Project article
Documentation of the design can be found in:
https://www.codeproject.com/Articles/129929/PracticeSharp-or-Practice-A-Utility-for-Practicing

# License 
License: ![alt LGPLV3](https://www.gnu.org/graphics/lgplv3-147x51.png)

# Credits
* NAudio - https://github.com/naudio/NAudio (Ms-PL)
* NAudioWMA - https://github.com/naudio/NAudio.Wma (Ms-PL)
* SoundTouch - https://www.surina.net/soundtouch (LGPL)
* Vorbis# (csvorbis) - https://github.com/mono/csvorbis (LGPL) 
* Some ideas are based on DragonOgg - https://sourceforge.net/projects/dragonogg/ (LGPL)
* WMA playback support is based on a Code Project article by Idael Cardoso - "C# Windows Media Format SDK Translation", http://www.codeproject.com/KB/audio-video/ManWMF.aspx 
* FLAC playback support is using NAudioFlac - https://www.nuget.org/packages/NAudio.Flac/ 
* WiX - The Windows Installer XML (WiX) http://wix.codeplex.com/ (Common Public License Version 1.0)
* WMA Sample file - http://samplephotovideo.com/2015/12/wma-windows-media-sample-audio-file-wma/
* M4A Sample file - https://github.com/robovm/apple-ios-samples
* Ogg Sample file- https://en.wikipedia.org/wiki/File:ACDC_-_Back_In_Black-sample.ogg
