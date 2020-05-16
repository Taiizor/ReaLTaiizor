#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeTrackBar

    public class HopeTrackBar : Control
    {
        #region Variables
        private RectangleF valueBar = new RectangleF(7, 5, 0, 6);
        private RectangleF valueRect = new RectangleF(0, 1, 14, 14);
        private bool mouseFlat = false;
        #endregion

        #region Settings
        private Color _themeColor = HopeColors.PrimaryColor;
        public Color ThemeColor
        {
            get { return _themeColor; }
            set
            {
                _themeColor = value;
                Invalidate();
            }
        }

        private int _minValue = 0;
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                if (value > _maxValue || value > _value)
                    return;
                _minValue = value;
                Invalidate();
            }
        }

        private int _maxValue = 10;
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value < _minValue || value < _value)
                    return;
                _maxValue = value;
                Invalidate();
            }
        }

        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value >= _minValue && value <= _maxValue ? value : _minValue;
                Invalidate();
            }
        }

        private bool _showValue = false;
        public bool ShowValue
        {
            get { return _showValue; }
            set
            {
                _showValue = value;
                Invalidate();
            }
        }

        private bool _AlwaysValueVisible = false;
        public bool AlwaysValueVisible
        {
            get { return _AlwaysValueVisible; }
            set
            {
                _AlwaysValueVisible = value;
                Invalidate();
            }
        }

        private int ValueWidth
        {
            get { return Convert.ToInt32(_value * (Width - 30) / (_maxValue - _minValue)); }
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
                _value = _minValue + Convert.ToInt32((float)(_maxValue - _minValue) * ((float)e.X / (float)Width));
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
            graphics.Clear(Parent.BackColor);

            if (ShowValue && (mouseFlat || _AlwaysValueVisible))
            {
                graphics.FillEllipse(new SolidBrush(_themeColor), new RectangleF(ValueWidth - 2 + 5, 1, 18, 18));
                graphics.FillPolygon(new SolidBrush(_themeColor), new PointF[]
                {
                    new PointF(ValueWidth+1.305F-2+5,13.5F+1),
                    new PointF(ValueWidth+7.794F+9-2+5,13.5F+1),
                    new PointF(ValueWidth+9-2+5,28F)
                });
                graphics.DrawString(_value.ToString(), Font, new SolidBrush(ForeColor), new RectangleF(ValueWidth - 2 + 5, 2, 18, 18), HopeStringAlign.Center);
            }

            graphics.FillRectangle(new SolidBrush(RoundRectangle.BackColor), new RectangleF(15, Height - 10, Width - 30, 4));
            graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillRectangle(new SolidBrush(_themeColor), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillEllipse(new SolidBrush(_themeColor), new RectangleF(ValueWidth + 5, Height - 17, 16, 16));
            graphics.FillEllipse(new SolidBrush(BackColor), new RectangleF(ValueWidth + 8, Height - 14, 10, 10));
        }

        public HopeTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 45;
            Font = new Font("Segoe UI", 8F);
            BackColor = Color.White;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}