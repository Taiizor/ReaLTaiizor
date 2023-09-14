#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ControlBox

    public class ControlBox : Control
    {
        #region Enums

        public enum ButtonHoverState
        {
            Minimize,
            Maximize,
            Close,
            None
        }

        #endregion

        #region Variables

        private ButtonHoverState ButtonHState = ButtonHoverState.None;

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
        public bool EnableMaximizeButton
        {
            get => _EnableMaximize;
            set
            {
                _EnableMaximize = value;
                Invalidate();
            }
        }

        private bool _EnableMinimize = true;
        public bool EnableMinimizeButton
        {
            get => _EnableMinimize;
            set
            {
                _EnableMinimize = value;
                Invalidate();
            }
        }

        private bool _EnableHoverHighlight = false;
        public bool EnableHoverHighlight
        {
            get => _EnableHoverHighlight;
            set
            {
                _EnableHoverHighlight = value;
                Invalidate();
            }
        }

        public Color MinimizeHoverColor { get; set; } = Color.FromArgb(63, 63, 65);
        public Color MaximizeHoverColor { get; set; } = Color.FromArgb(74, 74, 74);
        public Color CloseHoverColor { get; set; } = Color.FromArgb(230, 17, 35);

        #endregion

        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(90, 25);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int X = e.Location.X;
            int Y = e.Location.Y;
            if (Y > 0 && Y < Height)
            {
                if (X is >= 0 and <= 30)
                {
                    ButtonHState = ButtonHoverState.Minimize;
                    if (_EnableMinimize == true)
                    {
                        Cursor = Cursors.Hand;
                    }
                    else
                    {
                        Cursor = Cursors.No;
                    }
                }
                else if (X is > 30 and <= 60)
                {
                    ButtonHState = ButtonHoverState.Maximize;
                    if (_EnableMaximize == true)
                    {
                        Cursor = Cursors.Hand;
                    }
                    else
                    {
                        Cursor = Cursors.No;
                    }
                }
                else if (X > 60 && X < Width)
                {
                    ButtonHState = ButtonHoverState.Close;
                    Cursor = Cursors.Hand;
                }
                else
                {
                    ButtonHState = ButtonHoverState.None;
                    Cursor = Cursors.Hand;
                }
            }
            else
            {
                ButtonHState = ButtonHoverState.None;
                Cursor = Cursors.Hand;
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            switch (ButtonHState)
            {
                case ButtonHoverState.Close:
                    Parent.FindForm().Close();
                    break;
                case ButtonHoverState.Minimize:
                    if (_EnableMinimize == true)
                    {
                        Parent.FindForm().WindowState = FormWindowState.Minimized;
                        /*foreach (Form Form in Application.OpenForms)
                            Form.WindowState = FormWindowState.Minimized;*/
                    }
                    break;
                case ButtonHoverState.Maximize:
                    if (_EnableMaximize == true)
                    {
                        if (Parent.FindForm().WindowState == FormWindowState.Normal)
                        {
                            Parent.FindForm().WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            Parent.FindForm().WindowState = FormWindowState.Normal;
                        }
                    }
                    break;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ButtonHState = ButtonHoverState.None;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        #endregion

        public ControlBox() : base()
        {
            DoubleBuffered = true;
            EnableHoverHighlight = true;
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Cursor = Cursors.Hand;
            BackColor = Color.FromArgb(32, 34, 37);
            ForeColor = Color.FromArgb(155, 155, 155);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            try
            {
                if (DefaultLocation)
                {
                    Location = new(Parent.Width - 100, 18);
                }
            }
            catch (Exception)
            {
                //
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics G = e.Graphics;
            G.Clear(base.BackColor);

            //Close
            G.DrawString("r", new Font("Marlett", 12), new SolidBrush(base.ForeColor), new Point(75, 5), new StringFormat { Alignment = StringAlignment.Center });

            //Maximize
            switch (Parent.FindForm().WindowState)
            {
                case FormWindowState.Maximized:
                    if (_EnableMaximize == true)
                    {
                        G.DrawString("2", new Font("Marlett", 12), new SolidBrush(base.ForeColor), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("2", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                    }

                    break;
                case FormWindowState.Normal:
                    if (_EnableMaximize == true)
                    {
                        G.DrawString("1", new Font("Marlett", 12), new SolidBrush(base.ForeColor), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                    }
                    else
                    {
                        G.DrawString("1", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                    }

                    break;
            }

            //Minimize
            if (_EnableMinimize == true)
            {
                G.DrawString("0", new Font("Marlett", 12), new SolidBrush(base.ForeColor), new Point(17, 0), new StringFormat { Alignment = StringAlignment.Center });
            }
            else
            {
                G.DrawString("0", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(17, 0), new StringFormat { Alignment = StringAlignment.Center });
            }

            if (_EnableHoverHighlight == true)
            {
                switch (ButtonHState)
                {
                    /*case ButtonHoverState.None:
                        G.Clear(base.BackColor);
                        break;*/
                    case ButtonHoverState.Minimize:
                        if (_EnableMinimize == true)
                        {
                            G.FillRectangle(new SolidBrush(MinimizeHoverColor), new Rectangle(0, 0, 30, Height));
                            G.DrawString("0", new Font("Marlett", 12), new SolidBrush(Color.White), new Point(17, 0), new StringFormat { Alignment = StringAlignment.Center });
                        }
                        else
                        {
                            G.DrawString("0", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(17, 0), new StringFormat { Alignment = StringAlignment.Center });
                        }

                        break;
                    case ButtonHoverState.Maximize:
                        switch (Parent.FindForm().WindowState)
                        {
                            case FormWindowState.Maximized:
                                if (_EnableMaximize == true)
                                {
                                    G.FillRectangle(new SolidBrush(MaximizeHoverColor), new Rectangle(30, 0, 30, Height));
                                    G.DrawString("2", new Font("Marlett", 12), new SolidBrush(Color.White), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                                }
                                else
                                {
                                    G.DrawString("2", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                                }

                                break;
                            case FormWindowState.Normal:
                                if (_EnableMaximize == true)
                                {
                                    G.FillRectangle(new SolidBrush(MaximizeHoverColor), new Rectangle(30, 0, 30, Height));
                                    G.DrawString("1", new Font("Marlett", 12), new SolidBrush(Color.White), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                                }
                                else
                                {
                                    G.DrawString("1", new Font("Marlett", 12), new SolidBrush(Color.FromArgb(55, 60, 50)), new Point(46, 4), new StringFormat { Alignment = StringAlignment.Center });
                                }

                                break;
                        }
                        break;
                    case ButtonHoverState.Close:
                        G.FillRectangle(new SolidBrush(CloseHoverColor), new Rectangle(60, 0, 30, Height));
                        G.DrawString("r", new Font("Marlett", 12), new SolidBrush(Color.White), new Point(75, 5), new StringFormat { Alignment = StringAlignment.Center });
                        break;
                }
            }
        }
    }

    #endregion
}