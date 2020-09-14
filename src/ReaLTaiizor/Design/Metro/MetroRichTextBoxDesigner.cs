#region Imports

using ReaLTaiizor.Action.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroRichTextBoxDesignerDesign

    internal class MetroRichTextBoxDesigner : ControlDesigner
    {
        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ?? (_actionListCollection = new DesignerActionListCollection { new MetroRichTextBoxActionList(Component) });
    }

    #endregion
}