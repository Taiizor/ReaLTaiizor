#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
using ReaLTaiizor.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.Drawing.Drawing2D;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Extension.Metro;
using System.Runtime.InteropServices;
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
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroForm : Form, iForm
    {
        #region Properties

        [Category("Metro"), Description("Gets or sets the form backcolor.")]
        public Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets the form bordercolor.")]
        public Color BorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the form textcolor.")]
        public Color TextColor { get; set; }

        [Category("Metro"), Description("Gets or sets the form small line color 1.")]
        public Color SmallLineColor1 { get; set; }

        [Category("Metro"), Description("Gets or sets the form small line color 2.")]
        public Color SmallLineColor2 { get; set; }

        [Category("Metro"), Description("Gets or sets the header color.")]
        public Color HeaderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the width of the small rectangle on top left of the window.")]
        public int SmallRectThickness { get; set; } = 10;

        [Category("Metro"), Description("Gets or sets whether the border be shown."), DefaultValue(true)]
        public bool ShowBorder { get; set; } = true;

        [Category("Metro"), Description("Gets or sets the border thickness.")]
        public float BorderThickness { get; set; } = 1;

        [DefaultValue(FormBorderStyle.None)]
        [Browsable(false)]
        private new FormBorderStyle FormBorderStyle
        {
            set => base.FormBorderStyle = FormBorderStyle.None;
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
        public bool ShowTitle { get; set; } = true;

        [Category("Metro"), Description("Gets or sets the title alignment.")]
        public TextAlign TextAlign { get; set; } = TextAlign.Left;

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
                        if (c.GetType() != typeof(MetroControlBox)) continue;
                        c.BringToFront();
                        c.Location = new Point(Width - 12, 11);
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

        [Category("Metro"),
         Description("Gets or sets whether the small rectangle on top left of the window be shown.")]
        public bool ShowLeftRect
        {
            get => _showLeftRect;
            set
            {
                _showLeftRect = value;
                if (value)
                    ShowHeader = false;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets whether the form can be move or not."), DefaultValue(true)]
        public bool Moveable { get; set; } = true;

        [Category("Metro"), Description("Gets or sets whether the form use animation.")]
        public bool UseSlideAnimation { get; set; } = false;

        [Browsable(false)]
        public new Padding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }

        [Category("Metro"), Description("Gets or sets the backgroundimage transparency.")]
        public float BackgroundImageTransparency
        {
            get => _backgorundImageTrasparency;
            set
            {
                if (value > 1)
                    throw new Exception("The Value must be between 0-1.");

                _backgorundImageTrasparency = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the header height.")]
        public int HeaderHeight { get; set; } = 30;

        [Category("Metro"), Description("Gets or sets the background image displayed in the control.")]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        [Category("Metro"), Description("Gets or sets whether the drop shadow effect apply on form.")]
        public bool DropShadowEffect { get; set; }

        [Category("Metro"), Description("Gets or sets whether the user be able to resize the form or not.")]
        public bool AllowResize { get; set; }

        #endregion Properties

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
            _backgorundImageTrasparency = 0.90f;
            Font = MetroFonts.SemiLight(13);
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

            using (var b = new SolidBrush(BackgroundColor))
            {
                e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
                if (BackgroundImage != null)
                    _mth.DrawImageWithTransparency(e.Graphics, BackgroundImageTransparency, BackgroundImage, ClientRectangle);
            }
            if (ShowBorder)
            {
                using (var p = new Pen(BorderColor, BorderThickness))
                    e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            if (ShowLeftRect)
            {
                using (var b = new LinearGradientBrush(new Rectangle(0, 25, SmallRectThickness, 35), SmallLineColor1, SmallLineColor2, 90))
                {
                    using (var textBrush = new SolidBrush(TextColor))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(0, 40, 10, 35));
                        e.Graphics.DrawString(Text, Font, textBrush, new Point(20, 46));
                    }
                }
            }
            else
            {
                if (ShowHeader)
                {
                    using (var b = new SolidBrush(HeaderColor))
                        e.Graphics.FillRectangle(b, new Rectangle(1, 1, Width - 1, HeaderHeight));
                }

                var textBrush = new SolidBrush(TextColor);
                if (ShowTitle)
                {
                    switch (TextAlign)
                    {
                        case TextAlign.Left:
                            using (var stringFormat = new StringFormat() { LineAlignment = StringAlignment.Center })
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width, HeaderHeight), stringFormat);
                            break;

                        case TextAlign.Center:
                            using (var stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 21, HeaderHeight), stringFormat);
                            break;

                        case TextAlign.Right:
                            using (var stringFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                                e.Graphics.DrawString(Text, Font, textBrush, new Rectangle(20, 0, Width - 26, HeaderHeight), stringFormat);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                textBrush.Dispose();
            }
        }

        #endregion Draw Control

        #region Methods

        private void ResizeForm(ref Message message)
        {
            if (!AllowResize) return;
            var x = (int)(message.LParam.ToInt64() & 65535);
            var y = (int)((message.LParam.ToInt64() & -65536) >> 0x10);
            var point = PointToClient(new Point(x, y));

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
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager MetroStyleManager
        {
            get => _metroStyleManager;
            set
            {
                _metroStyleManager = value;
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
        private MetroStyleManager _metroStyleManager;
        private bool _showLeftRect;
        private bool _showHeader;
        private float _backgorundImageTrasparency;

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
                    ThemeName = "MetroLite";
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
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.FormDictionary)
                        {
                            if (!string.Equals(varkey.Key, null, StringComparison.Ordinal) && varkey.Key != null)
                            {
                                if (varkey.Key == "ForeColor")
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "BackColor")
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "BorderColor")
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "TextColor")
                                    TextColor = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "SmallLineColor1")
                                    SmallLineColor1 = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "SmallLineColor2")
                                    SmallLineColor2 = _utl.HexColor((string)varkey.Value);
                                else if (varkey.Key == "SmallRectThickness")
                                    SmallRectThickness = int.Parse(varkey.Value.ToString());
                                else if (varkey.Key == "HeaderColor")
                                    HeaderColor = _utl.HexColor((string)varkey.Value);
                            }
                            else
                                throw new Exception("FormDictionary is empty");
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

            if ((message.Msg != _WM_NCHITTEST) | !Moveable) return;

            // Allow users to move the form.
            if ((int)message.Result == _HTCLIENT)
                message.Result = new IntPtr(_HTCAPTION);

            // Allow users to resize the form.
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
                if (!DropShadowEffect) return base.CreateParams;
                var cp = base.CreateParams;
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
                AnimateWindow(Handle, 800, User32.AW_HIDE | (UseSlideAnimation ? AnimateWindowFlags.AW_HOR_NEGATIVE | AnimateWindowFlags.AW_SLIDE : AnimateWindowFlags.AW_BLEND));
        }

        #endregion
    }

    #endregion
}