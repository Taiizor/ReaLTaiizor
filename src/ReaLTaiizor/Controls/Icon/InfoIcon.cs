#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region InfoIcon

    public class InfoIcon : Control
    {
        #region Settings

        public Color BaseColor { get; set; } = Color.FromArgb(246, 246, 246);
        public Color CircleColor { get; set; } = Color.Gray;
        public string String { get; set; } = "¡";

        #endregion

        public InfoIcon()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            ForeColor = Color.DimGray;
            BackColor = Color.Transparent;
            ForeColor = Color.Gray;
            Font = new("Segoe UI", 25, FontStyle.Bold);
            Size = new(33, 33);
            DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Font = new(Font.FontFamily, Height - 6, Font.Style);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            e.Graphics.FillEllipse(new SolidBrush(CircleColor), new Rectangle(0, 0, Width, Height));
            e.Graphics.FillEllipse(new SolidBrush(BaseColor), new Rectangle(3, 3, Width - 6, Height - 6));

            e.Graphics.DrawString(String, Font, new SolidBrush(ForeColor), new Rectangle(1, -13 * Convert.ToInt32(Math.Sqrt(Height / 33)), Width, Height + 13), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
        }
    }

    #endregion
}