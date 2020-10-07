#region Imports

using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTextBox

    public class CrownTextBox : TextBox
    {
        #region Constructor Region

        public CrownTextBox()
        {
            BackColor = CrownColors.LightBackground;
            ForeColor = CrownColors.LightText;
            Padding = new Padding(2, 2, 2, 2);
            BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion
    }

    #endregion
}