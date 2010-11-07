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
    public partial class PresetControl : UserControl
    {
        public PresetControl()
        {
            InitializeComponent();
        }

        private void PresetControl_Load(object sender, EventArgs e)
        {
            ChangeState(PresetStates.Off);
            PresetData = new PresetData();
            presetDescLabel.Text = Resources.PresetNoDesc;
        }

        public void Reset()
        {
            PresetData.Reset();

            BlinkLed(Resources.amber_off_16, Resources.amber_on_16, 4, 50);
            ShowSelectedLed();

            PresetDescription = string.Empty;
        }

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
                return presetButton.Text;
            }

            set
            {
                presetButton.Text = value;
            }
        }

        [BrowsableAttribute(true)]
        public string Id
        {
            get;
            set;
        }

        public PresetData PresetData
        {
            get;
            set;
        }

        private void ChangeState(PresetStates value)
        {
            m_state = value;

            switch (m_state)
            {
                case PresetStates.Selected:
                {
                    ShowSelectedLed();
                    if (PresetSelected != null)
                    {
                        PresetSelected(this, EventArgs.Empty);
                    }
                   
                    break;
                }
                case PresetStates.WaitForSave:
                {
                    ledPictureBox.Image = Resources.red_on_16;

                    break;
                }
                case PresetStates.Saving:
                {
                    BlinkLed(Resources.red_off_16, Resources.red_on_16, 2, 200);

                    if (PresetSaveSelected != null)
                    {
                        PresetSaveSelected(this, EventArgs.Empty);
                    }

                    this.State = PresetControl.PresetStates.Selected;

                    break;
                }

                default:
                    ShowRegularLed();
                    break;
            }

            
        }

        public enum PresetStates { Off, Selected, WaitForSave, Saving };

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

        public event EventHandler PresetSelected;
        public event EventHandler PresetSaveSelected;

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

        private void ShowRegularLed()
        {
            ledPictureBox.Image = Resources.green_off_16;
        }

        private void ShowSelectedLed()
        {
            ledPictureBox.Image = Resources.green_on_16;
        }

        private PresetStates m_state;

        private void presetDescLabel_Click(object sender, EventArgs e)
        {
            PresetTextInputDialog inputDialog = new PresetTextInputDialog();
            if ( presetDescLabel.Tag != null )
            {
                inputDialog.PresetText = presetDescLabel.Text;
            }

            if (DialogResult.OK == inputDialog.ShowDialog(this))
            {
                PresetDescription = inputDialog.PresetText.Trim();
            }
        }

        public string PresetDescription
        {
            get
            {
                if (presetDescLabel.Tag != null)
                    return presetDescLabel.Text;
                else
                    return string.Empty;
            }
            set
            {
                if (value == string.Empty)
                {
                    presetDescLabel.Text = Resources.PresetNoDesc;
                    presetDescLabel.Tag = null;
                }
                else
                {
                    presetDescLabel.Text = value;
                    presetDescLabel.Tag = "HasValue";
                }
            }
        }
    }
}
