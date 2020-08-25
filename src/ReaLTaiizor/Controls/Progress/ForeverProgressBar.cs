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
	#region ForeverProgressBar

	public class ForeverProgressBar : Control
	{
		private int W;
		private int H;
		private int _Value = 0;
		private int _Maximum = 100;
		private bool _Pattern = true;
		private bool _ShowBalloon = true;
		private bool _PercentSign = false;

		[Category("Control")]
		public int Maximum
		{
			get { return _Maximum; }
			set
			{
				if (value < _Value)
					_Value = value;
				_Maximum = value;
				Invalidate();
			}
		}

		[Category("Control")]
		public int Value
		{
			get
			{
				return _Value;
				/*
				switch (_Value)
				{
					case 0:
						return 0;
						Invalidate();
						break;
					default:
						return _Value;
						Invalidate();
						break;
				}
				*/
			}
			set
			{
				if (value > _Maximum)
				{
					value = _Maximum;
					Invalidate();
				}

				_Value = value;
				Invalidate();
			}
		}

		public bool Pattern
		{
			get { return _Pattern; }
			set { _Pattern = value; }
		}

		public bool ShowBalloon
		{
			get { return _ShowBalloon; }
			set { _ShowBalloon = value; }
		}

		public bool PercentSign
		{
			get { return _PercentSign; }
			set { _PercentSign = value; }
		}

		[Category("Colors")]
		public Color ProgressColor
		{
			get { return _ProgressColor; }
			set { _ProgressColor = value; }
		}

		[Category("Colors")]
		public Color DarkerProgress
		{
			get { return _DarkerProgress; }
			set { _DarkerProgress = value; }
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Height = 42;
		}

		protected override void CreateHandle()
		{
			base.CreateHandle();
			Height = 42;
		}

		public void Increment(int Amount)
		{
			Value += Amount;
		}

		private Color _BaseColor = Color.FromArgb(45, 47, 49);
		private Color _ProgressColor = ForeverLibrary.ForeverColor;
		private Color _DarkerProgress = Color.FromArgb(23, 148, 92);

		public ForeverProgressBar()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.FromArgb(60, 70, 73);
			ForeColor = _ProgressColor;
			Height = 42;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//UpdateColors();

			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width - 1;
			H = Height - 1;

			Rectangle Base = new Rectangle(0, 24, W, H);
			GraphicsPath GP = new GraphicsPath();
			GraphicsPath GP2 = new GraphicsPath();
			GraphicsPath GP3 = new GraphicsPath();

			var _with15 = G;
			_with15.SmoothingMode = SmoothingMode.HighQuality;
			_with15.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with15.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with15.Clear(BackColor);

			//-- Progress Value
			//int iValue = Convert.ToInt32(((float)_Value) / ((float)(_Maximum * Width)));
			float percent = ((float)_Value) / ((float)_Maximum);
			int iValue = (int)(percent * ((float)Width));

			switch (Value)
			{
				case 0:
					//-- Base
					_with15.FillRectangle(new SolidBrush(_BaseColor), Base);
					//--Progress
					_with15.FillRectangle(new SolidBrush(_ProgressColor), new Rectangle(0, 24, iValue - 1, H - 1));
					break;
				case 100:
					//-- Base
					_with15.FillRectangle(new SolidBrush(_BaseColor), Base);
					//--Progress
					_with15.FillRectangle(new SolidBrush(_ProgressColor), new Rectangle(0, 24, iValue - 1, H - 1));
					break;
				default:
					//-- Base
					_with15.FillRectangle(new SolidBrush(_BaseColor), Base);

					//--Progress
					GP.AddRectangle(new Rectangle(0, 24, iValue - 1, H - 1));
					_with15.FillPath(new SolidBrush(_ProgressColor), GP);

					if (_Pattern)
					{
						//-- Hatch Brush
						HatchBrush HB = new HatchBrush(HatchStyle.Plaid, _DarkerProgress, _ProgressColor);
						_with15.FillRectangle(HB, new Rectangle(0, 24, iValue - 1, H - 1));
					}

					if (_ShowBalloon)
					{
						//-- Balloon
						Rectangle Balloon = new Rectangle(iValue - 18, 0, 34, 16);
						GP2 = ForeverLibrary.RoundRec(Balloon, 4);
						_with15.FillPath(new SolidBrush(_BaseColor), GP2);

						//-- Arrow
						GP3 = ForeverLibrary.DrawArrow(iValue - 9, 16, true);
						_with15.FillPath(new SolidBrush(_BaseColor), GP3);

						//-- Value > You can add "%" > value & "%"
						string text = (_PercentSign ? Value.ToString() + "%" : Value.ToString());
						int wOffset = (_PercentSign ? iValue - 15 : iValue - 11);
						_with15.DrawString(text, new Font("Segoe UI", 10), new SolidBrush(ForeColor), new Rectangle(wOffset, -2, W, H), ForeverLibrary.NearSF);
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

			_ProgressColor = Colors.Forever;
		}
	}

	#endregion
}