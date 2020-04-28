#region Imports

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region FoxCheckBox

	[DefaultEvent("CheckedChanged")]
	public class FoxCheckBox : FoxBase.CheckControl1
	{

		private Graphics G;
		private readonly string B64C = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKTQpQueKAAAAn0lEQVQI1yXKMU4CQRxG8TczW5nYWRCX+4it1/AUVvacArkGBQkBLmKUkBB3ne/b+VNs9ZKXXwKAOicT8cR3mVejUbo0scpf/NKSypRE7Sr1VReFdgx55D+rE3Wlq0J798SD3qeFqC+6KHR2b9BGoa3e9KPQwUvjgtYKNY0KnfxsVCr84Q+FQsdZGcOQB/ypgxezqhgi3VIr02PDyRgDd6AdcPpYOg4ZAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQxOjUyLTA1OjAwH7rbKgAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0MTo1Mi0wNTowMG7nY5YAAAAASUVORK5CYII=";

		private readonly string B64U = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKir4YIkqAAAAgUlEQVQI122OMQrCQAAENxoMxz3Aj8Y3WAv6jtzVAYPYKah/8AtC5AZdm1TqFss0y6xGseXoxb26yA172iKx5o1JDg4kzMhK9JgnJpMn6uVIwoCn7hx1lmsSplAwyfVJs2Wlr8wlR7qfOYc/Ina8MNnBgTxdeogNg5ubrnLDQFv0AXVYjzifEiowAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQyOjQyLTA1OjAwOCdgtwAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0Mjo0Mi0wNTowMEl62AsAAAAASUVORK5CYII=";
		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;


			if (Enabled)
			{

				if (Checked)
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#C8C8C8")))
					{
						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#2C9CDA")))
						{
							using (Pen BackBorder = new Pen(FoxLibrary.ColorFromHex("#2A8AC1")))
							{
								using (Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64C))))
								{
									G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

									G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));
									G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));

									G.DrawImage(I, new Point(9, 9));
								}
							}
						}
					}


				}
				else
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#C8C8C8")))
					{
						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#FF9500")))
						{
							using (Pen BackBorder = new Pen(FoxLibrary.ColorFromHex("#DC8400")))
							{
								using (Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64U))))
								{
									G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

									G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));
									G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));

									G.DrawImage(I, new Point(Width - 19, 9));
								}
							}
						}
					}

				}


			}
			else
			{

				if (Checked)
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#E6E6E6")))
					{
						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#7DB7D8")))
						{
							using (Pen BackBorder = new Pen(FoxLibrary.ColorFromHex("#7CA6BF")))
							{
								using (Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64C))))
								{
									G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

									G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));
									G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(5, 5, 17, 17), 2));

									G.DrawImage(I, new Point(9, 9));
								}
							}
						}
					}


				}
				else
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#E6E6E6")))
					{
						using (SolidBrush Background = new SolidBrush(FoxLibrary.ColorFromHex("#FFCB7C")))
						{
							using (Pen BackBorder = new Pen(FoxLibrary.ColorFromHex("#E2BD85")))
							{
								using (Image I = Image.FromStream(new MemoryStream(Convert.FromBase64String(B64U))))
								{
									G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

									G.FillPath(Background, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));
									G.DrawPath(BackBorder, FoxLibrary.RoundRect(new Rectangle(Width - 23, 5, 17, 17), 2));

									G.DrawImage(I, new Point(Width - 19, 9));
								}
							}
						}
					}

				}


			}


			base.OnPaint(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Size = new Size(55, 28);
		}

	}

	#endregion
}