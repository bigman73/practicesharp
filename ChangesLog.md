# Changes Log #
## 1.7.1: (FUTURE) ##
  * Localization - support of languages (English, German, Hebrew)
  * Export output to file

## 1.7.0: (SOON) ##
(FUTURE)
  * Upgraded NAudio to 1.7
  * Upgraded SoundTouch to 1.8
  * Upgraded .NET boot strap installer to 2.3
  * Moved from .NET 2.0 to .NET 3.5
  * Bug fixes:
    * https://code.google.com/p/practicesharp/issues/detail?id=13&can=1
    * https://code.google.com/p/practicesharp/issues/detail?id=14&can=1
    * Major issue with FLAC play back (re-positioning).  https://code.google.com/p/practicesharp/issues/detail?id=18&can=1

## 1.6.4: ##
Released version: 3/20/2013
  * SoundTouch library updated to 1.7 (optimizations and fixes by library author)
  * NAudio library updated to 1.6 (optimizations and fixes by library author)
  * Change in logic: Cue now occurs in front of the loop instead of at the end
  * Change: Pitch resolution is now in 1/4 semi-tones, instead of 1/2 semi-tones
  * Bug fixes:
    * Major issue - http://code.google.com/p/practicesharp/issues/detail?id=10
    * Major issue - http://code.google.com/p/practicesharp/issues/detail?id=12
    * http://code.google.com/p/practicesharp/source/detail?r=288
    * http://code.google.com/p/practicesharp/source/detail?r=290
    * http://code.google.com/p/practicesharp/source/detail?r=292
    * http://code.google.com/p/practicesharp/issues/detail?id=8
    * http://code.google.com/p/practicesharp/issues/detail?id=9


## 1.5.0: ##
Released: 3/2/2012
  * Fixed random crash/hang when loading recent files - DirectSound is not stable in NAudio, moved instead to WaveOut (XP) or Wasapi (Vista,7)
  * Fixed speed and pitch track bar mouse behavior. Values are now rounded up properly and the ticks are 'sticky'
  * Added a 'Show technical log' (F12) use-case. It is useful for viewing & sending the log if things don't work properly for some reason
  * Fixed issue when loading files after an existing was loaded. There was a short playback of the old song. (SoundTouch buffers not flushed properly and had some left over samples). Not affecting stability, but annoying.
  * **New feature!** Vocal Suppression (AKA Voice Removal or Karaoke), note: works on Stereo files only

## 1.4.1: ##
Released: 2/10/2012
  * 8 Presets (instead of 4)
  * [Keyboard shortcuts](Keyboard.md)
  * Added a new Use-Case: Preset Quick-Write (Using Ctrl+W)
  * Improved look and feel (glass buttons)
  * Changed pitch into accurate semi-tones intervals
  * Fixed a minor bug with Loop reset to start

## 1.3.0: ##
Released: 1/5/2012
  * Recompiled with new versions of NAudio and SoundTouch libraries
  * Support for AIFF Playback
  * Slight change of Graphics

## 1.2.0: ##
Released: 2/9/2011
  * **Improved slow down playback quality** by fine tuning the SoundTouch engine. There is a **significant improvement** in sound quality when playback is slowed down, in particular for singing/speech parts but also for music.
> Manual settings are now used instead of the default automatic provide by Sound Touch.
  * Added TimeStretchProfiles, to support custom tuning of the SoundTouch engine.
  * Fixed minor bug with positionLabel handling: Clicking on it was not working in Pause mode
  * Position Reset (Back to start) keyboard changed from Home (used by other TrackBars by default) to F5

NOTE: Version 1.1.0 was experimental, hence the jump from 1.0.1 to 1.2.0

## 1.0.1: No longer a beta :) ##
  * Added Wix/MSI setup, with dotNetInstaller boot strapper
  * 'Initialized' Status -> Renamed to 'Ready'
  * Fix: When application was loaded, the previous file did not show the loop boundaries ("bar"). Only after playing the file, it then showed up.
  * Major bug fix: Stopping the audio was not done correctly and caused threading issues (lockups) and/or crashes (was noticeable in particular on slower machines I've tested)

NOTE: I apologize but I released a bad 1.0 version, 1.0.1 replaces 1.0.

## 0.6 (Beta 6): ##
  * FLAC Playback Support
  * (Core) Fixed: WMA playback was broken
  * (Core) Equalizer fine tuning: Lo (Bass) more pronounced

## 0.5 (Beta 5): ##

  * Bug fixes (Core) - Application sometimes crashed when a file was loaded, Stop/Terminate playback logic was incorrect
  * Bug fixes (UI) - Cancel Write mode did not work properly, it caused the preset to be re-selected loosing the current settings, Current Up/Down controls did not transition properly (e.g. decrementing the seconds below zero did not affect minutes)
  * Added logging using NLog (log file resides in %LOCALAPPDATA%\PracticeSharp\Practice#.log)
  * Added central exception handlers that log the exceptions and show a proper dialog

## 0.4 (Beta 4): ##
  * Equalizer controls
  * Keyboard shortcuts
  * Fix: SoundTouch DLL now works properly with SoundTouchSharp in Release Mode
  * Fix: Flickers, Tab Order, Tool Tips, cosmetics

## 0.3 (Beta 3): ##
  * Added playback support for WMA
  * Integrated with new version NAudio 64392. NAudio has incoporated WMA support that was developed in Practice# (based on original code from Idael Cardaso)
  * Bug fixes, mainly stability issues with preset saving and when application was shut down
  * All Playback initialization and termination fully moved into the audio processing thread (not done on main UI thread any longer)
  * Improved playback hiccups - Mainly due to synchronous events from PracticeSharpLogic to MainForm. Now PracticeSharpLogic events are asynchronous.
  * Added support for a command line argument - the filename (to allow running Practice# from command line or through a future shell extension)
  * Code clean up

## 0.2 (Beta 2): ##
  * Added playback support for Ogg Vorbis (using Vorbis#, based one some ideas from DragonOgg)
  * Added a new library/NAudio plugin to support Ogg Vorbis in NAudio (Standalone TestApp also available)
  * Fixed some issues with play back
  * Changed default volume to 50%
  * Reset all settings/UI Controls when loading files. New files that have never been used, have no presets and as a result all the previous file information stays (Tempo, Pitch, Volume, Presets)

## 0.1 (Beta 1): ##
  * Improved Updated Version checker, it now works asynchronously thus not blocking the application on load
  * Fixed: The playTimeTrackBar was jumping slightly back and forth when changing presets
  * NAudio: Using latest version (63489)

## 0.0.15 (Alpha 15): ##
  * Added Updated Version checker (checks if latest version on GoogleCode is newer than installed version) - If new version was released then the application provides a dialog and jumps to Practice# GoogleCode downloads page

## 0.0.14 (Alpha 14): ##
  * Added top main menu
  * Added About dialog
  * Added Recent Files

## 0.0.13 (Alpha 13): ##
  * Enhanced audio playback quality (strong AA filter)
  * Fixed behavior of Start and End loop marker controls to affect time of all 3 controls (Minute,Seconds,Milli)
  * Fixed stability issues

## 0.0.12 (Alpha 12): ##
  * Changed preset UI to contain text inside the Preset Control
  * Fixed stability issues

## 0.0.11 (Alpha 11): ##
  * First version release to GoogleCode