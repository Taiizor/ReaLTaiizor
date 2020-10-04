#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Colors
{
    #region RoyalColors

    public static class RoyalColors
    {
        private static Color foreColor = Color.FromArgb(31, 31, 31);
        public static Color ForeColor
        {
            get => foreColor;
            set => foreColor = value;
        }

        private static Color backColor = Color.FromArgb(243, 243, 243);
        public static Color BackColor
        {
            get => backColor;
            set => backColor = value;
        }

        private static Color borderColor = Color.FromArgb(180, 180, 180);
        public static Color BorderColor
        {
            get => borderColor;
            set => borderColor = value;
        }

        private static Color hotTrackColor = Color.FromArgb(221, 221, 221);
        public static Color HotTrackColor
        {
            get => hotTrackColor;
            set => hotTrackColor = value;
        }

        private static Color accentColor = Color.FromArgb(51, 102, 255);
        public static Color AccentColor
        {
            get => accentColor;
            set => accentColor = value;
        }

        private static Color pressedForeColor = Color.White;
        public static Color PressedForeColor
        {
            get => pressedForeColor;
            set => pressedForeColor = value;
        }

        private static Color pressedBackColor = accentColor;
        public static Color PressedBackColor
        {
            get => pressedBackColor;
            set => pressedBackColor = value;
        }
    }

    #endregion
}