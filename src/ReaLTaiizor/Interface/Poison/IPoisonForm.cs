#region Imports

using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Manager;

#endregion

namespace ReaLTaiizor.Interface.Poison
{
    #region IPoisonFormInterface

    public interface IPoisonForm
    {
        ColorStyle Style { get; set; }
        ThemeStyle Theme { get; set; }

        PoisonStyleManager StyleManager { get; set; }
    }

    #endregion
}