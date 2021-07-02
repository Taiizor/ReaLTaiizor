#region Imports

using ReaLTaiizor.Extension.Poison;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Resolver.Poison
{
    #region PoisonFontResolverResolver

    public class PoisonFontResolver : PoisonFonts.IPoisonFontResolver
    {
        public Font ResolveFont(string familyName, float emSize, FontStyle fontStyle, GraphicsUnit unit)
        {
            Font fontTester = new(familyName, emSize, fontStyle, unit);
            if (fontTester.Name == familyName || !TryResolve(ref familyName, ref fontStyle))
            {
                return fontTester;
            }

            fontTester.Dispose();

            FontFamily fontFamily = GetFontFamily(familyName);
            return new Font(fontFamily, emSize, fontStyle, unit);
        }

        private const string OPEN_SANS_REGULAR = "Open Sans";
        private const string OPEN_SANS_LIGHT = "Open Sans Light";
        private const string OPEN_SANS_BOLD = "Open Sans Bold";

        private readonly PrivateFontCollection fontCollection = new();

        private static bool TryResolve(ref string familyName, ref FontStyle fontStyle)
        {
            if (familyName == "Segoe UI Light")
            {
                familyName = OPEN_SANS_LIGHT;
                if (fontStyle != FontStyle.Bold)
                {
                    fontStyle = FontStyle.Regular;
                }

                return true;
            }

            if (familyName == "Segoe UI")
            {
                if (fontStyle == FontStyle.Bold)
                {
                    familyName = OPEN_SANS_BOLD;
                    return true;
                }

                familyName = OPEN_SANS_REGULAR;
                return true;
            }

            return false;
        }

        private FontFamily GetFontFamily(string familyName)
        {
            lock (fontCollection)
            {
                foreach (FontFamily fontFamily in fontCollection.Families)
                {
                    if (fontFamily.Name == familyName)
                    {
                        return fontFamily;
                    }
                }

                string resourceName = "ReaLTaiizor.Resources.Poison." + familyName.Replace(' ', '_') + ".ttf";

                Stream fontStream = null;
                IntPtr data = IntPtr.Zero;
                try
                {
                    //fontStream = GetType().Assembly.GetManifestResourceStream(resourceName);
                    fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                    int bytes = (int)fontStream.Length;
                    data = Marshal.AllocCoTaskMem(bytes);
                    byte[] fontdata = new byte[bytes];
                    fontStream.Read(fontdata, 0, bytes);
                    Marshal.Copy(fontdata, 0, data, bytes);
                    fontCollection.AddMemoryFont(data, bytes);
                    return fontCollection.Families[fontCollection.Families.Length - 1];
                }
                finally
                {
                    if (fontStream != null)
                    {
                        fontStream.Dispose();
                    }

                    if (data != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(data);
                    }
                }
            }
        }
    }

    #endregion
}