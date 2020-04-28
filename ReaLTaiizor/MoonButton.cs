#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region MoonButton

    public class MoonButton : MoonControl
    {
        Color G1;
        Color G2;

        Color BG;
        public MoonButton()
        {
            this.Size = new Size(120, 26);
            SetColor("G1", Color.White);
            SetColor("G2", Color.LightGray);
            SetColor("BG", Color.FromArgb(240, 240, 240));
            Cursor = Cursors.Hand;
        }

        protected override void ColorHook()
        {
            G1 = GetColor("G1");
            G2 = GetColor("G2");
            BG = GetColor("BG");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            if (State == MouseStateMoon.Over)
                G.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            else if (State == MouseStateMoon.Down)
            {
                LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(Width, Height)), Color.FromArgb(240, 240, 240), Color.White, 90f);
                G.FillRectangle(LGB, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            }
            else if (State == MouseStateMoon.None)
            {
                LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(Width, Height)), Color.White, Color.FromArgb(240, 240, 240), 90f);
                G.FillRectangle(LGB, new Rectangle(new Point(0, 0), new Size(Width, Height)));
            }

            DrawBorders(Pens.LightGray);
            DrawCorners(Color.Transparent);

            StringFormat SF = new StringFormat();
            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
            G.DrawString(this.Text, new Font("Segoe UI", 9), Brushes.Gray, new RectangleF(2, 2, this.Width - 5, this.Height - 4), SF);
        }
    }

    #endregion
}