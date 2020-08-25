#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region FoxButton

	public class FoxButton : Utils.FoxBase.ButtonFoxBase
	{

		private Graphics G;

		private Color _BaseColor = FoxLibrary.ColorFromHex("#F9F9F9");
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		private Color _OverColor = FoxLibrary.ColorFromHex("#F2F2F2");
		public Color OverColor
		{
			get { return _OverColor; }
			set { _OverColor = value; }
		}

		private Color _DownColor = FoxLibrary.ColorFromHex("#E8E8E8");
		public Color DownColor
		{
			get { return _DownColor; }
			set { _DownColor = value; }
		}

		private Color _BorderColor = FoxLibrary.ColorFromHex("#C1C1C1");
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		private Color _DisabledBaseColor = FoxLibrary.ColorFromHex("#F9F9F9");
		public Color DisabledBaseColor
		{
			get { return _DisabledBaseColor; }
			set { _DisabledBaseColor = value; }
		}

		private Color _DisabledTextColor = FoxLibrary.ColorFromHex("#A6B2BE");
		public Color DisabledTextColor
		{
			get { return _DisabledTextColor; }
			set { _DisabledTextColor = value; }
		}

		private Color _DisabledBorderColor = FoxLibrary.ColorFromHex("#D1D1D1");
		public Color DisabledBorderColor
		{
			get { return _DisabledBorderColor; }
			set { _DisabledBorderColor = value; }
		}

		public FoxButton() : base()
        {
			Font = new Font("Segoe UI", 10);
		}

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
						using (SolidBrush Background = new SolidBrush(_BaseColor))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
					case FoxLibrary.MouseState.Over:

						using (SolidBrush Background = new SolidBrush(_OverColor))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
					case FoxLibrary.MouseState.Down:
						using (SolidBrush Background = new SolidBrush(_DownColor))
							G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
						break;
				}

				using (Pen Border = new Pen(_BorderColor))
					G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

				using (SolidBrush TextColor = new SolidBrush(ForeColor))
					FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));
			}
			else
			{
				using (SolidBrush Background = new SolidBrush(_DisabledBaseColor))
					G.FillPath(Background, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));

				using (SolidBrush TextColor = new SolidBrush(_DisabledTextColor))
					FoxLibrary.CenterString(G, Text, Font, TextColor.Color, new Rectangle(3, 0, Width, Height));

				using (Pen Border = new Pen(_DisabledBorderColor))
					G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
			}

			base.OnPaint(e);
		}

	}

	#endregion
}