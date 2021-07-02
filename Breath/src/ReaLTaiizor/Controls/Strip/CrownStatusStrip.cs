#region Imports

using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

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
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            ForeColor = ThemeProvider.Theme.Colors.LightText;
            Padding = new Padding(0, 5, 0, 3);
            Size = new(Size.Width, 24);
            SizingGrip = false;
        }

        #endregion

        #region Paint Region

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            using (Pen p = new(ThemeProvider.Theme.Colors.DarkBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
            }

            using (Pen p = new(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
            }
        }

        #endregion
    }

    #endregion
}