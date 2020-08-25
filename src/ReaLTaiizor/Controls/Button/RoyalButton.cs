#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalButton

    public class RoyalButton : ControlRoyalBase
    {
        public event EventHandler HotTrackChanged;
        public event EventHandler SelectedChanged;

        bool hotTracked = false;
        public bool HotTracked
        {
            get { return hotTracked; }
        }

        bool pressed = false;
        public bool Pressed
        {
            get { return pressed; }
        }

        bool drawBorder;
        public bool DrawBorder
        {
            get { return drawBorder; }
            set { drawBorder = value; Invalidate(); }
        }

        Color hotTrackColor;
        public Color HotTrackColor
        {
            get { return hotTrackColor; }
            set { hotTrackColor = value; Invalidate(); }
        }

        Color pressedColor;
        public Color PressedColor
        {
            get { return pressedColor; }
            set { pressedColor = value; Invalidate(); }
        }

        Color pressedForeColor;
        public Color PressedForeColor
        {
            get { return pressedForeColor; }
            set { pressedForeColor = value; Invalidate(); }
        }

        Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        int borderThickness;
        public int BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; Invalidate(); }
        }

        Image image;
        public Image Image
        {
            get { return image; }
            set { image = value; Invalidate(); }
        }

        RoyalLayoutFlags layoutFlags;
        public RoyalLayoutFlags LayoutFlags
        {
            get { return layoutFlags; }
            set { layoutFlags = value; Invalidate(); }
        }

        public RoyalButton()
        {
            BackColor = RoyalColors.BackColor;
            HotTrackColor = RoyalColors.HotTrackColor;
            PressedColor = RoyalColors.PressedBackColor;
            PressedForeColor = RoyalColors.PressedForeColor;
            BorderColor = RoyalColors.BorderColor;
            ForeColor = RoyalColors.ForeColor;
            DrawBorder = true;
            BorderThickness = 3;

            Image = null;
            LayoutFlags = RoyalLayoutFlags.ImageBeforeText;
            Size = new Size(120, 40);
            Cursor = Cursors.Hand;

            HotTrackChanged = new EventHandler(OnHotTrackChanged);
            SelectedChanged = new EventHandler(OnSelectedChanged);
        }

        protected virtual void OnHotTrackChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        protected virtual void OnSelectedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hotTracked = true;
            HotTrackChanged(this, EventArgs.Empty);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hotTracked = false;
            HotTrackChanged(this, EventArgs.Empty);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            pressed = true;
            SelectedChanged(this, EventArgs.Empty);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            pressed = false;
            SelectedChanged(this, EventArgs.Empty);
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color foreColor = ForeColor;
            Color backColor = BackColor;

            if (hotTracked && !pressed)
                backColor = hotTrackColor;
            else if (pressed)
            {
                foreColor = pressedForeColor;
                backColor = pressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.ClipRectangle);

            if (DrawBorder)
                e.Graphics.DrawRectangle(new Pen(BorderColor, BorderThickness), new Rectangle(1, 1, Width - BorderThickness, Height - BorderThickness));

            SizeF textSize = e.Graphics.MeasureString(Text, Font);

            if (Image != null)
            {
                if (LayoutFlags == RoyalLayoutFlags.ImageBeforeText)
                {
                    e.Graphics.DrawImage(Image, new Point(0, 0));
                    e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF(Image.Width + 2, (Height - textSize.Height) / 2));
                }
                else if (LayoutFlags == RoyalLayoutFlags.TextBeforeImage)
                {
                    e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF(0, (Height - textSize.Height) / 2));
                    e.Graphics.DrawImage(Image, new Point(Width - Image.Width, 0));
                }
                else if (LayoutFlags == RoyalLayoutFlags.ImageOnly)
                    e.Graphics.DrawImage(Image, new Point((Width - Image.Width) / 2, (Height - Image.Height) / 2));
                else if (LayoutFlags == RoyalLayoutFlags.TextOnly)
                    e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2));
            }
            else
                e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2));
            base.OnPaint(e);
        }
    }

    #endregion
}