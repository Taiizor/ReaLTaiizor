#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxButton

	public class FoxButton : FoxBase.ButtonFoxBase
	{

		private Graphics G;
		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Parent.BackColor);

			if (Enabled)
			{
				switch (State)
				{

					case FoxLibrary.MouseState.None:

						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#F9F9F9")))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
					case FoxLibrary.MouseState.Over:

						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#F2F2F2")))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
					case FoxLibrary.MouseState.Down:

						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#E8E8E8")))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
				}

				using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#C1C1C1")))
					G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

				using (Font TextFont = new Font("Segoe UI", 10))
				{
					using (SolidBrush TextColor = new SolidBrush(ForeColor))
						FoxLibrary.CenterString(G, Text, TextFont, TextColor.Color, new Rectangle(3, 0, Width, Height));
				}


			}
			else
			{
				using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#F9F9F9")))
				{
					G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
				}

				using (Font TextFont = new Font("Segoe UI", 10))
				{
					using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#A6B2BE")))
					{
						FoxLibrary.CenterString(G, Text, TextFont, TextColor.Color, new Rectangle(3, 0, Width, Height));
					}
				}

				using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#D1D1D1")))
				{
					G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
				}

			}

			base.OnPaint(e);
		}

	}

	#endregion
}