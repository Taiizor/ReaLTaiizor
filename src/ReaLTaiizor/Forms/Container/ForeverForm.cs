#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region ForeverForm

    public class ForeverForm : ContainerControl
    {
        private int W;
        private int H;
        private bool Cap = false;
        private Point MousePoint = new(0, 0);
        private readonly int MoveHeight = 50;

        private Image _Image; //Properties.Resources.Taiizor
        private Size _ImageSize;
        private const int wmNcHitTest = 0x84;
        private const int htLeft = 10;
        private const int htRight = 11;
        private const int htTop = 12;
        private const int htTopLeft = 13;
        private const int htTopRight = 14;
        private const int htBottom = 15;
        private const int htBottomLeft = 16;
        private const int htBottomRight = 17;

        [Category("Colors")]
        public Color HeaderColor { get; set; } = Color.FromArgb(45, 47, 49);

        [Category("Colors")]
        public Color BaseColor { get; set; } = Color.FromArgb(60, 70, 73);

        [Category("Colors")]
        public Color BorderColor { get; set; } = Color.DodgerBlue;

        [Category("Colors")]
        public Color TextColor { get; set; } = Color.FromArgb(234, 234, 234);

        [Category("Colors")]
        public Color TextLight
        {
            get => _TextLight;
            set => _TextLight = value;
        }

        [Category("Colors")]
        public Color ForeverColor
        {
            // get { return ForeverLibrary.ForeverColor; }
            // set { ForeverLibrary.ForeverColor = value; }
            get; set;
        } = ForeverLibrary.ForeverColor;

        [Category("Options")]
        public bool HeaderMaximize { get; set; } = false;

        [Category("Options")]
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
            }
        }

        [Category("Options")]
        public bool Sizable { get; set; } = true;

        [Category("Options")]
        public Font HeaderTextFont { get; set; } = new("Segoe UI", 12);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (Sizable && m.Msg == wmNcHitTest && FindForm().WindowState != FormWindowState.Maximized)
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
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
            {
                Cap = true;
                MousePoint = e.Location;
            }
        }

        private void ForeverForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HeaderMaximize)
            {
                if (e.Button == MouseButtons.Left & new Rectangle(0, 0, Width, MoveHeight).Contains(e.Location))
                {
                    if (FindForm().WindowState == FormWindowState.Normal)
                    {
                        FindForm().WindowState = FormWindowState.Maximized;
                        FindForm().Refresh();
                    }
                    else if (FindForm().WindowState == FormWindowState.Maximized)
                    {
                        FindForm().WindowState = FormWindowState.Normal;
                        FindForm().Refresh();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Cap)
            {
                // Parent.Location = MousePosition - MousePoint;
                Parent.Location = new(MousePosition.X - MousePoint.X, MousePosition.Y - MousePoint.Y);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.AllowTransparency = false;
            ParentForm.TransparencyKey = Color.Fuchsia;
            ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            Dock = DockStyle.Fill;
            Invalidate();
        }

        private readonly Color _HeaderLight = Color.FromArgb(171, 171, 172);
        private readonly Color _BaseLight = Color.FromArgb(196, 199, 200);
        public Color _TextLight = Color.SeaGreen;

        public ForeverForm()
        {
            MouseDoubleClick += ForeverForm_MouseDoubleClick;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.White;
            Font = new("Segoe UI", 12);
            MinimumSize = new(210, 50);
            Padding = new Padding(1, 51, 1, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            W = Width;
            H = Height;

            Rectangle Base = new(0, 0, W, H);
            Rectangle Header = new(0, 0, W, 50);

            Graphics _with2 = G;
            _with2.SmoothingMode = SmoothingMode.HighQuality;
            _with2.PixelOffsetMode = PixelOffsetMode.HighQuality;
            _with2.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _with2.Clear(BackColor);

            //-- Base
            _with2.FillRectangle(new SolidBrush(BaseColor), Base);

            //-- Header
            _with2.FillRectangle(new SolidBrush(HeaderColor), Header);

            //-- Logo
            if (_Image == null)
            {
                _with2.FillRectangle(new SolidBrush(_TextLight), new Rectangle(8, 16, 4, 18));
                _with2.FillRectangle(new SolidBrush(ForeverColor), 16, 16, 4, 18);
                _with2.DrawString(Text, HeaderTextFont, new SolidBrush(TextColor), new Rectangle(26, 15, W, H), ForeverLibrary.NearSF);
            }
            else
            {
                _with2.DrawImage(_Image, 12, 12, 27, 27);
                _with2.DrawString(Text, HeaderTextFont, new SolidBrush(TextColor), new Rectangle(48, 15, W, H), ForeverLibrary.NearSF);
            }

            //-- Border
            _with2.DrawRectangle(new(BorderColor), Base);

            base.OnPaint(e);
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(B, 0, 0);
            B.Dispose();
        }
    }

    #endregion
}