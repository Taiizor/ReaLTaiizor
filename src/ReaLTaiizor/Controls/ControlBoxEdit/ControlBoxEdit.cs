#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ControlBoxEdit

    public class ControlBoxEdit : Control
    {

        #region Enums

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion
        #region Properties

        private bool _DefaultLocation = true;
        public bool DefaultLocation
        {
            get => _DefaultLocation;
            set
            {
                _DefaultLocation = value;
                Invalidate();
            }
        }

        #endregion
        #region Variables

        private MouseState State = MouseState.None;
        private int i;
        private Rectangle CloseRect = new(28, 0, 47, 18);
        private Rectangle MinimizeRect = new(0, 0, 28, 18);

        #endregion
        #region EventArgs

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (i > 0 & i < 28)
            {
                FindForm().WindowState = FormWindowState.Minimized;
            }
            else if (i > 30 & i < 75)
            {
                FindForm().Close();
            }

            State = MouseState.Down;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            i = e.Location.X;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = 77;
            Height = 19;
        }

        #endregion

        public ControlBoxEdit()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DefaultLocation)
            {
                Location = new(checked(FindForm().Width - 81), -1);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            GraphicsPath GP_MinimizeRect = new();
            GraphicsPath GP_CloseRect = new();

            GP_MinimizeRect.AddRectangle(MinimizeRect);
            GP_CloseRect.AddRectangle(CloseRect);
            G.Clear(BackColor);

            switch (State)
            {
                case MouseState.None:
                NonePoint:
                    LinearGradientBrush MinimizeGradient = new(MinimizeRect, Color.FromArgb(73, 73, 73), Color.FromArgb(58, 58, 58), 90);
                    G.FillPath(MinimizeGradient, GP_MinimizeRect);
                    G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_MinimizeRect);
                    G.DrawString("0", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), MinimizeRect.Width - 22, MinimizeRect.Height - 16);

                    LinearGradientBrush CloseGradient = new(CloseRect, Color.FromArgb(73, 73, 73), Color.FromArgb(58, 58, 58), 90);
                    G.FillPath(CloseGradient, GP_CloseRect);
                    G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_CloseRect);
                    G.DrawString("r", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), CloseRect.Width - 4, CloseRect.Height - 16);
                    break;
                case MouseState.Over:
                    if (i > 0 & i < 28)
                    {
                        LinearGradientBrush xMinimizeGradient = new(MinimizeRect, Color.FromArgb(76, 76, 76, 76), Color.FromArgb(48, 48, 48), 90f);
                        G.FillPath(xMinimizeGradient, GP_MinimizeRect);
                        G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_MinimizeRect);
                        G.DrawString("0", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), MinimizeRect.Width - 22, MinimizeRect.Height - 16);

                        LinearGradientBrush xCloseGradient = new(CloseRect, Color.FromArgb(73, 73, 73), Color.FromArgb(58, 58, 58), 90);
                        G.FillPath(xCloseGradient, GP_CloseRect);
                        G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_CloseRect);
                        G.DrawString("r", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), CloseRect.Width - 4, CloseRect.Height - 16);
                    }
                    else if (i > 30 & i < 75)
                    {
                        LinearGradientBrush xCloseGradient = new(CloseRect, Color.FromArgb(76, 76, 76, 76), Color.FromArgb(48, 48, 48), 90);
                        G.FillPath(xCloseGradient, GP_CloseRect);
                        G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_CloseRect);
                        G.DrawString("r", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), CloseRect.Width - 4, CloseRect.Height - 16);

                        LinearGradientBrush xMinimizeGradient = new(MinimizeRect, Color.FromArgb(73, 73, 73), Color.FromArgb(58, 58, 58), 90);
                        G.FillPath(xMinimizeGradient, RoundRectangle.RoundRect(MinimizeRect, 1));
                        G.DrawPath(new(Color.FromArgb(40, 40, 40)), GP_MinimizeRect);
                        G.DrawString("0", new Font("Marlett", 11, FontStyle.Regular), new SolidBrush(Color.FromArgb(221, 221, 221)), MinimizeRect.Width - 22, MinimizeRect.Height - 16);
                    }
                    else
                    {
                        goto NonePoint; // Return to [MouseState = None]
                    }

                    break;
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            GP_CloseRect.Dispose();
            GP_MinimizeRect.Dispose();
            B.Dispose();
        }
    }

    #endregion
}