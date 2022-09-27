#region Imports

using ReaLTaiizor.Helper;
using System;
using System.Drawing;
using System.Reflection;

#endregion

namespace ReaLTaiizor.Extension
{
    #region MaterialExtension

    public static class MaterialExtension
    {
        public static bool HasProperty(this object objectToCheck, string propertyName)
        {
            try
            {
                Type type = objectToCheck.GetType();

                return type.GetProperty(propertyName) != null;
            }
            catch (AmbiguousMatchException)
            {
                // ambiguous means there is more than one result,
                // which means: a method with that name does exist
                return true;
            }
        }

        public static bool IsMaterialControl(this object obj)
        {
            if (obj is MaterialDrawHelper.MaterialControlI)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ToSecureString(this string plainString)
        {
            if (plainString == null)
            {
                return null;
            }

            string secureString = "";
            for (uint i = 0; i < plainString.Length; i++)
            {
                secureString += '\u25CF';
            }
            return secureString;
        }

        public static Color ToColor(this int argb)
        {
            return Color.FromArgb(
                (argb & 0xFF0000) >> 16,
                (argb & 0x00FF00) >> 8,
                 argb & 0x0000FF);
        }

        public static Color RemoveAlpha(this Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }

        public static int PercentageToColorComponent(this int percentage)
        {
            return (int)(percentage / 100d * 255d);
        }
    }

    #endregion
}