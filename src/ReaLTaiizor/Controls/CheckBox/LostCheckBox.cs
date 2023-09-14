#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostCheckBox

    public class LostCheckBox : ControlLostBase
    {
        public event EventHandler CheckedChanged;
        private bool _checked = false;
        private Color _checkedcolor = ThemeLost.FontBrush.Color;
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                Invalidate();
                CheckedChanged?.Invoke(this, null);
            }
        }
        public Color CheckedColor
        {
            get => _checkedcolor;
            set
            {
                _checkedcolor = value;
                Invalidate();
            }
        }

        public LostCheckBox() : base()
        {
            Font = ThemeLost.BodyFont;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver ? new SolidBrush(ThemeLost.ForeColor.Shade(ThemeLost.ShadowSize, 0)) : new SolidBrush(BackColor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (MouseOver)
            {
                e.Graphics.FillRectangle(new SolidBrush(ThemeLost.ForeColor.Shade(ThemeLost.ShadowSize, 0)), ClientRectangle);
            }

            e.Graphics.DrawRectangle(ThemeLost.FontPen, 1, 1, Height - 2, Height - 2);

            if (Checked)
            {
                e.Graphics.FillRectangle(new SolidBrush(_checkedcolor), 4, 4, Height - 7, Height - 7);
            }

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), Height + 3, (Height / 2) - (textSize.Height / 2));
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Checked = !Checked;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            HasShadow = true;
            Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HasShadow = false;
            Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            Invalidate();
        }
    }

    #endregion
}