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

namespace BigMansStuff.PracticeSharp.Core
{
    /// <summary>
    /// Core Logic (Back-End) for PracticeSharp application
    /// Acts as a mediator between the User Interface, NAudio and SoundSharp layers
    /// </summary>
    public class PracticeSharpLogic: IDisposable
    {
        #region Public Methods
        
        /// <summary>
        /// Initialize the PracticeSharp core logic layer
        /// </summary>
        public void Initialize()
        {
            ChangeStatus(Statuses.Initializing);
            try
            {
                InitializeSoundTouchSharp();

                m_startMarker = TimeSpan.Zero;
                m_endMarker = TimeSpan.Zero;
                m_cue = TimeSpan.Zero;

                ChangeStatus(Statuses.Initialized);
            }
            catch (Exception ex)
            {
                ChangeStatus(Statuses.Error);
                throw ex;
            }
        }

        /// <summary>
        /// Terminates all current play back and resources
        /// </summary>
        public void Terminate()
        {
            ChangeStatus(Statuses.Terminating);

            Stop();

            TerminateSoundTouchSharp();

            ChangeStatus(Statuses.Terminated);
        }

        /// <summary>
        /// Loads the given file name and start playing it
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFile(string filename)
        {
            // Stop a previous file load
            if (m_waveOutDevice != null)
            {
                Stop();
            }

            m_filename = filename;

            InitializeNAudio();

            CreateSoundTouchInputProvider(m_filename);

            try
            {
                m_waveOutDevice.Init(m_inputProvider);
            }
            catch (Exception initException)
            {
                Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");

                throw;
            }

            m_filePlayDuration = m_waveChannel.TotalTime;

            m_status = Statuses.Pausing;

            // Create the Audio Processing Worker (Thread)
            m_audioProcessingWorker = new BackgroundWorker();
            m_audioProcessingWorker.DoWork += new DoWorkEventHandler(audioProcessingWorker_DoWork);
            m_audioProcessingWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Starts/Resumes play back of the current file
        /// </summary>
        public void Play()
        {
            if (m_status == Statuses.Initialized || m_status == Statuses.Pausing)
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
            if (m_waveOutDevice != null)
            {
                m_waveOutDevice.Stop();
            }

            if ( m_audioProcessingWorker != null )
            {
                m_stopWorker = true;
                while (m_workerRunning)
                {
                    Thread.Sleep(10);
                }

                m_audioProcessingWorker.Dispose();
                m_audioProcessingWorker = null;
            }

            TerminateNAudio();

            m_soundTouchSharp.Clear();

            m_currentPlayTime = TimeSpan.Zero;
            m_startMarker = TimeSpan.Zero;
            m_endMarker = TimeSpan.Zero;
            m_cue = TimeSpan.Zero;
            m_loop = false;
            m_volume = PresetData.DefaultVolume;


            ChangeStatus(Statuses.Stopped);
        }

        /// <summary>
        /// Pauses the play back
        /// </summary>
        public void Pause()
        {
            ChangeStatus(Statuses.Pausing);
            m_waveOutDevice.Pause();
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
            get { lock (m_tempoLock) return m_tempo; }
            set { lock (m_tempoLock) { m_tempo = value; } }
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
            get { lock (m_propertiesLock) return m_pitch; }
            set { lock (m_propertiesLock) { m_pitch = value; } }
        }

        /// <summary>
        /// Play back Volume in percent - 0%..100%
        /// </summary>
        public float Volume
        {
            get
            {
                lock (m_propertiesLock) { return m_volume; }
            }
            set
            {
                lock (m_propertiesLock) { m_volume = value; }
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

                lock (m_currentPlayTimeLock)
                {
                    return m_currentPlayTime;
                }
            }
            set
            {
                lock (m_currentPlayTimeLock)
                {
                    m_newPlayTime = value;
                    m_newPlayTimeRequested = true;
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
                lock (m_loopLock)
                {
                    return m_loop;
                }
            }
            set
            {
                lock (m_loopLock)
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

                lock (m_propertiesLock)
                {
                    return m_startMarker;
                }
            }
            set
            {
                lock (m_propertiesLock)
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

                lock (m_propertiesLock)
                {
                    return m_endMarker;
                }
            }
            set
            {
                lock (m_propertiesLock)
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

                lock (m_propertiesLock)
                {
                    return m_cue;
                }
            }
            set
            {
                lock (m_propertiesLock)
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

        public enum Statuses { Initializing, Initialized, Playing, Stopped, Pausing, Terminating, Terminated, Error };

        #endregion

        #region Private Methods

        /// <summary>
        /// The heart of sound processing - Reads chunks from the input file, 
        /// processes it using SoundTouch and then playes it using NAudio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audioProcessingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ProcessAudio();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                ChangeStatus( Statuses.Error );
            }

        }

        private void ProcessAudio()
        {
            m_stopWorker = false;
            m_workerRunning = true;
            try
            {
                const int BufferSamples = 5 * 2048; // floats, not bytes

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
                int totalRead = 0;
                uint totalSamples = 0;
                int bufferIndex = 0;

                float tempo;
                float pitch;
                int averageBytesPerSec;
                TimeSpan startMarker;
                TimeSpan endMarker;
                long startPosition;
                long endPosition;
                bool loop;

                while (!m_stopWorker && m_waveChannel.Position < m_waveChannel.Length)
                {
                    lock (m_propertiesLock)
                    {
                        m_waveChannel.Volume = m_volume;
                    }

                    // Change current play position
                    lock (m_currentPlayTimeLock)
                    {
                        if (m_newPlayTimeRequested)
                        {
                            // Temporary workaround to NAudio bug: If play time is out of bounds there is a Null exception
                            if (m_newPlayTime > m_filePlayDuration)
                            {
                                m_newPlayTime = m_filePlayDuration.Subtract( new TimeSpan( 0, 0, 0, 0, 100 ) );
                            }
                            m_waveChannel.CurrentTime = m_newPlayTime;
                            m_newPlayTimeRequested = false;
                        }
                    }

                    // Read one chunk from input file
                    bytesRead = m_waveChannel.Read(convertInputBuffer.Bytes, 0, convertInputBuffer.Bytes.Length);
                    // Console.WriteLine("File: Total Read: {0}, Current Time {1}", totalRead, m_waveChannel.CurrentTime);

                    startMarker = this.StartMarker;
                    endMarker = this.EndMarker;

                    lock (m_loopLock)
                    {
                        loop = m_loop;
                    }

                    if (!loop || endMarker == TimeSpan.Zero)
                        endPosition = m_waveChannel.Length;
                    else
                        endPosition = Convert.ToInt32(endMarker.TotalSeconds * format.AverageBytesPerSecond);

                    startPosition = Convert.ToInt32(startMarker.TotalSeconds * format.AverageBytesPerSecond);

                    if (m_waveChannel.Position > endPosition)
                    {
                        // ** End marker reached **
                        // Now the input buffer is processed, 'flush' some last samples that are
                        // hiding in the SoundTouch's internal processing pipeline.
                        m_soundTouchSharp.Flush();
                        if (!m_stopWorker)
                        {
                            while (!m_stopWorker && samplesProcessed != 0)
                            {
                                SetSoundSharpValues(out tempo, out pitch, out averageBytesPerSec);

                                samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);

                                if (samplesProcessed > 0)
                                {
                                    totalSamples += samplesProcessed;

                                    // TODO: Calculation and add relative CurrentTime
                                    TimeSpan currentBufferTime = m_waveChannel.CurrentTime;

                                    m_inputProvider.AddSamples(convertOutputBuffer.Bytes, 0, (int)samplesProcessed * sizeof(float) * format.Channels, currentBufferTime, averageBytesPerSec);
                                }

                                samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);
                            }
                        }

                        lock (m_loopLock)
                        {
                            loop = m_loop;
                        }

                        if (loop)
                        {
                            m_waveChannel.Position = startPosition;
                            WaitForCue();
                        }
                        else
                        {
                            break;
                        }
                    }

                    totalRead += bytesRead;
                    //Console.WriteLine("File: Total Read: {0}, Current Time {1}", totalRead, m_waveChannel.CurrentTime);

                    floatsRead = bytesRead / ((sizeof(float)) * format.Channels);

                    SetSoundSharpValues(out tempo, out pitch, out averageBytesPerSec);

                    // Put samples in SoundTouch
                    m_soundTouchSharp.PutSamples(convertInputBuffer.Floats, (uint)floatsRead);

                    // Receive samples from SoundTouch
                    do
                    {
                        samplesProcessed = m_soundTouchSharp.ReceiveSamples(convertOutputBuffer.Floats, outBufferSizeFloats);

                        if (samplesProcessed > 0)
                        {
                            totalSamples += samplesProcessed;
                            // TODO: Calculation and add relative CurrentTime
                            TimeSpan currentBufferTime = m_waveChannel.CurrentTime;
                            int bufferAverageBytesPerSecond = Convert.ToInt32(format.AverageBytesPerSecond / m_tempo);

                            m_inputProvider.AddSamples(convertOutputBuffer.Bytes, 0, (int)samplesProcessed * sizeof(float) * format.Channels, currentBufferTime, averageBytesPerSec);

                            // Wait for queue to free up - only then add continue reading from the file
                            while (!m_stopWorker && m_inputProvider.GetQueueCount() > 2)
                            {
                                Thread.Sleep(10);
                            }
                            bufferIndex++;
                        }
                    } while (!m_stopWorker && samplesProcessed != 0);
                } // while


                //Console.WriteLine("Total bytes read: " + totalRead);
                //Console.WriteLine("Total bytes processed: " + totalSamples * sizeof(float) * format.Channels);

                ChangeStatus(Statuses.Stopped);
            }
            finally
            {
                m_workerRunning = false;
            }
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
                        CueWaitPulsed(this, new EventArgs());
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
                StatusChanged(this, m_status);
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

            // TODO: Move to apply after each file block read
            m_soundTouchSharp.SetTempo(m_tempo);

            m_soundTouchSharp.SetSetting(SoundTouchSharp.SoundTouchSettings.SETTING_USE_QUICKSEEK, 0);
            m_soundTouchSharp.SetSetting(SoundTouchSharp.SoundTouchSettings.SETTING_USE_AA_FILTER, 1);

            // TODO: Support speech mode
            /*             if (params->speech)
                            {
                                // use settings for speech processing
                                m_soundTouchSharp.SetSetting(SETTING_SEQUENCE_MS, 40);
                                m_soundTouchSharp.SetSetting(SETTING_SEEKWINDOW_MS, 15);
                                m_soundTouchSharp.SetSetting(SETTING_OVERLAP_MS, 8);
                            }
            */
        }

