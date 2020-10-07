#region Imports

using ReaLTaiizor.Colors;
using System.ComponentModel;
using ReaLTaiizor.Enum.Crown;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DocumentDocking

    [ToolboxItem(false)]
    public class Document : DockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockArea DefaultDockArea => base.DefaultDockArea;

        #endregion

        #region Constructor Region

        public Document()
        {
            BackColor = CrownColors.GreyBackground;
            base.DefaultDockArea = DockArea.Document;
        }

        #endregion
    }

    #endregion
}