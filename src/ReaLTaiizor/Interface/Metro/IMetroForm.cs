#region Imports

using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;

#endregion

namespace ReaLTaiizor.Interface.Metro
{
    #region IMetroFormInterface

    public interface IMetroForm
    {
        Style Style { get; set; }

        MetroStyleManager StyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }
    }

    #endregion
}