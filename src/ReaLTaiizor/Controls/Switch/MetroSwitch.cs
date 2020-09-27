#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Animate.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroSwitch

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSwitch), "Bitmaps.Switch.bmp")]
    [Designer(typeof(MetroSwitchDesigner))]
    [DefaultEvent("SwitchedChanged")]
    [DefaultProperty("Switched")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroSwitch : Control, iControl, IDisposable
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

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private MetroStyleManager _metroStyleManager;
        private bool _switched;
        private Style _style;
        private int _switchlocation = 0;
        private IntAnimate _animator;

        #endregion Internal Vars

        #region Constructors

        public MetroSwitch()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Cursor = Cursors.Hand;
            _utl = new Utilites();
            _animator = new IntAnimate();
            _animator.Setting(100, 0, 132, EasingType.Linear);
            _animator.Update = (alpha) =>
            {
                _switchlocation = alpha;
                Invalidate(false);
            };
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void UpdateProperties()
        {
            Invalidate();
        }

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.Black;
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(165, 159, 147);
                    DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    SymbolColor = Color.FromArgb(92, 92, 92);
                    UnCheckColor = Color.FromArgb(155, 155, 155);
                    CheckColor = Color.FromArgb(65, 177, 225);
                    DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.FromArgb(30, 30, 30);
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(85, 85, 85);
                    SymbolColor = Color.FromArgb(92, 92, 92);
                    UnCheckColor = Color.FromArgb(155, 155, 155);
                    CheckColor = Color.FromArgb(65, 177, 225);
                    DisabledUnCheckColor = Color.FromArgb(200, 205, 205, 205);
                    DisabledCheckColor = Color.FromArgb(100, 65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.SwitchBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SymbolColor":
                                    SymbolColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "UnCheckColor":
                                    UnCheckColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CheckColor":
                                    CheckColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledUnCheckColor":
                                    DisabledUnCheckColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledCheckColor":
                                    DisabledCheckColor = _utl.HexColor((string)varkey.Value);
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

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var rect = new Rectangle(1, 1, 56, 20);

            var rect2 = new Rectangle(3, 3, 52, 16);

            using (var backBrush = new SolidBrush(BackgroundColor))
            {
                using (var checkback = new SolidBrush(Enabled ? Switched ? CheckColor : UnCheckColor : Switched ? DisabledCheckColor : DisabledUnCheckColor))
                {
                    using (var checkMarkBrush = new SolidBrush(SymbolColor))
                    {
                        using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor, 2))
                        {
                            G.FillRectangle(backBrush, rect);
                            G.FillRectangle(checkback, rect2);
                            G.DrawRectangle(p, rect);
                            G.FillRectangle(checkMarkBrush, new Rectangle((Convert.ToInt32(rect.Width * (_switchlocation / 180.0))), 0, 16, 22));
                        }
                    }
                }
            }
        }

        #endregion Draw Control

        #region Events

        public delegate void SwitchedChangedEventHandler(object sender);

        public event SwitchedChangedEventHandler SwitchedChanged;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Switched = !Switched;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(58, 22);
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

        [Browsable(false)]
        public Enum.Metro.CheckState CheckState { get; set; }

        [Category("Metro"), Description("Gets or sets a value indicating whether the control is checked.")]
        public bool Switched
        {
            get => _switched;
            set
            {
                _switched = value;
                SwitchedChanged?.Invoke(this);
                _animator.Reverse(!value);
                CheckState = value != true ? Enum.Metro.CheckState.Unchecked : Enum.Metro.CheckState.Checked;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the border color.")]
        public Color BorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the Checkd backColor.")]
        public Color CheckColor { get; set; }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the CheckdBackColor while disabled.")]
        public Color DisabledCheckColor { get; set; }

        [Category("Metro"), Description("Gets or sets the Un-Checkd BackColor while disabled.")]
        public Color DisabledUnCheckColor { get; set; }

        [Category("Metro"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the color of the check symbol.")]
        public Color SymbolColor { get; set; }

        [Category("Metro"), Description("Gets or sets the Un-Checkd backColor.")]
        public Color UnCheckColor { get; set; }

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