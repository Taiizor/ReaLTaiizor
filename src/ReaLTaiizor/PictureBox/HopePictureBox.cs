#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopePictureBox

    public class HopePictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            var graphics = pe.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            if (Image == null)
                graphics.FillRectangle(new SolidBrush(BackColor), new RectangleF(0, 0, Width, Height));
            base.OnPaint(pe);

            var backPath = RoundRectangle.CreateRoundRect(0, 00, Width, Height, 4);
            graphics.DrawPath(new Pen(Parent.BackColor, 4), backPath);
        }

        public HopePictureBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            BackColor = HopeColors.PlaceholderText;
        }
    }

    #endregion
}