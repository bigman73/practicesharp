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
            this.presetButton = new System.Windows.Forms.Button();
            this.presetDescLabel = new System.Windows.Forms.Label();
            this.ledPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ledPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // presetButton
            // 
            this.presetButton.Location = new System.Drawing.Point(18, 21);
            this.presetButton.Name = "presetButton";
            this.presetButton.Size = new System.Drawing.Size(64, 85);
            this.presetButton.TabIndex = 44;
            this.presetButton.Text = "Preset 1";
            this.presetButton.UseVisualStyleBackColor = true;
            this.presetButton.Click += new System.EventHandler(this.presetButton_Click);
            // 
            // presetDescLabel
            // 
            this.presetDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.presetDescLabel.Location = new System.Drawing.Point(3, 106);
            this.presetDescLabel.Name = "presetDescLabel";
            this.presetDescLabel.Size = new System.Drawing.Size(97, 12);
            this.presetDescLabel.TabIndex = 47;
            this.presetDescLabel.Text = "[No Desc]";
            this.presetDescLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.presetDescLabel.Click += new System.EventHandler(this.presetDescLabel_Click);
            // 
            // ledPictureBox
            // 
            this.ledPictureBox.Location = new System.Drawing.Point(42, 3);
            this.ledPictureBox.Name = "ledPictureBox";
            this.ledPictureBox.Size = new System.Drawing.Size(16, 16);
            this.ledPictureBox.TabIndex = 45;
            this.ledPictureBox.TabStop = false;
            // 
            // PresetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.presetDescLabel);
            this.Controls.Add(this.ledPictureBox);
            this.Controls.Add(this.presetButton);
            this.Name = "PresetControl";
            this.Size = new System.Drawing.Size(103, 121);
            this.Load += new System.EventHandler(this.PresetControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ledPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ledPictureBox;
        private System.Windows.Forms.Button presetButton;
        private System.Windows.Forms.Label presetDescLabel;
    }
}
