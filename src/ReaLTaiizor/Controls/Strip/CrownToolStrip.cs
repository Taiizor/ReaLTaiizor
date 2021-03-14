#region Imports

using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownToolStrip

    public class CrownToolStrip : ToolStrip
    {
        #region Constructor Region

        public CrownToolStrip()
        {
            Renderer = new Util.ToolStripRenderer();
            Padding = new Padding(5, 0, 1, 0);
            AutoSize = false;
            Size = new(1, 28);
        }

        #endregion
    }

    #endregion
}