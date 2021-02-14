#region Imports

using ReaLTaiizor.Enum.Poison;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonScrollBarDesignerDesign

    [Designer(typeof(ScrollableControlDesigner), typeof(ParentControlDesigner))]
    internal class PoisonScrollBarDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(Component)["Orientation"];

                if (propDescriptor != null)
                {
                    ScrollOrientationType orientation = (ScrollOrientationType)propDescriptor.GetValue(Component);

                    if (orientation == ScrollOrientationType.Vertical)
                    {
                        return SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.TopSizeable;
                    }

                    return SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                }

                return base.SelectionRules;
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            properties.Remove("Text");
            properties.Remove("BackgroundImage");
            properties.Remove("ForeColor");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("BackColor");
            properties.Remove("Font");
            properties.Remove("RightToLeft");

            base.PreFilterProperties(properties);
        }
    }

    #endregion
}