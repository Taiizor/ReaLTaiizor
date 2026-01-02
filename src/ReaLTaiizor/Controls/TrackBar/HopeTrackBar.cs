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

        public int MinValue
        {
            get;
            set
            {
                if (value > MaxValue || value > _value)
                {
                    return;
                }

                field = value;
                Invalidate();
            }
        } = 0;

        public int MaxValue
        {
            get;
            set
            {
                if (value < MinValue || value < _value)
                {
                    return;
                }

                field = value;
                Invalidate();
            }
        } = 10;

        private int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                _value = value >= MinValue && value <= MaxValue ? value : MinValue;
                Invalidate();
            }
        }

        public bool ShowValue
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        public bool AlwaysValueVisible
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        private int ValueWidth => Convert.ToInt32(_value * (Width - 30) / (MaxValue - MinValue));

        public Color ThemeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color BaseColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(44, 55, 66);

        public Color BarColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = RoundRectangle.BackColor;

        public Color BallonColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color BallonArrowColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color FillBarColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HopeColors.PrimaryColor;

        public Color HeadBorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DodgerBlue;

        public Color HeadColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Black;

        public Color UnknownColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        #endregion

        #region Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = ShowValue ? 45 : 16;
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
                _value = MinValue + Convert.ToInt32((MaxValue - MinValue) * (e.X / (float)Width));
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
            graphics.Clear(BaseColor);

            if (ShowValue && (mouseFlat || AlwaysValueVisible))
            {
                graphics.FillEllipse(new SolidBrush(BallonColor), new RectangleF(ValueWidth - 2 + 5, 1, 18, 18));
                graphics.FillPolygon(new SolidBrush(BallonArrowColor), new PointF[]
                {
                    new(ValueWidth + 1.305F - 2 + 5,13.5F + 1),
                    new(ValueWidth + 7.794F + 9-2 + 5,13.5F + 1),
                    new(ValueWidth + 9-2 + 5,28F)
                });
                graphics.DrawString(_value.ToString(), Font, new SolidBrush(ForeColor), new RectangleF(ValueWidth - 2 + 5, 2, 18, 18), HopeStringAlign.Center);
            }

            graphics.FillRectangle(new SolidBrush(BarColor), new RectangleF(15, Height - 10, Width - 30, 4));
            graphics.FillRectangle(new SolidBrush(UnknownColor), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillRectangle(new SolidBrush(FillBarColor), new RectangleF(15, Height - 10, ValueWidth, 4));
            graphics.FillEllipse(new SolidBrush(HeadBorderColor), new RectangleF(ValueWidth + 5, Height - 17, 16, 16));
            graphics.FillEllipse(new SolidBrush(HeadColor), new RectangleF(ValueWidth + 8, Height - 14, 10, 10));
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