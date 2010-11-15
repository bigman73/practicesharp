using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// Utility class for updating a new version of Practice#
    /// </summary>
    static class VersionUpdater
    {
        /// <summary>
        /// Checks if a new Version of Practice# has been released
        /// </summary>
        public static void CheckNewVersion( Version installedVersion )
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

            if (
                (lastVersionCheckDateTime != DateTime.MinValue && DateTime.Now >= nextCheckDateTime))
            {
                Properties.Settings.Default.LastVersionCheckDateTime = DateTime.Now;

                if (VersionUpdater.IsNewVersionReleased(installedVersion, out newVersion))
                {
                    if (DialogResult.Yes == MessageBox.Show(string.Format(Resources.NewVersionFound, newVersion.ToString()), "Practice#", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        System.Diagnostics.Process.Start("http://code.google.com/p/practicesharp/downloads/list");
                    }
                    else
                    {
                        Properties.Settings.Default.SupressVersionCheck = true;
                    }
                }
            }

        }

        /// <summary>
        /// Utility method that checks if a new version of Practice# has been released
        /// </summary>
        /// <param name="installedVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        public static bool IsNewVersionReleased( Version installedVersion, out Version newVersion )
        {
            newVersion = null;

            try
            {
                // Web Scrape the Google Code LatestVersion wiki page
                string target = @"http://code.google.com/p/practicesharp/wiki/LatestVersion";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(target);
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
                        if (newVersion > installedVersion)
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception)
            {
                // Ignore errors, we're just checking for a new version - the internet connection might be off
            }

            return false; 
        }
    }
}
