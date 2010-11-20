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

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// A dialog form that allows entering the preset's text description
    /// </summary>
    public partial class PresetTextInputDialog : Form
    {
        #region Construction

        public PresetTextInputDialog()
        {
            InitializeComponent();
        }

        private void PresetTextInputDialog_Load(object sender, EventArgs e)
        {
            textBox.Text = PresetText;
            textBox.SelectAll();
        }

        #endregion
        
        #region Properties

        public string PresetText { get; set; }

        #endregion

        #region Event Handlers

        /// <summary>
        /// OK Click Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            // Update the PresetText property
            PresetText = textBox.Text;
        }

        #endregion
    }
}
