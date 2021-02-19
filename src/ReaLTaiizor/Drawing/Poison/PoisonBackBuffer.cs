#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

#endregion

namespace ReaLTaiizor.Drawing.Poison
{
    #region PoisonBackBufferDrawing

    internal sealed class PoisonBackBuffer
    {
        private readonly Bitmap backBuffer;

        public PoisonBackBuffer(Size bufferSize)
        {
            backBuffer = new(bufferSize.Width, bufferSize.Height, PixelFormat.Format32bppArgb);
        }

        public Graphics CreateGraphics()
        {
            Graphics g = Graphics.FromImage(backBuffer);

            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            return g;
        }

        public void Draw(Graphics g)
        {
            g.DrawImageUnscaled(backBuffer, Point.Empty);
        }
    }

    #endregion
}