#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeButton

    public class HopeButton : Control
    {
        #region Variables
        bool enterFlag = false;
        bool clickFlag = false;
        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public HopeButtonType ButtonType { get; set; } = HopeButtonType.Primary;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color TextColor { get; set; } = Color.White;

        #endregion

        #region Events
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            enterFlag = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            enterFlag = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            clickFlag = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            clickFlag = false;
            Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            if (ButtonType == HopeButtonType.Default)
            {
                var BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                graphics.FillPath(new SolidBrush(enterFlag ? Color.FromArgb(25, HopeColors.PrimaryColor) : Color.White), BG);
                graphics.DrawPath(new Pen(clickFlag ? HopeColors.PrimaryColor : HopeColors.OneLevelBorder, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HopeColors.PrimaryColor : HopeColors.MainText), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
            else
            {
                var BG = RoundRectangle.CreateRoundRect(0, 0, Width, Height, 3);
                var backColor = HopeColors.PrimaryColor;
                switch (ButtonType)
                {
                    case HopeButtonType.Primary:
                        backColor = HopeColors.PrimaryColor;
                        break;
                    case HopeButtonType.Success:
                        backColor = HopeColors.Success;
                        break;
                    case HopeButtonType.Info:
                        backColor = HopeColors.Info;
                        break;
                    case HopeButtonType.Warning:
                        backColor = HopeColors.Warning;
                        break;
                    case HopeButtonType.Danger:
                        backColor = HopeColors.Danger;
                        break;
                    default:
                        break;
                }

                var brush = new SolidBrush(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : backColor);
                graphics.FillPath(brush, BG);
                if (!Enabled)
                    graphics.FillPath(new SolidBrush(Color.FromArgb(125, HopeColors.OneLevelBorder)), BG);
                graphics.DrawString(Text, Font, new SolidBrush(TextColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
        }

        public HopeButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            Size = new Size(120, 40);
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}