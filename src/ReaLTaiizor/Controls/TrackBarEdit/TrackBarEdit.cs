#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region TrackBarEdit

    [DefaultEvent("ValueChanged")]
    public class TrackBarEdit : Control
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
        private int _Value = 0;
        private bool _DrawHatch = true;

        #endregion
        #region Custom Properties

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

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler();
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
                    ValueChanged?.Invoke();
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
            get => (float)(_Value / ((double)ValueDivison));
            set => Value = (int)Math.Round((double)(value * ((float)ValueDivison)));
        }

        public Color ValueColour
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(224, 224, 224);

        public bool DrawHatch
        {
            get => _DrawHatch;
            set
            {
                _DrawHatch = value;
                Invalidate();
            }
        }

        public bool DrawValueString
        {
            get;
            set
            {
                field = value;
                if (field == true)
                {
                    Height = 40;
                }
                else
                {
                    Height = 22;
                }
                Invalidate();
            }
        } = false;

        public bool JumpToMouse { get; set; } = false;

        #endregion
        #region EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Cap && (e.X > -1) && (e.X < (Width + 1)))
            {
                Value = Minimum + ((int)Math.Round((double)((Maximum - Minimum) * (e.X / ((double)Width)))));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ValueDrawer = (int)Math.Round((double)((_Value - Minimum) / ((double)(Maximum - Minimum)) * (Width - 11)));
                TrackBarHandleRect = new(ValueDrawer, 0, 10, 20);
                Cap = TrackBarHandleRect.Contains(e.Location);
                if (JumpToMouse)
                {
                    Value = Minimum + ((int)Math.Round((double)((Maximum - Minimum) * (e.X / ((double)Width)))));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }


        #endregion

        public TrackBarEdit()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            _DrawHatch = true;
            Size = new(80, 22);
            MinimumSize = new(37, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DrawValueString == true)
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
            HatchBrush Hatch = new(HatchStyle.WideDownwardDiagonal, Color.FromArgb(20, Color.Black), Color.Transparent);
            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingMode.AntiAlias;
            checked
            {
                PipeBorder = RoundRectangle.RoundRect(1, 6, Width - 3, 8, 3);
                try
                {
                    ValueDrawer = (int)Math.Round(unchecked(checked((_Value - Minimum) / (double)(Maximum - Minimum)) * checked(Width - 11)));
                }
                catch (Exception)
                {
                }
                TrackBarHandleRect = new(ValueDrawer, 0, 10, 20);
                G.SetClip(PipeBorder);
                ValueRect = new(1, 7, TrackBarHandleRect.X + TrackBarHandleRect.Width - 2, 7);
                VlaueLGB = new(ValueRect, ValueColour, ValueColour, 90f);
                G.FillRectangle(VlaueLGB, ValueRect);

                if (_DrawHatch == true)
                {
                    G.FillRectangle(Hatch, ValueRect);
                }

                G.ResetClip();
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.DrawPath(new(Color.FromArgb(180, 180, 180)), PipeBorder);
                TrackBarHandle = RoundRectangle.RoundRect(TrackBarHandleRect, 3);
                TrackBarHandleLGB = new(ClientRectangle, SystemColors.Control, SystemColors.Control, 90f);
                G.FillPath(TrackBarHandleLGB, TrackBarHandle);
                G.DrawPath(new(Color.FromArgb(180, 180, 180)), TrackBarHandle);

                if (DrawValueString == true)
                {
                    G.DrawString(Convert.ToString(ValueToSet), Font, Brushes.Gray, 0, 25);
                }
            }
        }
    }

    #endregion
}