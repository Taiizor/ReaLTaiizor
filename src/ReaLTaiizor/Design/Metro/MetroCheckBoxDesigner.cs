#region Imports

using ReaLTaiizor.Action.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroCheckBoxDesignerDesign

    internal class MetroCheckBoxDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroCheckBoxActionList(Component) };
    }

    #endregion
}