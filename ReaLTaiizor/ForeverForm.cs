#region Imports

using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
	#region ForeverForm

	public class ForeverForm : ContainerControl
	{
		private int W;
		private int H;
		private bool Cap = false;
		private bool _HeaderMaximize = false;
		private Point MousePoint = new Point(0, 0);
		private int MoveHeight = 50;

		[Category("Colors")]
		public Color HeaderColor
		{
			get { return _HeaderColor; }
			set { _HeaderColor = value; }
		}

		[Category("Colors")]
		public Color BaseColor
		{
			get { return _BaseColor; }
			set { _BaseColor = value; }
		}

		[Category("Colors")]
		public Color BorderColor
		{
			get { return _BorderColor; }
			set { _BorderColor = value; }
		}

		[Category("Colors")]
		public Color ForeverColor
		{
			// get { return ForeverLibrary.ForeverColor; }
			// set { ForeverLibrary.ForeverColor = value; }
			get { return _ForeverColor; }
			set { _ForeverColor = value; }
		}

		[Category("Options")]
		public bool HeaderMaximize
		{
			get { return _HeaderMaximize; }
			set { _HeaderMaximize = value; }
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
			{
				Cap = true;
				MousePoint = e.Location;
			}
		}

		private void ForeverForm_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (HeaderMaximize)
			{
				if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
				{
					if (FindForm().WindowState == FormWindowState.Normal)
					{
						FindForm().WindowState = FormWindowState.Maximized;
						FindForm().Refresh();
					}
					else if (FindForm().WindowState == FormWindowState.Maximized)
					{
						FindForm().WindowState = FormWindowState.Normal;
						FindForm().Refresh();
					}
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Cap = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (Cap)
			{
				// Parent.Location = MousePosition - MousePoint;
				Parent.Location = new Point(
					MousePosition.X - MousePoint.X,
					MousePosition.Y - MousePoint.Y
				);
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			ParentForm.FormBorderStyle = FormBorderStyle.None;
			ParentForm.AllowTransparency = false;
			ParentForm.TransparencyKey = Color.Fuchsia;
			ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
			Dock = DockStyle.Fill;
			Invalidate();
		}

		private Color _HeaderColor = Color.FromArgb(45, 47, 49);
		private Color _BaseColor = Color.FromArgb(60, 70, 73);
		private Color _BorderColor = Color.FromArgb(53, 58, 60);
		private Color _ForeverColor = ForeverLibrary.ForeverColor;
		private Color TextColor = Color.FromArgb(234, 234, 234);

		private Color _HeaderLight = Color.FromArgb(171, 171, 172);
		private Color _BaseLight = Color.FromArgb(196, 199, 200);
		public Color TextLight = Color.FromArgb(45, 47, 49);

		public ForeverForm()
		{
			MouseDoubleClick += ForeverForm_MouseDoubleClick;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			BackColor = Color.White;
			Font = new Font("Segoe UI", 12);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);
			W = Width;
			H = Height;

			Rectangle Base = new Rectangle(0, 0, W, H);
			Rectangle Header = new Rectangle(0, 0, W, 50);

			var _with2 = G;
			_with2.SmoothingMode = SmoothingMode.HighQuality;
			_with2.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with2.Clear(BackColor);

			//-- Base
			_with2.FillRectangle(new SolidBrush(_BaseColor), Base);

			//-- Header
			_with2.FillRectangle(new SolidBrush(_HeaderColor), Header);

			//-- Logo
			_with2.FillRectangle(new SolidBrush(Color.FromArgb(243, 243, 243)), new Rectangle(8, 16, 4, 18));
			_with2.FillRectangle(new SolidBrush(ForeverColor), 16, 16, 4, 18);
			_with2.DrawString(Text, Font, new SolidBrush(TextColor), new Rectangle(26, 15, W, H), ForeverLibrary.NearSF);

			//-- Border
			_with2.DrawRectangle(new Pen(_BorderColor), Base);

			base.OnPaint(e);
			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}
	}

	#endregion
}