#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MoonProgressBar

    public class MoonProgressBar : MoonControl
    {
        private Color BG;
        private int HBPos;

        private int _Minimum;
        public int Minimum
        {
            get => _Minimum;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
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

        private int _Maximum = 100;
        public int Maximum
        {
            get => _Maximum;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Property value is not valid.");
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
                if (value > _Maximum || value < _Minimum)
                {
                    throw new Exception("Property value is not valid.");
                }

                _Value = value;
                Invalidate();
            }
        }

        public Color LineColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color LinesColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LightGray;

        public Color LinerColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Transparent;

        public Color LineUpColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(240, 240, 240);

        public Color LineEndColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LightGray;

        public HatchStyle HatchType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HatchStyle.BackwardDiagonal;

        private void Increment(int amount)
        {
            Value += amount;
        }

        public bool Animated
        {
            get => IsAnimated;
            set
            {
                IsAnimated = value;
                Invalidate();
            }
        }

        protected override void OnAnimation()
        {
            if (HBPos == 0)
            {
                HBPos = 7;
            }
            else
            {
                HBPos += 1;
            }
        }

        public MoonProgressBar()
        {
            Animated = true;
            SetColor("BG", Color.FromArgb(240, 240, 240));
        }

        protected override void ColorHook()
        {
            BG = GetColor("BG");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            DrawBorders(Pens.LightGray, 1);
            DrawCorners(Color.Transparent);

            LinearGradientBrush LGB = new(new Rectangle(new Point(2, 2), new Size(Width - 2, Height - 5)), LineColor, LineUpColor, 90f);
            G.FillRectangle(LGB, new Rectangle(new Point(2, 2), new Size((Width / Maximum * Value) - 5, Height - 5)));

            G.RenderingOrigin = new(HBPos, 0);
            HatchBrush HB = new(HatchType, LinesColor, LinerColor);
            G.FillRectangle(HB, new Rectangle(new Point(1, 2), new Size((Width / Maximum * Value) - 3, Height - 3)));
            G.DrawLine(new(new SolidBrush(LineEndColor)), new Point((Width / Maximum * Value) - 2, 1), new Point((Width / Maximum * Value) - 2, Height - 3));
        }
    }

    #endregion
}