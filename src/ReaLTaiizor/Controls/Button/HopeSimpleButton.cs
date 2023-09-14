#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeSimpleButton

    public class HopeSimpleButton : Control
    {
        #region Variables
        private bool enterFlag = false;
        private bool clickFlag = false;
        #endregion

        #region Settings
        private HopeButtonType _buttonType = HopeButtonType.Primary;
        public HopeButtonType ButtonType
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                Invalidate();
            }
        }

        private Color _textColor = Color.White;
        public Color TextColor
        {
            get => _textColor;
            set
            {
                _textColor = value;
                Invalidate();
            }
        }

        public Color DefaultColor { get; set; } = HopeColors.DefaultColor;

        public Color DefaultClickColor { get; set; } = HopeColors.DarkPrimary;

        public Color PrimaryColor { get; set; } = HopeColors.PrimaryColor;

        public Color SuccessColor { get; set; } = HopeColors.Success;

        public Color InfoColor { get; set; } = HopeColors.Info;

        public Color WarningColor { get; set; } = HopeColors.Warning;

        public Color DangerColor { get; set; } = HopeColors.Danger;

        public Color BorderColor { get; set; } = HopeColors.OneLevelBorder;

        public Color HoverTextColor { get; set; } = HopeColors.MainText;

        public Color HoverClickTextColor { get; set; } = HopeColors.DarkPrimary;
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


            if (_buttonType == HopeButtonType.Default)
            {
                GraphicsPath BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                graphics.DrawPath(new(enterFlag ? (clickFlag ? DefaultClickColor : DefaultColor) : BorderColor, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? (clickFlag ? HoverClickTextColor : HoverTextColor) : _textColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
            else
            {
                Color backColor = DefaultColor;
                switch (_buttonType)
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

                GraphicsPath BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                if (!enterFlag)
                {
                    BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                    graphics.DrawPath(new(backColor, 0.5f), BG);
                }
                else
                {
                    BG = RoundRectangle.CreateRoundRect(0, 0, Width, Height, 3);
                }

                SolidBrush brush = new(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : Color.FromArgb(25, backColor));
                graphics.FillPath(brush, BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HoverTextColor : _textColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
        }

        public HopeSimpleButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            Cursor = Cursors.Hand;
            Size = new(150, 40);
        }
    }

    #endregion
}