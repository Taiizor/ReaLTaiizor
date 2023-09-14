#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Colors
{
    #region RoyalColors

    public static class RoyalColors
    {
        public static Color ForeColor { get; set; } = Color.FromArgb(31, 31, 31);
        public static Color BackColor { get; set; } = Color.FromArgb(243, 243, 243);
        public static Color BorderColor { get; set; } = Color.FromArgb(180, 180, 180);
        public static Color HotTrackColor { get; set; } = Color.FromArgb(221, 221, 221);
        public static Color AccentColor { get; set; } = Color.FromArgb(51, 102, 255);
        public static Color PressedForeColor { get; set; } = Color.White;
        public static Color PressedBackColor { get; set; } = AccentColor;
    }

    #endregion
}