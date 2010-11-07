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

namespace BigMansStuff.PracticeSharp.UI
{
    public partial class MainForm : Form
    {
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
            try
            {
                InitializeApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed initialize the Practice Sharp back end - " + ex.ToString());
            }

            AutoLoadLastFile();
        }

        /// <summary>
        /// Initialize the PracticeSharp Application
        /// </summary>
        private void InitializeApplication()
        {
            // Get and show current application version
            m_appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = "Practice# Version (" + m_appVersion.ToString() + ")";

            // Initialize Application Date Folder - used for storing Preset Bank files
            m_appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PracticeSharp";
            if (!Directory.Exists(m_appDataFolder))
            {
                Directory.CreateDirectory(m_appDataFolder);
            }

            // Create the PracticeSharpLogic back end layer
            m_practiceSharpLogic = new PracticeSharpLogic();
            m_practiceSharpLogic.Initialize();
            m_practiceSharpLogic.StatusChanged += new PracticeSharpLogic.StatusChangedEventHandler(practiceSharpLogic_StatusChanged);
            m_practiceSharpLogic.PlayTimeChanged += new EventHandler(practiceSharpLogic_PlayTimeChanged);
            m_practiceSharpLogic.CueWaitPulsed += new EventHandler(practiceSharpLogic_CueWaitPulsed);

            playPauseButton.Enabled = false;
            startLoopNowButton.Enabled = false;
            endLoopNowButton.Enabled = false;
            cueComboBox.Enabled = false;
            writeBankButton.Enabled = false;
            resetBankButton.Enabled = false;

            openFileDialog.InitialDirectory = Properties.Settings.Default.LastAudioFolder;

            playPauseButton.Image = Resources.Play_Normal;
            writeBankButton.Image = Resources.save_icon;
            resetBankButton.Image = Resources.Eraser_icon;
            openFileButton.Image = Resources.OpenFile_icon;

            cueComboBox.SelectedIndex = 0;
            m_presetControls = new Dictionary<string, PresetControl>();
            m_presetControls.Add("1", presetControl1);
            m_presetControls.Add("2", presetControl2);
            m_presetControls.Add("3", presetControl3);
            m_presetControls.Add("4", presetControl4);


            // Set defaults
            speedTrackBar_ValueChanged(this, new EventArgs());
            volumeTrackBar_ValueChanged(this, new EventArgs());
            playTimeTrackBar_ValueChanged(this, new EventArgs());

            presetControl1.State = PresetControl.PresetStates.Selected;
        }



        #endregion

        // TODO: Clicking on Volume mutes/unmutes
        // TODO: Speed/Volume/Position labels need to change color when hovering over them to show that the label is active
        // FIXED: Put labels in play track bar for 1/4, 1/2 and 3/4, that will show the duration time
        // FIXED: Put 25%, 50%, 75% labels for volume
        // FIXED: Put X0.5, X1.5 for Speed
        // TODO: Create/Find a hover button control that switches images from Regular image to hot image
        // FIXED: Play does not work properly when current play time is in the end after stopping - it should jump to 0 or startMarker
        // TODO: Add another track bar for transposing voice up/down 
        // TODO: SoundTouch Release DLL crashes. Check why Debug works but Release does not.

        #region Destruction

        /// <summary>
        /// Form closing (before it actually closes) event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_practiceSharpLogic != null)
            {
                m_practiceSharpLogic.Dispose();
            }
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
        private void OpenFile(string filename, bool autoPlay)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                playTimeUpdateTimer.Enabled = false;

                Properties.Settings.Default.LastFilename = filename;
                Properties.Settings.Default.Save();

                m_currentFilename = filename;
                filenameToolStripStatusLabel.Text = filename;
                m_practiceSharpLogic.LoadFile(filename);

                // Load Presets Bank for this input file
                LoadPresetsBank();

                // playTimeTrackBar.Maximum = Convert.ToInt32(m_practiceSharpLogic.FilePlayDuration.TotalSeconds);

                playPauseButton.Enabled = true;
                startLoopNowButton.Enabled = true;
                endLoopNowButton.Enabled = true;
                cueComboBox.Enabled = true;
                writeBankButton.Enabled = true;
                resetBankButton.Enabled = true;               


