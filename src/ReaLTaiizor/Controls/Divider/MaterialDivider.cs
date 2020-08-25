#region Imports

using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helpers.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialDivider

    public sealed class MaterialDivider : Control, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

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