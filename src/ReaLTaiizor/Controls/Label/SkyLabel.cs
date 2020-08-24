#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls.Label
{
    #region SkyLabel

    public class SkyLabel : System.Windows.Forms.Label
    {
        public SkyLabel() : base()
        {
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            ForeColor = Color.FromArgb(27, 94, 137);
        }
    }

    #endregion
}