#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroScrollBar

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroScrollBar), "Bitmaps.ScrollBar.bmp")]
    [Designer(typeof(MetroScrollBarDesigner))]
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    public class MetroScrollBar : Control, IMetroControl
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
        private int _minimum;
        private int _maximum;
        private int _value;
        private int _val;
        private Rectangle _bar;
        private Rectangle _thumb;
        private bool _showThumb;
        private int _thumbSize;
        private MouseMode _thumbState;

        private bool _isDerivedStyle = true;
        private int _smallChange;
        private int _largeChange;
        private ScrollOrientate _orientation;
        private Color _disabledForeColor;
        private Color _disabledBackColor;

        #endregion Internal Vars

        #region Constructors

        public MetroScrollBar()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            SetDefaults();
            _utl = new Utilites();
            ApplyTheme();
        }

        private void SetDefaults()
        {
            _minimum = 0;
            _maximum = 100;
            _value = 0;
            _thumbSize = 20;
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
                    ForeColor = Color.FromArgb(65, 177, 225);
                    BackColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(65, 177, 225);
                    BackColor = Color.FromArgb(30, 30, 30);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ScrollBarDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
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

        public void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle r = new(0, 0, Width, Height);

            using SolidBrush bg = new(Enabled ? BackColor : DisabledBackColor);
            using SolidBrush thumbBrush = new(Enabled ? ForeColor : DisabledForeColor);
            g.FillRectangle(bg, r);
            g.FillRectangle(thumbBrush, _thumb);
        }

        #endregion

        #region Properties

        [Category("Metro"), Description("Gets or sets the lower limit of the scrollable range.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                if (value > _value)
                {
                    _value = value;
                }
                else if (value > _maximum)
                {
                    _maximum = value;
                }

                InvalidateLayout();
            }
        }

        [Category("Metro"), Description("Gets or sets the upper limit of the scrollable range.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value < _value)
                {
                    _value = value;
                }
                else if (value > _minimum)
                {
                    _maximum = value;
                }

                if (Orientation != ScrollOrientate.Vertical)
                {
                    if (Orientation == ScrollOrientate.Horizontal)
                    {
                        _thumbSize = value > Width ? Convert.ToInt32(Width * (Width / (double)_maximum)) : 0;
                    }
                }
                else
                {
                    _thumbSize = value > Height ? Convert.ToInt32(Height * (Height / (double)_maximum)) : 0;
                }

                InvalidateLayout();
            }
        }

        [Category("Metro"), Description("Gets or sets a numeric value that represents the current position of the scroll bar box.")]
        public int Value
        {
            get => _value;
            set
            {
                if (value > Maximum)
                {
                    _value = Maximum;
                }
                else if (value < Minimum)
                {
                    _value = Minimum;
                }
                else
                {
                    _value = value;
                }

                InvalidatePosition();
                Scroll?.Invoke(this);
            }
        }

        [Category("Metro"), Description("Gets or sets the distance to move a scroll bar in response to a small scroll command.")]
        [DefaultValue(1)]
        public int SmallChange
        {
            get => _smallChange;
            set
            {
                _smallChange = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the distance to move a scroll bar in response to a large scroll command.")]
        [DefaultValue(10)]
        public int LargeChange
        {
            get => _largeChange;
            set
            {
                _largeChange = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the scroll bar orientation.")]
        [DefaultValue(ScrollOrientate.Horizontal)]
        public ScrollOrientate Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the scroll bar forecolor.")]
        public override Color ForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets backcolor used by the control.")]
        public override Color BackColor { get; set; }

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

        protected override void OnSizeChanged(EventArgs e)
        {
            InvalidateLayout();
        }

        private void InvalidateLayout()
        {
            _bar = new(0, 0, Width, Height);
            _showThumb = Maximum - Minimum > 0;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    if (_showThumb)
                    {
                        _thumb = new(0, 0, Width, _thumbSize);
                    }

                    break;
                case ScrollOrientate.Horizontal:
                    if (_showThumb)
                    {
                        _thumb = new(0, 0, Width, _thumbSize);
                    }

                    break;
            }

            Scroll?.Invoke(this);
            InvalidatePosition();
        }

        public event ScrollEventHandler Scroll;

        public delegate void ScrollEventHandler(object sender);

        private void InvalidatePosition()
        {
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    _thumb.Y = Convert.ToInt32(CurrentValue() * (_bar.Height - _thumbSize));
                    break;
                case ScrollOrientate.Horizontal:
                    _thumb.X = Convert.ToInt32(CurrentValue() * (_bar.Width - _thumbSize));
                    break;
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left || !_showThumb)
            {
                return;
            }

            if (_thumb.Contains(e.Location))
            {
                _thumbState = MouseMode.Pushed;
                Invalidate();
                return;
            }
            _val = Orientation switch
            {
                ScrollOrientate.Vertical => e.Y < _thumb.Y ? Value - LargeChange : Value + LargeChange,
                ScrollOrientate.Horizontal => e.X < _thumb.X ? Value - LargeChange : Value + LargeChange,
                _ => throw new ArgumentOutOfRangeException(),
            };
            Value = Math.Min(Math.Max(_val, Minimum), Maximum);
            InvalidatePosition();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!(_thumbState == MouseMode.Pushed | !_showThumb))
            {
                return;
            }

            int thumbPosition;
            int thumbBounds;
            switch (Orientation)
            {
                case ScrollOrientate.Vertical:
                    thumbPosition = e.Y - (_thumbSize / 2);
                    thumbBounds = _bar.Height - _thumbSize;
                    _val = Convert.ToInt32((double)thumbPosition / thumbBounds * (Maximum - Minimum)) - Minimum;
                    break;
                case ScrollOrientate.Horizontal:
                    thumbPosition = e.X - (_thumbSize / 2);
                    thumbBounds = _bar.Width - _thumbSize;
                    _val = Convert.ToInt32((double)thumbPosition / thumbBounds * (Maximum - Minimum)) - Minimum;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Value = Math.Min(Math.Max(_val, Minimum), Maximum);
            InvalidatePosition();

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _thumbState = _thumb.Contains(e.Location) ? MouseMode.Hovered : MouseMode.Normal;
            _thumbState = Orientation switch
            {
                ScrollOrientate.Vertical => (e.Location.Y < 16) | (e.Location.Y > Width - 16) ? MouseMode.Hovered : MouseMode.Normal,
                ScrollOrientate.Horizontal => e.Location.X < 16 | e.Location.X > Width - 16 ? MouseMode.Hovered : MouseMode.Normal,
                _ => throw new ArgumentOutOfRangeException(),
            };
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _thumbState = MouseMode.Normal;
            Invalidate();
        }

        private double CurrentValue()
        {
            return (double)(Value - Minimum) / (Maximum - Minimum);
        }

        #endregion
    }

    #endregion
}