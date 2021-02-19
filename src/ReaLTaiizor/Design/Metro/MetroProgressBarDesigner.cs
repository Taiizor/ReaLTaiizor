#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroProgressBarDesignerDesign

    internal class MetroProgressBarDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroProgressBarActionList(Component) };
    }

    #endregion
}