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
    #region HopeRoundPrgoressBar

    public class HopeRoundProgressBar : Control
    {

        private int tempValue = 0;
        private int _valueNumber = 0;
        public int ValueNumber
        {
            get { return _valueNumber; }
            set
            {
                _valueNumber = value > 100 ? 100 : (value < 0 ? 0 : value);
                Invalidate();
            }
        }

        private float _roundWidth = 6;

        private bool _isError = false;
        public bool IsError
        {
            get { return _isError; }
            set
            {
                _isError = value;
                Invalidate();
            }
        }

        private string _PercentText = "%";
        public string PercentText
        {
            get { return _PercentText; }
            set { _PercentText = value; }
        }

        private Color _BorderColor = HopeColors.OneLevelBorder;
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        private Color _DangerColor = HopeColors.Danger;
        public Color DangerColor
        {
            get { return _DangerColor; }
            set { _DangerColor = value; }
        }

        private Color _DangerTextColorA = HopeColors.Danger;
        public Color DangerTextColorA
        {
            get { return _DangerTextColorA; }
            set { _DangerTextColorA = value; }
        }

        private Color _DangerTextColorB = HopeColors.Danger;
        public Color DangerTextColorB
        {
            get { return _DangerTextColorB; }
            set { _DangerTextColorB = value; }
        }

        private Color _FullTextColorA = HopeColors.Success;
        public Color FullTextColorA
        {
            get { return _FullTextColorA; }
            set { _FullTextColorA = value; }
        }

        private Color _FullTextColorB = HopeColors.Success;
        public Color FullTextColorB
        {
            get { return _FullTextColorB; }
            set { _FullTextColorB = value; }
        }

        private Color _BarColor = HopeColors.PrimaryColor;
        public Color BarColor
        {
            get { return _BarColor; }
            set { _BarColor = value; }
        }

        private Color _FullBarColor = HopeColors.Success;
        public Color FullBarColor
        {
            get { return _FullBarColor; }
            set { _FullBarColor = value; }
        }

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = Height;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            graphics.FillEllipse(new SolidBrush(_BorderColor), new Rectangle(0, 0, Width, Height));

            if (_isError)
            {
                graphics.FillPie(new SolidBrush(_DangerColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);

                graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - _roundWidth * 2, Width - _roundWidth * 2));
                graphics.DrawLine(new Pen(_DangerTextColorA, 2f), Width / 2 - 6, Height / 2 - 6, Width / 2 + 6, Height / 2 + 6);
                graphics.DrawLine(new Pen(_DangerTextColorB, 2f), Width / 2 - 6, Height / 2 + 6, Width / 2 + 6, Height / 2 - 6);
            }
            else
            {
                if (_valueNumber == 100)
                {
                    graphics.FillPie(new SolidBrush(_FullBarColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);
                    graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - _roundWidth * 2, Width - _roundWidth * 2));
                    graphics.DrawLine(new Pen(_FullTextColorA, 2f), Width / 2 - 6, Height / 2, Width / 2 - 3, Height / 2 + 6);
                    graphics.DrawLine(new Pen(_FullTextColorB, 2f), Width / 2 + 6, Height / 2 - 6, Width / 2 - 3, Height / 2 + 6);
                }
                else
                {
                    graphics.FillPie(new SolidBrush(_BarColor), new Rectangle(0, 0, Width, Width), 0, _valueNumber * 3.6f);
                    graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(_roundWidth, _roundWidth, Width - _roundWidth * 2, Width - _roundWidth * 2));
                    graphics.DrawString(_valueNumber.ToString() + _PercentText, Font, new SolidBrush(ForeColor), new RectangleF(_roundWidth, _roundWidth, Width - (_roundWidth * 2), Width - (_roundWidth * 2)), HopeStringAlign.Center);
                }
            }
        }

        public HopeRoundProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12f);
            BackColor = Color.White;
            ForeColor = HopeColors.PrimaryColor;
        }
    }

    #endregion
}