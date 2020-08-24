#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls.Label
{
    #region LabelEdit

    public class LabelEdit : System.Windows.Forms.Label
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