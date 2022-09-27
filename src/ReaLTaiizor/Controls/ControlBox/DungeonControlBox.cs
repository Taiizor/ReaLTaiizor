#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonControlBox

    public class DungeonControlBox : Control
    {

        #region Enums

        public enum MouseState
        {
            None = 0,
            Over = 1,
            Down = 2
        }

        #endregion
        #region MouseStates
        private MouseState State = MouseState.None;
        private int X;
        private Rectangle CloseBtn = new(3, 2, 17, 17);
        private Rectangle MinBtn = new(23, 2, 17, 17);
        private Rectangle MaxBtn = new(43, 2, 17, 17);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            State = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (X is > 3 and < 20)
            {
                FindForm().Close();
            }
            else if (X is > 23 and < 40)
            {
                if (_EnableMinimize == true)
                {
                    FindForm().WindowState = FormWindowState.Minimized;
                }
                else if (_EnableMaximize == true)
                {
                    if (FindForm().WindowState == FormWindowState.Maximized)
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Maximized;
                    }
                }
            }
            else if (X is > 43 and < 60)
            {
                if (_EnableMaximize == true)
                {
                    if (FindForm().WindowState == FormWindowState.Maximized)
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Maximized;
                    }
                }
            }
            State = MouseState.Over;
            Invalidate();
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
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Invalidate();
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

        private bool _EnableMaximize = true;
        public bool EnableMaximize
        {
            get => _EnableMaximize;
            set
            {
                _EnableMaximize = value;
                if (_EnableMaximize == false || _EnableMinimize == false)
                {
                    if (_EnableMaximize == false && _EnableMinimize == false)
                    {
                        Size = new(24, 22);
                    }
                    else
                    {
                        Size = new(44, 22);
                    }
                }
                else
                {
                    Size = new(64, 22);
                }

                Invalidate();
            }
        }

        private bool _EnableMinimize = true;
        public bool EnableMinimize
        {
            get => _EnableMinimize;
            set
            {
                _EnableMinimize = value;
                if (_EnableMaximize == false || _EnableMinimize == false)
                {
                    if (_EnableMaximize == false && _EnableMinimize == false)
                    {
                        Size = new(24, 22);
                    }
                    else
                    {
                        Size = new(44, 22);
                    }
                }
                else
                {
                    Size = new(64, 22);
                }

                Invalidate();
            }
        }

        #endregion

        public DungeonControlBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Font = new("Marlett", 7);
            Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_EnableMaximize == false || _EnableMinimize == false)
            {
                if (_EnableMaximize == false && _EnableMinimize == false)
                {
                    Size = new(23, 22);
                }
                else
                {
                    Size = new(44, 22);
                }
            }
            else
            {
                Size = new(64, 22);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            // Auto-decide control location on the theme container
            if (DefaultLocation)
            {
                Location = new(5, 13);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            base.OnPaint(e);
            G.SmoothingMode = SmoothingMode.AntiAlias;

            LinearGradientBrush LGBClose = new(CloseBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90);
            G.FillEllipse(LGBClose, CloseBtn);
            G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), CloseBtn);
            G.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));

            if (_EnableMinimize == true && _EnableMaximize == true)
            {
                LinearGradientBrush LGBMinimize = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                G.FillEllipse(LGBMinimize, MinBtn);
                G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                G.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));

                LinearGradientBrush LGBMaximize = new(MaxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                G.FillEllipse(LGBMaximize, MaxBtn);
                G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MaxBtn);
                G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
            }
            else if (_EnableMinimize == true && _EnableMaximize == false)
            {
                LinearGradientBrush LGBMinimize = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                G.FillEllipse(LGBMinimize, MinBtn);
                G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                G.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));
            }
            else
            {
                LinearGradientBrush LGBMaximize = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                G.FillEllipse(LGBMaximize, MinBtn);
                G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, 7, 0, 0));
            }

            switch (State)
            {
                case MouseState.None:
                    LinearGradientBrush xLGBClose_1 = new(CloseBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90);
                    G.FillEllipse(xLGBClose_1, CloseBtn);
                    G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), CloseBtn);
                    G.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));

                    if (_EnableMinimize == true && _EnableMaximize == true)
                    {
                        LinearGradientBrush xLGBMinimize_1 = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                        G.FillEllipse(xLGBMinimize_1, MinBtn);
                        G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                        G.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));

                        LinearGradientBrush xLGBMaximize = new(MaxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                        G.FillEllipse(xLGBMaximize, MaxBtn);
                        G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MaxBtn);
                        G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
                    }
                    else if (_EnableMinimize == true && _EnableMaximize == false)
                    {
                        LinearGradientBrush xLGBMinimize_1 = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                        G.FillEllipse(xLGBMinimize_1, MinBtn);
                        G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                        G.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));
                    }
                    else
                    {
                        LinearGradientBrush xLGBMaximize = new(MinBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                        G.FillEllipse(xLGBMaximize, MinBtn);
                        G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                        G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, 7, 0, 0));
                    }
                    Cursor = Cursors.Hand;
                    break;
                case MouseState.Over:
                    if (X is > 3 and < 20)
                    {
                        LinearGradientBrush xLGBClose = new(CloseBtn, Color.FromArgb(248, 152, 124), Color.FromArgb(231, 92, 45), 90);
                        G.FillEllipse(xLGBClose, CloseBtn);
                        G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), CloseBtn);
                        G.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));
                    }
                    else if (X is > 23 and < 40)
                    {
                        if (_EnableMinimize == true)
                        {
                            LinearGradientBrush xLGBMinimize = new(MinBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90);
                            G.FillEllipse(xLGBMinimize, MinBtn);
                            G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                            G.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));
                        }
                        else if (_EnableMaximize == true)
                        {
                            LinearGradientBrush xLGBMaximize = new(MinBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90);
                            G.FillEllipse(xLGBMaximize, MinBtn);
                            G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MinBtn);
                            G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, 7, 0, 0));
                        }
                    }
                    else if (X is > 43 and < 60)
                    {
                        if (_EnableMaximize == true && _EnableMinimize == true)
                        {
                            LinearGradientBrush xLGBMaximize = new(MaxBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90);
                            G.FillEllipse(xLGBMaximize, MaxBtn);
                            G.DrawEllipse(new(Color.FromArgb(57, 56, 53)), MaxBtn);
                            G.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
                        }
                    }
                    break;
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}