using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BigMansStuff.PracticeSharp.UI
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public Version AppVersion { get; set; }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            thisVersionLabel.Text = AppVersion.ToString();
        }

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

    }
}
