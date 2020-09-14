#region Imports

using ReaLTaiizor.Action.Metro;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroTrackBarDesignerDesign

    internal class MetroTrackBarDesigner : ControlDesigner
    {
        private readonly string[] _propertiesToRemove =
        {
            "BackgroundImage", "BackgroundImageLayout", "ForeColor",
            "RightToLeft","ImeMode"
        };


        protected override void PostFilterProperties(System.Collections.IDictionary properties)
        {
            foreach (var property in _propertiesToRemove)
                properties.Remove(property);
            base.PostFilterProperties(properties);
        }

        private DesignerActionListCollection _actionListCollection;

        public override DesignerActionListCollection ActionLists => _actionListCollection ?? (_actionListCollection = new DesignerActionListCollection { new MetroTrackBarActionList(Component) });
    }

    #endregion
}