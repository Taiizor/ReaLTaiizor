#region Imports

using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region StatusStrip

    public class FormStatusStrip : StatusStrip
    {

        public FormStatusStrip()
        {
            Renderer = new ControlRenderer();
            SizingGrip = false;
        }

        public new ControlRenderer Renderer
        {
            get => (ControlRenderer)base.Renderer;
            set => base.Renderer = value;
        }
    }

    #endregion
}