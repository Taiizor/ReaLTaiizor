#region Imports

using System.Drawing;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTitle

    public class CrownTitle : Label
    {
        #region Properties

        private Color _TextColor = CrownColors.LightText;
        public Color TextColor
        {
            get => _TextColor;
            set { _TextColor = value; Invalidate(); }
        }

        private Color _LineColor = CrownColors.GreyHighlight;
        public Color LineColor
        {
            get => _LineColor;
            set { _LineColor = value; Invalidate(); }
        }

        #endregion

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
            Rectangle rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            SizeF textSize = g.MeasureString(Text, Font);

            using (SolidBrush b = new SolidBrush(TextColor))
            {
                g.DrawString(Text, Font, b, new PointF(-2, 0));
            }

            using (Pen p = new Pen(LineColor))
            {
                PointF p1 = new PointF(textSize.Width + 5, textSize.Height / 2);
                PointF p2 = new PointF(rect.Width, textSize.Height / 2);
                g.DrawLine(p, p1, p2);
            }
        }

        #endregion
    }

    #endregion
}