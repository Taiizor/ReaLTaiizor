#region Imports

using ReaLTaiizor.Util;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownMenuStrip

    public class CrownMenuStrip : MenuStrip
    {
        #region Constructor Region

        public CrownMenuStrip()
        {
            Renderer = new MenuRenderer();
            Padding = new Padding(3, 2, 0, 2);
        }

        #endregion
    }

    #endregion
}