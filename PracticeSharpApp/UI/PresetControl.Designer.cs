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
    partial class PresetControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.presetIdLabel = new BigMansStuff.PracticeSharp.UI.HoverLabel();
            this.presetButton = new BigMansStuff.PracticeSharp.UI.UltraButton();
            this.ledPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ledPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // presetIdLabel
            // 
            this.presetIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.presetIdLabel.AutoSize = true;
            this.presetIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.presetIdLabel.Location = new System.Drawing.Point(80, 25);
            this.presetIdLabel.Name = "presetIdLabel";
            this.presetIdLabel.Size = new System.Drawing.Size(31, 33);
            this.presetIdLabel.TabIndex = 48;
            this.presetIdLabel.Text = "1";
            this.presetIdLabel.Click += new System.EventHandler(this.presetIdLabel_Click);
            // 
            // presetButton
            // 
            this.presetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.presetButton.BackColor = System.Drawing.Color.Transparent;
            this.presetButton.ButtonText = "";
            this.presetButton.CornerRadius = 2;
            this.presetButton.GlowColor = System.Drawing.Color.Red;
            this.presetButton.ImageSize = new System.Drawing.Size(16, 16);
            this.presetButton.Location = new System.Drawing.Point(1, 1);
            this.presetButton.Name = "presetButton";
            this.presetButton.Size = new System.Drawing.Size(82, 58);
            this.presetButton.TabIndex = 49;
            this.presetButton.Click += new System.EventHandler(this.presetButton_Click);
            // 
            // ledPictureBox
            // 
            this.ledPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ledPictureBox.Location = new System.Drawing.Point(87, 5);
            this.ledPictureBox.Name = "ledPictureBox";
            this.ledPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ledPictureBox.TabIndex = 50;
            this.ledPictureBox.TabStop = false;
            // 
            // PresetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ledPictureBox);
            this.Controls.Add(this.presetButton);
            this.Controls.Add(this.presetIdLabel);
            this.Name = "PresetControl";
            this.Size = new System.Drawing.Size(103, 60);
            this.Load += new System.EventHandler(this.PresetControl_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.PresetControl_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.ledPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HoverLabel presetIdLabel;
        private UltraButton presetButton;
        private System.Windows.Forms.PictureBox ledPictureBox;
    }
}
