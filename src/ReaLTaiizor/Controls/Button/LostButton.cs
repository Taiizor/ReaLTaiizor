#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostButton

    public class LostButton : ControlLostBase
    {
        public Image Image
        {
            get;
            set { field = value; Invalidate(); }
        } = null;

        public Color HoverColor
        {
            get;
            set { field = value; Invalidate(); }
        } = ThemeLost.AccentBrush.Color;

        public LostButton() : base()
        {
            Cursor = Cursors.Hand;
            Size = new(120, 40);
            Font = ThemeLost.BodyFont;
            ForeColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver ? new SolidBrush(HoverColor) : new SolidBrush(BackColor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image != null)
            {
                e.Graphics.DrawImage(Image, (Width / 2) - (Image.Width / 2), (Height / 2) - (Image.Height / 2), Image.Width, Image.Height);
            }

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), (Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = false;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = true;
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