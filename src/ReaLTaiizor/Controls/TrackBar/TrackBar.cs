#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region TrackBar

    [DefaultEvent("ValueChanged")]
    public class TrackBar : Control
    {

        #region Enums

        public enum ValueDivisor
        {
            By1 = 1,
            By10 = 10,
            By100 = 100,
            By1000 = 1000
        }

        #endregion
        #region Variables

        private Rectangle FillValue;
        private Rectangle PipeBorder;
        private Rectangle TrackBarHandleRect;
        private bool Cap;
        private int ValueDrawer;

        private Size ThumbSize = new(14, 14);
        private Rectangle TrackThumb;
        private int _Value = 0;

        #endregion
        #region Properties

        public int Minimum
        {
            get;
            set
            {

                if (value >= Maximum)
                {
                    value = Maximum - 10;
                }

                if (_Value < value)
                {
                    _Value = value;
                }

                field = value;
                Invalidate();
            }
        } = 0;

        public int Maximum
        {
            get;
            set
            {

                if (value <= Minimum)
                {
                    value = Minimum + 10;
                }

                if (_Value > value)
                {
                    _Value = value;
                }

                field = value;
                Invalidate();
            }
        } = 10;

        public delegate void ValueChangedEventHandler();
        private ValueChangedEventHandler ValueChangedEvent;

        public event ValueChangedEventHandler ValueChanged
        {
            add => ValueChangedEvent = (ValueChangedEventHandler)System.Delegate.Combine(ValueChangedEvent, value);
            remove => ValueChangedEvent = (ValueChangedEventHandler)System.Delegate.Remove(ValueChangedEvent, value);
        }

        public int Value
        {
            get => _Value;
            set
            {
                if (_Value != value)
                {
                    if (value < Minimum)
                    {
                        _Value = Minimum;
                    }
                    else
                    {
                        if (value > Maximum)
                        {
                            _Value = Maximum;
                        }
                        else
                        {
                            _Value = value;
                        }
                    }
                    Invalidate();
                    ValueChangedEvent?.Invoke();
                }
            }
        }

        public ValueDivisor ValueDivison
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = ValueDivisor.By1;

        [Browsable(false)]
        public float ValueToSet
        {
            get => _Value / (int)ValueDivison;
            set => Value = (int)(value * (int)ValueDivison);
        }

        public bool JumpToMouse
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        #endregion
        #region EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            checked
            {
                bool flag = Cap && e.X > -1 && e.X < Width + 1;
                if (flag)
                {
                    Value = Minimum + (int)Math.Round((Maximum - Minimum) * (e.X / (double)Width));
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ValueDrawer = (int)Math.Round((_Value - Minimum) / (double)(Maximum - Minimum) * (Width - 11));
                TrackBarHandleRect = new(ValueDrawer, 0, 25, 25);
                Cap = TrackBarHandleRect.Contains(e.Location);
                Focus();
                if (JumpToMouse)
                {
                    Value = Minimum + (int)Math.Round((Maximum - Minimum) * (e.X / (double)Width));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }

        #endregion

        public TrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            Size = new(80, 22);
            MinimumSize = new(47, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 22;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            TrackThumb = new(7, 10, Width - 16, 2);
            PipeBorder = new(1, 10, Width - 3, 2);

            try
            {
                ValueDrawer = (int)Math.Round((_Value - Minimum) / (double)(Maximum - Minimum) * Width);
            }
            catch (Exception)
            {
                //
            }

            TrackBarHandleRect = new(ValueDrawer, 0, 3, 20);

            G.FillRectangle(new SolidBrush(Color.FromArgb(124, 131, 137)), PipeBorder);
            FillValue = new(0, 10, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 3);

            G.ResetClip();

            G.SmoothingMode = SmoothingMode.Default;
            G.DrawRectangle(new(Color.FromArgb(124, 131, 137)), PipeBorder); // Draw pipe border
            G.FillRectangle(new SolidBrush(Color.FromArgb(32, 34, 37)), FillValue);

            G.ResetClip();

            G.SmoothingMode = SmoothingMode.HighQuality;

            G.FillEllipse(new SolidBrush(Color.FromArgb(32, 34, 37)), TrackThumb.X + (int)Math.Round(unchecked(TrackThumb.Width * (Value / (double)Maximum))) - (int)Math.Round(ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round(TrackThumb.Height / 2.0) - (int)Math.Round(ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
            G.DrawEllipse(new(Color.FromArgb(32, 34, 37)), TrackThumb.X + (int)Math.Round(unchecked(TrackThumb.Width * (Value / (double)Maximum))) - (int)Math.Round(ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round(TrackThumb.Height / 2.0) - (int)Math.Round(ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
        }
    }

    #endregion
}