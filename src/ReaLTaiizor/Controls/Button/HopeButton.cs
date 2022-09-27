#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeButton

    public class HopeButton : Control
    {
        #region Variables
        private bool enterFlag = false;
        private bool clickFlag = false;
        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public HopeButtonType ButtonType { get; set; } = HopeButtonType.Primary;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color TextColor { get; set; } = Color.White;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color DefaultColor { get; set; } = HopeColors.DefaultColor;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color PrimaryColor { get; set; } = HopeColors.PrimaryColor;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessColor { get; set; } = HopeColors.Success;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoColor { get; set; } = HopeColors.Info;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningColor { get; set; } = HopeColors.Warning;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color DangerColor { get; set; } = HopeColors.Danger;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color BorderColor { get; set; } = HopeColors.OneLevelBorder;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color HoverTextColor { get; set; } = HopeColors.MainText;

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
                GraphicsPath BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                graphics.FillPath(new SolidBrush(enterFlag ? Color.FromArgb(25, DefaultColor) : DefaultColor), BG);
                graphics.DrawPath(new(clickFlag ? DefaultColor : BorderColor, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HoverTextColor : TextColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
            else
            {
                GraphicsPath BG = RoundRectangle.CreateRoundRect(0, 0, Width, Height, 3);
                Color backColor = DefaultColor;
                switch (ButtonType)
                {
                    case HopeButtonType.Primary:
                        backColor = PrimaryColor;
                        break;
                    case HopeButtonType.Success:
                        backColor = SuccessColor;
                        break;
                    case HopeButtonType.Info:
                        backColor = InfoColor;
                        break;
                    case HopeButtonType.Warning:
                        backColor = WarningColor;
                        break;
                    case HopeButtonType.Danger:
                        backColor = DangerColor;
                        break;
                    default:
                        break;
                }

                SolidBrush brush = new(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : backColor);
                graphics.FillPath(brush, BG);
                if (!Enabled)
                {
                    graphics.FillPath(new SolidBrush(Color.FromArgb(125, BorderColor)), BG);
                }

                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HoverTextColor : TextColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
        }

        public HopeButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            Size = new(120, 40);
            Cursor = Cursors.Hand;
        }
    }

    #endregion
}