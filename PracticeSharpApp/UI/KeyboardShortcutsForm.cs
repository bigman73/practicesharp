using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// A dialog that shows the keyboard shortctus image
    /// </summary>
    public partial class KeyboardShortcutsForm : Form
    {
        #region Initialization

        /// <summary>
        /// Constructor
        /// </summary>
        public KeyboardShortcutsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardShortcutsForm_Load(object sender, EventArgs e)
        {
            keyboardShortcutsPicturebox.Image = Resources.KeyboardShortcuts;
        }

        #endregion
    }
}
