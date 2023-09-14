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
    #region ForeverNumeric

    public class ForeverNumeric : Control
    {
        private int W;
        private int H;
        private readonly MouseStateForever State = MouseStateForever.None;
        private int x;
        private int y;
        private long _Value;
        private long _Min;
        private long _Max;
        private bool Bool;

        public long Value
        {
            get => _Value;
            set
            {
                if (value <= _Max & value >= _Min)
                {
                    _Value = value;
                }

                Invalidate();
            }
        }

        public long Maximum
        {
            get => _Max;
            set
            {
                if (value > _Min)
                {
                    _Max = value;
                }

                if (_Value > _Max)
                {
                    _Value = _Max;
                }

                Invalidate();
            }
        }

        public long Minimum
        {
            get => _Min;
            set
            {
                if (value < _Max)
                {
                    _Min = value;
                }

                if (_Value < _Min)
                {
                    _Value = Minimum;
                }

                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            x = e.Location.X;
            y = e.Location.Y;
            Invalidate();
            if (e.X < Width - 23)
            {
                Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (x > Width - 21 && x < Width - 3)
            {
                if (y < 15)
                {
                    if ((Value + 1) <= _Max)
                    {
                        _Value += 1;
                    }
                }
                else
                {
                    if ((Value - 1) >= _Min)
                    {
                        _Value -= 1;
                    }
                }
            }
            else
            {
                Bool = !Bool;
                Focus();
            }
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (Bool)
                {
                    _Value = Convert.ToInt64(_Value.ToString() + e.KeyChar.ToString());
                }

                if (_Value > _Max)
                {
                    _Value = _Max;
                }

                Invalidate();
            }
            catch
            {
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Back)
            {
                Value = 0;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 30;
        }

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color ButtonColorA { get; set; } = ForeverLibrary.ForeverColor;

        [Category("Colors")]
        public Color ButtonColorB { get; set; } = Color.White;

        [Category("Colors")]
        public Color ButtonColorC { get; set; } = Color.White;

        public ForeverNumeric()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 10);
            _Min = 0;
            _Max = 100;
            ForeColor = Color.Silver;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //UpdateColors();

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;

            Rectangle Base = new(0, 0, W, H);

            Graphics _with18 = G;
            _with18.SmoothingMode = SmoothingMode.HighQuality;
            _with18.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with18.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with18.Clear(BaseColor);

            //-- Base
            _with18.FillRectangle(new SolidBrush(BaseColor), Base);
            _with18.FillRectangle(new SolidBrush(ButtonColorA), new Rectangle(Width - 24, 0, 24, H));

            //-- Add
            _with18.DrawString("+", new Font("Segoe UI", 12), new SolidBrush(ButtonColorB), new Point(Width - 12, 8), ForeverLibrary.CenterSF);
            //-- Subtract
            _with18.DrawString("-", new Font("Segoe UI", 10, FontStyle.Bold), new SolidBrush(ButtonColorC), new Point(Width - 12, 22), ForeverLibrary.CenterSF);

            //-- Text
            _with18.DrawString(Value.ToString(), Font, new SolidBrush(ForeColor), new Rectangle(5, 1, W, H), new StringFormat { LineAlignment = StringAlignment.Center });

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }

        private void UpdateColors()
        {
            ForeverColors Colors = ForeverLibrary.GetColors(this);

            ButtonColorA = Colors.Forever;
        }
    }

    #endregion
}