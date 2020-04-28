#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxLinkLabel

	public class FoxLinkLabel : FoxBase.ButtonFoxBase
	{


		private Graphics G;

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			switch (State)
			{

				case FoxLibrary.MouseState.Over:

					using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#178CE5")))
					{
						using (Font TextFont = new Font("Segoe UI", 10, FontStyle.Underline))
						{
							G.DrawString(Text, TextFont, TextColor, new Point(0, 0));
						}
					}


					break;
				case FoxLibrary.MouseState.Down:

					using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#FF9500")))
					{
						using (Font TextFont = new Font("Segoe UI", 10))
						{
							G.DrawString(Text, TextFont, TextColor, new Point(0, 0));
						}
					}


					break;
				default:

					using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#0095DD")))
					{
						using (Font TextFont = new Font("Segoe UI", 10))
						{
							G.DrawString(Text, TextFont, TextColor, new Point(0, 0));
						}
					}


					break;
			}

			base.OnPaint(e);

		}

	}

	#endregion
}