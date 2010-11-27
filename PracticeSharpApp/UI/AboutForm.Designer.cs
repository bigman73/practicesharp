namespace BigMansStuff.PracticeSharp.UI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.closeButton = new System.Windows.Forms.Button();
            this.googleCodeProjectLinkLabel = new System.Windows.Forms.LinkLabel();
            this.aboutLabel = new System.Windows.Forms.Label();
            this.naudioLinkLabel = new System.Windows.Forms.LinkLabel();
            this.soundTouchLinkLabel = new System.Windows.Forms.LinkLabel();
            this.creditsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.thisVersionLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.codeProjectLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(137, 248);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // googleCodeProjectLinkLabel
            // 
            this.googleCodeProjectLinkLabel.AutoSize = true;
            this.googleCodeProjectLinkLabel.Location = new System.Drawing.Point(12, 121);
            this.googleCodeProjectLinkLabel.Name = "googleCodeProjectLinkLabel";
            this.googleCodeProjectLinkLabel.Size = new System.Drawing.Size(126, 13);
            this.googleCodeProjectLinkLabel.TabIndex = 1;
            this.googleCodeProjectLinkLabel.TabStop = true;
            this.googleCodeProjectLinkLabel.Text = "Google Code Project Site";
            this.googleCodeProjectLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.googleCodeProjectLinkLabel_LinkClicked);
            // 
            // aboutLabel
            // 
            this.aboutLabel.Location = new System.Drawing.Point(12, 10);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(157, 58);
            this.aboutLabel.TabIndex = 2;
            this.aboutLabel.Text = "Practice# is a Windows tool for practicing playing an instrument with playback mu" +
                "sic. It will turn your playing skills sharper!";
            // 
            // naudioLinkLabel
            // 
            this.naudioLinkLabel.AutoSize = true;
            this.naudioLinkLabel.Location = new System.Drawing.Point(12, 186);
            this.naudioLinkLabel.Name = "naudioLinkLabel";
            this.naudioLinkLabel.Size = new System.Drawing.Size(42, 13);
            this.naudioLinkLabel.TabIndex = 3;
            this.naudioLinkLabel.TabStop = true;
            this.naudioLinkLabel.Text = "NAudio";
            this.naudioLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.naudioLinkLabel_LinkClicked);
            // 
            // soundTouchLinkLabel
            // 
            this.soundTouchLinkLabel.AutoSize = true;
            this.soundTouchLinkLabel.Location = new System.Drawing.Point(12, 199);
            this.soundTouchLinkLabel.Name = "soundTouchLinkLabel";
            this.soundTouchLinkLabel.Size = new System.Drawing.Size(69, 13);
            this.soundTouchLinkLabel.TabIndex = 4;
            this.soundTouchLinkLabel.TabStop = true;
            this.soundTouchLinkLabel.Text = "SoundTouch";
            this.soundTouchLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.soundTouchLinkLabel_LinkClicked);
            // 
            // creditsLabel
            // 
            this.creditsLabel.AutoSize = true;
            this.creditsLabel.Location = new System.Drawing.Point(12, 173);
            this.creditsLabel.Name = "creditsLabel";
            this.creditsLabel.Size = new System.Drawing.Size(42, 13);
            this.creditsLabel.TabIndex = 5;
            this.creditsLabel.Text = "Credits:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "This Version:";
            // 
            // thisVersionLabel
            // 
            this.thisVersionLabel.AutoSize = true;
            this.thisVersionLabel.Location = new System.Drawing.Point(86, 97);
            this.thisVersionLabel.Name = "thisVersionLabel";
            this.thisVersionLabel.Size = new System.Drawing.Size(27, 13);
            this.thisVersionLabel.TabIndex = 7;
            this.thisVersionLabel.Text = "N/A";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BigMansStuff.PracticeSharp.UI.Resources.PracticeSharp;
            this.pictureBox1.Location = new System.Drawing.Point(190, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 143);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BigMansStuff.PracticeSharp.UI.Resources.lgplv3_147x51;
            this.pictureBox2.Location = new System.Drawing.Point(190, 174);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(131, 60);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(172, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "© 2010, Yuval Naveh";
            // 
            // codeProjectLinkLabel
            // 
            this.codeProjectLinkLabel.AutoSize = true;
            this.codeProjectLinkLabel.Location = new System.Drawing.Point(12, 139);
            this.codeProjectLinkLabel.Name = "codeProjectLinkLabel";
            this.codeProjectLinkLabel.Size = new System.Drawing.Size(89, 13);
            this.codeProjectLinkLabel.TabIndex = 11;
            this.codeProjectLinkLabel.TabStop = true;
            this.codeProjectLinkLabel.Text = "Code Project Site";
            this.codeProjectLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.codeProjectLinkLabel_LinkClicked);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(338, 283);
            this.Controls.Add(this.codeProjectLinkLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.thisVersionLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.creditsLabel);
            this.Controls.Add(this.soundTouchLinkLabel);
            this.Controls.Add(this.naudioLinkLabel);
            this.Controls.Add(this.aboutLabel);
            this.Controls.Add(this.googleCodeProjectLinkLabel);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "About Practice#";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.LinkLabel googleCodeProjectLinkLabel;
        private System.Windows.Forms.Label aboutLabel;
        private System.Windows.Forms.LinkLabel naudioLinkLabel;
        private System.Windows.Forms.LinkLabel soundTouchLinkLabel;
        private System.Windows.Forms.Label creditsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label thisVersionLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel codeProjectLinkLabel;
    }
}