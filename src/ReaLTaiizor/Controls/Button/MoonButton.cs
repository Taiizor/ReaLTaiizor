#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MoonButton

    public class MoonButton : MoonControl
    {
        private Color G1;
        private Color G2;
        private Color BG;
        private Color FC;

        public MoonButton()
        {
            Size = new(120, 26);
            SetColor("G1", Color.White);
            SetColor("G2", Color.LightGray);
            SetColor("BG", Color.FromArgb(240, 240, 240));
            SetColor("FC", Color.Gray);
            Font = new("Segoe UI", 9);
            Cursor = Cursors.Hand;
        }

        protected override void ColorHook()
        {
            G1 = GetColor("G1");
            G2 = GetColor("G2");
            BG = GetColor("BG");
            FC = GetColor("FC");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            if (State == MouseStateMoon.Over)
            {
                G.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            }
            else if (State == MouseStateMoon.Down)
            {
                LinearGradientBrush LGB = new(new Rectangle(new Point(0, 0), new Size(Width, Height)), Color.FromArgb(240, 240, 240), Color.White, 90f);
                G.FillRectangle(LGB, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            }
            else if (State == MouseStateMoon.None)
            {
                LinearGradientBrush LGB = new(new Rectangle(new Point(0, 0), new Size(Width, Height)), Color.White, Color.FromArgb(240, 240, 240), 90f);
                G.FillRectangle(LGB, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            }

            DrawBorders(Pens.LightGray);
            DrawCorners(Color.Transparent);

            StringFormat SF = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            G.DrawString(Text, Font, new SolidBrush(FC), new RectangleF(2, 2, Width - 5, Height - 4), SF);
        }
    }

    #endregion
}