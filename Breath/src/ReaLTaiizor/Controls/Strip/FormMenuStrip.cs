#region Imports

using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FormMenuStrip

    public class FormMenuStrip : MenuStrip
    {

        public FormMenuStrip()
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