#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalButton

    public class RoyalButton : ControlRoyalBase
    {
        public event EventHandler HotTrackChanged;
        public event EventHandler SelectedChanged;

        public bool HotTracked { get; private set; } = false;

        public bool Pressed { get; private set; } = false;

        private bool drawBorder;
        public bool DrawBorder
        {
            get => drawBorder;
            set { drawBorder = value; Invalidate(); }
        }

        private Color hotTrackColor;
        public Color HotTrackColor
        {
            get => hotTrackColor;
            set { hotTrackColor = value; Invalidate(); }
        }

        private Color pressedColor;
        public Color PressedColor
        {
            get => pressedColor;
            set { pressedColor = value; Invalidate(); }
        }

        private Color pressedForeColor;
        public Color PressedForeColor
        {
            get => pressedForeColor;
            set { pressedForeColor = value; Invalidate(); }
        }

        private Color borderColor;
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        private int borderThickness;
        public int BorderThickness
        {
            get => borderThickness;
            set { borderThickness = value; Invalidate(); }
        }

        private Image image;
        public Image Image
        {
            get => image;
            set { image = value; Invalidate(); }
        }

        private RoyalLayoutFlags layoutFlags;
        public RoyalLayoutFlags LayoutFlags
        {
            get => layoutFlags;
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
            Size = new(120, 40);
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
            HotTracked = true;
            HotTrackChanged(this, EventArgs.Empty);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HotTracked = false;
            HotTrackChanged(this, EventArgs.Empty);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Pressed = true;
            SelectedChanged(this, EventArgs.Empty);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Pressed = false;
            SelectedChanged(this, EventArgs.Empty);
            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color foreColor = ForeColor;
            Color backColor = BackColor;

            if (HotTracked && !Pressed)
            {
                backColor = hotTrackColor;
            }
            else if (Pressed)
            {
                foreColor = pressedForeColor;
                backColor = pressedColor;
            }

            e.Graphics.FillRectangle(new SolidBrush(backColor), e.ClipRectangle);

            if (DrawBorder)
            {
                e.Graphics.DrawRectangle(new(BorderColor, BorderThickness), new Rectangle(1, 1, Width - BorderThickness, Height - BorderThickness));
            }

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
                {
                    e.Graphics.DrawImage(Image, new Point((Width - Image.Width) / 2, (Height - Image.Height) / 2));
                }
                else if (LayoutFlags == RoyalLayoutFlags.TextOnly)
                {
                    e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2));
                }
            }
            else
            {
                e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), new PointF((Width - textSize.Width) / 2, (Height - textSize.Height) / 2));
            }

            base.OnPaint(e);
        }
    }

    #endregion
}