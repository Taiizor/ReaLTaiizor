#region Imports

using ReaLTaiizor.Forms;
using System.ComponentModel;
using System.Windows.Forms.Design;

#endregion

namespace ReaLTaiizor.Design.Parrot
{
    #region ParrotFormDesignerDesign

    public class ParrotFormDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (Control is ParrotForm)
            {
                base.EnableDesignMode(((ParrotForm)Control).WorkingArea, "WorkingArea");
            }
        }
    }

    #endregion
}