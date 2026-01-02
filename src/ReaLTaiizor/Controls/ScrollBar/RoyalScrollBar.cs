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

        public int Value
        {
            get;
            set
            {
                if (value < 0)
                {
                    field = 0;
                }
                else if (value > Max)
                {
                    field = Max;
                }
                else
                {
                    field = value;
                    ValueChanged(this, EventArgs.Empty);
                }
                Invalidate();
            }
        }

        public int Min
        {
            get;
            set { field = value; Invalidate(); }
        }

        public int Max
        {
            get;
            set
            {
                field = value;

                if (Orientation == Orientation.Vertical)
                {
                    if (field > Height)
                    {
                        thumbSize = Height * (Height / (double)field);
                    }
                    else
                    {
                        thumbSize = 0;
                    }
                }
                else if (Orientation == Orientation.Horizontal)
                {
                    if (field > Width)
                    {
                        thumbSize = Width * (Width / (double)field);
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

        public int SmallChange
        {
            get;
            set { field = value; Invalidate(); }
        }

        public int LargeChange
        {
            get;
            set { field = value; Invalidate(); }
        }

        private double thumbSize;
        private bool thumbSelected;
        private Point lastMousePos;

        public Orientation Orientation
        {
            get;
            set { field = value; Invalidate(); }
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
                thumbSize = Height * (Height / (double)Max);
                double y = (double)(Height - thumbSize) * (Value / (double)Max);

                rect = new(new Point(0, (int)y), new Size(Width, (int)thumbSize));
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = Width * (Width / (double)Max);
                double x = (double)(Width - thumbSize) * (Value / (double)Max);

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
                thumbSize = Height * (Height / (double)Max);
                double y = (double)(Height - thumbSize) * (Value / (double)Max);

                thumbRect = new(0, (int)y, Width, (int)thumbSize);
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = Width * (Width / (double)Max);
                double x = (double)(Width - thumbSize) * (Value / (double)Max);

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
                            Value -= LargeChange;
                        }
                        else if (mouseRect.Y > thumbRect.Bottom)
                        {
                            Value += LargeChange;
                        }
                    }
                    else if (Orientation == Orientation.Horizontal)
                    {
                        if (mouseRect.X < thumbRect.Left)
                        {
                            Value -= LargeChange;
                        }
                        else if (mouseRect.X > thumbRect.Right)
                        {
                            Value += LargeChange;
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