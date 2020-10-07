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
            var g = e.Graphics;
            var rect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);

            var textSize = g.MeasureString(Text, Font);

            using (var b = new SolidBrush(TextColor))
            {
                g.DrawString(Text, Font, b, new PointF(-2, 0));
            }

            using (var p = new Pen(LineColor))
            {
                var p1 = new PointF(textSize.Width + 5, textSize.Height / 2);
                var p2 = new PointF(rect.Width, textSize.Height / 2);
                g.DrawLine(p, p1, p2);
            }
        }

        #endregion
    }

    #endregion
}