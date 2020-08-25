#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeButton

    public class HopeButton : Control
    {
        #region Variables
        bool enterFlag = false;
        bool clickFlag = false;

        Color _DefaultColor = HopeColors.DefaultColor;
        Color _PrimaryColor = HopeColors.PrimaryColor;
        Color _SuccessColor = HopeColors.Success;
        Color _InfoColor = HopeColors.Info;
        Color _WarningColor = HopeColors.Warning;
        Color _DangerColor = HopeColors.Danger;
        Color _BorderColor = HopeColors.OneLevelBorder;
        Color _HoverTextColor = HopeColors.MainText;
        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public HopeButtonType ButtonType { get; set; } = HopeButtonType.Primary;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color TextColor { get; set; } = Color.White;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color DefaultColor
        {
            get { return _DefaultColor; }
            set { _DefaultColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color PrimaryColor
        {
            get { return _PrimaryColor; }
            set { _PrimaryColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color SuccessColor
        {
            get { return _SuccessColor; }
            set { _SuccessColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color InfoColor
        {
            get { return _InfoColor; }
            set { _InfoColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color WarningColor
        {
            get { return _WarningColor; }
            set { _WarningColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color DangerColor
        {
            get { return _DangerColor; }
            set { _DangerColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color HoverTextColor
        {
            get { return _HoverTextColor; }
            set { _HoverTextColor = value; }
        }

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
                graphics.FillPath(new SolidBrush(enterFlag ? Color.FromArgb(25, _DefaultColor) : _DefaultColor), BG);
                graphics.DrawPath(new Pen(clickFlag ? _DefaultColor : _BorderColor, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? _HoverTextColor : TextColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
            else
            {
                var BG = RoundRectangle.CreateRoundRect(0, 0, Width, Height, 3);
                var backColor = _DefaultColor;
                switch (ButtonType)
                {
                    case HopeButtonType.Primary:
                        backColor = _PrimaryColor;
                        break;
                    case HopeButtonType.Success:
                        backColor = _SuccessColor;
                        break;
                    case HopeButtonType.Info:
                        backColor = _InfoColor;
                        break;
                    case HopeButtonType.Warning:
                        backColor = _WarningColor;
                        break;
                    case HopeButtonType.Danger:
                        backColor = _DangerColor;
                        break;
                    default:
                        break;
                }

                var brush = new SolidBrush(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : backColor);
                graphics.FillPath(brush, BG);
                if (!Enabled)
                    graphics.FillPath(new SolidBrush(Color.FromArgb(125, _BorderColor)), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? _HoverTextColor : TextColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
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