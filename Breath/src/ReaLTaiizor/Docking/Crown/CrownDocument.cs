#region Imports

using ReaLTaiizor.Enum.Crown;
using System.ComponentModel;
using static ReaLTaiizor.Helper.CrownHelper;

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
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            base.DefaultDockArea = DockArea.Document;
        }

        #endregion
    }

    #endregion
}