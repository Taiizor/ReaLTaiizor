#region Imports

using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownSeparator

    public class CrownSeparator : Control
    {
        #region Constructor Region

        public CrownSeparator()
        {
            SetStyle(ControlStyles.Selectable, false);

            Dock = DockStyle.Top;
            Size = new(1, 2);
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Pen p = new(ThemeProvider.Theme.Colors.DarkBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
            }

            using (Pen p = new(ThemeProvider.Theme.Colors.LightBorder))
            {
                g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion
    }

    #endregion
}