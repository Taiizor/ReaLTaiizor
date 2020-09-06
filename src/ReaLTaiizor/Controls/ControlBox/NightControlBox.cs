#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

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

        private bool _EnableMaximize = true;
        [Browsable(true)]
        [Description("Determines whether the control should enable the use of the maximize button.")]
        public bool EnableMaximizeButton
        {
            get { return _EnableMaximize; }
            set
            {
                _EnableMaximize = value;
                Invalidate();
            }
        }

        private bool _EnableMinimize = true;
        [Browsable(true)]
        [Description("Determines whether the control should enable the use of the minimize button.")]
        public bool EnableMinimizeButton
        {
            get { return _EnableMinimize; }
            set
            {
                _EnableMinimize = value;
                Invalidate();
            }
        }

        private bool _DefaultLocation = true;
        [Browsable(true)]
        [Description("ControlBox set location to default.")]
        public bool DefaultLocation
        {
            get { return _DefaultLocation; }
            set
            {
                _DefaultLocation = value;
                Invalidate();
            }
        }

        private Color _EnableMinimizeColor = ColorTranslator.FromHtml("#A0A0A0");
        [Browsable(true)]
        [Description("Enabled is Minimize ForeColor.")]
        public Color EnableMinimizeColor
        {
            get { return _EnableMinimizeColor; }
            set
            {
                _EnableMinimizeColor = value;
                Invalidate();
            }
        }

        private Color _DisableMinimizeColor = ColorTranslator.FromHtml("#696969");
        [Browsable(true)]
        [Description("Disabled is Minimize ForeColor.")]
        public Color DisableMinimizeColor
        {
            get { return _DisableMinimizeColor; }
            set
            {
                _DisableMinimizeColor = value;
                Invalidate();
            }
        }

        private Color _MinimizeHoverColor = Color.FromArgb(15, Color.White);
        [Browsable(true)]
        [Description("Minimize is HoverColor.")]
        public Color MinimizeHoverColor
        {
            get { return _MinimizeHoverColor; }
            set
            {
                _MinimizeHoverColor = value;
                Invalidate();
            }
        }

        private Color _MinimizeHoverForeColor = Color.White;
        [Browsable(true)]
        [Description("Minimize is HoverForeColor.")]
        public Color MinimizeHoverForeColor
        {
            get { return _MinimizeHoverForeColor; }
            set
            {
                _MinimizeHoverForeColor = value;
                Invalidate();
            }
        }

        private Color _EnableMaximizeColor = ColorTranslator.FromHtml("#A0A0A0");
        [Browsable(true)]
        [Description("Enabled is Maximize ForeColor.")]
        public Color EnableMaximizeColor
        {
            get { return _EnableMaximizeColor; }
            set
            {
                _EnableMaximizeColor = value;
                Invalidate();
            }
        }

        private Color _DisableMaximizeColor = ColorTranslator.FromHtml("#696969");
        [Browsable(true)]
        [Description("Disabled is Maximize ForeColor.")]
        public Color DisableMaximizeColor
        {
            get { return _DisableMaximizeColor; }
            set
            {
                _DisableMaximizeColor = value;
                Invalidate();
            }
        }

        private Color _MaximizeHoverColor = Color.FromArgb(15, Color.White);
        [Browsable(true)]
        [Description("Maximize is HoverColor.")]
        public Color MaximizeHoverColor
        {
            get { return _MaximizeHoverColor; }
            set
            {
                _MaximizeHoverColor = value;
                Invalidate();
            }
        }

        private Color _MaximizeHoverForeColor = Color.White;
        [Browsable(true)]
        [Description("Maximize is HoverForeColor.")]
        public Color MaximizeHoverForeColor
        {
            get { return _MaximizeHoverForeColor; }
            set
            {
                _MaximizeHoverForeColor = value;
                Invalidate();
            }
        }

        private Color _EnableCloseColor = ColorTranslator.FromHtml("#A0A0A0");
        [Browsable(true)]
        [Description("Enabled is Close ForeColor.")]
        public Color EnableCloseColor
        {
            get { return _EnableCloseColor; }
            set
            {
                _EnableCloseColor = value;
                Invalidate();
            }
        }

        private Color _CloseHoverColor = ColorTranslator.FromHtml("#C75050");
        [Browsable(true)]
        [Description("Close is HoverColor.")]
        public Color CloseHoverColor
        {
            get { return _CloseHoverColor; }
            set
            {
                _CloseHoverColor = value;
                Invalidate();
            }
        }

        private Color _CloseHoverForeColor = Color.White;
        [Browsable(true)]
        [Description("Close is HoverForeColor.")]
        public Color CloseHoverForeColor
        {
            get { return _CloseHoverForeColor; }
            set
            {
                _CloseHoverForeColor = value;
                Invalidate();
            }
        }

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
            Size = new Size(139, 31);
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
            var pf = FindForm();

            if (_EnableMaximize)
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

            if (_EnableMinimize)
            {
                if (hover_min & e.Button == MouseButtons.Left)
                    pf.WindowState = FormWindowState.Minimized;
            }

            if (hover_close & e.Button == MouseButtons.Left)
                Application.Exit();
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
                    Location = new Point(Parent.Width - 139, 0); //Location = new Point(FindForm().Width - 139, 0);
            }
            catch (Exception)
            {
                //
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            // This defines the size of the background that is drawn when
            // the mouse moves over one of the three ControlBox buttons
            var btnBackgroundSize = new Size(46, Height);

            // Minimize button
            var minimizeBtnFont = new Font("Tahoma", 12);
            var minimizeBtnPoint = new Point(15, 5);
            var minimizeBtnBrush = new SolidBrush(_EnableMinimize ? _EnableMinimizeColor : _DisableMinimizeColor);

            if (hover_min && _EnableMinimize)
            {
                using (var backColor = new SolidBrush(_MinimizeHoverColor))
                    g.FillRectangle(backColor, new Rectangle(new Point(1, 0), btnBackgroundSize));

                minimizeBtnBrush = new SolidBrush(_MinimizeHoverForeColor);
            }

            g.DrawString("\u2212", minimizeBtnFont, minimizeBtnBrush, minimizeBtnPoint);
            minimizeBtnBrush.Dispose();
            minimizeBtnFont.Dispose();

            // Maxmize button
            var maximizeBtnFont = new Font("Marlett", 9);
            var maximizeBtnPoint = new Point(63, 10);
            var maximizeBtnBrush = new SolidBrush(_EnableMaximize ? _EnableMaximizeColor : _DisableMaximizeColor);

            if (hover_max && _EnableMaximize)
            {
                using (var backColor = new SolidBrush(_MaximizeHoverColor))
                    g.FillRectangle(backColor, new Rectangle(new Point(47, 0), btnBackgroundSize));

                maximizeBtnBrush = new SolidBrush(_MaximizeHoverForeColor);
            }

            g.DrawString(FindForm().WindowState != FormWindowState.Maximized ? "1" : "2", maximizeBtnFont, maximizeBtnBrush, maximizeBtnPoint);

            maximizeBtnBrush.Dispose();
            maximizeBtnFont.Dispose();

            // Close button
            var closeBtnFont = new Font("Tahoma", 11);
            var closeBtnPoint = new Point(107, 6);
            var closeBtnBrush = new SolidBrush(_EnableCloseColor);

            if (hover_close)
            {
                using (var backColor = new SolidBrush(_CloseHoverColor))
                    g.FillRectangle(backColor, new Rectangle(new Point(93, 0), btnBackgroundSize));

                closeBtnBrush = new SolidBrush(_CloseHoverForeColor);
            }

            g.DrawString("\u2A09", closeBtnFont, closeBtnBrush, closeBtnPoint);
            closeBtnBrush.Dispose();
            closeBtnFont.Dispose();

            base.OnPaint(e);
        }
    }

    #endregion
}