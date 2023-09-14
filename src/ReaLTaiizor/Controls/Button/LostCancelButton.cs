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
        private Image _image = null;
        public Image Image
        {
            get => _image;
            set { _image = value; Invalidate(); }
        }

        private Color _hovercolor = Color.IndianRed;
        public Color HoverColor
        {
            get => _hovercolor;
            set { _hovercolor = value; Invalidate(); }
        }

        private Color _backcolor = Color.Crimson;
        public Color BackColor
        {
            get => _backcolor;
            set { _backcolor = value; Invalidate(); }
        }

        private Color _forecolor = Color.White;
        public Color ForeColor
        {
            get => _forecolor;
            set { _forecolor = value; Invalidate(); }
        }

        public LostCancelButton() : base()
        {
            Cursor = Cursors.Hand;
            Size = new(120, 40);
            Font = ThemeLost.BodyFont;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver ? new SolidBrush(_hovercolor) : new SolidBrush(_backcolor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(MouseOver ? new SolidBrush(_hovercolor) : new SolidBrush(_backcolor), ClientRectangle);

            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, (Width / 2) - (BackgroundImage.Width / 2), (Height / 2) - (BackgroundImage.Height / 2));
            }

            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            e.Graphics.DrawString(Text, Font, new SolidBrush(_forecolor), (Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));
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