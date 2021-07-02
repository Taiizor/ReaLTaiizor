#region Imports

using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTitle

    public class CrownTitle : Label
    {
        #region Constructor Region

        public CrownTitle()
        {
            //
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, ClientSize.Width, ClientSize.Height);

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, rect);
            }

            SizeF textSize = g.MeasureString(Text, Font);

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.LightText))
            {
                g.DrawString(Text, Font, b, new PointF(-2, 0));
            }

            using Pen p = new(ThemeProvider.Theme.Colors.GreyHighlight);
            PointF p1 = new(textSize.Width + 5, textSize.Height / 2);
            PointF p2 = new(rect.Width, textSize.Height / 2);
            g.DrawLine(p, p1, p2);
        }

        #endregion
    }

    #endregion
}