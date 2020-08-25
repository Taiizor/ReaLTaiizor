#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeNumeric

    public class HopeNumeric : Control
    {

        #region Variables
        private bool enterFlag = false;
        private System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
        private RectangleF upRectangleF = new RectangleF();
        private RectangleF downRectangleF = new RectangleF();
        private Point mousePoint = new Point();

        private Color _BaseColor = HopeColors.FourLevelBorder;
        private Color _BorderColorA = HopeColors.PlaceholderText;
        private Color _BorderHoverColorA = HopeColors.PrimaryColor;
        private Color _BorderColorB = HopeColors.PlaceholderText;
        private Color _HoverButtonTextColorA = HopeColors.PrimaryColor;
        private Color _HoverButtonTextColorB = HopeColors.PrimaryColor;
        private Color _ButtonTextColorA = HopeColors.SecondaryText;
        private Color _ButtonTextColorB = HopeColors.SecondaryText;

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
            get { return _style; }
            set
            {
                _style = value;
                if (_style == NumericStyle.LeftRight)
                {
                    downRectangleF = new RectangleF(0, 0, Height, Height);
                    upRectangleF = new RectangleF(Width - Height, 0, Height, Height);
                }
                else
                {
                    downRectangleF = new RectangleF(Width - Height, Height / 2, Height, Height / 2);
                    upRectangleF = new RectangleF(Width - Height, 0, Height, Height / 2);
                }
                Invalidate();
            }
        }

        private float _minNum = 0;
        public float MinNum
        {
            get { return _minNum; }
            set
            {
                _minNum = value > _maxNum ? _maxNum : value;
            }
        }

        private float _maxNum = 10;
        public float MaxNum
        {
            get { return _maxNum; }
            set
            {
                _maxNum = value < _minNum ? _minNum : value;
            }
        }

        private float _value = 0;
        public float ValueNumber
        {
            get { return _value; }
            set
            {
                if (value > _maxNum || value < _minNum)
                    return;
                else
                {
                    _value = value;
                    Invalidate();
                }
            }
        }

        private float _step = 1;
        public float Step
        {
            get { return _step; }
            set
            {
                _step = value;
            }
        }

        private int _precision = 0;
        public int Precision
        {
            get { return _precision; }
            set
            {
                _precision = (value < 0 || value > 6) ? 0 : value;
                Invalidate();
            }
        }

        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }

        public Color BorderColorA
        {
            get { return _BorderColorA; }
            set { _BorderColorA = value; }
        }

        public Color BorderHoverColorA
        {
            get { return _BorderHoverColorA; }
            set { _BorderHoverColorA = value; }
        }

        public Color BorderColorB
        {
            get { return _BorderColorB; }
            set { _BorderColorB = value; }
        }

        public Color HoverButtonTextColorA
        {
            get { return _HoverButtonTextColorA; }
            set { _HoverButtonTextColorA = value; }
        }

        public Color HoverButtonTextColorB
        {
            get { return _HoverButtonTextColorB; }
            set { _HoverButtonTextColorB = value; }
        }

        public Color ButtonTextColorA
        {
            get { return _ButtonTextColorA; }
            set { _ButtonTextColorA = value; }
        }

        public Color ButtonTextColorB
        {
            get { return _ButtonTextColorB; }
            set { _ButtonTextColorB = value; }
        }

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
            if (upRectangleF.Contains(mousePoint))
                ValueNumber += Step;
            if (downRectangleF.Contains(mousePoint))
                ValueNumber -= Step;
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

            var bg = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            graphics.FillPath(new SolidBrush(_BaseColor), bg);
            graphics.DrawPath(new Pen(enterFlag ? _BorderHoverColorA : _BorderColorA, 1f), bg);

            textBox.Text = Math.Round(_value, Precision).ToString();
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;
            switch (_style)
            {
                case NumericStyle.LeftRight:
                    textBox.Size = new Size(Width - 2 * Height, Height - 2);
                    textBox.Location = new Point(Height, 5);
                    graphics.DrawLine(new Pen(_BorderColorB, 0.5f), textBox.Location.X - 0.5f, 1, textBox.Location.X - 0.5f, Height - 1);
                    break;
                case NumericStyle.TopDown:
                    textBox.Size = new Size(Width - Height - 2, Height - 2);
                    textBox.Location = new Point(2, 5);
                    graphics.DrawLine(new Pen(_BorderColorB, 0.5f), textBox.Location.X + textBox.Width + 0.5f, Height / 2, Width - 1, Height / 2);
                    break;
            }
            graphics.DrawString("+", new Font("Segoe UI", 14f), new SolidBrush((upRectangleF.Contains(mousePoint) && enterFlag) ? _HoverButtonTextColorA : _ButtonTextColorA), upRectangleF, HopeStringAlign.Center);
            graphics.DrawString("-", new Font("Segoe UI", 14f), new SolidBrush((downRectangleF.Contains(mousePoint) && enterFlag) ? _HoverButtonTextColorB : _ButtonTextColorB), downRectangleF, HopeStringAlign.Center);
            graphics.DrawLine(new Pen(_BorderColorB, 0.5f), textBox.Location.X + textBox.Width + 0.5f, 1, textBox.Location.X + textBox.Width + 0.5f, Height - 1);
            graphics.FillRectangle(new SolidBrush(BackColor), textBox.Location.X, 1, textBox.Width, Height - 2);
            base.Controls.Add(textBox);
        }

        public HopeNumeric()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);

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
            #endregion
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                float f;
                if (float.TryParse(textBox.Text, out f))
                    ValueNumber = f;
                base.Focus();
            }
        }
    }

    #endregion
}