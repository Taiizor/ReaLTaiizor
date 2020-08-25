#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region ForeverComboBox

	public class ForeverComboBox : System.Windows.Forms.ComboBox
	{
		private int W;
		private int H;
		private int _StartIndex = 0;
		private int x;
		private int y;

		private MouseStateForever State = MouseStateForever.None;
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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			x = e.Location.X;
			y = e.Location.Y;
			Invalidate();
			if (e.X < Width - 41)
				Cursor = Cursors.Default; //Cursors.IBeam
			else
				Cursor = Cursors.Hand;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
			Invalidate();
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				Invalidate();
			}
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			Invalidate();
		}

		[Category("Colors")]
		public Color HoverColor
		{
			get { return _HoverColor; }
			set { _HoverColor = value; }
		}

		[Category("Colors")]
		public Color HoverFontColor
		{
			get { return _HoverFontColor; }
			set { _HoverFontColor = value; }
		}

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		[Category("Colors")]
		public Color BGColor
		{
			get { return _BGColor; }
			set { _BGColor = value; }
		}

		private int StartIndex
		{
			get { return _StartIndex; }
			set
			{
				_StartIndex = value;
				try
				{
					base.SelectedIndex = value;
				}
				catch
				{
				}
				Invalidate();
			}
		}

		public void DrawItem_(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0)
				return;
			e.DrawBackground();
			e.DrawFocusRectangle();

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				//-- Selected item
				e.Graphics.FillRectangle(new SolidBrush(_HoverColor), e.Bounds);
			}
			else
			{
				//-- Not Selected
				e.Graphics.FillRectangle(new SolidBrush(_BaseColor), e.Bounds);
			}

			//-- Text
			e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), new Font("Segoe UI", 8), new SolidBrush(_HoverFontColor), new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height));

			e.Graphics.Dispose();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 18;
		}

		private Color _BaseColor = Color.FromArgb(25, 27, 29);
		private Color _BGColor = Color.FromArgb(45, 47, 49);
		private Color _HoverColor = Color.FromArgb(35, 168, 109);
		private Color _HoverFontColor = Color.White;

		public ForeverComboBox()
		{
			DrawItem += DrawItem_;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			DrawMode = DrawMode.OwnerDrawFixed;
			ForeColor = _HoverFontColor;
			DropDownStyle = ComboBoxStyle.DropDownList;
			Cursor = Cursors.Hand;
			StartIndex = 0;
			ItemHeight = 18;
			Font = new Font("Segoe UI", 8, FontStyle.Regular);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width;
			H = Height;

			Rectangle Base = new Rectangle(0, 0, W, H);
			Rectangle Button = new Rectangle(Convert.ToInt32(W - 40), 0, W, H);
			GraphicsPath GP = new GraphicsPath();
			GraphicsPath GP2 = new GraphicsPath();

			var _with16 = G;
			_with16.Clear(Color.FromArgb(45, 45, 48));
			_with16.SmoothingMode = SmoothingMode.HighQuality;
			_with16.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with16.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			//-- Base
			_with16.FillRectangle(new SolidBrush(_BGColor), Base);

			//-- Text
			_with16.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(4, 6), ForeverLibrary.NearSF);

			//-- Button
			GP.Reset();
			GP.AddRectangle(Button);
			_with16.SetClip(GP);
			_with16.FillRectangle(new SolidBrush(_BaseColor), Button);
			_with16.ResetClip();

			//-- Lines
			_with16.DrawLine(Pens.White, W - 10, 6, W - 30, 6);
			_with16.DrawLine(Pens.White, W - 10, 12, W - 30, 12);
			_with16.DrawLine(Pens.White, W - 10, 18, W - 30, 18);

			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}
	}

	#endregion
}