#region Imports

using ReaLTaiizor.Util;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownContextMenuStrip

    public class CrownContextMenuStrip : ContextMenuStrip
    {
        #region Constructor Region

        public CrownContextMenuStrip()
        {
            Renderer = new MenuRenderer();
        }

        #endregion
    }

    #endregion
}