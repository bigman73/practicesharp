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
using NAudio.Wave;
using System.ComponentModel;
using System.Threading;
using System.IO;
using BigMansStuff.NAudio.Ogg;
using NAudio.WindowsMediaFormat;
using NLog;

namespace BigMansStuff.PracticeSharp.Core
{
    /// <summary>
    /// Core Logic (Back-End) for PracticeSharp application
    /// Acts as a mediator between the User Interface, NAudio and SoundSharp layers
    /// </summary>
    public class PracticeSharpLogic: IDisposable
    {
        #region Logger
        private static Logger m_logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Public Methods
        
        /// <summary>
        /// Initialize the PracticeSharp core logic layer
        /// </summary>
        public void Initialize()
        {
            ChangeStatus(Statuses.Initializing);
            m_startMarker = TimeSpan.Zero;
            m_endMarker = TimeSpan.Zero;
            m_cue = TimeSpan.Zero;
        }

        /// <summary>
        /// Terminates all current play back and resources
        /// </summary>
        public void Terminate()
        {
            ChangeStatus(Statuses.Terminating);

            Stop();

            // Release lock, just in case the thread is locked
            lock (FirstPlayLock)
            {
                Monitor.Pulse(FirstPlayLock);
            }

            ChangeStatus(Statuses.Terminated);
        }

        /// <summary>
        /// Loads the given file name and start playing it
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFile(string filename)
        {
            // Stop a previous file running
            if (m_waveOutDevice != null)
            {
                Stop();
            }

            m_filename = filename;
            m_status = Statuses.Loading;

            // TODO: CHECK WHY RE-ENTRY HAPPENS?!?
            if ( m_audioProcessingThread != null)
            {
                System.Diagnostics.Debug.Assert(true, "m_audioProcessingThread re-entry - already exists");
            }

            // Create the Audio Processing Worker (Thread)
            m_audioProcessingThread = new Thread( new ThreadStart( audioProcessingWorker_DoWork) );
            m_audioProcessingThread.IsBackground = true;
            m_audioProcessingThread.Priority = ThreadPriority.Highest;
            // Important: MTA is needed for WMFSDK to function properly (for WMA support)
            // All WMA (COM) related actions MUST be done within the Thread's MTA otherwise there is a COM exception
            m_audioProcessingThread.SetApartmentState( ApartmentState.MTA );

            // Allow initialization to start >>Inside<< the thread, the thread will stop and wait for a pulse
            m_audioProcessingThread.Start();

            // Wait for thread for finish initialization
            lock (InitializedLock)
            {
                Monitor.Wait(InitializedLock);
            }

        }

        /// <summary>
        /// Starts/Resumes play back of the current file
        /// </summary>
        public void Play()
        {
            // Not playing now - Start the audio processing thread
            if (m_status == Statuses.Initialized)
            {
                lock (FirstPlayLock)
                {
                    Monitor.Pulse(FirstPlayLock);
                }
            }
            else if (m_status == Statuses.Pausing)
            {
                m_waveOutDevice.Play();
                ChangeStatus(Statuses.Playing);
            }
        }

        /// <summary>
        /// Stops the play back
        /// </summary>
        /// <remarks>
        /// This is a hard stop that cannot be resumed.
        ///  </remarks>
        public void Stop()
        {
           /* if (m_status == Statuses.Initializing || m_status == Statuses.Initialized )
            {
                // Nothing to stop, the core never start playing
                return; 
            }*/

            // Stop NAudio play back
            if (m_waveOutDevice != null)
            {
                // Workaround to crashes/locks that sometimes occur due to threading issues.
                //   Solution: Resume paused playback BEFORE stopping to release NAudio pause state 
                if (m_waveOutDevice.PlaybackState == PlaybackState.Paused)
                {
                    m_logger.Debug("Experimental - Resume paused playback BEFORE loading another file");
                    // Mute volume when Resume play - we don't it to really play, just to release the playback thread
                    m_waveOutDevice.Volume = 0;
                    m_waveOutDevice.Play();
                    // Give the NAudio playback some short time to release itself
                    Thread.Sleep(10);
                }

                m_waveOutDevice.Stop();
            }

            // Stop the audio processing thread
            if ( m_audioProcessingThread != null )
            {
                m_stopWorker = true;
                while (m_workerRunning)
                {
                    Thread.Sleep(10);
                }

                m_audioProcessingThread = null;
            }

            // Playback status changed to -> Stopped
            ChangeStatus(Statuses.Stopped);
        }

