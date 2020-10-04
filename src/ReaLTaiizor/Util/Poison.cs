#region Imports

using ReaLTaiizor.Enum.Poison;

#endregion

namespace ReaLTaiizor.Util
{
    #region PoisonUtil

    #region PoisonDefaults

    internal static class PoisonDefaults
    {
        public const ColorStyle Style = ColorStyle.Blue;
        public const ThemeStyle Theme = ThemeStyle.Light;

        public static class PropertyCategory
        {
            public const string Appearance = "Poison Appearance";
            public const string Behaviour = "Poison Behaviour";
        }
    }

    #endregion

    #region HiddenTabClass
    public class HiddenTabs
    {
        public HiddenTabs(int id, string page)
        {
            _index = id;
            _tabpage = page;
        }

        private int _index;
        private string _tabpage;

        public int index { get { return _index; } }

        public string tabpage { get { return _tabpage; } }
    }
    #endregion HiddenTabClass

    #endregion
}