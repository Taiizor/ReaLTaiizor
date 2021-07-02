#region Imports

using ReaLTaiizor.Colors;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopePictureBox

    public class HopePictureBox : PictureBox
    {
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics graphics = pe.Graphics;
            graphics.SmoothingMode = SmoothingType;
            graphics.PixelOffsetMode = PixelOffsetType;
            graphics.TextRenderingHint = TextRenderingType;
            graphics.Clear(Parent.BackColor);

            if (Image == null)
            {
                graphics.FillRectangle(new SolidBrush(BackColor), new RectangleF(0, 0, Width, Height));
            }

            base.OnPaint(pe);

            GraphicsPath backPath = RoundRectangle.CreateRoundRect(0, 00, Width, Height, 4);
            graphics.DrawPath(new(Parent.BackColor, 4), backPath);
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