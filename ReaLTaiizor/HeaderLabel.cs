#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion


namespace ReaLTaiizor
{
    #region Header Label

    public class HeaderLabel : Label
    {
        public HeaderLabel()
        {
            Font = new Font("Microsoft Sans Serif", 11, FontStyle.Bold);
            ForeColor = Color.FromArgb(255, 255, 255);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}