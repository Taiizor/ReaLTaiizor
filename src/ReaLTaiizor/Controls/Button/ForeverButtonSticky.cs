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
	#region ForeverButtonSticky

	public class ForeverButtonSticky : Control
	{
		private int W;
		private int H;
		private MouseStateForever State = MouseStateForever.None;
		private bool _Rounded = false;

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

		private bool[] GetConnectedSides()
		{
			bool[] Bool = new bool[4] { false, false, false, false };

			foreach (Control B in Parent.Controls)
			{
				if (B is ForeverButtonSticky)
				{
					if (object.ReferenceEquals(B, this) || !Rect.IntersectsWith(Rect))
						continue;
					double A = (Math.Atan2(Left - B.Left, Top - B.Top) * 2 / Math.PI);
					if (A / 1 == A)
						Bool[(int)A + 1] = true;
				}
			}

			return Bool;
		}

		private Rectangle Rect
		{
			get { return new Rectangle(Left, Top, Width, Height); }
		}

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

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			//Height = 32
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			//Size = New Size(112, 32)
		}

		private Color _BaseColor = ForeverLibrary.ForeverColor;
		private Color _TextColor = Color.FromArgb(243, 243, 243);

		public ForeverButtonSticky()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			Size = new Size(160, 40);
			BackColor = Color.Transparent;
			Font = new Font("Segoe UI", 12);
			Cursor = Cursors.Hand;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width;
			H = Height;

			GraphicsPath GP = new GraphicsPath();

			bool[] GCS = GetConnectedSides();
			// dynamic RoundedBase = ForeverLibrary.RoundRect(0, 0, W, H, ???, !(GCS(2) | GCS(1)), !(GCS(1) | GCS(0)), !(GCS(3) | GCS(0)), !(GCS(3) | GCS(2)));
			GraphicsPath RoundedBase = ForeverLibrary.RoundRect(0, 0, W, H, 0.3, !(GCS[2] || GCS[1]), !(GCS[1] || GCS[0]), !(GCS[3] || GCS[0]), !(GCS[3] || GCS[2]));
			Rectangle Base = new Rectangle(0, 0, W, H);

			var _with17 = G;
			_with17.SmoothingMode = SmoothingMode.HighQuality;
			_with17.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with17.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with17.Clear(BackColor);

			switch (State)
			{
				case MouseStateForever.None:
					if (Rounded)
					{
						//-- Base
						GP = RoundedBase;
						_with17.FillPath(new SolidBrush(_BaseColor), GP);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with17.FillRectangle(new SolidBrush(_BaseColor), Base);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					break;
				case MouseStateForever.Over:
					if (Rounded)
					{
						//-- Base
						GP = RoundedBase;
						_with17.FillPath(new SolidBrush(_BaseColor), GP);
						_with17.FillPath(new SolidBrush(Color.FromArgb(20, Color.White)), GP);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with17.FillRectangle(new SolidBrush(_BaseColor), Base);
						_with17.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), Base);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					break;
				case MouseStateForever.Down:
					if (Rounded)
					{
						//-- Base
						GP = RoundedBase;
						_with17.FillPath(new SolidBrush(_BaseColor), GP);
						_with17.FillPath(new SolidBrush(Color.FromArgb(20, Color.Black)), GP);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
					}
					else
					{
						//-- Base
						_with17.FillRectangle(new SolidBrush(_BaseColor), Base);
						_with17.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), Base);

						//-- Text
						_with17.DrawString(Text, Font, new SolidBrush(_TextColor), Base, ForeverLibrary.CenterSF);
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