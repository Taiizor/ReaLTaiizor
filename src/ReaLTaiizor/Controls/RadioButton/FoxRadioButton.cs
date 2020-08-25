#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region FoxRadioButton

	[DefaultEvent("CheckedChanged")]
	public class FoxRadioButton : Utils.FoxBase.FoxBaseRadioButton
	{
		private Graphics G;

		private Color _CheckedColor = FoxLibrary.ColorFromHex("#2C9CDA");
		public Color CheckedColor
		{
			get { return _CheckedColor; }
			set { _CheckedColor = value; }
		}

		private Color _DisabledCheckedColor = FoxLibrary.ColorFromHex("#B6B4B4");
		public Color DisabledCheckedColor
		{
			get { return _DisabledCheckedColor; }
			set { _DisabledCheckedColor = value; }
		}

		private Color _BorderColor = FoxLibrary.ColorFromHex("#C8C8C8");
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		private Color _DisabledBorderColor = FoxLibrary.ColorFromHex("#E6E6E6");
		public Color DisabledBorderColor
		{
			get { return _DisabledBorderColor; }
			set { _DisabledBorderColor = value; }
		}

		private Color _DisabledTextColor = FoxLibrary.ColorFromHex("#A6B2BE");
		public Color DisabledTextColor
		{
			get { return _DisabledTextColor; }
			set { _DisabledTextColor = value; }
		}

		private Color _HoverBorderColor = FoxLibrary.ColorFromHex("#2C9CDA");
		public Color HoverBorderColor
		{
			get { return _HoverBorderColor; }
			set { _HoverBorderColor = value; }
		}

		public FoxRadioButton() : base()
        {
			Font = new Font("Segoe UI", 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(BackColor);

			if (Enabled)
			{
				switch (State)
				{
					case FoxLibrary.MouseState.None:
						using (Pen Border = new Pen(_BorderColor))
							G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
						break;
					default:
						using (Pen Border = new Pen(_HoverBorderColor))
							G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));
						break;
				}

				using (SolidBrush TextColor = new SolidBrush(ForeColor))
					G.DrawString(Text, Font, TextColor, new Point(27, 1));
			}
			else
			{
				using (Pen Border = new Pen(_DisabledBorderColor))
					G.DrawEllipse(Border, new Rectangle(0, 0, 20, 20));

				using (SolidBrush TextColor = new SolidBrush(_DisabledTextColor))
					G.DrawString(Text, Font, TextColor, new Point(27, 1));
			}

			if (Checked)
			{
				if (Enabled)
				{
					using (SolidBrush FillColor = new SolidBrush(_CheckedColor))
						G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
				}
				else
				{
					using (SolidBrush FillColor = new SolidBrush(_DisabledCheckedColor))
						G.FillEllipse(FillColor, new Rectangle(4, 4, 12, 12));
				}
			}

			base.OnPaint(e);
		}
	}

	#endregion
}