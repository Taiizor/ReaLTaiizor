#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostProgressBar

    public class LostProgressBar : ControlLostBase
    {
        private int _progress = 50;
        private Color _color = ThemeLost.AccentBrush.Color;
        private bool _hover = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Progress
        {
            get => _progress;
            set { _progress = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color
        {
            get => _color;
            set { _color = value; Invalidate(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Hover
        {
            get => _hover;
            set { _hover = value; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            if (MouseOver && _hover)
            {
                pevent.Graphics.FillRectangle(new SolidBrush(ThemeLost.ForeColor.Shade(ThemeLost.ShadowSize, 0)), ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(ThemeLost.FontPen, 1, 1, Width - 2, Height - 2);
            e.Graphics.FillRectangle(new SolidBrush(_color), 5, 5, (Width - 10) * (_progress / 100f), Height - 9);
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