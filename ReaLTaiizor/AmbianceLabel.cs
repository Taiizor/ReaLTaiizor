#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region AmbianceLabel

    public class AmbianceLabel : Label
    {

        public AmbianceLabel()
        {
            Font = new Font("Segoe UI", 11);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}