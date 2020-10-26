#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Enum.Poison;

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