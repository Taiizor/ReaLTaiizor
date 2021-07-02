#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Util
{
    #region ThunderUtil

    public enum MouseStateThunder : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3,
    }

    public static class DrawThunder
    {
        public static GraphicsPath RoundRect(Rectangle rect, int Curve)
        {
            GraphicsPath P = new();
            int ArcRectWidth = Curve * 2;
            P.AddArc(new Rectangle(rect.X, rect.Y, ArcRectWidth, ArcRectWidth), -180, 90);
            P.AddArc(new Rectangle(rect.Width - ArcRectWidth + rect.X, rect.Y, ArcRectWidth, ArcRectWidth), -90, 90);
            P.AddArc(new Rectangle(rect.Width - ArcRectWidth + rect.X, rect.Height - ArcRectWidth + rect.Y, ArcRectWidth, ArcRectWidth), 0, 90);
            P.AddArc(new Rectangle(rect.X, rect.Height - ArcRectWidth + rect.Y, ArcRectWidth, ArcRectWidth), 90, 90);
            P.AddLine(new Point(rect.X, rect.Height - ArcRectWidth + rect.Y), new Point(rect.X, Curve + rect.Y));
            return P;
        }

        public static GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
        {
            return RoundRect(new Rectangle(X, Y, Width, Height), Curve);
        }
    }

    #endregion
}