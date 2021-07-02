#region Imports

using ReaLTaiizor.Child.Crown;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownScrollView

    public abstract class CrownScrollView : CrownScrollBase
    {
        #region Constructor Region

        protected CrownScrollView()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );
        }

        #endregion

        #region Paint Region

        protected abstract void PaintContent(Graphics g);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw background
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            // Offset the graphics based on the viewport, render the control contents, then reset it.
            g.TranslateTransform(Viewport.Left * -1, Viewport.Top * -1);

            PaintContent(g);

            g.TranslateTransform(Viewport.Left, Viewport.Top);

            // Draw the bit where the scrollbars meet
            if (_vScrollBar.Visible && _hScrollBar.Visible)
            {
                using SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground);
                Rectangle rect = new(_hScrollBar.Right, _vScrollBar.Bottom, _vScrollBar.Width, _hScrollBar.Height);

                g.FillRectangle(b, rect);
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