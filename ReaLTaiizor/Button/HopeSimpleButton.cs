#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region HopeSimpleButton

    public class HopeSimpleButton : Control
    {
        #region Variables
        bool enterFlag = false;
        bool clickFlag = false;
        #endregion

        #region Settings
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
                var BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                graphics.DrawPath(new Pen(enterFlag ? (clickFlag ? HopeColors.DarkPrimary : HopeColors.PrimaryColor) : HopeColors.OneLevelBorder, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? (clickFlag ? HopeColors.DarkPrimary : HopeColors.PrimaryColor) : HopeColors.MainText), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
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
                    case HopeButtonType.Waring:
                        backColor = HopeColors.Warning;
                        break;
                    case HopeButtonType.Danger:
                        backColor = HopeColors.Danger;
                        break;
                    default:
                        break;
                }

                var BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                if (!enterFlag)
                {
                    BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                    graphics.DrawPath(new Pen(backColor, 0.5f), BG);
                }
                else
                    BG = RoundRectangle.CreateRoundRect(0, 0, Width, Height, 3);

                var brush = new SolidBrush(enterFlag ? (clickFlag ? backColor : Color.FromArgb(225, backColor)) : Color.FromArgb(25, backColor));
                graphics.FillPath(brush, BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? _textColor : backColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
        }

        public HopeSimpleButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            Cursor = Cursors.Hand;
            Size = new Size(150, 40);
        }
    }

    #endregion
}