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
            System.Diagnostics.Process.Start("http://code.google.com/p/practicesharp/");
        }

        private void naudioLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://naudio.codeplex.com/");
        }

        private void soundTouchLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.surina.net/soundtouch/");

        }

        #endregion
    }
}
