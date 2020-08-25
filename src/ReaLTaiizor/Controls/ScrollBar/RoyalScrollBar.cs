#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalScrollBar

    public class RoyalScrollBar : ControlRoyalBase
    {
        public event EventHandler ValueChanged;

        Color gutterColor;
        public Color GutterColor
        {
            get { return gutterColor; }
            set { gutterColor = value; Invalidate(); }
        }

        Color thumbColor;
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; Invalidate(); }
        }

        int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                    _value = 0;
                else if (value > Max)
                    _value = Max;
                else
                {
                    _value = value;
                    ValueChanged(this, EventArgs.Empty);
                }
                Invalidate();
            }
        }

        int min;
        public int Min
        {
            get { return min; }
            set { min = value; Invalidate(); }
        }

        int max;
        public int Max
        {
            get { return max; }
            set
            {
                max = value;

                if (Orientation == Orientation.Vertical)
                {
                    if (max > Height)
                        thumbSize = (double)Height * ((double)Height / (double)max);
                    else
                        thumbSize = 0;
                }
                else if (Orientation == Orientation.Horizontal)
                {
                    if (max > Width)
                        thumbSize = (double)Width * ((double)Width / (double)max);
                    else
                        thumbSize = 0;
                }

                Refresh();
                Invalidate();
            }
        }

        int smallChange;
        public int SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; Invalidate(); }
        }

        int largeChange;
        public int LargeChange
        {
            get { return largeChange; }
            set { largeChange = value; Invalidate(); }
        }

        double thumbSize;
        bool thumbSelected;
        Point lastMousePos;

        Orientation orientation;
        public Orientation Orientation
        {
            get { return orientation; }
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
                e.Graphics.FillRectangle(new SolidBrush(GutterColor), e.ClipRectangle);
        }

        protected void DrawThumb(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);

            if (Orientation == Orientation.Vertical)
            {
                thumbSize = (double)Height * ((double)Height / (double)max);
                double y = (double)(Height - thumbSize) * ((double)Value / (double)max);

                rect = new Rectangle(new Point(0, (int)y), new Size(Width, (int)thumbSize));
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = (double)Width * ((double)Width / (double)max);
                double x = (double)(Width - thumbSize) * ((double)Value / (double)max);

                rect = new Rectangle(new Point((int)x, 0), new Size((int)thumbSize, Height));
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
                    Value = Min;
            }
            else if (e.Delta < 0)
            {
                Value += SmallChange;

                if (Value > Max)
                    Value = Max;
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
            Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
            Rectangle gutterRect = new Rectangle(0, 0, Width, Height);
            Rectangle thumbRect = new Rectangle(0, 0, 10, 10);

            if (Orientation == Orientation.Vertical)
            {
                thumbSize = (double)Height * ((double)Height / (double)max);
                double y = (double)(Height - thumbSize) * ((double)Value / (double)max);

                thumbRect = new Rectangle(0, (int)y, Width, (int)thumbSize);
            }
            else if (Orientation == Orientation.Horizontal)
            {
                thumbSize = (double)Width * ((double)Width / (double)max);
                double x = (double)(Width - thumbSize) * ((double)Value / (double)max);

                thumbRect = new Rectangle((int)x, 0, (int)thumbSize, Height);
            }

            if (mouseRect.IntersectsWith(gutterRect))
            {
                if (mouseRect.IntersectsWith(thumbRect))
                    thumbSelected = true;
                else
                {
                    if (Orientation == Orientation.Vertical)
                    {
                        if (mouseRect.Y < thumbRect.Top)
                            Value -= largeChange;
                        else if (mouseRect.Y > thumbRect.Bottom)
                            Value += largeChange;
                    }
                    else if (Orientation == Orientation.Horizontal)
                    {
                        if (mouseRect.X < thumbRect.Left)
                            Value -= largeChange;
                        else if (mouseRect.X > thumbRect.Right)
                            Value += largeChange;
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
                        double y = (double)e.Y - (thumbSize / 2);
                        y = Math.Min(y, (Height - thumbSize));
                        y = Math.Max(y, 0);

                        double v = (double)Max * (y / ((double)Height - thumbSize));
                        Value = (int)v;
                    }
                }
                else if (Orientation == Orientation.Horizontal)
                {
                    if (e.X != lastMousePos.X)
                    {
                        double x = (double)e.X - (thumbSize / 2);
                        x = Math.Min(x, (Width - thumbSize));
                        x = Math.Max(x, 0);

                        double v = (double)Max * (x / ((double)Width - thumbSize));
                        Value = (int)v;
                    }
                }
            }

            lastMousePos = new Point(e.X, e.Y);
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