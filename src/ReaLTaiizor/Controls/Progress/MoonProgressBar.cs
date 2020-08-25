#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MoonProgressBar

    public class MoonProgressBar : MoonControl
    {
        Color BG;

        int HBPos;

        private int _Minimum;
        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value < 0)
                    throw new Exception("Property value is not valid.");

                _Minimum = value;
                if (value > _Value)
                    _Value = value;
                if (value > _Maximum)
                    _Maximum = value;
                Invalidate();
            }
        }

        private int _Maximum = 100;
        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < 0)
                    throw new Exception("Property value is not valid.");

                _Maximum = value;
                if (value < _Value)
                    _Value = value;
                if (value < _Minimum)
                    _Minimum = value;
                Invalidate();
            }
        }

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum || value < _Minimum)
                    throw new Exception("Property value is not valid.");

                _Value = value;
                Invalidate();
            }
        }

        private Color _LineColor = Color.White;
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                Invalidate();
            }
        }

        private Color _LinesColor = Color.LightGray;
        public Color LinesColor
        {
            get { return _LinesColor; }
            set
            {
                _LinesColor = value;
                Invalidate();
            }
        }

        private Color _LinerColor = Color.Transparent;
        public Color LinerColor
        {
            get { return _LinerColor; }
            set
            {
                _LinerColor = value;
                Invalidate();
            }
        }

        private Color _LineUpColor = Color.FromArgb(240, 240, 240);
        public Color LineUpColor
        {
            get { return _LineUpColor; }
            set
            {
                _LineUpColor = value;
                Invalidate();
            }
        }

        private Color _LineEndColor = Color.LightGray;
        public Color LineEndColor
        {
            get { return _LineEndColor; }
            set
            {
                _LineEndColor = value;
                Invalidate();
            }
        }

        private HatchStyle _HatchType = HatchStyle.BackwardDiagonal;
        public HatchStyle HatchType
        {
            get { return _HatchType; }
            set
            {
                _HatchType = value;
                Invalidate();
            }
        }

        private void Increment(int amount)
        {
            Value += amount;
        }

        public bool Animated
        {
            get { return IsAnimated; }
            set
            {
                IsAnimated = value;
                Invalidate();
            }
        }

        protected override void OnAnimation()
        {
            if (HBPos == 0)
                HBPos = 7;
            else
                HBPos += 1;
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

            LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(2, 2), new Size(Width - 2, Height - 5)), LineColor, LineUpColor, 90f);
            G.FillRectangle(LGB, new Rectangle(new Point(2, 2), new Size((Width / Maximum) * Value - 5, Height - 5)));

            G.RenderingOrigin = new Point(HBPos, 0);
            HatchBrush HB = new HatchBrush(HatchType, LinesColor, LinerColor);
            G.FillRectangle(HB, new Rectangle(new Point(1, 2), new Size((Width / Maximum) * Value - 3, Height - 3)));
            G.DrawLine(new Pen(new SolidBrush(LineEndColor)), new Point((Width / Maximum) * Value - 2, 1), new Point((Width / Maximum) * Value - 2, Height - 3));
        }
    }

    #endregion
}