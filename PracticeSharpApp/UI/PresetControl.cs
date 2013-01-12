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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BigMansStuff.PracticeSharp.Core;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// PresetControl contains the UI and logic needed for a preset "switch" button
    /// The user interface concept is similar to commerical sound effect modules (e.g. Boss ME-70)
    /// </summary>
    public partial class PresetControl : UserControl
    {
        #region Construction

        /// <summary>
        /// Default constructor
        /// </summary>
        public PresetControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load event - initializes the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetControl_Load(object sender, EventArgs e)
        {
            ChangeState(PresetStates.Off);
            PresetData = new PresetData();
            presetIdLabel.Text = this.Id.ToString();
            presetIdLabel.ForeColor = LABEL_INACTIVE_COLOR;
            presetIdLabel.HoverColor = LABEL_HOVER_COLOR;
            presetIdLabel.RegularColor = LABEL_INACTIVE_COLOR;

            presetButton.ButtonText = Resources.PresetNoDesc;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets the control's state to default
        /// </summary>
        public void Reset( bool animate )
        {
            PresetData.Reset();

            if (animate)
            {
                BlinkLed(Resources.amber_off_16, Resources.amber_on_16, 4, 50);
            }

            ShowRegularLed();

            PresetDescription = string.Empty;
        }

        /// <summary>
        /// Invokes changing of the Preset's description
        /// </summary>
        public void ChangeDescription()
        {
            PresetTextInputDialog inputDialog = new PresetTextInputDialog();
            if (presetButton.Tag != null)
            {
                inputDialog.PresetText = presetButton.ButtonText;
            }

            if (DialogResult.OK == inputDialog.ShowDialog(this))
            {
                PresetDescription = inputDialog.PresetText.Trim();
            }

            // Raise a save event - Renaming the description of a preset should be persisted immediately
            if (PresetDescriptionChanged != null)
            {
                PresetDescriptionChanged(this, new EventArgs());
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property for getting/setting the Preset State
        /// </summary>
        public PresetStates State
        {
            get
            {
                return m_state;
            }

            set
            {
                ChangeState(value);

            }
        }

        [BrowsableAttribute(true)]
        public string Title
        {
            get
            {
                return presetButton.ButtonText;
            }

            set
            {
                presetButton.ButtonText = value;
            }
        }

        [BrowsableAttribute(true)]
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Property for Preset Description
        /// </summary>
        public string PresetDescription
        {
            get
            {
                if (presetButton.Tag != null)
                    return presetButton.ButtonText;
                else
                    return string.Empty;
            }
            set
            {
                PresetData.Description = value;
                if (value == string.Empty)
                {
                    presetButton.ButtonText = Resources.PresetNoDesc;
                    presetButton.Tag = null;
                }
                else
                {
                    presetButton.ButtonText = value;
                    presetButton.Tag = "HasValue";
                }
            }
        }

        /// <summary>
        /// Property for the Preset Data that contains the actual preset values
        /// </summary>
        public PresetData PresetData
        {
            get;
            set;
        }

        public enum PresetStates { Off, Selected, WaitForSave, Saving };
        
        #endregion  

        #region Events

        public event EventHandler PresetSelected;
        public event EventHandler PresetSaveSelected;
        public event EventHandler PresetDescriptionChanged;

        #endregion

        #region Event Handlers

        /// <summary>
        /// Click event handler for the preset button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presetButton_Click(object sender, EventArgs e)
        {
            if ( m_state == PresetStates.Off || m_state == PresetStates.Selected )
                // Note: Allow Re-Select to revert back to Preset saved values
                ChangeState(PresetStates.Selected);
            else if (m_state == PresetStates.WaitForSave)
            {
                ChangeState(PresetStates.Saving);
            }
        }

        /// <summary>
        /// Click event handler for Preset Id label - Allows changing the description of a preset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void presetIdLabel_Click(object sender, EventArgs e)
        {
            ChangeDescription();
        }

        /// <summary>
        /// Layout event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PresetControl_Layout(object sender, LayoutEventArgs e)
        {
            //ledPictureBox.Left = presetButton.Right + 1;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Blinks the preset led for several times (defined by blinkCount) with a pause duration (defined by duration)
        /// </summary>
        /// <param name="image1"></param>
        /// <param name="image2"></param>
        /// <param name="blinkCount"></param>
        /// <param name="duration"></param>
        private void BlinkLed(Image image1, Image image2, int blinkCount, int duration)
        {
            for (int i = 0; i < blinkCount; i++)
            {
                ledPictureBox.Image = image1;
                Application.DoEvents();
                Thread.Sleep(duration);

                ledPictureBox.Image = image2;
                Application.DoEvents();
                Thread.Sleep(duration);
            }
        }

        /// <summary>
        /// Shows the regular led
        /// </summary>
        private void ShowRegularLed()
        {
            ledPictureBox.Image = Resources.red_off_16;
        }

        /// <summary>
        /// Shows the selected led
        /// </summary>
        private void ShowSelectedLed()
        {
            ledPictureBox.Image = Resources.red_on_16;
        }

        /// <summary>
        /// Changes states of the Preset Control
        /// </summary>
        /// <param name="value"></param>
        private void ChangeState(PresetStates value)
        {
            PresetStates prevState = m_state;
            m_state = value;

            switch (m_state)
            {
                case PresetStates.Selected:
                    {
                        ShowSelectedLed();

                        presetIdLabel.ForeColor = LABEL_ACTIVE_COLOR;
                        presetIdLabel.RegularColor = LABEL_ACTIVE_COLOR;

                        // Don't fire event in case this is a Cancel of WaitForSave
                        if (prevState == PresetStates.WaitForSave)
                        {
                            break;
                        }

                        if (PresetSelected != null)
                        {
                            PresetSelected(this, EventArgs.Empty);
                        }

                        break;
                    }
                case PresetStates.WaitForSave:
                    {
                        ledPictureBox.Image = Resources.amber_on_16;


                        break;
                    }
                case PresetStates.Saving:
                    {
                        BlinkLed(Resources.red_off_16, Resources.amber_on_16, 2, 200);

                        if (PresetSaveSelected != null)
                        {
                            PresetSaveSelected(this, EventArgs.Empty);
                        }

                        this.State = PresetControl.PresetStates.Selected;

                        break;
                    }

                default:
                    ShowRegularLed();
                    presetIdLabel.ForeColor = LABEL_INACTIVE_COLOR;
                    presetIdLabel.RegularColor = LABEL_INACTIVE_COLOR;

                    break;
            }
        }
    
        #endregion

        #region Private members

        private PresetStates m_state;

        #endregion     

        #region Constants
        private static Color LABEL_ACTIVE_COLOR = Color.FromArgb(0xFBB063);
        private static Color LABEL_INACTIVE_COLOR = Color.Black;
        private static Color LABEL_HOVER_COLOR = Color.LightBlue;
        #endregion

    }
}
