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
    #region HopeTrackBar

    public class HopeTrackBar : Control
    {
        #region Variables
        private RectangleF valueBar = new(7, 5, 0, 6);
        private RectangleF valueRect = new(0, 1, 14, 14);
        private bool mouseFlat = false;
        #endregion

        #region Settings

        private int _minValue = 0;
        public int MinValue
        {
            get => _minValue;
            set
            {
                if (value > _maxValue || value > _value)
                {
                    return;
                }

                _minValue = value;
                Invalidate();
            }
        }

        private int _maxValue = 10;
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                if (value < _minValue || value < _value)
                {
                    return;
                }

                _maxValue = value;
                Invalidate();
            }
        }

        private int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                _value = value >= _minValue && value <= _maxValue ? value : _minValue;
                Invalidate();
            }
        }

        private bool _showValue = false;
        public bool ShowValue
        {
            get => _showValue;
            set
            {
                _showValue = value;
                Invalidate();
            }
        }

        private bool _AlwaysValueVisible = false;
        public bool AlwaysValueVisible
        {
            get => _AlwaysValueVisible;
            set
            {
                _AlwaysValueVisible = value;
                Invalidate();
            }
        }

        private int ValueWidth => Convert.ToInt32(_value * (Width - 30) / (_maxValue - _minValue));

        private Color _themeColor = HopeColors.PrimaryColor;
        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.FromArgb(44, 55, 66);
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _BarColor = RoundRectangle.BackColor;
        public Color BarColor
        {
            get => _BarColor;
            set
            {
                _BarColor = value;
                Invalidate();
            }
        }

        private Color _BallonColor = HopeColors.PrimaryColor;
        public Color BallonColor
        {
            get => _BallonColor;
            set
            {
                _BallonColor = value;
                Invalidate();
            }
        }

        private Color _BallonArrowColor = HopeColors.PrimaryColor;
        public Color BallonArrowColor
        {
            get => _BallonArrowColor;
            set
            {
                _BallonArrowColor = value;
                Invalidate();
            }
        }

        private Color _FillBarColor = HopeColors.PrimaryColor;
        public Color FillBarColor
        {
            get => _FillBarColor;
            set
            {
                _FillBarColor = value;
                Invalidate();
            }
        }

        private Color _HeadBorderColor = Color.DodgerBlue;
        public Color HeadBorderColor
        {
            get => _HeadBorderColor;
            set
            {
                _HeadBorderColor = value;
                Invalidate();
            }
        }

        private Color _HeadColor = Color.Black;
        public Color HeadColor
        {
            get => _HeadColor;
            set
            {
                _HeadColor = value;
                Invalidate();
            }
        }

        private Color _UnknownColor = Color.White;
        public Color UnknownColor
        {
            get => _UnknownColor;
            set
            {
                _UnknownColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = _showValue ? 45 : 16;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                mouseFlat = new RectangleF(ValueWidth + 7, Height - 15, 14, 14).Contains(e.Location);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseFlat && e.X > -1 && e.X < (Width + 1))
            {
                _value = _minValue + Convert.ToInt32((_maxValue - _minValue) * (e.X / (float)Width));
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseFlat = false;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(_BaseColor);

            if (ShowValue && (mouseFlat || _AlwaysValueVisible))
            {
                graphics.FillEllipse(new SolidBrush(_BallonColor), new RectangleF(ValueWidth - 2 + 5, 1, 18, 18));
                graphics.FillPolygon(new SolidBrush(_BallonArrowColor), new PointF[]
                {
                    new PointF(ValueWidth + 1.305F - 2 + 5,13.5F + 1),
                    new PointF(ValueWidth + 7.794F + 9-2 + 5,13.5F + 1),
                    new PointF(ValueWidth + 9-2 + 5,28F)
                });
                graphics.DrawString(_value.ToString(), Font, new SolidBrush(ForeColor), new RectangleF(ValueWidth - 2 + 5, 2, 18, 18), HopeStringAlign.Center);
            }

            graphics.FillRectangle(new SolidBrush(_BarColor), new RectangleF(15, Height - 10, Width - 30, 4));
            graphics.FillRectangle(new SolidBrush(_UnknownColor), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillRectangle(new SolidBrush(_FillBarColor), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillEllipse(new SolidBrush(_HeadBorderColor), new RectangleF(ValueWidth + 5, Height - 17, 16, 16));
            graphics.FillEllipse(new SolidBrush(_HeadColor), new RectangleF(ValueWidth + 8, Height - 14, 10, 10));
        }

        public HopeTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 45;
            Font = new("Segoe UI", 8F);
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}