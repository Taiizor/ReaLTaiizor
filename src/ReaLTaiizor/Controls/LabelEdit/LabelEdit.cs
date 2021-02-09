#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LabelEdit

    public class LabelEdit : Label
    {
        public LabelEdit()
        {
            Font = new("Microsoft Sans Serif", 9);
            ForeColor = Color.FromArgb(116, 125, 132);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}