#region Imports

using ReaLTaiizor.Action.Metro;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroRadioButtonDesignerDesign

    internal class MetroRadioButtonDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroRadioButtonActionList(Component) };
    }

    #endregion
}