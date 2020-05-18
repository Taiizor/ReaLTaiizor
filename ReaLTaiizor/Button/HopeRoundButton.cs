#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeRoundButton

    public class HopeRoundButton : Control
    {
        #region Variables
        bool enterFlag = false;
        bool clickFlag = false;
        #endregion

        #region Events

        private HopeButtonType _buttonType = HopeButtonType.Primary;
        public HopeButtonType ButtonType
        {
            get { return _buttonType; }
            set
            {
                _buttonType = value;
                Invalidate();
            }
        }

        private Color _textColor = Color.White;
        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                Invalidate();
            }
        }
        #endregion

        #region 事件
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

            var backPath = new GraphicsPath();
            backPath.AddArc(new RectangleF(0.5f, 0.5f, Height - 1, Height - 1), 90, 180);
            backPath.AddArc(new RectangleF(Width - Height + 0.5f, 0.5f, Height - 1, Height - 1), 270, 180);
            backPath.CloseAllFigures();

            if (_buttonType == HopeButtonType.Default)
            {
                graphics.DrawPath(new Pen(clickFlag ? HopeColors.PrimaryColor : HopeColors.OneLevelBorder, 1), backPath);
                graphics.FillPath(new SolidBrush(enterFlag ? Color.FromArgb(25, HopeColors.PrimaryColor) : Color.White), backPath);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? HopeColors.PrimaryColor : HopeColors.MainText), new RectangleF(Height / 2, 0, Width - Height, Height), HopeStringAlign.Center);
            }
            else
            {
                var backColor = HopeColors.PrimaryColor;
                switch (_buttonType)
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
                graphics.FillPath(brush, backPath);
                graphics.DrawString(Text, Font, new SolidBrush(_textColor), new RectangleF(Height / 2, 0, Width - Height, Height), HopeStringAlign.Center);
            }
        }


        public HopeRoundButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            Cursor = Cursors.Hand;
            Size = new Size(190, 40);
        }
    }

    #endregion
}