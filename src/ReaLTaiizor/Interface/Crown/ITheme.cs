#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;

#endregion

namespace ReaLTaiizor.Interface.Crown
{
    #region IThemeInterface

    public interface ITheme
    {
        Sizes Sizes { get; }

        CrownColors Colors { get; }
    }

    #endregion
}