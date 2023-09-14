#region Imports

using ReaLTaiizor.Controls;
using System.Collections;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonTileDesignerDesign

    internal class PoisonTileDesigner : ParentControlDesigner
    {
        public override SelectionRules SelectionRules => base.SelectionRules;

        public override bool CanParent(System.Windows.Forms.Control control)
        {
            return control is PoisonLabel or PoisonProgressSpinner;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
            properties.Remove("AutoEllipsis");
            properties.Remove("UseCompatibleTextRendering");

            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");

            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("UseVisualStyleBackColor");

            properties.Remove("Font");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }
    }

    #endregion
}