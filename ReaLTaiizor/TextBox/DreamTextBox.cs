#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region DreamTextBox

    public class DreamTextBox : TextBox
    {
        public DreamTextBox()
        {
            ForeColor = Color.FromArgb(40, 218, 255);
            BackColor = Color.FromArgb(41, 41, 41);
            BorderStyle = BorderStyle.FixedSingle;
            Text = String.Empty;
        }

        Color C1 = Color.FromArgb(31, 31, 31);
        Color C2 = Color.FromArgb(41, 41, 41);
        Color C3 = Color.FromArgb(51, 51, 51);
        Color C4 = Color.FromArgb(0, 0, 0, 0);
        Color C5 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(0, 0, Width, Height / 2);
                    Rectangle R2 = new Rectangle(0, Height / 2, Width, Height);
                    G.DrawRectangle(new Pen(C1), 0, 0, Width - 1, Height - 1);

                    Brush GB1 = new LinearGradientBrush(R1, C3, C2, 40.0F);
                    Brush GB2 = new LinearGradientBrush(R2, C2, C3, 90.0F);
                    G.FillRectangle(GB1, R1);
                    G.FillRectangle(GB2, R2);
                    //Draw.Gradient(G, C2, C3, 1, 1, Width - 2, Height - 2);

                    Pen P2 = new Pen(Color.Black, 2);
                    G.DrawRectangle(P2, 0, 0, Width, Height);
                    SizeF O = G.MeasureString(Text, Font);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), Width / 2 - O.Width / 2, Height / 2 - O.Height / 2);

                    //Draw.Blend(G, C4, C5, C4, 0.5, 0, 1, 1, Width - 2, 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                }
            }
        }
    }

    #endregion
}