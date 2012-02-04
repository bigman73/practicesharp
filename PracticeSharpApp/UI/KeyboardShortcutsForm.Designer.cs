namespace BigMansStuff.PracticeSharp.UI
{
    partial class KeyboardShortcutsForm
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
            this.keyboardShortcutsPicturebox = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.keyboardShortcutsPicturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // keyboardShortcutsPicturebox
            // 
            this.keyboardShortcutsPicturebox.Location = new System.Drawing.Point(0, 0);
            this.keyboardShortcutsPicturebox.Name = "keyboardShortcutsPicturebox";
            this.keyboardShortcutsPicturebox.Size = new System.Drawing.Size(819, 358);
            this.keyboardShortcutsPicturebox.TabIndex = 0;
            this.keyboardShortcutsPicturebox.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Location = new System.Drawing.Point(368, 372);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // KeyboardShortcutsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 407);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.keyboardShortcutsPicturebox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KeyboardShortcutsForm";
            this.Text = "Keyboard Shortcuts";
            this.Load += new System.EventHandler(this.KeyboardShortcutsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.keyboardShortcutsPicturebox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox keyboardShortcutsPicturebox;
        private System.Windows.Forms.Button closeButton;
    }
}