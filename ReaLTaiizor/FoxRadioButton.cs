#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxRadioButton

	[DefaultEvent("CheckedChanged")]
	public class FoxRadioButton : FoxBase.FoxBaseRadioButton
	{

		private Graphics G;
		public bool Bold { get; set; }

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

						using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#C8C8C8")))
						{
							G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
						}


						break;
					default:

						using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#2C9CDA")))
						{
							G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
						}


						break;
				}

				using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#424E5A")))
				{


					if (Bold)
					{
						using (Font BFont = new Font("Segoe UI", 10, FontStyle.Bold))
						{
							G.DrawString(Text, BFont, TextColor, new Point(27, 1));
						}

					}
					else
					{
						using (Font DFont = new Font("Segoe UI", 10))
						{
							G.DrawString(Text, DFont, TextColor, new Point(27, 1));
						}

					}

				}


			}
			else
			{
				using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#E6E6E6")))
				{
					G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
				}

				using (SolidBrush TextColor = new SolidBrush(FoxLibrary.ColorFromHex("#A6B2BE")))
				{


					if (Bold)
					{
						using (Font BFont = new Font("Segoe UI", 10, FontStyle.Bold))
						{
							G.DrawString(Text, BFont, TextColor, new Point(27, 1));
						}

					}
					else
					{
						using (Font DFont = new Font("Segoe UI", 10))
						{
							G.DrawString(Text, DFont, TextColor, new Point(27, 1));
						}

					}

				}

			}


			if (Checked)
			{

				if (Enabled)
				{
					using (SolidBrush FillColor = new SolidBrush(FoxLibrary.ColorFromHex("#2C9CDA")))
					{
						G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
					}


				}
				else
				{
					using (SolidBrush FillColor = new SolidBrush(FoxLibrary.ColorFromHex("#B6B4B4")))
					{
						G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
					}

				}

			}

			base.OnPaint(e);
		}

	}

	#endregion
}