#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region  TickIcon

    public class TickIcon : Control
    {

        public TickIcon()
        {
            ForeColor = Color.DimGray;
            BackColor = Color.FromArgb(246, 246, 246);
            Size = new Size(33, 33);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            e.Graphics.FillEllipse(new SolidBrush(Color.Gray), new Rectangle(1, 1, 29, 29));
            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(246, 246, 246)), new Rectangle(3, 3, 25, 25));

            e.Graphics.DrawString("ü", new Font("Wingdings", 25, FontStyle.Bold), new SolidBrush(Color.Gray), new Rectangle(0, -3, Width, 43), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });
        }
    }

    #endregion
}