                playDurationLabel.Text = 
                       string.Format( "{0}:{1}", m_practiceSharpLogic.FilePlayDuration.Minutes.ToString( "00" ),
                                    m_practiceSharpLogic.FilePlayDuration.Seconds.ToString( "00" )  ) ;
                play1QDurationLabel.Text = 
                       string.Format( "{0}:{1}", ( m_practiceSharpLogic.FilePlayDuration.TotalSeconds / 4 / 60 ).ToString( "00" ),
                                    ( m_practiceSharpLogic.FilePlayDuration.Seconds / 4 ).ToString( "00" )  ) ;
                play2QDurationLabel.Text =
                       string.Format("{0}:{1}", (m_practiceSharpLogic.FilePlayDuration.Minutes / 2).ToString("00"),
                                    (m_practiceSharpLogic.FilePlayDuration.Seconds / 2).ToString("00"));
                play3QDurationLabel.Text =
                       string.Format("{0}:{1}", (m_practiceSharpLogic.FilePlayDuration.TotalSeconds * 3 / 4 / 60).ToString("00"),
                                    (m_practiceSharpLogic.FilePlayDuration.Seconds * 3 / 4 ).ToString("00"));

                playTimeUpdateTimer.Enabled = true;

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
        }

      
        private void UpdateCoreCurrentPlayTime()
        {
            m_practiceSharpLogic.CurrentPlayTime = new TimeSpan(0, 0, (int)currentMinuteUpDown.Value, (int)currentSecondUpDown.Value, (int)currentMilliUpDown.Value);
        }

        private void UpdateCoreStartMarker()
        {
            m_practiceSharpLogic.StartMarker = new TimeSpan(0, 0, (int)startLoopMinuteUpDown.Value, (int)startLoopSecondUpDown.Value, (int)startLoopMilliUpDown.Value);

            positionMarkersPanel.Refresh();
        }

        private void UpdateCoreEndMarker()
        {
            m_practiceSharpLogic.EndMarker = new TimeSpan(0, 0, (int)endLoopMinuteUpDown.Value, (int)endLoopSecondUpDown.Value, (int)endLoopMilliUpDown.Value);

            positionMarkersPanel.Refresh();
        }

        private void LoadPresetsBank()
        {
            m_presetsBankFilename = m_appDataFolder + "\\" + Path.GetFileName(m_currentFilename) + ".practicesharpbank.xml";

            if (!File.Exists(m_presetsBankFilename))
            {
                return;
            }

            try
            {
                // Loads the presets bank XML file
                XmlDocument doc = new XmlDocument();
                doc.Load(m_presetsBankFilename);

                XmlElement root = doc.DocumentElement;
                XmlNode presetsBankNode = root.SelectSingleNode("/PracticeSharp/PresetsBank");
                string activePresetId = presetsBankNode.Attributes["ActivePreset"].Value;
                XmlNodeList presetNodes = presetsBankNode.SelectNodes("Preset");
                foreach (XmlNode presetNode in presetNodes)
                {
                    string presetId = presetNode.Attributes["Id"].Value;
                    // Load XML values into PresetData object
                    PresetData presetData = m_presetControls[presetId].PresetData;
                    presetData.Tempo = Convert.ToSingle(presetNode.Attributes["Tempo"].Value);
                    presetData.Pitch = Convert.ToSingle(presetNode.Attributes["Pitch"].Value);
                    presetData.Volume = Convert.ToSingle(presetNode.Attributes["Volume"].Value);
                    presetData.CurrentPlayTime = TimeSpan.Parse(presetNode.Attributes["PlayTime"].Value);
                    presetData.StartMarker = TimeSpan.Parse(presetNode.Attributes["LoopStartMarker"].Value);
                    presetData.EndMarker = TimeSpan.Parse(presetNode.Attributes["LoopEndMarker"].Value);
                    presetData.Loop = Convert.ToBoolean(presetNode.Attributes["IsLoop"].Value);
                    presetData.Cue = TimeSpan.Parse(presetNode.Attributes["Cue"].Value);
                    presetData.Description = Convert.ToString(presetNode.Attributes["Description"].Value);

                    PresetControl presetControl = m_presetControls[presetId];
                    presetControl.PresetDescription = presetData.Description;
                }

                m_currentPreset = m_presetControls[activePresetId];
                m_currentPreset.State = PresetControl.PresetStates.Selected;              
            }
            catch ( Exception )
            {
                MessageBox.Show(this, "Failed loading Presets Bank for file: " + m_currentFilename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    
            }

        }

        /// <summary>
        /// Writes a full Preset Bank XML into a file
        /// </summary>
        private void WritePresetsBank()
        {
            // Create an XML Document
            XmlDocument doc = new XmlDocument();
            XmlElement elRoot = (XmlElement)doc.AppendChild(doc.CreateElement("PracticeSharp"));
            elRoot.SetAttribute("Version", m_appVersion.ToString());
            XmlElement elPresets = (XmlElement)elRoot.AppendChild(doc.CreateElement("PresetsBank"));
            elPresets.SetAttribute("Filename", Path.GetFileName(m_currentFilename));

            elPresets.SetAttribute("ActivePreset", m_currentPreset.Id);

            foreach ( PresetControl presetControl in m_presetControls.Values )
            {
                PresetData presetData = presetControl.PresetData;

                XmlElement elPreset = (XmlElement)elPresets.AppendChild(doc.CreateElement("Preset"));
                elPreset.SetAttribute("Id", presetControl.Id);
                elPreset.SetAttribute("Tempo", presetData.Tempo.ToString());
                elPreset.SetAttribute("Pitch", presetData.Pitch.ToString());
                elPreset.SetAttribute("Volume", presetData.Volume.ToString());
                elPreset.SetAttribute("PlayTime", presetData.CurrentPlayTime.ToString());
                elPreset.SetAttribute("LoopStartMarker", presetData.StartMarker.ToString());
                elPreset.SetAttribute("LoopEndMarker", presetData.EndMarker.ToString());
                elPreset.SetAttribute("IsLoop", presetData.Loop.ToString());
                elPreset.SetAttribute("Cue", presetData.Cue.ToString());
                elPreset.SetAttribute("IsLoop", presetData.Loop.ToString());
                elPreset.SetAttribute("Description", presetData.Description);
            }
            
            // Write to XML file
            using (StreamWriter writer = new StreamWriter(m_presetsBankFilename, false, Encoding.UTF8))
            {
                writer.Write(doc.OuterXml);
            }

            // Console.WriteLine(doc.OuterXml);
        }


        #endregion 

        #region GUI Event Handlers
        /// <summary>
        /// Play/Pause button click event handler - Plays or Pauses the current play back of the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playPauseButton_Click(object sender, EventArgs e)
        {
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                playPauseButton.Image = Resources.Play_Hot;
                m_practiceSharpLogic.Pause();
            }
            else if ( (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Pausing ) ||
                      (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Initialized ) )
            {
                playPauseButton.Image = Resources.Pause_Hot;

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
        private void playPauseButton_MouseEnter(object sender, EventArgs e)
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
        private void playPauseButton_MouseLeave(object sender, EventArgs e)
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
        private void positionMarkersPanel_Paint(object sender, PaintEventArgs e)
        {
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Initialized)
            {
                return;
            }

            TimeSpan startMarker = m_practiceSharpLogic.StartMarker;
            TimeSpan endMarker = m_practiceSharpLogic.EndMarker;
            TimeSpan filePlayDuration = m_practiceSharpLogic.FilePlayDuration;

            int startMarkerX = Convert.ToInt32(startMarker.TotalSeconds / filePlayDuration.TotalSeconds * positionMarkersPanel.Width);
            int endMarkerX = Convert.ToInt32(endMarker.TotalSeconds / filePlayDuration.TotalSeconds * positionMarkersPanel.Width);

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
        private void writePresetButton_Click(object sender, EventArgs e)
        {
            if (m_currentPreset.State != PresetControl.PresetStates.WaitForSave)
            {
                // Enter preset 'Write Mode'
                foreach (PresetControl presetControl in m_presetControls.Values)
                {
                    presetControl.State = PresetControl.PresetStates.WaitForSave;
                }
            }
            else
            {
                // Cancel write mode
                m_currentPreset.State = PresetControl.PresetStates.Selected;
            }
        }

        /// <summary>
        /// PresetSelected Event handler - Handles a preset select request (user clicking on preset requesting to activate it)
        /// As a result the preset values are applied
        /// Note: The preset can be selected even if it is active - in this case the values will revert to the last saved values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presetControl_PresetSelected(object sender, EventArgs e)
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

        private void ApplyPresetValueUIControls(PresetData presetData)
        {
            // Apply preset values
            tempoTrackBar.Value = Convert.ToInt32(presetData.Tempo * 100.0f);
            pitchTrackBar.Value = Convert.ToInt32(presetData.Pitch * 96.0f);
            volumeTrackBar.Value = Convert.ToInt32(presetData.Volume * 100.0f);
            if (m_practiceSharpLogic.FilePlayDuration == TimeSpan.Zero)
            {
                playTimeTrackBar.Value = playTimeTrackBar.Minimum;
            }
            else
            {
                playTimeTrackBar.Value = Convert.ToInt32(100.0f * presetData.CurrentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
            }
            startLoopMinuteUpDown.Value = Convert.ToInt32(presetData.StartMarker.Minutes);
            startLoopSecondUpDown.Value = Convert.ToInt32(presetData.StartMarker.Seconds);
            startLoopMilliUpDown.Value = Convert.ToInt32(presetData.StartMarker.Milliseconds);
            endLoopMinuteUpDown.Value = Convert.ToInt32(presetData.EndMarker.Minutes);
            endLoopSecondUpDown.Value = Convert.ToInt32(presetData.EndMarker.Seconds);
            endLoopMilliUpDown.Value = Convert.ToInt32(presetData.EndMarker.Milliseconds);
            int cueItemIndex = cueComboBox.FindString(Convert.ToInt32(presetData.Cue.TotalSeconds).ToString());
            cueComboBox.SelectedIndex = cueItemIndex;
            loopCheckBox.Checked = presetData.Loop;
        }

        /// <summary>
        /// PresetSaveSelected Event handle - Handles a preset save request (user clicks on preset when Write Mode is on, i.e. Red Leds are turned on)
        /// As a result the preset is saved into the preset bank file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presetControl_PresetSaveSelected(object sender, EventArgs e)
        {
            m_currentPreset = sender as PresetControl;

            foreach (PresetControl presetControl in m_presetControls.Values)
            {
                if (presetControl != m_currentPreset)
                {
                    presetControl.State = PresetControl.PresetStates.Off;
                }
                else
                {
                    // Update the preset data for the selected preset
                    presetControl.PresetData.Tempo = m_practiceSharpLogic.Tempo;
                    presetControl.PresetData.Pitch = m_practiceSharpLogic.Pitch;
                    presetControl.PresetData.Volume = m_practiceSharpLogic.Volume;
                    presetControl.PresetData.CurrentPlayTime = m_practiceSharpLogic.CurrentPlayTime;
                    presetControl.PresetData.StartMarker = m_practiceSharpLogic.StartMarker;
                    presetControl.PresetData.EndMarker = m_practiceSharpLogic.EndMarker;
                    presetControl.PresetData.Cue = m_practiceSharpLogic.Cue;
                    presetControl.PresetData.Loop = m_practiceSharpLogic.Loop;
                    presetControl.PresetData.Description = presetControl.PresetDescription;
                }
            }

            // (Re-)Write preset bank file
            WritePresetsBank();
        }


        private void volumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            float newVolume = volumeTrackBar.Value / 100.0f;
            m_practiceSharpLogic.Volume = newVolume;

            volumeValueLabel.Text = ( newVolume * 100 ).ToString();

        }

   
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resetBankButton_Click(object sender, EventArgs e)
        {

        }

        private void resetBankButton_MouseDown(object sender, MouseEventArgs e)
        {
            resetBankTimer.Start();
        }

        private void resetBankTimer_Tick(object sender, EventArgs e)
        {
            resetBankTimer.Stop();

            // Reset bank values
            m_currentPreset.Reset();
            ApplyPresetValueUIControls( m_currentPreset.PresetData );

            WritePresetsBank();
        }

        private void resetBankButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Cancel reset
            resetBankTimer.Stop();
        }

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
                if (droppedFile.ToLower().EndsWith(".mp3") || droppedFile.ToLower().EndsWith(".wav"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    Console.WriteLine("DragAndDrop are only allowed for music files (MP3, WAV)");
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
                string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);
                string droppedFile = files[0];
                Console.WriteLine("[DragDrop] DragAndDrop Files: " + droppedFile );
                OpenFile(droppedFile, true);
            }
        }

        #endregion

        /// <summary>
        /// Click event handler for openFileButton - Invokes the open file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            // Show the open file dialog
            if (DialogResult.OK == openFileDialog.ShowDialog(this))
            {
                // Get directory path and store it as a user settting
                FileInfo fi = new FileInfo( openFileDialog.FileName );
                Properties.Settings.Default.LastAudioFolder = fi.Directory.FullName;
                Properties.Settings.Default.Save();

                // Open the file for playing
                OpenFile(openFileDialog.FileName, true);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            m_practiceSharpLogic.Stop();
            playTimeUpdateTimer.Enabled = false;
        }

        /// <summary>
        /// Event handler for ValueChanged of the speedTrackBar -
        ///   Changes the underlying tempo of PracticeSharpLogic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Convert to Percent 
            float newTempo = tempoTrackBar.Value / 100.0f;
            // Assign new Tempo
            m_practiceSharpLogic.Tempo = newTempo;
            
            // Update speed value label
            speedValueLabel.Text = newTempo.ToString();
        }


        private void pitchTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Convert to Percent 
            float newPitch = pitchTrackBar.Value / 96.0f;
            // Assign new Pitch
            m_practiceSharpLogic.Pitch = newPitch;

            // Update Pitch value label
            pitchValueLabel.Text = newPitch.ToString( "0.00" );
    
        } 

    

        private void speedLabel_Click(object sender, EventArgs e)
        {
            tempoTrackBar.Value = Convert.ToInt32( PresetData.DefaultTempo * 100 );
        }

        private void volumeLabel_Click(object sender, EventArgs e)
        {
            volumeTrackBar.Value = Convert.ToInt32( PresetData.DefaultVolume * 100 );
        }

        private void pitchLabel_Click(object sender, EventArgs e)
        {
              pitchTrackBar.Value = Convert.ToInt32( PresetData.DefaultPitch * 100 );
        }

        private void positionLabel_Click(object sender, EventArgs e)
        {
            // Reset current play time so it starts from the begining
            if (m_practiceSharpLogic.Loop)
            {
                // In case of a loop, move the current play time to the start marker
                m_practiceSharpLogic.CurrentPlayTime = m_practiceSharpLogic.StartMarker;
            }
            else
            {
                m_practiceSharpLogic.CurrentPlayTime = TimeSpan.Zero;
            }
        }
    
        private void loopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_practiceSharpLogic.Loop = loopCheckBox.Checked;
        }

        private void playTimeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            // Don't allow re-entry of UI events when the track bar is being programmtically changed
            if (m_ignorePlayTimeUIEvents)
                return;

            float playPosSeconds = ( float ) ( playTimeTrackBar.Value / 100.0f * m_practiceSharpLogic.FilePlayDuration.TotalSeconds );
            TimeSpan newPlayTime = new TimeSpan( 0, 0, 0, ( int ) playPosSeconds, 
                    ( int ) ( 100 * ( playPosSeconds - Math.Truncate( playPosSeconds ) ) )) ;

            m_practiceSharpLogic.CurrentPlayTime = newPlayTime;
            if (m_practiceSharpLogic.Status != PracticeSharpLogic.Statuses.Playing)
            {
                UpdateCurrentUpDownControls(newPlayTime);
            }
        }

        private void playPositionTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            m_isUpdatePlayTimeNeeded = false;
            m_playTimeTrackBarIsChanging = true;

            Application.DoEvents();
            // Let previous pending updates to position track bar to complete - otherwise the track bar 'jumps'
            Thread.Sleep(100);
            Application.DoEvents();

            UpdateNewPlayTimeByMousePos(e);
        }

        private void playTimeTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_playTimeTrackBarIsChanging)
            {
                UpdateNewPlayTimeByMousePos(e);
            }
        }

        private void playPositionTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            m_playTimeTrackBarIsChanging = false;
        }

        private void UpdateNewPlayTimeByMousePos(MouseEventArgs e)
        {
            int newValue = Convert.ToInt32(playTimeTrackBar.Maximum * ((float)e.X / playTimeTrackBar.Width));
            if (newValue > playTimeTrackBar.Maximum)
                newValue = playTimeTrackBar.Maximum;
            if (newValue < playTimeTrackBar.Minimum)
                newValue = playTimeTrackBar.Minimum;

            TimeSpan newPlayTime = new TimeSpan(0, 0, Convert.ToInt32( ( newValue / 100.0f ) * m_practiceSharpLogic.FilePlayDuration.TotalSeconds) );

            m_practiceSharpLogic.CurrentPlayTime = newPlayTime;
           
            if (m_practiceSharpLogic.Status == PracticeSharpLogic.Statuses.Playing)
            {
                m_ignorePlayTimeUIEvents = true;
                try
                {
                    // Time in which playtime trackbar updates coming from PracticeSharpLogic are not allowed (to eliminate 'Jumps' due to old locations)
                    m_playTimeTrackBarMaskOutTime = DateTime.Now.AddSeconds(1);

                    playTimeTrackBar.Value = newValue;
                }
                finally
                {
                    m_ignorePlayTimeUIEvents = false;
                }
            }
            else
            {
                playTimeTrackBar.Value = newValue;
            }
        }

        private void playTimeUpdateTimer_Tick(object sender, EventArgs e)
        {
            lock (this)
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
                UpdateCurrentUpDownControls(m_practiceSharpLogic.CurrentPlayTime);

                if (!m_playTimeTrackBarIsChanging && DateTime.Now > m_playTimeTrackBarMaskOutTime)
                {
                    int currentPlayTimeValue = Convert.ToInt32(100.0f * m_practiceSharpLogic.CurrentPlayTime.TotalSeconds / m_practiceSharpLogic.FilePlayDuration.TotalSeconds);
                    playTimeTrackBar.Value = currentPlayTimeValue;
                }

                positionMarkersPanel.Refresh();
            }
            finally
            {
                m_ignorePlayTimeUIEvents = false;
            }
        }

        private void UpdateCurrentUpDownControls( TimeSpan playTime )
        {
            // Update current play time controls
            currentMinuteUpDown.Value = playTime.Minutes;
            currentSecondUpDown.Value = playTime.Seconds;
            currentMilliUpDown.Value = playTime.Milliseconds;
        }

        private void startLoopNowButton_Click(object sender, EventArgs e)
        {
            startLoopMinuteUpDown.Value = currentMinuteUpDown.Value;
            startLoopSecondUpDown.Value = currentSecondUpDown.Value;
            startLoopMilliUpDown.Value = currentMilliUpDown.Value;
        }

        private void endLoopNowButton_Click(object sender, EventArgs e)
        {
            endLoopMinuteUpDown.Value = currentMinuteUpDown.Value;
            endLoopSecondUpDown.Value = currentSecondUpDown.Value;
            endLoopMilliUpDown.Value = currentMilliUpDown.Value;
        }

        private void currentUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Don't allow re-entry of UI events when the track bar is being programmatically changed
            if (m_ignorePlayTimeUIEvents)
                return;

            UpdateCoreCurrentPlayTime();
        }

        private void startLoopUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCoreStartMarker();
        }

        private void endLoopUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateCoreEndMarker();
        }

        private void cueComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_practiceSharpLogic.Cue = new TimeSpan(0, 0, Convert.ToInt32(cueComboBox.Text));
        }
        
        #endregion

        #region PracticeSharpLogic Event Handlers

        private void practiceSharpLogic_StatusChanged(object sender, PracticeSharpLogic.Statuses newStatus)
        {
            this.BeginInvoke( new MethodInvoker( delegate()
            {
                playStatusToolStripLabel.Text = newStatus.ToString();

                if ( (newStatus == PracticeSharpLogic.Statuses.Stopped)
                   || (newStatus == PracticeSharpLogic.Statuses.Error) )
                {
                    playPauseButton.Image = Resources.Play_Normal;
                    playTimeUpdateTimer.Enabled = false;
                }
            } )
            );
        }

        private void practiceSharpLogic_PlayTimeChanged(object sender, EventArgs e)
        {
            this.Invoke(
                new MethodInvoker(delegate()
                {
                    lock (this)
                    {
                        m_isUpdatePlayTimeNeeded = true;
                    }
                }));
        }

        private void practiceSharpLogic_CueWaitPulsed(object sender, EventArgs e)
        {
            this.BeginInvoke(

                new MethodInvoker(delegate()
                {
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

        private DateTime m_playTimeTrackBarMaskOutTime = DateTime.Now;

        private string m_currentFilename;
        private string m_presetsBankFilename;
        private string m_appDataFolder;
        private Version m_appVersion;

        #endregion

        #region Constants

        const int MarkerWidth = 5; 
        const int MarkerHeight = 10;


        #endregion
    }
}
