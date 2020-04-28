#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region MoonProgressBar

    public class MoonProgressbar : MoonControl
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

        public MoonProgressbar()
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

            LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(2, 2), new Size(Width - 2, Height - 5)), Color.White, Color.FromArgb(240, 240, 240), 90f);
            G.FillRectangle(LGB, new Rectangle(new Point(2, 2), new Size((Width / Maximum) * Value - 5, Height - 5)));

            G.RenderingOrigin = new Point(HBPos, 0);
            HatchBrush HB = new HatchBrush(HatchStyle.BackwardDiagonal, Color.LightGray, Color.Transparent);
            G.FillRectangle(HB, new Rectangle(new Point(1, 2), new Size((Width / Maximum) * Value - 3, Height - 3)));
            G.DrawLine(Pens.LightGray, new Point((Width / Maximum) * Value - 2, 1), new Point((Width / Maximum) * Value - 2, Height - 3));
        }
    }

    #endregion
}