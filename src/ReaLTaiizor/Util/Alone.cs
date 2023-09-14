#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

#endregion

namespace ReaLTaiizor.Util
{
    #region AloneUtil

    public sealed class AloneLibrary
    {
        public enum RoundingStyle : byte
        {
            All,
            Top,
            Bottom,
            Left,
            Right,
            TopRight,
            BottomRight
        }

        public static void CenterString(Graphics G, string T, Font F, Color C, Rectangle R)
        {
            SizeF sizeF = G.MeasureString(T, F);
            using SolidBrush solidBrush = new(C);
            G.DrawString(T, F, solidBrush, checked(new Point((int)Math.Round(unchecked((R.Width / 2.0) - (double)(sizeF.Width / 2f))), (int)Math.Round(unchecked((R.Height / 2.0) - (double)(sizeF.Height / 2f))))));
        }

        public static Color ColorFromHex(string Hex)
        {
            return Color.FromArgb(checked((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), NumberStyles.HexNumber)));
        }

        public static Rectangle FullRectangle(Size S, bool Subtract)
        {
            Rectangle result;
            if (Subtract)
            {
                result = checked(new Rectangle(0, 0, S.Width - 1, S.Height - 1));
            }
            else
            {
                result = new(0, 0, S.Width, S.Height);
            }

            return result;
        }

        public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, RoundingStyle Style = RoundingStyle.All)
        {
            GraphicsPath graphicsPath = new();
            checked
            {
                int num = Rounding * 2;
                graphicsPath.StartFigure();
                bool flag = Rounding == 0;
                GraphicsPath result;
                if (flag)
                {
                    graphicsPath.AddRectangle(Rect);
                    graphicsPath.CloseAllFigures();
                    result = graphicsPath;
                }
                else
                {
                    switch (Style)
                    {
                        case RoundingStyle.All:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case RoundingStyle.Top:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                        case RoundingStyle.Bottom:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case RoundingStyle.Left:
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Y, num, num), -180f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                            graphicsPath.AddArc(new Rectangle(Rect.X, Rect.Height - num + Rect.Y, num, num), 90f, 90f);
                            break;
                        case RoundingStyle.Right:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            break;
                        case RoundingStyle.TopRight:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Y, num, num), -90f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                            graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                        case RoundingStyle.BottomRight:
                            graphicsPath.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                            graphicsPath.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                            graphicsPath.AddArc(new Rectangle(Rect.Width - num + Rect.X, Rect.Height - num + Rect.Y, num, num), 0f, 90f);
                            graphicsPath.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                            break;
                    }
                    graphicsPath.CloseAllFigures();
                    result = graphicsPath;
                }
                return result;
            }
        }
    }

    #endregion
}