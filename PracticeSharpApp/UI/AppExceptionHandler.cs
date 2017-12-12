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
using NLog;
using System.Windows.Forms;
using System.Threading;
using NLog.Config;
using NLog.Targets;
using NLog.Layouts;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// Central Application Exception Handler 
    /// Catches and handles unhandled exceptions that might occur in the application
    /// </summary>
    static class AppExceptionHandler
    {
        #region Logger
        private static Logger m_logger = LogManager.GetCurrentClassLogger();
        #endregion    

        #region Initialization

        /// <summary>
        /// Initialize the application central exception handlers
        /// </summary>
        public static void InitializeExceptionHandling()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledException);

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        }

        #endregion

        #region Exception Handling

        /// <summary>
        /// Central Application Domain Unhandled Exception Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            m_logger.Fatal(ex, "AppDomain - Unhandled Exception");

            HandleUnhandledException(ex);
        }
    
        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
        {
            m_logger.Fatal(t.Exception, "Application - Thread Exception" + t.Exception.ToString());

            HandleUnhandledException(t.Exception);
        }

        /// <summary>
        /// Creates the error message and displays it.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            //string logFilename = LogManager.Configuration.AllTargets[0].
            LoggingConfiguration config = LogManager.Configuration; 
            FileTarget standardTarget = config.FindTargetByName("logfile") as FileTarget;
            string logFilename = string.Empty;
            if ( standardTarget != null )
            {
                logFilename = SimpleLayout.Evaluate(standardTarget.FileName.ToString());
            }

           string errorMsg = string.Format(
                        "An application error occurred.\nDescription: {0}, {1}\n\nFull details are available in the log file: {2}\n\nPlease contact the author at {3}.",
                        e.GetType().Name, e.Message, logFilename, "http://code.google.com/p/practicesharp/issues/list" );
            
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Generic handler for unhandled exceptions - Show a message error and allows the user to Abort/Retry/Ignore 
        /// </summary>
        /// <param name="ex"></param>
        private static void HandleUnhandledException(Exception ex)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = ShowThreadExceptionDialog("Practice# Error", ex);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Practice# Error",
                        "Fatal Practice# Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        #endregion
    }
}
