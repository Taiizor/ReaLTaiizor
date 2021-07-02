#region Imports

using ReaLTaiizor.Controls;
using System.ComponentModel;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Poison
{
    #region PoisonPanelDesignerDesign

    internal class PoisonPanelDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (Control is PoisonPanel)
            {
                //EnableDesignMode(((PoisonPanel)Control).ScrollablePanel, "ScrollablePanel");
            }
        }
    }

    #endregion
}