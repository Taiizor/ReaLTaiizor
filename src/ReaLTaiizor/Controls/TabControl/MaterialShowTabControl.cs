#region Imports

using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialShowTabControl

    public class MaterialShowTabControl : TabControl, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public MaterialShowTabControl()
        {
            Multiline = true;
        }
    }

    #endregion
}