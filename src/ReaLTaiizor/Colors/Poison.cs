#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Colors
{
    #region PoisonColors

    public sealed class PoisonColors
    {
        public static Color Black => Color.FromArgb(0, 0, 0);

        public static Color White => Color.FromArgb(255, 255, 255);

        public static Color Silver => Color.FromArgb(85, 85, 85);

        public static Color Blue => Color.FromArgb(0, 174, 219);

        public static Color Green => Color.FromArgb(0, 177, 89);

        public static Color Lime => Color.FromArgb(142, 188, 0);

        public static Color Teal => Color.FromArgb(0, 170, 173);

        public static Color Orange => Color.FromArgb(243, 119, 53);

        public static Color Brown => Color.FromArgb(165, 81, 0);

        public static Color Pink => Color.FromArgb(231, 113, 189);

        public static Color Magenta => Color.FromArgb(255, 0, 148);

        public static Color Purple => Color.FromArgb(124, 65, 153);

        public static Color Red => Color.FromArgb(209, 17, 65);

        public static Color Yellow => Color.FromArgb(255, 196, 37);

        public static Color Custom { get; set; } = Color.FromArgb(225, 195, 143);
    }

    #endregion
}