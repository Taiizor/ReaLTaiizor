#region Imports

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyNumeric

    public class SkyNumeric : Control
    {
        #region " Properties & Flicker Control "
        private int X;
        private int Y;
        private long _Value;
        private long _Max;
        private long _Min;
        private bool Typing;

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
                    _Value = _Min;
                }

                Invalidate();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Y = e.Location.Y;
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 30;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (X > Width - 21 && X < Width - 3)
            {
                if (Y < 15)
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
                Typing = !Typing;
                Focus();
            }
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (Typing)
                {
                    _Value = Convert.ToInt32(Convert.ToString(Convert.ToString(_Value) + e.KeyChar.ToString()));
                }

                if (_Value > _Max) { _Value = _Max; }

            }
            catch
            {
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Up)
            {
                if ((Value + 1) <= _Max)
                {
                    _Value += 1;
                }

                Invalidate();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if ((Value - 1) >= _Min)
                {
                    _Value -= 1;
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                string tmp = _Value.ToString();
                tmp = tmp.Remove(Convert.ToInt32(tmp.Length - 1));
                if (tmp.Length == 0) { tmp = "0"; }
                _Value = Convert.ToInt32(tmp);
            }
            Invalidate();
        }

        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new()
            {
                FirstPoint,
                SecondPoint,
                ThirdPoint
            };
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }
        #endregion

        #region Variables
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color TopTriangleColor { get; set; } = Color.FromArgb(27, 94, 137);

        public Color BotTriangleColor { get; set; } = Color.FromArgb(27, 94, 137);

        public Color BorderColorA { get; set; } = Color.FromArgb(220, 220, 220);

        public Color BorderColorB { get; set; } = Color.FromArgb(228, 228, 228);

        public Color BorderColorC { get; set; } = Color.FromArgb(191, 191, 191);

        public Color BorderColorD { get; set; } = Color.FromArgb(254, 254, 254);

        public Color ButtonBackColorA { get; set; } = Color.FromArgb(245, 245, 245);

        public Color ButtonBackColorB { get; set; } = Color.FromArgb(232, 232, 232);

        public Color ButtonBorderColorA { get; set; } = Color.FromArgb(252, 252, 252);

        public Color ButtonBorderColorB { get; set; } = Color.FromArgb(190, 190, 190);

        public Color ButtonBorderColorC { get; set; } = Color.FromArgb(200, 167, 167, 167);

        public Color ButtonBorderColorD { get; set; } = Color.FromArgb(188, 188, 188);

        public Color ButtonBorderColorE { get; set; } = Color.FromArgb(252, 252, 252);
        #endregion

        public SkyNumeric()
        {
            _Max = 9999999;
            _Min = 0;
            Cursor = Cursors.IBeam;
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(233, 233, 233);
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Font = new("Verdana", 6.75f, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.Clear(BackColor);
            G.SmoothingMode = SmoothingType;

            LinearGradientBrush innerBorderBrush = new(new Rectangle(1, 1, Width - 3, Height - 3), BorderColorA, BorderColorB, 90);
            Pen innerBorderPen = new(innerBorderBrush);
            G.DrawRectangle(innerBorderPen, new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawLine(new(BorderColorC), new Point(1, 1), new Point(Width - 3, 1));

            G.DrawRectangle(new(BorderColorD), new Rectangle(0, 0, Width - 1, Height - 1));

            LinearGradientBrush buttonGradient = new(new Rectangle(Width - 23, 4, 19, 21), ButtonBackColorA, ButtonBackColorB, 90);
            G.FillRectangle(buttonGradient, buttonGradient.Rectangle);
            G.DrawRectangle(new(ButtonBorderColorA), new Rectangle(Width - 22, 5, 17, 19));
            G.DrawRectangle(new(ButtonBorderColorB), new Rectangle(Width - 23, 4, 19, 21));
            G.DrawLine(new(ButtonBorderColorC), new Point(Width - 22, Height - 4), new Point(Width - 5, Height - 4));
            G.DrawLine(new(ButtonBorderColorD), new Point(Width - 22, Height - 16), new Point(Width - 5, Height - 16));
            G.DrawLine(new(ButtonBorderColorE), new Point(Width - 22, Height - 15), new Point(Width - 5, Height - 15));

            //Top Triangle
            DrawTriangle(TopTriangleColor, new Point(Width - 17, 18), new Point(Width - 9, 18), new Point(Width - 13, 21), G);

            //Bottom Triangle
            DrawTriangle(BotTriangleColor, new Point(Width - 17, 10), new Point(Width - 9, 10), new Point(Width - 13, 7), G);

            G.DrawString(Value.ToString(), Font, new SolidBrush(ForeColor), new Rectangle(5, 0, Width - 1, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}