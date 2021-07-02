#region Imports

using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Manager;

#endregion

namespace ReaLTaiizor.Interface.Poison
{
    #region IPoisonComponentInterface

    public interface IPoisonComponent
    {
        ColorStyle Style { get; set; }
        ThemeStyle Theme { get; set; }

        PoisonStyleManager StyleManager { get; set; }
    }

    #endregion
}