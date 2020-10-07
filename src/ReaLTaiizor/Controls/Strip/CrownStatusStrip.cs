#region Imports

using System.Drawing;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownStatusStrip

    public class CrownStatusStrip : StatusStrip
    {
        #region Constructor Region

        public CrownStatusStrip()
        {
            AutoSize = false;
            BackColor = CrownColors.GreyBackground;
            ForeColor = CrownColors.LightText;
            Padding = new Padding(0, 5, 0, 3);
            Size = new Size(Size.Width, 24);
            SizingGrip = false;
        }

        #endregion

        #region Paint Region

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (SolidBrush b = new SolidBrush(CrownColors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            using (Pen p = new Pen(CrownColors.DarkBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
            }

            using (Pen p = new Pen(CrownColors.LightBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
            }
        }

        #endregion
    }

    #endregion
}