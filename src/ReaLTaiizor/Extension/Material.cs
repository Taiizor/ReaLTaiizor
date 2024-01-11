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

        // Simulate Clamp function from .NET Core 2.0, Core 2.1, Core 2.2, Core 3.0, Core 3.1, 5, 6, 7, 8
        // https://source.dot.net/#System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/Math.cs,538
        public static int Clamp(this int value, int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentException(string.Format("'{0}' cannot be greater than {1}.", min, max));
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        public static int GetIntFromHexColor(string HexColor)
        {
            //#ffffff // #ffffffff
            try
            {
                if (HexColor.Length is <= 9 or >= 2)
                {
                    if (HexColor[0] == '#')
                    {
                        return Convert.ToInt32(HexColor.Replace("#", "0x"), 16);
                    }
                    else
                    {
                        return Convert.ToInt32(HexColor.Insert(0, "0x"), 16);
                    }

                }
            }
            catch
            {
                return 0x00;
            }

            return 0x00;
        }
    }

    #endregion
}