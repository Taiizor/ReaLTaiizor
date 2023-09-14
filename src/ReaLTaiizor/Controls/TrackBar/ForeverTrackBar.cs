#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ForeverTrackBar

    [DefaultEvent("Scroll")]
    public class ForeverTrackBar : Control
    {
        private int W;
        private int H;
        private int Val;
        private bool Bool;
        private Rectangle Track;
        private Rectangle Knob;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Val = Convert.ToInt32((_Value - _Minimum) / (float)(_Maximum - _Minimum) * (Width - 11));
                Track = new(Val, 0, 10, 20);

                Bool = Track.Contains(e.Location);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Bool && e.X > -1 && e.X < (Width + 1))
            {
                Value = _Minimum + Convert.ToInt32((_Maximum - _Minimum) * (e.X / (float)Width));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Bool = false;
        }

        [Flags()]
        public enum _Style
        {
            Slider,
            Knob
        }

        public _Style Style { get; set; }

        [Category("Colors")]
        public Color TrackColor { get; set; } = ForeverLibrary.ForeverColor;

        [Category("Colors")]
        public Color HatchColor { get; set; } = Color.FromArgb(23, 148, 92);

        [Category("Colors")]
        public Color SliderColor { get; set; } = Color.FromArgb(25, 27, 29);

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        public event ScrollEventHandler Scroll;
        public delegate void ScrollEventHandler(object sender);

        private int _Minimum;
        public int Minimum
        {
            get
            {
                int functionReturnValue = 0;
                return functionReturnValue;
            }
            set
            {
                if (value < 0)
                {
                    _Value = 0;
                }

                _Minimum = value;

                if (value > _Value)
                {
                    _Value = value;
                }

                if (value > _Maximum)
                {
                    _Maximum = value;
                }

                Invalidate();
            }
        }

        private int _Maximum = 10;
        public int Maximum
        {
            get => _Maximum;
            set
            {
                if (value < 0)
                {
                    _Value = 0;
                }

                _Maximum = value;
                if (value < _Value)
                {
                    _Value = value;
                }

                if (value < _Minimum)
                {
                    _Minimum = value;
                }

                Invalidate();
            }
        }

        private int _Value;
        public int Value
        {
            get => _Value;
            set
            {
                if (value == _Value)
                {
                    return;
                }

                if (value > _Maximum || value < _Minimum)
                {
                    if (value > _Maximum)
                    {
                        _Value = Maximum;
                    }
                    else
                    {
                        _Value = _Minimum;
                    }
                }

                _Value = value;
                Invalidate();
                Scroll?.Invoke(this);
            }
        }

        public bool ShowValue { get; set; } = false;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Subtract)
            {
                if (Value == 0)
                {
                    return;
                }

                Value -= 1;
            }
            else if (e.KeyCode == Keys.Add)
            {
                if (Value == _Maximum)
                {
                    return;
                }

                Value += 1;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 23;
        }

        public ForeverTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 18;
            Cursor = Cursors.Hand;

            BackColor = Color.FromArgb(60, 70, 73);
            ForeColor = Color.White;
            Font = new("Segoe UI", 8);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width - 1;
            H = Height - 1;

            Rectangle Base = new(1, 6, W - 2, 8);
            GraphicsPath GP = new();
            GraphicsPath GP2 = new();

            Graphics _with20 = G;
            _with20.SmoothingMode = SmoothingMode.HighQuality;
            _with20.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with20.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with20.Clear(BackColor);

            //-- Value
            Val = Convert.ToInt32((_Value - _Minimum) / (float)(_Maximum - _Minimum) * (W - 10));
            Track = new(Val, 0, 10, 20);
            Knob = new(Val, 4, 11, 14);

            //-- Base
            GP.AddRectangle(Base);
            _with20.SetClip(GP);
            _with20.FillRectangle(new SolidBrush(BaseColor), new Rectangle(0, 7, W, 8));
            _with20.FillRectangle(new SolidBrush(TrackColor), new Rectangle(0, 7, Track.X + Track.Width, 8));
            _with20.ResetClip();

            //-- Hatch Brush
            HatchBrush HB = new(HatchStyle.Plaid, HatchColor, TrackColor);
            _with20.FillRectangle(HB, new Rectangle(-10, 7, Track.X + Track.Width, 8));

            //-- Slider/Knob
            switch (Style)
            {
                case _Style.Slider:
                    GP2.AddRectangle(Track);
                    _with20.FillPath(new SolidBrush(SliderColor), GP2);
                    break;
                case _Style.Knob:
                    GP2.AddEllipse(Knob);
                    _with20.FillPath(new SolidBrush(SliderColor), GP2);
                    break;
            }

            //-- Show the value 
            if (ShowValue)
            {
                _with20.DrawString(Value.ToString(), Font, new SolidBrush(ForeColor), new Rectangle(1, 6, W, H), new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                });
            }

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            TrackColor = Colors.Forever;
        }
    }

    #endregion
}