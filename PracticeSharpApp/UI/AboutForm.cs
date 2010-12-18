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
    /// Practice# About Form
    /// </summary>
    public partial class AboutForm : Form
    {
        #region Construction

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Load Event Handler - Initialized form after it has been loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_Load(object sender, EventArgs e)
        {
            thisVersionLabel.Text = AppVersion.ToString();
        }

        #endregion

        #region Properties

        public Version AppVersion { get; set; }

        #endregion

        #region GUI Event Handlers

        private void googleCodeProjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite("http://code.google.com/p/practicesharp/");
        }

        private void codeProjectLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite("http://www.codeproject.com/KB/audio-video/practice_sharp.aspx");
        }

        private void naudioLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite("http://naudio.codeplex.com/");
        }

        private void soundTouchLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite("http://www.surina.net/soundtouch/");
        }

        private void csVorbisLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchWebSite("https://github.com/mono/csvorbis/");
        }
        
        #endregion

        #region Privte Methods

        /// <summary>
        /// Utility function - launches the web site url in the default browser
        /// </summary>
        /// <param name="webSiteUrl"></param>
        private void LaunchWebSite(string webSiteUrl)
        {
            System.Diagnostics.Process.Start(webSiteUrl);
        }


        #endregion
    }
}
