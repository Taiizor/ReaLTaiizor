#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region TrackEdit

    [DefaultEvent("ValueChanged")]
    public class TrackEdit : Control
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
        private GraphicsPath TrackBarHandle;
        private Rectangle TrackBarHandleRect;
        private Rectangle ValueRect;
        private LinearGradientBrush VlaueLGB;
        private LinearGradientBrush TrackBarHandleLGB;
        private bool Cap;

        private int ValueDrawer;
        private int _Minimum = 0;
        private int _Maximum = 10;
        private int _Value = 0;
        private Color _ValueColour = Color.FromArgb(224, 224, 224);
        private bool _DrawHatch = true;
        private bool _DrawValueString = false;
        private bool _JumpToMouse = false;
        private ValueDivisor DividedValue = ValueDivisor.By1;

        #endregion
        #region Custom Properties

        public int Minimum
        {
            get { return _Minimum; }

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
            get { return _Maximum; }

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

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler();
        public int Value
        {
            get { return _Value; }
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
                    ValueChanged?.Invoke();
                }
            }
        }

        public ValueDivisor ValueDivison
        {
            get
            {
                return this.DividedValue;
            }
            set
            {
                this.DividedValue = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public float ValueToSet
        {
            get
            {
                return (float)(((double)this._Value) / ((double)this.DividedValue));
            }
            set
            {
                this.Value = (int)Math.Round((double)(value * ((float)this.DividedValue)));
            }
        }

        public Color ValueColour
        {
            get { return _ValueColour; }
            set
            {
                _ValueColour = value;
                Invalidate();
            }
        }

        public bool DrawHatch
        {
            get { return _DrawHatch; }
            set
            {
                _DrawHatch = value;
                Invalidate();
            }
        }

        public bool DrawValueString
        {
            get { return _DrawValueString; }
            set
            {
                _DrawValueString = value;
                if (_DrawValueString == true)
                {
                    Height = 40;
                }
                else
                {
                    Height = 22;
                }
                Invalidate();
            }
        }

        public bool JumpToMouse
        {
            get
            {
                return this._JumpToMouse;
            }
            set
            {
                this._JumpToMouse = value;
            }
        }

        #endregion
        #region EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((this.Cap && (e.X > -1)) && (e.X < (this.Width + 1)))
            {
                this.Value = this._Minimum + ((int)Math.Round((double)((this._Maximum - this._Minimum) * (((double)e.X) / ((double)this.Width)))));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this.ValueDrawer = (int)Math.Round((double)((((double)(this._Value - this._Minimum)) / ((double)(this._Maximum - this._Minimum))) * (this.Width - 11)));
                this.TrackBarHandleRect = new Rectangle(this.ValueDrawer, 0, 10, 20);
                this.Cap = this.TrackBarHandleRect.Contains(e.Location);
                if (this._JumpToMouse)
                {
                    this.Value = this._Minimum + ((int)Math.Round((double)((this._Maximum - this._Minimum) * (((double)e.X) / ((double)this.Width)))));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.Cap = false;
        }


        #endregion

        public TrackEdit()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            _DrawHatch = true;
            Size = new Size(80, 22);
            MinimumSize = new Size(37, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_DrawValueString == true)
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
            HatchBrush Hatch = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.FromArgb(20, Color.Black), Color.Transparent);
            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            checked
            {
                this.PipeBorder = RoundRectangle.RoundRect(1, 6, this.Width - 3, 8, 3);
                try
                {
                    this.ValueDrawer = (int)Math.Round(unchecked(checked((double)(this._Value - this._Minimum) / (double)(this._Maximum - this._Minimum)) * (double)checked(this.Width - 11)));
                }
                catch (Exception)
                {
                }
                this.TrackBarHandleRect = new Rectangle(this.ValueDrawer, 0, 10, 20);
                G.SetClip(this.PipeBorder);
                this.ValueRect = new Rectangle(1, 7, this.TrackBarHandleRect.X + this.TrackBarHandleRect.Width - 2, 7);
                this.VlaueLGB = new LinearGradientBrush(this.ValueRect, this._ValueColour, this._ValueColour, 90f);
                G.FillRectangle(this.VlaueLGB, this.ValueRect);

                if (_DrawHatch == true)
                {
                    G.FillRectangle(Hatch, this.ValueRect);
                }

                G.ResetClip();
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.DrawPath(new Pen(Color.FromArgb(180, 180, 180)), this.PipeBorder);
                this.TrackBarHandle = RoundRectangle.RoundRect(this.TrackBarHandleRect, 3);
                this.TrackBarHandleLGB = new LinearGradientBrush(this.ClientRectangle, SystemColors.Control, SystemColors.Control, 90f);
                G.FillPath(this.TrackBarHandleLGB, this.TrackBarHandle);
                G.DrawPath(new Pen(Color.FromArgb(180, 180, 180)), this.TrackBarHandle);

                if (_DrawValueString == true)
                {
                    G.DrawString(Convert.ToString(ValueToSet), Font, Brushes.Gray, 0, 25);
                }
            }
        }
    }

    #endregion
}