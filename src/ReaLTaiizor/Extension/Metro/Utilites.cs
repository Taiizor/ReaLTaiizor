#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Extension.Metro
{
    #region UtilitesExtension

    internal class Utilites
    {
        public static PathGradientBrush GlowBrush(Color CenterColor, Color SurroundColor, Point P, Rectangle Rect)
        {
            GraphicsPath GP = new GraphicsPath { FillMode = FillMode.Winding };
            GP.AddRectangle(Rect);
            return new PathGradientBrush(GP)
            {
                CenterColor = CenterColor,
                SurroundColors = new[] { SurroundColor },
                FocusScales = P
            };
        }

        public SolidBrush SolidBrushRGBColor(int R, int G, int B, int A = 0)
        {
            return new SolidBrush(Color.FromArgb(A, R, G, B));
        }

        public SolidBrush SolidBrushHTMlColor(string C_WithoutHash)
        {
            return new SolidBrush(HexColor(C_WithoutHash));
        }

        public Pen PenRGBColor(int R, int G, int B, int A, float Size)
        {
            return new Pen(Color.FromArgb(A, R, G, B), Size);
        }

        public Pen PenHTMlColor(string C_WithoutHash, float Size = 1)
        {
            return new Pen(HexColor(C_WithoutHash), Size);
        }

        public Color HexColor(string hexColor)
        {
            return ColorTranslator.FromHtml(hexColor);
        }

        public Color GetAlphaHexColor(int alpha, string hexColor)
        {
            return Color.FromArgb(alpha, ColorTranslator.FromHtml(hexColor));
        }

        public void InitControlHandle(Control ctrl)
        {
            if (!ctrl.IsHandleCreated)
            {
                var unused = ctrl.Handle;
                foreach (Control child in ctrl.Controls)
                    InitControlHandle(child);
            }
        }
    }

    #endregion
}