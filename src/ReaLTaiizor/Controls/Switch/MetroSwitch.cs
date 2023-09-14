#region Imports

using ReaLTaiizor.Animate.Metro;
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
    #region MetroSwitch

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroSwitch), "Bitmaps.Switch.bmp")]
    [Designer(typeof(MetroSwitchDesigner))]
    [DefaultEvent("SwitchedChanged")]
    [DefaultProperty("Switched")]
    [ComVisible(true)]
    public class MetroSwitch : Control, IMetroControl, IDisposable
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

        private MetroStyleManager _styleManager;
        private bool _switched;
        private Style _style;
        private int _switchLocation;
        private readonly IntAnimate _animator;

        private bool _isDerivedStyle = true;
        private Enum.Metro.CheckState _checkState;
        private Color _borderColor;
        private Color _checkColor;
        private Color _disabledBorderColor;
        private Color _disabledCheckColor;
        private Color _disabledUnCheckColor;
        private Color _backgroundColor;
        private Color _symbolColor;
        private Color _unCheckColor;

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
            base.Cursor = Cursors.Hand;
            _utl = new Utilites();
            _animator = new IntAnimate();
            _animator.Setting(100, 0, 132);
            _animator.Update = (alpha) =>
            {
                _switchLocation = alpha;
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
            if (!IsDerivedStyle)
            {
                return;
            }

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
                    ThemeName = "MetroLight";
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
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.SwitchBoxDictionary)
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
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new(1, 1, 56, 20);
            Rectangle rect2 = new(3, 3, 52, 16);

            using SolidBrush backBrush = new(BackgroundColor);
            using SolidBrush checkback = new(Enabled ? Switched ? CheckColor : UnCheckColor : Switched ? DisabledCheckColor : DisabledUnCheckColor);
            using SolidBrush checkMarkBrush = new(SymbolColor);
            using Pen p = new(Enabled ? BorderColor : DisabledBorderColor, 2);
            g.FillRectangle(backBrush, rect);
            g.FillRectangle(checkback, rect2);
            g.DrawRectangle(p, rect);
            g.FillRectangle(checkMarkBrush, new Rectangle(Convert.ToInt32(rect.Width * (_switchLocation / 180.0)), 0, 16, 22));
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
            Size = new(58, 22);
            Invalidate();
        }

        /*
            protected override void WndProc(ref Message m)
            {
                //_utl.SmoothCursor(ref m);
                _utl.SmoothCursor(ref m, base.Cursor);
                //_utl.NormalCursor(ref m, base.Cursor);

                base.WndProc(ref m);
            }
        */

        #endregion Events

        #region Properties

        [Browsable(false)]
        public Enum.Metro.CheckState CheckState
        {
            get => _checkState;
            set
            {
                _checkState = value;
                Refresh();
            }
        }

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
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the Checkd backColor.")]
        public Color CheckColor
        {
            get => _checkColor;
            set
            {
                _checkColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor
        {
            get => _disabledBorderColor;
            set
            {
                _disabledBorderColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the CheckdBackColor while disabled.")]
        public Color DisabledCheckColor
        {
            get => _disabledCheckColor;
            set
            {
                _disabledCheckColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the Un-Checkd BackColor while disabled.")]
        public Color DisabledUnCheckColor
        {
            get => _disabledUnCheckColor;
            set
            {
                _disabledUnCheckColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the color of the check symbol.")]
        public Color SymbolColor
        {
            get => _symbolColor;
            set
            {
                _symbolColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the Un-Checkd backColor.")]
        public Color UnCheckColor
        {
            get => _unCheckColor;
            set
            {
                _unCheckColor = value;
                Refresh();
            }
        }

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

        #endregion Properties

        #region Disposing

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    #endregion
}