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
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;
using NLog;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// Utility class for updating a new version of Practice#
    /// Performs a WebScrape of the Latest Version Wiki Page on Practice# Google Code site,
    ///   and compares to the currently installed version
    /// </summary>
    internal class VersionUpdater
    {
        #region Logger
        private static Logger m_logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Construction
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mainForm"></param>
        /// <param name="installedVersion"></param>
        public VersionUpdater(Form mainForm, Version installedVersion)
        {
            m_mainForm = mainForm;
            m_installedVersion = installedVersion;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Checks if a new Version of Practice# has been released
        /// </summary>
        public void CheckNewVersion()
        {
            m_worker = new BackgroundWorker();
            m_worker.DoWork += new DoWorkEventHandler(CheckNewVersionAsync_DoWork);
            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
            m_worker.RunWorkerAsync();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Do the actual work of checking for a new version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckNewVersionAsync_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Version newVersion;
                DateTime lastVersionCheckDateTime = Properties.Settings.Default.LastVersionCheckDateTime;

                DateTime nextCheckDateTime;
                if (lastVersionCheckDateTime == DateTime.MinValue)
                {
                    lastVersionCheckDateTime = DateTime.Now;
                    nextCheckDateTime = DateTime.MinValue;
                }
                else
                {
                    if (Properties.Settings.Default.SupressVersionCheck)
                    {
                        // Supressed Mode - Checks once a month
                        nextCheckDateTime = lastVersionCheckDateTime.AddMonths(1);
                    }
                    else
                    {
                        // Regular mode - Checks once a week
                        nextCheckDateTime = lastVersionCheckDateTime.AddDays(7);
                    }
                }

                if ( lastVersionCheckDateTime != DateTime.MinValue && DateTime.Now >= nextCheckDateTime)
                {
                    Properties.Settings.Default.SupressVersionCheck = false;
                    Properties.Settings.Default.Save();
                
                    // Check if the new version is actually newer than the installed version
                    if (IsNewVersionReleased(out newVersion))
                    {
                        DialogResult dialogResult = DialogResult.None;
                        if (m_mainForm.Disposing || m_mainForm.IsDisposed)
                            return;
                        m_mainForm.Invoke( new MethodInvoker( delegate() 
                        {
                            dialogResult = MessageBox.Show(m_mainForm, string.Format(Resources.NewVersionFound, newVersion.ToString()), "Practice#", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        } ) );

                        if ( dialogResult == DialogResult.Yes )
                        {
                            // Launch the Practice# GoogleCode Downloads web page
                            System.Diagnostics.Process.Start( DownloadsWebPageURL );
                        }
                        else
                        {
                            Properties.Settings.Default.SupressVersionCheck = true;
                        }
                        Properties.Settings.Default.LastVersionCheckDateTime = DateTime.Now;
                        Properties.Settings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                // Ignore errors, we're just checking for a new version - the internet connection might be off
                m_logger.ErrorException("Failed getting application version from the internet", ex);
            }
        }

        /// <summary>
        /// RunWorkerComplete event handler - Called when the Background Worker has completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Worker has completed - clean up members
            m_mainForm = null;
            m_worker = null;
            m_installedVersion = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Utility method that checks if a new version of Practice# has been released
        /// </summary>
        /// <param name="installedVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private bool IsNewVersionReleased( out Version newVersion )
        {
            newVersion = null;

            // Web Scrape   Google Code LatestVersion wiki page
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LatestVersionWebPageURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader htmlStream = new StreamReader(responseStream, Encoding.UTF8))
            {
                string pageHtml = htmlStream.ReadToEnd();

                // Match the latest version value by using a regular expression
                Regex regEx = new Regex(@"Practice# latest Version is: (\d+.\d+.\d+.\d+)");
                Match match = regEx.Match(pageHtml);

                if (match != null)
                {
                    newVersion = new Version(match.Groups[1].Value);
                    if (newVersion > m_installedVersion)
                    {
                        return true;
                    }
                }
            }
                
            return false; 
        }

        #endregion

        #region Private Members

        private Form m_mainForm;
        private Version m_installedVersion;
        private BackgroundWorker m_worker;
    
        #endregion

        #region Constants

        private const string DownloadsWebPageURL = "http://code.google.com/p/practicesharp/downloads/list";
        private const string LatestVersionWebPageURL = "http://practicesharp.googlecode.com/svn/wiki/LatestVersion.wiki";

        #endregion
    }
}
