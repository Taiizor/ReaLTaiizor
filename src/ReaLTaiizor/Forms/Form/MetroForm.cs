#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Native.User32;

#endregion

namespace ReaLTaiizor.Forms
{
    #region MetroForm

    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(MetroForm), "Bitmaps.Form.bmp")]
    [DesignerCategory("Form")]
    [DefaultEvent("Load")]
    [DesignTimeVisible(false)]
    [ComVisible(true)]
    [InitializationEvent("Load")]
    public class MetroForm : Form, IMetroForm
    {
        #region Constructor

        protected MetroForm()
        {
            SetStyle
            (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ContainerControl |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            _mth = new Methods();
            _utl = new Utilites();
            _user32 = new User32();
            Padding = new Padding(12, 70, 12, 12);
            FormBorderStyle = FormBorderStyle.None;
            _backgroundImageTransparency = 0.90f;
            base.Font = MetroFonts.SemiLight(13);
            DropShadowEffect = true;
            _showLeftRect = true;
            _showHeader = false;
            AllowResize = true;
            ApplyTheme();

        }

        #endregion Constructor

        #region Draw Control
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.InterpolationMode = InterpolationMode.High;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            using (SolidBrush b = new(BackgroundColor))
            {
                e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
                if (BackgroundImage != null)
                {
                    _mth.DrawImageWithTransparency(e.Graphics, BackgroundImageTransparency, BackgroundImage, ClientRectangle);
                }
            }
            if (ShowBorder)
            {
                using Pen p = new(BorderColor, BorderThickness);
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            if (ShowLeftRect)
            {
                using LinearGradientBrush b = new(new Rectangle(0, 25, SmallRectThickness, 35), SmallLineColor1, SmallLineColor2, 90);
                using SolidBrush textBrush = new(TextColor);
                e.Graphics.FillRectangle(b, new Rectangle(0, 40, SmallRectThickness, 35));
                e.Graphics.DrawString(Text, Font, textBrush, new Point(SmallRectThickness + 10, 46));
            }
            else
            {
                if (ShowHeader)
                {
                    using SolidBrush b = new(HeaderColor);
                    e.Graphics.FillRectangle(b, new Rectangle(1, 1, Width - 1, HeaderHeight));
                }

                SolidBrush textBrush = new(TextColor);
                if (ShowTitle)
                {
                    switch (TextAlign)
                    {
                        case TextAlign.Left:
                            using (StringFormat stringFormat = new() { LineAlignment = StringAlignment.Center })
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width, HeaderHeight), stringFormat);
                            }

                            break;
                        case TextAlign.Center:
                            using (StringFormat stringFormat = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 21, HeaderHeight), stringFormat);
                            }

                            break;
                        case TextAlign.Right:
                            using (StringFormat stringFormat = new() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                            {
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 26, HeaderHeight), stringFormat);
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                textBrush.Dispose();
            }
        }

        #endregion Draw Control

        #region Properties

        [Category("Metro"), Description("Gets or sets the form backcolor.")]
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets the form bordercolor.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the form textcolor.")]
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the form small line color 1.")]
        public Color SmallLineColor1
        {
            get => _smallLineColor1;
            set
            {
                _smallLineColor1 = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the form small line color 2.")]
        public Color SmallLineColor2
        {
            get => _smallLineColor2;
            set
            {
                _smallLineColor2 = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the header color.")]
        public Color HeaderColor
        {
            get => _headerColor;
            set
            {
                _headerColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the width of the small rectangle on top left of the window.")]
        public int SmallRectThickness
        {
            get => _smallRectThickness;
            set
            {
                _smallRectThickness = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the border be shown."), DefaultValue(true)]
        public bool ShowBorder
        {
            get => _showBorder;
            set
            {
                _showBorder = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the border thickness.")]
        public float BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value;
                Refresh();
            }
        }

        [DefaultValue(FormBorderStyle.None)]
        [Browsable(false)]
        private new FormBorderStyle FormBorderStyle
        {
            set
            {
                if (!System.Enum.IsDefined(typeof(FormBorderStyle), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(FormBorderStyle));
                }

                base.FormBorderStyle = FormBorderStyle.None;
            }
        }


        [Category("WindowStyle")]
        [Browsable(false)]
        [DefaultValue(false)]
        [Description("FormMaximizeBox")]
        public new bool MaximizeBox => false;

        [Category("WindowStyle")]
        [Browsable(false)]
        [DefaultValue(false)]
        [Description("FormMinimizeBox")]
        public new bool MinimizeBox
        {
            get => false;
            set => value = false;
        }

        [Category("Metro"), Description("Gets or sets whether the title be shown.")]
        public bool ShowTitle
        {
            get => _showTitle;
            set
            {
                _showTitle = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the title alignment.")]
        public TextAlign TextAlign
        {
            get => _textAlign;
            set
            {
                _textAlign = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets whether show the header.")]
        public bool ShowHeader
        {
            get => _showHeader;
            set
            {
                _showHeader = value;
                if (value)
                {
                    ShowLeftRect = false;
                    Padding = new Padding(2, HeaderHeight + 30, 2, 2);
                    Text = Text.ToUpper();
                    TextColor = Color.White;
                    ShowTitle = true;
                    foreach (Control c in Controls)
                    {
                        if (c.GetType() != typeof(MetroControlBox))
                        {
                            continue;
                        }

                        c.BringToFront();
                        c.Location = new(Width - 12, 11);
                    }
                }
                else
                {
                    Padding = new Padding(12, 90, 12, 12);
                    ShowTitle = false;
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the small rectangle on top left of the window be shown.")]
        public bool ShowLeftRect
        {
            get => _showLeftRect;
            set
            {
                _showLeftRect = value;
                if (value)
                {
                    ShowHeader = false;
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the form can be move or not."), DefaultValue(true)]
        public bool Moveable
        {
            get => _movable;
            set
            {
                _movable = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the form use animation.")]
        public bool UseSlideAnimation
        {
            get => _useSlideAnimation;
            set
            {
                _useSlideAnimation = value;
                Refresh();
            }
        }

        [Browsable(false)]
        public new Padding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }

        [Category("Metro"), Description("Gets or sets the backgroundimage transparency.")]
        public float BackgroundImageTransparency
        {
            get => _backgroundImageTransparency;
            set
            {
                if (value > 1)
                {
                    throw new Exception("The Value must be between 0-1.");
                }

                _backgroundImageTransparency = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the header height.")]
        public int HeaderHeight
        {
            get => _headerHeight;
            set
            {
                _headerHeight = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the background image displayed in the control.")]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        [Category("Metro"), Description("Gets or sets whether the drop shadow effect apply on form.")]
        public bool DropShadowEffect
        {
            get => _dropShadowEffect;
            set
            {
                _dropShadowEffect = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the user be able to resize the form or not.")]
        public bool AllowResize
        {
            get => _allowResize;
            set
            {
                _allowResize = value;
                Refresh();
            }
        }


        #endregion Properties

        #region Methods

        private void ResizeForm(ref Message message)
        {
            if (!AllowResize)
            {
                return;
            }

            int x = (int)(message.LParam.ToInt64() & 65535);
            int y = (int)((message.LParam.ToInt64() & -65536) >> 0x10);
            Point point = PointToClient(new Point(x, y));

            #region  From Corners  

            if (point.Y >= Height - 0x10)
            {
                if (point.X >= Width - 0x10)
                {
                    message.Result = (IntPtr)(IsMirrored ? 0x10 : 0x11);
                    return;
                }

                if (point.X <= 0x10)
                {
                    message.Result = (IntPtr)(IsMirrored ? 0x11 : 0x10);
                    return;
                }
            }
            else if (point.Y <= 0x10)
            {
                if (point.X <= 0x10)
                {
                    message.Result = (IntPtr)(IsMirrored ? 0xe : 0xd);
                    return;
                }

                if (point.X >= Width - 0x10)
                {
                    message.Result = (IntPtr)(IsMirrored ? 0xd : 0xe);
                    return;
                }
            }

            #endregion

            #region From Sides

            if (point.Y <= 0x10)
            {
                message.Result = (IntPtr)0xc;
                return;
            }

            if (point.Y >= Height - 0x10)
            {
                message.Result = (IntPtr)0xf;
                return;
            }

            if (point.X <= 0x10)
            {
                message.Result = (IntPtr)0xa;
                return;
            }

            if (point.X >= Width - 0x10)
            {
                message.Result = (IntPtr)0xb;
            }

            #endregion
        }

        #endregion Methods

        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control."), DefaultValue(Style.Light)]
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
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager StyleManager
        {
            get => _styleManager;
            set
            {
                _styleManager = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        private User32 User32 => _user32;

        private User32 _user32 { get; }

        #endregion Interfaces

        #region Global Vars

        private readonly Utilites _utl;
        private readonly Methods _mth;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private bool _showLeftRect;
        private bool _showHeader;
        private float _backgroundImageTransparency;

        private Color _backgroundColor;
        private Color _borderColor;
        private Color _textColor;
        private Color _smallLineColor1;
        private Color _smallLineColor2;
        private Color _headerColor;
        private int _smallRectThickness = 10;
        private bool _showBorder;
        private float _borderThickness = 1;
        private bool _showTitle = true;
        private TextAlign _textAlign = TextAlign.Left;
        private bool _movable = true;
        private bool _useSlideAnimation;
        private int _headerHeight = 40;
        private bool _dropShadowEffect;
        private bool _allowResize;

        #endregion Internal Vars

        #region ApplyTheme

        internal void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.Gray;
                    BackgroundColor = Color.White;
                    BorderColor = Color.FromArgb(65, 177, 225);
                    TextColor = ShowHeader ? Color.White : Color.Gray;
                    SmallLineColor1 = Color.FromArgb(65, 177, 225);
                    SmallLineColor2 = Color.FromArgb(65, 177, 225);
                    HeaderColor = Color.FromArgb(65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.White;
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    BorderColor = Color.FromArgb(65, 177, 225);
                    SmallLineColor1 = Color.FromArgb(65, 177, 225);
                    SmallLineColor2 = Color.FromArgb(65, 177, 225);
                    HeaderColor = Color.FromArgb(65, 177, 225);
                    TextColor = ShowHeader ? Color.Gray : Color.White;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.FormDictionary)
                        {
                            if (!string.Equals(varkey.Key, null, StringComparison.Ordinal) && varkey.Key != null)
                            {
                                if (varkey.Key == "ForeColor")
                                {
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "BackColor")
                                {
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "BorderColor")
                                {
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "TextColor")
                                {
                                    TextColor = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "SmallLineColor1")
                                {
                                    SmallLineColor1 = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "SmallLineColor2")
                                {
                                    SmallLineColor2 = _utl.HexColor((string)varkey.Value);
                                }
                                else if (varkey.Key == "SmallRectThickness")
                                {
                                    SmallRectThickness = int.Parse(varkey.Value.ToString());
                                }
                                else if (varkey.Key == "HeaderColor")
                                {
                                    HeaderColor = _utl.HexColor((string)varkey.Value);
                                }
                            }
                            else
                            {
                                throw new Exception("FormDictionary is empty");
                            }
                        }
                    }

                    UpdateProperties();
                    break;
            }
        }

        private void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Events

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if ((message.Msg != _WM_NCHITTEST) | !Moveable)
            {
                return;
            }

            if ((int)message.Result == _HTCLIENT)
            {
                message.Result = new IntPtr(_HTCAPTION);
            }

            ResizeForm(ref message);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            AutoScaleMode = AutoScaleMode.None;
            base.OnHandleCreated(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if (!DropShadowEffect)
                {
                    return base.CreateParams;
                }

                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= _CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
            AnimateWindow(Handle, 800, AnimateWindowFlags.AW_ACTIVATE | (UseSlideAnimation ? AnimateWindowFlags.AW_HOR_POSITIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            // https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func
            if (e.Cancel == false)
            {
                AnimateWindow(Handle, 800, User32.AW_HIDE | (UseSlideAnimation ? AnimateWindowFlags.AW_HOR_NEGATIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
            }
        }

        #endregion
    }

    #endregion
}