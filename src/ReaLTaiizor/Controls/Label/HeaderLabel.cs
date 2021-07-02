#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HeaderLabel

    public class HeaderLabel : Label
    {
        public HeaderLabel()
        {
            Font = new("Microsoft Sans Serif", 11, FontStyle.Bold);
            ForeColor = Color.FromArgb(255, 255, 255);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}