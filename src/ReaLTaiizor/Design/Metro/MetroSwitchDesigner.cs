#region Imports

using ReaLTaiizor.Action.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroSwitchDesignerDesign

    internal class MetroSwitchDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroSwitchActionList(Component) };
    }

    #endregion
}