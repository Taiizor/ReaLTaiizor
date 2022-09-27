#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CircleProgressBar

    public class CircleProgressBar : Control
    {

        #region Enums

        public enum _ProgressShape
        {
            Round,
            Flat
        }

        #endregion
        #region Variables

        private long _Value;
        private long _Maximum = 100;
        private Color _PercentColor = Color.White;
        private Color _ProgressColor1 = Color.FromArgb(92, 92, 92);
        private Color _ProgressColor2 = Color.FromArgb(92, 92, 92);
        private _ProgressShape ProgressShapeVal;

        #endregion
        #region Custom Properties

        public long Value
        {
            get => _Value;
            set
            {
                if (value > _Maximum)
                {
                    value = _Maximum;
                }

                _Value = value;
                Invalidate();
            }
        }

        public long Maximum
        {
            get => _Maximum;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                _Maximum = value;
                Invalidate();
            }
        }

        public Color PercentColor
        {
            get => _PercentColor;
            set
            {
                _PercentColor = value;
                Invalidate();
            }
        }

        public Color ProgressColor1
        {
            get => _ProgressColor1;
            set
            {
                _ProgressColor1 = value;
                Invalidate();
            }
        }

        public Color ProgressColor2
        {
            get => _ProgressColor2;
            set
            {
                _ProgressColor2 = value;
                Invalidate();
            }
        }

        public _ProgressShape ProgressShape
        {
            get => ProgressShapeVal;
            set
            {
                ProgressShapeVal = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetStandardSize();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
        }

        protected override void OnPaintBackground(PaintEventArgs p)
        {
            base.OnPaintBackground(p);
        }

        #endregion

        public CircleProgressBar()
        {
            Size = new(130, 130);
            Font = new("Segoe UI", 15);
            MinimumSize = new(100, 100);
            DoubleBuffered = true;
        }

        private void SetStandardSize()
        {
            int _Size = Math.Max(Width, Height);
            Size = new(_Size, _Size);
        }

        public void Increment(int Val)
        {
            _Value += Val;
            Invalidate();
        }

        public void Decrement(int Val)
        {
            _Value -= Val;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using Bitmap bitmap = new(Width, Height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(BackColor);
            using (LinearGradientBrush brush = new(ClientRectangle, _ProgressColor1, _ProgressColor2, LinearGradientMode.ForwardDiagonal))
            {
                using Pen pen = new(brush, 14f);
                switch (ProgressShapeVal)
                {
                    case _ProgressShape.Round:
                        pen.StartCap = LineCap.Round;
                        pen.EndCap = LineCap.Round;
                        break;

                    case _ProgressShape.Flat:
                        pen.StartCap = LineCap.Flat;
                        pen.EndCap = LineCap.Flat;
                        break;
                }
                graphics.DrawArc(pen, 0x12, 0x12, Width - 0x23 - 2, Height - 0x23 - 2, -90, (int)Math.Round((double)(360.0 / _Maximum * _Value)));
            }
            using (LinearGradientBrush brush2 = new(ClientRectangle, Color.FromArgb(0x34, 0x34, 0x34), Color.FromArgb(0x34, 0x34, 0x34), LinearGradientMode.Vertical))
            {
                graphics.FillEllipse(brush2, 0x18, 0x18, Width - 0x30 - 1, Height - 0x30 - 1);
            }

            SizeF MS = graphics.MeasureString(Convert.ToString(Convert.ToInt32(100 / _Maximum * _Value)), Font);
            graphics.DrawString(Convert.ToString(Convert.ToInt32(100 / _Maximum * _Value)), Font, new SolidBrush(_PercentColor), Convert.ToInt32((Width / 2) - (MS.Width / 2)), Convert.ToInt32((Height / 2) - (MS.Height / 2)));
            e.Graphics.DrawImage(bitmap, 0, 0);
            graphics.Dispose();
            bitmap.Dispose();
        }
    }

    #endregion
}