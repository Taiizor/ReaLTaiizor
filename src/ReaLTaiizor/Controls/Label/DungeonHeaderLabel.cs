#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonHeaderLabel

    public class DungeonHeaderLabel : Label
    {

        public DungeonHeaderLabel()
        {
            Font = new("Segoe UI", 11, FontStyle.Bold);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}