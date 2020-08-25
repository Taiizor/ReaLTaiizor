#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region FoxBigLabel

	public class FoxBigLabel : Control
	{
		private Graphics G;
		private Color _LineColor = FoxLibrary.ColorFromHex("#C8C8C8");
		private Direction _Line = Direction.Bottom;

		public Color LineColor
		{
			get { return _LineColor; }
			set { _LineColor = value; }
		}

		public Direction Line
        {
			get { return _Line; }
			set { _Line = value; }
		}

		public enum Direction
        {
			Top,
			Bottom
        }

		public FoxBigLabel()
		{
			Font = new Font("Segoe UI Semibold", 20);
			ForeColor = FoxLibrary.ColorFromHex("#4C5864");
			DoubleBuffered = true;
			Size = new Size(165, 51);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//Size = new Size(Width, 51);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Parent.BackColor);
			using (SolidBrush HColor = new SolidBrush(ForeColor))
				G.DrawString(Text, Font, HColor, new Point(0, 0));

			using (Pen BottomLine = new Pen(_LineColor))
			{
				if (_Line == Direction.Bottom)
					G.DrawLine(BottomLine, new Point(0, Height - 1), new Point(Width, Height - 1));
				else
					G.DrawLine(BottomLine, new Point(0, 0), new Point(Width, 0));
			}

			base.OnPaint(e);

		}

	}

	#endregion
}