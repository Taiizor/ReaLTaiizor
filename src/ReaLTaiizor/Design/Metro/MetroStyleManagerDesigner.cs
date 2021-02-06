#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroStyleManagerDesignerDesign

    public class MetroStyleManagerDesigner : ComponentDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroStyleManagerActionList(Component) };
    }

    #endregion
}