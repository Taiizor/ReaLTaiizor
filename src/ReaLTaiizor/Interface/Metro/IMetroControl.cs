#region Imports

using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Manager;

#endregion

namespace ReaLTaiizor.Interface.Metro
{
    #region IMetroControlInterface

    public interface IMetroControl
    {
        Style Style { get; set; }

        MetroStyleManager StyleManager { get; set; }

        string ThemeAuthor { get; set; }

        string ThemeName { get; set; }

        bool IsDerivedStyle { get; set; }
    }

    #endregion
}