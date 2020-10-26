#region Imports

using System.Collections;
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
            "RightToLeft", "ImeMode", "BorderStyle", "Margin", "Padding", "Enabled", "UseVisualStyleBackColor"
        };

        protected override void PostFilterProperties(IDictionary properties)
        {
            foreach (string property in _propertiesToRemove)
            {
                properties.Remove(property);
            }

            base.PostFilterProperties(properties);
        }
    }

    #endregion
}