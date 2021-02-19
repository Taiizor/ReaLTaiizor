﻿#region Imports

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
        private Color _DefaultColor = HopeColors.DefaultColor;
        private Color _DefaultClickColor = HopeColors.DarkPrimary;
        private Color _PrimaryColor = HopeColors.PrimaryColor;
        private Color _SuccessColor = HopeColors.Success;
        private Color _InfoColor = HopeColors.Info;
        private Color _WarningColor = HopeColors.Warning;
        private Color _DangerColor = HopeColors.Danger;
        private Color _BorderColor = HopeColors.OneLevelBorder;
        private Color _HoverTextColor = HopeColors.MainText;
        private Color _HoverClickTextColor = HopeColors.DarkPrimary;
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

        public Color DefaultColor
        {
            get => _DefaultColor;
            set => _DefaultColor = value;
        }

        public Color DefaultClickColor
        {
            get => _DefaultClickColor;
            set => _DefaultClickColor = value;
        }

        public Color PrimaryColor
        {
            get => _PrimaryColor;
            set => _PrimaryColor = value;
        }

        public Color SuccessColor
        {
            get => _SuccessColor;
            set => _SuccessColor = value;
        }

        public Color InfoColor
        {
            get => _InfoColor;
            set => _InfoColor = value;
        }

        public Color WarningColor
        {
            get => _WarningColor;
            set => _WarningColor = value;
        }

        public Color DangerColor
        {
            get => _DangerColor;
            set => _DangerColor = value;
        }

        public Color BorderColor
        {
            get => _BorderColor;
            set => _BorderColor = value;
        }

        public Color HoverTextColor
        {
            get => _HoverTextColor;
            set => _HoverTextColor = value;
        }

        public Color HoverClickTextColor
        {
            get => _HoverClickTextColor;
            set => _HoverClickTextColor = value;
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
                GraphicsPath BG = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
                graphics.DrawPath(new(enterFlag ? (clickFlag ? _DefaultClickColor : _DefaultColor) : _BorderColor, 1), BG);
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? (clickFlag ? _HoverClickTextColor : _HoverTextColor) : _textColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
            }
            else
            {
                Color backColor = _DefaultColor;
                switch (_buttonType)
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
                graphics.DrawString(Text, Font, new SolidBrush(enterFlag ? _HoverTextColor : _textColor), new RectangleF(0, 0, Width, Height), HopeStringAlign.Center);
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