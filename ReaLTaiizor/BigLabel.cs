#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion


namespace ReaLTaiizor
{
    #region BigLabel

    public class BigLabel : Label
    {

        public BigLabel()
        {
            Font = new Font("Segoe UI", 25, FontStyle.Regular);
            ForeColor = Color.FromArgb(80, 80, 80);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}