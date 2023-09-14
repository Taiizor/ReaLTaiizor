#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Child.Material
{
    #region MaterialDrawerForm

    [ToolboxItem(false)]
    public class MaterialDrawerForm : Form
    {
        public MaterialMouseWheelRedirector MouseWheelRedirector;

        public MaterialDrawerForm()
        {
            MouseWheelRedirector = new MaterialMouseWheelRedirector();
            SetStyle(ControlStyles.Selectable | ControlStyles.OptimizedDoubleBuffer | ControlStyles.EnableNotifyMessage, true);
        }

        public void Attach(Control control)
        {
            MaterialMouseWheelRedirector.Attach(control);
        }

        public void Detach(Control control)
        {
            MaterialMouseWheelRedirector.Detach(control);
        }
    }

    #endregion
}