#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroToolTipDesignerDesign

    internal class MetroToolTipDesigner : ComponentDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroToolTipActionList(Component) };
    }

    #endregion
}