#region Imports

using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTextBox

    public class CrownTextBox : TextBox
    {
        #region Constructor Region

        public CrownTextBox()
        {
            Padding = new Padding(2, 2, 2, 2);
            BorderStyle = BorderStyle.FixedSingle;
            ForeColor = ThemeProvider.Theme.Colors.LightText;
            BackColor = ThemeProvider.Theme.Colors.LightBackground;
        }

        #endregion
    }

    #endregion
}