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
    public partial class PresetTextInputDialog : Form
    {
        public PresetTextInputDialog()
        {
            InitializeComponent();
        }

        public string PresetText { get; set; }

        private void okButton_Click(object sender, EventArgs e)
        {
            PresetText = textBox.Text;
        }

        private void PresetTextInputDialog_Load(object sender, EventArgs e)
        {
            textBox.Text = PresetText;
            textBox.SelectAll();
        }
    }
}
