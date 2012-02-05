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
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace BigMansStuff.PracticeSharp.UI
{
    /// <summary>
    /// A replacement for the Windows Button Control, that offers better visualization
    /// </summary>
    /// <remarks>
    /// Original code written by Thomas R. Wolfe: 
    /// http://www.codeproject.com/Articles/19318/Vista-Style-Button-in-C
    /// </remarks>
    [DefaultEvent("Click")]
    public class UltraButton : System.Windows.Forms.UserControl
    {
        #region -  Designer  -

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initialize the component with it's
        /// default settings.
        /// </summary>
        public UltraButton()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            m_fadeInTimer.Interval = 30;
            m_fadeOutTimer.Interval = 30;
        }

        /// <summary>
        /// Release resources used by the control.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region -  Component Designer generated code  -

        private void InitializeComponent()
        {
            // 
            // UltraButton
            // 
            this.Name = "UltraButton";
            this.Size = new System.Drawing.Size(100, 32);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UltraButton_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UltraButton_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UltraButton_KeyDown);
            this.MouseEnter += new System.EventHandler(this.UltraButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UltraButton_MouseLeave);
            this.MouseUp += new MouseEventHandler(UltraButton_MouseUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UltraButton_MouseDown);
            this.GotFocus += new EventHandler(UltraButton_MouseEnter);
            this.LostFocus += new EventHandler(UltraButton_MouseLeave);
            this.m_fadeInTimer.Tick += new EventHandler(FadeInTimer_Tick);
            this.m_fadeOutTimer.Tick += new EventHandler(FadeOutTimer_Tick);
            this.Resize += new EventHandler(UltraButton_Resize);
        }

        #endregion

        #endregion

        #region -  Enums  -

        /// <summary>
        /// A private enumeration that determines 
        /// the mouse state in relation to the 
        /// current instance of the control.
        /// </summary>
        enum State { None, Hover, Pressed };

        /// <summary>
        /// A public enumeration that determines whether
        /// the button background is painted when the 
        /// mouse is not inside the ClientArea.
        /// </summary>
        public enum Style
        {
            /// <summary>
            /// Draw the button as normal
            /// </summary>
            Default,
            /// <summary>
            /// Only draw the background on mouse over.
            /// </summary>
            Flat
        };

        #endregion

        #region -  Properties  -

        #region -  Text  -

        /// <summary>
        /// The text that is displayed on the button.
        /// </summary>
        [Category("Text"),
         Description("The text that is displayed on the button.")]
        public string ButtonText
        {
            get { return m_text; }
            set { m_text = value; this.Invalidate(); }
        }

        /// <summary>
        /// The color with which the text is drawn.
        /// </summary>
        [Category("Text"),
         Browsable(true),
         DefaultValue(typeof(Color), "White"),
         Description("The color with which the text is drawn.")]
        public override Color ForeColor
        {
            get { return m_foreColor; }
            set { m_foreColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// The alignment of the button text
        /// that is displayed on the control.
        /// </summary>
        [Category("Text"),
         DefaultValue(typeof(ContentAlignment), "MiddleCenter"),
         Description("The alignment of the button text " +
                     "that is displayed on the control.")]
        public ContentAlignment TextAlign
        {
            get { return m_textAlign; }
            set { m_textAlign = value; this.Invalidate(); }
        }

        /// <summary>
        /// Text Padding
        /// </summary>
        /// <remarks>
        /// Yuval Naveh, 2/5/2012
        /// </remarks>
        [Category("Text"),
         DefaultValue(typeof(Size), "8,8"),
         Description("The text padding, between the text and the edges of the button")]
        public Size TextPadding
        {
            get { return m_textPadding; }
            set { m_textPadding = value; }
        }

        #endregion

        #region -  Image  -

        /// <summary>
        /// The image displayed on the button that 
        /// is used to help the user identify
        /// it's function if the text is ambiguous.
        /// </summary>
        [Category("Image"),
         DefaultValue(null),
         Description("The image displayed on the button that " +
                     "is used to help the user identify" +
                     "it's function if the text is ambiguous.")]
        public Image Image
        {
            get { return m_image; }
            set { m_image = value; this.Invalidate(); }
        }

        /// <summary>
        /// The alignment of the image 
        /// in relation to the button.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(ContentAlignment), "MiddleLeft"),
         Description("The alignment of the image " +
                     "in relation to the button.")]
        public ContentAlignment ImageAlign
        {
            get { return m_imageAlign; }
            set { m_imageAlign = value; this.Invalidate(); }
        }

        /// <summary>
        /// The size of the image to be displayed on the
        /// button. This property defaults to 24x24.
        /// </summary>
        [Category("Image"),
         DefaultValue(typeof(Size), "24, 24"),
         Description("The size of the image to be displayed on the" +
                     "button. This property defaults to 24x24.")]
        public Size ImageSize
        {
            get { return m_imageSize; }
            set { m_imageSize = value; this.Invalidate(); }
        }

        #endregion

        #region -  Appearance  -

     
        /// <summary>
        /// Sets whether the button background is drawn 
        /// while the mouse is outside of the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Style), "Default"),
         Description("Sets whether the button background is drawn " +
                     "while the mouse is outside of the client area.")]
        public Style ButtonStyle
        {
            get { return m_buttonStyle; }
            set { m_buttonStyle = value; this.Invalidate(); }
        }

        /// <summary>
        /// The radius for the button corners. The 
        /// greater this value is, the more 'smooth' 
        /// the corners are. This property should
        ///  not be greater than half of the 
        ///  controls height.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(8),
         Description("The radius for the button corners. The " +
                     "greater this value is, the more 'smooth' " +
                     "the corners are. This property should " +
                     "not be greater than half of the " +
                     "controls height.")]
        public int CornerRadius
        {
            get { return m_cornerRadius; }
            set { m_cornerRadius = value; this.Invalidate(); }
        }

        /// <summary>
        /// The colour of the highlight on the top of the button.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "White"),
         Description("The colour of the highlight on the top of the button.")]
        public Color HighlightColor
        {
            get { return m_highlightColor; }
            set { m_highlightColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// The bottom color of the button that 
        /// will be drawn over the base color.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "Black"),
         Description("The bottom color of the button that " +
                     "will be drawn over the base color.")]
        public Color ButtonColor
        {
            get { return m_buttonColor; }
            set { m_buttonColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// The colour that the button glows when
        /// the mouse is inside the client area.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "141,189,255"),
         Description("The colour that the button glows when " +
                     "the mouse is inside the client area.")]
        public Color GlowColor
        {
            get { return m_glowColor; }
            set { m_glowColor = value; this.Invalidate(); }
        }

        
        /// <summary>
        /// The constant alpha factor of the glow
        /// </summary>
        /// <remarks>
        /// Yuval Naveh 2/5/2012
        /// </remarks>
        [Category("Appearance"),
         DefaultValue(128),
         Description("The flow alpha factor")]
        public int GlowAlphaFactor
        {
            get { return m_glowAlphaFactor; }
            set { m_glowAlphaFactor = value; this.Invalidate(); }
        }

        /// <summary>
        /// The background image for the button, 
        /// this image is drawn over the base 
        /// color of the button.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(null),
         Description("The background image for the button, " +
                     "this image is drawn over the base " +
                     "color of the button.")]
        public Image BackImage
        {
            get { return m_backImage; }
            set { m_backImage = value; this.Invalidate(); }
        }

        /// <summary>
        /// The backing color that the rest of 
        /// the button is drawn. For a glassier 
        /// effect set this property to Transparent.
        /// </summary>
        [Category("Appearance"),
         DefaultValue(typeof(Color), "Black"),
         Description("The backing color that the rest of" +
                     "the button is drawn. For a glassier " +
                     "effect set this property to Transparent.")]
        public Color BaseColor
        {
            get { return m_baseColor; }
            set { m_baseColor = value; this.Invalidate(); }
        }

        #endregion

        #endregion

        #region -  Functions  -

        private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
        {
            float x = r.X, y = r.Y, w = r.Width, h = r.Height;
            GraphicsPath rr = new GraphicsPath();
            rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
            rr.AddLine(x + r1, y, x + w - r2, y);
            rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
            rr.AddLine(x + w, y + r2, x + w, y + h - r3);
            rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
            rr.AddLine(x + w - r3, y + h, x + r4, y + h);
            rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
            rr.AddLine(x, y + h - r4, x, y + r1);
            return rr;
        }

        private StringFormat StringFormatAlignment(ContentAlignment textalign)
        {
            StringFormat sf = new StringFormat();
            switch (textalign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }
            switch (textalign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }

        /// <summary>
        /// Performs a programmatic click on the button (as if the user clicked it)
        /// </summary>
        /// <remarks>
        /// Yuval Naveh, 2/5/2012
        /// </remarks>
        public void PerformClick()
        {
            this.OnClick(EventArgs.Empty);
        }

        #endregion

        #region -  Drawing  -

        /// <summary>
        /// Draws the outer border for the control
        /// using the ButtonColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawOuterStroke(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.m_buttonState == State.None) { return; }
            Rectangle r = this.ClientRectangle;
            r.Width -= 1; r.Height -= 1;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this.ButtonColor))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        /// <summary>
        /// Draws the inner border for the control
        /// using the HighlightColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawInnerStroke(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.m_buttonState == State.None) { return; }
            Rectangle r = this.ClientRectangle;
            r.X++; r.Y++;
            r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (Pen p = new Pen(this.HighlightColor))
                {
                    g.DrawPath(p, rr);
                }
            }
        }

        /// <summary>
        /// Draws the background for the control
        /// using the background image and the 
        /// BaseColor.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawBackground(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.m_buttonState == State.None) { return; }
            int alpha = (m_buttonState == State.Pressed) ? 204 : 127;
            Rectangle r = this.ClientRectangle;
            r.Width--; r.Height--;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                using (SolidBrush sb = new SolidBrush(this.BaseColor))
                {
                    g.FillPath(sb, rr);
                }
                SetClip(g);
                if (this.BackImage != null) { g.DrawImage(this.BackImage, this.ClientRectangle); }
                g.ResetClip();
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, this.ButtonColor)))
                {
                    g.FillPath(sb, rr);
                }
            }
        }

        /// <summary>
        /// Draws the Highlight over the top of the
        /// control using the HightlightColor.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawHighlight(Graphics g)
        {
            if (this.ButtonStyle == Style.Flat && this.m_buttonState == State.None) { return; }
            int alpha = (m_buttonState == State.Pressed) ? 60 : 150;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height / 2);
            using (GraphicsPath r = RoundRect(rect, CornerRadius, CornerRadius, 0, 0))
            {
                using (LinearGradientBrush lg = new LinearGradientBrush(r.GetBounds(),
                                            Color.FromArgb(alpha, this.HighlightColor),
                                            Color.FromArgb(alpha / 3, this.HighlightColor),
                                            LinearGradientMode.Vertical))
                {
                    g.FillPath(lg, r);
                }
            }
        }

        /// <summary>
        /// Draws the glow for the button when the
        /// mouse is inside the client area using
        /// the GlowColor property.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawGlow(Graphics g)
        {
            if (this.m_buttonState == State.Pressed) { return; }
            SetClip(g);
            using (GraphicsPath glow = new GraphicsPath())
            {
                glow.AddEllipse(-5, this.Height / 2 - 10, this.Width + 11, this.Height + 11);
                using (PathGradientBrush gl = new PathGradientBrush(glow))
                {
                    gl.CenterColor = Color.FromArgb( (int ) ( m_glowAlpha * ( m_glowAlphaFactor / 255.0f ) ), this.GlowColor);
                    gl.SurroundColors = new Color[] { Color.FromArgb(0, this.GlowColor) };
                    g.FillPath(gl, glow);
                }
            }
            g.ResetClip();
        }

        /// <summary>
        /// Draws the text for the button.
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawText(Graphics g)
        {
            StringFormat sf = StringFormatAlignment(this.TextAlign);
            // Yuval Naveh, 2/5/2012 - Added text padding property instead of hard coded values
            Rectangle r = new Rectangle(TextPadding.Width, TextPadding.Height, this.Width - TextPadding.Width * 2, this.Height - TextPadding.Height * 2);
            g.DrawString(this.ButtonText, this.Font, new SolidBrush(this.ForeColor), r, sf);
        }

        /// <summary>
        /// Draws the image for the button
        /// </summary>
        /// <param name="g">The graphics object used in the paint event.</param>
        private void DrawImage(Graphics g)
        {
            if (this.Image == null) { return; }
            Rectangle r = new Rectangle(8, 8, this.ImageSize.Width, this.ImageSize.Height);
            switch (this.ImageAlign)
            {
                case ContentAlignment.TopCenter:
                    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, 8, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.TopRight:
                    r = new Rectangle(this.Width - 8 - this.ImageSize.Width, 8, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleLeft:
                    r = new Rectangle(8, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleCenter:
                    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.MiddleRight:
                    r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomLeft:
                    r = new Rectangle(8, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomCenter:
                    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;
                case ContentAlignment.BottomRight:
                    r = new Rectangle(this.Width - 8 - this.ImageSize.Width, this.Height - 8 - this.ImageSize.Height, this.ImageSize.Width, this.ImageSize.Height);
                    break;
            }
            g.DrawImage(this.Image, r);
        }

        private void SetClip(Graphics g)
        {
            Rectangle r = this.ClientRectangle;
            r.X++; r.Y++; r.Width -= 3; r.Height -= 3;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                g.SetClip(rr);
            }
        }

        #endregion

        #region -  Private Methods  -

        /// <summary>
        /// Paint routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UltraButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            DrawBackground(e.Graphics);
            DrawHighlight(e.Graphics);
            DrawImage(e.Graphics);
            DrawText(e.Graphics);
            DrawGlow(e.Graphics);
            DrawOuterStroke(e.Graphics);
            DrawInnerStroke(e.Graphics);
        }

        /// <summary>
        /// Resize routine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UltraButton_Resize(object sender, EventArgs e)
        {
            Rectangle r = this.ClientRectangle;
            r.X -= 1; r.Y -= 1;
            r.Width += 2; r.Height += 2;
            using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
            {
                this.Region = new Region(rr);
            }
        }

        #region -  Mouse and Keyboard Events  -

        private void UltraButton_MouseEnter(object sender, EventArgs e)
        {
            // Yuval Naveh, 2/5/2012 - Fixed according to post in CodeProject article
            Point mousePt = this.PointToClient(Cursor.Position);
            // if mouse coordinates inside Control
            if (!this.ClientRectangle.Contains(mousePt))
                return;

            m_buttonState = State.Hover;
            m_fadeOutTimer.Stop();
            m_fadeInTimer.Start();
        }

        private void UltraButton_MouseLeave(object sender, EventArgs e)
        {
            m_buttonState = State.None;
            if (this.m_buttonStyle == Style.Flat) { m_glowAlpha = 0; }
            m_fadeInTimer.Stop();
            m_fadeOutTimer.Start();
        }

        private void UltraButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_buttonState = State.Pressed;
                if (this.m_buttonStyle != Style.Flat) { m_glowAlpha = 255; }
                m_fadeInTimer.Stop();
                m_fadeOutTimer.Stop();
                this.Invalidate();
            }
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat) { m_glowAlpha = 0; }
            if (m_glowAlpha + 30 >= 255)
            {
                m_glowAlpha = 255;
                m_fadeInTimer.Stop();
            }
            else
            {
                m_glowAlpha += 30;
            }
            this.Invalidate();
        }

        private void FadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.ButtonStyle == Style.Flat) { m_glowAlpha = 0; }
            if (m_glowAlpha - 30 <= 0)
            {
                m_glowAlpha = 0;
                m_fadeOutTimer.Stop();
            }
            else
            {
                m_glowAlpha -= 30;
            }
            this.Invalidate();
        }

        private void UltraButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                UltraButton_MouseDown(sender, m);
            }
        }

        private void UltraButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
                m_calledByKey = true;
                UltraButton_MouseUp(sender, m);
            }
        }

        private void UltraButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_buttonState = State.Hover;
                m_fadeInTimer.Stop();
                m_fadeOutTimer.Stop();
                this.Invalidate();
                if (m_calledByKey == true) { this.OnClick(EventArgs.Empty); m_calledByKey = false; }
            }
        }

        #endregion

        #endregion

        #region -  Private Members  -

        // Members of properties
        private string m_text;
        private Color m_foreColor = Color.White;
        private ContentAlignment m_textAlign = ContentAlignment.MiddleCenter;
        private Size m_textPadding = new Size(8, 8);

        private Image m_image;
        private ContentAlignment m_imageAlign = ContentAlignment.MiddleLeft;
        private Size m_imageSize = new Size(24, 24);

        private Style m_buttonStyle = Style.Default;
        private int m_cornerRadius = 8;
        private Color m_highlightColor = Color.White;
        private Color m_buttonColor = Color.Black;
        private Color m_glowColor = Color.FromArgb(141, 189, 255);
        private int m_glowAlphaFactor = 128;
        private Image m_backImage;
        private Color m_baseColor = Color.Black;

        private bool m_calledByKey = false;
        private State m_buttonState = State.None;
        private Timer m_fadeInTimer = new Timer();
        private Timer m_fadeOutTimer = new Timer();
        private int m_glowAlpha = 0;

        #endregion
    }
}