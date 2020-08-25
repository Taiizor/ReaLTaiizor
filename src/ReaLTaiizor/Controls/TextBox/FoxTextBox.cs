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
	#region FoxTextBox

	[DefaultEvent("TextChanged")]
	public class FoxTextBox : Control
	{
		private System.Windows.Forms.TextBox withEventsField_TB = new System.Windows.Forms.TextBox();

		private System.Windows.Forms.TextBox TB
		{
			get { return withEventsField_TB; }
			set
			{
				if (withEventsField_TB != null)
					withEventsField_TB.TextChanged -= TextChangeTb;
				withEventsField_TB = value;
				if (withEventsField_TB != null)
					withEventsField_TB.TextChanged += TextChangeTb;
			}
		}

		private Graphics G;
		private FoxLibrary.MouseState State;
		private bool _allowpassword = false;
		private int _maxChars = 32767;
		private HorizontalAlignment _textAlignment;
		private bool _multiLine = false;

		private bool _readOnly = false;

		private bool IsEnabled;
		public new bool Enabled
		{
			get { return EnabledCalc; }
			set
			{
				IsEnabled = value;

				if (Enabled)
					Cursor = Cursors.Hand;
				else
					Cursor = Cursors.Default;

				Invalidate();
			}
		}

		[DisplayName("Enabled")]
		public bool EnabledCalc
		{
			get { return IsEnabled; }
			set
			{
				Enabled = value;
				Invalidate();
			}
		}

		public new bool UseSystemPasswordChar
		{
			get { return _allowpassword; }
			set
			{
				TB.UseSystemPasswordChar = UseSystemPasswordChar;
				_allowpassword = value;
				Invalidate();
			}
		}

		public new int MaxLength
		{
			get { return _maxChars; }
			set
			{
				_maxChars = value;
				TB.MaxLength = MaxLength;
				Invalidate();
			}
		}

		public new HorizontalAlignment TextAlign
		{
			get { return _textAlignment; }
			set
			{
				_textAlignment = value;
				Invalidate();
			}
		}

		public new bool MultiLine
		{
			get { return _multiLine; }
			set
			{
				_multiLine = value;
				TB.Multiline = value;
				OnResize(EventArgs.Empty);
				Invalidate();
			}
		}

		public new bool ReadOnly
		{
			get { return _readOnly; }
			set
			{
				_readOnly = value;
				if (TB != null)
					TB.ReadOnly = value;
			}
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			Invalidate();
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			TB.ForeColor = ForeColor;
			Invalidate();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			TB.Font = Font;
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			TB.Focus();
		}

		private void TextChangeTb(object sender, EventArgs e)
		{
			Text = TB.Text;
		}

		public void NewTextBox()
		{
			var _with1 = TB;
			_with1.Text = string.Empty;
			_with1.BackColor = BackColor;
			_with1.ForeColor = ForeColor;
			_with1.TextAlign = HorizontalAlignment.Left;
			_with1.BorderStyle = BorderStyle.None;
			_with1.Location = new Point(3, 3);
			_with1.Font = Font;
			_with1.Size = new Size(Width - 3, Height - 3);
			_with1.UseSystemPasswordChar = UseSystemPasswordChar;
		}

		public FoxTextBox() : base()
		{
			TextChanged += FoxTextbox_TextChanged;
			NewTextBox();
			Controls.Add(TB);
			SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
			DoubleBuffered = true;
			TextAlign = HorizontalAlignment.Left;
			BackColor = Color.White;
			ForeColor = Color.FromArgb(66, 78, 90);
			Font = new Font("Segoe UI", 10);
			Size = new Size(90, 29);
			Enabled = true;
		}

		void FoxTextbox_TextChanged(object sender, EventArgs e)
		{
			TB.Text = Text;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			G = e.Graphics;
			G.SmoothingMode = SmoothingMode.HighQuality;
			G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			G.Clear(Parent.BackColor);


			if (Enabled)
			{
				if (State == FoxLibrary.MouseState.Down)
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#2C9CDA")))
						G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
				}
				else
				{
					using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#C8C8C8")))
						G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
				}
			}
			else
			{
				using (Pen Border = new Pen(FoxLibrary.ColorFromHex("#E6E6E6")))
					G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
			}

			TB.TextAlign = TextAlign;
			TB.UseSystemPasswordChar = UseSystemPasswordChar;

			base.OnPaint(e);

		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (!MultiLine)
			{
				TB.Location = new Point(10, (Height / 2) - (TB.Height / 2) - 0);
				TB.Size = new Size(Width - 20, TB.Height);
			}
			else
			{
				TB.Location = new Point(10, 10);
				TB.Size = new Size(Width - 20, Height - 20);
			}
		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			State = FoxLibrary.MouseState.Down;
			Invalidate();
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			State = FoxLibrary.MouseState.None;
			Invalidate();
		}
	}

	#endregion
}