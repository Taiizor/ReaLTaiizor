#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region StyleManagerDesignerDesign

    public class StyleManagerDesigner : ComponentDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ?? (_actionListCollection = new DesignerActionListCollection { new StyleManagerActionList(Component) });
    }

    #endregion
}