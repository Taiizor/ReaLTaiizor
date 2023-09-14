#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region CyberUtil

    public static class CyberLibrary
    {
        #region Draw Engine

        internal class DrawEngine
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="rectangle"></param>
            /// <param name="value_angle"></param>
            /// <returns></returns>
            public static GraphicsPath RoundedRectangle(Rectangle rectangle, float value_angle)
            {
                GraphicsPath graphicsPath = new();

                try
                {
                    graphicsPath.AddArc(rectangle.X, rectangle.Y, value_angle, value_angle, 180, 90);
                    graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y, value_angle, value_angle, 270, 90);
                    graphicsPath.AddArc(rectangle.X + rectangle.Width - value_angle, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 0, 90);
                    graphicsPath.AddArc(rectangle.X, rectangle.Y + rectangle.Height - value_angle, value_angle, value_angle, 90, 90);

                    graphicsPath.CloseFigure();
                }
                catch (Exception Ex)
                {
                    HelpEngine.Error($"[DrawEngine.RoundedRectangle] Ошибка: \n{Ex}");
                }

                return graphicsPath;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="graphics"></param>
            /// <param name="color"></param>
            /// <param name="point_1"></param>
            /// <param name="point_2"></param>
            /// <param name="max_alpha"></param>
            /// <param name="pen_width"></param>
            public static void DrawBlurred(Graphics graphics, Color color, Point point_1, Point point_2, int max_alpha, int pen_width)
            {
                float stepAlpha = (float)max_alpha / pen_width;

                float actualAlpha = stepAlpha;
                for (int pWidth = pen_width; pWidth > 0; pWidth--)
                {
                    Color BlurredColor = Color.FromArgb((int)actualAlpha, color);
                    Pen BlurredPen = new(BlurredColor, pWidth)
                    {
                        StartCap = LineCap.Round,
                        EndCap = LineCap.Round
                    };

                    graphics.DrawLine(BlurredPen, point_1, point_2);

                    actualAlpha += stepAlpha;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="graphics"></param>
            /// <param name="color"></param>
            /// <param name="graphicsPath"></param>
            /// <param name="max_alpha"></param>
            /// <param name="pen_width"></param>
            public static void DrawBlurred(Graphics graphics, Color color, GraphicsPath graphicsPath, int max_alpha, int pen_width)
            {
                float tmp = max_alpha / pen_width;
                float actualAlpha = tmp;

                for (int tmp_width = pen_width; tmp_width > 0; tmp_width--)
                {
                    Pen blurredPen = new(Color.FromArgb((int)actualAlpha, color), tmp_width)
                    {
                        StartCap = LineCap.Round,
                        EndCap = LineCap.Round
                    };
                    actualAlpha += tmp;

                    graphics.DrawPath(blurredPen, graphicsPath);
                }
            }

            #region RGB

            /// <summary>
            /// 
            /// </summary>
            private static float Temp = 0;

            /// <summary>
            /// 
            /// </summary>
            public static readonly Timer GlobalRGB = new() { Interval = 300 };

            /// <summary>
            /// 
            /// </summary>
            /// <param name="status"></param>
            public static void TimerGlobalRGB(bool status)
            {
                GlobalRGB.Stop();
                if (!status)
                {
                    return;
                }

                GlobalRGB.Tick += (Sender, EventArgs) =>
                {
                    Temp++;
                    if (Temp >= 360)
                    {
                        Temp = 0;
                    }
                };

                GlobalRGB.Start();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hue"></param>
            /// <param name="saturation"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public static Color HSV_To_RGB(float hue, float saturation, float value)
            {
                if (saturation < float.Epsilon)
                {
                    int c = (int)(value * 255);
                    return Color.FromArgb(c, c, c);
                }

                if (GlobalRGB.Enabled)
                {
                    hue = Temp;
                }

                float r, g, b, f, p, q, t;
                int i;

                hue /= 60;
                i = (int)Math.Floor(hue);

                f = hue - i;
                p = value * (1 - saturation);
                q = value * (1 - (saturation * f));
                t = value * (1 - (saturation * (1 - f)));

                switch (i)
                {
                    case 0: r = value; g = t; b = p; break;
                    case 1: r = q; g = value; b = p; break;
                    case 2: r = p; g = value; b = t; break;
                    case 3: r = p; g = q; b = value; break;
                    case 4: r = t; g = p; b = value; break;
                    default: r = value; g = p; b = q; break;
                }

                return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
            }

            #endregion
        }

        #endregion

        #region Help Engine

        internal class HelpEngine
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            public static void Error(string text)
            {
                MessageBox.Show(text, "Cyber", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="familyName"></param>
            /// <param name="emSize"></param>
            /// <param name="fontStyle"></param>
            /// <returns></returns>
            public static Font GetDefaultFont(string familyName = "Arial", float emSize = 11.0F, FontStyle fontStyle = FontStyle.Regular)
            {
                return new Font(familyName, emSize, fontStyle);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="bitmap"></param>
            /// <param name="SmoothingMode"></param>
            /// <param name="TextRenderingHint"></param>
            /// <returns></returns>
            public static Graphics GetGraphics(ref Bitmap bitmap, SmoothingMode SmoothingMode, TextRenderingHint TextRenderingHint)
            {
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.SmoothingMode = SmoothingMode;
                graphics.TextRenderingHint = TextRenderingHint;

                return graphics;
            }

            /// <summary>
            /// 
            /// </summary>
            public class GetRandom
            {
                /// <summary>
                /// 
                /// </summary>
                private readonly Random Randomise = new(Environment.TickCount);

                /// <summary>
                /// 
                /// </summary>
                /// <param name="alpha"></param>
                /// <returns></returns>
                public Color ColorArgb(int alpha = 255)
                {
                    return Color.FromArgb(alpha, Int(0, 255), Int(0, 255), Int(0, 255));
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="min"></param>
                /// <param name="max"></param>
                /// <returns></returns>
                public int Int(int min, int max)
                {
                    return Randomise.Next(min, max);
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="min"></param>
                /// <param name="max"></param>
                /// <returns></returns>
                public float Float(int min, int max)
                {
                    return Randomise.Next(min * 100, max * 100) / 100;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public bool Bool()
                {
                    return Int(0, 2) == 1;
                }
            }
        }

        #endregion
    }

    #endregion
}