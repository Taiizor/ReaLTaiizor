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
	#region ForeverTextBox

	[DefaultEvent("TextChanged")]
	public class ForeverTextBox : Control
	{
		private int W;
		private int H;
		private MouseStateForever State = MouseStateForever.None;
		private System.Windows.Forms.TextBox TB;

		private HorizontalAlignment _TextAlign = HorizontalAlignment.Left;

		[Category("Options")]
		public HorizontalAlignment TextAlign
		{
			get { return _TextAlign; }
			set
			{
				_TextAlign = value;
				if (TB != null)
					TB.TextAlign = value;
			}
		}

		private int _MaxLength = 32767;
		[Category("Options")]
		public int MaxLength
		{
			get { return _MaxLength; }
			set
			{
				_MaxLength = value;
				if (TB != null)
					TB.MaxLength = value;
			}
		}

		private bool _ReadOnly;
		[Category("Options")]
		public bool ReadOnly
		{
			get { return _ReadOnly; }
			set
			{
				_ReadOnly = value;
				if (TB != null)
					TB.ReadOnly = value;
			}
		}

		private bool _UseSystemPasswordChar;
		[Category("Options")]
		public bool UseSystemPasswordChar
		{
			get { return _UseSystemPasswordChar; }
			set
			{
				_UseSystemPasswordChar = value;
				if (TB != null)
					TB.UseSystemPasswordChar = value;
			}
		}

		private bool _Multiline;
		[Category("Options")]
		public bool Multiline
		{
			get { return _Multiline; }
			set
			{
				_Multiline = value;
				if (TB != null)
				{
					TB.Multiline = value;

					if (value)
						TB.Height = Height - 11;
					else
						Height = TB.Height + 11;
				}
			}
		}

		private bool _FocusOnHover = false;
		[Category("Options")]
		public bool FocusOnHover
		{
			get { return _FocusOnHover; }
			set { _FocusOnHover = value; }
		}

		[Category("Options")]
		public override string Text
		{
			get { return base.Text; }
			set
			{
				base.Text = value;
				if (TB != null)
					TB.Text = value;
			}
		}

		[Category("Options")]
		public override Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				if (TB != null)
				{
					TB.Font = value;
					TB.Location = new Point(3, 5);
					TB.Width = Width - 6;

					if (!_Multiline)
						Height = TB.Height + 11;
				}
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (!Controls.Contains(TB))
				Controls.Add(TB);
		}

		private void OnBaseTextChanged(object s, EventArgs e)
		{
			Text = TB.Text;
		}

		private void OnBaseKeyDown(object s, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				TB.SelectAll();
				e.SuppressKeyPress = true;
			}
			if (e.Control && e.KeyCode == Keys.C)
			{
				TB.Copy();
				e.SuppressKeyPress = true;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			TB.Location = new Point(5, 5);
			TB.Width = Width - 10;

			if (_Multiline)
				TB.Height = Height - 11;
			else
				Height = TB.Height + 11;

			base.OnResize(e);
		}

		private Color _BorderColor = ForeverLibrary.ForeverColor;
		[Category("Colors")]
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		private Color _BaseColor = Color.FromArgb(45, 47, 49);
		[Category("Colors")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
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
			TB.Focus();
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			State = MouseStateForever.Over;
			if (FocusOnHover)
				TB.Focus();
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			State = MouseStateForever.None;
			Invalidate();
		}

		public ForeverTextBox()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;

			ForeColor = Color.FromArgb(192, 192, 192);

			TB = new System.Windows.Forms.TextBox();
			TB.Font = new Font("Segoe UI", 10);
			TB.Text = Text;
			TB.BackColor = _BaseColor;
			TB.ForeColor = ForeColor;
			TB.MaxLength = _MaxLength;
			TB.Multiline = _Multiline;
			TB.ReadOnly = _ReadOnly;
			TB.UseSystemPasswordChar = _UseSystemPasswordChar;
			TB.BorderStyle = BorderStyle.None;
			TB.Location = new Point(6, 6);
			TB.Width = Width - 10;

			TB.Cursor = Cursors.IBeam;

			if (_Multiline)
				TB.Height = Height - 11;
			else
				Height = TB.Height + 11;
			Width = 103;

			TB.TextChanged += OnBaseTextChanged;
			TB.KeyDown += OnBaseKeyDown;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			BackColor = Color.Transparent;

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width - 2;
			H = Height - 2;

			Rectangle Base = new Rectangle(1, 1, W, H);

			var _with12 = G;
			_with12.SmoothingMode = SmoothingMode.HighQuality;
			_with12.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with12.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with12.Clear(_BorderColor);

			//-- Colors
			TB.BackColor = _BaseColor;
			TB.ForeColor = ForeColor;

			//-- Base
			_with12.FillRectangle(new SolidBrush(_BaseColor), Base);

			base.OnPaint(e);
			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}

        #region TB Events

        public new event KeyEventHandler KeyUp
		{
			add
			{
				TB.KeyUp += value;
			}
			remove
			{
				TB.KeyUp -= value;
			}
		}

		public new event KeyEventHandler KeyDown
		{
			add
			{
				TB.KeyDown += value;
			}
			remove
			{
				TB.KeyDown -= value;
			}
		}

		public new event KeyPressEventHandler KeyPress
		{
			add
			{
                TB.KeyPress += value;
			}
			remove
			{
				TB.KeyPress -= value;
			}
		}

        #endregion

        private void UpdateColors()
		{
			ForeverColors Colors = ForeverLibrary.GetColors(this);

			_BorderColor = Colors.Forever;
		}
	}

	#endregion
}