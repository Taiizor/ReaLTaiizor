#region Imports

using ReaLTaiizor.Util;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AloneProgressBar

    public class AloneProgressBar : Control
    {
        private int _Val;

        private int _Min;

        private int _Max;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private readonly Color _Stripes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private readonly Color _BackgroundColor;

        public Color Stripes
        {
            get;
            set;
        }

        public Color BackgroundColor
        {
            get;
            set;
        }

        public int Value
        {
            get => _Val;
            set
            {
                _Val = value;
                Invalidate();
            }
        }

        public int Minimum
        {
            get => _Min;
            set
            {
                _Min = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _Max;
            set
            {
                _Max = value;
                Invalidate();
            }
        }

        public Color BorderColor { get; set; } = Color.DodgerBlue;

        public AloneProgressBar()
        {
            _Val = 50;
            _Min = 0;
            _Max = 100;
            Stripes = Color.DarkGreen;
            BackgroundColor = Color.Green;
            DoubleBuffered = true;
            Maximum = 100;
            Minimum = 0;
            Value = 50;
            BackColor = Color.FromArgb(45, 45, 48);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            graphics.Clear(BackColor);
            using (Pen pen = new(BorderColor))
            {
                graphics.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
            }

            bool flag = Value != 0;
            if (flag)
            {
                using HatchBrush hatchBrush = new(HatchStyle.LightUpwardDiagonal, Stripes, BackgroundColor);
                graphics.FillPath(hatchBrush, AloneLibrary.RoundRect(checked(new Rectangle(0, 0, (int)Math.Round(unchecked((Value / (double)Maximum * Width) - 1.0)), base.Height - 1)), 6, AloneLibrary.RoundingStyle.All));
            }
        }
    }

    #endregion
}