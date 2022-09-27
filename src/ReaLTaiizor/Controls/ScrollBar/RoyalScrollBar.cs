#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalScrollBar

    public class RoyalScrollBar : ControlRoyalBase
    {
        public event EventHandler ValueChanged;

        private Color gutterColor;
        public Color GutterColor
        {
            get => gutterColor;
            set { gutterColor = value; Invalidate(); }
        }

        private Color thumbColor;
        public Color ThumbColor
        {
            get => thumbColor;
            set { thumbColor = value; Invalidate(); }
        }

        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    _value = 0;
                }
                else if (value > Max)
                {
                    _value = Max;
                }
                else
                {
                    _value = value;
                    ValueChanged(this, EventArgs.Empty);
                }
                Invalidate();
            }
        }

        private int min;
        public int Min
        {
            get => min;
            set { min = value; Invalidate(); }
        }

        private int max;
        public int Max
        {
            get => max;
            set
            {
                max = value;

                if (Orientation == Orientation.Vertical)
                {
                    if (max > Height)
                    {
                        thumbSize = Height * (Height / (double)max);
                    }
                    else
                    {
                        thumbSize = 0;
                    }
                }
                else if (Orientation == Orientation.Horizontal)
                {
                    if (max > Width)
                    {
                        thumbSize = Width * (Width / (double)max);
                    }
                    else
                    {
                        thumbSize = 0;
                    }
                }

                Refresh();
                Invalidate();
            }
        }

        private int smallChange;
        public int SmallChange
        {
            get => smallChange;
            set { smallChange = value; Invalidate(); }
        }

        private int largeChange;
        public int LargeChange
        {
            get => largeChange;
            set { largeChange = value; Invalidate(); }
        }

        private double thumbSize;
        private bool thumbSelected;
        private Point lastMousePos;
        private Orientation orientation;
        public Orientation Orientation
        {
            get => orientation;
            set { orientation = value; Invalidate(); }
        }

        public RoyalScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);

            ValueChanged = new EventHandler(OnValueChanged);

            gutterColor = RoyalColors.HotTrackColor;
            thumbColor = RoyalColors.AccentColor;

            Value = 0;
            Min = 0;
            Max = 1;
            SmallChange = 10;
            LargeChange = 50;
            Orientation = Orientation.Vertical;

            thumbSize = 10;
            thumbSelected = false;
        }

        protected void DrawGutter(PaintEventArgs e)
        {
            if (Max > Height)
            {
                e.Graphics.FillRectangle(new SolidBrush(GutterColor), e.ClipRectangle);
            }
        }

        protected void DrawThumb(PaintEventArgs e)
        {
            Rectangle rect = new(0, 0, 10, 10);

            if (Orientation == Orientation.Vertical)
            {
                thumbSize = Height * (Height / (double)max);
                double y = (double)(Height - thumbSize) * (Value / (double)max);

                rect = new(new Point(0, (int)y), new Size(Width, (int)thumbSize));
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = Width * (Width / (double)max);
                double x = (double)(Width - thumbSize) * (Value / (double)max);

                rect = new(new Point((int)x, 0), new Size((int)thumbSize, Height));
            }

            e.Graphics.FillRectangle(new SolidBrush(ThumbColor), rect);
        }

        protected virtual void OnValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Value -= SmallChange;

                if (Value < Min)
                {
                    Value = Min;
                }
            }
            else if (e.Delta < 0)
            {
                Value += SmallChange;

                if (Value > Max)
                {
                    Value = Max;
                }
            }

            Refresh();
            base.OnMouseWheel(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Focus();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle mouseRect = new(e.X, e.Y, 1, 1);
            Rectangle gutterRect = new(0, 0, Width, Height);
            Rectangle thumbRect = new(0, 0, 10, 10);

            if (Orientation == Orientation.Vertical)
            {
                thumbSize = Height * (Height / (double)max);
                double y = (double)(Height - thumbSize) * (Value / (double)max);

                thumbRect = new(0, (int)y, Width, (int)thumbSize);
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = Width * (Width / (double)max);
                double x = (double)(Width - thumbSize) * (Value / (double)max);

                thumbRect = new((int)x, 0, (int)thumbSize, Height);
            }

            if (mouseRect.IntersectsWith(gutterRect))
            {
                if (mouseRect.IntersectsWith(thumbRect))
                {
                    thumbSelected = true;
                }
                else
                {
                    if (Orientation == Orientation.Vertical)
                    {
                        if (mouseRect.Y < thumbRect.Top)
                        {
                            Value -= largeChange;
                        }
                        else if (mouseRect.Y > thumbRect.Bottom)
                        {
                            Value += largeChange;
                        }
                    }
                    else if (Orientation == Orientation.Horizontal)
                    {
                        if (mouseRect.X < thumbRect.Left)
                        {
                            Value -= largeChange;
                        }
                        else if (mouseRect.X > thumbRect.Right)
                        {
                            Value += largeChange;
                        }
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            thumbSelected = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (thumbSelected)
            {
                if (Orientation == Orientation.Vertical)
                {
                    if (e.Y != lastMousePos.Y)
                    {
                        double y = e.Y - (thumbSize / 2);
                        y = Math.Min(y, Height - thumbSize);
                        y = Math.Max(y, 0);

                        double v = Max * (y / (Height - thumbSize));
                        Value = (int)v;
                    }
                }
                else if (Orientation == Orientation.Horizontal)
                {
                    if (e.X != lastMousePos.X)
                    {
                        double x = e.X - (thumbSize / 2);
                        x = Math.Min(x, Width - thumbSize);
                        x = Math.Max(x, 0);

                        double v = Max * (x / (Width - thumbSize));
                        Value = (int)v;
                    }
                }
            }

            lastMousePos = new(e.X, e.Y);
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawGutter(e);
            DrawThumb(e);

            base.OnPaint(e);
        }
    }

    #endregion
}