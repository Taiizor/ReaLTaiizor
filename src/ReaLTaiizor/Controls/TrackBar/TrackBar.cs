#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

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

        private Size ThumbSize = new Size(14, 14);
        private Rectangle TrackThumb;

        private int _Minimum = 0;
        private int _Maximum = 10;
        private int _Value = 0;

        private bool _JumpToMouse = false;
        private ValueDivisor DividedValue = ValueDivisor.By1;

        #endregion
        #region Properties

        public int Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {

                if (value >= _Maximum)
                    value = _Maximum - 10;
                if (_Value < value)
                    _Value = value;

                _Minimum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {

                if (value <= _Minimum)
                    value = _Minimum + 10;
                if (_Value > value)
                    _Value = value;

                _Maximum = value;
                Invalidate();
            }
        }

        public delegate void ValueChangedEventHandler();
        private ValueChangedEventHandler ValueChangedEvent;

        public event ValueChangedEventHandler ValueChanged
        {
            add
            {
                ValueChangedEvent = (ValueChangedEventHandler)System.Delegate.Combine(ValueChangedEvent, value);
            }
            remove
            {
                ValueChangedEvent = (ValueChangedEventHandler)System.Delegate.Remove(ValueChangedEvent, value);
            }
        }

        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value != value)
                {
                    if (value < _Minimum)
                        _Value = _Minimum;
                    else
                    {
                        if (value > _Maximum)
                            _Value = _Maximum;
                        else
                            _Value = value;
                    }
                    Invalidate();
                    ValueChangedEvent?.Invoke();
                }
            }
        }

        public ValueDivisor ValueDivison
        {
            get
            {
                return DividedValue;
            }
            set
            {
                DividedValue = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public float ValueToSet
        {
            get
            {
                return _Value / (int)DividedValue;
            }
            set
            {
                Value = (int)(value * (int)DividedValue);
            }
        }

        public bool JumpToMouse
        {
            get
            {
                return _JumpToMouse;
            }
            set
            {
                _JumpToMouse = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            checked
            {
                bool flag = Cap && e.X > -1 && e.X < Width + 1;
                if (flag)
                    Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)Width));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ValueDrawer = (int)Math.Round(((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum)) * (double)(Width - 11));
                TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 25, 25);
                Cap = TrackBarHandleRect.Contains(e.Location);
                Focus();
                if (_JumpToMouse)
                    Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)Width));
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
            SetStyle((ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer), true);

            Size = new Size(80, 22);
            MinimumSize = new Size(47, 22);
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
            TrackThumb = new Rectangle(7, 10, Width - 16, 2);
            PipeBorder = new Rectangle(1, 10, Width - 3, 2);

            try
            {
                ValueDrawer = (int)Math.Round(((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum)) * (double)(Width));
            }
            catch (Exception)
            {
                //
            }

            TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 3, 20);

            G.FillRectangle(new SolidBrush(Color.FromArgb(124, 131, 137)), PipeBorder);
            FillValue = new Rectangle(0, 10, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 3);

            G.ResetClip();

            G.SmoothingMode = SmoothingMode.Default;
            G.DrawRectangle(new Pen(Color.FromArgb(124, 131, 137)), PipeBorder); // Draw pipe border
            G.FillRectangle(new SolidBrush(Color.FromArgb(32, 34, 37)), FillValue);

            G.ResetClip();

            G.SmoothingMode = SmoothingMode.HighQuality;

            G.FillEllipse(new SolidBrush(Color.FromArgb(32, 34, 37)), TrackThumb.X + (int)Math.Round(unchecked((double)TrackThumb.Width * ((double)Value / (double)Maximum))) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
            G.DrawEllipse(new Pen(Color.FromArgb(32, 34, 37)), TrackThumb.X + (int)Math.Round(unchecked((double)TrackThumb.Width * ((double)Value / (double)Maximum))) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
        }
    }

    #endregion
}