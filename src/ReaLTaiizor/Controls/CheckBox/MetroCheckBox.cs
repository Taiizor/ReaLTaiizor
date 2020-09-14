#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.Drawing.Drawing2D;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Animate.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroCheckBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroCheckBox), "Bitmaps.CheckBox.bmp")]
    [Designer(typeof(MetroCheckBoxDesigner))]
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroCheckBox : Control, iControl, IDisposable
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
        public StyleManager StyleManager
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
        private StyleManager _styleManager;
        private bool _checked;
        private IntAnimate _animator;

        #endregion Internal Vars

        #region Constructors

        public MetroCheckBox()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Font = MetroFonts.SemiBold(10);
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            _utl = new Utilites();
            _animator = new IntAnimate();
            _animator.Setting(100, 0, 255, EasingType.Linear);
            _animator.Update = (alpha) => Invalidate();
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.Black;
                    BackgroundColor = Color.White;
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    CheckSignColor = Color.FromArgb(65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(85, 85, 85);
                    CheckSignColor = Color.FromArgb(65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                        foreach (var varkey in StyleManager.CheckBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CheckColor":
                                    CheckSignColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CheckedStyle":
                                    if ((string)varkey.Value == "Sign")
                                        SignStyle = SignStyle.Sign;
                                    else if ((string)varkey.Value == "Shape")
                                        SignStyle = SignStyle.Shape;
                                    break;
                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var rect = new Rectangle(0, 0, 16, 15);
            var alpha = _animator.Value;

            using (var backBrush = new SolidBrush(Enabled ? BackgroundColor : Color.FromArgb(238, 238, 238)))
            {
                using (var checkMarkPen = new Pen(Enabled ? Checked || _animator.Active ? Color.FromArgb(alpha, CheckSignColor) : BackgroundColor : Color.FromArgb(alpha, DisabledBorderColor), 2))
                {
                    using (var checkMarkBrush = new SolidBrush(Enabled ? Checked || _animator.Active ? Color.FromArgb(alpha, CheckSignColor) : BackgroundColor : DisabledBorderColor))
                    {
                        using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor))
                        {
                            using (var sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center })
                            {
                                using (var tb = new SolidBrush(ForeColor))
                                {
                                    G.FillRectangle(backBrush, rect);
                                    G.DrawRectangle(Enabled ? p : checkMarkPen, rect);
                                    DrawSymbol(G, checkMarkPen, checkMarkBrush);
                                    G.DrawString(Text, Font, tb, new Rectangle(19, 2, Width, Height - 4), sf);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawSymbol(Graphics g, Pen pen, SolidBrush solidBrush)
        {
            if (solidBrush == null) throw new ArgumentNullException(nameof(solidBrush));
            if (SignStyle == SignStyle.Sign)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawLines(pen, new[]
                {
                    new Point(3, 7),
                    new Point(7, 10),
                    new Point(13, 3)
                });
                g.SmoothingMode = SmoothingMode.None;
            }
            else
            {
                g.FillRectangle(solidBrush, new Rectangle(3, 3, 11, 10));
            }
        }

        #endregion Draw Control

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;

        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = !Checked;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 16;
            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == User32.WM_SETCURSOR)
            {
                User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        #endregion Events

        #region Properties

        [Category("Metro"), Description("Gets or sets a value indicating whether the control is checked.")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                CheckedChanged?.Invoke(this);
                _animator.Reverse(!value);
                CheckState = value ? Enum.Metro.CheckState.Checked : Enum.Metro.CheckState.Unchecked;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the the sign style of check.")]
        public SignStyle SignStyle { get; set; } = SignStyle.Sign;

        [Browsable(false)]
        public Enum.Metro.CheckState CheckState { get; set; }

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the form backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the border color.")]
        public Color BorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the color of the check symbol.")]
        public Color CheckSignColor { get; set; }

        #endregion Properties

        #region Disposing

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #endregion

    }

    #endregion
}