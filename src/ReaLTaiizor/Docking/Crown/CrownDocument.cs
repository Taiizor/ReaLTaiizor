#region Imports

using ReaLTaiizor.Colors;
using System.ComponentModel;
using ReaLTaiizor.Enum.Crown;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDocumentDocking

    [ToolboxItem(false)]
    public class CrownDocument : CrownDockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockArea DefaultDockArea => base.DefaultDockArea;

        #endregion

        #region Constructor Region

        public CrownDocument()
        {
            BackColor = CrownColors.GreyBackground;
            base.DefaultDockArea = DockArea.Document;
        }

        #endregion
    }

    #endregion
}