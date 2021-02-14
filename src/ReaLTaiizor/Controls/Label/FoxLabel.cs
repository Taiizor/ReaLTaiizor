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
    #region FoxLabel

    public class FoxLabel : Control
    {
        private Graphics G;

        public FoxLabel()
        {
            Font = new("Segoe UI", 10, FontStyle.Bold);
            ForeColor = FoxLibrary.ColorFromHex("#4C5864");
            DoubleBuffered = true;
            Size = new(65, 19);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //Size = new(65, 19);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            G.Clear(BackColor);

            using (SolidBrush HColor = new(ForeColor))
            {
                G.DrawString(Text, Font, HColor, new Point(0, 0));
            }

            base.OnPaint(e);

        }

    }

    #endregion
}