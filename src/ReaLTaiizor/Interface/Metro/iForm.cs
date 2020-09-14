#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Enum.Metro;

#endregion

namespace ReaLTaiizor.Interface.Metro
{
    #region iFormInterface

    public interface iForm
    {
        Style Style { get; set; }

        StyleManager StyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }
    }

    #endregion
}