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
	#region ForeverStatusBar

	public class ForeverStatusBar : Control
	{
		private int W;
		private int H;
		private bool _ShowTimeDate = false;

		protected override void CreateHandle()
		{
			base.CreateHandle();
			Dock = DockStyle.Bottom;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
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

		[Category("Colors")]
		public Color TimeColor
		{
			get { return _TimeColor; }
			set { _TimeColor = value; }
		}

		[Category("Colors")]
		public Color RectColor
		{
			get { return _RectColor; }
			set { _RectColor = value; }
		}

		[Category("Options")]
		public bool ShowTimeDate
		{
			get { return _ShowTimeDate; }
			set { _ShowTimeDate = value; }
		}

		[Category("Options")]
		public string TimeFormat
		{
			get { return _TimeFormat; }
			set { _TimeFormat = value; }
		}

		[Category("Options")]
		public string TimeFormatDefault
		{
			get { return "dd.MM.yyyy - HH:mm:ss"; }
		}

		public string GetTimeFormat(string TF)
		{
			try
			{
				return DateTime.Now.ToString(TF);
			}
			catch
			{
				TimeFormat = TimeFormatDefault;
				_TimeFormat = TimeFormatDefault;
				return DateTime.Now.ToString(TimeFormatDefault);
			}
		}

		private Color _BaseColor = Color.FromArgb(45, 47, 49);
		private Color _TextColor = Color.White;
		private Color _TimeColor = Color.White;
		private Color _RectColor = ForeverLibrary.ForeverColor;
		private string _TimeFormat = "dd.MM.yyyy - HH:mm:ss";

		public ForeverStatusBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			Font = new Font("Segoe UI", 8);
			ForeColor = Color.White;
			TimeFormat = _TimeFormat;
			Size = new Size(Width, 20);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width;
			H = Height;

			Rectangle Base = new Rectangle(0, 0, W, H);

			var _with21 = G;
			_with21.SmoothingMode = SmoothingMode.HighQuality;
			_with21.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with21.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with21.Clear(BaseColor);

			//-- Base
			_with21.FillRectangle(new SolidBrush(BaseColor), Base);

			//-- Text
			_with21.DrawString(Text, Font, new SolidBrush(_TextColor), new Rectangle(10, 4, W, H), ForeverLibrary.NearSF);

			//-- Rectangle
			_with21.FillRectangle(new SolidBrush(_RectColor), new Rectangle(4, 4, 4, 14));

			//-- TimeDate
			if (ShowTimeDate)
			{
				string Time = GetTimeFormat(_TimeFormat);
				_with21.DrawString(Time, Font, new SolidBrush(_TimeColor), new Rectangle(-4, 2, W, H), new StringFormat
				{
					Alignment = StringAlignment.Far,
					LineAlignment = StringAlignment.Center
				});
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

			_RectColor = Colors.Forever;
		}
	}

	#endregion
}