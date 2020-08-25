#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

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

        private Size ThumbSize = new Size(15, 15);
        private Rectangle TrackThumb;

        private int _Minimum = 0;
        private int _Maximum = 10;
        private int _Value = 0;

        private bool _DrawValueString = false;
        private bool _JumpToMouse = false;
        private ValueDivisor DividedValue = ValueDivisor.By1;

        private Color _EmptyBackColor = Color.FromArgb(221, 221, 221);
        private Color _BorderColor = Color.FromArgb(200, 200, 200);
        private Color _FillBackColor = Color.FromArgb(217, 99, 50);
        private Color _ThumbBackColor = Color.FromArgb(244, 244, 244);
        private Color _ThumbBorderColor = Color.FromArgb(180, 180, 180);

        #endregion

        #region Properties

        public Color EmptyBackColor
        {
            get { return _EmptyBackColor; }
            set { _EmptyBackColor = value; }
        }

        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        public Color FillBackColor
        {
            get { return _FillBackColor; }
            set { _FillBackColor = value; }
        }

        public Color ThumbBackColor
        {
            get { return _ThumbBackColor; }
            set { _ThumbBackColor = value; }
        }

        public Color ThumbBorderColor
        {
            get { return _ThumbBorderColor; }
            set { _ThumbBorderColor = value; }
        }

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

        public bool DrawValueString
        {
            get
            {
                return _DrawValueString;
            }
            set
            {
                _DrawValueString = value;
                if (_DrawValueString == true)
                    Height = 35;
                else
                    Height = 22;
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
                    Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)Width));
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
                    ValueDrawer = (int)Math.Round(((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum)) * (double)(Width - 11));
                    TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 25, 25);
                    Cap = TrackBarHandleRect.Contains(e.Location);
                    Focus();
                    flag = _JumpToMouse;
                    if (flag)
                    {
                        Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)Width));
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
            SetStyle((ControlStyles)(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer), true);

            Cursor = Cursors.Hand;
            Size = new Size(80, 22);
            MinimumSize = new Size(47, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_DrawValueString)
                Height = 40;
            else
                Height = 22;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;

            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            TrackThumb = new Rectangle(8, 10, Width - 16, 2);
            PipeBorder = RoundRectangle.RoundRect(1, 8, Width - 3, 5, 2);

            try
            {
                ValueDrawer = (int)Math.Round(((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum)) * (double)(Width - 11));
            }
            catch (Exception)
            {
            }

            TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 10, 20);

            G.SetClip(PipeBorder); // Set the clipping region of this Graphics to the specified GraphicsPath
            G.FillPath(new SolidBrush(_EmptyBackColor), PipeBorder);
            FillValue = RoundRectangle.RoundRect(1, 8, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 5, 2);

            G.ResetClip(); // Reset the clip region of this Graphics to an infinite region

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.DrawPath(new Pen(_BorderColor), PipeBorder); // Draw pipe border
            G.FillPath(new SolidBrush(_FillBackColor), FillValue);

            G.FillEllipse(new SolidBrush(_ThumbBackColor), TrackThumb.X + (int)Math.Round(unchecked((double)TrackThumb.Width * ((double)Value / (double)Maximum))) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
            G.DrawEllipse(new Pen(_ThumbBorderColor), TrackThumb.X + (int)Math.Round(unchecked((double)TrackThumb.Width * ((double)Value / (double)Maximum))) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);

            if (_DrawValueString == true)
                G.DrawString(Convert.ToString(ValueToSet), Font, Brushes.DimGray, 1, 20);
        }
    }

    #endregion
}