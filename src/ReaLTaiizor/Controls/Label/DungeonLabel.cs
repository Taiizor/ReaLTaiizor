#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonLabel

    public class DungeonLabel : System.Windows.Forms.Label
    {

        public DungeonLabel()
        {
            Font = new Font("Segoe UI", 11);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}