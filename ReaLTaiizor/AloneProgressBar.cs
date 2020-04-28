#region Imports

using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

#endregion

namespace ReaLTaiizor
{
    #region AloneProgressBar

    public class AloneProgressBar : Control
    {
        private int _Val;

        private int _Min;

        private int _Max;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private Color _Stripes;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), CompilerGenerated]
        private Color _BackgroundColor;

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
            get
            {
                return this._Val;
            }
            set
            {
                this._Val = value;
                base.Invalidate();
            }
        }

        public int Minimum
        {
            get
            {
                return this._Min;
            }
            set
            {
                this._Min = value;
                base.Invalidate();
            }
        }

        public int Maximum
        {
            get
            {
                return this._Max;
            }
            set
            {
                this._Max = value;
                base.Invalidate();
            }
        }

        public AloneProgressBar()
        {
            this._Val = 0;
            this._Min = 0;
            this._Max = 100;
            this.Stripes = Color.DarkGreen;
            this.BackgroundColor = Color.Green;
            this.DoubleBuffered = true;
            this.Maximum = 100;
            this.Minimum = 0;
            this.Value = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);
            graphics.Clear(Color.White);
            using (Pen pen = new Pen(AloneLibrary.ColorFromHex("#D0D5D9")))
            {
                graphics.DrawPath(pen, AloneLibrary.RoundRect(AloneLibrary.FullRectangle(base.Size, true), 6, AloneLibrary.RoundingStyle.All));
            }
            bool flag = this.Value != 0;
            if (flag)
            {
                using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.LightUpwardDiagonal, this.Stripes, this.BackgroundColor))
                {
                    graphics.FillPath(hatchBrush, AloneLibrary.RoundRect(checked(new Rectangle(0, 0, (int)Math.Round(unchecked((double)this.Value / (double)this.Maximum * (double)base.Width - 1.0)), base.Height - 1)), 6, AloneLibrary.RoundingStyle.All));
                }
            }
        }
    }

    #endregion
}