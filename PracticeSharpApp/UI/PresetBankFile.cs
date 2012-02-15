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
using System.Xml;
using System.IO;
using BigMansStuff.PracticeSharp.Core;
using System.Windows.Forms;
using NLog;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// Helper class - Handles reading and writing of Presets Bank XML File
    /// </summary>
    class PresetBankFile
    {
        #region Logger
        private static Logger m_logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Construction
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appDataFolder"></param>
        /// <param name="appVersion"></param>
        /// <param name="currentAudioFilename"></param>
        public PresetBankFile( string appDataFolder, string appVersion, string currentAudioFilename )
        {
            m_appDataFolder = appDataFolder;
            m_appVersion = appVersion;
            m_currentAudioFilename = currentAudioFilename;
            m_presetsBankFilename = m_appDataFolder + "\\" + Path.GetFileName(m_currentAudioFilename) + "." + PracticeSharpXMLFilename;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes a full Preset Bank XML into a file
        /// </summary>
        public void WritePresetsBank(Dictionary<string, PresetControl> presetControls, string currentPresetId)
        {
            // Create an XML Document
            XmlDocument doc = new XmlDocument();
            XmlElement elRoot = (XmlElement)doc.AppendChild(doc.CreateElement(PresetBankFile.XML_Node_Root));
            elRoot.SetAttribute(PresetBankFile.XML_Attr_Version, m_appVersion.ToString());
            XmlElement elPresets = (XmlElement)elRoot.AppendChild(doc.CreateElement(PresetBankFile.XML_Node_PresetsBank));
            elPresets.SetAttribute(PresetBankFile.XML_Attr_Filename, Path.GetFileName(m_currentAudioFilename));
            elPresets.SetAttribute(PresetBankFile.XML_Attr_ActivePreset, currentPresetId);

            // Write XML Nodes for each Preset
            foreach (PresetControl presetControl in presetControls.Values )
            {
                PresetData presetData = presetControl.PresetData;

                XmlElement elPreset = (XmlElement)elPresets.AppendChild(doc.CreateElement(PresetBankFile.XML_Node_Preset));
                // Write Preset Attributes
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Id, presetControl.Id);
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Tempo, presetData.Tempo.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Pitch, presetData.Pitch.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Volume, presetData.Volume.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_LoEq, presetData.LoEqValue.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_MedEq, presetData.MedEqValue.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_HiEq, presetData.HiEqValue.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_PlayTime, presetData.CurrentPlayTime.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_LoopStartMarker, presetData.StartMarker.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_LoopEndMarker, presetData.EndMarker.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_IsLoop, presetData.Loop.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Cue, presetData.Cue.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_IsLoop, presetData.Loop.ToString());
                elPreset.SetAttribute(PresetBankFile.XML_Attr_Description, presetData.Description);
                elPreset.SetAttribute(PresetBankFile.XML_Attr_TimeStretchProfileId, presetData.TimeStretchProfile.Id);
                elPreset.SetAttribute(PresetBankFile.XML_Attr_RemoveVocals, presetData.RemoveVocals.ToString());
            }

            // Write to XML file
            using (StreamWriter writer = new StreamWriter(m_presetsBankFilename, false, Encoding.UTF8))
            {
                writer.Write(doc.OuterXml);
            }
        }

        /// <summary>
        /// Loads the presets from the preset bank file
        /// </summary>
        /// <returns>
        /// The active preset Id
        /// </returns>
        public string LoadPresetsBank( Dictionary<string,PresetControl> presetControls )
        {
            if (!File.Exists(m_presetsBankFilename))
            {
                return null;
            }

            try
            {
                // Loads the presets bank XML file
                XmlDocument doc = new XmlDocument();
                doc.Load(m_presetsBankFilename);

                XmlElement root = doc.DocumentElement;
                XmlNode presetsBankNode = root.SelectSingleNode("/" + PresetBankFile.XML_Node_Root + "/" + PresetBankFile.XML_Node_PresetsBank);
                string activePresetId = presetsBankNode.Attributes[PresetBankFile.XML_Attr_ActivePreset].Value;
                XmlNodeList presetNodes = presetsBankNode.SelectNodes(PresetBankFile.XML_Node_Preset);
                // Load all preset nodes
                foreach (XmlNode presetNode in presetNodes)
                {
                    string presetId = presetNode.Attributes[PresetBankFile.XML_Attr_Id].Value;
                    // Load XML values into PresetData object
                    PresetData presetData = presetControls[presetId].PresetData;
                    presetData.Tempo = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_Tempo, PresetData.DefaultTempo);
                    presetData.Pitch = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_Pitch, PresetData.DefaultPitch);
                    presetData.Volume = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_Volume, Properties.Settings.Default.DefaultVolume);

                    presetData.LoEqValue = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_LoEq, PresetData.DefaultLoEq);
                    presetData.MedEqValue = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_MedEq, PresetData.DefaultMedEq);
                    presetData.HiEqValue = ReadXMLAttributeFloat(presetNode, PresetBankFile.XML_Attr_HiEq, PresetData.DefaultHiEq);

                    presetData.CurrentPlayTime = TimeSpan.Parse(presetNode.Attributes[PresetBankFile.XML_Attr_PlayTime].Value);
                    presetData.StartMarker = TimeSpan.Parse(presetNode.Attributes[PresetBankFile.XML_Attr_LoopStartMarker].Value);
                    presetData.EndMarker = TimeSpan.Parse(presetNode.Attributes[PresetBankFile.XML_Attr_LoopEndMarker].Value);
                    presetData.Loop = Convert.ToBoolean(presetNode.Attributes[PresetBankFile.XML_Attr_IsLoop].Value);
                    presetData.Cue = TimeSpan.Parse(presetNode.Attributes[PresetBankFile.XML_Attr_Cue].Value);
                    presetData.Description = Convert.ToString(presetNode.Attributes[PresetBankFile.XML_Attr_Description].Value);

                    TimeStretchProfile timeStretchProfile = TimeStretchProfileManager.DefaultProfile;
                    if (presetNode.Attributes[PresetBankFile.XML_Attr_TimeStretchProfileId] != null)
                    {
                        string timeStretchProfileId = Convert.ToString(presetNode.Attributes[PresetBankFile.XML_Attr_TimeStretchProfileId].Value);
                        if (!TimeStretchProfileManager.TimeStretchProfiles.TryGetValue(timeStretchProfileId, out timeStretchProfile))
                        {
                            // Handle a mismatched profile by using the default profile
                            timeStretchProfile = TimeStretchProfileManager.DefaultProfile;
                        }
                    }
                    presetData.TimeStretchProfile = timeStretchProfile;

                    if (presetNode.Attributes[PresetBankFile.XML_Attr_RemoveVocals] != null)
                    {
                        presetData.RemoveVocals = Convert.ToBoolean(presetNode.Attributes[PresetBankFile.XML_Attr_RemoveVocals].Value);
                    }

                    PresetControl presetControl = presetControls[presetId];
                    presetControl.PresetDescription = presetData.Description;
                }

                return activePresetId;
            }
            catch (Exception ex)
            {
                m_logger.ErrorException("Failed loading Presets Bank for file: " + m_currentAudioFilename, ex);
                MessageBox.Show(null, "Failed loading Presets Bank for file: " + m_currentAudioFilename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper function - Reads an Float XML attribute in a safe way, if the value does not exist it returns the defaultValue
        /// </summary>
        /// <param name="presetNode"></param>
        /// <param name="attributeName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private float ReadXMLAttributeFloat(XmlNode presetNode, string attributeName, float defaultValue)
        {
            XmlAttribute attribute = presetNode.Attributes[attributeName];
            if (attribute == null)
                return defaultValue;

            string value = attribute.Value;
            if (value == null)
                return defaultValue;
            else
                return Convert.ToSingle(value);
        }

        #endregion

        #region Private Members

        private string m_appVersion;
        private string m_presetsBankFilename;
        private string m_appDataFolder;
        private string m_currentAudioFilename;

        #endregion

        #region Constants

        public const string PracticeSharpXMLFilename = "practicesharpbank.xml";

        #endregion

        #region XML Constants

        const string XML_Node_Root = "PracticeSharp";
        const string XML_Node_PresetsBank = "PresetsBank";
        const string XML_Node_Preset = "Preset";
        const string XML_Attr_ActivePreset = "ActivePreset";
        const string XML_Attr_Version = "Version";
        const string XML_Attr_Filename = "Filename";
        const string XML_Attr_Id = "Id";
        const string XML_Attr_Tempo = "Tempo";
        const string XML_Attr_Pitch = "Pitch";
        const string XML_Attr_Volume = "Volume";
        const string XML_Attr_LoEq = "LoEq";
        const string XML_Attr_MedEq = "MedEq";
        const string XML_Attr_HiEq = "HiEq";
        const string XML_Attr_PlayTime = "PlayTime";
        const string XML_Attr_LoopStartMarker = "LoopStartMarker";
        const string XML_Attr_LoopEndMarker = "LoopEndMarker";
        const string XML_Attr_IsLoop = "IsLoop";
        const string XML_Attr_Cue = "Cue";
        const string XML_Attr_Description = "Description";
        const string XML_Attr_TimeStretchProfileId = "TimeStretchProfileId";
        const string XML_Attr_RemoveVocals = "RemoveVocals";

        #endregion
    }
}
