#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region NightControlBox

    public class NightControlBox : Control
    {
        #region Fields

        private bool hover_min, hover_max, hover_close;

        #endregion

        #region Custom Properties

        [Browsable(true)]
        [Description("Determines whether the control should enable the use of the maximize button.")]
        public bool EnableMaximizeButton
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Browsable(true)]
        [Description("Determines whether the control should enable the use of the minimize button.")]
        public bool EnableMinimizeButton
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Browsable(true)]
        [Description("ControlBox set location to default.")]
        public bool DefaultLocation
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Browsable(true)]
        [Description("Enabled is Minimize ForeColor.")]
        public Color EnableMinimizeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#A0A0A0");

        [Browsable(true)]
        [Description("Disabled is Minimize ForeColor.")]
        public Color DisableMinimizeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#696969");

        [Browsable(true)]
        [Description("Minimize is HoverColor.")]
        public Color MinimizeHoverColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(15, Color.White);

        [Browsable(true)]
        [Description("Minimize is HoverForeColor.")]
        public Color MinimizeHoverForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Browsable(true)]
        [Description("Enabled is Maximize ForeColor.")]
        public Color EnableMaximizeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#A0A0A0");

        [Browsable(true)]
        [Description("Disabled is Maximize ForeColor.")]
        public Color DisableMaximizeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#696969");

        [Browsable(true)]
        [Description("Maximize is HoverColor.")]
        public Color MaximizeHoverColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(15, Color.White);

        [Browsable(true)]
        [Description("Maximize is HoverForeColor.")]
        public Color MaximizeHoverForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Browsable(true)]
        [Description("Enabled is Close ForeColor.")]
        public Color EnableCloseColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#A0A0A0");

        [Browsable(true)]
        [Description("Close is HoverColor.")]
        public Color CloseHoverColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ColorTranslator.FromHtml("#C75050");

        [Browsable(true)]
        [Description("Close is HoverForeColor.")]
        public Color CloseHoverForeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        #endregion

        #region Hidden Properties

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RightToLeft RightToLeft { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ContextMenuStrip ContextMenuStrip { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size MinimumSize { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size MaximumSize { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Font Font { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Margin { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Tag { get; set; }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get; set; }

        #endregion

        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(139, 31);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.X > 0 && e.X < 47 && e.Y > 0 && e.Y < 31)
            {
                hover_min = true;
                hover_max = false;
                hover_close = false;
            }
            else if (e.X > 46 && e.X < 94 && e.Y > 0 && e.Y < 31)
            {
                hover_min = false;
                hover_max = true;
                hover_close = false;
            }
            else if (e.X > 93 && e.X < 150 && e.Y > 0 && e.Y < 31)
            {
                hover_min = false;
                hover_max = false;
                hover_close = true;
            }
            else
            {
                hover_min = false;
                hover_max = false;
                hover_close = false;
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hover_min = false;
            hover_max = false;
            hover_close = false;

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Parent form
            Form pf = FindForm();

            if (EnableMaximizeButton)
            {
                if (hover_max & e.Button == MouseButtons.Left)
                {
                    switch (pf.WindowState)
                    {
                        case FormWindowState.Normal:
                            pf.WindowState = FormWindowState.Maximized;
                            break;
                        case FormWindowState.Maximized:
                            pf.WindowState = FormWindowState.Normal;
                            break;
                    }
                }
            }

            if (EnableMinimizeButton)
            {
                if (hover_min & e.Button == MouseButtons.Left)
                {
                    pf.WindowState = FormWindowState.Minimized;
                }
            }

            if (hover_close & e.Button == MouseButtons.Left)
            {
                pf.Close();
            }
        }

        #endregion

        public NightControlBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Cursor = Cursors.Hand;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            try
            {
                if (DefaultLocation)
                {
                    Location = new(Parent.Width - 139, 0); //Location = new(FindForm().Width - 139, 0);
                }
            }
            catch (Exception)
            {
                //
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // This defines the size of the background that is drawn when
            // the mouse moves over one of the three ControlBox buttons
            Size btnBackgroundSize = new(46, Height);

            // Minimize button
            Font minimizeBtnFont = new("Tahoma", 12);
            Point minimizeBtnPoint = new(15, 5);
            SolidBrush minimizeBtnBrush = new(EnableMinimizeButton ? EnableMinimizeColor : DisableMinimizeColor);

            if (hover_min && EnableMinimizeButton)
            {
                using (SolidBrush backColor = new(MinimizeHoverColor))
                {
                    g.FillRectangle(backColor, new Rectangle(new Point(1, 0), btnBackgroundSize));
                }

                minimizeBtnBrush = new(MinimizeHoverForeColor);
            }

            g.DrawString("\u2212", minimizeBtnFont, minimizeBtnBrush, minimizeBtnPoint);
            minimizeBtnBrush.Dispose();
            minimizeBtnFont.Dispose();

            // Maxmize button
            Font maximizeBtnFont = new("Marlett", 9);
            Point maximizeBtnPoint = new(63, 10);
            SolidBrush maximizeBtnBrush = new(EnableMaximizeButton ? EnableMaximizeColor : DisableMaximizeColor);

            if (hover_max && EnableMaximizeButton)
            {
                using (SolidBrush backColor = new(MaximizeHoverColor))
                {
                    g.FillRectangle(backColor, new Rectangle(new Point(47, 0), btnBackgroundSize));
                }

                maximizeBtnBrush = new(MaximizeHoverForeColor);
            }

            g.DrawString(FindForm().WindowState != FormWindowState.Maximized ? "1" : "2", maximizeBtnFont, maximizeBtnBrush, maximizeBtnPoint);

            maximizeBtnBrush.Dispose();
            maximizeBtnFont.Dispose();

            // Close button
            Font closeBtnFont = new("Tahoma", 11);
            Point closeBtnPoint = new(107, 6);
            SolidBrush closeBtnBrush = new(EnableCloseColor);

            if (hover_close)
            {
                using (SolidBrush backColor = new(CloseHoverColor))
                {
                    g.FillRectangle(backColor, new Rectangle(new Point(93, 0), btnBackgroundSize));
                }

                closeBtnBrush = new(CloseHoverForeColor);
            }

            g.DrawString("\u2A09", closeBtnFont, closeBtnBrush, closeBtnPoint);
            closeBtnBrush.Dispose();
            closeBtnFont.Dispose();

            base.OnPaint(e);
        }
    }

    #endregion
}