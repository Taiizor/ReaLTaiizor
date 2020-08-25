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
	#region ForeverRadioButton

	[DefaultEvent("CheckedChanged")]
	public class ForeverRadioButton : Control
	{
		private MouseStateForever State = MouseStateForever.None;
		private int W;
		private int H;
		private _Options O;

		private bool _Checked;
		public bool Checked
		{
			get { return _Checked; }
			set
			{
				_Checked = value;
				InvalidateControls();
				CheckedChanged?.Invoke(this);
				Invalidate();
			}
		}

		public event CheckedChangedEventHandler CheckedChanged;
		public delegate void CheckedChangedEventHandler(object sender);

		protected override void OnClick(EventArgs e)
		{
			if (!_Checked)
				Checked = true;
			base.OnClick(e);
		}

		private void InvalidateControls()
		{
			if (!IsHandleCreated || !_Checked)
				return;
			foreach (Control C in Parent.Controls)
			{
				if (!object.ReferenceEquals(C, this) && C is ForeverRadioButton)
				{
					((ForeverRadioButton)C).Checked = false;
					Invalidate();
				}
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			InvalidateControls();
		}

		[Flags()]
		public enum _Options
		{
			Style1,
			Style2
		}

		[Category("Options")]
		public _Options Options
		{
			get { return O; }
			set { O = value; }
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 22;
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

		private Color _BaseColor = Color.FromArgb(45, 47, 49);
		private Color _BorderColor = ForeverLibrary.ForeverColor;

		[Category("Options")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		[Category("Options")]
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		public ForeverRadioButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			Cursor = Cursors.Hand;
			Size = new Size(145, 22);
			BackColor = Color.FromArgb(60, 70, 73);
			ForeColor = Color.FromArgb(243, 243, 243);
			Font = new Font("Segoe UI", 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width - 1;
			H = Height - 1;

			Rectangle Base = new Rectangle(0, 2, Height - 5, Height - 5);
			Rectangle Dot = new Rectangle(4, 6, H - 12, H - 12);

			var _with10 = G;
			_with10.SmoothingMode = SmoothingMode.HighQuality;
			_with10.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with10.Clear(BackColor);

			switch (O)
			{
				case _Options.Style1:
					//-- Base
					_with10.FillEllipse(new SolidBrush(_BaseColor), Base);

					switch (State)
					{
						case MouseStateForever.Over:
							_with10.DrawEllipse(new Pen(_BorderColor), Base);
							break;
						case MouseStateForever.Down:
							_with10.DrawEllipse(new Pen(_BorderColor), Base);
							break;
					}

					//-- If Checked 
					if (Checked)
					{
						_with10.FillEllipse(new SolidBrush(_BorderColor), Dot);
					}
					break;
				case _Options.Style2:
					//-- Base
					_with10.FillEllipse(new SolidBrush(_BaseColor), Base);

					switch (State)
					{
						case MouseStateForever.Over:
							//-- Base
							_with10.DrawEllipse(new Pen(_BorderColor), Base);
							_with10.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
						case MouseStateForever.Down:
							//-- Base
							_with10.DrawEllipse(new Pen(_BorderColor), Base);
							_with10.FillEllipse(new SolidBrush(Color.FromArgb(118, 213, 170)), Base);
							break;
					}

					//-- If Checked
					if (Checked)
					{
						//-- Base
						_with10.FillEllipse(new SolidBrush(_BorderColor), Dot);
					}
					break;
			}

			_with10.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(20, 2, W, H), ForeverLibrary.NearSF);

			base.OnPaint(e);
			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}

		private void UpdateColors()
		{
			ForeverColors Colors = ForeverLibrary.GetColors(this);

			_BorderColor = Colors.Forever;
		}
	}

	#endregion
}