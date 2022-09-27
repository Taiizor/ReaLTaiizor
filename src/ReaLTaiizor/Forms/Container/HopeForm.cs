#region Imports

using ReaLTaiizor.Colors;
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
    #region HopeForm

    public class HopeForm : ContainerControl
    {
        #region Variables
        private bool mouseFlag = false;
        private Point mousePoint;
        private Rectangle minRectangle;
        private Rectangle maxRectangle;
        private Rectangle closeRectangle;

        private Color _themeColor = HopeColors.LightPrimary;
        private Color _ControlBoxColorN = Color.White;
        private Color _ControlBoxColorH = HopeColors.TwoLevelBorder;
        private Color _ControlBoxColorHC = HopeColors.Danger;
        private Image _Image = Properties.Resources.Taiizor;
        private Size _ImageSize;
        #endregion

        #region Settings

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

        public Color ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                Invalidate();
            }
        }

        public Color ControlBoxColorN
        {
            get => _ControlBoxColorN;
            set
            {
                _ControlBoxColorN = value;
                Invalidate();
            }
        }

        public Color ControlBoxColorH
        {
            get => _ControlBoxColorH;
            set
            {
                _ControlBoxColorH = value;
                Invalidate();
            }
        }

        public Color ControlBoxColorHC
        {
            get => _ControlBoxColorHC;
            set
            {
                _ControlBoxColorHC = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        public bool MinimizeBox
        {
            get
            {
                try
                {
                    return ParentForm.MinimizeBox;
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    ParentForm.MinimizeBox = value;
                    Invalidate();
                }
                catch
                {
                }
            }
        }

        [DefaultValue(true)]
        public bool MaximizeBox
        {
            get
            {
                try
                {
                    return ParentForm.MaximizeBox;
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    ParentForm.MaximizeBox = value;
                    Invalidate();
                }
                catch
                {
                }
            }
        }

        [DefaultValue(true)]
        public bool ControlBox
        {
            get
            {
                try
                {
                    return ParentForm.ControlBox;
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    ParentForm.ControlBox = value;
                    Invalidate();
                }
                catch
                {
                }
            }
        }
        #endregion

        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                mouseFlag = true;
                mousePoint = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseFlag)
            {
                if (!minRectangle.Contains(mousePoint) && !maxRectangle.Contains(mousePoint) && !closeRectangle.Contains(mousePoint))
                {
                    if (Dock == DockStyle.Top)
                    {
                        Parent.Location = new(MousePosition.X - mousePoint.X, MousePosition.Y - mousePoint.Y);
                    }
                    else
                    {
                        Parent.Location = new(MousePosition.X - mousePoint.X, MousePosition.Y - Parent.Height - mousePoint.Y + Height);
                    }
                }
            }
            else
            {
                mousePoint = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseFlag = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (minRectangle.Contains(mousePoint))
            {
                ParentForm.WindowState = FormWindowState.Minimized;
            }

            if (maxRectangle.Contains(mousePoint))
            {
                if (ParentForm.WindowState == FormWindowState.Maximized)
                {
                    ParentForm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    ParentForm.WindowState = FormWindowState.Maximized;
                }
            }
            if (closeRectangle.Contains(mousePoint))
            {
                ParentForm.Close();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 40;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.AllowTransparency = false;
            ParentForm.FindForm().StartPosition = FormStartPosition.CenterScreen;
            ParentForm.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Dock is DockStyle.Left or DockStyle.Right or DockStyle.None)
            {
                Dock = DockStyle.Top;
            }

            if (Dock == DockStyle.Top && Location.X != 0 && Location.Y != 0)
            {
                Location = new(0, 0);
            }
            else if (Dock == DockStyle.Bottom && Location.X != 0 && Location.Y != ParentForm.Height - Height)
            {
                Location = new(0, ParentForm.Height - Height);
            }

            Width = ParentForm.Width;
            ParentForm.MinimumSize = new(190, 40);

            Bitmap bitmap = new(Width, Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(_themeColor);

            Font icoFont = new("Marlett", 12);

            if (_Image != null)
            {
                graphics.DrawImage(_Image, new Rectangle(12, 7, 26, 26));

                graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(45, 1, Width - 100, Height), HopeStringAlign.Left);
            }
            else
            {
                graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(15, 1, Width - 100, Height), HopeStringAlign.Left);
            }

            if (ControlBox)
            {
                if (MinimizeBox)
                {
                    minRectangle = new(Width - 54 - ((MaximizeBox ? 1 : 0) * 22), (Height - 16) / 2, 18, 18);

                    if (minRectangle.Contains(mousePoint))
                    {
                        graphics.DrawString("0", icoFont, new SolidBrush(_ControlBoxColorH), minRectangle, HopeStringAlign.Center);
                        Cursor = Cursors.Hand;
                    }
                    else
                    {
                        graphics.DrawString("0", icoFont, new SolidBrush(_ControlBoxColorN), minRectangle, HopeStringAlign.Center);
                    }
                }
                if (MaximizeBox)
                {
                    maxRectangle = new(Width - 54, (Height - 16) / 2, 18, 18);

                    if (maxRectangle.Contains(mousePoint))
                    {
                        if (ParentForm.WindowState == FormWindowState.Normal)
                        {
                            graphics.DrawString("1", icoFont, new SolidBrush(_ControlBoxColorH), maxRectangle, HopeStringAlign.Center);
                        }
                        else
                        {
                            graphics.DrawString("2", icoFont, new SolidBrush(_ControlBoxColorH), maxRectangle, HopeStringAlign.Center);
                        }

                        Cursor = Cursors.Hand;
                    }
                    else
                    {
                        if (ParentForm.WindowState == FormWindowState.Normal)
                        {
                            graphics.DrawString("1", icoFont, new SolidBrush(_ControlBoxColorN), maxRectangle, HopeStringAlign.Center);
                        }
                        else
                        {
                            graphics.DrawString("2", icoFont, new SolidBrush(_ControlBoxColorN), maxRectangle, HopeStringAlign.Center);
                        }
                    }
                }

                closeRectangle = new(Width - 32, (Height - 16) / 2, 18, 18);

                if (closeRectangle.Contains(mousePoint))
                {
                    graphics.DrawString("r", icoFont, new SolidBrush(_ControlBoxColorHC), closeRectangle, HopeStringAlign.Center);
                    Cursor = Cursors.Hand;
                }
                else
                {
                    graphics.DrawString("r", icoFont, new SolidBrush(_ControlBoxColorN), closeRectangle, HopeStringAlign.Center);
                }

                if (!minRectangle.Contains(mousePoint) && !maxRectangle.Contains(mousePoint) && !closeRectangle.Contains(mousePoint))
                {
                    Cursor = Cursors.Default;
                }
            }

            base.OnPaint(e);
            graphics.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
            bitmap.Dispose();
        }

        public HopeForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            ForeColor = HopeColors.FourLevelBorder;
            Height = 40;
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            Dock = DockStyle.Top;
        }
    }

    #endregion
}