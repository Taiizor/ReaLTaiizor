﻿#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialSlider

    public class MaterialSlider : Control, MaterialControlI
    {
        #region "Private members"

        private bool _mousePressed;
        private int _mouseX;
        //private int _indicatorSize;
        private bool _hovered = false;
        private Rectangle _indicatorRectangle;
        private Rectangle _indicatorRectangleNormal;
        private Rectangle _indicatorRectanglePressed;
        private Rectangle _textRectangle;
        private Rectangle _valueRectangle;
        private Rectangle _sliderRectangle;

        private const int _activeTrack = 6;
        private const int _inactiveTrack = 4;
        private const int _thumbRadius = 20;
        private const int _thumbRadiusHoverPressed = 40;

        #endregion

        #region "Public Properties"
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [Category("Material")]
        [DefaultValue(Directions.Normal)]
        [Description("Define control direction change value with mouse wheel")]
        public Directions ScrollDirection { get; set; }

        protected int _stepChange = 2;
        [Category("Material")]
        [Description("Define control step change value")]
        public int StepChange
        {
            get => _stepChange;
            set => _stepChange = value.Clamp(1, RangeMax);
        }

        private int _value;
        [DefaultValue(50)]
        [Category("Material")]
        [Description("Define control value")]
        public int Value
        {
            get => _value;
            set
            {
                if (value < _rangeMin)
                {
                    _value = _rangeMin;
                }
                else if (value > _rangeMax)
                {
                    _value = _rangeMax;
                }
                else
                {
                    _value = value;
                }
                //_mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width) - _thumbRadius / 2));
                _mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width - _thumbRadius)));
                RecalcutlateIndicator();
            }
        }

        private int _valueMax;
        [DefaultValue(0)]
        [Category("Material")]
        [Description("Define position indicator maximum value. Ignored when set to 0.")]
        public int ValueMax
        {
            get => _valueMax;
            set
            {
                if (value > _rangeMax)
                {
                    _valueMax = _rangeMax;
                }
                else if (value < _rangeMin)
                {
                    _valueMax = _rangeMin;
                }
                else
                {
                    _valueMax = value;
                }
            }
        }

        private int _rangeMax;
        [DefaultValue(100)]
        [Category("Material")]
        [Description("Define control range maximum value")]
        public int RangeMax
        {
            get => _rangeMax;
            set
            {
                _rangeMax = value;
                //_mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width) - _thumbRadius / 2));
                _mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width - _thumbRadius)));
                RecalcutlateIndicator();
            }
        }

        private int _rangeMin;
        [DefaultValue(0)]
        [Category("Material")]
        [Description("Define control range minimum value")]
        public int RangeMin
        {
            get => _rangeMin;
            set
            {
                _rangeMin = value;
                //_mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width) - _thumbRadius / 2));
                _mouseX = _sliderRectangle.X + ((int)((double)_value / (double)(RangeMax - RangeMin) * (double)(_sliderRectangle.Width - _thumbRadius)));
                RecalcutlateIndicator();
            }
        }

        private string _text;
        [DefaultValue("MyData")]
        [Category("Material")]
        [Description("Set control text")]
        public override string Text
        {
            get => _text;
            set
            {
                _text = value;
                UpdateRects();
                Invalidate();
            }
        }

        private string _valueSuffix;
        [DefaultValue("")]
        [Category("Material")]
        [Description("Set control value suffix text")]
        public string ValueSuffix
        {
            get => _valueSuffix;
            set
            {
                _valueSuffix = value;
                UpdateRects();
            }
        }

        private string _valuePrefix;
        [DefaultValue("")]
        [Category("Material")]
        [Description("Set control value prefix text")]
        public string ValuePrefix
        {
            get => _valuePrefix;
            set
            {
                _valuePrefix = value;
                UpdateRects();
            }
        }

        private bool _showText;
        [DefaultValue(true)]
        [Category("Material"), DisplayName("Show Text")]
        [Description("Show text")]
        public bool ShowText
        {
            get => _showText;
            set { _showText = value; UpdateRects(); Invalidate(); }
        }

        private bool _showValue;
        [DefaultValue(true)]
        [Category("Material"), DisplayName("Show Value")]
        [Description("Show value")]
        public bool ShowValue
        {
            get => _showValue;
            set { _showValue = value; UpdateRects(); Invalidate(); }
        }

        private bool _useAccentColor;
        [Category("Material"), DefaultValue(false), DisplayName("Use Accent Color")]
        public bool UseAccentColor
        {
            get => _useAccentColor;
            set { _useAccentColor = value; Invalidate(); }
        }

        private MaterialSkinManager.FontType _fontType = MaterialSkinManager.FontType.Body1;

        [Category("Material"),
        DefaultValue(typeof(MaterialSkinManager.FontType), "Body1")]
        public MaterialSkinManager.FontType FontType
        {
            get => _fontType;
            set
            {
                _fontType = value;
                Font = SkinManager.GetFontByType(_fontType);
                Refresh();
            }
        }


        #endregion

        #region "Events"

        [Category("Behavior")]
        [Description("Occurs when value change.")]
        public delegate void ValueChanged(object sender, int newValue);
        public event ValueChanged onValueChanged;

        #endregion

        #region "Enums"

        public enum Directions
        {
            Normal,
            Reverse
        }

        #endregion

        public MaterialSlider()
        {
            SetStyle(ControlStyles.Selectable, true);
            ForeColor = SkinManager.TextHighEmphasisColor; // Color.Black;
            RangeMax = 100;
            RangeMin = 0;
            Size = new Size(250, _thumbRadiusHoverPressed);
            Text = "My Data";
            Value = 50;
            ValueSuffix = "";
            ValuePrefix = "";
            ShowText = true;
            ShowValue = true;
            UseAccentColor = false;

            UpdateRects();

            //EnabledChanged += MaterialSlider_EnabledChanged;

            DoubleBuffered = true;

        }

        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();
        //}

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = _thumbRadiusHoverPressed;
            UpdateRects();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _hovered = true;
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _hovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && e.Y > _indicatorRectanglePressed.Top && e.Y < _indicatorRectanglePressed.Bottom)
            {
                _mousePressed = true;
                UpdateValue(e);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int scrollLines = SystemInformation.MouseWheelScrollLines;
            Value += e.Delta / 40 / scrollLines * StepChange * (ScrollDirection == Directions.Normal ? 1 : -1);
            Value = Value.Clamp(RangeMin, RangeMax);
            onValueChanged?.Invoke(this, _value);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hovered = true;
            if (!this.Focused)
            {
                this.Focus();
            }

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hovered = false;
            if (this.Focused)
            {
                this.Parent.Focus();
            }

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _mousePressed = false;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_mousePressed)
            {
                UpdateValue(e);
            }
        }

        private void UpdateValue(MouseEventArgs e)
        {
            int v = 0;
            if (e.X >= _sliderRectangle.X + (_thumbRadius / 2) && e.X <= _sliderRectangle.Right - (_thumbRadius / 2))
            {
                _mouseX = e.X - (_thumbRadius / 2);
                double ValuePerPx = ((double)(RangeMax - RangeMin)) / (_sliderRectangle.Width - _thumbRadius);
                v = (int)(ValuePerPx * (_mouseX - _sliderRectangle.X));
                //if (_valueMax!=0 && v > _valueMax) v = _valueMax;
            }
            else if (e.X < _sliderRectangle.X)// + (_thumbRadius / 2))
            {
                _mouseX = _sliderRectangle.X;
                v = _rangeMin;
            }
            else if (e.X > _sliderRectangle.Right - _thumbRadius)// / 2)
            {
                _mouseX = _sliderRectangle.Right - _thumbRadius;
                v = _rangeMax;
            }

            if (_valueMax != 0 && v > _valueMax)
            {
                Value = _valueMax;
            }
            else
            {
                if (v != _value)
                {
                    _value = v;
                    onValueChanged?.Invoke(this, _value);
                }
                RecalcutlateIndicator();
            }
        }

        private void UpdateRects()
        {
            Size textSize;
            Size valueSize;
            using (MaterialNativeTextRenderer NativeText = new(CreateGraphics()))
            {
                textSize = NativeText.MeasureLogString(_showText ? Text : "", SkinManager.GetLogFontByType(_fontType));
                valueSize = NativeText.MeasureLogString(_showValue ? _valuePrefix + RangeMax.ToString() + _valueSuffix : "", SkinManager.GetLogFontByType(_fontType));
            }
            _valueRectangle = new Rectangle(Width - valueSize.Width - (_thumbRadiusHoverPressed / 4), 0, valueSize.Width + (_thumbRadiusHoverPressed / 4), Height);
            _textRectangle = new Rectangle(0, 0, textSize.Width + (_thumbRadiusHoverPressed / 4), Height);
            _sliderRectangle = new Rectangle(_textRectangle.Right, 0, _valueRectangle.Left - _textRectangle.Right, _thumbRadius);
            _mouseX = _sliderRectangle.X + ((int)(((double)_value / (double)(_rangeMax - _rangeMin) * (double)_sliderRectangle.Width) - (_thumbRadius / 2)));
            RecalcutlateIndicator();
        }

        private void RecalcutlateIndicator()
        {
            _indicatorRectangle = new Rectangle(_mouseX, (Height - _thumbRadius) / 2, _thumbRadius, _thumbRadius);
            _indicatorRectangleNormal = new Rectangle(_indicatorRectangle.X, (Height / 2) - (_thumbRadius / 2), _thumbRadius, _thumbRadius);
            _indicatorRectanglePressed = new Rectangle(_indicatorRectangle.X + (_thumbRadius / 2) - (_thumbRadiusHoverPressed / 2), (Height / 2) - (_thumbRadiusHoverPressed / 2), _thumbRadiusHoverPressed, _thumbRadiusHoverPressed);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);

            Color _inactiveTrackColor;
            Color _accentColor;
            Brush _accentBrush;
            Brush _disabledBrush;
            Color _disabledColor;
            Color _thumbHoverColor;
            Color _thumbPressedColor;

            if (_useAccentColor)
            {
                _accentColor = SkinManager.ColorScheme.AccentColor;
            }
            else
            {
                _accentColor = SkinManager.ColorScheme.PrimaryColor;
            }

            _accentBrush = new SolidBrush(_accentColor);
            _disabledBrush = new SolidBrush(Color.FromArgb(255, 158, 158, 158));

            if (SkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                _disabledColor = Color.FromArgb((int)(2.55 * 30), 255, 255, 255);
                _inactiveTrackColor = _accentColor.Darken(0.25f);
            }
            else
            {
                _disabledColor = Color.FromArgb((int)(2.55 * (_hovered ? 38 : 26)), 0, 0, 0);
                _inactiveTrackColor = _accentColor.Lighten(0.6f);
            }

            //_disabledBrush = new SolidBrush(_disabledColor);
            //_thumbHoverColor = Color.FromArgb((int)(2.55 * 15), (Value == 0 ? Color.Gray : _accentColor));
            //_thumbPressedColor = Color.FromArgb((int)(2.55 * 30), (Value == 0 ? Color.Gray : _accentColor));            _thumbHoverColor = Color.FromArgb((int)(2.55 * 15), (Value == 0 ? Color.Gray : _accentColor));
            _thumbHoverColor = Color.FromArgb((int)(2.55 * 15), _accentColor);
            _thumbPressedColor = Color.FromArgb((int)(2.55 * 30), _accentColor);
            //Pen LinePen = new Pen(_disabledColor, _inactiveTrack);

            //Draw track
            //g.DrawLine(LinePen, _indicatorSize / 2, Height / 2 + (Height - _indicatorSize) / 2, Width - _indicatorSize / 2, Height / 2 + (Height - _indicatorSize) / 2);
            //g.DrawLine(LinePen, _sliderRectangle.X + (_indicatorSize / 2), Height / 2 , _sliderRectangle.Right - (_indicatorSize / 2), Height / 2 );

            GraphicsPath _inactiveTrackPath = CreateRoundRect(_sliderRectangle.X + (_thumbRadius / 2), _sliderRectangle.Y + (Height / 2) - (_inactiveTrack / 2), _sliderRectangle.Width - _thumbRadius, _inactiveTrack, 2);
            //g.FillPath(_disabledBrush, _inactiveTrackPath);
            GraphicsPath _activeTrackPath = CreateRoundRect(_sliderRectangle.X + (_thumbRadius / 2), _sliderRectangle.Y + (Height / 2) - (_activeTrack / 2), _indicatorRectangleNormal.X - _sliderRectangle.X, _activeTrack, 2);

            if (Enabled)
            {
                //Draw inactive track
                g.FillPath(new SolidBrush(_inactiveTrackColor), _inactiveTrackPath);

                //Draw active track
                //g.DrawLine(SkinManager.ColorScheme.AccentPen, _indicatorSize / 2, Height / 2 + (Height - _indicatorSize) / 2, _indicatorRectangleNormal.X, Height / 2 + (Height - _indicatorSize) / 2);
                //g.DrawLine(AccentPen, _sliderRectangle.X + (_indicatorSize / 2), Height / 2 , _indicatorRectangleNormal.X + (_indicatorSize / 2), Height / 2 ) ;

                g.FillPath(_accentBrush, _activeTrackPath);

                if (_mousePressed)
                {
                    //g.FillEllipse(_accentBrush, _indicatorRectanglePressed);
                    g.FillEllipse(_accentBrush, _indicatorRectangleNormal);
                    g.FillEllipse(new SolidBrush(_thumbPressedColor), _indicatorRectanglePressed);

                }
                else
                {
                    g.FillEllipse(_accentBrush, _indicatorRectangleNormal);

                    if (_hovered)
                    {
                        g.FillEllipse(new SolidBrush(_thumbHoverColor), _indicatorRectanglePressed);
                    }
                }
            }
            else
            {
                //Draw inactive track
                g.FillPath(new SolidBrush(_disabledColor.Lighten(0.25f)), _inactiveTrackPath);

                //Draw active track
                g.FillPath(_disabledBrush, _activeTrackPath);
                g.FillEllipse(_disabledBrush, _indicatorRectangleNormal);
            }

            using MaterialNativeTextRenderer NativeText = new(g);
            if (_showText == true)
            {
                // Draw text
                NativeText.DrawTransparentText(
                Text,
                SkinManager.GetLogFontByType(_fontType),
                Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                _textRectangle.Location,
                _textRectangle.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            if (_showValue == true)
            {
                // Draw value
                NativeText.DrawTransparentText(
                    ValuePrefix + Value.ToString() + ValueSuffix,
                    SkinManager.GetLogFontByType(_fontType),
                    Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                    _valueRectangle.Location,
                    _valueRectangle.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Right | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

        }
    }

    #endregion
}