        /// <summary>
        /// Pauses the play back
        /// </summary>
        public void Pause()
        {
            m_logger.Debug("Pause() requested");

            // Playback status changed to -> Pausing
            ChangeStatus(Statuses.Pausing);
            m_waveOutDevice.Pause();
        }

        /// <summary>
        /// Utility function that identifies wether the file is an audio file or not
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsAudioFile(string filename)
        {
            filename = filename.ToLower();
            bool result = filename.EndsWith(".mp3") || 
                          filename.EndsWith(".wav") || 
                          filename.EndsWith(".ogg") || 
                          filename.EndsWith(".wma");

            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Getter of the file play duration - the length of the file playing time
        /// </summary>
        public TimeSpan FilePlayDuration
        {
            get
            {
                return m_filePlayDuration;
            }
        }

        /// <summary>
        /// Property for controlling the play back Tempo (Speed)
        /// Domain values - A non-negative floating number (normally 0.1 - 3.0)
        /// 1.0 = Regular speed
        /// greater than 1.0 = Faster (e.g. 2.0 runs two times faster)
        /// less than 1.0 = Slower (e.g. 0.5 runs two times slower)
        /// </summary>
        /// <remarks>
        /// To be used from the controlling component (i.e. the GUI)
        /// </remarks>
        public float Tempo
        {
            get { lock (TempoLock) return m_tempo; }
            set { lock (TempoLock) { m_tempo = value; } }
        }

        /// <summary>
        /// Property for controlling the play back Pitch
        /// Domain values - A floating number (-2.0 to 2.0 octaves)
        /// 0.0 = Regular Pitch
        /// greater than 0.0 = Higher
        /// less than 0.0 = Lower
        /// </summary>
        /// <remarks>
        /// To be used from the controlling component (i.e. the GUI)
        /// </remarks>
        public float Pitch
        {
            get { lock (PropertiesLock) return m_pitch; }
            set { lock (PropertiesLock) { m_pitch = value; } }
        }

        /// <summary>
        /// Play back Volume in percent - 0%..100%
        /// </summary>
        public float Volume
        {
            get
            {
                lock (PropertiesLock) { return m_volume; }
            }
            set
            {
                lock (PropertiesLock) { m_volume = value; }
            }
        }

        public float EqualizerLoBand
        {
            get
            {
                lock (PropertiesLock) { return m_eqLo; }
            }
            set
            {
                lock (PropertiesLock) 
                { 
                    m_eqLo = value;
                    m_eqParamsChanged = true;
                }
            }
        }        

        public float EqualizerMedBand
        {
            get
            {
                lock (PropertiesLock) { return m_eqMed; }
            }
            set
            {
                lock (PropertiesLock) 
                { 
                    m_eqMed = value;
                    m_eqParamsChanged = true;
                }
            }
        }

        public float EqualizerHiBand
        {
            get
            {
                lock (PropertiesLock) { return m_eqHi; }
            }
            set
            {
                lock (PropertiesLock) 
                { 
                    m_eqHi = value;
                    m_eqParamsChanged = true;
                }
            }
        }

        /// <summary>
        /// Current status
        /// </summary>
        public Statuses Status { get { return m_status; } }

        /// <summary>
        /// The current real play time - taking into account the tempo value
        /// </summary>
        public TimeSpan CurrentPlayTime
        {
            get
            {
                if (m_inputProvider == null)
                    return TimeSpan.Zero;

                lock (CurrentPlayTimeLock)
                {
                    return m_currentPlayTime;
                }
            }
            set
            {
                lock (CurrentPlayTimeLock)
                {
                    m_newPlayTime = value;
                    m_newPlayTimeRequested = true;

                    if (m_status == Statuses.Pausing || m_status == Statuses.Initialized || m_status == Statuses.Stopped)
                    {
                        m_currentPlayTime = m_newPlayTime;
                        // RaisePlayTimeChangedEvent();
                    }
                }
            }
        }

        /// <summary>
        /// Boolean flag for playing the selected region (or whole file) in a loop
        ///     True - play on loop
        ///     False - play once and stop
        /// </summary>
        public bool Loop
        {
            get
            {
                lock (LoopLock)
                {
                    return m_loop;
                }
            }
            set
            {
                lock (LoopLock)
                {
                    m_loop = value;
                }
            }
        }

        public TimeSpan StartMarker
        {
            get
            {
                if (m_inputProvider == null)
                    return TimeSpan.Zero;

                lock (PropertiesLock)
                {
                    return m_startMarker;
                }
            }
            set
            {
                lock (PropertiesLock)
                {
                    m_startMarker = value;
                }
            }
        }

        public TimeSpan EndMarker
        {
            get
            {
                if (m_inputProvider == null)
                    return TimeSpan.Zero;

                lock (PropertiesLock)
                {
                    return m_endMarker;
                }
            }
            set
            {
                lock (PropertiesLock)
                {
                    m_endMarker = value;
                }
            }
        }

        public TimeSpan Cue
        {
            get
            {
                if (m_inputProvider == null)
                    return TimeSpan.Zero;

                lock (PropertiesLock)
                {
                    return m_cue;
                }
            }
            set
            {
                lock (PropertiesLock)
                {
                    m_cue = value;
                }
            }
        }

        #endregion

        #region Events

        public event EventHandler PlayTimeChanged;
        public delegate void StatusChangedEventHandler(object sender, Statuses newStatus);
        public event StatusChangedEventHandler StatusChanged;
        public event EventHandler CueWaitPulsed;

        #endregion

        #region Enums

        public enum Statuses { None, Initializing, Initialized, Loading, Playing, Stopped, Pausing, Terminating, Terminated, Error };

        #endregion

        #region Private Methods

        /// <summary>
        /// Audio processing thread procedure
        /// </summary>
        private void audioProcessingWorker_DoWork()
        {
            try
            {
                try
                {
                    InitializeSoundTouchSharp();

                    InitializeFileAudio();

                    InitializeEqualizerEffect();

                    // Special case handling for re-playing after last playing stopped in non-loop mode: 
                    //   Reset current play time to begining of file in case the previous play has reached the end of file
                    if (!m_newPlayTimeRequested && m_currentPlayTime >= m_waveChannel.TotalTime)
                        m_currentPlayTime = TimeSpan.Zero;

                    // Playback status changed to -> Initialized
                    ChangeStatus(Statuses.Initialized);
                }
                finally
                {
                    // Pulse the initialized lock to release the client (UI) that is waiting for initialization to finish
                    lock (InitializedLock)
                    {
                        Monitor.Pulse(InitializedLock);
                    }
                }

                // Wait for first Play to pulse and free lock
                lock (FirstPlayLock)
                {
                    Monitor.Wait(FirstPlayLock);
                }
                // Safety guard - if thread never really started playing but PracticeSharpLogic was terminated
                if (m_status == Statuses.Terminating || m_status == Statuses.Terminated)
                    return;

                m_waveOutDevice.Play();
                // Playback status changed to -> Playing
                ChangeStatus(Statuses.Playing);

                try
                {
                    ProcessAudio();
                }
                finally
                {
                    m_audioProcessingThread = null;

                    // Dispose of NAudio in context of thread (for WMF it will must be disposed in the same thread)
                    TerminateNAudio();

                    TerminateSoundTouchSharp();
                }
            }
            catch (Exception ex)
            {
                m_logger.ErrorException("Exception in audioProcessingWorker_DoWork", ex);
                ChangeStatus( Statuses.Error );
            }
        }

        /// <summary>
        /// The heart of Practice# audio processing:
        /// 1. Reads chunks of uncompressed samples from the input file
        /// 2. Processes the samples using SoundTouch 
        /// 3. Receive process samples from SoundTouch
        /// 4. Play the processed samples with NAudio
        /// 
        /// Also handles logic required for dynamically changing values on-the-fly of: Volume, Loop, Cue, Current Play Position.
        /// </summary>
        private void ProcessAudio()
        {
            m_stopWorker = false;
            m_workerRunning = true;
            m_logger.Debug("ProcessAudio() started");

            try
            {
                #region Setup
                WaveFormat format = m_waveChannel.WaveFormat;
                int bufferSecondLength = format.SampleRate * format.Channels;
                byte[] inputBuffer = new byte[BufferSamples * sizeof(float)];
                byte[] soundTouchOutBuffer = new byte[BufferSamples * sizeof(float)];

                ByteAndFloatsConverter convertInputBuffer = new ByteAndFloatsConverter { Bytes = inputBuffer };
                ByteAndFloatsConverter convertOutputBuffer = new ByteAndFloatsConverter { Bytes = soundTouchOutBuffer };
                uint outBufferSizeFloats = (uint)convertOutputBuffer.Bytes.Length / (uint)(sizeof(float) * format.Channels);

                int bytesRead;
                int floatsRead;
                uint samplesProcessed = 0;
                int bufferIndex = 0;
                TimeSpan actualEndMarker = TimeSpan.Zero;
                bool loop;

                #endregion

                while (!m_stopWorker && m_waveChannel.Position < m_waveChannel.Length)
                {
                    lock (PropertiesLock)
                    {
                        m_waveChannel.Volume = m_volume;
                    }

                    #region Read samples from file

                    // Change current play position
                    lock (CurrentPlayTimeLock)
                    {
                        if (m_newPlayTimeRequested)
                        {
                            m_waveChannel.CurrentTime = m_newPlayTime;

                            m_newPlayTimeRequested = false;
                        }
                    }

                    // *** Read one chunk from input file ***
                    bytesRead = m_waveChannel.Read(convertInputBuffer.Bytes, 0, convertInputBuffer.Bytes.Length);
                    // **************************************

                    floatsRead = bytesRead / ((sizeof(float)) * format.Channels);

                    #endregion

                    #region Apply Equalizer Effect

                    ApplyEqualizerEffect(convertInputBuffer.Floats, floatsRead);

                    #endregion

                    actualEndMarker = this.EndMarker;
                    loop = this.Loop;

                    if (!loop || actualEndMarker == TimeSpan.Zero)
                        actualEndMarker = m_waveChannel.TotalTime;

                    if (m_waveChannel.CurrentTime > actualEndMarker)
                    {
                        #region Flush left over samples

                        // ** End marker reached **
                        // Now the input buffer is processed, 'flush' some last samples that are
                        // hiding in the SoundTouch's internal processing pipeline.
                        m_soundTouchSharp.Flush();
                        if (!m_stopWorker)
                        {
                            while (!m_stopWorker && samplesProcessed != 0)
                            {
                                SetSoundSharpValues();

                                samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);

                                if (samplesProcessed > 0)
                                {
                                    TimeSpan currentBufferTime = m_waveChannel.CurrentTime;
                                    m_inputProvider.AddSamples(convertOutputBuffer.Bytes, 0, (int)samplesProcessed * sizeof(float) * format.Channels, currentBufferTime);
                                }

                                samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);
                            }
                        }

                        #endregion

                        #region Handle Loop & Cue
                        loop = this.Loop;
                        if (loop)
                        {
                            m_waveChannel.CurrentTime = this.StartMarker;
                            WaitForCue();
                        }
                        else
                        {
                            break;
                        }

                        #endregion
                    }

                    #region Put samples in SoundTouch

                    SetSoundSharpValues();

                    // ***                    Put samples in SoundTouch                   ***
                    m_soundTouchSharp.PutSamples(convertInputBuffer.Floats, (uint)floatsRead);
                    // **********************************************************************

                    #endregion

                    #region Receive & Play Samples
                    // Receive samples from SoundTouch
                    do
                    {
                        // ***                Receive samples back from SoundTouch            ***
                        // *** This is where Time Stretching and Pitch Changing is done *********
                        samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);
                        // **********************************************************************

                        if (samplesProcessed > 0)
                        {
                            TimeSpan currentBufferTime = m_waveChannel.CurrentTime;

                            // ** Play samples that came out of SoundTouch by adding them to AdvancedBufferedWaveProvider - the buffered player 
                            m_inputProvider.AddSamples(convertOutputBuffer.Bytes, 0, (int)samplesProcessed * sizeof(float) * format.Channels, currentBufferTime);
                            // **********************************************************************

                            // Wait for queue to free up - only then add continue reading from the file
                            while (!m_stopWorker && m_inputProvider.GetQueueCount() > BusyQueuedBuffersThreshold)
                            {
                                Thread.Sleep(10);
                            }
                            bufferIndex++;
                        }
                    } while (!m_stopWorker && samplesProcessed != 0);
                    #endregion
                } // while

