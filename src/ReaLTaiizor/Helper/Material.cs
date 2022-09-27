#region Imports

using ReaLTaiizor.Manager;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Helper
{
    #region MaterialHelper

    public static class MaterialColorHelper
    {
        public static Color Lighten(this Color color, float percent)
        {
            float lighting = color.GetBrightness();
            lighting = lighting + (lighting * percent);
            if (lighting > 1.0)
            {
                lighting = 1;
            }
            else if (lighting <= 0)
            {
                lighting = 0.1f;
            }
            Color tintedColor = FromHsl(color.A, color.GetHue(), color.GetSaturation(), lighting);

            return tintedColor;
        }

        public static Color Darken(this Color color, float percent)
        {
            float lighting = color.GetBrightness();
            lighting = lighting - (lighting * percent);
            if (lighting > 1.0)
            {
                lighting = 1;
            }
            else if (lighting <= 0)
            {
                lighting = 0;
            }
            Color tintedColor = FromHsl(color.A, color.GetHue(), color.GetSaturation(), lighting);

            return tintedColor;
        }

        public static Color FromHsl(int alpha, float hue, float saturation, float lighting)
        {
            if (alpha is < 0 or > 255)
            {
                throw new ArgumentOutOfRangeException("alpha");
            }
            if (hue is < 0f or > 360f)
            {
                throw new ArgumentOutOfRangeException("hue");
            }
            if (saturation is < 0f or > 1f)
            {
                throw new ArgumentOutOfRangeException("saturation");
            }
            if (lighting is < 0f or > 1f)
            {
                throw new ArgumentOutOfRangeException("lighting");
            }

            if (0 == saturation)
            {
                return Color.FromArgb(alpha, Convert.ToInt32(lighting * 255), Convert.ToInt32(lighting * 255), Convert.ToInt32(lighting * 255));
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

            if (0.5 < lighting)
            {
                fMax = lighting - (lighting * saturation) + saturation;
                fMin = lighting + (lighting * saturation) - saturation;
            }
            else
            {
                fMax = lighting + (lighting * saturation);
                fMin = lighting - (lighting * saturation);
            }

            iSextant = (int)Math.Floor(hue / 60f);
            if (300f <= hue)
            {
                hue -= 360f;
            }
            hue /= 60f;
            hue -= 2f * (float)Math.Floor((iSextant + 1f) % 6f / 2f);
            if (0 == iSextant % 2)
            {
                fMid = (hue * (fMax - fMin)) + fMin;
            }
            else
            {
                fMid = fMin - (hue * (fMax - fMin));
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            return iSextant switch
            {
                1 => Color.FromArgb(alpha, iMid, iMax, iMin),
                2 => Color.FromArgb(alpha, iMin, iMax, iMid),
                3 => Color.FromArgb(alpha, iMin, iMid, iMax),
                4 => Color.FromArgb(alpha, iMid, iMin, iMax),
                5 => Color.FromArgb(alpha, iMax, iMin, iMid),
                _ => Color.FromArgb(alpha, iMax, iMid, iMin),
            };
        }

        public static Color RemoveAlpha(Color foreground, Color background)
        {
            if (foreground.A == 255)
            {
                return foreground;
            }

            double alpha = foreground.A / 255.0;
            double diff = 1.0 - alpha;
            return Color.FromArgb(255,
                (byte)((foreground.R * alpha) + (background.R * diff)),
                (byte)((foreground.G * alpha) + (background.G * diff)),
                (byte)((foreground.B * alpha) + (background.B * diff)));
        }
    }

    public static class MaterialDrawHelper
    {
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new();
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        public static GraphicsPath CreateRoundRect(RectangleF rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            double ratio = blend / 255d;
            double invRatio = 1d - ratio;
            int r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            int g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            int b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }

        public static void DrawSquareShadow(Graphics g, Rectangle bounds)
        {
            using SolidBrush shadowBrush = new(Color.FromArgb(12, 0, 0, 0));
            GraphicsPath path;
            path = CreateRoundRect(new RectangleF(bounds.X - 3.5f, bounds.Y - 1.5f, bounds.Width + 6, bounds.Height + 6), 8);
            g.FillPath(shadowBrush, path);
            path = CreateRoundRect(new RectangleF(bounds.X - 2.5f, bounds.Y - 1.5f, bounds.Width + 4, bounds.Height + 4), 6);
            g.FillPath(shadowBrush, path);
            path = CreateRoundRect(new RectangleF(bounds.X - 1.5f, bounds.Y - 0.5f, bounds.Width + 2, bounds.Height + 2), 4);
            g.FillPath(shadowBrush, path);
            path = CreateRoundRect(new RectangleF(bounds.X - 0.5f, bounds.Y + 1.5f, bounds.Width + 0, bounds.Height + 0), 4);
            g.FillPath(shadowBrush, path);
            path = CreateRoundRect(new RectangleF(bounds.X - 0.5f, bounds.Y + 2.5f, bounds.Width + 0, bounds.Height + 0), 4);
            g.FillPath(shadowBrush, path);
            path.Dispose();
        }

        public static void DrawRoundShadow(Graphics g, Rectangle bounds)
        {
            using SolidBrush shadowBrush = new(Color.FromArgb(12, 0, 0, 0));
            g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 2, bounds.Y - 1, bounds.Width + 4, bounds.Height + 6));
            g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 4));
            g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y - 0, bounds.Width + 0, bounds.Height + 2));
            g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 2, bounds.Width + 0, bounds.Height + 0));
            g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 1, bounds.Width + 0, bounds.Height + 0));
        }

        public interface MaterialControlI
        {
            int Depth { get; set; }

            MaterialSkinManager SkinManager { get; }

            MaterialMouseState MouseState { get; set; }
        }

        public enum MaterialMouseState
        {
            HOVER,
            DOWN,
            OUT
        }
    }

    #endregion
}