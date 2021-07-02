#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region CrownTranslucentForm

    internal class CrownTranslucentForm : Form
    {
        #region Property Region

        protected override bool ShowWithoutActivation => true;

        #endregion

        #region Constructor Region

        public CrownTranslucentForm(Color backColor, double opacity = 0.6)
        {
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.None;
            Size = new(1, 1);
            ShowInTaskbar = false;
            AllowTransparency = true;
            Opacity = opacity;
            BackColor = backColor;
        }

        #endregion
    }

    #endregion
}