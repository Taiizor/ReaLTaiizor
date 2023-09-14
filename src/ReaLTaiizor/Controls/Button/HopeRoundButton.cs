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
    #region HopeRoundButton

    public class HopeRoundButton : Control
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

        public Color PrimaryColor { get; set; } = HopeColors.PrimaryColor;

        public Color SuccessColor { get; set; } = HopeColors.Success;

        public Color InfoColor { get; set; } = HopeColors.Info;

        public Color WarningColor { get; set; } = HopeColors.Warning;

        public Color DangerColor { get; set; } = HopeColors.Danger;

        public Color BorderColor { get; set; } = HopeColors.OneLevelBorder;

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

            GraphicsPath backPath = new();
            backPath.AddArc(new RectangleF(0.5f, 0.5f, Height - 1, Height - 1), 90, 180);
            backPath.AddArc(new RectangleF(Width - Height + 0.5f, 0.5f, Height - 1, Height - 1), 270, 180);
            backPath.CloseAllFigures();

            if (_buttonType == HopeButtonType.Default)
            {
                graphics.DrawPath(new(clickFlag ? DefaultColor : BorderColor, 1), backPath);
                graphics.FillPath(new SolidBrush(enterFlag ? Color.FromArgb(25, DefaultColor) : DefaultColor), backPath);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HoverTextColor : _textColor), new RectangleF(Height / 2, 0, Width - Height, Height), HopeStringAlign.Center);
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
                SolidBrush brush = new(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : backColor);
                graphics.FillPath(brush, backPath);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HoverTextColor : _textColor), new RectangleF(Height / 2, 0, Width - Height, Height), HopeStringAlign.Center);
            }
        }


        public HopeRoundButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            Cursor = Cursors.Hand;
            Size = new(190, 40);
        }
    }

    #endregion
}