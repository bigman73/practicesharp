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
        public HoverLabel()
        {
            HoverColor = Color.FromName(DefaultHoverColorName);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            ForeColor = HoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ForeColor = DefaultForeColor;
        }

        [BrowsableAttribute(true)]
        [DefaultValue(typeof(Color), DefaultHoverColorName)]
        public Color HoverColor { get; set; }

        public const string DefaultHoverColorName = "Blue";
    }
}
