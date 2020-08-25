#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region FoxLinkLabel

	public class FoxLinkLabel : Utils.FoxBase.ButtonFoxBase
	{
		private Graphics G;

		private Color _DownColor = FoxLibrary.ColorFromHex("#FF9500");
		public Color DownColor
		{
			get { return _DownColor; }
			set { _DownColor = value; }
		}

		private Color _OverColor = FoxLibrary.ColorFromHex("#178CE5");
		public Color OverColor
		{
			get { return _OverColor; }
			set { _OverColor = value; }
		}

		public FoxLinkLabel() : base()
        {
			Size = new Size(82, 18);
			Font = new Font("Segoe UI", 10);
			ForeColor = FoxLibrary.ColorFromHex("#0095DD");
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			switch (State)
			{
				case FoxLibrary.MouseState.Over:
					using (SolidBrush TextColor = new SolidBrush(_OverColor))
					{
						using (Font TextFont = new Font(Font.FontFamily, Font.Size, FontStyle.Underline))
							G.DrawString(Text, TextFont, TextColor, new Point(0, 0));
					}
					break;
				case FoxLibrary.MouseState.Down:
					using (SolidBrush TextColor = new SolidBrush(_DownColor))
						G.DrawString(Text, Font, TextColor, new Point(0, 0));
					break;
				default:
					using (SolidBrush TextColor = new SolidBrush(ForeColor))
						G.DrawString(Text, Font, TextColor, new Point(0, 0));
					break;
			}

			base.OnPaint(e);
		}

	}

	#endregion
}