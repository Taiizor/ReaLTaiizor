#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region DreamProgressBar

    public class DreamProgressBar : ProgressBar
    {
        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                Invalidate();
            }
        }

        private int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value == 0)
                    value = 1;
                _Maximum = value;
                Invalidate();
            }
        }

        public DreamProgressBar()
        {
            Value = 50;
        }

        Color C1 = Color.FromArgb(31, 31, 31), C2 = Color.FromArgb(41, 41, 41), C3 = Color.FromArgb(51, 51, 51), C4 = Color.FromArgb(0, 0, 0, 0), C5 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int V = Width * _Value / _Maximum;
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(1, 1, Width - 2, Height - 2);
                    Rectangle R2 = new Rectangle(2, 2, V - 4, Height - 4);
                    Brush GB1 = new LinearGradientBrush(R1, C2, C3, 90.0F);
                    Brush GB2 = new LinearGradientBrush(R2, C3, C2, 30.0F);
                    G.FillRectangle(GB1, R1);
                    G.FillRectangle(GB2, R2);
                    // Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2)
                    G.DrawRectangle(new Pen(C2), 1, 1, V - 3, Height - 3);
                    // Draw.Gradient(G, C3, C2, 2, 2, V - 4, Height - 4)

                    G.DrawRectangle(new Pen(C1), 0, 0, Width - 1, Height - 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                    /*
                     *  Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2)
                    G.DrawRectangle(New Pen(C2), 1, 1, V - 3, Height - 3)
                    Draw.Gradient(G, C3, C2, 2, 2, V - 4, Height - 4)

                    G.DrawRectangle(New Pen(C1), 0, 0, Width - 1, Height - 1)

                    e.Graphics.DrawImage(B.Clone, 0, 0)
                     */

                }
            }
        }
    }

    #endregion
}