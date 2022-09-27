#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonTrackBar

    [DefaultEvent("ValueChanged")]
    public class DungeonTrackBar : Control
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

        private GraphicsPath PipeBorder;
        private GraphicsPath FillValue;
        private Rectangle TrackBarHandleRect;
        private bool Cap;
        private int ValueDrawer;

        private Size ThumbSize = new(15, 15);
        private Rectangle TrackThumb;

        private int _Minimum = 0;
        private int _Maximum = 10;
        private int _Value = 0;

        private bool _DrawValueString = false;
        private bool _JumpToMouse = false;
        private ValueDivisor DividedValue = ValueDivisor.By1;

        #endregion

        #region Properties

        public Color EmptyBackColor { get; set; } = Color.FromArgb(221, 221, 221);

        public Color BorderColor { get; set; } = Color.FromArgb(200, 200, 200);

        public Color FillBackColor { get; set; } = Color.FromArgb(217, 99, 50);

        public Color ThumbBackColor { get; set; } = Color.FromArgb(244, 244, 244);

        public Color ThumbBorderColor { get; set; } = Color.FromArgb(180, 180, 180);

        public int Minimum
        {
            get => _Minimum;
            set
            {

                if (value >= _Maximum)
                {
                    value = _Maximum - 10;
                }

                if (_Value < value)
                {
                    _Value = value;
                }

                _Minimum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _Maximum;
            set
            {

                if (value <= _Minimum)
                {
                    value = _Minimum + 10;
                }

                if (_Value > value)
                {
                    _Value = value;
                }

                _Maximum = value;
                Invalidate();
            }
        }

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
                    if (value < _Minimum)
                    {
                        _Value = _Minimum;
                    }
                    else
                    {
                        if (value > _Maximum)
                        {
                            _Value = _Maximum;
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
            get => DividedValue;
            set
            {
                DividedValue = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public float ValueToSet
        {
            get => _Value / (int)DividedValue;
            set => Value = (int)(value * (int)DividedValue);
        }

        public bool JumpToMouse
        {
            get => _JumpToMouse;
            set
            {
                _JumpToMouse = value;
                Invalidate();
            }
        }

        public bool DrawValueString
        {
            get => _DrawValueString;
            set
            {
                _DrawValueString = value;
                if (_DrawValueString == true)
                {
                    Height = 35;
                }
                else
                {
                    Height = 22;
                }

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
                {
                    Value = _Minimum + (int)Math.Round((_Maximum - _Minimum) * (e.X / (double)Width));
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool flag = e.Button == MouseButtons.Left;
            checked
            {
                if (flag)
                {
                    ValueDrawer = (int)Math.Round((_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 11));
                    TrackBarHandleRect = new(ValueDrawer, 0, 25, 25);
                    Cap = TrackBarHandleRect.Contains(e.Location);
                    Focus();
                    flag = _JumpToMouse;
                    if (flag)
                    {
                        Value = _Minimum + (int)Math.Round((_Maximum - _Minimum) * (e.X / (double)Width));
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }

        #endregion

        public DungeonTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            Cursor = Cursors.Hand;
            Size = new(80, 22);
            MinimumSize = new(47, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_DrawValueString)
            {
                Height = 40;
            }
            else
            {
                Height = 22;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            TrackThumb = new(8, 10, Width - 16, 2);
            PipeBorder = RoundRectangle.RoundRect(1, 8, Width - 3, 5, 2);

            try
            {
                ValueDrawer = (int)Math.Round((_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 11));
            }
            catch (Exception)
            {
            }

            TrackBarHandleRect = new(ValueDrawer, 0, 10, 20);

            G.SetClip(PipeBorder); // Set the clipping region of this Graphics to the specified GraphicsPath
            G.FillPath(new SolidBrush(EmptyBackColor), PipeBorder);
            FillValue = RoundRectangle.RoundRect(1, 8, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 5, 2);

            G.ResetClip(); // Reset the clip region of this Graphics to an infinite region

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.DrawPath(new(BorderColor), PipeBorder); // Draw pipe border
            G.FillPath(new SolidBrush(FillBackColor), FillValue);

            G.FillEllipse(new SolidBrush(ThumbBackColor), TrackThumb.X + (int)Math.Round(unchecked(TrackThumb.Width * (Value / (double)Maximum))) - (int)Math.Round(ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round(TrackThumb.Height / 2.0) - (int)Math.Round(ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
            G.DrawEllipse(new(ThumbBorderColor), TrackThumb.X + (int)Math.Round(unchecked(TrackThumb.Width * (Value / (double)Maximum))) - (int)Math.Round(ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round(TrackThumb.Height / 2.0) - (int)Math.Round(ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);

            if (_DrawValueString == true)
            {
                G.DrawString(Convert.ToString(ValueToSet), Font, Brushes.DimGray, 1, 20);
            }
        }
    }

    #endregion
}