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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.currentMilliUpDown = new System.Windows.Forms.NumericUpDown();
            this.currentSecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.currentMinuteUpDown = new System.Windows.Forms.NumericUpDown();
            this.playPauseButton = new System.Windows.Forms.Button();
            this.startLoopNowButton = new System.Windows.Forms.Button();
            this.endLoopNowButton = new System.Windows.Forms.Button();
            this.speedLabel = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            this.volume100Label = new System.Windows.Forms.Label();
            this.volume0Label = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            this.play0Label = new System.Windows.Forms.Label();
            this.playDurationLabel = new System.Windows.Forms.Label();
            this.positionMarkersPanel = new System.Windows.Forms.Panel();
            this.writeBankButton = new System.Windows.Forms.Button();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.playStatusToolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.filenameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.resetBankButton = new System.Windows.Forms.Button();
            this.presetPanel = new System.Windows.Forms.Panel();
            this.presetControl3 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl1 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl2 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.presetControl4 = new BigMansStuff.PracticeSharp.UI.PresetControl();
            this.resetBankTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.playTimeUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.cueComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.volumeValueLabel = new System.Windows.Forms.Label();
            this.speedValueLabel = new System.Windows.Forms.Label();
            this.cuePictureBox = new System.Windows.Forms.PictureBox();
            this.loopPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.play2QDurationLabel = new System.Windows.Forms.Label();
            this.play3QDurationLabel = new System.Windows.Forms.Label();
            this.play1QDurationLabel = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pitchValueLabel = new System.Windows.Forms.Label();
            this.pitchLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pitchTrackBar = new System.Windows.Forms.TrackBar();
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
            this.SuspendLayout();
            // 
            // tempoTrackBar
            // 
            this.tempoTrackBar.Location = new System.Drawing.Point(61, 36);
            this.tempoTrackBar.Maximum = 200;
            this.tempoTrackBar.Minimum = 10;
            this.tempoTrackBar.Name = "tempoTrackBar";
            this.tempoTrackBar.Size = new System.Drawing.Size(520, 45);
            this.tempoTrackBar.SmallChange = 5;
            this.tempoTrackBar.TabIndex = 1;
            this.tempoTrackBar.TickFrequency = 10;
            this.tempoTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tempoTrackBar.Value = 100;
            this.tempoTrackBar.ValueChanged += new System.EventHandler(this.speedTrackBar_ValueChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "MP3 files|*.mp3|WAV files|*.wav";
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.Location = new System.Drawing.Point(62, 167);
            this.volumeTrackBar.Maximum = 100;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(520, 45);
            this.volumeTrackBar.TabIndex = 5;
            this.volumeTrackBar.TickFrequency = 10;
            this.volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volumeTrackBar.Value = 25;
            this.volumeTrackBar.ValueChanged += new System.EventHandler(this.volumeTrackBar_ValueChanged);
            // 
            // speed1XLabel
            // 
            this.speed1XLabel.AutoSize = true;
            this.speed1XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed1XLabel.Location = new System.Drawing.Point(297, 20);
            this.speed1XLabel.Name = "speed1XLabel";
            this.speed1XLabel.Size = new System.Drawing.Size(23, 13);
            this.speed1XLabel.TabIndex = 4;
            this.speed1XLabel.Text = "X 1";
            this.speed1XLabel.Click += new System.EventHandler(this.speedLabel_Click);
            // 
            // playTimeTrackBar
            // 
            this.playTimeTrackBar.LargeChange = 0;
            this.playTimeTrackBar.Location = new System.Drawing.Point(63, 233);
            this.playTimeTrackBar.Maximum = 100;
            this.playTimeTrackBar.Name = "playTimeTrackBar";
            this.playTimeTrackBar.Size = new System.Drawing.Size(520, 45);
            this.playTimeTrackBar.SmallChange = 5;
            this.playTimeTrackBar.TabIndex = 7;
            this.playTimeTrackBar.TickFrequency = 5;
            this.playTimeTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.playTimeTrackBar.ValueChanged += new System.EventHandler(this.playTimeTrackBar_ValueChanged);
            this.playTimeTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.playPositionTrackBar_MouseDown);
            this.playTimeTrackBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.playTimeTrackBar_MouseMove);
            this.playTimeTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.playPositionTrackBar_MouseUp);
            // 
            // speed01XLabel
            // 
            this.speed01XLabel.AutoSize = true;
            this.speed01XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed01XLabel.Location = new System.Drawing.Point(61, 20);
            this.speed01XLabel.Name = "speed01XLabel";
            this.speed01XLabel.Size = new System.Drawing.Size(32, 13);
            this.speed01XLabel.TabIndex = 6;
            this.speed01XLabel.Text = "X 0.1";
            // 
            // speed2XLabel
            // 
            this.speed2XLabel.AutoSize = true;
            this.speed2XLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.speed2XLabel.Location = new System.Drawing.Point(556, 20);
            this.speed2XLabel.Name = "speed2XLabel";
            this.speed2XLabel.Size = new System.Drawing.Size(23, 13);
            this.speed2XLabel.TabIndex = 7;
            this.speed2XLabel.Text = "X 2";
            // 
            // startLoopMinuteUpDown
            // 
            this.startLoopMinuteUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopMinuteUpDown.Location = new System.Drawing.Point(123, 3);
            this.startLoopMinuteUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.startLoopMinuteUpDown.Name = "startLoopMinuteUpDown";
            this.startLoopMinuteUpDown.Size = new System.Drawing.Size(38, 20);
            this.startLoopMinuteUpDown.TabIndex = 10;
            this.startLoopMinuteUpDown.ValueChanged += new System.EventHandler(this.startLoopUpDown_ValueChanged);
            // 
            // startLoopSecondUpDown
            // 
            this.startLoopSecondUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopSecondUpDown.Location = new System.Drawing.Point(167, 3);
            this.startLoopSecondUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.startLoopSecondUpDown.Name = "startLoopSecondUpDown";
            this.startLoopSecondUpDown.Size = new System.Drawing.Size(38, 20);
            this.startLoopSecondUpDown.TabIndex = 11;
            this.startLoopSecondUpDown.ValueChanged += new System.EventHandler(this.startLoopUpDown_ValueChanged);
            // 
            // startLoopMilliUpDown
            // 
            this.startLoopMilliUpDown.BackColor = System.Drawing.Color.LimeGreen;
            this.startLoopMilliUpDown.Location = new System.Drawing.Point(211, 3);
            this.startLoopMilliUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.startLoopMilliUpDown.Name = "startLoopMilliUpDown";
            this.startLoopMilliUpDown.Size = new System.Drawing.Size(43, 20);
            this.startLoopMilliUpDown.TabIndex = 12;
            this.startLoopMilliUpDown.ValueChanged += new System.EventHandler(this.startLoopUpDown_ValueChanged);
            // 
            // endLoopMinuteUpDown
            // 
            this.endLoopMinuteUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopMinuteUpDown.Location = new System.Drawing.Point(123, 28);
            this.endLoopMinuteUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.endLoopMinuteUpDown.Name = "endLoopMinuteUpDown";
            this.endLoopMinuteUpDown.Size = new System.Drawing.Size(38, 20);
            this.endLoopMinuteUpDown.TabIndex = 13;
            this.endLoopMinuteUpDown.ValueChanged += new System.EventHandler(this.endLoopUpDown_ValueChanged);
            // 
            // endLoopSecondUpDown
            // 
            this.endLoopSecondUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopSecondUpDown.Location = new System.Drawing.Point(167, 28);
            this.endLoopSecondUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.endLoopSecondUpDown.Name = "endLoopSecondUpDown";
            this.endLoopSecondUpDown.Size = new System.Drawing.Size(38, 20);
            this.endLoopSecondUpDown.TabIndex = 14;
            this.endLoopSecondUpDown.ValueChanged += new System.EventHandler(this.endLoopUpDown_ValueChanged);
            // 
            // endLoopMilliUpDown
            // 
            this.endLoopMilliUpDown.BackColor = System.Drawing.Color.Turquoise;
            this.endLoopMilliUpDown.Location = new System.Drawing.Point(211, 28);
            this.endLoopMilliUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.endLoopMilliUpDown.Name = "endLoopMilliUpDown";
            this.endLoopMilliUpDown.Size = new System.Drawing.Size(43, 20);
            this.endLoopMilliUpDown.TabIndex = 15;
            this.endLoopMilliUpDown.ValueChanged += new System.EventHandler(this.endLoopUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Start Loop:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "End Loop:";
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.AutoSize = true;
            this.loopCheckBox.Location = new System.Drawing.Point(3, 16);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(50, 17);
            this.loopCheckBox.TabIndex = 18;
            this.loopCheckBox.Text = "Loop";
            this.loopCheckBox.UseVisualStyleBackColor = true;
            this.loopCheckBox.CheckedChanged += new System.EventHandler(this.loopCheckBox_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Current:";
            // 
            // currentMilliUpDown
            // 
            this.currentMilliUpDown.Location = new System.Drawing.Point(344, 1);
            this.currentMilliUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.currentMilliUpDown.Name = "currentMilliUpDown";
            this.currentMilliUpDown.Size = new System.Drawing.Size(43, 20);
            this.currentMilliUpDown.TabIndex = 4;
            this.currentMilliUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // currentSecondUpDown
            // 
            this.currentSecondUpDown.Location = new System.Drawing.Point(300, 1);
            this.currentSecondUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.currentSecondUpDown.Name = "currentSecondUpDown";
            this.currentSecondUpDown.Size = new System.Drawing.Size(38, 20);
            this.currentSecondUpDown.TabIndex = 3;
            this.currentSecondUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // currentMinuteUpDown
            // 
            this.currentMinuteUpDown.Location = new System.Drawing.Point(256, 1);
            this.currentMinuteUpDown.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.currentMinuteUpDown.Name = "currentMinuteUpDown";
            this.currentMinuteUpDown.Size = new System.Drawing.Size(38, 20);
            this.currentMinuteUpDown.TabIndex = 2;
            this.currentMinuteUpDown.ValueChanged += new System.EventHandler(this.currentUpDown_ValueChanged);
            // 
            // playPauseButton
            // 
            this.playPauseButton.ImageKey = "Play-Normal.png";
            this.playPauseButton.Location = new System.Drawing.Point(70, 11);
            this.playPauseButton.Name = "playPauseButton";
            this.playPauseButton.Size = new System.Drawing.Size(56, 56);
            this.playPauseButton.TabIndex = 1;
            this.playPauseButton.UseVisualStyleBackColor = true;
            this.playPauseButton.Click += new System.EventHandler(this.playPauseButton_Click);
            this.playPauseButton.MouseEnter += new System.EventHandler(this.playPauseButton_MouseEnter);
            this.playPauseButton.MouseLeave += new System.EventHandler(this.playPauseButton_MouseLeave);
            // 
            // startLoopNowButton
            // 
            this.startLoopNowButton.Location = new System.Drawing.Point(260, 1);
            this.startLoopNowButton.Name = "startLoopNowButton";
            this.startLoopNowButton.Size = new System.Drawing.Size(37, 23);
            this.startLoopNowButton.TabIndex = 24;
            this.startLoopNowButton.Text = "Now";
            this.startLoopNowButton.UseVisualStyleBackColor = true;
            this.startLoopNowButton.Click += new System.EventHandler(this.startLoopNowButton_Click);
            // 
            // endLoopNowButton
            // 
            this.endLoopNowButton.Location = new System.Drawing.Point(260, 26);
            this.endLoopNowButton.Name = "endLoopNowButton";
            this.endLoopNowButton.Size = new System.Drawing.Size(37, 23);
            this.endLoopNowButton.TabIndex = 25;
            this.endLoopNowButton.Text = "Now";
            this.endLoopNowButton.UseVisualStyleBackColor = true;
            this.endLoopNowButton.Click += new System.EventHandler(this.endLoopNowButton_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(7, 47);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(43, 13);
            this.speedLabel.TabIndex = 0;
            this.speedLabel.Text = "Speed";
            this.speedLabel.Click += new System.EventHandler(this.speedLabel_Click);
            // 
            // volumeLabel
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volumeLabel.Location = new System.Drawing.Point(8, 178);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(48, 13);
            this.volumeLabel.TabIndex = 4;
            this.volumeLabel.Text = "Volume";
            this.volumeLabel.Click += new System.EventHandler(this.volumeLabel_Click);
            // 
            // volume100Label
            // 
            this.volume100Label.AutoSize = true;
            this.volume100Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.volume100Label.Location = new System.Drawing.Point(553, 151);
            this.volume100Label.Name = "volume100Label";
            this.volume100Label.Size = new System.Drawing.Size(33, 13);
            this.volume100Label.TabIndex = 28;
            this.volume100Label.Text = "100%";
            // 
            // volume0Label
            // 
            this.volume0Label.AutoSize = true;
            this.volume0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.volume0Label.Location = new System.Drawing.Point(65, 151);
            this.volume0Label.Name = "volume0Label";
            this.volume0Label.Size = new System.Drawing.Size(21, 13);
            this.volume0Label.TabIndex = 29;
            this.volume0Label.Text = "0%";
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionLabel.Location = new System.Drawing.Point(8, 247);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(52, 13);
            this.positionLabel.TabIndex = 6;
            this.positionLabel.Text = "Position";
            this.positionLabel.Click += new System.EventHandler(this.positionLabel_Click);
            // 
            // play0Label
            // 
            this.play0Label.AutoSize = true;
            this.play0Label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play0Label.Location = new System.Drawing.Point(66, 217);
            this.play0Label.Name = "play0Label";
            this.play0Label.Size = new System.Drawing.Size(34, 13);
            this.play0Label.TabIndex = 31;
            this.play0Label.Text = "00:00";
            // 
            // playDurationLabel
            // 
            this.playDurationLabel.AutoSize = true;
            this.playDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.playDurationLabel.Location = new System.Drawing.Point(553, 217);
            this.playDurationLabel.Name = "playDurationLabel";
            this.playDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.playDurationLabel.TabIndex = 36;
            this.playDurationLabel.Text = "00:00";
            // 
            // positionMarkersPanel
            // 
            this.positionMarkersPanel.Location = new System.Drawing.Point(75, 234);
            this.positionMarkersPanel.Name = "positionMarkersPanel";
            this.positionMarkersPanel.Size = new System.Drawing.Size(492, 5);
            this.positionMarkersPanel.TabIndex = 37;
            this.positionMarkersPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.positionMarkersPanel_Paint);
            // 
            // writeBankButton
            // 
            this.writeBankButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeBankButton.ForeColor = System.Drawing.SystemColors.Control;
            this.writeBankButton.Location = new System.Drawing.Point(6, 3);
            this.writeBankButton.Name = "writeBankButton";
            this.writeBankButton.Size = new System.Drawing.Size(56, 56);
            this.writeBankButton.TabIndex = 0;
            this.writeBankButton.UseVisualStyleBackColor = true;
            this.writeBankButton.Click += new System.EventHandler(this.writePresetButton_Click);
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
            this.toolStripStatusLabel4,
            this.playStatusToolStripLabel,
            this.toolStripStatusLabel5,
            this.filenameToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 485);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(608, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 44;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusLabel4.Text = "Status:";
            // 
            // playStatusToolStripLabel
            // 
            this.playStatusToolStripLabel.Name = "playStatusToolStripLabel";
            this.playStatusToolStripLabel.Size = new System.Drawing.Size(26, 17);
            this.playStatusToolStripLabel.Text = "Idle";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabel5.Text = "File:";
            // 
            // filenameToolStripStatusLabel
            // 
            this.filenameToolStripStatusLabel.Name = "filenameToolStripStatusLabel";
            this.filenameToolStripStatusLabel.Size = new System.Drawing.Size(29, 17);
            this.filenameToolStripStatusLabel.Text = "N/A";
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
            // resetBankButton
            // 
            this.resetBankButton.Location = new System.Drawing.Point(6, 65);
            this.resetBankButton.Name = "resetBankButton";
            this.resetBankButton.Size = new System.Drawing.Size(56, 56);
            this.resetBankButton.TabIndex = 1;
            this.resetBankButton.UseVisualStyleBackColor = true;
            this.resetBankButton.Click += new System.EventHandler(this.resetBankButton_Click);
            this.resetBankButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resetBankButton_MouseDown);
            this.resetBankButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.resetBankButton_MouseUp);
            // 
            // presetPanel
            // 
            this.presetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.presetPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.presetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.presetPanel.Controls.Add(this.presetControl3);
            this.presetPanel.Controls.Add(this.resetBankButton);
            this.presetPanel.Controls.Add(this.presetControl1);
            this.presetPanel.Controls.Add(this.presetControl2);
            this.presetPanel.Controls.Add(this.writeBankButton);
            this.presetPanel.Controls.Add(this.presetControl4);
            this.presetPanel.Location = new System.Drawing.Point(14, 357);
            this.presetPanel.Name = "presetPanel";
            this.presetPanel.Size = new System.Drawing.Size(576, 125);
            this.presetPanel.TabIndex = 9;
            // 
            // presetControl3
            // 
            this.presetControl3.Id = "3";
            this.presetControl3.Location = new System.Drawing.Point(336, 3);
            this.presetControl3.Name = "presetControl3";
            presetData1.Cue = System.TimeSpan.Parse("00:00:00");
            presetData1.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData1.Description = "";
            presetData1.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData1.Loop = false;
            presetData1.Pitch = 0F;
            presetData1.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData1.Tempo = 1F;
            presetData1.Volume = 0.25F;
            this.presetControl3.PresetData = presetData1;
            this.presetControl3.PresetDescription = "";
            this.presetControl3.Size = new System.Drawing.Size(101, 118);
            this.presetControl3.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl3.TabIndex = 4;
            this.presetControl3.Title = "Preset 3";
            this.presetControl3.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl3.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl3.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl1
            // 
            this.presetControl1.Id = "1";
            this.presetControl1.Location = new System.Drawing.Point(83, 3);
            this.presetControl1.Name = "presetControl1";
            presetData2.Cue = System.TimeSpan.Parse("00:00:00");
            presetData2.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData2.Description = "";
            presetData2.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData2.Loop = false;
            presetData2.Pitch = 0F;
            presetData2.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData2.Tempo = 1F;
            presetData2.Volume = 0.25F;
            this.presetControl1.PresetData = presetData2;
            this.presetControl1.PresetDescription = "";
            this.presetControl1.Size = new System.Drawing.Size(101, 118);
            this.presetControl1.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl1.TabIndex = 2;
            this.presetControl1.Title = "Preset 1";
            this.presetControl1.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl1.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl1.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl2
            // 
            this.presetControl2.Id = "2";
            this.presetControl2.Location = new System.Drawing.Point(209, 3);
            this.presetControl2.Name = "presetControl2";
            presetData3.Cue = System.TimeSpan.Parse("00:00:00");
            presetData3.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData3.Description = "";
            presetData3.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData3.Loop = false;
            presetData3.Pitch = 0F;
            presetData3.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData3.Tempo = 1F;
            presetData3.Volume = 0.25F;
            this.presetControl2.PresetData = presetData3;
            this.presetControl2.PresetDescription = "";
            this.presetControl2.Size = new System.Drawing.Size(101, 118);
            this.presetControl2.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl2.TabIndex = 3;
            this.presetControl2.Title = "Preset 2";
            this.presetControl2.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl2.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl2.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // presetControl4
            // 
            this.presetControl4.Id = "4";
            this.presetControl4.Location = new System.Drawing.Point(464, 3);
            this.presetControl4.Name = "presetControl4";
            presetData4.Cue = System.TimeSpan.Parse("00:00:00");
            presetData4.CurrentPlayTime = System.TimeSpan.Parse("00:00:00");
            presetData4.Description = "";
            presetData4.EndMarker = System.TimeSpan.Parse("00:00:00");
            presetData4.Loop = false;
            presetData4.Pitch = 0F;
            presetData4.StartMarker = System.TimeSpan.Parse("00:00:00");
            presetData4.Tempo = 1F;
            presetData4.Volume = 0.25F;
            this.presetControl4.PresetData = presetData4;
            this.presetControl4.PresetDescription = "";
            this.presetControl4.Size = new System.Drawing.Size(101, 118);
            this.presetControl4.State = BigMansStuff.PracticeSharp.UI.PresetControl.PresetStates.Off;
            this.presetControl4.TabIndex = 5;
            this.presetControl4.Title = "Preset 4";
            this.presetControl4.PresetSelected += new System.EventHandler(this.presetControl_PresetSelected);
            this.presetControl4.PresetSaveSelected += new System.EventHandler(this.presetControl_PresetSaveSelected);
            this.presetControl4.PresetDescriptionChanged += new System.EventHandler(this.presetControl_PresetDescriptionChanged);
            // 
            // resetBankTimer
            // 
            this.resetBankTimer.Interval = 1000;
            this.resetBankTimer.Tick += new System.EventHandler(this.resetBankTimer_Tick);
            // 
            // openFileButton
            // 
            this.openFileButton.ImageKey = "Play-Normal.png";
            this.openFileButton.Location = new System.Drawing.Point(3, 11);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(56, 56);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.stopButton_Click);
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
            this.cueComboBox.Location = new System.Drawing.Point(355, 3);
            this.cueComboBox.Name = "cueComboBox";
            this.cueComboBox.Size = new System.Drawing.Size(42, 21);
            this.cueComboBox.TabIndex = 50;
            this.cueComboBox.SelectedValueChanged += new System.EventHandler(this.cueComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(326, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Cue";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Sec.";
            // 
            // volumeValueLabel
            // 
            this.volumeValueLabel.AutoSize = true;
            this.volumeValueLabel.Location = new System.Drawing.Point(18, 151);
            this.volumeValueLabel.Name = "volumeValueLabel";
            this.volumeValueLabel.Size = new System.Drawing.Size(10, 13);
            this.volumeValueLabel.TabIndex = 53;
            this.volumeValueLabel.Text = "-";
            // 
            // speedValueLabel
            // 
            this.speedValueLabel.AutoSize = true;
            this.speedValueLabel.Location = new System.Drawing.Point(18, 20);
            this.speedValueLabel.Name = "speedValueLabel";
            this.speedValueLabel.Size = new System.Drawing.Size(10, 13);
            this.speedValueLabel.TabIndex = 54;
            this.speedValueLabel.Text = "-";
            // 
            // cuePictureBox
            // 
            this.cuePictureBox.Image = global::BigMansStuff.PracticeSharp.UI.Resources.blue_off_16;
            this.cuePictureBox.Location = new System.Drawing.Point(308, 5);
            this.cuePictureBox.Name = "cuePictureBox";
            this.cuePictureBox.Size = new System.Drawing.Size(18, 18);
            this.cuePictureBox.TabIndex = 55;
            this.cuePictureBox.TabStop = false;
            // 
            // loopPanel
            // 
            this.loopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.loopPanel.Controls.Add(this.cueComboBox);
            this.loopPanel.Controls.Add(this.cuePictureBox);
            this.loopPanel.Controls.Add(this.label1);
            this.loopPanel.Controls.Add(this.label5);
            this.loopPanel.Controls.Add(this.startLoopMinuteUpDown);
            this.loopPanel.Controls.Add(this.startLoopSecondUpDown);
            this.loopPanel.Controls.Add(this.startLoopMilliUpDown);
            this.loopPanel.Controls.Add(this.endLoopMinuteUpDown);
            this.loopPanel.Controls.Add(this.endLoopSecondUpDown);
            this.loopPanel.Controls.Add(this.endLoopMilliUpDown);
            this.loopPanel.Controls.Add(this.label7);
            this.loopPanel.Controls.Add(this.label8);
            this.loopPanel.Controls.Add(this.loopCheckBox);
            this.loopPanel.Controls.Add(this.endLoopNowButton);
            this.loopPanel.Controls.Add(this.startLoopNowButton);
            this.loopPanel.Location = new System.Drawing.Point(132, 27);
            this.loopPanel.Name = "loopPanel";
            this.loopPanel.Size = new System.Drawing.Size(443, 54);
            this.loopPanel.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(310, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "50%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(427, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "75%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(188, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "25%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(425, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "X 1.5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label10.Location = new System.Drawing.Point(174, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 61;
            this.label10.Text = "X 0.5";
            // 
            // play2QDurationLabel
            // 
            this.play2QDurationLabel.AutoSize = true;
            this.play2QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play2QDurationLabel.Location = new System.Drawing.Point(293, 217);
            this.play2QDurationLabel.Name = "play2QDurationLabel";
            this.play2QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play2QDurationLabel.TabIndex = 62;
            this.play2QDurationLabel.Text = "00:00";
            // 
            // play3QDurationLabel
            // 
            this.play3QDurationLabel.AutoSize = true;
            this.play3QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play3QDurationLabel.Location = new System.Drawing.Point(425, 217);
            this.play3QDurationLabel.Name = "play3QDurationLabel";
            this.play3QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play3QDurationLabel.TabIndex = 63;
            this.play3QDurationLabel.Text = "00:00";
            // 
            // play1QDurationLabel
            // 
            this.play1QDurationLabel.AutoSize = true;
            this.play1QDurationLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.play1QDurationLabel.Location = new System.Drawing.Point(175, 217);
            this.play1QDurationLabel.Name = "play1QDurationLabel";
            this.play1QDurationLabel.Size = new System.Drawing.Size(34, 13);
            this.play1QDurationLabel.TabIndex = 64;
            this.play1QDurationLabel.Text = "00:00";
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.controlPanel.Controls.Add(this.loopPanel);
            this.controlPanel.Controls.Add(this.playPauseButton);
            this.controlPanel.Controls.Add(this.label9);
            this.controlPanel.Controls.Add(this.currentMilliUpDown);
            this.controlPanel.Controls.Add(this.currentMinuteUpDown);
            this.controlPanel.Controls.Add(this.openFileButton);
            this.controlPanel.Controls.Add(this.button1);
            this.controlPanel.Controls.Add(this.currentSecondUpDown);
            this.controlPanel.Location = new System.Drawing.Point(14, 266);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(575, 85);
            this.controlPanel.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label11.Location = new System.Drawing.Point(174, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "-0.5";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label12.Location = new System.Drawing.Point(425, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 71;
            this.label12.Text = "+0.5";
            // 
            // pitchValueLabel
            // 
            this.pitchValueLabel.AutoSize = true;
            this.pitchValueLabel.Location = new System.Drawing.Point(18, 84);
            this.pitchValueLabel.Name = "pitchValueLabel";
            this.pitchValueLabel.Size = new System.Drawing.Size(10, 13);
            this.pitchValueLabel.TabIndex = 70;
            this.pitchValueLabel.Text = "-";
            // 
            // pitchLabel
            // 
            this.pitchLabel.AutoSize = true;
            this.pitchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pitchLabel.Location = new System.Drawing.Point(7, 111);
            this.pitchLabel.Name = "pitchLabel";
            this.pitchLabel.Size = new System.Drawing.Size(36, 13);
            this.pitchLabel.TabIndex = 2;
            this.pitchLabel.Text = "Pitch";
            this.pitchLabel.Click += new System.EventHandler(this.pitchLabel_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label15.Location = new System.Drawing.Point(556, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "+1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label16.Location = new System.Drawing.Point(61, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 13);
            this.label16.TabIndex = 67;
            this.label16.Text = "-1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label17.Location = new System.Drawing.Point(316, 84);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 66;
            this.label17.Text = "0";
            // 
            // pitchTrackBar
            // 
            this.pitchTrackBar.LargeChange = 16;
            this.pitchTrackBar.Location = new System.Drawing.Point(61, 101);
            this.pitchTrackBar.Maximum = 96;
            this.pitchTrackBar.Minimum = -96;
            this.pitchTrackBar.Name = "pitchTrackBar";
            this.pitchTrackBar.Size = new System.Drawing.Size(520, 45);
            this.pitchTrackBar.SmallChange = 8;
            this.pitchTrackBar.TabIndex = 3;
            this.pitchTrackBar.TickFrequency = 8;
            this.pitchTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.pitchTrackBar.ValueChanged += new System.EventHandler(this.pitchTrackBar_ValueChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 507);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pitchValueLabel);
            this.Controls.Add(this.pitchLabel);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pitchTrackBar);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.play1QDurationLabel);
            this.Controls.Add(this.play3QDurationLabel);
            this.Controls.Add(this.play2QDurationLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.speedValueLabel);
            this.Controls.Add(this.volumeValueLabel);
            this.Controls.Add(this.presetPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.positionMarkersPanel);
            this.Controls.Add(this.playDurationLabel);
            this.Controls.Add(this.play0Label);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.volume0Label);
            this.Controls.Add(this.volume100Label);
            this.Controls.Add(this.volumeLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.speed2XLabel);
            this.Controls.Add(this.speed01XLabel);
            this.Controls.Add(this.playTimeTrackBar);
            this.Controls.Add(this.speed1XLabel);
            this.Controls.Add(this.volumeTrackBar);
            this.Controls.Add(this.tempoTrackBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Practice #";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox loopCheckBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown currentMilliUpDown;
        private System.Windows.Forms.NumericUpDown currentSecondUpDown;
        private System.Windows.Forms.NumericUpDown currentMinuteUpDown;
        private System.Windows.Forms.Button playPauseButton;
        private System.Windows.Forms.Button startLoopNowButton;
        private System.Windows.Forms.Button endLoopNowButton;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.Label volume100Label;
        private System.Windows.Forms.Label volume0Label;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.Label play0Label;
        private System.Windows.Forms.Label playDurationLabel;
        private System.Windows.Forms.Panel positionMarkersPanel;
        private PresetControl presetControl1;
        private PresetControl presetControl2;
        private PresetControl presetControl3;
        private PresetControl presetControl4;
        private System.Windows.Forms.Button writeBankButton;
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
        private System.Windows.Forms.Button resetBankButton;
        private System.Windows.Forms.Panel presetPanel;
        private System.Windows.Forms.Timer resetBankTimer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer playTimeUpdateTimer;
        private System.Windows.Forms.ComboBox cueComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label volumeValueLabel;
        private System.Windows.Forms.Label speedValueLabel;
        private System.Windows.Forms.PictureBox cuePictureBox;
        private System.Windows.Forms.Panel loopPanel;
        private System.Windows.Forms.ToolStripStatusLabel filenameToolStripStatusLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label play2QDurationLabel;
        private System.Windows.Forms.Label play3QDurationLabel;
        private System.Windows.Forms.Label play1QDurationLabel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label pitchValueLabel;
        private System.Windows.Forms.Label pitchLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar pitchTrackBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel playStatusToolStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
    }
}

