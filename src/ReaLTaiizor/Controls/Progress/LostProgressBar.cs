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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Progress
        {
            get;
            set { field = value; Invalidate(); }
        } = 50;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color Color
        {
            get;
            set { field = value; Invalidate(); }
        } = ThemeLost.AccentBrush.Color;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Hover
        {
            get;
            set { field = value; Invalidate(); }
        } = false;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);

            if (MouseOver && Hover)
            {
                pevent.Graphics.FillRectangle(new SolidBrush(ThemeLost.ForeColor.Shade(ThemeLost.ShadowSize, 0)), ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(ThemeLost.FontPen, 1, 1, Width - 2, Height - 2);
            e.Graphics.FillRectangle(new SolidBrush(Color), 5, 5, (Width - 10) * (Progress / 100f), Height - 9);
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