                #region Stop PlayBack 
                m_logger.Debug("ProcessAudio() finished - stop playback");
                // Stop listening to PlayPositionChanged events
                m_inputProvider.PlayPositionChanged -= new EventHandler(inputProvider_PlayPositionChanged);

                // Fix to current play time not finishing up at end marker (Wave channel uses positions)
                if (!m_stopWorker && CurrentPlayTime < actualEndMarker)
                {
                    lock (CurrentPlayTimeLock)
                    {
                        m_currentPlayTime = actualEndMarker;
                    }
                }

                ChangeStatus(Statuses.Stopped);
                #endregion
            }
            finally
            {
                m_workerRunning = false;
            }
        }


        /// <summary>
        /// Applies the DSP Effects in the effects chain
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        private void ApplyEqualizerEffect( float[] buffer, int count)
        {
            int samples = count * 2;

            // Apply Equalizer parameters (if they were changed)
            lock (PropertiesLock)
            {
                if (m_eqParamsChanged)
                {
                    m_eqEffect.LoGainFactor.Value = m_eqEffect.LoGainFactor.Maximum * m_eqLo;
                    //m_eqEffect.LoDriveFactor.Value = (m_eqLo + 1.0f) / 2 * 100.0f;
                    m_eqEffect.MedGainFactor.Value = m_eqEffect.MedGainFactor.Maximum * m_eqMed;
                    // m_eqEffect.MedDriveFactor.Value = (m_eqMed + 1.0f) / 2 * 100.0f;
                    m_eqEffect.HiGainFactor.Value = m_eqEffect.HiGainFactor.Maximum * m_eqHi;
                    //m_eqEffect.HiDriveFactor.Value = (m_eqHi + 1.0f) / 2 * 100.0f;

                    m_eqEffect.OnFactorChanges();

                    m_eqParamsChanged = false;
                }
            }

            // Run each sample in the buffer through the equalizer effect
            for(int sample = 0; sample < samples; sample += 2)
            {
                // Get the samples, per audio channel
                float sampleLeft = buffer[sample];
                float sampleRight = buffer[sample + 1];
               
                // Apply the equalizer effect to the samples
                m_eqEffect.Sample(ref sampleLeft, ref sampleRight);

                // Put the modified samples back into the buffer
                buffer[sample] = sampleLeft;
                buffer[sample + 1] = sampleRight;
            }
        }
    
        /// <summary>
        /// Initialize the file play back audio infrastructure
        /// </summary>
        private void InitializeFileAudio()
        {
            InitializeNAudioLibrary();

            CreateSoundTouchInputProvider(m_filename);

            try
            {
                m_waveOutDevice.Init(m_inputProvider);
            }
            catch (Exception initException)
            {
                m_logger.ErrorException("Exception in InitializeFileAudio - m_waveOutDevice.Init", initException);

                throw;
            }

            m_filePlayDuration = m_waveChannel.TotalTime;
        }

        /// <summary>
        /// Waits for the loop cue - basically waits a few seconds before the loop starts again, this allows the musician to rest a bit and prepare
        /// </summary>
        private void WaitForCue()
        {
            TimeSpan cue = this.Cue;
            if (cue.TotalSeconds > 0)
            {
                // Wait Cue - 1 seconds in a slow pulse (one per second) busy-loop
                int pulseCount = 0;
                while (!m_stopWorker && pulseCount < cue.TotalSeconds - 1)
                {
                    if (CueWaitPulsed != null)
                    {
                        // explicitly invoke each subscribed event handler *asynchronously*
                        foreach (EventHandler subscriber in CueWaitPulsed.GetInvocationList())
                        {
                            // Event is unidirectional - No call back (i.e. EndInvoke) needed
                            subscriber.BeginInvoke(this, new EventArgs(), null, subscriber);
                        }
                    }

                    Thread.Sleep(1000);
                    pulseCount++;
                }

                // Wait 1 seconds in a fast pulse (4 per second) busy-loop
                pulseCount = 0;
                while (!m_stopWorker && pulseCount < 4)
                {
                    if (CueWaitPulsed != null)
                    {
                        CueWaitPulsed(this, new EventArgs());
                    }

                    Thread.Sleep(250);
                    pulseCount++;
                }
            }
        }

        /// <summary>
        /// Changes the status and raises the StatusChanged event
        /// </summary>
        /// <param name="newStatus"></param>
        private void ChangeStatus(Statuses newStatus)
        {
            m_status = newStatus;

            Console.WriteLine("PracticeSharpLogic - Status changed: " + m_status);
            // Raise StatusChanged Event
            if (StatusChanged != null)
            {
                // explicitly invoke each subscribed event handler *asynchronously*
                foreach (StatusChangedEventHandler subscriber in StatusChanged.GetInvocationList())
                {
                    // Event is unidirectional - No call back (i.e. EndInvoke) needed
                    subscriber.BeginInvoke(this, newStatus, null, subscriber);
                }
            }
        }

        /// <summary>
        /// Creates input provider needed for reading an audio file into the SoundTouch library
        /// </summary>
        /// <param name="filename"></param>
        private void CreateSoundTouchInputProvider(string filename)
        {
            Console.WriteLine("Input file: " + filename);
            CreateInputWaveChannel(filename);

            WaveFormat format = m_waveChannel.WaveFormat;
            m_inputProvider = new AdvancedBufferedWaveProvider(format);
            m_inputProvider.PlayPositionChanged += new EventHandler(inputProvider_PlayPositionChanged);
            m_inputProvider.MaxQueuedBuffers = 1000;

            m_soundTouchSharp.SetSampleRate(format.SampleRate);
            m_soundTouchSharp.SetChannels(format.Channels);

            m_soundTouchSharp.SetTempoChange(0);
            m_soundTouchSharp.SetPitchSemiTones(0);
            m_soundTouchSharp.SetRateChange(0);

            m_soundTouchSharp.SetTempo(m_tempo);

            m_soundTouchSharp.SetSetting(SoundTouchSharp.SoundTouchSettings.SETTING_USE_QUICKSEEK, 0);
            m_soundTouchSharp.SetSetting(SoundTouchSharp.SoundTouchSettings.SETTING_USE_AA_FILTER, 1);
            m_soundTouchSharp.SetSetting(SoundTouchSharp.SoundTouchSettings.SETTING_AA_FILTER_LENGTH, 128);
        }

        /// <summary>
        /// Sets the SoundSharp values - tempo & pitch
        /// </summary>
        /// <param name="tempo"></param>
        /// <param name="pitch"></param>
        private void SetSoundSharpValues()
        {
            float tempo = this.Tempo;
            float pitch = this.Pitch;
            // Assign updated tempo
            m_soundTouchSharp.SetTempo(tempo);
            // Assign updated pitch
            m_soundTouchSharp.SetPitchOctaves(pitch);
        }

        /// <summary>
        /// NAudio Event handler - Fired every time the play position has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputProvider_PlayPositionChanged(object sender, EventArgs e)
        {
            lock (CurrentPlayTimeLock)
            {
                m_currentPlayTime = (e as BufferedPlayEventArgs).PlayTime;
            }

            RaisePlayTimeChangedEvent();
        }

        private void RaisePlayTimeChangedEvent()
        {
            if (PlayTimeChanged != null)
            {
                // explicitly invoke each subscribed event handler *asynchronously*
                foreach (EventHandler subscriber in PlayTimeChanged.GetInvocationList())
                {
                    // Event is unidirectional - No call back (i.e. EndInvoke) needed
                    subscriber.BeginInvoke(this, new EventArgs(), null, subscriber);
                }
            }
        }

        /// <summary>
        /// Creates an input WaveChannel (Audio file reader for MP3/WAV/OGG/WMA/Other formats in the future)
        /// </summary>
        /// <param name="filename"></param>
        private void CreateInputWaveChannel(string filename)
        {
            string fileExt = Path.GetExtension( filename.ToLower() );
            if ( fileExt == MP3Extension )
            {
                m_waveReader = new Mp3FileReader(filename);
                m_blockAlignedStream = new BlockAlignReductionStream(m_waveReader);
                // Wave channel - reads from file and returns raw wave blocks
                m_waveChannel = new WaveChannel32(m_blockAlignedStream);
            }
            else if ( fileExt == WAVExtension )
            {
                m_waveReader = new WaveFileReader(filename);
                if (m_waveReader.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
                {
                    m_waveReader = WaveFormatConversionStream.CreatePcmStream(m_waveReader);
                    m_waveReader = new BlockAlignReductionStream(m_waveReader);
                }
                if (m_waveReader.WaveFormat.BitsPerSample != 16)
                {
                    var format = new WaveFormat(m_waveReader.WaveFormat.SampleRate,
                       16, m_waveReader.WaveFormat.Channels);
                    m_waveReader = new WaveFormatConversionStream(format, m_waveReader);
                }
                
                m_waveChannel = new WaveChannel32(m_waveReader);
            }
            else if (fileExt == OGGVExtension)
            {
                m_waveReader = new OggVorbisFileReader(filename);
                if (m_waveReader.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
                {
                    m_waveReader = WaveFormatConversionStream.CreatePcmStream(m_waveReader);
                    m_waveReader = new BlockAlignReductionStream(m_waveReader);
                }
                if (m_waveReader.WaveFormat.BitsPerSample != 16)
                {
                    var format = new WaveFormat(m_waveReader.WaveFormat.SampleRate,
                       16, m_waveReader.WaveFormat.Channels);
                    m_waveReader = new WaveFormatConversionStream(format, m_waveReader);
                }

                m_waveChannel = new WaveChannel32(m_waveReader);
            }
            else if (fileExt == WMAExtension)
            {
                m_waveReader = new WMAFileReader(filename);
                if (m_waveReader.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
                {
                    m_waveReader = WaveFormatConversionStream.CreatePcmStream(m_waveReader);
                    m_waveReader = new BlockAlignReductionStream(m_waveReader);
                }
                if (m_waveReader.WaveFormat.BitsPerSample != 16)
                {
                    var format = new WaveFormat(m_waveReader.WaveFormat.SampleRate,
                       16, m_waveReader.WaveFormat.Channels);
                    m_waveReader = new WaveFormatConversionStream(format, m_waveReader);
                }

                m_waveChannel = new WaveChannel32(m_waveReader);
            }
            else
            {
                throw new ApplicationException("Cannot create Input WaveChannel - Unknown file type: " + fileExt);
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize the NAudio framework
        /// </summary>
        private void InitializeNAudioLibrary()
        {
            try
            {
                m_latency = BigMansStuff.PracticeSharp.Properties.Settings.Default.Latency;
                m_waveOutDevice = new DirectSoundOut(m_latency);
            }
            catch (Exception driverCreateException)
            {
                m_logger.ErrorException("NAudio Driver Creation Failed", driverCreateException);
                throw driverCreateException;
            }
        }

        /// <summary>
        /// Initialize the sound touch library (using the SoundTouchSharp wrapper)
        /// </summary>
        private void InitializeSoundTouchSharp()
        {
            m_soundTouchSharp = new SoundTouchSharp();
            m_soundTouchSharp.CreateInstance();
            Console.WriteLine("SoundTouch Initialized - Version: " + m_soundTouchSharp.SoundTouchVersionId + ", " + m_soundTouchSharp.SoundTouchVersionString);
        }

        /// <summary>
        /// Initialize the Equalizer DSP Effect
        /// </summary>
        private void InitializeEqualizerEffect()
        {
            // Initialize Equalizer
            m_eqEffect = new EqualizerEffect();
            m_eqEffect.SampleRate = m_waveChannel.WaveFormat.SampleRate;
            m_eqEffect.LoDriveFactor.Value = 50;
            m_eqEffect.LoGainFactor.Value = 0;
            m_eqEffect.MedDriveFactor.Value = 50;
            m_eqEffect.MedGainFactor.Value = 0;
            m_eqEffect.HiDriveFactor.Value = 50;
            m_eqEffect.HiGainFactor.Value = 0;
            m_eqEffect.Init();
            m_eqEffect.OnFactorChanges();
        }

        #endregion

        #region Termination

        /// <summary>
        /// Disposes of the current allocated resources
        /// </summary>
        public void Dispose()
        {
            if (m_status != Statuses.Terminated)
            {
                Terminate();
            }

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Terminates the NAudio library resources and connection
        /// </summary>
        private void TerminateNAudio()
        {
            if (m_waveReader != null)
            {
                m_waveReader.Dispose();
                m_waveReader = null;
            }

            if (m_waveChannel != null)
            {
                m_waveChannel.Dispose();
                m_waveChannel = null;
            }

            if (m_blockAlignedStream != null)
            {
                m_blockAlignedStream.Dispose();
                m_blockAlignedStream = null;
            }

            if (m_inputProvider != null)
            {
                if (m_inputProvider is IDisposable)
                {
                    (m_inputProvider as IDisposable).Dispose();
                }
                m_inputProvider = null;
            }

            if (m_waveOutDevice != null)
            {
                m_waveOutDevice.Dispose();
                m_waveOutDevice = null;
            }

            m_logger.Debug("NAudio terminated");
        }

        /// <summary>
        /// Terminates the SoundTouch resources and connection
        /// </summary>
        private void TerminateSoundTouchSharp()
        {

            if (m_soundTouchSharp != null)
            {
                m_soundTouchSharp.Clear();
                m_soundTouchSharp.Dispose();
                m_soundTouchSharp = null;
                Console.WriteLine("SoundTouch terminated");
            }
        }
        
        #endregion

        #region Private Members

        private Statuses m_status = Statuses.None;
        private string m_filename;
        private int m_latency = 125; // msec
        
        private SoundTouchSharp m_soundTouchSharp;
        private IWavePlayer m_waveOutDevice;
        private AdvancedBufferedWaveProvider m_inputProvider;

        private WaveStream m_blockAlignedStream = null;
        private WaveStream m_waveReader = null;
        private WaveChannel32 m_waveChannel = null;

        private float m_tempo = 1f;
        private float m_pitch = 0f;
        private bool m_loop;
        private float m_volume;
        private float m_eqLo;
        private float m_eqMed;
        private float m_eqHi;
        private bool m_eqParamsChanged;
        private EqualizerEffect m_eqEffect;

        private Thread m_audioProcessingThread;
        private bool m_stopWorker = false;
        private bool m_workerRunning = false;

        private TimeSpan m_filePlayDuration;

        private TimeSpan m_startMarker;
        private TimeSpan m_endMarker;
        private TimeSpan m_cue;

        private TimeSpan m_currentPlayTime;
        private TimeSpan m_newPlayTime;
        private bool m_newPlayTimeRequested;

        private List<DSPEffect> m_dspEffects = new List<DSPEffect>();

        // Thread Locks
        private readonly object LoopLock = new object();
        private readonly object CurrentPlayTimeLock = new object();
        private readonly object InitializedLock = new object();
        private readonly object FirstPlayLock = new object();
        private readonly object TempoLock = new object();
        private readonly object PropertiesLock = new object();

        #endregion

        #region Constants

        const string MP3Extension = ".mp3";
        const string WAVExtension = ".wav";
        const string OGGVExtension = ".ogg";
        const string WMAExtension = ".wma";

        const int BusyQueuedBuffersThreshold = 3;
            
        const int BufferSamples = 5 * 2048; // floats, not bytes

        #endregion
    }
}
