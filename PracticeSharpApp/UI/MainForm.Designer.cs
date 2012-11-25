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

namespace BigMansStuff.PracticeSharp.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                if (m_versionUpdater != null)
                {
                    m_versionUpdater.Dispose();
                    m_versionUpdater = null;
                }

                if (m_practiceSharpLogic != null)
                {
                    m_practiceSharpLogic.Dispose();
                    m_practiceSharpLogic = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            BigMansStuff.PracticeSharp.Core.PresetData presetData1 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData2 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData3 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData4 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData5 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData6 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData7 = new BigMansStuff.PracticeSharp.Core.PresetData();
            BigMansStuff.PracticeSharp.Core.PresetData presetData8 = new BigMansStuff.PracticeSharp.Core.PresetData();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tempoTrackBar = new System.Windows.Forms.TrackBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.volumeTrackBar = new System.Windows.Forms.TrackBar();
            this.speed1XLabel = new System.Windows.Forms.Label();
            this.playTimeTrackBar = new System.Windows.Forms.TrackBar();
            this.speed01XLabel = new System.Windows.Forms.Label();
            this.speed2XLabel = new System.Windows.Forms.Label();
            this.startLoopMinuteUpDown = new System.Windows.Forms.NumericUpDown();
            this.startLoopSecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.startLoopMilliUpDown = new System.Windows.Forms.NumericUpDown();
            this.endLoopMinuteUpDown = new System.Windows.Forms.NumericUpDown();
            this.endLoopSecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.endLoopMilliUpDown = new System.Windows.Forms.NumericUpDown();
            this.startLoopLabel = new System.Windows.Forms.Label();
            this.endLoopLabel = new System.Windows.Forms.Label();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.currentLabel = new System.Windows.Forms.Label();
            this.currentMilliUpDown = new System.Windows.Forms.NumericUpDown();
            this.currentSecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.currentMinuteUpDown = new System.Windows.Forms.NumericUpDown();
            this.volume100Label = new System.Windows.Forms.Label();
            this.volume0Label = new System.Windows.Forms.Label();
            this.play0Label = new System.Windows.Forms.Label();
            this.playDurationLabel = new System.Windows.Forms.Label();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.appStatusDescLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.appStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.filenameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.presetPanel = new System.Windows.Forms.Panel();
            this.resetBankTimer = new System.Windows.Forms.Timer(this.components);
            this.playTimeUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.cueComboBox = new System.Windows.Forms.ComboBox();
            this.cueLabel = new System.Windows.Forms.Label();
            this.volumeValueLabel = new System.Windows.Forms.Label();
            this.speedValueLabel = new System.Windows.Forms.Label();
            this.cuePictureBox = new System.Windows.Forms.PictureBox();
            this.loopPanel = new System.Windows.Forms.Panel();
            this.cueSecondsLabel = new System.Windows.Forms.Label();
            this.vol50Label = new System.Windows.Forms.Label();
            this.vol75Label = new System.Windows.Forms.Label();
            this.vol25Label = new System.Windows.Forms.Label();
            this.speed15XLabel = new System.Windows.Forms.Label();
            this.speed05XLabel = new System.Windows.Forms.Label();
            this.play2QDurationLabel = new System.Windows.Forms.Label();
            this.play3QDurationLabel = new System.Windows.Forms.Label();
            this.play1QDurationLabel = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.pitchValueLabel = new System.Windows.Forms.Label();
            this.pitch1Label = new System.Windows.Forms.Label();
            this.pitchM1Label = new System.Windows.Forms.Label();
            this.pitch0Label = new System.Windows.Forms.Label();
            this.pitchTrackBar = new System.Windows.Forms.TrackBar();
            this.trackBarPanel = new System.Windows.Forms.Panel();
            this.removeVocalsCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeStretchProfileComboBox = new System.Windows.Forms.ComboBox();
            this.timeStretchProfileLabel = new System.Windows.Forms.Label();
            this.hiEqValueLabel = new System.Windows.Forms.Label();
            this.medEqValueLabel = new System.Windows.Forms.Label();
            this.loEqValueLabel = new System.Windows.Forms.Label();
            this.eqM100Label = new System.Windows.Forms.Label();
            this.eq100Label = new System.Windows.Forms.Label();
            this.eq0Label = new System.Windows.Forms.Label();
            this.hiEqTrackBar = new System.Windows.Forms.TrackBar();
            this.medEqTrackBar = new System.Windows.Forms.TrackBar();
            this.loEqTrackBar = new System.Windows.Forms.TrackBar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recent8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardShortcutsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTechLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.filenameDescLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.equalizerHoverLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.hiEqHoverLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.medEqHoverLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.loEqHoverLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.positionMarkersPanel = new BigMansStuff.PracticeSharp.UI.FlickerFreePanel();
            this.pitchLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.speedLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.volumeLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.positionLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.openFileButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.playPauseButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.endLoopNowButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.startLoopNowButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.resetPresetButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.writePresetButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.presetControl7 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl5 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl6 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl8 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl3 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl1 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl2 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl4 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            ((System.ComponentModel.ISupportInitialize)(this.tempoTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playTimeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopMinuteUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopSecondUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopMilliUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopMinuteUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopSecondUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopMilliUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentMilliUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentSecondUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentMinuteUpDown)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.presetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cuePictureBox)).BeginInit();
            this.loopPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).BeginInit();
            this.trackBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hiEqTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medEqTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loEqTrackBar)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tempoTrackBar
            // 
            this.tempoTrackBar.LargeChange = 0;
            this.tempoTrackBar.Location = new System.Drawing.Point(51, 54);
            this.tempoTrackBar.Maximum = 200;
            this.tempoTrackBar.Minimum = 10;
            this.tempoTrackBar.Name = "tempoTrackBar";
            this.tempoTrackBar.Size = new System.Drawing.Size(287, 45);
            this.tempoTrackBar.SmallChange = 5;
            this.tempoTrackBar.TabIndex = 0;
            this.tempoTrackBar.TickFrequency = 10;
            this.tempoTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tempoTrackBar.Value = 100;
            this.tempoTrackBar.ValueChanged += new System.EventHandler(this.tempoTrackBar_ValueChanged);
            this.tempoTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tempoTrackBar_MouseDown);
            this.tempoTrackBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tempoTrackBar_MouseMove);
            this.tempoTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tempoTrackBar_MouseUp);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "All Music Files|*.mp3;*.wav;*.ogg;*.flac;*.wma;*.aiff|MP3 files|*.mp3|WAV files|*" +
    ".wav|Ogg Vorbis files|*.ogg|FLAC files|*.flac|WMA files|*.wma|AIFF files|*.aiff";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(381, 39);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volumeTrackBar.Size = new System.Drawing.Size(45, 151);
            this.volumeTrackBar.TabIndex = 2;
            this.volumeTrackBar.TickFrequency = 10;
            this.volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volumeTrackBar.Value = 25;
            this.volumeTrackBar.ValueChanged += new System.EventHandler(this.volumeTrackBar_ValueChanged);
            this.volumeTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.volumeTrackBar_MouseDown);
            // 
            // speed1XLabel
            // 
            this.speed1XLabel.AutoSize = true;
            this.speed1XLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed1XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed1XLabel.Location = new System.Drawing.Point(178, 38);
            this.speed1XLabel.Name = "speed1XLabel";
            this.speed1XLabel.Size = new System.Drawing.Size(15, 12);
            this.speed1XLabel.TabIndex = 4;
            this.speed1XLabel.Text = "x1";
            this.speed1XLabel.Click += new System.EventHandler(this.speedLabel_Click);
            // 
            // playTimeTrackBar
            // 
            this.playTimeTrackBar.LargeChange = 0;
            this.playTimeTrackBar.Location = new System.Drawing.Point(61, 234);
            this.playTimeTrackBar.Maximum = 100;
            this.playTimeTrackBar.Name = "playTimeTrackBar";
            this.playTimeTrackBar.Size = new System.Drawing.Size(520, 45);
            this.playTimeTrackBar.SmallChange = 5;
            this.playTimeTrackBar.TabIndex = 6;
            this.playTimeTrackBar.TickFrequency = 5;
            this.playTimeTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.playTimeTrackBar.ValueChanged += new System.EventHandler(this.playTimeTrackBar_ValueChanged);
            this.playTimeTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playPositionTrackBar_MouseDown);
            this.playTimeTrackBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.playTimeTrackBar_MouseMove);
            this.playTimeTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.playTimeTrackBar_MouseUp);
            // 
            // speed01XLabel
            // 
            this.speed01XLabel.AutoSize = true;
            this.speed01XLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed01XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed01XLabel.Location = new System.Drawing.Point(51, 38);
            this.speed01XLabel.Name = "speed01XLabel";
            this.speed01XLabel.Size = new System.Drawing.Size(23, 12);
            this.speed01XLabel.TabIndex = 6;
            this.speed01XLabel.Text = "x0.1";
            // 
            // speed2XLabel
            // 
            this.speed2XLabel.AutoSize = true;
            this.speed2XLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed2XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed2XLabel.Location = new System.Drawing.Point(315, 38);
            this.speed2XLabel.Name = "speed2XLabel";
            this.speed2XLabel.Size = new System.Drawing.Size(15, 12);
            this.speed2XLabel.TabIndex = 7;
            this.speed2XLabel.Text = "x2";
            // 
            // startLoopMinuteUpDown
            // 
            this.startLoopMinuteUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopMinuteUpDown.Location = new System.Drawing.Point(132, 3);
            this.startLoopMinuteUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.startLoopMinuteUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.startLoopMinuteUpDown.Name = "startLoopMinuteUpDown";
            this.startLoopMinuteUpDown.Size = new System.Drawing.Size(38, 20);
            this.startLoopMinuteUpDown.TabIndex = 0;
            this.startLoopMinuteUpDown.ValueChanged += new System.EventHandler(this.startLoopMinuteUpDown_ValueChanged);
            // 
            // startLoopSecondUpDown
            // 
            this.startLoopSecondUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopSecondUpDown.Location = new System.Drawing.Point(176, 3);
            this.startLoopSecondUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.startLoopSecondUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.startLoopSecondUpDown.Name = "startLoopSecondUpDown";
            this.startLoopSecondUpDown.Size = new System.Drawing.Size(38, 20);
            this.startLoopSecondUpDown.TabIndex = 2;
            this.startLoopSecondUpDown.ValueChanged += new System.EventHandler(this.startLoopSecondUpDown_ValueChanged);
            // 
            // startLoopMilliUpDown
            // 
            this.startLoopMilliUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopMilliUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.startLoopMilliUpDown.Location = new System.Drawing.Point(220, 3);
            this.startLoopMilliUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.startLoopMilliUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.startLoopMilliUpDown.Name = "startLoopMilliUpDown";
            this.startLoopMilliUpDown.Size = new System.Drawing.Size(51, 20);
            this.startLoopMilliUpDown.TabIndex = 4;
            this.startLoopMilliUpDown.ValueChanged += new System.EventHandler(this.startLoopMilliUpDown_ValueChanged);
            // 
            // endLoopMinuteUpDown
            // 
            this.endLoopMinuteUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopMinuteUpDown.Location = new System.Drawing.Point(132, 28);
            this.endLoopMinuteUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.endLoopMinuteUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.endLoopMinuteUpDown.Name = "endLoopMinuteUpDown";
            this.endLoopMinuteUpDown.Size = new System.Drawing.Size(38, 20);
            this.endLoopMinuteUpDown.TabIndex = 1;
            this.endLoopMinuteUpDown.ValueChanged += new System.EventHandler(this.endLoopMinuteUpDown_ValueChanged);
            // 
            // endLoopSecondUpDown
            // 
            this.endLoopSecondUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopSecondUpDown.Location = new System.Drawing.Point(176, 28);
            this.endLoopSecondUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.endLoopSecondUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.endLoopSecondUpDown.Name = "endLoopSecondUpDown";
            this.endLoopSecondUpDown.Size = new System.Drawing.Size(38, 20);
            this.endLoopSecondUpDown.TabIndex = 3;
            this.endLoopSecondUpDown.ValueChanged += new System.EventHandler(this.endLoopSecondUpDown_ValueChanged);
            // 
            // endLoopMilliUpDown
            // 
            this.endLoopMilliUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopMilliUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.endLoopMilliUpDown.Location = new System.Drawing.Point(220, 28);
            this.endLoopMilliUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.endLoopMilliUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.endLoopMilliUpDown.Name = "endLoopMilliUpDown";
            this.endLoopMilliUpDown.Size = new System.Drawing.Size(51, 20);
            this.endLoopMilliUpDown.TabIndex = 5;
            this.endLoopMilliUpDown.ValueChanged += new System.EventHandler(this.endLoopMilliUpDown_ValueChanged);
            // 
            // startLoopLabel
            // 
            this.startLoopLabel.AutoSize = true;
            this.startLoopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLoopLabel.Location = new System.Drawing.Point(64, 3);
            this.startLoopLabel.Name = "startLoopLabel";
            this.startLoopLabel.Size = new System.Drawing.Size(66, 15);
            this.startLoopLabel.TabIndex = 16;
            this.startLoopLabel.Text = "Start Loop:";
            // 
            // endLoopLabel
            // 
            this.endLoopLabel.AutoSize = true;
            this.endLoopLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endLoopLabel.Location = new System.Drawing.Point(64, 28);
            this.endLoopLabel.Name = "endLoopLabel";
            this.endLoopLabel.Size = new System.Drawing.Size(63, 15);
            this.endLoopLabel.TabIndex = 17;
            this.endLoopLabel.Text = "End Loop:";
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.AutoSize = true;
            this.loopCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loopCheckBox.Location = new System.Drawing.Point(2, 27);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(54, 19);
            this.loopCheckBox.TabIndex = 18;
            this.loopCheckBox.Text = "Loop";
            this.loopCheckBox.UseVisualStyleBackColor = true;
            this.loopCheckBox.CheckedChanged += new System.EventHandler(this.loopCheckBox_CheckedChanged);
            // 
            // currentLabel
            // 
            this.currentLabel.AutoSize = true;
            this.currentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLabel.Location = new System.Drawing.Point(196, 3);
            this.currentLabel.Name = "currentLabel";
            this.currentLabel.Size = new System.Drawing.Size(66, 20);
            this.currentLabel.TabIndex = 3;
            this.currentLabel.Text = "Current:";
            // 
            // currentMilliUpDown
            // 
            this.currentMilliUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentMilliUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.currentMilliUpDown.Location = new System.Drawing.Point(353, 1);
            this.currentMilliUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.currentMilliUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.currentMilliUpDown.Name = "currentMilliUpDown";
            this.currentMilliUpDown.Size = new System.Drawing.Size(51, 26);
            this.currentMilliUpDown.TabIndex = 6;
            this.currentMilliUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // currentSecondUpDown
            // 
            this.currentSecondUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSecondUpDown.Location = new System.Drawing.Point(309, 1);
            this.currentSecondUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.currentSecondUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.currentSecondUpDown.Name = "currentSecondUpDown";
            this.currentSecondUpDown.Size = new System.Drawing.Size(38, 26);
            this.currentSecondUpDown.TabIndex = 5;
            this.currentSecondUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // currentMinuteUpDown
            // 
            this.currentMinuteUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentMinuteUpDown.Location = new System.Drawing.Point(265, 1);
            this.currentMinuteUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.currentMinuteUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.currentMinuteUpDown.Name = "currentMinuteUpDown";
            this.currentMinuteUpDown.Size = new System.Drawing.Size(38, 26);
            this.currentMinuteUpDown.TabIndex = 4;
            this.currentMinuteUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // volume100Label
            // 
            this.volume100Label.AutoSize = true;
            this.volume100Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volume100Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.volume100Label.Location = new System.Drawing.Point(353, 39);
            this.volume100Label.Name = "volume100Label";
            this.volume100Label.Size = new System.Drawing.Size(28, 12);
            this.volume100Label.TabIndex = 28;
            this.volume100Label.Text = "100%";
            // 
            // volume0Label
            // 
            this.volume0Label.AutoSize = true;
            this.volume0Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volume0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.volume0Label.Location = new System.Drawing.Point(363, 171);
            this.volume0Label.Name = "volume0Label";
            this.volume0Label.Size = new System.Drawing.Size(18, 12);
            this.volume0Label.TabIndex = 29;
            this.volume0Label.Text = "0%";
            // 
            // play0Label
            // 
            this.play0Label.AutoSize = true;
            this.play0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play0Label.Location = new System.Drawing.Point(64, 218);
            this.play0Label.Name = "play0Label";
            this.play0Label.Size = new System.Drawing.Size(34, 13);
            this.play0Label.TabIndex = 31;
            this.play0Label.Text = "00:00";
            // 
            // playDurationLabel
            // 
            this.playDurationLabel.AutoSize = true;
            this.playDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.playDurationLabel.Location = new System.Drawing.Point(549, 218);
            this.playDurationLabel.Name = "playDurationLabel";
            this.playDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.playDurationLabel.TabIndex = 36;
            this.playDurationLabel.Text = "00:00";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // tutorialToolStripMenuItem
            // 
            this.tutorialToolStripMenuItem.Name = "tutorialToolStripMenuItem";
            this.tutorialToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.tutorialToolStripMenuItem.Text = "Tutorial";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Exit";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appStatusDescLabel,
            this.appStatusLabel,
            this.filenameDescLabel,
            this.filenameLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 510);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(603, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // appStatusDescLabel
            // 
            this.appStatusDescLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appStatusDescLabel.Name = "appStatusDescLabel";
            this.appStatusDescLabel.Size = new System.Drawing.Size(45, 17);
            this.appStatusDescLabel.Text = "Status:";
            // 
            // appStatusLabel
            // 
            this.appStatusLabel.Name = "appStatusLabel";
            this.appStatusLabel.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.appStatusLabel.Size = new System.Drawing.Size(41, 17);
            this.appStatusLabel.Text = "Idle";
            this.appStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filenameLabel
            // 
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.filenameLabel.Size = new System.Drawing.Size(34, 17);
            this.filenameLabel.Text = "N/A";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel2.Text = "File:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // presetPanel
            // 
            this.presetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.presetPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.presetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.presetPanel.Controls.Add(this.resetPresetButton);
            this.presetPanel.Controls.Add(this.writePresetButton);
            this.presetPanel.Controls.Add(this.presetControl7);
            this.presetPanel.Controls.Add(this.presetControl5);
            this.presetPanel.Controls.Add(this.presetControl6);
            this.presetPanel.Controls.Add(this.presetControl8);
            this.presetPanel.Controls.Add(this.presetControl3);
            this.presetPanel.Controls.Add(this.presetControl1);
            this.presetPanel.Controls.Add(this.presetControl2);
            this.presetPanel.Controls.Add(this.presetControl4);
            this.presetPanel.Location = new System.Drawing.Point(14, 382);
            this.presetPanel.Name = "presetPanel";
            this.presetPanel.Size = new System.Drawing.Size(576, 125);
            this.presetPanel.TabIndex = 3;
            // 
            // resetBankTimer
            // 
            this.resetBankTimer.Interval = 1000;
            this.resetBankTimer.Tick += new System.EventHandler(this.resetBankTimer_Tick);
            // 
            // playTimeUpdateTimer
            // 
            this.playTimeUpdateTimer.Interval = 50;
            this.playTimeUpdateTimer.Tick += new System.EventHandler(this.playTimeUpdateTimer_Tick);
            // 
            // cueComboBox
            // 
            this.cueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cueComboBox.FormattingEnabled = true;
            this.cueComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "5",
            "10"});
            this.cueComboBox.Location = new System.Drawing.Point(366, 3);
            this.cueComboBox.Name = "cueComboBox";
            this.cueComboBox.Size = new System.Drawing.Size(42, 21);
            this.cueComboBox.TabIndex = 8;
            this.cueComboBox.SelectedValueChanged += new System.EventHandler(this.cueComboBox_SelectedValueChanged);
            // 
            // cueLabel
            // 
            this.cueLabel.AutoSize = true;
            this.cueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cueLabel.Location = new System.Drawing.Point(341, 6);
            this.cueLabel.Name = "cueLabel";
            this.cueLabel.Size = new System.Drawing.Size(26, 13);
            this.cueLabel.TabIndex = 51;
            this.cueLabel.Text = "Cue";
            // 
            // volumeValueLabel
            // 
            this.volumeValueLabel.AutoSize = true;
            this.volumeValueLabel.Location = new System.Drawing.Point(391, 190);
            this.volumeValueLabel.Name = "volumeValueLabel";
            this.volumeValueLabel.Size = new System.Drawing.Size(10, 13);
            this.volumeValueLabel.TabIndex = 6;
            this.volumeValueLabel.Text = "-";
            this.volumeValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedValueLabel
            // 
            this.speedValueLabel.AutoSize = true;
            this.speedValueLabel.Location = new System.Drawing.Point(10, 38);
            this.speedValueLabel.Name = "speedValueLabel";
            this.speedValueLabel.Size = new System.Drawing.Size(10, 13);
            this.speedValueLabel.TabIndex = 0;
            this.speedValueLabel.Text = "-";
            // 
            // cuePictureBox
            // 
            this.cuePictureBox.Image = global::BigMansStuff.PracticeSharp.Resources.blue_off_16;
            this.cuePictureBox.Location = new System.Drawing.Point(20, 6);
            this.cuePictureBox.Name = "cuePictureBox";
            this.cuePictureBox.Size = new System.Drawing.Size(18, 18);
            this.cuePictureBox.TabIndex = 55;
            this.cuePictureBox.TabStop = false;
            // 
            // loopPanel
            // 
            this.loopPanel.BackColor = System.Drawing.Color.Silver;
            this.loopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loopPanel.Controls.Add(this.cueSecondsLabel);
            this.loopPanel.Controls.Add(this.endLoopNowButton);
            this.loopPanel.Controls.Add(this.startLoopNowButton);
            this.loopPanel.Controls.Add(this.cueComboBox);
            this.loopPanel.Controls.Add(this.cuePictureBox);
            this.loopPanel.Controls.Add(this.cueLabel);
            this.loopPanel.Controls.Add(this.startLoopMinuteUpDown);
            this.loopPanel.Controls.Add(this.startLoopSecondUpDown);
            this.loopPanel.Controls.Add(this.startLoopMilliUpDown);
            this.loopPanel.Controls.Add(this.endLoopMinuteUpDown);
            this.loopPanel.Controls.Add(this.endLoopSecondUpDown);
            this.loopPanel.Controls.Add(this.endLoopMilliUpDown);
            this.loopPanel.Controls.Add(this.startLoopLabel);
            this.loopPanel.Controls.Add(this.endLoopLabel);
            this.loopPanel.Controls.Add(this.loopCheckBox);
            this.loopPanel.Location = new System.Drawing.Point(132, 30);
            this.loopPanel.Name = "loopPanel";
            this.loopPanel.Size = new System.Drawing.Size(443, 54);
            this.loopPanel.TabIndex = 2;
            // 
            // cueSecondsLabel
            // 
            this.cueSecondsLabel.AutoSize = true;
            this.cueSecondsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cueSecondsLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.cueSecondsLabel.Location = new System.Drawing.Point(410, 7);
            this.cueSecondsLabel.Name = "cueSecondsLabel";
            this.cueSecondsLabel.Size = new System.Drawing.Size(23, 12);
            this.cueSecondsLabel.TabIndex = 90;
            this.cueSecondsLabel.Text = "sec.";
            // 
            // vol50Label
            // 
            this.vol50Label.AutoSize = true;
            this.vol50Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vol50Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.vol50Label.Location = new System.Drawing.Point(358, 107);
            this.vol50Label.Name = "vol50Label";
            this.vol50Label.Size = new System.Drawing.Size(23, 12);
            this.vol50Label.TabIndex = 57;
            this.vol50Label.Text = "50%";
            // 
            // vol75Label
            // 
            this.vol75Label.AutoSize = true;
            this.vol75Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vol75Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.vol75Label.Location = new System.Drawing.Point(358, 73);
            this.vol75Label.Name = "vol75Label";
            this.vol75Label.Size = new System.Drawing.Size(23, 12);
            this.vol75Label.TabIndex = 58;
            this.vol75Label.Text = "75%";
            // 
            // vol25Label
            // 
            this.vol25Label.AutoSize = true;
            this.vol25Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vol25Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.vol25Label.Location = new System.Drawing.Point(358, 139);
            this.vol25Label.Name = "vol25Label";
            this.vol25Label.Size = new System.Drawing.Size(23, 12);
            this.vol25Label.TabIndex = 59;
            this.vol25Label.Text = "25%";
            // 
            // speed15XLabel
            // 
            this.speed15XLabel.AutoSize = true;
            this.speed15XLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed15XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed15XLabel.Location = new System.Drawing.Point(246, 38);
            this.speed15XLabel.Name = "speed15XLabel";
            this.speed15XLabel.Size = new System.Drawing.Size(23, 12);
            this.speed15XLabel.TabIndex = 60;
            this.speed15XLabel.Text = "x1.5";
            // 
            // speed05XLabel
            // 
            this.speed05XLabel.AutoSize = true;
            this.speed05XLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speed05XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed05XLabel.Location = new System.Drawing.Point(111, 38);
            this.speed05XLabel.Name = "speed05XLabel";
            this.speed05XLabel.Size = new System.Drawing.Size(23, 12);
            this.speed05XLabel.TabIndex = 61;
            this.speed05XLabel.Text = "x0.5";
            // 
            // play2QDurationLabel
            // 
            this.play2QDurationLabel.AutoSize = true;
            this.play2QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play2QDurationLabel.Location = new System.Drawing.Point(291, 218);
            this.play2QDurationLabel.Name = "play2QDurationLabel";
            this.play2QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play2QDurationLabel.TabIndex = 62;
            this.play2QDurationLabel.Text = "00:00";
            // 
            // play3QDurationLabel
            // 
            this.play3QDurationLabel.AutoSize = true;
            this.play3QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play3QDurationLabel.Location = new System.Drawing.Point(423, 218);
            this.play3QDurationLabel.Name = "play3QDurationLabel";
            this.play3QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play3QDurationLabel.TabIndex = 63;
            this.play3QDurationLabel.Text = "00:00";
            // 
            // play1QDurationLabel
            // 
            this.play1QDurationLabel.AutoSize = true;
            this.play1QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play1QDurationLabel.Location = new System.Drawing.Point(173, 218);
            this.play1QDurationLabel.Name = "play1QDurationLabel";
            this.play1QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play1QDurationLabel.TabIndex = 64;
            this.play1QDurationLabel.Text = "00:00";
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlPanel.Controls.Add(this.openFileButton);
            this.controlPanel.Controls.Add(this.playPauseButton);
            this.controlPanel.Controls.Add(this.loopPanel);
            this.controlPanel.Controls.Add(this.currentLabel);
            this.controlPanel.Controls.Add(this.currentMilliUpDown);
            this.controlPanel.Controls.Add(this.currentMinuteUpDown);
            this.controlPanel.Controls.Add(this.currentSecondUpDown);
            this.controlPanel.Location = new System.Drawing.Point(14, 291);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(575, 88);
            this.controlPanel.TabIndex = 2;
            // 
            // pitchValueLabel
            // 
            this.pitchValueLabel.AutoSize = true;
            this.pitchValueLabel.Location = new System.Drawing.Point(8, 124);
            this.pitchValueLabel.Name = "pitchValueLabel";
            this.pitchValueLabel.Size = new System.Drawing.Size(10, 13);
            this.pitchValueLabel.TabIndex = 3;
            this.pitchValueLabel.Text = "-";
            // 
            // pitch1Label
            // 
            this.pitch1Label.AutoSize = true;
            this.pitch1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitch1Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pitch1Label.Location = new System.Drawing.Point(315, 124);
            this.pitch1Label.Name = "pitch1Label";
            this.pitch1Label.Size = new System.Drawing.Size(21, 12);
            this.pitch1Label.TabIndex = 68;
            this.pitch1Label.Text = "8va";
            // 
            // pitchM1Label
            // 
            this.pitchM1Label.AutoSize = true;
            this.pitchM1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitchM1Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pitchM1Label.Location = new System.Drawing.Point(51, 124);
            this.pitchM1Label.Name = "pitchM1Label";
            this.pitchM1Label.Size = new System.Drawing.Size(21, 12);
            this.pitchM1Label.TabIndex = 67;
            this.pitchM1Label.Text = "8vb";
            // 
            // pitch0Label
            // 
            this.pitch0Label.AutoSize = true;
            this.pitch0Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitch0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pitch0Label.Location = new System.Drawing.Point(188, 125);
            this.pitch0Label.Name = "pitch0Label";
            this.pitch0Label.Size = new System.Drawing.Size(10, 12);
            this.pitch0Label.TabIndex = 66;
            this.pitch0Label.Text = "0";
            // 
            // pitchTrackBar
            // 
            this.pitchTrackBar.LargeChange = 0;
            this.pitchTrackBar.Location = new System.Drawing.Point(51, 141);
            this.pitchTrackBar.Maximum = 96;
            this.pitchTrackBar.Minimum = -96;
            this.pitchTrackBar.Name = "pitchTrackBar";
            this.pitchTrackBar.Size = new System.Drawing.Size(287, 45);
            this.pitchTrackBar.SmallChange = 4;
            this.pitchTrackBar.TabIndex = 1;
            this.pitchTrackBar.TickFrequency = 8;
            this.pitchTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.pitchTrackBar.ValueChanged += new System.EventHandler(this.pitchTrackBar_ValueChanged);
            this.pitchTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pitchTrackBar_MouseDown);
            this.pitchTrackBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pitchTrackBar_MouseMove);
            this.pitchTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pitchTrackBar_MouseUp);
            // 
            // trackBarPanel
            // 
            this.trackBarPanel.Controls.Add(this.removeVocalsCheckBox);
            this.trackBarPanel.Controls.Add(this.label1);
            this.trackBarPanel.Controls.Add(this.timeStretchProfileComboBox);
            this.trackBarPanel.Controls.Add(this.timeStretchProfileLabel);
            this.trackBarPanel.Controls.Add(this.hiEqValueLabel);
            this.trackBarPanel.Controls.Add(this.medEqValueLabel);
            this.trackBarPanel.Controls.Add(this.loEqValueLabel);
            this.trackBarPanel.Controls.Add(this.eqM100Label);
            this.trackBarPanel.Controls.Add(this.eq100Label);
            this.trackBarPanel.Controls.Add(this.eq0Label);
            this.trackBarPanel.Controls.Add(this.equalizerHoverLabel);
            this.trackBarPanel.Controls.Add(this.hiEqHoverLabel);
            this.trackBarPanel.Controls.Add(this.medEqHoverLabel);
            this.trackBarPanel.Controls.Add(this.loEqHoverLabel);
            this.trackBarPanel.Controls.Add(this.hiEqTrackBar);
            this.trackBarPanel.Controls.Add(this.medEqTrackBar);
            this.trackBarPanel.Controls.Add(this.loEqTrackBar);
            this.trackBarPanel.Controls.Add(this.positionMarkersPanel);
            this.trackBarPanel.Controls.Add(this.speedValueLabel);
            this.trackBarPanel.Controls.Add(this.tempoTrackBar);
            this.trackBarPanel.Controls.Add(this.volumeTrackBar);
            this.trackBarPanel.Controls.Add(this.pitchValueLabel);
            this.trackBarPanel.Controls.Add(this.speed1XLabel);
            this.trackBarPanel.Controls.Add(this.pitchLabel);
            this.trackBarPanel.Controls.Add(this.playTimeTrackBar);
            this.trackBarPanel.Controls.Add(this.pitch1Label);
            this.trackBarPanel.Controls.Add(this.speed01XLabel);
            this.trackBarPanel.Controls.Add(this.pitchM1Label);
            this.trackBarPanel.Controls.Add(this.speed2XLabel);
            this.trackBarPanel.Controls.Add(this.pitch0Label);
            this.trackBarPanel.Controls.Add(this.speedLabel);
            this.trackBarPanel.Controls.Add(this.pitchTrackBar);
            this.trackBarPanel.Controls.Add(this.volumeLabel);
            this.trackBarPanel.Controls.Add(this.volume100Label);
            this.trackBarPanel.Controls.Add(this.play1QDurationLabel);
            this.trackBarPanel.Controls.Add(this.volume0Label);
            this.trackBarPanel.Controls.Add(this.play3QDurationLabel);
            this.trackBarPanel.Controls.Add(this.positionLabel);
            this.trackBarPanel.Controls.Add(this.play2QDurationLabel);
            this.trackBarPanel.Controls.Add(this.play0Label);
            this.trackBarPanel.Controls.Add(this.speed05XLabel);
            this.trackBarPanel.Controls.Add(this.playDurationLabel);
            this.trackBarPanel.Controls.Add(this.speed15XLabel);
            this.trackBarPanel.Controls.Add(this.vol25Label);
            this.trackBarPanel.Controls.Add(this.volumeValueLabel);
            this.trackBarPanel.Controls.Add(this.vol75Label);
            this.trackBarPanel.Controls.Add(this.vol50Label);
            this.trackBarPanel.Location = new System.Drawing.Point(6, 24);
            this.trackBarPanel.Name = "trackBarPanel";
            this.trackBarPanel.Size = new System.Drawing.Size(584, 266);
            this.trackBarPanel.TabIndex = 1;
            // 
            // removeVocalsCheckBox
            // 
            this.removeVocalsCheckBox.AutoSize = true;
            this.removeVocalsCheckBox.Location = new System.Drawing.Point(9, 184);
            this.removeVocalsCheckBox.Name = "removeVocalsCheckBox";
            this.removeVocalsCheckBox.Size = new System.Drawing.Size(104, 17);
            this.removeVocalsCheckBox.TabIndex = 90;
            this.removeVocalsCheckBox.Text = "Suppress vocals";
            this.removeVocalsCheckBox.UseVisualStyleBackColor = true;
            this.removeVocalsCheckBox.CheckedChanged += new System.EventHandler(this.removeVocalsCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(7, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 12);
            this.label1.TabIndex = 89;
            this.label1.Text = "Semi-Tones";
            // 
            // timeStretchProfileComboBox
            // 
            this.timeStretchProfileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeStretchProfileComboBox.FormattingEnabled = true;
            this.timeStretchProfileComboBox.Location = new System.Drawing.Point(146, 97);
            this.timeStretchProfileComboBox.Name = "timeStretchProfileComboBox";
            this.timeStretchProfileComboBox.Size = new System.Drawing.Size(193, 21);
            this.timeStretchProfileComboBox.TabIndex = 88;
            this.timeStretchProfileComboBox.Visible = false;
            this.timeStretchProfileComboBox.SelectedIndexChanged += new System.EventHandler(this.timeStretchProfileComboBox_SelectedIndexChanged);
            // 
            // timeStretchProfileLabel
            // 
            this.timeStretchProfileLabel.AutoSize = true;
            this.timeStretchProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeStretchProfileLabel.Location = new System.Drawing.Point(6, 100);
            this.timeStretchProfileLabel.Name = "timeStretchProfileLabel";
            this.timeStretchProfileLabel.Size = new System.Drawing.Size(138, 15);
            this.timeStretchProfileLabel.TabIndex = 87;
            this.timeStretchProfileLabel.Text = "Time Stretch Profile:";
            this.timeStretchProfileLabel.Visible = false;
            // 
            // hiEqValueLabel
            // 
            this.hiEqValueLabel.AutoSize = true;
            this.hiEqValueLabel.Location = new System.Drawing.Point(537, 190);
            this.hiEqValueLabel.Name = "hiEqValueLabel";
            this.hiEqValueLabel.Size = new System.Drawing.Size(10, 13);
            this.hiEqValueLabel.TabIndex = 86;
            this.hiEqValueLabel.Text = "-";
            this.hiEqValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // medEqValueLabel
            // 
            this.medEqValueLabel.AutoSize = true;
            this.medEqValueLabel.Location = new System.Drawing.Point(498, 190);
            this.medEqValueLabel.Name = "medEqValueLabel";
            this.medEqValueLabel.Size = new System.Drawing.Size(10, 13);
            this.medEqValueLabel.TabIndex = 85;
            this.medEqValueLabel.Text = "-";
            this.medEqValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loEqValueLabel
            // 
            this.loEqValueLabel.AutoSize = true;
            this.loEqValueLabel.Location = new System.Drawing.Point(459, 190);
            this.loEqValueLabel.Name = "loEqValueLabel";
            this.loEqValueLabel.Size = new System.Drawing.Size(10, 13);
            this.loEqValueLabel.TabIndex = 84;
            this.loEqValueLabel.Text = "-";
            this.loEqValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // eqM100Label
            // 
            this.eqM100Label.AutoSize = true;
            this.eqM100Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eqM100Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.eqM100Label.Location = new System.Drawing.Point(429, 171);
            this.eqM100Label.Name = "eqM100Label";
            this.eqM100Label.Size = new System.Drawing.Size(31, 12);
            this.eqM100Label.TabIndex = 83;
            this.eqM100Label.Text = "-100%";
            // 
            // eq100Label
            // 
            this.eq100Label.AutoSize = true;
            this.eq100Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eq100Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.eq100Label.Location = new System.Drawing.Point(432, 39);
            this.eq100Label.Name = "eq100Label";
            this.eq100Label.Size = new System.Drawing.Size(28, 12);
            this.eq100Label.TabIndex = 82;
            this.eq100Label.Text = "100%";
            // 
            // eq0Label
            // 
            this.eq0Label.AutoSize = true;
            this.eq0Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eq0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.eq0Label.Location = new System.Drawing.Point(442, 108);
            this.eq0Label.Name = "eq0Label";
            this.eq0Label.Size = new System.Drawing.Size(18, 12);
            this.eq0Label.TabIndex = 81;
            this.eq0Label.Text = "0%";
            // 
            // hiEqTrackBar
            // 
            this.hiEqTrackBar.LargeChange = 10;
            this.hiEqTrackBar.Location = new System.Drawing.Point(535, 39);
            this.hiEqTrackBar.Maximum = 100;
            this.hiEqTrackBar.Minimum = -100;
            this.hiEqTrackBar.Name = "hiEqTrackBar";
            this.hiEqTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.hiEqTrackBar.Size = new System.Drawing.Size(45, 151);
            this.hiEqTrackBar.SmallChange = 5;
            this.hiEqTrackBar.TabIndex = 5;
            this.hiEqTrackBar.TickFrequency = 20;
            this.hiEqTrackBar.ValueChanged += new System.EventHandler(this.hiEqTrackBar_ValueChanged);
            this.hiEqTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.hiEqTrackBar_MouseDown);
            // 
            // medEqTrackBar
            // 
            this.medEqTrackBar.LargeChange = 10;
            this.medEqTrackBar.Location = new System.Drawing.Point(497, 39);
            this.medEqTrackBar.Maximum = 100;
            this.medEqTrackBar.Minimum = -100;
            this.medEqTrackBar.Name = "medEqTrackBar";
            this.medEqTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.medEqTrackBar.Size = new System.Drawing.Size(45, 151);
            this.medEqTrackBar.SmallChange = 5;
            this.medEqTrackBar.TabIndex = 4;
            this.medEqTrackBar.TickFrequency = 20;
            this.medEqTrackBar.ValueChanged += new System.EventHandler(this.medEqTrackBar_ValueChanged);
            this.medEqTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.medEqTrackBar_MouseDown);
            // 
            // loEqTrackBar
            // 
            this.loEqTrackBar.LargeChange = 10;
            this.loEqTrackBar.Location = new System.Drawing.Point(459, 39);
            this.loEqTrackBar.Maximum = 100;
            this.loEqTrackBar.Minimum = -100;
            this.loEqTrackBar.Name = "loEqTrackBar";
            this.loEqTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.loEqTrackBar.Size = new System.Drawing.Size(45, 151);
            this.loEqTrackBar.SmallChange = 5;
            this.loEqTrackBar.TabIndex = 3;
            this.loEqTrackBar.TickFrequency = 20;
            this.loEqTrackBar.ValueChanged += new System.EventHandler(this.loEqTrackBar_ValueChanged);
            this.loEqTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loEqtrackBar_MouseDown);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentFilesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(603, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recent1ToolStripMenuItem,
            this.recent2ToolStripMenuItem,
            this.recent3ToolStripMenuItem,
            this.recent4ToolStripMenuItem,
            this.recent5ToolStripMenuItem,
            this.recent6ToolStripMenuItem,
            this.recent7ToolStripMenuItem,
            this.recent8ToolStripMenuItem});
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.recentFilesToolStripMenuItem.Text = "Recent Files";
            this.recentFilesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.recentFilesToolStripMenuItem_DropDownOpening);
            // 
            // recent1ToolStripMenuItem
            // 
            this.recent1ToolStripMenuItem.Name = "recent1ToolStripMenuItem";
            this.recent1ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent1ToolStripMenuItem.Text = "Recent1";
            // 
            // recent2ToolStripMenuItem
            // 
            this.recent2ToolStripMenuItem.Name = "recent2ToolStripMenuItem";
            this.recent2ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent2ToolStripMenuItem.Text = "Recent2";
            // 
            // recent3ToolStripMenuItem
            // 
            this.recent3ToolStripMenuItem.Name = "recent3ToolStripMenuItem";
            this.recent3ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent3ToolStripMenuItem.Text = "Recent3";
            // 
            // recent4ToolStripMenuItem
            // 
            this.recent4ToolStripMenuItem.Name = "recent4ToolStripMenuItem";
            this.recent4ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent4ToolStripMenuItem.Text = "Recent4";
            // 
            // recent5ToolStripMenuItem
            // 
            this.recent5ToolStripMenuItem.Name = "recent5ToolStripMenuItem";
            this.recent5ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent5ToolStripMenuItem.Text = "Recent5";
            // 
            // recent6ToolStripMenuItem
            // 
            this.recent6ToolStripMenuItem.Name = "recent6ToolStripMenuItem";
            this.recent6ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent6ToolStripMenuItem.Text = "Recent6";
            // 
            // recent7ToolStripMenuItem
            // 
            this.recent7ToolStripMenuItem.Name = "recent7ToolStripMenuItem";
            this.recent7ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent7ToolStripMenuItem.Text = "Recent7";
            // 
            // recent8ToolStripMenuItem
            // 
            this.recent8ToolStripMenuItem.Name = "recent8ToolStripMenuItem";
            this.recent8ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.recent8ToolStripMenuItem.Text = "Recent8";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyboardShortcutsMenuItem,
            this.showTechLogToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // keyboardShortcutsMenuItem
            // 
            this.keyboardShortcutsMenuItem.Name = "keyboardShortcutsMenuItem";
            this.keyboardShortcutsMenuItem.Size = new System.Drawing.Size(189, 22);
            this.keyboardShortcutsMenuItem.Text = "Keyboard Shortcuts..";
            this.keyboardShortcutsMenuItem.Click += new System.EventHandler(this.keyboardShortcutsMenuItem_Click);
            // 
            // showTechLogToolStripMenuItem
            // 
            this.showTechLogToolStripMenuItem.Name = "showTechLogToolStripMenuItem";
            this.showTechLogToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.showTechLogToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.showTechLogToolStripMenuItem.Text = "Show Tech. Log..";
            this.showTechLogToolStripMenuItem.Click += new System.EventHandler(this.showTechLogToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(189, 22);
            this.aboutMenuItem.Text = "About..";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 1500;
            this.toolTip.ReshowDelay = 100;
            // 
            // filenameDescLabel
            // 
            this.filenameDescLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameDescLabel.Name = "filenameDescLabel";
            this.filenameDescLabel.Size = new System.Drawing.Size(29, 17);
            this.filenameDescLabel.Text = "File:";
            // 
            // equalizerHoverLabel
            // 
            this.equalizerHoverLabel.AutoSize = true;
            this.equalizerHoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equalizerHoverLabel.Location = new System.Drawing.Point(470, 4);
            this.equalizerHoverLabel.Name = "equalizerHoverLabel";
            this.equalizerHoverLabel.Size = new System.Drawing.Size(68, 15);
            this.equalizerHoverLabel.TabIndex = 80;
            this.equalizerHoverLabel.Text = "Equalizer";
            this.equalizerHoverLabel.Click += new System.EventHandler(this.equalizerHoverLabel_Click);
            // 
            // hiEqHoverLabel
            // 
            this.hiEqHoverLabel.AutoSize = true;
            this.hiEqHoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hiEqHoverLabel.Location = new System.Drawing.Point(540, 23);
            this.hiEqHoverLabel.Name = "hiEqHoverLabel";
            this.hiEqHoverLabel.Size = new System.Drawing.Size(19, 13);
            this.hiEqHoverLabel.TabIndex = 79;
            this.hiEqHoverLabel.Text = "Hi";
            this.hiEqHoverLabel.Click += new System.EventHandler(this.hiEqHoverLabel_Click);
            // 
            // medEqHoverLabel
            // 
            this.medEqHoverLabel.AutoSize = true;
            this.medEqHoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.medEqHoverLabel.Location = new System.Drawing.Point(492, 23);
            this.medEqHoverLabel.Name = "medEqHoverLabel";
            this.medEqHoverLabel.Size = new System.Drawing.Size(31, 13);
            this.medEqHoverLabel.TabIndex = 78;
            this.medEqHoverLabel.Text = "Med";
            this.medEqHoverLabel.Click += new System.EventHandler(this.medEqHoverLabel_Click);
            // 
            // loEqHoverLabel
            // 
            this.loEqHoverLabel.AutoSize = true;
            this.loEqHoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loEqHoverLabel.Location = new System.Drawing.Point(455, 23);
            this.loEqHoverLabel.Name = "loEqHoverLabel";
            this.loEqHoverLabel.Size = new System.Drawing.Size(21, 13);
            this.loEqHoverLabel.TabIndex = 77;
            this.loEqHoverLabel.Text = "Lo";
            this.loEqHoverLabel.Click += new System.EventHandler(this.loEqHoverLabel_Click);
            // 
            // positionMarkersPanel
            // 
            this.positionMarkersPanel.Location = new System.Drawing.Point(74, 233);
            this.positionMarkersPanel.Name = "positionMarkersPanel";
            this.positionMarkersPanel.Size = new System.Drawing.Size(496, 5);
            this.positionMarkersPanel.TabIndex = 73;
            this.positionMarkersPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.positionMarkersPanel_Paint);
            // 
            // pitchLabel
            // 
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitchLabel.Location = new System.Drawing.Point(6, 152);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(39, 15);
            this.pitchLabel.TabIndex = 4;
            this.pitchLabel.Text = "Pitch";
            this.pitchLabel.Click += new System.EventHandler(this.pitchLabel_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(6, 65);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(48, 15);
            this.speedLabel.TabIndex = 1;
            this.speedLabel.Text = "Speed";
            this.speedLabel.Click += new System.EventHandler(this.speedLabel_Click);
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volumeLabel.Location = new System.Drawing.Point(378, 4);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(55, 15);
            this.volumeLabel.TabIndex = 7;
            this.volumeLabel.Text = "Volume";
            this.volumeLabel.Click += new System.EventHandler(this.volumeLabel_Click);
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionLabel.Location = new System.Drawing.Point(6, 243);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(59, 15);
            this.positionLabel.TabIndex = 9;
            this.positionLabel.Text = "Position";
            this.positionLabel.Click += new System.EventHandler(this.positionLabel_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.BackColor = System.Drawing.Color.Transparent;
            this.openFileButton.ButtonText = null;
            this.openFileButton.GlowColor = System.Drawing.Color.Yellow;
            this.openFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.openFileButton.ImageSize = new System.Drawing.Size(48, 48);
            this.openFileButton.Location = new System.Drawing.Point(7, 28);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(56, 56);
            this.openFileButton.TabIndex = 13;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // playPauseButton
            // 
            this.playPauseButton.BackColor = System.Drawing.Color.Transparent;
            this.playPauseButton.ButtonText = null;
            this.playPauseButton.GlowColor = System.Drawing.Color.Yellow;
            this.playPauseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.playPauseButton.ImageSize = new System.Drawing.Size(48, 48);
            this.playPauseButton.Location = new System.Drawing.Point(68, 28);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(56, 56);
            this.playPauseButton.TabIndex = 12;
            this.playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            // 
            // endLoopNowButton
            // 
            this.endLoopNowButton.BackColor = System.Drawing.Color.Transparent;
            this.endLoopNowButton.ButtonColor = System.Drawing.Color.Turquoise;
            this.endLoopNowButton.ButtonText = "Now";
            this.endLoopNowButton.CornerRadius = 2;
            this.endLoopNowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.endLoopNowButton.GlowColor = System.Drawing.Color.Yellow;
            this.endLoopNowButton.HighlightColor = System.Drawing.Color.Black;
            this.endLoopNowButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.endLoopNowButton.ImageSize = new System.Drawing.Size(48, 48);
            this.endLoopNowButton.Location = new System.Drawing.Point(281, 28);
            this.endLoopNowButton.Name = "endLoopNowButton";
            this.endLoopNowButton.Size = new System.Drawing.Size(46, 21);
            this.endLoopNowButton.TabIndex = 56;
            this.endLoopNowButton.TextPadding = new System.Drawing.Size(1, 1);
            this.endLoopNowButton.Click += new System.EventHandler(this.endLoopNowButton_Click);
            // 
            // startLoopNowButton
            // 
            this.startLoopNowButton.BackColor = System.Drawing.Color.Transparent;
            this.startLoopNowButton.ButtonColor = System.Drawing.Color.Lime;
            this.startLoopNowButton.ButtonText = "Now";
            this.startLoopNowButton.CornerRadius = 2;
            this.startLoopNowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.startLoopNowButton.GlowColor = System.Drawing.Color.Yellow;
            this.startLoopNowButton.HighlightColor = System.Drawing.Color.Black;
            this.startLoopNowButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startLoopNowButton.ImageSize = new System.Drawing.Size(48, 48);
            this.startLoopNowButton.Location = new System.Drawing.Point(281, 1);
            this.startLoopNowButton.Name = "startLoopNowButton";
            this.startLoopNowButton.Size = new System.Drawing.Size(46, 22);
            this.startLoopNowButton.TabIndex = 14;
            this.startLoopNowButton.TextPadding = new System.Drawing.Size(1, 1);
            this.startLoopNowButton.Click += new System.EventHandler(this.startLoopNowButton_Click);
            // 
            // resetPresetButton
            // 
            this.resetPresetButton.BackColor = System.Drawing.Color.Transparent;
            this.resetPresetButton.ButtonColor = System.Drawing.Color.GhostWhite;
            this.resetPresetButton.ButtonText = null;
            this.resetPresetButton.GlowColor = System.Drawing.Color.Yellow;
            this.resetPresetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resetPresetButton.ImageSize = new System.Drawing.Size(48, 48);
            this.resetPresetButton.Location = new System.Drawing.Point(6, 63);
            this.resetPresetButton.Name = "resetPresetButton";
            this.resetPresetButton.Size = new System.Drawing.Size(56, 56);
            this.resetPresetButton.TabIndex = 11;
            this.resetPresetButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resetBankButton_MouseDown);
            this.resetPresetButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.resetBankButton_MouseUp);
            // 
            // writePresetButton
            // 
            this.writePresetButton.BackColor = System.Drawing.Color.Transparent;
            this.writePresetButton.ButtonColor = System.Drawing.Color.GhostWhite;
            this.writePresetButton.ButtonText = null;
            this.writePresetButton.GlowColor = System.Drawing.Color.Yellow;
            this.writePresetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.writePresetButton.ImageSize = new System.Drawing.Size(48, 48);
            this.writePresetButton.Location = new System.Drawing.Point(6, 4);
            this.writePresetButton.Name = "writePresetButton";
            this.writePresetButton.Size = new System.Drawing.Size(56, 56);
            this.writePresetButton.TabIndex = 10;
            this.writePresetButton.Click += new System.EventHandler(this.writePresetButton_Click);
            // 
            // presetControl7
            // 
            this.presetControl7.Id = "7";
            this.presetControl7.Location = new System.Drawing.Point(329, 62);
            this.presetControl7.Name = "presetControl7";
            presetData1.Cue = System.TimeSpan.Parse("00:00:00");
            presetData1.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData1.Description = "";
            presetData1.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData1.HiEqValue = 0F;
            presetData1.LoEqValue = 0F;
            presetData1.Loop = false;
            presetData1.MedEqValue = 0F;
            presetData1.Pitch = 0F;
            presetData1.RemoveVocals = false;
            presetData1.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData1.Tempo = 1F;
            presetData1.TimeStretchProfile = null;
            presetData1.Volume = 0.75F;
            this.presetControl7.PresetData = presetData1;
            this.presetControl7.PresetDescription = "";
            this.presetControl7.Size = new System.Drawing.Size(111, 59);
            this.presetControl7.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl7.TabIndex = 8;
            this.presetControl7.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl7, "Preset 7 (Alt+7)");
            this.presetControl7.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl7.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl7.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl5
            // 
            this.presetControl5.Id = "5";
            this.presetControl5.Location = new System.Drawing.Point(71, 62);
            this.presetControl5.Name = "presetControl5";
            presetData2.Cue = System.TimeSpan.Parse("00:00:00");
            presetData2.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData2.Description = "";
            presetData2.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData2.HiEqValue = 0F;
            presetData2.LoEqValue = 0F;
            presetData2.Loop = false;
            presetData2.MedEqValue = 0F;
            presetData2.Pitch = 0F;
            presetData2.RemoveVocals = false;
            presetData2.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData2.Tempo = 1F;
            presetData2.TimeStretchProfile = null;
            presetData2.Volume = 0.75F;
            this.presetControl5.PresetData = presetData2;
            this.presetControl5.PresetDescription = "";
            this.presetControl5.Size = new System.Drawing.Size(111, 59);
            this.presetControl5.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl5.TabIndex = 6;
            this.presetControl5.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl5, "Preset 5 (Alt+5)");
            this.presetControl5.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl5.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl5.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl6
            // 
            this.presetControl6.Id = "6";
            this.presetControl6.Location = new System.Drawing.Point(200, 62);
            this.presetControl6.Name = "presetControl6";
            presetData3.Cue = System.TimeSpan.Parse("00:00:00");
            presetData3.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData3.Description = "";
            presetData3.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData3.HiEqValue = 0F;
            presetData3.LoEqValue = 0F;
            presetData3.Loop = false;
            presetData3.MedEqValue = 0F;
            presetData3.Pitch = 0F;
            presetData3.RemoveVocals = false;
            presetData3.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData3.Tempo = 1F;
            presetData3.TimeStretchProfile = null;
            presetData3.Volume = 0.75F;
            this.presetControl6.PresetData = presetData3;
            this.presetControl6.PresetDescription = "";
            this.presetControl6.Size = new System.Drawing.Size(111, 59);
            this.presetControl6.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl6.TabIndex = 7;
            this.presetControl6.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl6, "Preset 6 (Alt+6)");
            this.presetControl6.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl6.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl6.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl8
            // 
            this.presetControl8.Id = "8";
            this.presetControl8.Location = new System.Drawing.Point(458, 62);
            this.presetControl8.Name = "presetControl8";
            presetData4.Cue = System.TimeSpan.Parse("00:00:00");
            presetData4.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData4.Description = "";
            presetData4.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData4.HiEqValue = 0F;
            presetData4.LoEqValue = 0F;
            presetData4.Loop = false;
            presetData4.MedEqValue = 0F;
            presetData4.Pitch = 0F;
            presetData4.RemoveVocals = false;
            presetData4.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData4.Tempo = 1F;
            presetData4.TimeStretchProfile = null;
            presetData4.Volume = 0.75F;
            this.presetControl8.PresetData = presetData4;
            this.presetControl8.PresetDescription = "";
            this.presetControl8.Size = new System.Drawing.Size(111, 59);
            this.presetControl8.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl8.TabIndex = 9;
            this.presetControl8.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl8, "Preset 8 (Alt+8)");
            this.presetControl8.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl8.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl8.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl3
            // 
            this.presetControl3.Id = "3";
            this.presetControl3.Location = new System.Drawing.Point(329, 2);
            this.presetControl3.Name = "presetControl3";
            presetData5.Cue = System.TimeSpan.Parse("00:00:00");
            presetData5.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData5.Description = "";
            presetData5.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData5.HiEqValue = 0F;
            presetData5.LoEqValue = 0F;
            presetData5.Loop = false;
            presetData5.MedEqValue = 0F;
            presetData5.Pitch = 0F;
            presetData5.RemoveVocals = false;
            presetData5.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData5.Tempo = 1F;
            presetData5.TimeStretchProfile = null;
            presetData5.Volume = 0.75F;
            this.presetControl3.PresetData = presetData5;
            this.presetControl3.PresetDescription = "";
            this.presetControl3.Size = new System.Drawing.Size(111, 59);
            this.presetControl3.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl3.TabIndex = 4;
            this.presetControl3.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl3, "Preset 3 (Alt+3)");
            this.presetControl3.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl3.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl3.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl1
            // 
            this.presetControl1.Id = "1";
            this.presetControl1.Location = new System.Drawing.Point(71, 2);
            this.presetControl1.Name = "presetControl1";
            presetData6.Cue = System.TimeSpan.Parse("00:00:00");
            presetData6.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData6.Description = "";
            presetData6.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData6.HiEqValue = 0F;
            presetData6.LoEqValue = 0F;
            presetData6.Loop = false;
            presetData6.MedEqValue = 0F;
            presetData6.Pitch = 0F;
            presetData6.RemoveVocals = false;
            presetData6.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData6.Tempo = 1F;
            presetData6.TimeStretchProfile = null;
            presetData6.Volume = 0.75F;
            this.presetControl1.PresetData = presetData6;
            this.presetControl1.PresetDescription = "";
            this.presetControl1.Size = new System.Drawing.Size(111, 59);
            this.presetControl1.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl1.TabIndex = 2;
            this.presetControl1.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl1, "Preset 1 (Alt+1)");
            this.presetControl1.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl1.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl1.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl2
            // 
            this.presetControl2.Id = "2";
            this.presetControl2.Location = new System.Drawing.Point(200, 2);
            this.presetControl2.Name = "presetControl2";
            presetData7.Cue = System.TimeSpan.Parse("00:00:00");
            presetData7.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData7.Description = "";
            presetData7.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData7.HiEqValue = 0F;
            presetData7.LoEqValue = 0F;
            presetData7.Loop = false;
            presetData7.MedEqValue = 0F;
            presetData7.Pitch = 0F;
            presetData7.RemoveVocals = false;
            presetData7.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData7.Tempo = 1F;
            presetData7.TimeStretchProfile = null;
            presetData7.Volume = 0.75F;
            this.presetControl2.PresetData = presetData7;
            this.presetControl2.PresetDescription = "";
            this.presetControl2.Size = new System.Drawing.Size(111, 59);
            this.presetControl2.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl2.TabIndex = 3;
            this.presetControl2.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl2, "Preset 2 (Alt+2)");
            this.presetControl2.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl2.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl2.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl4
            // 
            this.presetControl4.Id = "4";
            this.presetControl4.Location = new System.Drawing.Point(458, 2);
            this.presetControl4.Name = "presetControl4";
            presetData8.Cue = System.TimeSpan.Parse("00:00:00");
            presetData8.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData8.Description = "";
            presetData8.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData8.HiEqValue = 0F;
            presetData8.LoEqValue = 0F;
            presetData8.Loop = false;
            presetData8.MedEqValue = 0F;
            presetData8.Pitch = 0F;
            presetData8.RemoveVocals = false;
            presetData8.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData8.Tempo = 1F;
            presetData8.TimeStretchProfile = null;
            presetData8.Volume = 0.75F;
            this.presetControl4.PresetData = presetData8;
            this.presetControl4.PresetDescription = "";
            this.presetControl4.Size = new System.Drawing.Size(111, 59);
            this.presetControl4.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl4.TabIndex = 5;
            this.presetControl4.Title = "[No Desc]";
            this.toolTip.SetToolTip(this.presetControl4, "Preset 4 (Alt+4)");
            this.presetControl4.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl4.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl4.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 532);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.trackBarPanel);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.presetPanel);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Practice #";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.tempoTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playTimeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopMinuteUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopSecondUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startLoopMilliUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopMinuteUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopSecondUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endLoopMilliUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentMilliUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentSecondUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentMinuteUpDown)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.presetPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cuePictureBox)).EndInit();
            this.loopPanel.ResumeLayout(false);
            this.loopPanel.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pitchTrackBar)).EndInit();
            this.trackBarPanel.ResumeLayout(false);
            this.trackBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hiEqTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medEqTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loEqTrackBar)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tempoTrackBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TrackBar volumeTrackBar;
        private System.Windows.Forms.Label speed1XLabel;
        private System.Windows.Forms.TrackBar playTimeTrackBar;
        private System.Windows.Forms.Label speed01XLabel;
        private System.Windows.Forms.Label speed2XLabel;
        private System.Windows.Forms.NumericUpDown startLoopMinuteUpDown;
        private System.Windows.Forms.NumericUpDown startLoopSecondUpDown;
        private System.Windows.Forms.NumericUpDown startLoopMilliUpDown;
        private System.Windows.Forms.NumericUpDown endLoopMinuteUpDown;
        private System.Windows.Forms.NumericUpDown endLoopSecondUpDown;
        private System.Windows.Forms.NumericUpDown endLoopMilliUpDown;
        private System.Windows.Forms.Label startLoopLabel;
        private System.Windows.Forms.Label endLoopLabel;
        private System.Windows.Forms.CheckBox loopCheckBox;
        private System.Windows.Forms.Label currentLabel;
        private System.Windows.Forms.NumericUpDown currentMilliUpDown;
        private System.Windows.Forms.NumericUpDown currentSecondUpDown;
        private System.Windows.Forms.NumericUpDown currentMinuteUpDown;
        private HoverLabel speedLabel;
        private HoverLabel volumeLabel;
        private System.Windows.Forms.Label volume100Label;
        private System.Windows.Forms.Label volume0Label;
        private HoverLabel positionLabel;
        private System.Windows.Forms.Label play0Label;
        private System.Windows.Forms.Label playDurationLabel;
        private PresetControl presetControl1;
        private PresetControl presetControl2;
        private PresetControl presetControl3;
        private PresetControl presetControl4;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tutorialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel presetPanel;
        private System.Windows.Forms.Timer resetBankTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer playTimeUpdateTimer;
        private System.Windows.Forms.ComboBox cueComboBox;
        private System.Windows.Forms.Label cueLabel;
        private System.Windows.Forms.Label volumeValueLabel;
        private System.Windows.Forms.Label speedValueLabel;
        private System.Windows.Forms.PictureBox cuePictureBox;
        private System.Windows.Forms.Panel loopPanel;
        private System.Windows.Forms.ToolStripStatusLabel filenameLabel;
        private System.Windows.Forms.Label vol50Label;
        private System.Windows.Forms.Label vol75Label;
        private System.Windows.Forms.Label vol25Label;
        private System.Windows.Forms.Label speed15XLabel;
        private System.Windows.Forms.Label speed05XLabel;
        private System.Windows.Forms.Label play2QDurationLabel;
        private System.Windows.Forms.Label play3QDurationLabel;
        private System.Windows.Forms.Label play1QDurationLabel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label pitchValueLabel;
        private HoverLabel pitchLabel;
        private System.Windows.Forms.Label pitch1Label;
        private System.Windows.Forms.Label pitchM1Label;
        private System.Windows.Forms.Label pitch0Label;
        private System.Windows.Forms.TrackBar pitchTrackBar;
        private System.Windows.Forms.ToolStripStatusLabel appStatusDescLabel;
        private System.Windows.Forms.ToolStripStatusLabel appStatusLabel;
        private System.Windows.Forms.Panel trackBarPanel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recent8ToolStripMenuItem;
        private FlickerFreePanel positionMarkersPanel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TrackBar hiEqTrackBar;
        private System.Windows.Forms.TrackBar medEqTrackBar;
        private System.Windows.Forms.TrackBar loEqTrackBar;
        private HoverLabel equalizerHoverLabel;
        private HoverLabel hiEqHoverLabel;
        private HoverLabel medEqHoverLabel;
        private HoverLabel loEqHoverLabel;
        private System.Windows.Forms.Label eqM100Label;
        private System.Windows.Forms.Label eq100Label;
        private System.Windows.Forms.Label eq0Label;
        private System.Windows.Forms.Label hiEqValueLabel;
        private System.Windows.Forms.Label medEqValueLabel;
        private System.Windows.Forms.Label loEqValueLabel;
        private System.Windows.Forms.ComboBox timeStretchProfileComboBox;
        private System.Windows.Forms.Label timeStretchProfileLabel;
        private PresetControl presetControl7;
        private PresetControl presetControl5;
        private PresetControl presetControl6;
        private PresetControl presetControl8;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyboardShortcutsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.Label label1;
        private UltraButton writePresetButton;
        private UltraButton resetPresetButton;
        private UltraButton playPauseButton;
        private UltraButton openFileButton;
        private UltraButton startLoopNowButton;
        private UltraButton endLoopNowButton;
        private System.Windows.Forms.Label cueSecondsLabel;
        private System.Windows.Forms.ToolStripMenuItem showTechLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox removeVocalsCheckBox;
        private System.Windows.Forms.ToolStripStatusLabel filenameDescLabel;
    }
}

