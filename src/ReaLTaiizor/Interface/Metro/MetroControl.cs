#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Enum.Metro;

#endregion

namespace ReaLTaiizor.Interface.Metro
{
    #region MetroControlInterface

    public interface MetroControl
    {
        Style Style { get; set; }

        MetroStyleManager StyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }
    }

    #endregion
}