#region Imports

using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FormContextMenuStrip

    public class FormContextMenuStrip : ContextMenuStrip
    {

        public FormContextMenuStrip()
        {
            Renderer = new ControlRenderer();
        }

        public new ControlRenderer Renderer
        {
            get => (ControlRenderer)base.Renderer;
            set => base.Renderer = value;
        }
    }

    #endregion
}