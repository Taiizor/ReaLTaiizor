#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region AmbianceHeaderLabel

    public class AmbianceHeaderLabel : Label
    {

        public AmbianceHeaderLabel()
        {
            Font = new Font("Segoe UI", 11, FontStyle.Bold);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}