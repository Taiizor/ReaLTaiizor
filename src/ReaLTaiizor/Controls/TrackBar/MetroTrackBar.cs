#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Extension.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroTrackBar

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroTrackBar), "Bitmaps.Slider.bmp")]
    [Designer(typeof(MetroTrackBarDesigner))]
    [DefaultProperty("Value")]
    [DefaultEvent("Scroll")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroTrackBar : Control, iControl
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

        private Style _style;
        private MetroStyleManager _metroStyleManager;
        private bool _variable;
        private Rectangle _track;
        private int _maximum;
        private int _minimum;
        private int _value;
        private int _currentValue;

        #endregion Internal Vars

        #region Constructors

        public MetroTrackBar()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            _maximum = 100;
            _minimum = 0;
            _value = 0;
            _currentValue = Convert.ToInt32(Value / (double)(Maximum) - (2 * Width));
            UpdateStyles();
            _utl = new Utilites();
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    HandlerColor = Color.FromArgb(180, 180, 180);
                    BackgroundColor = Color.FromArgb(205, 205, 205);
                    ValueColor = Color.FromArgb(65, 177, 225);
                    DisabledBackColor = Color.FromArgb(235, 235, 235);
                    DisabledValueColor = Color.FromArgb(205, 205, 205);
                    DisabledHandlerColor = Color.FromArgb(196, 196, 196);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    HandlerColor = Color.FromArgb(143, 143, 143);
                    BackgroundColor = Color.FromArgb(90, 90, 90);
                    ValueColor = Color.FromArgb(65, 177, 225);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledValueColor = Color.FromArgb(109, 109, 109);
                    DisabledHandlerColor = Color.FromArgb(90, 90, 90);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.TrackBarDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "HandlerColor":
                                    HandlerColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "ValueColor":
                                    ValueColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledValueColor":
                                    DisabledValueColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledHandlerColor":
                                    DisabledHandlerColor = _utl.HexColor((string)varkey.Value);
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

            Cursor = Cursors.Hand;

            using (var bg = new SolidBrush(Enabled ? BackgroundColor : DisabledBackColor))
            {
                using (var v = new SolidBrush(Enabled ? ValueColor : DisabledValueColor))
                {
                    using (var vc = new SolidBrush(Enabled ? HandlerColor : DisabledHandlerColor))
                    {
                        G.FillRectangle(bg, new Rectangle(0, 6, Width, 4));
                        if (_currentValue != 0)
                            G.FillRectangle(v, new Rectangle(0, 6, _currentValue, 4));
                        G.FillRectangle(vc, _track);
                    }
                }
            }
        }

        #endregion

        #region Properties

        [Category("Metro"), Description("Gets or sets the upper limit of the range this TrackBar is working with.")]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                RenewCurrentValue();
                MoveTrack();
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the lower limit of the range this TrackBar is working with.")]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (!(value < 0))
                {
                    _minimum = value;
                    RenewCurrentValue();
                    MoveTrack();
                    Invalidate();
                }
            }
        }

        [Category("Metro"), Description("Gets or sets a numeric value that represents the current position of the scroll box on the track bar.")]
        public int Value
        {
            get => _value;
            set
            {
                if (value != _value)
                {
                    _value = value;
                    RenewCurrentValue();
                    MoveTrack();
                    Invalidate();
                    Scroll?.Invoke(this);
                }
            }
        }

        [Browsable(false)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description(" Gets or sets the value color in normal mouse sate.")]
        public Color ValueColor { get; set; }

        [Category("Metro"), Description("Gets or sets the handler color.")]
        public Color HandlerColor { get; set; }

        [Category("Metro"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets the value of the control whenever while disabled.")]
        public Color DisabledValueColor { get; set; }

        [Category("Metro"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets the handler color while the control disabled.")]
        public Color DisabledHandlerColor { get; set; }

        #endregion

        #region Events

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_variable && e.X > -1 && e.X < Width + 1)
                Value = Minimum + (int)Math.Round((double)(Maximum - Minimum) * e.X / Width);
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Height > 0)
            {
                RenewCurrentValue();
                _track = new Rectangle(_currentValue, 0, 6, 16);
                _variable = new Rectangle(_currentValue, 0, 6, 16).Contains(e.Location);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _variable = false;
            base.OnMouseUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left)
            {
                if (Value != 0)
                    Value -= 1;
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
            {
                if (Value != Maximum)
                    Value += 1;
            }
            base.OnKeyDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            RenewCurrentValue();
            MoveTrack();
            Height = 16;
            Invalidate();
            base.OnResize(e);
        }

        private void MoveTrack()
        {
            _track = new Rectangle(_currentValue, 0, 6, 16);
        }

        public void RenewCurrentValue()
        {
            _currentValue = Convert.ToInt32(Math.Round((double)(Value - Minimum) / (Maximum - Minimum) * (Width - 6)));
            Invalidate();
        }

        #endregion
    }

    #endregion
}