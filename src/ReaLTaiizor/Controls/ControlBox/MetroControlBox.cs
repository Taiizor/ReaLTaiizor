#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroControlBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroControlBox), "Bitmaps.ControlButton.bmp")]
    [Designer(typeof(MetroControlBoxDesigner))]
    [DefaultProperty("Click")]
    [ComVisible(true)]
    public class MetroControlBox : Control, IMetroControl
    {
        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => StyleManager?.Style ?? _style;
            set
            {
                _style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;
                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;
                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;
                    default:
                        ApplyTheme();
                        break;
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager StyleManager
        {
            get => _styleManager;
            set { _styleManager = value; Invalidate(); }
        }

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private bool _isDerivedStyle = true;
        private bool _maximizeBox = true;
        private Color _closeNormalForeColor;
        private Color _closeHoverForeColor;
        private Color _closeHoverBackColor;
        private Color _maximizeHoverForeColor;
        private Color _maximizeHoverBackColor;
        private Color _maximizeNormalForeColor;
        private Color _minimizeHoverForeColor;
        private Color _minimizeHoverBackColor;
        private Color _minimizeNormalForeColor;
        private Color _disabledForeColor;

        #endregion Internal Vars

        #region Constructors

        public MetroControlBox()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            _utl = new Utilites();
            base.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Cursor = Cursors.Hand;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            if (!IsDerivedStyle)
            {
                return;
            }

            switch (style)
            {
                case Style.Light:
                    CloseHoverBackColor = Color.FromArgb(183, 40, 40);
                    CloseHoverForeColor = Color.White;
                    CloseNormalForeColor = Color.Gray;
                    MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    MaximizeHoverForeColor = Color.Gray;
                    MaximizeNormalForeColor = Color.Gray;
                    MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    MinimizeHoverForeColor = Color.Gray;
                    MinimizeNormalForeColor = Color.Gray;
                    DisabledForeColor = Color.DimGray;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    break;
                case Style.Dark:
                    CloseHoverBackColor = Color.FromArgb(183, 40, 40);
                    CloseHoverForeColor = Color.White;
                    CloseNormalForeColor = Color.Gray;
                    MaximizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    MaximizeHoverForeColor = Color.Gray;
                    MaximizeNormalForeColor = Color.Gray;
                    MinimizeHoverBackColor = Color.FromArgb(238, 238, 238);
                    MinimizeHoverForeColor = Color.Gray;
                    MinimizeNormalForeColor = Color.Gray;
                    DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ControlBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "CloseHoverBackColor":
                                    CloseHoverBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CloseHoverForeColor":
                                    CloseHoverForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CloseNormalForeColor":
                                    CloseNormalForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MaximizeHoverBackColor":
                                    MaximizeHoverBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MaximizeHoverForeColor":
                                    MaximizeHoverForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MaximizeNormalForeColor":
                                    MaximizeNormalForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MinimizeHoverBackColor":
                                    MinimizeHoverBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MinimizeHoverForeColor":
                                    MinimizeHoverForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "MinimizeNormalForeColor":
                                    MinimizeNormalForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                default:
                                    return;
                            }
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
            Invalidate();
        }

        #endregion Theme Changing

        #region Properties

        #region Public

        [Category("Metro"), Description("Gets or sets the Default Location associated with the control.")]
        public LocationType DefaultLocation { get; set; } = LocationType.Normal;

        [Category("Metro")]
        [Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
                     "Set it to false if you want the style of this control be independent. ")]
        public bool IsDerivedStyle
        {
            get => _isDerivedStyle;
            set
            {
                _isDerivedStyle = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.")]
        public bool MaximizeBox
        {
            get => _maximizeBox;
            set
            {
                _maximizeBox = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")]
        private bool _minimizeBox = true;
        public bool MinimizeBox
        {
            get => _minimizeBox;
            set
            {
                _minimizeBox = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Close forecolor used by the control.")]
        public Color CloseNormalForeColor
        {
            get => _closeNormalForeColor;
            set
            {
                _closeNormalForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Close forecolor used by the control.")]
        public Color CloseHoverForeColor
        {
            get => _closeHoverForeColor;
            set
            {
                _closeHoverForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Close backcolor used by the control.")]
        public Color CloseHoverBackColor
        {
            get => _closeHoverBackColor;
            set
            {
                _closeHoverBackColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Maximize forecolor used by the control.")]
        public Color MaximizeHoverForeColor
        {
            get => _maximizeHoverForeColor;
            set
            {
                _maximizeHoverForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Maximize backcolor used by the control.")]
        public Color MaximizeHoverBackColor
        {
            get => _maximizeHoverBackColor;
            set
            {
                _maximizeHoverBackColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Maximize forecolor used by the control.")]
        public Color MaximizeNormalForeColor
        {
            get => _maximizeNormalForeColor;
            set
            {
                _maximizeNormalForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Minimize forecolor used by the control.")]
        public Color MinimizeHoverForeColor
        {
            get => _minimizeHoverForeColor;
            set
            {
                _minimizeHoverForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Minimize backcolor used by the control.")]
        public Color MinimizeHoverBackColor
        {
            get => _minimizeHoverBackColor;
            set
            {
                _minimizeHoverBackColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets Minimize forecolor used by the control.")]
        public Color MinimizeNormalForeColor
        {
            get => _minimizeNormalForeColor;
            set
            {
                _minimizeNormalForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets disabled forecolor used by the control.")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set
            {
                _disabledForeColor = value;
                Refresh();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        #endregion

        #region Private 

        private bool MinimizeHovered { get; set; }

        private bool MaximizeHovered { get; set; }

        private bool CloseHovered { get; set; }

        #endregion

        #endregion

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (SolidBrush closeBoxState = new(CloseHovered ? CloseHoverBackColor : Color.Transparent))
            {
                using Font f = new(@"Marlett", 12);
                using SolidBrush tb = new(CloseHovered ? CloseHoverForeColor : CloseNormalForeColor);
                using StringFormat sf = new() { Alignment = StringAlignment.Center };
                g.FillRectangle(closeBoxState, new Rectangle(70, 5, 27, Height));
                g.DrawString("r", f, CloseHovered ? tb : Brushes.Gray, new Point(Width - 16, 8), sf);
            }
            using (SolidBrush maximizeBoxState = new(MaximizeBox ? MaximizeHovered ? MaximizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using Font f = new(@"Marlett", 12);
                using SolidBrush tb = new(MaximizeBox ? MaximizeHovered ? MaximizeHoverForeColor : MaximizeNormalForeColor : DisabledForeColor);
                string maxSymbol = Parent.FindForm()?.WindowState == FormWindowState.Maximized ? "2" : "1";
                using StringFormat sf = new() { Alignment = StringAlignment.Center };
                g.FillRectangle(maximizeBoxState, new Rectangle(38, 5, 24, Height));
                g.DrawString(maxSymbol, f, tb, new Point(51, 7), sf);
            }
            using (SolidBrush minimizeBoxState = new(MinimizeBox ? MinimizeHovered ? MinimizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using Font f = new(@"Marlett", 12);
                using SolidBrush tb = new(MinimizeBox ? MinimizeHovered ? MinimizeHoverForeColor : MinimizeNormalForeColor : DisabledForeColor);
                using StringFormat sf = new() { Alignment = StringAlignment.Center };
                g.FillRectangle(minimizeBoxState, new Rectangle(5, 5, 27, Height));
                g.DrawString("0", f, tb, new Point(20, 7), sf);
            }

        }

        #endregion

        #region Events

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            try
            {
                switch (DefaultLocation)
                {
                    case LocationType.Space:
                        Location = new(Parent.Width - Width - 12, 13);
                        break;
                    case LocationType.Edge:
                        Location = new(Parent.Width - Width, 0);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(100, 25);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Location.Y > 0 && e.Location.Y < (Height - 2))
            {
                if (e.Location.X is > 0 and < 34)
                {
                    MinimizeHovered = true;
                    MaximizeHovered = false;
                    CloseHovered = false;
                }
                else if (e.Location.X is > 33 and < 65)
                {
                    MinimizeHovered = false;
                    MaximizeHovered = true;
                    CloseHovered = false;
                }
                else if (e.Location.X > 64 && e.Location.X < Width)
                {
                    MinimizeHovered = false;
                    MaximizeHovered = false;
                    CloseHovered = true;
                }
                else
                {
                    MinimizeHovered = false;
                    MaximizeHovered = false;
                    CloseHovered = false;
                }
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (CloseHovered)
            {
                Parent.FindForm()?.Close();
            }
            else if (MinimizeHovered)
            {
                if (!MinimizeBox)
                {
                    return;
                }

                Parent.FindForm().WindowState = FormWindowState.Minimized;
            }
            else if (MaximizeHovered)
            {
                if (MaximizeBox)
                {
                    Parent.FindForm().WindowState = Parent.FindForm()?.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MinimizeHovered = false;
            MaximizeHovered = false;
            CloseHovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        #endregion
    }

    #endregion
}