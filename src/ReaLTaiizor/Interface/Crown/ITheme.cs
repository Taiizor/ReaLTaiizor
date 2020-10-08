#region Imports

using ReaLTaiizor.Util;
using ReaLTaiizor.Colors;

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