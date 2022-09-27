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
            public Color _BackColor { get; set; } = Color.FromArgb(45, 47, 49);

            [Category("Colors")]
            public Color _CheckedColor { get; set; } = ForeverLibrary.ForeverColor;

            [Category("Colors")]
            public Color _BorderColor { get; set; } = Color.FromArgb(53, 58, 60);

            public override Color ButtonSelectedBorder => _BackColor;

            public override Color CheckBackground => _CheckedColor;

            public override Color CheckPressedBackground => _CheckedColor;

            public override Color CheckSelectedBackground => _CheckedColor;

            public override Color ImageMarginGradientBegin => _CheckedColor;

            public override Color ImageMarginGradientEnd => _CheckedColor;

            public override Color ImageMarginGradientMiddle => _CheckedColor;

            public override Color MenuBorder => _BorderColor;

            public override Color MenuItemBorder => _BorderColor;

            public override Color MenuItemSelected => _CheckedColor;

            public override Color SeparatorDark => _BorderColor;

            public override Color ToolStripDropDownBackground => _BackColor;
        }
    }

    #endregion
}