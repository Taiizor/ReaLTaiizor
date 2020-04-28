#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region MoonForm

    public class MoonForm : MoonLibrary
    {
        Color G1;
        Color G2;

        Color BG;
        public MoonForm()
        {
            TransparencyKey = Color.Fuchsia;
            SetColor("G1", Color.White);
            SetColor("G2", Color.LightGray);
            SetColor("BG", Color.FromArgb(240, 240, 240));
            StartPosition = FormStartPosition.CenterScreen;
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

            LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(1, 1), new Size(this.Width - 2, 23)), G1, G2, 90f);
            G.FillRectangle(LGB, new Rectangle(new Point(1, 1), new Size(this.Width - 2, 23)));

            G.DrawLine(Pens.LightGray, 1, 25, this.Width - 2, 25);
            G.DrawLine(Pens.White, 1, 26, this.Width - 2, 26);

            DrawCorners(TransparencyKey);
            DrawBorders(Pens.LightGray, 1);

            Rectangle IconRec = new Rectangle(3, 3, 20, 20);
            G.DrawIcon(ParentForm.Icon, IconRec);

            G.DrawString(ParentForm.Text, new Font("Segoe UI", 9), Brushes.Gray, new Point(25, 5));
        }
    }

    #endregion
}