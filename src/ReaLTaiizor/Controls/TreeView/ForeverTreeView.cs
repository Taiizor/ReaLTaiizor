#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
	#region ForeverTreeView

	public class ForeverTreeView : System.Windows.Forms.TreeView
	{
		private TreeNodeStates State;

		protected override void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			try
			{
				Rectangle Bounds = new Rectangle(e.Bounds.Location.X, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height);
				//e.Node.Nodes.Item.
				switch (State)
				{
					case TreeNodeStates.Default:
						e.Graphics.FillRectangle(Brushes.Red, Bounds);
						e.Graphics.DrawString(e.Node.Text, Font, Brushes.LimeGreen, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
						Invalidate();
						break;
					case TreeNodeStates.Checked:
						e.Graphics.FillRectangle(Brushes.Green, Bounds);
						e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
						Invalidate();
						break;
					case TreeNodeStates.Selected:
						e.Graphics.FillRectangle(Brushes.Green, Bounds);
						e.Graphics.DrawString(e.Node.Text, Font, Brushes.Black, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);
						Invalidate();
						break;
				}

			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

			base.OnDrawNode(e);
		}

		private Color _BaseColor = Color.FromArgb(45, 47, 49);
		private Color _LineColor = Color.FromArgb(25, 27, 29);

		public ForeverTreeView()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;

			BackColor = _BaseColor;
			ForeColor = Color.White;
			LineColor = _LineColor;
			DrawMode = TreeViewDrawMode.OwnerDrawAll;

			Font = new Font("Segoe UI", 8);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Bitmap B = new Bitmap(Width, Height);
			Graphics G = Graphics.FromImage(B);

			Rectangle Base = new Rectangle(0, 0, Width, Height);

			var _with22 = G;
			_with22.SmoothingMode = SmoothingMode.HighQuality;
			_with22.PixelOffsetMode = PixelOffsetMode.HighQuality;
			_with22.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			_with22.Clear(BackColor);

			_with22.FillRectangle(new SolidBrush(_BaseColor), Base);
			_with22.DrawString(Text, Font, Brushes.Black, new Rectangle(Bounds.X + 2, Bounds.Y + 2, Bounds.Width, Bounds.Height), ForeverLibrary.NearSF);


			base.OnPaint(e);
			G.Dispose();
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			e.Graphics.DrawImageUnscaled(B, 0, 0);
			B.Dispose();
		}
	}

	#endregion
}