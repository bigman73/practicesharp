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
using System.IO;
using System.Windows.Forms;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// MRU Manager for recent opened files
    /// </summary>
    public class MRUManager
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MRUManager<T>"/> class.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="mruFilename"></param>
        public MRUManager(int limit, string mruFilename)
        {
            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException("limit", "limit must be greater than zero.");
            }

            m_limit = limit;
            m_mruFilename = mruFilename;

            m_items = new List<string>();

            LoadFromFile();
        }

        #endregion

        #region Public API

        /// <summary>
        /// Gets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public int Limit
        {
            get { return m_limit; }
        }

        /// <summary>
        /// Gets the items on the list
        /// </summary>
        public List<string> Items
        {
            get { return new List<string>(m_items); }
        }

        /// <summary>
        /// Add an MRU item
        /// </summary>
        /// <param name="item"></param>
        public void Add(string item)
        {
            if (m_items.Contains(item))
            {
                m_items.Remove(item);
            }

            m_items.Insert(0, item);

            RemoveExtraItems();

            SaveToFile();
        }

        /// <summary>
        /// Remove a single item
        /// </summary>
        /// <param name="item"></param>
        public void Remove(string item)
        {
            if (m_items.Contains(item))
            {
                m_items.Remove(item);
                SaveToFile();
            }
        }

        /// <summary>
        /// Clears this instance - removes all items
        /// </summary>
        public void ClearMRU()
        {
            ClearItems();

            SaveToFile();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Clears items
        /// </summary>
        private void ClearItems()
        {
            m_items.Clear();
        }

        /// <summary>
        /// Saves to file.
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                // Write MRU items to Text file
                using (StreamWriter writer = new StreamWriter(m_mruFilename, false, Encoding.UTF8))
                {
                    foreach (string item in m_items)
                    {
                        writer.WriteLine(item);
                    }

                    writer.Flush();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to save MRU list to disk." + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads MRU items from file.
        /// </summary>
        private void LoadFromFile()
        {
            if (!File.Exists(m_mruFilename))
                return;

            ClearItems();

            using (StreamReader reader = new StreamReader(m_mruFilename))
            {
                while (!reader.EndOfStream && m_items.Count < m_limit)
                {
                    string mruItem = reader.ReadLine();
                    m_items.Add(mruItem);
                }
            }
        }

        /// <summary>
        /// Removes the extra items.
        /// </summary>
        private void RemoveExtraItems()
        {
            if (m_items.Count > Limit)
            {
                for (int x = Limit; x < m_items.Count; x++)
                {
                    m_items.RemoveAt(x);
                }
            }
        }
        
        #endregion

        #region Private Members

        private int m_limit;
        private string m_mruFilename;
        private List<string> m_items;

        #endregion
    }
}

