#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroControlBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroControlBox), "Bitmaps.ControlButton.bmp")]
    [Designer(typeof(MetroControlBoxDesigner))]
    [DefaultProperty("Click")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroControlBox : Control, iControl
    {
        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => MetroStyleManager?.Style ?? _style;
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
        public MetroStyleManager MetroStyleManager
        {
            get => _metroStyleManager;
            set { _metroStyleManager = value; Invalidate(); }
        }

        [Category("Metro"), Description("Gets or sets the Default Location associated with the control.")]
        public LocationType DefaultLocation
        {
            get
            {
                return _DefaultLocation;
            }
            set
            {
                _DefaultLocation = value;
            }
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
        private MetroStyleManager _metroStyleManager;
        private LocationType _DefaultLocation = LocationType.Normal;

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
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
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
                    ThemeName = "MetroLite";
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
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.ControlBoxDictionary)
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
                    ;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
            Invalidate();
        }

        #endregion Theme Changing

        #region Properties

        #region Public

        [Category("Metro"), Description("Gets or sets a value indicating whether the Maximize button is Enabled in the caption bar of the form.")]
        public bool MaximizeBox { get; set; } = true;

        [Category("Metro"), Description("Gets or sets a value indicating whether the Minimize button is Enabled in the caption bar of the form.")]
        public bool MinimizeBox { get; set; } = true;

        [Category("Metro"), Description("Gets or sets Close forecolor used by the control.")]
        public Color CloseNormalForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets Close forecolor used by the control.")]
        public Color CloseHoverForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets Close backcolor used by the control.")]
        public Color CloseHoverBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets Maximize forecolor used by the control.")]
        public Color MaximizeHoverForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets Maximize backcolor used by the control.")]
        public Color MaximizeHoverBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets Maximize forecolor used by the control.")]
        public Color MaximizeNormalForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets Minimize forecolor used by the control.")]
        public Color MinimizeHoverForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets Minimize backcolor used by the control.")]
        public Color MinimizeHoverBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets Minimize forecolor used by the control.")]
        public Color MinimizeNormalForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets disabled forecolor used by the control.")]
        public Color DisabledForeColor { get; set; }

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
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (var closeBoxState = new SolidBrush(CloseHovered ? CloseHoverBackColor : Color.Transparent))
            {
                using (var f = new Font("Marlett", 12))
                {
                    using (var tb = new SolidBrush(CloseHovered ? CloseHoverForeColor : CloseNormalForeColor))
                    {
                        using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.FillRectangle(closeBoxState, new Rectangle(70, 5, 27, Height));
                            G.DrawString("r", f, CloseHovered ? tb : Brushes.Gray, new Point(Width - 16, 8), sf);
                        }
                    }
                }
            }
            using (var maximizeBoxState = new SolidBrush(MaximizeBox ? MaximizeHovered ? MaximizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using (var f = new Font("Marlett", 12))
                {
                    using (var tb = new SolidBrush(MaximizeBox ? MaximizeHovered ? MaximizeHoverForeColor : MaximizeNormalForeColor : DisabledForeColor))
                    {
                        var maxSymbol = Parent.FindForm().WindowState == FormWindowState.Maximized ? "2" : "1";
                        using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.FillRectangle(maximizeBoxState, new Rectangle(38, 5, 24, Height));
                            G.DrawString(maxSymbol, f, tb, new Point(51, 7), sf);
                        }
                    }
                }
            }
            using (var minimizeBoxState = new SolidBrush(MinimizeBox ? MinimizeHovered ? MinimizeHoverBackColor : Color.Transparent : Color.Transparent))
            {
                using (var f = new Font("Marlett", 12))
                {
                    using (var tb = new SolidBrush(MinimizeBox ? MinimizeHovered ? MinimizeHoverForeColor : MinimizeNormalForeColor : DisabledForeColor))
                    {
                        using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.FillRectangle(minimizeBoxState, new Rectangle(5, 5, 27, Height));
                            G.DrawString("0", f, tb, new Point(20, 7), sf);
                        }
                    }
                }
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
                        Location = new Point((Parent.Width - Width) - 12, 13);
                        break;
                    case LocationType.Edge:
                        Location = new Point(Parent.Width - Width, 0);
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
            Size = new Size(100, 25);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Location.Y > 0 && e.Location.Y < (Height - 2))
            {
                if (e.Location.X > 0 && e.Location.X < 34)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = true;
                    MaximizeHovered = false;
                    CloseHovered = false;
                }
                else if (e.Location.X > 33 && e.Location.X < 65)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = false;
                    MaximizeHovered = true;
                    CloseHovered = false;
                }
                else if (e.Location.X > 64 && e.Location.X < Width)
                {
                    Cursor = Cursors.Hand;
                    MinimizeHovered = false;
                    MaximizeHovered = false;
                    CloseHovered = true;
                }
                else
                {
                    Cursor = Cursors.Arrow;
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
                Parent.FindForm().Close();
            else if (MinimizeHovered)
            {
                if (MinimizeBox)
                    Parent.FindForm().WindowState = FormWindowState.Minimized;
            }
            else if (MaximizeHovered)
            {
                if (MaximizeBox)
                    Parent.FindForm().WindowState = Parent.FindForm().WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;
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