# practicesharp
A playback practice tool for musicians that allows slowing down, changing pitch, defining presets and loops on music files.

Current Version: 2.0.4

# Features
* Playback of the following formats: MP3, WAV, WMA, AIFF, Ogg Vorbis and FLAC
* Speed change - The ability to slow down or speed up music without changing its pitch
* Pitch change - The ability to change the pitch of music up or down without changing its speed. New! (1.4.6) - Accurate half semi-tone increments
* Loops - Start and End Marker with Cue (waiting a few seconds before the loop starts, useful to get hands back into position)
* Persistent Presets - The biggest difference currently between Practice# and the other tools out there. In fact, that is why this tool was written by me in the first place. When you practice some song over and over for a while it is extremely useful to have some presets of your practice habits for this song. For example: One preset can be for the intro and one for the verse or two presets can be for the solo - one regular speed and one slowed down to 75% with a cue of 2 seconds.
* Cross Windows Platform - Runs on all latest Windows Operating systems, Tested on Windows XP Pro SP3 32 bit, Vista Home Premium 32 Bit, Windows 7 Pro 64 bit and Windows 10.
* 3 Band Equalizer
* Voice Suppression
* Input channel selection (<NEW> - Version 2.0.1.0) - Select if the left, right or both channels are used from the input file. Very useful for double mono practice files that have for example drums on the left channel and guitar or bass on the right channel (common in instrument teaching books)

## WMA Support
As of Sep 2015 - naudio.wma has become project (github & nuget ) as an official part of the NAudio open source project
https://github.com/naudio/NAudio.Wma

![alt screenshot](https://github.com/bigman73/practicesharp/blob/master/Docs/Practice%23_ScreenShot.png)


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

# Code Project article
Documentation of thd design can be found in:
https://www.codeproject.com/Articles/129929/PracticeSharp-or-Practice-A-Utility-for-Practicing

# License 
License: ![alt LGPLV3](https://www.gnu.org/graphics/lgplv3-147x51.png)

# Credits
* NAudio - https://github.com/naudio/NAudio
* NAudioWMA - https://github.com/naudio/NAudio.Wma
* SoundTouch - https://www.surina.net/soundtouch
* WMA Sample File - http://samplephotovideo.com/2015/12/wma-windows-media-sample-audio-file-wma/