        /// <summary>
        /// Sets the SoundSharp values - tempo & pitch
        /// </summary>
        /// <param name="tempo"></param>
        /// <param name="averageBytesPerSec"></param>
        private void SetSoundSharpValues(out float tempo, out float pitch, out int averageBytesPerSec)
        {
            tempo = this.Tempo;
            pitch = this.Pitch;
            // Assign updated tempo
            m_soundTouchSharp.SetTempo(tempo);
            // Assign updated pitch
            m_soundTouchSharp.SetPitchOctaves(pitch);

            // Calculate the average bytes per second ratio - Needed for position calculation
            averageBytesPerSec = Convert.ToInt32(1.0f / tempo);
//            averageBytesPerSec = Convert.ToInt32((10 * Math.Abs(pitch) + 1) * 1.0f / tempo);
        }

        private void inputProvider_PlayPositionChanged(object sender, EventArgs e)
        {
            m_currentPlayTime = (e as BufferedPlayEventArgs).PlayTime;

            PlayTimeChanged(this, new EventArgs());
        }

        /// <summary>
        /// Creates an input WaveChannel (Audio file reader for MP3/WAV/Other formats in the future)
        /// </summary>
        /// <param name="filename"></param>
        private void CreateInputWaveChannel(string filename)
        {
            if (filename.ToLower().EndsWith(".mp3"))
            {
                m_mp3Reader = new Mp3FileReader(filename);
                m_blockAlignedStream = new BlockAlignReductionStream(m_mp3Reader);
                // Wave channel - reads from file and returns raw wave blocks
                m_waveChannel = new WaveChannel32(m_blockAlignedStream);
            }
            else if (filename.ToLower().EndsWith(".wav"))
            {
                m_waveReader = new WaveFileReader(filename);
                m_waveChannel = new WaveChannel32(m_waveReader);
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialize the NAudio framework
        /// </summary>
        private void InitializeNAudio()
        {
            try
            {
                m_waveOutDevice = new DirectSoundOut(m_latency);
            }
            catch (Exception driverCreateException)
            {
                Console.WriteLine("NAudio Driver Creation Failed" + driverCreateException.ToString());
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

        #endregion

        #region Termination

        /// <summary>
        /// Disposes of the current allocated resources
        /// </summary>
        public void Dispose()
        {
            Terminate();

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Terminates the NAudio library resources and connection
        /// </summary>
        private void TerminateNAudio()
        {
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

            if (m_mp3Reader != null)
            {
                m_mp3Reader.Dispose();
                m_mp3Reader = null;
            }
            if (m_waveReader != null)
            {
                m_waveReader.Dispose();
                m_waveReader = null;
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

            Console.WriteLine("NAudio terminated");
        }

        /// <summary>
        /// Terminates the SoundTouch resources and connection
        /// </summary>
        private void TerminateSoundTouchSharp()
        {

            if (m_soundTouchSharp != null)
            {
                m_soundTouchSharp.Dispose();
                m_soundTouchSharp = null;
                Console.WriteLine("SoundTouch terminated");
            }
        }
        
        #endregion

        #region Private Members

        private Statuses m_status;
        private string m_filename;
        private int m_latency = 100; // msec
        
        private SoundTouchSharp m_soundTouchSharp;
        private IWavePlayer m_waveOutDevice;
        private AdvancedBufferedWaveProvider m_inputProvider;

        private WaveStream m_mp3Reader = null;
        private WaveStream m_blockAlignedStream = null;
        private WaveFileReader m_waveReader = null;
        private WaveChannel32 m_waveChannel = null;

        private float m_tempo = 1f;
        private object m_tempoLock = new object();

        private float m_pitch = 0f;

        private bool m_loop;
        private object m_loopLock = new object();

        private float m_volume;
 
        private object m_propertiesLock = new object();

        private BackgroundWorker m_audioProcessingWorker;
        private bool m_stopWorker = false;
        private bool m_workerRunning = false;

        private TimeSpan m_filePlayDuration;

        private TimeSpan m_startMarker;
        private TimeSpan m_endMarker;
        private TimeSpan m_cue;

        private TimeSpan m_currentPlayTime;
        private TimeSpan m_newPlayTime;
        private bool m_newPlayTimeRequested;
        private object m_currentPlayTimeLock = new object();

        #endregion
    }
}
