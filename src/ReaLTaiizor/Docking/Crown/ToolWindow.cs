#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using ReaLTaiizor.Colors;
using System.Windows.Forms;
using System.ComponentModel;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region ToolWindowDocking

    [ToolboxItem(false)]
    public class ToolWindow : DockContent
    {
        #region Field Region

        private Rectangle _closeButtonRect;
        private bool _closeButtonHot = false;
        private bool _closeButtonPressed = false;

        private Rectangle _headerRect;
        private bool _shouldDrag;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding => base.Padding;

        #endregion

        #region Constructor Region

        public ToolWindow()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );

            BackColor = CrownColors.GreyBackground;
            base.Padding = new Padding(0, Consts.ToolWindowHeaderSize, 0, 0);

            UpdateCloseButton();
        }

        #endregion

        #region Method Region

        private bool IsActive()
        {
            if (DockPanel == null)
            {
                return false;
            }

            return DockPanel.ActiveContent == this;
        }

        private void UpdateCloseButton()
        {
            _headerRect = new Rectangle
            {
                X = ClientRectangle.Left,
                Y = ClientRectangle.Top,
                Width = ClientRectangle.Width,
                Height = Consts.ToolWindowHeaderSize
            };

            _closeButtonRect = new Rectangle
            {
                X = ClientRectangle.Right - Properties.Resources.tw_close.Width - 5 - 3,
                Y = ClientRectangle.Top + (Consts.ToolWindowHeaderSize / 2) - (Properties.Resources.tw_close.Height / 2),
                Width = Properties.Resources.tw_close.Width,
                Height = Properties.Resources.tw_close.Height
            };
        }

        #endregion

        #region Event Handler Region

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateCloseButton();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_closeButtonRect.Contains(e.Location) || _closeButtonPressed)
            {
                if (!_closeButtonHot)
                {
                    _closeButtonHot = true;
                    Invalidate();
                }
            }
            else
            {
                if (_closeButtonHot)
                {
                    _closeButtonHot = false;
                    Invalidate();
                }

                if (_shouldDrag)
                {
                    DockPanel.DragContent(this);
                    return;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_closeButtonRect.Contains(e.Location))
            {
                _closeButtonPressed = true;
                _closeButtonHot = true;
                Invalidate();
                return;
            }

            if (_headerRect.Contains(e.Location))
            {
                _shouldDrag = true;
                return;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_closeButtonRect.Contains(e.Location) && _closeButtonPressed)
            {
                Close();
            }

            _closeButtonPressed = false;
            _closeButtonHot = false;

            _shouldDrag = false;

            Invalidate();
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Fill body
            using (SolidBrush b = new SolidBrush(CrownColors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            bool isActive = IsActive();

            // Draw header
            Color bgColor = isActive ? CrownColors.BlueBackground : CrownColors.HeaderBackground;
            Color darkColor = isActive ? CrownColors.DarkBlueBorder : CrownColors.DarkBorder;
            Color lightColor = isActive ? CrownColors.LightBlueBorder : CrownColors.LightBorder;

            using (SolidBrush b = new SolidBrush(bgColor))
            {
                Rectangle bgRect = new Rectangle(0, 0, ClientRectangle.Width, Consts.ToolWindowHeaderSize);
                g.FillRectangle(b, bgRect);
            }

            using (Pen p = new Pen(darkColor))
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
                g.DrawLine(p, ClientRectangle.Left, Consts.ToolWindowHeaderSize - 1, ClientRectangle.Right, Consts.ToolWindowHeaderSize - 1);
            }

            using (Pen p = new Pen(lightColor))
            {
                g.DrawLine(p, ClientRectangle.Left, 1, ClientRectangle.Right, 1);
            }

            int xOffset = 2;

            // Draw icon
            if (Icon != null)
            {
                g.DrawImageUnscaled(Icon, ClientRectangle.Left + 5, ClientRectangle.Top + (Consts.ToolWindowHeaderSize / 2) - (Icon.Height / 2) + 1);
                xOffset = Icon.Width + 8;
            }

            // Draw text
            using (SolidBrush b = new SolidBrush(CrownColors.LightText))
            {
                Rectangle textRect = new Rectangle(xOffset, 0, ClientRectangle.Width - 4 - xOffset, Consts.ToolWindowHeaderSize);

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoWrap,
                    Trimming = StringTrimming.EllipsisCharacter
                };

                g.DrawString(DockText, Font, b, textRect, format);
            }

            // Close button
            Bitmap img = _closeButtonHot ? Properties.Resources.tw_close_selected : Properties.Resources.tw_close;

            if (isActive)
            {
                img = _closeButtonHot ? Properties.Resources.tw_active_close_selected : Properties.Resources.tw_active_close;
            }

            g.DrawImageUnscaled(img, _closeButtonRect.Left, _closeButtonRect.Top);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Absorb event
        }

        #endregion
    }

    #endregion
}