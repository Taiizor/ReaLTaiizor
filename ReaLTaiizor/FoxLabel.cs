#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxLabel

	public class FoxLabel : Control
	{
		private Graphics G;

		public FoxLabel()
		{
			Font = new Font("Segoe UI", 10, FontStyle.Bold);
			ForeColor = FoxLibrary.ColorFromHex("#4C5864");
			DoubleBuffered = true;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(65, 19);
		}


		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Parent.BackColor);

			using (Font HFont = new Font("Segoe UI", 10, FontStyle.Bold))
			{
				using (SolidBrush HColor = new SolidBrush(ForeColor))
					G.DrawString(Text, HFont, HColor, new Point(0, 0));
			}

			base.OnPaint(e);

		}

	}

	#endregion
}