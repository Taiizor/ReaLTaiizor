#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region Label Edit

    public class LabelEdit : Label
    {
        public LabelEdit()
        {
            Font = new Font("Microsoft Sans Serif", 9);
            ForeColor = Color.FromArgb(116, 125, 132);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}