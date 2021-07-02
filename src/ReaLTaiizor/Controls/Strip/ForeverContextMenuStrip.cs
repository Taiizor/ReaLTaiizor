#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

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
            Font = new("Segoe UI", 8);
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
                get => BackColor;
                set => BackColor = value;
            }

            [Category("Colors")]
            public Color _CheckedColor
            {
                get => CheckedColor;
                set => CheckedColor = value;
            }

            [Category("Colors")]
            public Color _BorderColor
            {
                get => BorderColor;
                set => BorderColor = value;
            }

            private Color BackColor = Color.FromArgb(45, 47, 49);
            private Color CheckedColor = ForeverLibrary.ForeverColor;
            private Color BorderColor = Color.FromArgb(53, 58, 60);

            public override Color ButtonSelectedBorder => BackColor;

            public override Color CheckBackground => CheckedColor;

            public override Color CheckPressedBackground => CheckedColor;

            public override Color CheckSelectedBackground => CheckedColor;

            public override Color ImageMarginGradientBegin => CheckedColor;

            public override Color ImageMarginGradientEnd => CheckedColor;

            public override Color ImageMarginGradientMiddle => CheckedColor;

            public override Color MenuBorder => BorderColor;

            public override Color MenuItemBorder => BorderColor;

            public override Color MenuItemSelected => CheckedColor;

            public override Color SeparatorDark => BorderColor;

            public override Color ToolStripDropDownBackground => BackColor;
        }
    }

    #endregion
}