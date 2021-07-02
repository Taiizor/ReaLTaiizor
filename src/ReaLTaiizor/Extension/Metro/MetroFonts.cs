#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region MetroFontsExtension

    public class MetroFonts
    {
        public static Font SemiLight(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Semilight, size);
        }

        public static Font Light(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Light, size);
        }
        public static Font SemiBold(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Semibold, size);
        }

        public static Font Bold(float size)
        {
            return GetFont(Properties.Resources.SegoeWP_Bold, size);
        }

        public static Font Regular(float size)
        {
            return GetFont(Properties.Resources.SegoeWP, size);
        }

        public static Font UIRegular(float size)
        {
            return new Font("Segoe UI", size);
        }

        public static Font GetFont(byte[] fontbyte, float size)
        {
            using PrivateFontCollection privateFontCollection = new();
            byte[] fnt = fontbyte;
            IntPtr buffer = Marshal.AllocCoTaskMem(fnt.Length);
            Marshal.Copy(fnt, 0, buffer, fnt.Length);
            privateFontCollection.AddMemoryFont(buffer, fnt.Length);
            return new Font(privateFontCollection.Families[0].Name, size);
        }
    }

    #endregion
}