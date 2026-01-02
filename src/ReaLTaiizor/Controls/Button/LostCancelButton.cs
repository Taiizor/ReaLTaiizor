#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostCancelButton

    public class LostCancelButton : ControlLostBase
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
        } = Color.IndianRed;

        public Color BackColor
        {
            get;
            set { field = value; Invalidate(); }
        } = Color.Crimson;

        public Color ForeColor
        {
            get;
            set { field = value; Invalidate(); }
        } = Color.White;

        public LostCancelButton() : base()
        {
            Cursor = Cursors.Hand;
            Size = new(120, 40);
            Font = ThemeLost.BodyFont;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver ? new SolidBrush(HoverColor) : new SolidBrush(BackColor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(MouseOver ? new SolidBrush(HoverColor) : new SolidBrush(BackColor), ClientRectangle);

            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, (Width / 2) - (BackgroundImage.Width / 2), (Height / 2) - (BackgroundImage.Height / 2));
            }

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), (Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));
        }

        /*
        public override void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new(Color.IndianRed.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }*/

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