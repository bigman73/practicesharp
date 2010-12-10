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
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// Hover Label changes its color when the mouse over it (Mouse Enter), and 
    ///   returns to its original color when the mouse leaves it (Mouse Leave)
    /// </summary>
    class HoverLabel: Label
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HoverLabel()
        {
            HoverColor = Color.FromName(DefaultHoverColorName);
        }

        /// <summary>
        /// Mouse Enter - Start Hover
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            ForeColor = HoverColor;
        }

        /// <summary>
        /// Mouse Leave - End Hover
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ForeColor = DefaultForeColor;
        }

        /// <summary>
        /// Allow programmatic clicking of the label
        /// </summary>
        public void PerformClick()
        {
            OnClick(new EventArgs());
        }

        [BrowsableAttribute(true)]
        [DefaultValue(typeof(Color), DefaultHoverColorName)]
        public Color HoverColor { get; set; }

        public const string DefaultHoverColorName = "Blue";
    }
}
