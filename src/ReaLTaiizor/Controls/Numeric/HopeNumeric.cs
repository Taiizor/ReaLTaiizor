#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeNumeric

    public class HopeNumeric : Control
    {
        #region Variables
        private bool enterFlag = false;
        private bool focus = false;
        private readonly TextBox textBox = new();
        private RectangleF upRectangleF = new();
        private RectangleF downRectangleF = new();
        private Point mousePoint = new();
        private string textValue = string.Empty;

        public enum NumericStyle
        {
            LeftRight = 0,
            TopDown = 1
        }
        #endregion

        #region Settings
        private NumericStyle _style = NumericStyle.LeftRight;
        public NumericStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                if (_style == NumericStyle.LeftRight)
                {
                    downRectangleF = new(0, 0, Height, Height);
                    upRectangleF = new(Width - Height, 0, Height, Height);
                }
                else
                {
                    downRectangleF = new(Width - Height, Height / 2, Height, Height / 2);
                    upRectangleF = new(Width - Height, 0, Height, Height / 2);
                }
                Invalidate();
            }
        }

        private float _minNum = 0;
        public float MinNum
        {
            get => _minNum;
            set => _minNum = value > _maxNum ? _maxNum : value;
        }

        private float _maxNum = 10;
        public float MaxNum
        {
            get => _maxNum;
            set => _maxNum = value < _minNum ? _minNum : value;
        }

        private float _value = 0;
        public float ValueNumber
        {
            get => _value;
            set
            {
                if (value > _maxNum || value < _minNum)
                {
                    if (value > _maxNum)
                    {
                        value = _maxNum;
                    }
                    else
                    {
                        value = _minNum;
                    }
                }

                _value = value;
                Invalidate();
            }
        }

        public bool EnterKey { get; set; } = true;

        public float Step { get; set; } = 1;

        private int _precision = 0;
        public int Precision
        {
            get => _precision;
            set
            {
                _precision = (value is < 0 or > 6) ? 0 : value;
                Invalidate();
            }
        }

        public Color BaseColor { get; set; } = HopeColors.FourLevelBorder;

        public Color BorderColorA { get; set; } = HopeColors.PlaceholderText;

        public Color BorderHoverColorA { get; set; } = HopeColors.PrimaryColor;

        public Color BorderColorB { get; set; } = HopeColors.PlaceholderText;

        public Color HoverButtonTextColorA { get; set; } = HopeColors.PrimaryColor;

        public Color HoverButtonTextColorB { get; set; } = HopeColors.PrimaryColor;

        public Color ButtonTextColorA { get; set; } = HopeColors.SecondaryText;

        public Color ButtonTextColorB { get; set; } = HopeColors.SecondaryText;

        #endregion

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            enterFlag = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            enterFlag = false;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mousePoint = e.Location;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (focus && !EnterKey)
            {
                focus = false;

                if (float.TryParse(textBox.Text, out float f))
                {
                    ValueNumber = f;
                }

                textBox.Text = Math.Round(_value, Precision).ToString();
            }

            if (upRectangleF.Contains(mousePoint))
            {
                ValueNumber += Step;
            }

            if (downRectangleF.Contains(mousePoint))
            {
                ValueNumber -= Step;
            }

            base.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 32;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            GraphicsPath bg = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(BaseColor), bg);
            graphics.DrawPath(new(enterFlag ? BorderHoverColorA : BorderColorA, 1f), bg);

            if ((!focus && EnterKey) || (!focus && !EnterKey))
            {
                textBox.Text = Math.Round(_value, Precision).ToString();
            }

            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            switch (_style)
            {
                case NumericStyle.LeftRight:
                    textBox.Size = new(Width - (2 * Height), Height - 2);
                    textBox.Location = new(Height, 5);
                    graphics.DrawLine(new(BorderColorB, 0.5f), textBox.Location.X - 0.5f, 1, textBox.Location.X - 0.5f, Height - 1);
                    break;
                case NumericStyle.TopDown:
                    textBox.Size = new(Width - Height - 2, Height - 2);
                    textBox.Location = new(2, 5);
                    graphics.DrawLine(new(BorderColorB, 0.5f), textBox.Location.X + textBox.Width + 0.5f, Height / 2, Width - 1, Height / 2);
                    break;
            }
            graphics.DrawString("+", new Font("Segoe UI", 14f), new SolidBrush((upRectangleF.Contains(mousePoint) && enterFlag) ? HoverButtonTextColorA : ButtonTextColorA), upRectangleF, HopeStringAlign.Center);
            graphics.DrawString("-", new Font("Segoe UI", 14f), new SolidBrush((downRectangleF.Contains(mousePoint) && enterFlag) ? HoverButtonTextColorB : ButtonTextColorB), downRectangleF, HopeStringAlign.Center);
            graphics.DrawLine(new(BorderColorB, 0.5f), textBox.Location.X + textBox.Width + 0.5f, 1, textBox.Location.X + textBox.Width + 0.5f, Height - 1);
            graphics.FillRectangle(new SolidBrush(BackColor), textBox.Location.X, 1, textBox.Width, Height - 2);
            base.Controls.Add(textBox);
        }

        public HopeNumeric()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);

            Width = 120;
            Height = 30;
            Style = NumericStyle.LeftRight;
            Cursor = Cursors.Hand;
            BackColor = Color.White;
            ForeColor = Color.Black;

            #region textBox
            textBox.BorderStyle = BorderStyle.None;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.Font = Font;
            textBox.Cursor = Cursors.IBeam;
            textBox.BackColor = BackColor;
            textBox.KeyPress += TextBox_KeyPress;
            textBox.GotFocus += TextBox_GotFocus;
            textBox.LostFocus += TextBox_LostFocus;
            #endregion
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (EnterKey && e.KeyChar == 13)
            {
                if (float.TryParse(textBox.Text, out float f))
                {
                    ValueNumber = f;
                }

                textBox.Text = Math.Round(_value, Precision).ToString();

                base.Focus();
            }
            else if (!EnterKey && e.KeyChar == 13)
            {
                base.Focus();
            }
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            focus = true;
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (focus)
            {
                if (!EnterKey && textBox.Text != Math.Round(_value, Precision).ToString())
                {
                    if (float.TryParse(textBox.Text, out float f))
                    {
                        ValueNumber = f;
                    }

                    textBox.Text = Math.Round(_value, Precision).ToString();
                }
                else if (EnterKey && textBox.Text != Math.Round(_value, Precision).ToString())
                {
                    textBox.Text = Math.Round(_value, Precision).ToString();
                }

                focus = false;
            }
        }
    }

    #endregion
}