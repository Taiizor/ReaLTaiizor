#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroNumeric

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroNumeric), "Bitmaps.Numeric.bmp")]
    [Designer(typeof(MetroNumericDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    public class MetroNumeric : Control, IMetroControl
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

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private Point _point;
        private int _value;
        private readonly Timer _holdTimer;
        private int _holdInterval = 10;

        private bool _isDerivedStyle = true;
        private int _maximum = 100;
        private int _minimum;
        private Color _backgroundColor;
        private Color _disabledForeColor;
        private Color _disabledBackColor;
        private Color _disabledBorderColor;
        private Color _borderColor;
        private Color _symbolsColor;

        #endregion Internal Vars

        #region Constructors

        public MetroNumeric()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            base.Font = MetroFonts.SemiLight(10);
            BackColor = Color.Transparent;
            _mth = new Methods();
            _utl = new Utilites();
            ApplyTheme();
            _point = new(0, 0);
            _holdTimer = new Timer()
            {
                Interval = _holdInterval,
                AutoReset = true,
                Enabled = false
            };
            _holdTimer.Elapsed += HoldTimer_Tick;
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            const char plus = '+';
            const char minus = '-';

            using SolidBrush bg = new(Enabled ? BackColor : DisabledBackColor);
            using Pen p = new(Enabled ? BorderColor : DisabledBorderColor);
            using SolidBrush s = new(Enabled ? SymbolsColor : DisabledForeColor);
            using SolidBrush tb = new(Enabled ? ForeColor : DisabledForeColor);
            using Font f2 = MetroFonts.SemiBold(18);
            using StringFormat sf = new() { LineAlignment = StringAlignment.Center };
            g.FillRectangle(bg, rect);
            g.DrawString(plus.ToString(), f2, s, new Rectangle(Width - 45, 1, 25, Height - 1), sf);
            g.DrawString(minus.ToString(), f2, s, new Rectangle(Width - 25, -1, 20, Height - 1), sf);
            g.DrawString(Value.ToString(), Font, tb, new Rectangle(0, 0, Width - 50, Height - 1), _mth.SetPosition(StringAlignment.Far));
            g.DrawRectangle(p, rect);

        }

        #endregion

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
                    ForeColor = Color.FromArgb(20, 20, 20);
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(150, 150, 150);
                    SymbolsColor = Color.FromArgb(128, 128, 128);
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackColor = Color.FromArgb(34, 34, 34);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    SymbolsColor = Color.FromArgb(110, 110, 110);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.NumericDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SymbolsColor":
                                    SymbolsColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
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

        private void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Properties

        [Category("Metro"), Description("Gets or sets the hold interval of the Numeric.")]
        public int HoldInterval
        {
            get => _holdInterval;
            set
            {
                _holdInterval = value;
                _holdTimer.Interval = _holdInterval;
            }
        }

        [Category("Metro"), Description("Gets or sets the increment number of the Numeric.")]
        public int Increment { get; set; } = 1;

        [Category("Metro"), Description("Gets or sets the decrement number of the Numeric.")]
        public int Decrement { get; set; } = 1;

        [Category("Metro"), Description("Gets or sets the maximum number of the Numeric.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the minimum number of the Numeric.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the current number of the Numeric.")]
        public int Value
        {
            get => _value;
            set
            {
                if (value <= Maximum & value >= Minimum)
                {
                    _value = value;
                }

                Invalidate();
            }
        }

        [Browsable(false)]
        public sealed override Color BackColor => Color.Transparent;

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

        [Category("Metro"), Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set
            {
                _disabledForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set
            {
                _disabledBackColor = value;
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

        [Category("Metro"), Description("Gets or sets forecolor used by the control.")]
        public override Color ForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets arrow color used by the control.")]
        public Color SymbolsColor
        {
            get => _symbolsColor;
            set
            {
                _symbolsColor = value;
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

        #endregion

        #region Events

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _point = e.Location;
            Invalidate();
            Cursor = _point.X > Width - 50 ? Cursors.Hand : Cursors.IBeam;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Revaluate();
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 26;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_point.X <= Width - 45 || _point.X >= Width - 3)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                _holdTimer.Enabled = true;
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _holdTimer.Enabled = false;
        }

        private void HoldTimer_Tick(object sender, EventArgs args)
        {
            Revaluate();
        }

        private void Revaluate()
        {
            if (_point.X <= Width - 45 || _point.X >= Width - 3)
            {
                return;
            }

            if (_point.X > Width - 45 && _point.X < Width - 25)
            {
                if (Value + Increment <= Maximum)
                {
                    Value += Increment;
                }
                else
                {
                    Value = Maximum;
                }
            }
            else
            {
                if (Value - Decrement >= Minimum)
                {
                    Value -= Decrement;
                }
                else
                {
                    Value = Minimum;
                }
            }
        }

        #endregion
    }

    #endregion
}