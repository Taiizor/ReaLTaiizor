#region Imports

using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Metro
{
    #region MetroTabPageDesignerDesign

    internal class MetroTabPageDesigner : ScrollableControlDesigner
    {
        private readonly string[] _propertiesToRemove =
        {
            "BackgroundImage", "BackgroundImageLayout", "ForeColor",
            "RightToLeft","ImeMode","BorderStyle","Margin","Padding","Enabled","UseVisualStyleBackColor"
        };

        protected override void PostFilterProperties(System.Collections.IDictionary properties)
        {
            foreach (var property in _propertiesToRemove)
                properties.Remove(property);
            base.PostFilterProperties(properties);
        }
    }

    #endregion
}