#region Imports

using ReaLTaiizor.Action.Metro;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroListBoxDesignerDesign

    internal class MetroListBoxDesigner : ControlDesigner
    {
        private readonly string[] _propertiesToRemove =
        {
            "BackgroundImage", "BackgroundImageLayout",
            "RightToLeft","ImeMode"
        };

        protected override void PostFilterProperties(IDictionary properties)
        {
            foreach (string property in _propertiesToRemove)
            {
                properties.Remove(property);
            }

            base.PostFilterProperties(properties);
        }

        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroListBoxActionList(Component) };
    }

    #endregion
}