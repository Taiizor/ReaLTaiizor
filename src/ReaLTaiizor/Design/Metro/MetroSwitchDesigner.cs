#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

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