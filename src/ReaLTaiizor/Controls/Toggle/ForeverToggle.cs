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
	#region ForeverToggle

	[DefaultEvent("CheckedChanged")]
	public class ForeverToggle : Control
	{
		private int W;
		private int H;
		private _Options O;
		private bool _Checked = false;
		private MouseStateForever State = MouseStateForever.None;

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		[Flags()]
		public enum _Options
		{
			Style1,
			Style2,
			Style3
		}

		[Category("Options")]
		public _Options Options
		{
			get { return O; }
			set { O = value; }
		}

		[Category("Options")]
		public bool Checked
		{
			get { return _Checked; }
			set { _Checked = value; }
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Width = 76;
			Height = 33;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = MouseStateForever.Over;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			State = MouseStateForever.Down;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			State = MouseStateForever.None;
			Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			State = MouseStateForever.Over;
			Invalidate();
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			_Checked = !_Checked;
			CheckedChanged?.Invoke(this);
		}

		private Color _BaseColor = ForeverLibrary.ForeverColor;
		private Color _BaseColorRed = Color.FromArgb(220, 85, 96);
		private Color _BGColor = Color.FromArgb(84, 85, 86);
		private Color _ToggleColor = Color.FromArgb(45, 47, 49);
		private Color _TextColor = Color.FromArgb(243, 243, 243);

		[Category("Options")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		[Category("Options")]
		public Color BaseColorRed
		{
			get { return _BaseColorRed; }
			set { _BaseColorRed = value; }
		}

		[Category("Options")]
		public Color BGColor
		{
			get { return _BGColor; }
			set { _BGColor = value; }
		}

		[Category("Options")]
		public Color ToggleColor
		{
			get { return _ToggleColor; }
			set { _ToggleColor = value; }
		}

		[Category("Options")]
		public Color TextColor
		{
			get { return _TextColor; }
			set { _TextColor = value; }
		}

		public ForeverToggle()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			Size = new Size(44, Height + 1);
			Cursor = Cursors.Hand;
			Font = new Font("Segoe UI", 10);
			Size = new Size(76, 33);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width - 1;
			H = Height - 1;

			GraphicsPath GP = new GraphicsPath();
			GraphicsPath GP2 = new GraphicsPath();
			Rectangle Base = new Rectangle(0, 0, W, H);
			Rectangle Toggle = new Rectangle(Convert.ToInt32(W / 2), 0, 38, H);

			var _with9 = G;
			_with9.SmoothingMode = SmoothingMode.HighQuality;
			_with9.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with9.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with9.Clear(BackColor);

			switch (O)
			{
				case _Options.Style1:
					//-- Style 1
					//-- Base
					GP = ForeverLibrary.RoundRec(Base, 6);
					GP2 = ForeverLibrary.RoundRec(Toggle, 6);
					_with9.FillPath(new SolidBrush(_BGColor), GP);
					_with9.FillPath(new SolidBrush(_ToggleColor), GP2);

					//-- Text
					_with9.DrawString("OFF", Font, new SolidBrush(_BGColor), new Rectangle(19, 1, W, H), ForeverLibrary.CenterSF);

					if (Checked)
					{
						//-- Base
						GP = ForeverLibrary.RoundRec(Base, 6);
						GP2 = ForeverLibrary.RoundRec(new Rectangle(Convert.ToInt32(W / 2), 0, 38, H), 6);
						_with9.FillPath(new SolidBrush(_ToggleColor), GP);
						_with9.FillPath(new SolidBrush(_BaseColor), GP2);

						//-- Text
						_with9.DrawString("ON", Font, new SolidBrush(_BaseColor), new Rectangle(8, 7, W, H), ForeverLibrary.NearSF);
					}
					break;
				case _Options.Style2:
					//-- Style 2
					//-- Base
					GP = ForeverLibrary.RoundRec(Base, 6);
					Toggle = new Rectangle(4, 4, 36, H - 8);
					GP2 = ForeverLibrary.RoundRec(Toggle, 4);
					_with9.FillPath(new SolidBrush(_BaseColorRed), GP);
					_with9.FillPath(new SolidBrush(_ToggleColor), GP2);

					//-- Lines
					_with9.DrawLine(new Pen(_BGColor), 18, 20, 18, 12);
					_with9.DrawLine(new Pen(_BGColor), 22, 20, 22, 12);
					_with9.DrawLine(new Pen(_BGColor), 26, 20, 26, 12);

					//-- Text
					_with9.DrawString("r", new Font("Marlett", 8), new SolidBrush(_TextColor), new Rectangle(19, 2, Width, Height), ForeverLibrary.CenterSF);

					if (Checked)
					{
						GP = ForeverLibrary.RoundRec(Base, 6);
						Toggle = new Rectangle(Convert.ToInt32(W / 2) - 2, 4, 36, H - 8);
						GP2 = ForeverLibrary.RoundRec(Toggle, 4);
						_with9.FillPath(new SolidBrush(_BaseColor), GP);
						_with9.FillPath(new SolidBrush(_ToggleColor), GP2);

						//-- Lines
						_with9.DrawLine(new Pen(_BGColor), Convert.ToInt32(W / 2) + 12, 20, Convert.ToInt32(W / 2) + 12, 12);
						_with9.DrawLine(new Pen(_BGColor), Convert.ToInt32(W / 2) + 16, 20, Convert.ToInt32(W / 2) + 16, 12);
						_with9.DrawLine(new Pen(_BGColor), Convert.ToInt32(W / 2) + 20, 20, Convert.ToInt32(W / 2) + 20, 12);

						//-- Text
						_with9.DrawString("ü", new Font("Wingdings", 14), new SolidBrush(_TextColor), new Rectangle(8, 7, Width, Height), ForeverLibrary.NearSF);
					}
					break;
				case _Options.Style3:
					//-- Style 3
					//-- Base
					GP = ForeverLibrary.RoundRec(Base, 16);
					Toggle = new Rectangle(W - 28, 4, 22, H - 8);
					GP2.AddEllipse(Toggle);
					_with9.FillPath(new SolidBrush(_ToggleColor), GP);
					_with9.FillPath(new SolidBrush(_BaseColorRed), GP2);

					//-- Text
					_with9.DrawString("OFF", Font, new SolidBrush(_BaseColorRed), new Rectangle(-12, 2, W, H), ForeverLibrary.CenterSF);

					if (Checked)
					{
						//-- Base
						GP = ForeverLibrary.RoundRec(Base, 16);
						Toggle = new Rectangle(6, 4, 22, H - 8);
						GP2.Reset();
						GP2.AddEllipse(Toggle);
						_with9.FillPath(new SolidBrush(_ToggleColor), GP);
						_with9.FillPath(new SolidBrush(_BaseColor), GP2);

						//-- Text
						_with9.DrawString("ON", Font, new SolidBrush(_BaseColor), new Rectangle(12, 2, W, H), ForeverLibrary.CenterSF);
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