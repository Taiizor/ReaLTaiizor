#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Enum.Poison;

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