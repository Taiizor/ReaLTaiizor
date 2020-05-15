#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxNotification

	public class FoxNotification : FoxBase.NotifyFoxBase
	{

		public Styles Style { get; set; }

		private Graphics G;
		private Color Background;
		private Color TextColor;

		private Color LeftBar;
		public enum Styles : byte
		{
			Green = 0,
			Blue = 1,
			Yellow = 2,
			Red = 3
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			switch (Style)
			{
				case Styles.Green:
					Background = FoxLibrary.ColorFromHex("#DFF0D6");
					TextColor = FoxLibrary.ColorFromHex("#4E8C45");
					LeftBar = FoxLibrary.ColorFromHex("#CEE5B6");

					break;
				case Styles.Blue:
					Background = FoxLibrary.ColorFromHex("#D9EDF8");
					TextColor = FoxLibrary.ColorFromHex("#498FB8");
					LeftBar = FoxLibrary.ColorFromHex("#AFD9F0");

					break;
				case Styles.Yellow:
					Background = FoxLibrary.ColorFromHex("#FCF8E1");
					TextColor = FoxLibrary.ColorFromHex("#908358");
					LeftBar = FoxLibrary.ColorFromHex("#FAEBC8");

					break;
				case Styles.Red:
					Background = FoxLibrary.ColorFromHex("#F2DEDE");
					TextColor = FoxLibrary.ColorFromHex("#C2635E");
					LeftBar = FoxLibrary.ColorFromHex("#EBCCD1");

					break;
			}

			using (Font TextFont = new Font("Segoe UI", 10))
			{
				using (SolidBrush Back = new SolidBrush(Background))
				{
					using (SolidBrush TC = new SolidBrush(TextColor))
					{
						using (SolidBrush LB = new SolidBrush(LeftBar))
						{
							G.FillRectangle(Back, FoxLibrary.FullRectangle(Size, true));
							G.SmoothingMode = SmoothingMode.None;
							G.FillRectangle(LB, new Rectangle(0, 1, 6, Height - 2));
							G.SmoothingMode = SmoothingMode.HighQuality;
							G.DrawString(Text, TextFont, TC, new Point(20, 11));
						}
					}
				}
			}

			base.OnPaint(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(Width, 40);
		}

	}

	#endregion
}