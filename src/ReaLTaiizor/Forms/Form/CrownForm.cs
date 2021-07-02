#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Forms
{
    #region CrownForm

    public class CrownForm : Form
    {
        #region Field Region

        private bool _flatBorder;

        #endregion

        #region Property Region

        [Category("Appearance")]
        [Description("Determines whether a single pixel border should be rendered around the form.")]
        [DefaultValue(false)]
        public bool FlatBorder
        {
            get => _flatBorder;
            set
            {
                _flatBorder = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public CrownForm()
        {
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        #region Paint Region

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (!_flatBorder)
            {
                return;
            }

            Graphics g = e.Graphics;

            using Pen p = new(ThemeProvider.Theme.Colors.DarkBorder);
            Rectangle modRect = new(ClientRectangle.Location, new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1));
            g.DrawRectangle(p, modRect);
        }

        #endregion
    }

    #endregion
}