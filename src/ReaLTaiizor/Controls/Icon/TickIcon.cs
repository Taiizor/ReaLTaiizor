#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region TickIcon

    public class TickIcon : Control
    {
        #region Settings

        public Color BaseColor { get; set; } = Color.FromArgb(246, 246, 246);
        public Color CircleColor { get; set; } = Color.Gray;
        public string String { get; set; } = "ü";

        #endregion

        public TickIcon()
        {
            ForeColor = Color.DimGray;
            BackColor = Color.FromArgb(246, 246, 246);
            ForeColor = Color.Gray;
            Font = new("Wingdings", 25, FontStyle.Bold);
            Size = new(33, 33);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            e.Graphics.FillEllipse(new SolidBrush(CircleColor), new Rectangle(1, 1, 29, 29));
            e.Graphics.FillEllipse(new SolidBrush(BaseColor), new Rectangle(3, 3, 25, 25));

            e.Graphics.DrawString(String, Font, new SolidBrush(ForeColor), new Rectangle(0, -3, Width, 43), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });
        }
    }

    #endregion
}