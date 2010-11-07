namespace BigMansStuff.PracticeSharp.UI
{
    partial class PresetTextInputDialog
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.descLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(168, 52);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(66, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(101, 52);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(61, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(110, 11);
            this.textBox.MaxLength = 15;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(106, 20);
            this.textBox.TabIndex = 0;
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(12, 14);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(92, 13);
            this.descLabel.TabIndex = 3;
            this.descLabel.Text = "Enter Preset Text:";
            // 
            // PresetTextInputDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(240, 80);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PresetTextInputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Practice#";
            this.Load += new System.EventHandler(this.PresetTextInputDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label descLabel;
    }
}