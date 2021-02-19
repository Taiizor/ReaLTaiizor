#region Imports

using ReaLTaiizor.Action.Metro;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroTileDesignerDesign

    internal class MetroTileDesigner : ControlDesigner
    {
        private readonly string[] _propertiesToRemove =
        {
             "BackgroundImageLayout", "ForeColor",
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

        public override DesignerActionListCollection ActionLists => _actionListCollection ??= new DesignerActionListCollection { new MetroTileActionList(Component) };
    }

    #endregion
}