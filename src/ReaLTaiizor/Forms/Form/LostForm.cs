#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region LostForm

    public class LostForm : FormLostBase
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int CS_DROPSHADOW = 0x20000;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int wmNcHitTest = 0x84;
        private const int htLeft = 10;
        private const int htRight = 11;
        private const int htTop = 12;
        private const int htTopLeft = 13;
        private const int htTopRight = 14;
        private const int htBottom = 15;
        private const int htBottomLeft = 16;
        private const int htBottomRight = 17;

        private Image _Image = Properties.Resources.Taiizor;
        private Size _ImageSize;
        public Image Image
        {
            get => _Image;
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                _Image = value;
                Invalidate();
            }
        }

        private bool _sizable = true;
        public bool Sizable
        {
            get => _sizable;
            set { _sizable = value; Invalidate(); }
        }

        private Color _bordercolor = ThemeLost.AccentColor;
        public Color BorderColor
        {
            get => _bordercolor;
            set { _bordercolor = value; Invalidate(); }
        }

        private ButtonBorderStyle _borderstyle = ButtonBorderStyle.Solid;
        public ButtonBorderStyle BorderStyle
        {
            get => _borderstyle;
            set { _borderstyle = value; Invalidate(); }
        }

        private Color _HeaderColor = ThemeLost.ForeBrush.Color;
        public Color HeaderColor
        {
            get => _HeaderColor;
            set { _HeaderColor = value; Invalidate(); }
        }

        private Color _TitleColor = ThemeLost.FontBrush.Color;
        public Color TitleColor
        {
            get => _TitleColor;
            set { _TitleColor = value; Invalidate(); }
        }

        public LostForm()
        {
            //FormBorderStyle = FormBorderStyle.Sizable;
            Padding = new Padding(2, 36, 2, 2);
            ResizeRedraw = true;
            MinimumSize = new(160, 160);
            Font = ThemeLost.TitleFont;
        }

        public override Rectangle ShadeRect(int index)
        {
            return new Rectangle(1 - index, 1 - index, Width - 2 + (index * 2), 30 + (index * 2));
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (_sizable && m.Msg == wmNcHitTest && WindowState != FormWindowState.Maximized)
            {
                int gripDist = 10;
                //int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                //int x = Cursor.Position.X;
                // int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                //Console.WriteLine(x);
                Point pt = PointToClient(Cursor.Position);
                //Console.WriteLine(pt);
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= 2 && clientSize.Height >= 2)
                {
                    m.Result = (IntPtr)htTop;
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htBottom;
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htLeft;
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htRight;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                if (ControlBox)
                {
                    if (!new Rectangle(Width - 31, 2, 29, 29).Contains(e.Location))
                    {
                        if ((MaximizeBox || MinimizeBox) && !new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location))
                        {
                            if (MaximizeBox && MinimizeBox && new Rectangle(Width - 89, 2, 29, 29).Contains(e.Location))
                            {
                                //return;
                            }
                            else
                            {
                                if (e.X <= Width && e.Y <= 30)
                                {
                                    ReleaseCapture();
                                    _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (e.X <= Width && e.Y <= 30)
                    {
                        ReleaseCapture();
                        _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                if (ControlBox)
                {
                    if (new Rectangle(Width - 31, 2, 29, 29).Contains(e.Location))
                    {
                        Close();
                    }

                    if (MinimizeBox)
                    {
                        if (MaximizeBox)
                        {
                            if (new Rectangle(Width - 89, 2, 29, 29).Contains(e.Location))
                            {
                                WindowState = FormWindowState.Minimized;
                            }
                        }
                        else
                        {
                            if (new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location))
                            {
                                WindowState = FormWindowState.Minimized;
                            }
                        }
                    }

                    if (MaximizeBox)
                    {
                        if (new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location))
                        {
                            Screen currentScreen = Screen.FromPoint(Location);
                            Rectangle workingArea = Screen.FromPoint(Location).WorkingArea;

                            if (WindowState == FormWindowState.Maximized)
                            {
                                WindowState = FormWindowState.Normal;
                                //Bounds = _lastBounds;
                            }
                            else
                            {
                                //_lastBounds = Bounds;
                                if (currentScreen == Screen.PrimaryScreen)
                                {
                                    MaximizedBounds = workingArea;
                                }
                                else
                                {
                                    MaximizedBounds = new(0, 0, workingArea.Width, workingArea.Height);
                                }

                                WindowState = FormWindowState.Maximized;
                                Console.WriteLine(workingArea);
                                Console.WriteLine(Bounds);
                                //Bounds = workingArea;
                            }

                            OnResize(null);
                        }
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ControlBox)
            {
                bool invalidate = false;

                bool temp = _wnd_exitOver;
                _wnd_exitOver = new Rectangle(Width - 31, 2, 29, 29).Contains(e.Location);
                if (temp != _wnd_exitOver)
                {
                    invalidate = true;
                }

                if (MaximizeBox)
                {
                    temp = _wnd_maximOver;
                    _wnd_maximOver = new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location);
                    if (temp != _wnd_maximOver)
                    {
                        invalidate = true;
                    }
                }

                if (MinimizeBox)
                {
                    if (MaximizeBox)
                    {
                        temp = _wnd_minimOver;
                        _wnd_minimOver = new Rectangle(Width - 89, 2, 29, 29).Contains(e.Location);
                        if (temp != _wnd_minimOver)
                        {
                            invalidate = true;
                        }
                    }
                    else
                    {
                        temp = _wnd_minimOver;
                        _wnd_minimOver = new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location);
                        if (temp != _wnd_minimOver)
                        {
                            invalidate = true;
                        }
                    }
                }

                if (invalidate)
                {
                    Invalidate(false);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (ControlBox)
            {
                bool invalidate = _wnd_exitOver || _wnd_maximOver || _wnd_minimOver;
                _wnd_exitOver = _wnd_maximOver = _wnd_minimOver = false;
                if (invalidate)
                {
                    Invalidate(false);
                }
            }
        }

        private bool _wnd_exitOver = false;
        private bool _wnd_maximOver = false;
        private bool _wnd_minimOver = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(_HeaderColor), 1, 2, Width - 3, 30);
            if (_Image == null)
            {
                e.Graphics.DrawString(Text, Font, new SolidBrush(_TitleColor), 4, 5);
            }
            else
            {
                e.Graphics.DrawImage(_Image, new Rectangle(4, 3, 27, 27));
                e.Graphics.DrawString(Text, Font, new SolidBrush(_TitleColor), 33, 5);
            }
            DrawShadow(e.Graphics);
            base.OnPaint(e);

            if (ControlBox)
            {
                //Exit button
                if (_wnd_exitOver)
                {
                    e.Graphics.FillRectangle(Brushes.IndianRed, Width - 31, 2, 29, 29);
                }

                e.Graphics.DrawLine(ThemeLost.FontPen, Width - 31 + 9, 2 + 9, Width - 31 + 19, 2 + 19);
                e.Graphics.DrawLine(ThemeLost.FontPen, Width - 31 + 19, 2 + 9, Width - 31 + 9, 2 + 19);

                //Maximize button
                if (MaximizeBox)
                {
                    if (_wnd_maximOver)
                    {
                        e.Graphics.FillRectangle(ThemeLost.BackBrush, Width - 60, 2, 29, 29);
                    }

                    e.Graphics.DrawRectangle(ThemeLost.FontPen, Width - 60 + 9, 2 + 9, 10, 10);
                }

                //Minimize button
                if (MinimizeBox)
                {
                    if (MaximizeBox)
                    {
                        if (_wnd_minimOver)
                        {
                            e.Graphics.FillRectangle(ThemeLost.BackBrush, Width - 89, 2, 29, 29);
                        }

                        e.Graphics.DrawLine(ThemeLost.FontPen, Width - 89 + 9, 2 + 19, Width - 89 + 19, 2 + 19);
                    }
                    else
                    {
                        if (_wnd_minimOver)
                        {
                            e.Graphics.FillRectangle(ThemeLost.BackBrush, Width - 60, 2, 29, 29);
                        }

                        e.Graphics.DrawLine(ThemeLost.FontPen, Width - 60 + 9, 2 + 19, Width - 60 + 19, 2 + 19);
                    }
                }
            }

            if (WindowState != FormWindowState.Maximized)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, _bordercolor, _borderstyle);
            }
        }
    }

    #endregion
}