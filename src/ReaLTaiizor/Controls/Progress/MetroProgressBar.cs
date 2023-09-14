#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroProgressBar

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroProgressBar), "Bitmaps.Progress.bmp")]
    [Designer(typeof(MetroProgressBarDesigner))]
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    [ComVisible(true)]
    public class MetroProgressBar : Control, IMetroControl
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
        private int _value;
        private int _currentValue;

        private bool _isDerivedStyle = true;
        private int _maximum = 100;
        private int _minimum;
        private ProgressOrientation _orientation = ProgressOrientation.Horizontal;
        private Color _backgroundColor;
        private Color _borderColor;
        private Color _progressColor;
        private Color _disabledProgressColor;
        private Color _disabledBackColor;
        private Color _disabledBorderColor;

        #endregion Internal Vars

        #region Constructors

        public MetroProgressBar()
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
                    ProgressColor = Color.FromArgb(65, 177, 225);
                    BorderColor = Color.FromArgb(238, 238, 238);
                    BackgroundColor = Color.FromArgb(238, 238, 238);
                    DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(238, 238, 238);
                    DisabledBackColor = Color.FromArgb(238, 238, 238);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ProgressColor = Color.FromArgb(65, 177, 225);
                    BackgroundColor = Color.FromArgb(38, 38, 38);
                    BorderColor = Color.FromArgb(38, 38, 38);
                    DisabledProgressColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBackColor = Color.FromArgb(38, 38, 38);
                    DisabledBorderColor = Color.FromArgb(38, 38, 38);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ProgressDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ProgressColor":
                                    ProgressColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledProgressColor":
                                    DisabledProgressColor = _utl.HexColor((string)varkey.Value);
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

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);

            using SolidBrush bg = new(Enabled ? BackgroundColor : DisabledBackColor);
            using Pen p = new(Enabled ? BorderColor : DisabledBorderColor);
            using SolidBrush ps = new(Enabled ? ProgressColor : DisabledProgressColor);
            g.FillRectangle(bg, rect);
            if (_currentValue != 0)
            {
                switch (Orientation)
                {
                    case ProgressOrientation.Horizontal:
                        g.FillRectangle(ps, new Rectangle(0, 0, _currentValue - 1, Height - 1));
                        break;
                    case ProgressOrientation.Vertical:
                        g.FillRectangle(ps, new Rectangle(0, Height - _currentValue, Width - 1, _currentValue - 1));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            g.DrawRectangle(p, rect);
        }

        #endregion

        #region Properties

        [Category("Metro"), Description("Gets or sets the current position of the progressbar.")]
        public int Value
        {
            get => _value < 0 ? 0 : _value;
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }

                _value = value;
                RenewCurrentValue();
                ValueChanged?.Invoke(this);
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the maximum value of the progressbar.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the minimum value of the progressbar.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                Refresh();
            }
        }


        [Browsable(false)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the minimum value of the progressbar.")]
        public ProgressOrientation Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                Refresh();
            }
        }

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

        [Category("Metro"), Description("Gets or sets the progress color of the control.")]
        public Color ProgressColor
        {
            get => _progressColor;
            set
            {
                _progressColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the progresscolor of the control whenever while disabled.")]
        public Color DisabledProgressColor
        {
            get => _disabledProgressColor;
            set
            {
                _disabledProgressColor = value;
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

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender);

        private void RenewCurrentValue()
        {
            if (Orientation == ProgressOrientation.Horizontal)
            {
                _currentValue = (int)Math.Round((Value - Minimum) / (double)(Maximum - Minimum) * (Width - 1));
            }
            else
            {
                _currentValue = Convert.ToInt32((Value / (double)Maximum * Height) - 1);
            }
        }

        #endregion
    }

    #endregion
}