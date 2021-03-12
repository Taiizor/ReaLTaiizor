#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region GraphicsQualityExtension

    public class GraphicsQuality
    {
        public static void SetQuality(Graphics e, SmoothingMode smoothingMode = SmoothingMode.Default, TextRenderingHint textRenderingHint = TextRenderingHint.ClearTypeGridFit, PixelOffsetMode pixelOffsetMode = PixelOffsetMode.Default, InterpolationMode interpolationMode = InterpolationMode.Default, CompositingQuality compositingQuality = CompositingQuality.Default)
        {
            try
            {
                e.SmoothingMode = smoothingMode;
                e.PixelOffsetMode = pixelOffsetMode;
                e.InterpolationMode = interpolationMode;
                e.CompositingQuality = compositingQuality;
                e.TextRenderingHint = textRenderingHint;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.StackTrace);
            }
        }

    }

    #endregion
}