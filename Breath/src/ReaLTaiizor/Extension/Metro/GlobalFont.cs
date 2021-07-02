#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region GlobalFontExtension

    public class GlobalFont
    {
        public static Font Regular(float size)
        {
            return new Font("Segoe UI", size);
        }

        public static Font Normal(string fnt, float size)
        {
            return new Font(fnt, size);
        }

        public static Font Light(float size)
        {
            return new Font("Segoe UI Light", size);
        }

        public static Font Italic(float size)
        {
            return new Font("Segoe UI", size, FontStyle.Italic);
        }

        public static Font SemiBold(float size)
        {
            return new Font("Segoe UI semibold", size);
        }

        public static Font Bold(float size)
        {
            return new Font("Segoe UI", size, FontStyle.Bold);
        }
    }

    #endregion
}