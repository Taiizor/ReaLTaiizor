#region Imports

using ReaLTaiizor.Manager;
using ReaLTaiizor.Enum.Metro;

#endregion

namespace ReaLTaiizor.Interface.Metro
{
    #region iControlInterface

    public interface iControl
    {
        Style Style { get; set; }

        MetroStyleManager MetroStyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }
    }

    #endregion
}