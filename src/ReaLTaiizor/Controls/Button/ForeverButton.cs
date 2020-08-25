#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region ForeverButton

	public class ForeverButton : Control
	{
		private int W;
		private int H;
		private bool _Rounded = false;
		private MouseStateForever State = MouseStateForever.None;

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		[Category("Colors")]
		public Color TextColor
		{
			get { return _TextColor; }
			set { _TextColor = value; }
		}

		[Category("Options")]
		public bool Rounded
		{
			get { return _Rounded; }
			set { _Rounded = value; }
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			State = MouseStateForever.Down;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			State = MouseStateForever.Over;
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = MouseStateForever.Over;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			State = MouseStateForever.None;
			Invalidate();
		}

		private Color _BaseColor = ForeverLibrary.ForeverColor;
		private Color _TextColor = Color.FromArgb(243, 243, 243);

		public ForeverButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Size = new Size(120, 40);
			BackColor = Color.Transparent;
			Font = new Font("Segoe UI", 12);
			Cursor = Cursors.Hand;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width - 1;
			H = Height - 1;

			GraphicsPath GP = new GraphicsPath();
			Rectangle Base = new Rectangle(0, 0, W, H);

			var _with8 = G;
			_with8.SmoothingMode = SmoothingMode.HighQuality;
			_with8.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with8.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with8.Clear(BackColor);

			switch (State)
			{
				case MouseStateForever.None:
					if (Rounded)
					{
						//-- Base
						GP = ForeverLibrary.RoundRec(Base, 6);
						_with8.FillPath(new SolidBrush(_BaseColor), GP);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with8.FillRectangle(new SolidBrush(_BaseColor), Base);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					break;
				case MouseStateForever.Over:
					if (Rounded)
					{
						//-- Base
						GP = ForeverLibrary.RoundRec(Base, 6);
						_with8.FillPath(new SolidBrush(_BaseColor), GP);
						_with8.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), GP);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with8.FillRectangle(new SolidBrush(_BaseColor), Base);
						_with8.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), Base);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					break;
				case MouseStateForever.Down:
					if (Rounded)
					{
						//-- Base
						GP = ForeverLibrary.RoundRec(Base, 6);
						_with8.FillPath(new SolidBrush(_BaseColor), GP);
						_with8.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), GP);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with8.FillRectangle(new SolidBrush(_BaseColor), Base);
						_with8.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), Base);

						//-- Text
						_with8.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					break;
			}

			base.OnPaint(e);
			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}

		private void UpdateColors()
		{
			ForeverColors Colors = ForeverLibrary.GetColors(this);

			_BaseColor = Colors.Forever;
		}
	}

	#endregion
}