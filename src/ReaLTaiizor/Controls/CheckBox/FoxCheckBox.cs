#region Imports

using System;
using System.IO;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region FoxCheckBox

	[DefaultEvent("CheckedChanged")]
	public class FoxCheckBox : Utils.FoxBase.CheckControlBox
	{
		private Graphics G;

		private readonly string B64C = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKTQpQueKAAAAn0lEQVQI1yXKMU4CQRxG8TczW5nYWRCX+4it1/AUVvacArkGBQkBLmKUkBB3ne/b+VNs9ZKXXwKAOicT8cR3mVejUbo0scpf/NKSypRE7Sr1VReFdgx55D+rE3Wlq0J798SD3qeFqC+6KHR2b9BGoa3e9KPQwUvjgtYKNY0KnfxsVCr84Q+FQsdZGcOQB/ypgxezqhgi3VIr02PDyRgDd6AdcPpYOg4ZAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQxOjUyLTA1OjAwH7rbKgAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0MTo1Mi0wNTowMG7nY5YAAAAASUVORK5CYII=";
		private readonly string B64U = "iVBORw0KGgoAAAANSUhEUgAAAAoAAAAKCAQAAAAnOwc2AAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QA/4ePzL8AAAAHdElNRQffCxwIKir4YIkqAAAAgUlEQVQI122OMQrCQAAENxoMxz3Aj8Y3WAv6jtzVAYPYKah/8AtC5AZdm1TqFss0y6xGseXoxb26yA172iKx5o1JDg4kzMhK9JgnJpMn6uVIwoCn7hx1lmsSplAwyfVJs2Wlr8wlR7qfOYc/Ina8MNnBgTxdeogNg5ubrnLDQFv0AXVYjzifEiowAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE1LTExLTI4VDA4OjQyOjQyLTA1OjAwOCdgtwAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxNS0xMS0yOFQwODo0Mjo0Mi0wNTowMEl62AsAAAAASUVORK5CYII=";

		private Color _CheckedBorderColorA = FoxLibrary.ColorFromHex("#C8C8C8");
		public Color CheckedBorderColorA
		{
			get { return _CheckedBorderColorA; }
			set { _CheckedBorderColorA = value; }
		}

		private Color _CheckedBorderColorB = FoxLibrary.ColorFromHex("#2A8AC1");
		public Color CheckedBorderColorB
		{
			get { return _CheckedBorderColorB; }
			set { _CheckedBorderColorB = value; }
		}

		private Color _UncheckedBorderColorA = FoxLibrary.ColorFromHex("#C8C8C8");
		public Color UncheckedBorderColorA
		{
			get { return _UncheckedBorderColorA; }
			set { _UncheckedBorderColorA = value; }
		}

		private Color _UncheckedBorderColorB = FoxLibrary.ColorFromHex("#DC8400");
		public Color UncheckedBorderColorB
		{
			get { return _UncheckedBorderColorB; }
			set { _UncheckedBorderColorB = value; }
		}

		private Color _CheckedColor = FoxLibrary.ColorFromHex("#2C9CDA");
		public Color CheckedColor
		{
			get { return _CheckedColor; }
			set { _CheckedColor = value; }
		}

		private Color _UncheckedColor = FoxLibrary.ColorFromHex("#FF9500");
		public Color UncheckedColor
		{
			get { return _UncheckedColor; }
			set { _UncheckedColor = value; }
		}

		private Color _DisabledCheckedBorderColorA = FoxLibrary.ColorFromHex("#E6E6E6");
		public Color DisabledCheckedBorderColorA
		{
			get { return _DisabledCheckedBorderColorA; }
			set { _DisabledCheckedBorderColorA = value; }
		}

		private Color _DisabledCheckedBorderColorB = FoxLibrary.ColorFromHex("#7CA6BF");
		public Color DisabledCheckedBorderColorB
		{
			get { return _DisabledCheckedBorderColorB; }
			set { _DisabledCheckedBorderColorB = value; }
		}

		private Color _DisabledCheckedColor = FoxLibrary.ColorFromHex("#7DB7D8");
		public Color DisabledCheckedColor
		{
			get { return _DisabledCheckedColor; }
			set { _DisabledCheckedColor = value; }
		}

		private Color _DisabledUncheckedBorderColorA = FoxLibrary.ColorFromHex("#E6E6E6");
		public Color DisabledUncheckedBorderColorA
		{
			get { return _DisabledUncheckedBorderColorA; }
			set { _DisabledUncheckedBorderColorA = value; }
		}

		private Color _DisabledUncheckedBorderColorB = FoxLibrary.ColorFromHex("#E2BD85");
		public Color DisabledUncheckedBorderColorB
		{
			get { return _DisabledUncheckedBorderColorB; }
			set { _DisabledUncheckedBorderColorB = value; }
		}

		private Color _DisabledUncheckedColor = FoxLibrary.ColorFromHex("#FFCB7C");
		public Color DisabledUncheckedColor
		{
			get { return _DisabledUncheckedColor; }
			set { _DisabledUncheckedColor = value; }
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			if (Enabled)
			{
				if (Checked)
				{
					using (Pen Border = new Pen(_CheckedBorderColorA))
					{
						using (SolidBrush Background = new SolidBrush(_CheckedColor))
						{
							using (Pen BackBorder = new Pen(_CheckedBorderColorB))
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
					using (Pen Border = new Pen(_UncheckedBorderColorA))
					{
						using (SolidBrush Background = new SolidBrush(_UncheckedColor))
						{
							using (Pen BackBorder = new Pen(_UncheckedBorderColorB))
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
					using (Pen Border = new Pen(_DisabledCheckedBorderColorA))
					{
						using (SolidBrush Background = new SolidBrush(_DisabledCheckedColor))
						{
							using (Pen BackBorder = new Pen(_DisabledCheckedBorderColorB))
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
					using (Pen Border = new Pen(_DisabledUncheckedBorderColorA))
					{
						using (SolidBrush Background = new SolidBrush(_DisabledUncheckedColor))
						{
							using (Pen BackBorder = new Pen(_DisabledUncheckedBorderColorB))
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