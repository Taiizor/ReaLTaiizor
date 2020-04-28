#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxBigLabel

	public class FoxBigLabel : Control
	{
		private Graphics G;

		public FoxBigLabel()
		{
			Font = new Font("Segoe UI Semibold", 20);
			ForeColor = FoxLibrary.ColorFromHex("#4C5864");
			DoubleBuffered = true;
			Size = new Size(165, Height);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(Width, 51);
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Parent.BackColor);

			using (Font HFont = new Font("Segoe UI Semibold", 20))
			{
				using (SolidBrush HColor = new SolidBrush(FoxLibrary.ColorFromHex("#4C5864")))
				{
					G.DrawString(Text, HFont, HColor, new Point(0, 0));
				}
			}

			using (Pen BottomLine = new Pen(FoxLibrary.ColorFromHex("#C8C8C8")))
			{
				G.DrawLine(BottomLine, new Point(0, 50), new Point(Width, 50));
			}

			base.OnPaint(e);

		}

	}

	#endregion
}