#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonLabel

    public class DungeonLabel : Label
    {

        public DungeonLabel()
        {
            Font = new("Segoe UI", 11);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
}