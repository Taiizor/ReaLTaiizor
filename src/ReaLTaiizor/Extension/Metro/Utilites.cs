#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
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

		public Pen PenRGBColor(int red, int green, int blue, int alpha, float size)
		{
			return new Pen(Color.FromArgb(alpha, red, green, blue), size);
		}

		public Pen PenHTMlColor(string colorWithoutHash, float size = 1)
		{
			return new Pen(HexColor(colorWithoutHash), size);
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
			if (ctrl.IsHandleCreated)
				return;
			var unused = ctrl.Handle;
			foreach (Control child in ctrl.Controls)
				InitControlHandle(child);
		}

		public void SmoothCursor(ref Message message)
		{
			if (message.Msg != User32.WM_SETCURSOR)
				return;
			User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
			message.Result = IntPtr.Zero;
		}

	}

	#endregion
}