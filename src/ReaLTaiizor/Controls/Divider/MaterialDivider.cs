#region Imports

using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialDivider

    public sealed class MaterialDivider : Control, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public MaterialDivider()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = SkinManager.DividersColor;
            Height = 1;
        }
    }

    #endregion
}