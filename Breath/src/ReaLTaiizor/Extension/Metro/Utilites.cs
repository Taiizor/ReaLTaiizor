#region Imports

using ReaLTaiizor.Native;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region UtilitesExtension

    internal class Utilites
    {
        public static PathGradientBrush GlowBrush(Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
        {
            GraphicsPath GP = new() { FillMode = FillMode.Winding };
            GP.AddRectangle(Rect);
            return new PathGradientBrush(GP)
            {
                CenterColor = CenterColor,
                SurroundColors = new[] { SurroundColor },
                FocusScales = P
            };
        }

        public static SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
        {
            return new SolidBrush(Color.FromArgb(A, R, G, B));
        }

        public SolidBrush SolidBrushHTMlColor(string C_WithoutHash)
        {
            return new SolidBrush(HexColor(C_WithoutHash));
        }

        public static Pen PenRGBColor(int red, int green, int blue, int alpha, float size)
        {
            return new(Color.FromArgb(alpha, red, green, blue), size);
        }

        public Pen PenHTMlColor(string colorWithoutHash, float size = 1)
        {
            return new(HexColor(colorWithoutHash), size);
        }

        public Color HexColor(string hexColor)
        {
            return ColorTranslator.FromHtml(hexColor);
        }

        public static Color GetAlphaHexColor(int alpha, string hexColor)
        {
            return Color.FromArgb(alpha, ColorTranslator.FromHtml(hexColor));
        }

        public void InitControlHandle(Control ctrl)
        {
            if (ctrl.IsHandleCreated)
            {
                return;
            }

            //IntPtr unused = ctrl.Handle;
            foreach (Control child in ctrl.Controls)
            {
                InitControlHandle(child);
            }
        }

        public void SmoothCursor(ref Message message)
        {
            if (message.Msg != User32.WM_SETCURSOR)
            {
                return;
            }

            User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
            message.Result = IntPtr.Zero;
        }

        public void SmoothCursor(ref Message message, Cursor Cursor)
        {
            if (message.Msg != User32.WM_SETCURSOR && Cursor != Cursors.Hand)
            {
                return;
            }

            User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
            message.Result = IntPtr.Zero;
        }

        public static void NormalCursor(ref Message message, Cursor Cursor)
        {
            if (message.Msg == User32.WM_SETCURSOR && Cursor == Cursors.Hand)
            {
                User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
                message.Result = IntPtr.Zero;
            }
        }
    }

    #endregion
}