#region Imports

using System.Drawing;
using ReaLTaiizor.Controls;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Child.Metro
{
    #region MetroToolStripMenuItemChild

    public sealed class MetroToolStripMenuItem : ToolStripMenuItem
    {
        #region Constructors

        public MetroToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(160, 30);
        }

        #endregion Constructors

        #region Adding DropDowns

        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            if (DesignMode)
                return base.CreateDefaultDropDown();
            var dp = new MetroContextMenuStrip();
            dp.Items.AddRange(base.CreateDefaultDropDown().Items);
            return dp;
        }

        #endregion Adding DropDowns
    }

    #endregion
}