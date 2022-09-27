#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxBigLabel

    public class FoxBigLabel : Control
    {
        private Graphics G;

        public Color LineColor { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");

        public Direction Line { get; set; } = Direction.Bottom;

        public enum Direction
        {
            Top,
            Bottom
        }

        public FoxBigLabel()
        {
            Font = new("Segoe UI Semibold", 20);
            ForeColor = FoxLibrary.ColorFromHex("#4C5864");
            DoubleBuffered = true;
            Size = new(165, 51);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //Size = new(Width, 51);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            G.Clear(Parent.BackColor);
            using (SolidBrush HColor = new(ForeColor))
            {
                G.DrawString(Text, Font, HColor, new Point(0, 0));
            }

            using (Pen BottomLine = new(LineColor))
            {
                if (Line == Direction.Bottom)
                {
                    G.DrawLine(BottomLine, new Point(0, Height - 1), new Point(Width, Height - 1));
                }
                else
                {
                    G.DrawLine(BottomLine, new Point(0, 0), new Point(Width, 0));
                }
            }

            base.OnPaint(e);

        }

    }

    #endregion
}