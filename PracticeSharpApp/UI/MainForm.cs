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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BigMansStuff.PracticeSharp.Core;
using System.IO;
using System.Threading;
using System.Xml;
using System.Configuration;
using System.Diagnostics;
using NLog;

namespace BigMansStuff.PracticeSharp.UI
{

    // TODO: Persist input channel selection in the preset file - Load and Save

    /// <summary>
    /// Practice# Main Form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Logger
        private static Logger m_logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Construction

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// Form loading event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.Name = "MainFormThread";

            // Note: Initialization of Application folders must come first, BEFORE the logger is initialized
            InitializeApplicationFolders();

            InitializeLogger();

            try
            {
                InitializeApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed initialize the Practice Sharp back end - " + ex.ToString());
            }

            // Handle command line arguments
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2 && PracticeSharpLogic.IsAudioFile(args[1]))
            {
                OpenFile(args[1], true);
            }
            else
                // No command line argument - Try to load the last played file
                AutoLoadLastFile();
        }

        private void InitializeLogger()
        {
            m_logger.Info("-------------------------------------------------------------");
            m_logger.Info("Practice# application started");
        }

        /// <summary>
        /// Initialize the PracticeSharp Application
        /// </summary>
        private void InitializeApplication()
        {
            // Set process priority to high - to minimize playback hiccups
            Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;

            InitializeConfiguration();
            InitializeMRUFiles();

            m_versionUpdater = new VersionUpdater(this, m_appVersion);
            m_versionUpdater.CheckNewVersion();

            // Create the PracticeSharpLogic back end layer
            m_practiceSharpLogic = new PracticeSharpLogic();
            m_practiceSharpLogic.Initialize();
            m_practiceSharpLogic.StatusChanged += new PracticeSharpLogic.StatusChangedEventHandler(PracticeSharpLogic_StatusChanged);
            m_practiceSharpLogic.PlayTimeChanged += new EventHandler(PracticeSharpLogic_PlayTimeChanged);
            m_practiceSharpLogic.CueWaitPulsed += new EventHandler(PracticeSharpLogic_CueWaitPulsed);

            EnableControls( false );

            openFileDialog.InitialDirectory = Properties.Settings.Default.LastAudioFolder;
            openFileDialog.FilterIndex = Properties.Settings.Default.LastFilterIndex;

            playPauseButton.Image = Resources.Play_Normal;
            writePresetButton.Image = Resources.save_icon;
            resetPresetButton.Image = Resources.Eraser_icon;
            openFileButton.Image = Resources.OpenFile_icon;

            cueComboBox.SelectedIndex = 0;
            m_presetControls = new Dictionary<string, PresetControl>
            {
                { "1", presetControl1 },
                { "2", presetControl2 },
                { "3", presetControl3 },
                { "4", presetControl4 },
                { "5", presetControl5 },
                { "6", presetControl6 },
                { "7", presetControl7 },
                { "8", presetControl8 }
            };

            // Set defaults
            TempoTrackBar_ValueChanged(this, new EventArgs());
            PitchTrackBar_ValueChanged(this, new EventArgs());
            VolumeTrackBar_ValueChanged(this, new EventArgs());
            PlayTimeTrackBar_ValueChanged(this, new EventArgs());

            InitializeTimeStretchProfiles();
        }

        /// <summary>
        /// Initialize time stretch profiles and their UI controls
        /// </summary>
        private void InitializeTimeStretchProfiles()
        {
            // Load Time Stretch Profiles
            TimeStretchProfileManager.Initialize();

            if (Properties.Settings.Default.ShowTimeStretchProfilesControls)
            {
                timeStretchProfileLabel.Visible = true;
                timeStretchProfileComboBox.Visible = true;
            }
            int defaultProfileIndex = 0;
            foreach (TimeStretchProfile timeStretchProfile in TimeStretchProfileManager.TimeStretchProfiles.Values)
            {
                int itemIndex = timeStretchProfileComboBox.Items.Add(timeStretchProfile);
                if (timeStretchProfile == TimeStretchProfileManager.DefaultProfile)
                {
                    defaultProfileIndex = itemIndex;
                }
            }

            // Select default profile
            timeStretchProfileComboBox.SelectedIndex = defaultProfileIndex;
        }
       
        /// <summary>
        /// Initializes the Most-Recently-Used files
        /// </summary>
        private void InitializeMRUFiles()
        {
            m_recentFilesMenuItems.AddRange(new ToolStripMenuItem[] { 
                        recent1ToolStripMenuItem,  recent2ToolStripMenuItem, recent3ToolStripMenuItem, recent4ToolStripMenuItem, recent5ToolStripMenuItem,
                        recent6ToolStripMenuItem, recent7ToolStripMenuItem, recent8ToolStripMenuItem });
            foreach (ToolStripMenuItem recentMenuItem in m_recentFilesMenuItems)
            {
                recentMenuItem.Click += new EventHandler(RecentMenuItem_Click);
            }

            m_mruFile = m_appDataFolder + "\\practicesharp_mru.txt";
            m_mruManager = new MRUManager(m_recentFilesMenuItems.Count, m_mruFile);
        }

        /// <summary>
        /// Initializes the configuration
        /// </summary>
        private void InitializeConfiguration()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            m_logger.Debug("Local user config path: {0}", config.FilePath);

            // Get current application version
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            m_appVersion = assembly.GetName().Version;
            string appVersionString = m_appVersion.ToString();
            m_logger.Info("Practice# version: " + appVersionString);

            // Upgrade user settings from last version to current version, if needed
            string appVersionConfigSetting = Properties.Settings.Default.ApplicationVersion;
            if (appVersionConfigSetting != m_appVersion.ToString())
            {
                m_logger.Info("Old application version (" + appVersionConfigSetting + "), Settings upgrades is required" );
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.ApplicationVersion = appVersionString;
                Properties.Settings.Default.Save();
            }


            if (appVersionConfigSetting != string.Empty)
            {
                // Note: DirectSound seems to be buggy with NAudio - it crashes randomly
                //  WasapiOut is much more stable
                /* Version appVersionNumber = new Version(appVersionConfigSetting);
                if (appVersionNumber < new Version("1.4.3.0"))
                {
                    m_logger.Info("Changing default sound output to WasapiOut");
                    Properties.Settings.Default.SoundOutput = "WasapiOut";
                    Properties.Settings.Default.Save();
                }*/
            }

            // Show current application version
            this.Text = string.Format(Resources.AppTitle, m_appVersion.ToString());
        }


        /// <summary>
        /// Initializes the application folders (Data & Log)
        /// </summary>
        private void InitializeApplicationFolders()
        {
            // Initialize Application Data Folder - used for storing Preset Bank files
            m_appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PracticeSharp";
            if (!Directory.Exists(m_appDataFolder))
            {
                Directory.CreateDirectory(m_appDataFolder);
            }

            // Workaround for older Windows (XP or less) that don't have  LOCALAPPDATA environment variable
            // This environment variable is used by NLog layout renderer (NLog.config)
            Environment.SetEnvironmentVariable("PracticeSharpLogFolder", m_appDataFolder );
        }

        #endregion

        #region Destruction

        /// <summary>
        /// Form closing (before it actually closes) event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            playTimeUpdateTimer.Stop();

            if (m_practiceSharpLogic != null)
            {
                m_practiceSharpLogic.Terminate();

                m_practiceSharpLogic.StatusChanged -= new PracticeSharpLogic.StatusChangedEventHandler(PracticeSharpLogic_StatusChanged);
                m_practiceSharpLogic.PlayTimeChanged -= new EventHandler(PracticeSharpLogic_PlayTimeChanged);
                m_practiceSharpLogic.CueWaitPulsed -= new EventHandler(PracticeSharpLogic_CueWaitPulsed);

                m_practiceSharpLogic.Dispose();
                m_practiceSharpLogic = null;

                // Allow previous events from practice sharp logic to finish - To avoid racing on MainForm which causes exceptions in PracticeSharp event handlers
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        #endregion

        #region GUI Event Handlers

        #region Keyboard
        /// <summary>
        /// Central key handler - KeyDown (As long as a key is pressed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // [ Pitch Down
            if (e.KeyChar == '[' )
            {              
                pitchTrackBar.Value = Math.Max(pitchTrackBar.Value - pitchTrackBar.SmallChange, pitchTrackBar.Minimum);
                e.Handled = true;
            }
            // ] Pitch Up
            else if (e.KeyChar == ']' )
            {
                pitchTrackBar.Value = Math.Min(pitchTrackBar.Value + pitchTrackBar.SmallChange, pitchTrackBar.Maximum);

                e.Handled = true;
            }
            // > - Jump forward: Start
            else if ( e.KeyChar == '>' || e.KeyChar == '.')
            {
                if (!m_jumpMode)
                {
                    m_jumpMode = true;
                    m_preJumpStatus = m_practiceSharpLogic.Status;

                    if (m_preJumpStatus == PracticeSharpLogic.Statuses.Playing)
                    {
                        m_practiceSharpLogic.Pause();
                    }
                }
                JumpForward();
                e.Handled = true;
            }
            // < - Jump Backward: Start
            else if (e.KeyChar == '<' || e.KeyChar == ',')
            {
                if (!m_jumpMode)
                {
                    m_jumpMode = true;
                    m_preJumpStatus = m_practiceSharpLogic.Status;

                    if (m_preJumpStatus == PracticeSharpLogic.Statuses.Playing)
                    {
                        m_practiceSharpLogic.Pause();
                    }
                }

                JumpBackward();
                e.Handled = true;
            }
            // S - Slower tempo
            else if (e.KeyChar.ToString().ToUpper() == "S")
            {
                tempoTrackBar.Value = Math.Max(tempoTrackBar.Value - tempoTrackBar.SmallChange, tempoTrackBar.Minimum);

                e.Handled = true;
            }
            // F - Faster tempo
            else if (e.KeyChar.ToString().ToUpper() == "F")
            {
                tempoTrackBar.Value = Math.Min(tempoTrackBar.Value + tempoTrackBar.SmallChange, tempoTrackBar.Maximum);

                e.Handled = true;
            }
            // - Volume Down
            else if (e.KeyChar == '-')
            {
                volumeTrackBar.Value = Math.Max(volumeTrackBar.Value - volumeTrackBar.SmallChange, volumeTrackBar.Minimum);
                e.Handled = true;
            }
            // + Volume Up
            else if (e.KeyChar == '+' || e.KeyChar == '=')
            {
                volumeTrackBar.Value = Math.Min(volumeTrackBar.Value + volumeTrackBar.SmallChange, volumeTrackBar.Maximum);
                e.Handled = true;
            }
            else if (e.KeyChar.ToString().ToUpper() == "V" )
            {
                // Toggle remove vocals mode
                removeVocalsCheckBox.Checked = !removeVocalsCheckBox.Checked;
                e.Handled = true;
            }
  
        } 

        /// <summary>
        /// Central key handler - KeyUp (when is released)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            // Jump Mode (< or >): End
            if (!e.Control && !e.Alt && !e.Shift && ( e.KeyValue == 190 || e.KeyValue == 188) )
            {
                m_jumpMode = false;

                TempMaskOutPlayTimeTrackBar();
                TempMaskOutCurrentControls();

                if (m_preJumpStatus == PracticeSharpLogic.Statuses.Playing)
                {
                    m_practiceSharpLogic.Play();
                }
                e.Handled = true;
            }

            // F12 - Show log file
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.F12)
            {
                showTechLogToolStripMenuItem.PerformClick();
                e.Handled = true;
            }

            // 1 - Preset #1
            else if (IsPresetSelectKey(e, Keys.D1))
            {
                presetControl1.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 2 - Preset #2
            else if (IsPresetSelectKey(e, Keys.D2))
            {
                presetControl2.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 3 - Preset #3
            else if (IsPresetSelectKey(e, Keys.D3))
            {
                presetControl3.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 4 - Preset #4
            else if (IsPresetSelectKey(e, Keys.D4))
            {
                presetControl4.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 5 - Preset #5
            else if (IsPresetSelectKey(e, Keys.D5))
            {
                presetControl5.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 6 - Preset #6
            else if (IsPresetSelectKey(e, Keys.D6))
            {
                presetControl6.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 7 - Preset #7
            else if (IsPresetSelectKey(e, Keys.D7))
            {
                presetControl7.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }
            // 8 - Preset #8
            else if (IsPresetSelectKey(e, Keys.D8))
            {
                presetControl8.State = PresetControl.PresetStates.Selected;
                e.Handled = true;
            }

            // A - Set start marker Now
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
            {
                startLoopNowButton.PerformClick();
                e.Handled = true;
            }
            // Z - Set end marker Now
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Z)
            {
                endLoopNowButton.PerformClick();
                e.Handled = true;
            }

            // Ctrl + O - Open File
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.O)
            {
                openFileButton.PerformClick();
                e.Handled = true;
            }
            // Ctrl + W - Immediate write of current preset
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.W)
            {
                ImmediatelyWriteCurrentPreset();
                e.Handled = true;
            }

            // Ctrl + S - Reset speed
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.S)
            {
                speedLabel.PerformClick();
                e.Handled = true;
            }

            // Ctrl + [ - Reset pitch
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.OemOpenBrackets)
            {
                pitchLabel.PerformClick();
                e.Handled = true;
            }

            // Space - Pause/Play
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Space)
            {
                playPauseButton.PerformClick();
                e.Handled = true;
            }
            // L - Jump to start of the loop
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.L)
            {
                positionLabel.PerformClick();
                e.Handled = true;
            }
            // Ctrl + L - Toggle Loop mode On/Off
            else if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.L)
            {
                loopCheckBox.Checked = !loopCheckBox.Checked;
                e.Handled = true;
            }
            // F1 - Help
            else if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.F1)
            {
                aboutMenuItem.PerformClick();
                e.Handled = true;
            }
            // ALT + CTRL P - Toggle visibility of Time Stretch Profile' UI Controls
            else if (e.Control && e.Alt && !e.Shift && e.KeyCode == Keys.P)
            {
                timeStretchProfileLabel.Visible = !timeStretchProfileLabel.Visible;
                timeStretchProfileComboBox.Visible = !timeStretchProfileComboBox.Visible;

                e.Handled = true;
            }
            // ALT+CTRL D - Opens User Settings Folder through shell
            else if (e.Control && e.Alt && !e.Shift && e.KeyCode == Keys.D)
            {
                Process.Start("explorer.exe", m_appDataFolder);

                e.Handled = true;
            }
        }

        private bool IsPresetSelectKey(KeyEventArgs e, Keys keyCode)
        {
            return !e.Control && !e.Alt && !e.Shift && e.KeyCode == keyCode && !(this.ActiveControl is NumericUpDown);
        }
        #endregion

        /// <summary>
        /// Event handler - Shows the technical log in notepad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTechLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NLog.Targets.FileTarget target = LogManager.Configuration.FindTargetByName("logfileError") as NLog.Targets.FileTarget;
            string appLogFilename = target.FileName.ToString();
            appLogFilename = appLogFilename.Replace("${environment:PracticeSharpLogFolder}", m_appDataFolder);
            Process.Start("notepad.exe", appLogFilename);
        }
        
        /// <summary>
        /// Play/Pause button click event handler - Plays or Pauses the current play back of the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPauseButton_Click(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                playPauseButton.Image = Resources.Play_Hot;
                m_practiceSharpLogic.Pause();
            }
            else if ( (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Pausing ) ||
                      (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Ready) )
            {
                playPauseButton.Image = Resources.Pause_Hot;

                // Mask the track bar & current controls updates - to remove jumps due to old play time positions
                TempMaskOutPlayTimeTrackBar();
                TempMaskOutCurrentControls();

                m_practiceSharpLogic.Play();
            }
            else if ( m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Stopped )
            {
                // Playing has stopped, need to reload the file
                if (playTimeTrackBar.Value == playTimeTrackBar.Maximum)
                    playTimeTrackBar.Value = playTimeTrackBar.Minimum;
                m_practiceSharpLogic.LoadFile( m_currentFilename );

                playPauseButton.Image = Resources.Pause_Hot;
                m_practiceSharpLogic.Play();
            }
        }

        /// <summary>
        /// MouseEnter event handler - Handles hover start logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPauseButton_MouseEnter(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
                playPauseButton.Image = Resources.Pause_Hot;
            else if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Pausing ||
                     m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Stopped)
                playPauseButton.Image = Resources.Play_Hot;
        }

        /// <summary>
        /// MouseLeave event handler - Handles hover begin logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayPauseButton_MouseLeave(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
                playPauseButton.Image = Resources.Pause_Normal;
            else if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Pausing || 
                     m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Stopped)
                playPauseButton.Image = Resources.Play_Normal;

        }

        /// <summary>
        /// Paints the PositionMarkersPanel - Paints the Start marker, End marker and region in-between
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionMarkersPanel_Paint(object sender, PaintEventArgs e)
        {
            // don't paint if we are initializing or terminating/ed
            if (m_practiceSharpLogic == null
                || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Initializing
                || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating
                || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated )
            {
                return;
            }
            

            TimeSpan startMarker = m_practiceSharpLogic.StartMarker;
            TimeSpan endMarker = m_practiceSharpLogic.EndMarker;
            TimeSpan filePlayDuration = m_practiceSharpLogic.FilePlayDuration;

            int startMarkerX;
            int endMarkerX;
            if (filePlayDuration.TotalSeconds <= 0)
            {
                startMarkerX = 0;
                endMarkerX = 0;
            }
            else
            {
                startMarkerX = Convert.ToInt32(startMarker.TotalSeconds / filePlayDuration.TotalSeconds * positionMarkersPanel.Width);
                endMarkerX = Convert.ToInt32(endMarker.TotalSeconds / filePlayDuration.TotalSeconds * positionMarkersPanel.Width);
            }

            // Draw the whole loop region - Start marker to End Marker
            e.Graphics.FillRectangle(Brushes.Wheat, startMarkerX, 0, 
                                                             endMarkerX - startMarkerX, MarkerHeight);
            // Draw just the start marker
            e.Graphics.FillRectangle(Brushes.LightGreen, startMarkerX, 0, MarkerWidth, MarkerHeight);
            // Draw just the end marker
            e.Graphics.FillRectangle(Brushes.LightBlue, endMarkerX - MarkerWidth, 0, MarkerWidth, MarkerHeight);
        }

        /// <summary>
        /// Click event handler for Write Preset Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WritePresetButton_Click(object sender, EventArgs e)
        {
            if (!m_writeMode)
            {
                m_writeMode = true;
                // Temporary Pause play until save has completed
                if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
                {
                    m_tempSavePausePlay = true;
                    playPauseButton.Enabled = false;
                    m_practiceSharpLogic.Pause();
                }

                // Enter preset 'Write Mode'
                foreach (PresetControl presetControl in m_presetControls.Values)
                {
                    presetControl.State = PresetControl.PresetStates.WaitForSave;
                }
            }
            // Write mode is already on - Cancel write mode               
            else
            {
                m_writeMode = false;

                // Set preset controls as Selected/Off
                foreach (PresetControl presetControl in m_presetControls.Values)
                {
                    if (m_currentPreset != null && presetControl == m_currentPreset)
                    {
                        m_currentPreset.State = PresetControl.PresetStates.Selected;
                    }
                    else
                    {
                        presetControl.State = PresetControl.PresetStates.Off;
                    }
                }

                if (m_tempSavePausePlay)
                {
                    playPauseButton.Enabled = true;
                    m_practiceSharpLogic.Play();
                    m_tempSavePausePlay = false;
                }
            }
        }

        /// <summary>
        /// PresetSelected Event handler - Handles a preset select request (user clicking on preset requesting to activate it)
        /// As a result the preset values are applied
        /// Note: The preset can be selected even if it is active - in this case the values will revert to the last saved values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetControl_PresetSelected(object sender, EventArgs e)
        {
            bool isPlaying = (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing);

            if (isPlaying)
            {
                m_practiceSharpLogic.Pause();
                TempMaskOutPlayTimeTrackBar();
            }
            try
            {
                m_currentPreset = sender as PresetControl;
                foreach (PresetControl presetControl in m_presetControls.Values)
                {
                    if (presetControl != m_currentPreset)
                    {
                        presetControl.State = PresetControl.PresetStates.Off;
                    }
                }

                PresetData presetData = m_currentPreset.PresetData;
            
                ApplyPresetValueUIControls(presetData);
            }
            finally
            {
                if (isPlaying) m_practiceSharpLogic.Play();
            }
        }
        
        /// <summary>
        /// PresetDescriptionChanged Event handler - When a preset description changed the preset bank has to be rewritten to persist the change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetControl_PresetDescriptionChanged(object sender, EventArgs e)
        {
            RewritePresetsBankFile();
        }

        /// <summary>
        /// PresetSaveSelected Event handler - Handles a preset save request (user clicks on preset when Write Mode is on, i.e. Red Leds are turned on)
        /// As a result the preset is saved into the preset bank file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetControl_PresetSaveSelected(object sender, EventArgs e)
        {
            m_currentPreset = sender as PresetControl;

            // TODO: Check why start time is not kept properly when preset is saved

            foreach (PresetControl presetControl in m_presetControls.Values)
            {
                if (presetControl != m_currentPreset)
                {
                    presetControl.State = PresetControl.PresetStates.Off;
                }
                else
                {
                    if (presetControl.PresetData.Description == string.Empty)
                    {
                        presetControl.ChangeDescription();
                    }

                    UpdatePresetValues(presetControl);
                }
            }

            // (Re-)Write preset bank file
            RewritePresetsBankFile();

            if (m_tempSavePausePlay)
            {
                playPauseButton.Enabled = true;
                m_practiceSharpLogic.Play();
                m_tempSavePausePlay = false;
            }
            m_writeMode = false;
        }

        /// <summary>
        /// Volume TrackBar ValueChanged Event Handler - Changes the volume in PractceSharpLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float newVolume = volumeTrackBar.Value / 100.0f;
            m_practiceSharpLogic.Volume = newVolume;

            volumeValueLabel.Text = ( newVolume * 100 ).ToString() + "%";
        }

        /// <summary>
        /// tempoTrackBar Mouse Down - Changes the tempo to the value under the current mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempoTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            tempoTrackBar.Tag = "MouseDown";
            UpdateHorizontalTrackBarByMousePosition(tempoTrackBar, e);
        }

        /// <summary>
        /// MouseMove event handler for tempo track bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempoTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (tempoTrackBar.Tag != null && tempoTrackBar.Tag.ToString() == "MouseDown")
            {
                UpdateHorizontalTrackBarByMousePosition(tempoTrackBar, e);
            }
        }

        /// <summary>
        /// tempoTrackBar Mouse Up - Changes the tempo to the value under the current mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempoTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            tempoTrackBar.Tag = string.Empty;
            UpdateHorizontalTrackBarByMousePosition(tempoTrackBar, e);
        }

        /// <summary>
        /// pitchTrackBar Mouse Down - Changes the pitch to the value under the current mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            pitchTrackBar.Tag = "MouseDown";
            UpdateHorizontalTrackBarByMousePosition(pitchTrackBar, e);
        }

        /// <summary>
        /// MouseMove event handler for pitch track bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (pitchTrackBar.Tag != null && pitchTrackBar.Tag.ToString() == "MouseDown")
            {
                UpdateHorizontalTrackBarByMousePosition(pitchTrackBar, e);
            }
        }


        /// <summary>
        /// pitchTrackBar Mouse Up - Changes the pitch to the value under the current mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            pitchTrackBar.Tag = string.Empty;
            UpdateHorizontalTrackBarByMousePosition(pitchTrackBar, e);
        }

        /// <summary>
        /// volumeTrackBar Mouse Down - Changes the volume to the value under the current mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateVerticalTrackBarByMousePosition(volumeTrackBar, e);
        }

        /// <summary>
        /// ValueChanged Event Handler - Changes the Current play time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Don't allow re-entry of UI events when the track bar is being programmatically changed
            if (m_ignorePlayTimeUIEvents)
                return;

            TimeSpan currentPlayTime = new TimeSpan(0, 0, Convert.ToInt32(currentMinuteUpDown.Value), Convert.ToInt32(currentSecondUpDown.Value), Convert.ToInt32(currentMilliUpDown.Value));
            // Mask out PracticeSharpLogic events to eliminate 'Racing' between GUI and PracticeSharpLogic over current playtime
            TempMaskOutCurrentControls();

            UpdateCoreCurrentPlayTime( ref currentPlayTime );

            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                playTimeTrackBar.Value = Convert.ToInt32(100.0f * currentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
            }

        }

        /// <summary>
        /// Click event handler for openFileButton - Invokes the open file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            // Temporary Pause play until save has completed
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                m_tempSavePausePlay = true;
                m_practiceSharpLogic.Pause();
            }
            try
            {
                // Show the open file dialog
                openFileDialog.InitialDirectory = Properties.Settings.Default.LastAudioFolder;
                if (DialogResult.OK == openFileDialog.ShowDialog(this))
                {
                    m_tempSavePausePlay = false;

                    // Get directory path and store it as a user settting
                    FileInfo fi = new FileInfo(openFileDialog.FileName);
                    Properties.Settings.Default.LastAudioFolder = fi.Directory.FullName;
                    Properties.Settings.Default.LastFilterIndex = openFileDialog.FilterIndex;
                    Properties.Settings.Default.Save();

                    // Open the file for playing
                    OpenFile(openFileDialog.FileName, true);
                }
            }
            finally
            {
                // Only resume playing if the file was not opened (i.e. the dialog was cancelled)
                if (m_tempSavePausePlay)
                {
                    m_tempSavePausePlay = false;
                    m_practiceSharpLogic.Play();
                }
            }
        }

        /// <summary>
        /// Event handler for ValueChanged of the tempoTrackBar -
        ///   Changes the underlying tempo of PracticeSharpLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TempoTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Convert to Percent 
            float newTempo = tempoTrackBar.Value / 100.0f;
            // Assign new Tempo
            m_practiceSharpLogic.Tempo = newTempo;
            
            // Update speed value label
            if (newTempo != 1.0f)
                speedValueLabel.ForeColor = Color.Blue;
            else
                speedValueLabel.ForeColor = Color.Black;
            speedValueLabel.Text = string.Format("x{0}", newTempo);
        }

        /// <summary>
        /// Event handler for ValueChanged of the pitchTrackBar -
        ///   Changes the underlying pitch of PracticeSharpLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if ( m_practiceSharpLogic == null)
                return;

            // Convert to Percent 
            float newPitchSemiTones = pitchTrackBar.Value / ( float ) TicksPerSemitone;

            if (m_practiceSharpLogic != null)
            {
                // Assign new Pitch
                m_practiceSharpLogic.Pitch = newPitchSemiTones;
            }

            // Update Pitch value label
            string pitchValue = string.Empty;
            if (newPitchSemiTones == 0)
                pitchValue = "==";
            else
            {
                if (newPitchSemiTones > 0)
                    pitchValue = "+";
                else // ( newPitchSemiTones < 0 )
                    pitchValue = "-";

                double intPart = Math.Abs(Math.Truncate(newPitchSemiTones));
                float reminder = Math.Abs(newPitchSemiTones - (int)newPitchSemiTones);

                // Add integer part
                if (intPart > 0)
                {
                    pitchValue += intPart.ToString();
                }

                if (reminder == 0.25f)
                {
                    pitchValue += "¼";
                }
                else if (reminder == 0.5f)
                {
                    pitchValue += "½";
                }
                else if (reminder == 0.75f)
                {
                    pitchValue += "¾";
                }
            }

            if (newPitchSemiTones != 0)
                pitchValueLabel.ForeColor = Color.Blue;
            else
                pitchValueLabel.ForeColor = Color.Black;

            pitchValueLabel.Text = pitchValue;
        } 

        /// <summary>
        /// speedLabel Click event handler - Reset the tempo to default value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedLabel_Click(object sender, EventArgs e)
        {
            tempoTrackBar.Value = Convert.ToInt32( PresetData.DefaultTempo * 100 );
        }

        /// <summary>
        /// volumeLabel Click event handler - Reset the volume to default value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeLabel_Click(object sender, EventArgs e)
        {
            volumeTrackBar.Value = Convert.ToInt32(Properties.Settings.Default.DefaultVolume * 100);
        }

        /// <summary>
        /// Pitch label Click event handler - Reset the Pitch to default value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitchLabel_Click(object sender, EventArgs e)
        {
              pitchTrackBar.Value = Convert.ToInt32( PresetData.DefaultPitch );
        }

        /// <summary>
        /// positionLabel Click event handler - Reset the position to default value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionLabel_Click(object sender, EventArgs e)
        {        
            m_practiceSharpLogic.ResetCurrentPlayTime();

            // When not in play mode, track bar does not get updated so update manually
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                UpdatePlayTimeTrackBarCurrentValue();
            }
        }
    
        /// <summary>
        /// Toggle the loop mode On/Off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_practiceSharpLogic.Loop = loopCheckBox.Checked;
        }

        /// <summary>
        /// Event handler for changes in the CueComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CueComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic != null)
            {
                m_practiceSharpLogic.Cue = new TimeSpan(0, 0, Convert.ToInt32(cueComboBox.Text));
            }
        }

        private void TimeStretchProfileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic != null)
            {
                m_practiceSharpLogic.TimeStretchProfile = timeStretchProfileComboBox.Items[timeStretchProfileComboBox.SelectedIndex] as TimeStretchProfile;
            }
        }

        /// <summary>
        /// Show the keyboard shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardShortcutsMenuItem_Click(object sender, EventArgs e)
        {
            using (KeyboardShortcutsForm form = new KeyboardShortcutsForm())
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// CheckChange event handler for RemoveVocals check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveVocalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_practiceSharpLogic.SuppressVocals = removeVocalsCheckBox.Checked;
        }





        #region Menu Items

        /// <summary>
        /// About Menu Handler - Shows the About Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.AppVersion = m_appVersion;
                aboutForm.ShowDialog(this);
            }

        }

        /// <summary>
        /// Exit Event Handler - Closes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Play Time - Event handlers

        private void PlayTimeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Don't allow re-entry of UI events when the track bar is being programmtically changed
            if (m_ignorePlayTimeUIEvents)
                return;
            if (m_practiceSharpLogic == null)
                return;

            float playPosSeconds = (float)(playTimeTrackBar.Value / 100.0f * m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
            TimeSpan newPlayTime = new TimeSpan(0, 0, 0, (int)playPosSeconds,
                    (int)(100 * (playPosSeconds - Math.Truncate(playPosSeconds))));

            m_practiceSharpLogic.CurrentPlayTime = newPlayTime;
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                UpdateCurrentUpDownControls(newPlayTime);
            }
        }

        private void PlayPositionTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            m_isUpdatePlayTimeNeeded = false;
            m_playTimeTrackBarIsChanging = true;

            Application.DoEvents();
            // Let previous pending updates to position track bar to complete - otherwise the track bar 'jumps'
            Thread.Sleep(100);
            Application.DoEvents();

            UpdateNewPlayTimeByMousePos(e);
        }

        private void PlayTimeTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_playTimeTrackBarIsChanging)
            {
                UpdateNewPlayTimeByMousePos(e);
            }
        }

        private void PlayTimeTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            m_playTimeTrackBarIsChanging = false;
        }

        private object _timerLock = new object();

        private void PlayTimeUpdateTimer_Tick(object sender, EventArgs e)
        {
            // TODO: Why is this still needed??
            lock (_timerLock)
            {
                if (!m_isUpdatePlayTimeNeeded)
                    return;
            }

            // Only update when file is playing
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
                return;

            m_isUpdatePlayTimeNeeded = false;

            m_ignorePlayTimeUIEvents = true;
            try
            {
                if (DateTime.Now > m_currentControlsMaskOutTime)
                {
                    UpdateCurrentUpDownControls(m_practiceSharpLogic.CurrentPlayTime);
                }

                if (!m_playTimeTrackBarIsChanging && DateTime.Now > m_playTimeTrackBarMaskOutTime)
                {
                    UpdatePlayTimeTrackBarCurrentValue();
                }

                positionMarkersPanel.Refresh();
            }
            finally
            {
                m_ignorePlayTimeUIEvents = false;
            }
        }

        #endregion

        #region Recent Files (MRU)

        /// <summary>
        /// Show all recently played files in correct order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecentFilesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            List<string> mruItems = m_mruManager.Items;

            for (int menuItemIndex = 0; menuItemIndex < m_recentFilesMenuItems.Count; menuItemIndex++)
            {
                ToolStripMenuItem menuItem = m_recentFilesMenuItems[menuItemIndex];

                menuItem.Visible = menuItemIndex < mruItems.Count;
                if (menuItemIndex < mruItems.Count)
                {
                    string recentFilename = mruItems[menuItemIndex];
                    menuItem.Visible = true;
                    int rightIndex = 0;
                    string recentFilenameDisplay = string.Empty;
                    if (recentFilename.Length > MaxRecentDisplayLength)
                    {
                        rightIndex = recentFilename.Length - MaxRecentDisplayLength;
                        recentFilenameDisplay = recentFilename.Substring(0, 3) + " ... " + recentFilename.Substring(rightIndex, MaxRecentDisplayLength);
                    }
                    else
                    {
                        recentFilenameDisplay = recentFilename;
                    }

                    menuItem.Text = recentFilenameDisplay;
                    menuItem.Tag = recentFilename;

                    // Disable current file in recent MRU items - its already open
                    if (recentFilename == m_currentFilename)
                    {
                        menuItem.Enabled = false;
                    }
                    else
                    {
                        menuItem.Enabled = true;
                    }
                }
                else
                {
                    menuItem.Visible = false;
                }

            }
        }

        /// <summary>
        /// Open a recent file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecentMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            string selectedRecentFilename = (string)menuItem.Tag;

            // Open Recent file and start playing it
            if (!OpenFile(selectedRecentFilename, true))
            {
                m_mruManager.Remove(selectedRecentFilename);
            }
        }

        #endregion

        #region Drag & Drop

        /// <summary>
        /// Drag Enter - The inital action when the dragged file enters the form, but not released yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string droppedFile = files[0];
                Console.WriteLine("[DragEnter] DragAndDrop Files: " + droppedFile);
                if (PracticeSharpLogic.IsAudioFile(droppedFile))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    Console.WriteLine("DragAndDrop are only allowed for recognized music files");
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Drag Drop - Handles the final action when the file is dropped (released over the form)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string droppedFile = files[0];
                Console.WriteLine("[DragDrop] DragAndDrop Files: " + droppedFile);
                OpenFile(droppedFile, true);
            }
        }

        #endregion

        #region Start & End Loop Markers

        private void StartLoopSecondUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;
            TimeSpan startMarker = m_practiceSharpLogic.StartMarker;

            if (startLoopSecondUpDown.Value < 0)
            {
                startMarker = startMarker.Subtract(new TimeSpan(0, 0, 1));
            }
            else if (startLoopSecondUpDown.Value > 59)
            {
                startMarker = startMarker.Add(new TimeSpan(0, 0, 1));
            }
            else
            {
                startMarker = new TimeSpan(0, 0, startMarker.Minutes, Convert.ToInt32(startLoopSecondUpDown.Value), startMarker.Milliseconds);
            }

            UpdateCoreStartMarker(startMarker);
        }

        private void StartLoopMilliUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;
            
            TimeSpan startMarker = m_practiceSharpLogic.StartMarker;

            if (startLoopMilliUpDown.Value < 0)
            {
                startMarker = startMarker.Subtract(new TimeSpan(0, 0, 0, 0, 1));
            }
            else if (startLoopMilliUpDown.Value > 999)
            {
                startMarker = startMarker.Add(new TimeSpan(0, 0, 0, 0, 1));
            }
            else
            {
                startMarker = new TimeSpan(0, 0, startMarker.Minutes, startMarker.Seconds, Convert.ToInt32(startLoopMilliUpDown.Value));
            }

            UpdateCoreStartMarker(startMarker);
        }

        private void StartLoopMinuteUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            TimeSpan startMarker = m_practiceSharpLogic.StartMarker;

            if (startLoopMinuteUpDown.Value < 0)
            {
                startMarker = startMarker.Subtract(new TimeSpan(0, 0, 1, 0, 0));
            }
            else if (startLoopMinuteUpDown.Value > 99)
            {
                startMarker = startMarker.Add(new TimeSpan(0, 0, 1, 0, 0));
            }
            else
            {
                startMarker = new TimeSpan(0, 0, Convert.ToInt32(startLoopMinuteUpDown.Value), startMarker.Seconds, startMarker.Milliseconds);
            }

            UpdateCoreStartMarker(startMarker);
        }

        private void EndLoopSecondUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            TimeSpan endMarker = m_practiceSharpLogic.EndMarker;

            if (endLoopSecondUpDown.Value < 0)
            {
                endMarker = endMarker.Subtract(new TimeSpan(0, 0, 1));
            }
            else if (endLoopSecondUpDown.Value > 59)
            {
                endMarker = endMarker.Add(new TimeSpan(0, 0, 1));
            }
            else
            {
                endMarker = new TimeSpan(0, 0, endMarker.Minutes, Convert.ToInt32(endLoopSecondUpDown.Value), endMarker.Milliseconds);
            }

            UpdateCoreEndMarker(endMarker);
        }

        private void EndLoopMinuteUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            TimeSpan endMarker = m_practiceSharpLogic.EndMarker;

            if (endLoopMinuteUpDown.Value < 0)
            {
                endMarker = endMarker.Subtract(new TimeSpan(0, 0, 1, 0, 0));
            }
            else if (endLoopMinuteUpDown.Value > 99)
            {
                endMarker = endMarker.Add(new TimeSpan(0, 0, 1, 0, 0));
            }
            else
            {
                endMarker = new TimeSpan(0, 0, Convert.ToInt32(endLoopMinuteUpDown.Value), endMarker.Seconds, endMarker.Milliseconds);
            }

            UpdateCoreEndMarker(endMarker);
        }

        private void EndLoopMilliUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            TimeSpan endMarker = m_practiceSharpLogic.EndMarker;

            if (endLoopMilliUpDown.Value < 0)
            {
                endMarker = endMarker.Subtract(new TimeSpan(0, 0, 0, 0, 1));
            }
            else if (endLoopMilliUpDown.Value > 999)
            {
                endMarker = endMarker.Add(new TimeSpan(0, 0, 0, 0, 1));
            }
            else
            {
                endMarker = new TimeSpan(0, 0, endMarker.Minutes, endMarker.Seconds, Convert.ToInt32(endLoopMilliUpDown.Value));
            }

            UpdateCoreEndMarker(endMarker);
        }


        /// <summary>
        /// Click event handler for Start Loop Now button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartLoopNowButton_Click(object sender, EventArgs e)
        {
            // Handle special case when Now is clicked after the End marker
            if (m_practiceSharpLogic.CurrentPlayTime > m_practiceSharpLogic.EndMarker)
            {
                endLoopMinuteUpDown.Value = currentMinuteUpDown.Value;
                endLoopSecondUpDown.Value = currentSecondUpDown.Value;
                endLoopMilliUpDown.Value = currentMilliUpDown.Value;
            }

            startLoopMinuteUpDown.Value = currentMinuteUpDown.Value;
            startLoopSecondUpDown.Value = currentSecondUpDown.Value;
            startLoopMilliUpDown.Value = currentMilliUpDown.Value;
        }

        /// <summary>
        /// Click event handler for End Loop Now button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndLoopNowButton_Click(object sender, EventArgs e)
        {
            endLoopMinuteUpDown.Value = currentMinuteUpDown.Value;
            endLoopSecondUpDown.Value = currentSecondUpDown.Value;
            endLoopMilliUpDown.Value = currentMilliUpDown.Value;
        }


        #endregion

        #region Reset Bank Button event handlers

        /// <summary>
        /// Mouse Down - start the reset timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBankButton_MouseDown(object sender, MouseEventArgs e)
        {
            resetBankTimer.Start();
        }

        /// <summary>
        /// Timer tick event handler - enough time has passed since the mouse down has started, it is time to do the actual reset action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBankTimer_Tick(object sender, EventArgs e)
        {
            resetBankTimer.Stop();

            // Reset bank values
            m_currentPreset.Reset(true);
            m_currentPreset.State = PresetControl.PresetStates.Selected;
            ApplyPresetValueUIControls(m_currentPreset.PresetData);

            RewritePresetsBankFile();
        }

        /// <summary>
        /// Mouse was up - Cancel the reset action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBankButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (resetBankTimer.Enabled)
            {
                // Cancel reset
                resetBankTimer.Stop();
            }
        }

        #endregion

        #region Equalizer

        private void LoEqTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            float newEqLo = loEqTrackBar.Value / 100.0f;
            m_practiceSharpLogic.EqualizerLoBand = newEqLo;

            // Update Value Label
            loEqValueLabel.Text = (newEqLo * 100).ToString() + "%";
        }

        private void MedEqTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            float newEqMed = medEqTrackBar.Value / 100.0f;
            m_practiceSharpLogic.EqualizerMedBand = newEqMed;

            // Update Value Label
            medEqValueLabel.Text = (newEqMed * 100).ToString() + "%";
        }

        private void HiEqTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null)
                return;

            float newEqHi = hiEqTrackBar.Value / 100.0f;
            m_practiceSharpLogic.EqualizerHiBand = newEqHi;

            // Update Value Label
            hiEqValueLabel.Text = (newEqHi * 100).ToString() + "%";
        }

        /// <summary>
        /// Equalizer label click event handler - resets all equalizer band to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualizerHoverLabel_Click(object sender, EventArgs e)
        {
            loEqHoverLabel.PerformClick();
            medEqHoverLabel.PerformClick();
            hiEqHoverLabel.PerformClick();
        }

        /// <summary>
        /// Lo Equalizer Label click event handler - resets the Lo equalizer band to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoEqHoverLabel_Click(object sender, EventArgs e)
        {
            loEqTrackBar.Value = Convert.ToInt32(PresetData.DefaultLoEq);
        }

        /// <summary>
        /// Med Equalizer Label click event handler - resets the Med equalizer band to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedEqHoverLabel_Click(object sender, EventArgs e)
        {
            medEqTrackBar.Value = Convert.ToInt32(PresetData.DefaultMedEq);
        }

        /// <summary>
        /// Hi Equalizer Label click event handler - resets the Hi equalizer band to default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiEqHoverLabel_Click(object sender, EventArgs e)
        {
            hiEqTrackBar.Value = Convert.ToInt32(PresetData.DefaultHiEq);
        }

        private void LoEqtrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateVerticalTrackBarByMousePosition(loEqTrackBar, e);
        }

        private void MedEqTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateVerticalTrackBarByMousePosition(medEqTrackBar, e);
        }

        private void HiEqTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateVerticalTrackBarByMousePosition(hiEqTrackBar, e);
        }

        #endregion Equalizer

        #region Input Channel Selection

        private void LeftChannelStripButton_Click(object sender, EventArgs e)
        {
            leftChannelStripButton.Checked = true;
            bothChannelsStripButton.Checked = false;
            dualMonoToolStripButton.Checked = false;
            rightChannelStripButton.Checked = false;
            m_practiceSharpLogic.InputChannelsMode = InputChannelsModes.Left;
        }

        private void BothChannelsStripButton_Click(object sender, EventArgs e)
        {
            leftChannelStripButton.Checked = false;
            bothChannelsStripButton.Checked = true;
            dualMonoToolStripButton.Checked = false;
            rightChannelStripButton.Checked = false;
            m_practiceSharpLogic.InputChannelsMode = InputChannelsModes.Both;
        }

        private void dualMonoToolStripButton_Click(object sender, EventArgs e)
        {
            leftChannelStripButton.Checked = false;
            bothChannelsStripButton.Checked = false;
            dualMonoToolStripButton.Checked = true;
            rightChannelStripButton.Checked = false;
            m_practiceSharpLogic.InputChannelsMode = InputChannelsModes.DualMono;
        }

        private void RightChannelStripButton_Click(object sender, EventArgs e)
        {
            leftChannelStripButton.Checked = false;
            bothChannelsStripButton.Checked = false;
            dualMonoToolStripButton.Checked = false;
            rightChannelStripButton.Checked = true;
            m_practiceSharpLogic.InputChannelsMode = InputChannelsModes.Right;
        }

        #endregion

        private void swapLRCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_practiceSharpLogic.SwapLeftRightSpeakers = swapLRCheckBox.Checked;
        }

        #endregion

        #region PracticeSharpLogic Event Handlers

        /// <summary>
        /// Event handler for PracticeSharpLogic status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newStatus"></param>
        private void PracticeSharpLogic_StatusChanged(object sender, PracticeSharpLogic.Statuses newStatus)
        {
            if ( m_jumpMode || m_practiceSharpLogic == null || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated)
                return;
            
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                if (m_practiceSharpLogic == null || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated)
                    return;

                appStatusLabel.Text = newStatus.ToString();

                if ( (newStatus == PracticeSharpLogic.Statuses.Stopped)
                   || (newStatus == PracticeSharpLogic.Statuses.Pausing)
                   || (newStatus == PracticeSharpLogic.Statuses.Error) )
                {
                    playPauseButton.Image = Resources.Play_Normal;
                    playTimeUpdateTimer.Enabled = false;

                    // m_ignorePlayTimeUIEvents = true;
                    // Force a last refresh of play time controls
                    UpdateCurrentUpDownControls(m_practiceSharpLogic.CurrentPlayTime);
                    int currentPlayTimeValue = 0;
                    if (m_practiceSharpLogic.FilePlayDuration.TotalSeconds > 0)
                    {
                        currentPlayTimeValue = Convert.ToInt32(100.0f * m_practiceSharpLogic.CurrentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
                        if (currentPlayTimeValue > playTimeTrackBar.Maximum)
                            currentPlayTimeValue = playTimeTrackBar.Maximum;
                    }
                    playTimeTrackBar.Value = currentPlayTimeValue;
                    positionMarkersPanel.Refresh();
                }
                else if (newStatus == PracticeSharpLogic.Statuses.Playing)
                {
                    playPauseButton.Image = Resources.Pause_Normal;
                    playTimeUpdateTimer.Enabled = true;
                }
            } )
            );
        }

        private void PracticeSharpLogic_PlayTimeChanged(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated)
                return;

            this.Invoke(
                new MethodInvoker(delegate()
                {
                    m_isUpdatePlayTimeNeeded = true;
                }));
        }

        private void PracticeSharpLogic_CueWaitPulsed(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic == null || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated)
                return;

            this.BeginInvoke(

                new MethodInvoker(delegate()
                {
                    if (m_practiceSharpLogic == null || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminating || m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Terminated)
                        return;

                    // Pulse the cue led once
                    cuePictureBox.Image = Resources.blue_on_16;
                    Application.DoEvents();

                    Thread.Sleep(100);
                    cuePictureBox.Image = Resources.blue_off_16;

                    Application.DoEvents();
                }
             ));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Automatically loads the last played file (and its presets bank if it exists)
        /// </summary>
        private void AutoLoadLastFile()
        {
            string lastFilename = Properties.Settings.Default.LastFilename;

            if (File.Exists(lastFilename))
            {
                // Open file but don't start playing automatically
                OpenFile(lastFilename, false);
            }
        }

        /// <summary>
        /// Open the given file
        /// </summary>
        /// <param name="filename"></param>
        private bool OpenFile(string filename, bool autoPlay)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!File.Exists(filename))
                {
                    MessageBox.Show("File does not exist: " + filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                playTimeUpdateTimer.Enabled = false;
                m_practiceSharpLogic.Stop();

                m_mruManager.Add(filename);

                Properties.Settings.Default.LastFilename = filename;
                Properties.Settings.Default.Save();

                // Reset current UI Controls
                foreach (PresetControl presetControl in m_presetControls.Values)
                {
                    presetControl.Reset( false );
                }
                ApplyPresetValueUIControls(m_presetControls["1"].PresetData);
                m_currentFilename = filename;
                filenameLabel.Text = Path.GetFileName( filename );
                m_practiceSharpLogic.LoadFile(filename);

                // Load Presets Bank for this input file
                m_presetBankFile = new PresetBankFile(m_appDataFolder,m_appVersion.ToString(),m_currentFilename);
                string activePresetId = m_presetBankFile.LoadPresetsBank(m_presetControls);

                // If no preset is active, select the first one by default
                if (activePresetId == null)
                    activePresetId = "1";

                m_currentPreset = m_presetControls[activePresetId];
                m_currentPreset.State = PresetControl.PresetStates.Selected;


                EnableControls(true);

                playDurationLabel.Text =
                       string.Format("{0}:{1}", m_practiceSharpLogic.FilePlayDuration.Minutes.ToString("00"),
                                    m_practiceSharpLogic.FilePlayDuration.Seconds.ToString("00"));
                play1QDurationLabel.Text =
                       string.Format("{0}:{1}", (m_practiceSharpLogic.FilePlayDuration.TotalSeconds / 4 / 60).ToString("00"),
                                    (m_practiceSharpLogic.FilePlayDuration.Seconds / 4).ToString("00"));
                play2QDurationLabel.Text =
                       string.Format("{0}:{1}", (m_practiceSharpLogic.FilePlayDuration.Minutes / 2).ToString("00"),
                                    (m_practiceSharpLogic.FilePlayDuration.Seconds / 2).ToString("00"));
                play3QDurationLabel.Text =
                       string.Format("{0}:{1}", (m_practiceSharpLogic.FilePlayDuration.TotalSeconds * 3 / 4 / 60).ToString("00"),
                                    (m_practiceSharpLogic.FilePlayDuration.Seconds * 3 / 4).ToString("00"));

                if (autoPlay)
                {
                    playPauseButton.Image = Resources.Pause_Normal;
                    m_practiceSharpLogic.Play();
                }
            }
            finally
            {
                playTimeUpdateTimer.Enabled = true;
                Cursor.Current = Cursors.Default;
            }

            return true;
        }

        /// <summary>
        /// Utility function - Enables/Disabled UI controls
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableControls(bool enabled)
        {
            tempoTrackBar.Enabled = enabled;
            pitchTrackBar.Enabled = enabled;
            volumeTrackBar.Enabled = enabled;
            playTimeTrackBar.Enabled = enabled;
            playPauseButton.Enabled = enabled;
            startLoopNowButton.Enabled = enabled;
            endLoopNowButton.Enabled = enabled;
            cueComboBox.Enabled = enabled;
            writePresetButton.Enabled = enabled;
            resetPresetButton.Enabled = enabled;
            presetControl1.Enabled = enabled;
            presetControl2.Enabled = enabled;
            presetControl3.Enabled = enabled;
            presetControl4.Enabled = enabled;
            loopPanel.Enabled = enabled;
            currentMinuteUpDown.Enabled = enabled;
            currentSecondUpDown.Enabled = enabled;
            currentMilliUpDown.Enabled = enabled;
        }

        /// <summary>
        /// Updates the CurrentPlayTime from the UI Current controls
        /// </summary>
        private void UpdateCoreCurrentPlayTime( ref TimeSpan currentPlayTime )
        {
            // Clip to actual file duration limits (0..FilePlayDuration)
            if ( currentPlayTime > m_practiceSharpLogic.FilePlayDuration )
                currentPlayTime = m_practiceSharpLogic.FilePlayDuration;
            else if (currentPlayTime < TimeSpan.Zero)
                currentPlayTime = TimeSpan.Zero;

            m_practiceSharpLogic.CurrentPlayTime = currentPlayTime;

            UpdateCurrentUpDownControls( currentPlayTime );
        }

        /// <summary>
        /// Updates the start marker value
        /// </summary>
        /// <param name="startMarker"></param>
        private void UpdateCoreStartMarker(TimeSpan startMarker)
        {
            if (startMarker > m_practiceSharpLogic.EndMarker)
            {
                startMarker = m_practiceSharpLogic.EndMarker;
            }
            else if (startMarker < TimeSpan.Zero)
            {
                startMarker = TimeSpan.Zero;
            }

            m_practiceSharpLogic.StartMarker = startMarker;

            positionMarkersPanel.Refresh();

            ApplyLoopStartMarkerUI(startMarker);
        }

        /// <summary>
        /// Updates the end marker value
        /// </summary>
        /// <param name="endMarker"></param>
        private void UpdateCoreEndMarker(TimeSpan endMarker)
        {
            if (endMarker < m_practiceSharpLogic.StartMarker)
            {
                endMarker = m_practiceSharpLogic.StartMarker;
            }
            else if (endMarker > m_practiceSharpLogic.FilePlayDuration)
            {
                endMarker = m_practiceSharpLogic.FilePlayDuration;
            }

            m_practiceSharpLogic.EndMarker = endMarker;

            positionMarkersPanel.Refresh();

            ApplyLoopEndMarkerUI(endMarker);
        }

        /// <summary>
        /// Updates all StartLoop UpDown controls together
        /// </summary>
        /// <param name="startMarker"></param>
        private void ApplyLoopStartMarkerUI(TimeSpan startMarker)
        {
            startLoopMinuteUpDown.ValueChanged -= StartLoopMinuteUpDown_ValueChanged;
            startLoopSecondUpDown.ValueChanged -= StartLoopSecondUpDown_ValueChanged;
            startLoopMilliUpDown.ValueChanged -= StartLoopMilliUpDown_ValueChanged;
            try
            {
                startLoopMinuteUpDown.Value = startMarker.Minutes;
                startLoopSecondUpDown.Value = startMarker.Seconds;
                startLoopMilliUpDown.Value = startMarker.Milliseconds;
            }
            finally
            {
                startLoopMinuteUpDown.ValueChanged += StartLoopMinuteUpDown_ValueChanged;
                startLoopSecondUpDown.ValueChanged += StartLoopSecondUpDown_ValueChanged;
                startLoopMilliUpDown.ValueChanged += StartLoopMilliUpDown_ValueChanged;
            }
        }

        /// <summary>
        /// Updates all EndLoop UpDown controls together
        /// </summary>
        /// <param name="endMarker"></param>
        private void ApplyLoopEndMarkerUI(TimeSpan endMarker)
        {
            endLoopMinuteUpDown.ValueChanged -= EndLoopMinuteUpDown_ValueChanged;
            endLoopSecondUpDown.ValueChanged -= EndLoopSecondUpDown_ValueChanged;
            endLoopMilliUpDown.ValueChanged -= EndLoopMilliUpDown_ValueChanged;
            try
            {
                endLoopMinuteUpDown.Value = endMarker.Minutes;
                endLoopSecondUpDown.Value = endMarker.Seconds;
                endLoopMilliUpDown.Value = endMarker.Milliseconds;
            }
            finally
            {
                endLoopMinuteUpDown.ValueChanged += EndLoopMinuteUpDown_ValueChanged;
                endLoopSecondUpDown.ValueChanged += EndLoopSecondUpDown_ValueChanged;
                endLoopMilliUpDown.ValueChanged += EndLoopMilliUpDown_ValueChanged;
            }
        }

        /// <summary>
        /// Updates all current UpDown controls together
        /// </summary>
        /// <param name="playTime"></param>
        private void UpdateCurrentUpDownControls(TimeSpan playTime)
        {
            currentMinuteUpDown.ValueChanged -= CurrentUpDown_ValueChanged;
            currentSecondUpDown.ValueChanged -= CurrentUpDown_ValueChanged;
            currentMilliUpDown.ValueChanged -= CurrentUpDown_ValueChanged;
            try
            {
                // Update current play time controls
                currentMinuteUpDown.Value = playTime.Minutes;
                currentSecondUpDown.Value = playTime.Seconds;
                currentMilliUpDown.Value = playTime.Milliseconds;
            }
            finally
            {
                currentMinuteUpDown.ValueChanged += CurrentUpDown_ValueChanged;
                currentSecondUpDown.ValueChanged += CurrentUpDown_ValueChanged;
                currentMilliUpDown.ValueChanged += CurrentUpDown_ValueChanged;
            }
        }

        /// <summary>
        /// Utility function - updates a horizontal track bar by a given mouse position
        /// </summary>
        /// <param name="trackBar"></param>
        /// <param name="e"></param>
        private void UpdateHorizontalTrackBarByMousePosition(TrackBar trackBar, MouseEventArgs e)
        {
            const int TrackBarMargin = 10;
            int maxValue = Convert.ToInt32( trackBar.Maximum );
            int minValue = Convert.ToInt32( trackBar.Minimum );
            int newValue = Convert.ToInt32(minValue + (maxValue - minValue) * (((float)e.X - TrackBarMargin) / (trackBar.Width - TrackBarMargin * 2)));

            // Make value 'sticky' - it can only get a tick value
            int mod = newValue % trackBar.SmallChange;
            if (Math.Abs(mod) > trackBar.SmallChange / 2)
                newValue = newValue + Math.Sign(mod) * trackBar.SmallChange - mod;
            else
                newValue = newValue - mod;
            
            // Limit new values
            if (newValue > maxValue)
                newValue = maxValue;
            else if (newValue < minValue)
                newValue = minValue;

            trackBar.Value = newValue;
        }

        /// <summary>
        /// Utility function - updates a vertical track bar by a given mouse position
        /// </summary>
        /// <param name="trackBar"></param>
        /// <param name="e"></param>
        private void UpdateVerticalTrackBarByMousePosition(TrackBar trackBar, MouseEventArgs e)
        {
            const int TrackBarMargin = 10;
            float maxValue = trackBar.Maximum;
            float minValue = trackBar.Minimum;
            float newValue = maxValue - (maxValue - minValue) * (((float)e.Y - TrackBarMargin) / (trackBar.Height - TrackBarMargin * 2));
            if (newValue > maxValue)
                newValue = maxValue;
            else if (newValue < minValue)
                newValue = minValue;

            int newTrackBarValue = Convert.ToInt32(newValue);

            trackBar.Value = newTrackBarValue;
        }

        /// <summary>
        /// Utility function - updates the new play time by a given mouse position
        /// </summary>
        /// <param name="e"></param>
        private void UpdateNewPlayTimeByMousePos(MouseEventArgs e)
        {
            const int TrackBarMargin = 10;
            float duration = (float)m_practiceSharpLogic.FilePlayDuration.TotalSeconds;
            float newValue = duration * (((float)e.X - TrackBarMargin) / (playTimeTrackBar.Width - TrackBarMargin * 2));
            if (newValue > duration)
                newValue = duration;
            else if (newValue < 0)
                newValue = 0;

            TimeSpan newPlayTime = new TimeSpan(0, 0, Convert.ToInt32(newValue));

            int newTrackBarValue = 0;
            if (duration != 0)
                newTrackBarValue = Convert.ToInt32(newValue / duration * 100.0f);

            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                m_practiceSharpLogic.CurrentPlayTime = newPlayTime;

                m_ignorePlayTimeUIEvents = true;
                try
                {
                    // Time in which playtime trackbar updates coming from PracticeSharpLogic are not allowed (to eliminate 'Jumps' due to old locations)
                    TempMaskOutPlayTimeTrackBar();

                    playTimeTrackBar.Value = newTrackBarValue;
                }
                finally
                {
                    m_ignorePlayTimeUIEvents = false;
                }
            }
            else
            {
                playTimeTrackBar.Value = newTrackBarValue;
            }
        }

        /// <summary>
        /// Helper function - Updates the play time track bar value to the CurrentPlayTime
        /// </summary>
        private void UpdatePlayTimeTrackBarCurrentValue()
        {
            int currentPlayTimeValue = Convert.ToInt32(100.0f * m_practiceSharpLogic.CurrentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
            if (currentPlayTimeValue > playTimeTrackBar.Maximum)
            {
                currentPlayTimeValue = playTimeTrackBar.Maximum;
            }

            playTimeTrackBar.Value = currentPlayTimeValue;
        }

        /// <summary>
        /// Mask out playtime TrackBar update messages for some time to avoid trackbar jumps 
        /// </summary>
        private void TempMaskOutPlayTimeTrackBar()
        {
            m_playTimeTrackBarMaskOutTime = DateTime.Now.AddMilliseconds(MaskOutInterval);
        }

        /// <summary>
        /// Mask out updates of current up-down controls to avoid jumps
        /// </summary>
        private void TempMaskOutCurrentControls()
        {
            m_currentControlsMaskOutTime = DateTime.Now.AddMilliseconds(MaskOutInterval);
        }

        /// <summary>
        /// Jump a few seconds forward
        /// </summary>
        private void JumpForward()
        {
            TempMaskOutPlayTimeTrackBar();
            TempMaskOutCurrentControls();

            m_practiceSharpLogic.CurrentPlayTime = m_practiceSharpLogic.CurrentPlayTime.Add(new TimeSpan(0, 0, JumpSeconds));            

            // When not in play mode, track bar does not get updated so update manually
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                UpdatePlayTimeTrackBarCurrentValue();
            }
        }

        /// <summary>
        /// Jump a few seconds backward
        /// </summary>
        private void JumpBackward()
        {
            TempMaskOutPlayTimeTrackBar();
            TempMaskOutCurrentControls();

            m_practiceSharpLogic.CurrentPlayTime = m_practiceSharpLogic.CurrentPlayTime.Subtract(new TimeSpan(0, 0, JumpSeconds));

            // When not in play mode, track bar does not get updated so update manually
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                UpdatePlayTimeTrackBarCurrentValue();
            }
        }

        #region Presets

        /// <summary>
        /// Re-Writes the presets bank file with data from the current presets - Delegates handling to PresetBankFile
        /// </summary>
        private void RewritePresetsBankFile()
        {
            // (Re-)Write preset bank file
            m_presetBankFile.WritePresetsBank(m_presetControls, m_currentPreset.Id);
        }

        /// <summary>
        /// Applies the preset values to UI controls - Effectively loads the UI controls with preset values
        /// </summary>
        /// <param name="presetData"></param>
        private void ApplyPresetValueUIControls(PresetData presetData)
        {
            // Apply preset values
            tempoTrackBar.Value = Convert.ToInt32(presetData.Tempo * 100.0f);
            pitchTrackBar.Value = Convert.ToInt32(presetData.Pitch * TicksPerSemitone);
            volumeTrackBar.Value = Convert.ToInt32(presetData.Volume * 100.0f);
            loEqTrackBar.Value = Convert.ToInt32(presetData.LoEqValue * 100.0f);
            LoEqTrackBar_ValueChanged(this, new EventArgs());
            medEqTrackBar.Value = Convert.ToInt32(presetData.MedEqValue * 100.0f);
            MedEqTrackBar_ValueChanged(this, new EventArgs());
            hiEqTrackBar.Value = Convert.ToInt32(presetData.HiEqValue * 100.0f);
            HiEqTrackBar_ValueChanged(this, new EventArgs());

            if (m_practiceSharpLogic.FilePlayDuration == TimeSpan.Zero)
            {
                playTimeTrackBar.Value = playTimeTrackBar.Minimum;
            }
            else
            {
                // Protect against invalid preset values
                if (presetData.CurrentPlayTime.TotalSeconds > m_practiceSharpLogic.FilePlayDuration.TotalSeconds)
                {
                    presetData.CurrentPlayTime = TimeSpan.Zero;
                    presetData.StartMarker = TimeSpan.Zero;
                    presetData.EndMarker = TimeSpan.Zero;
                }

                // Set the PlayTimeTrackBar value to the preset's CurrentPlayTime
                
                playTimeTrackBar.Value = Convert.ToInt32(100.0f * presetData.CurrentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
            }

            ApplyLoopStartMarkerUI(presetData.StartMarker);
            ApplyLoopEndMarkerUI(presetData.EndMarker);

            m_practiceSharpLogic.StartMarker = presetData.StartMarker;
            m_practiceSharpLogic.EndMarker = presetData.EndMarker;

            int cueItemIndex = cueComboBox.FindString(Convert.ToInt32(presetData.Cue.TotalSeconds).ToString());
            cueComboBox.SelectedIndex = cueItemIndex;
            loopCheckBox.Checked = presetData.Loop;
            positionMarkersPanel.Refresh();

            // Find matching Time Stretch Profile
            for ( int itemIndex = 0; itemIndex < timeStretchProfileComboBox.Items.Count; itemIndex++ )
            {
                TimeStretchProfile profile = timeStretchProfileComboBox.Items[itemIndex] as TimeStretchProfile;
                if (profile.Id == presetData.TimeStretchProfile.Id)
                {
                    timeStretchProfileComboBox.SelectedIndex = itemIndex;
                    break;
                }
            }
            timeStretchProfileComboBox.SelectedValue = presetData.TimeStretchProfile;

            removeVocalsCheckBox.Checked = presetData.RemoveVocals;
            switch (presetData.InputChannelsMode)
            {
                case InputChannelsModes.Left:
                    leftChannelStripButton.PerformClick();
                    break;
                case InputChannelsModes.Right:
                    rightChannelStripButton.PerformClick();
                    break;
                case InputChannelsModes.DualMono:
                    dualMonoToolStripButton.PerformClick();
                    break;
                default:
                    bothChannelsStripButton.PerformClick();
                    break;
            }
            swapLRCheckBox.Checked = presetData.SwapLeftRightSpeakers;
        }

        /// <summary>
        /// Immediately writes the state into the currently selected preset, without going through the two-phase button write mechanism
        /// </summary>
        private void ImmediatelyWriteCurrentPreset()
        {
            if (m_currentPreset == null)
                return;

            bool isPlaying = false;
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                m_practiceSharpLogic.Pause();
                isPlaying = true;
            }

            m_currentPreset.State = PresetControl.PresetStates.Saving;

            UpdatePresetValues(m_currentPreset);

            // (Re-)Write preset bank file
            RewritePresetsBankFile();

            m_currentPreset.State = PresetControl.PresetStates.Selected;

            if (isPlaying)
                m_practiceSharpLogic.Play();
        }

        /// <summary>
        /// Updates the preset values of the given present control, with the current session values
        /// </summary>
        /// <param name="presetControl"></param>
        private void UpdatePresetValues(PresetControl presetControl)
        {
            presetControl.PresetData.Tempo = m_practiceSharpLogic.Tempo;
            presetControl.PresetData.Pitch = m_practiceSharpLogic.Pitch;
            presetControl.PresetData.Volume = m_practiceSharpLogic.Volume;
            presetControl.PresetData.LoEqValue = m_practiceSharpLogic.EqualizerLoBand;
            presetControl.PresetData.MedEqValue = m_practiceSharpLogic.EqualizerMedBand;
            presetControl.PresetData.HiEqValue = m_practiceSharpLogic.EqualizerHiBand;
            presetControl.PresetData.CurrentPlayTime = m_practiceSharpLogic.CurrentPlayTime;
            presetControl.PresetData.StartMarker = m_practiceSharpLogic.StartMarker;
            presetControl.PresetData.EndMarker = m_practiceSharpLogic.EndMarker;
            presetControl.PresetData.Cue = m_practiceSharpLogic.Cue;
            presetControl.PresetData.Loop = m_practiceSharpLogic.Loop;
            presetControl.PresetData.Description = presetControl.PresetDescription;
            presetControl.PresetData.TimeStretchProfile = m_practiceSharpLogic.TimeStretchProfile;
            presetControl.PresetData.RemoveVocals = m_practiceSharpLogic.SuppressVocals;
            presetControl.PresetData.InputChannelsMode = m_practiceSharpLogic.InputChannelsMode;
            presetControl.PresetData.SwapLeftRightSpeakers = m_practiceSharpLogic.SwapLeftRightSpeakers;
        }

        #endregion


        #endregion 

        #region Private Members

        private bool m_isUpdatePlayTimeNeeded;
        private PracticeSharpLogic m_practiceSharpLogic;

        /// <summary>
        /// PresetControls dictionary:
        /// Key = Id
        /// Value = PresetControl Instance
        /// </summary>
        private Dictionary<string, PresetControl> m_presetControls;
        private PresetControl m_currentPreset;

        private bool m_ignorePlayTimeUIEvents = false;
        private bool m_playTimeTrackBarIsChanging = false;

        /// <summary>
        /// Flag for Temporary Pausing Play while saving
        /// </summary>
        private bool m_tempSavePausePlay = false;

        private bool m_writeMode = false;

        private DateTime m_playTimeTrackBarMaskOutTime = DateTime.Now;
        private DateTime m_currentControlsMaskOutTime = DateTime.Now;

        private string m_currentFilename;
        private PresetBankFile m_presetBankFile;
        private Version m_appVersion;
        private string m_appDataFolder;

        private MRUManager m_mruManager;
        private string m_mruFile;
        private List<ToolStripMenuItem> m_recentFilesMenuItems = new List<ToolStripMenuItem>();

        private VersionUpdater m_versionUpdater;

        private PracticeSharpLogic.Statuses m_preJumpStatus;
        private bool m_jumpMode = false;

        #endregion

        #region Constants

        const int MarkerWidth = 5; 
        const int MarkerHeight = 10;

        const int MaxRecentDisplayLength = 60;

        // msec
        const int MaskOutInterval = 450;

        const short JumpSeconds = 2;

        // 96 ticks are 12 semitones => each 8 ticks is one semitone
        const int TicksPerSemitone = 8;

        #endregion   

      

    }
}
