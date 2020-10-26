#region Imports

using System.Collections;
using ReaLTaiizor.Action.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroDividerDesignerDesign

    internal class MetroDividerDesigner : ControlDesigner
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

        public override DesignerActionListCollection ActionLists => _actionListCollection ?? (_actionListCollection = new DesignerActionListCollection { new MetroDividerActionList(Component) });
    }

    #endregion
}