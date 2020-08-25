#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;

#endregion

namespace ReaLTaiizor.Controls
{
	#region ForeverContextMenuStrip

	public class ForeverContextMenuStrip : ContextMenuStrip
	{
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		public ForeverContextMenuStrip() : base()
		{
			Renderer = new ToolStripProfessionalRenderer(new TColorTable());
			ShowImageMargin = false;
			ForeColor = Color.White;
			Font = new Font("Segoe UI", 8);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
		}

		public class TColorTable : ProfessionalColorTable
		{
			[Category("Colors")]
			public Color _BackColor
			{
				get { return BackColor; }
				set { BackColor = value; }
			}

			[Category("Colors")]
			public Color _CheckedColor
			{
				get { return CheckedColor; }
				set { CheckedColor = value; }
			}

			[Category("Colors")]
			public Color _BorderColor
			{
				get { return BorderColor; }
				set { BorderColor = value; }
			}

			private Color BackColor = Color.FromArgb(45, 47, 49);
			private Color CheckedColor = ForeverLibrary.ForeverColor;
			private Color BorderColor = Color.FromArgb(53, 58, 60);

			public override Color ButtonSelectedBorder
			{
				get { return BackColor; }
			}

			public override Color CheckBackground
			{
				get { return CheckedColor; }
			}

			public override Color CheckPressedBackground
			{
				get { return CheckedColor; }
			}

			public override Color CheckSelectedBackground
			{
				get { return CheckedColor; }
			}

			public override Color ImageMarginGradientBegin
			{
				get { return CheckedColor; }
			}

			public override Color ImageMarginGradientEnd
			{
				get { return CheckedColor; }
			}

			public override Color ImageMarginGradientMiddle
			{
				get { return CheckedColor; }
			}

			public override Color MenuBorder
			{
				get { return BorderColor; }
			}

			public override Color MenuItemBorder
			{
				get { return BorderColor; }
			}

			public override Color MenuItemSelected
			{
				get { return CheckedColor; }
			}

			public override Color SeparatorDark
			{
				get { return BorderColor; }
			}

			public override Color ToolStripDropDownBackground
			{
				get { return BackColor; }
			}
		}
	}

	#endregion
}