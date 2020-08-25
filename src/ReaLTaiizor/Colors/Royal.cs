#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Colors
{
    #region RoyalLibrary

    public static class RoyalColors
    {
        static Color foreColor = Color.FromArgb(31, 31, 31);
        public static Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        static Color backColor = Color.FromArgb(243, 243, 243);
        public static Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        static Color borderColor = Color.FromArgb(180, 180, 180);
        public static Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        static Color hotTrackColor = Color.FromArgb(221, 221, 221);
        public static Color HotTrackColor
        {
            get { return hotTrackColor; }
            set { hotTrackColor = value; }
        }

        static Color accentColor = Color.FromArgb(51, 102, 255);
        public static Color AccentColor
        {
            get { return accentColor; }
            set { accentColor = value; }
        }

        static Color pressedForeColor = Color.White;
        public static Color PressedForeColor
        {
            get { return pressedForeColor; }
            set { pressedForeColor = value; }
        }

        static Color pressedBackColor = accentColor;
        public static Color PressedBackColor
        {
            get { return pressedBackColor; }
            set { pressedBackColor = value; }
        }
    }

    #endregion
}