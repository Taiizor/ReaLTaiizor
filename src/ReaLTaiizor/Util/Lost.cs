#region Imports

using ReaLTaiizor.Extension;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Util
{
    #region LostUtil

    public static class ThemeLost
    {
        public static Font TitleFont = new("Segoe UI", 12);
        public static Font HeaderFont = new("Segoe UI", 9, FontStyle.Bold);
        public static Font BodyFont = new("Segoe UI", 9);

        public static Color FontColor = Color.White;
        public static SolidBrush FontBrush = new(FontColor);
        public static Pen FontPen = new(FontColor);

        public static Color ForeColor = Color.FromArgb(63, 63, 70);
        public static SolidBrush ForeBrush = new(ForeColor);
        public static Pen ForePen = new(ForeColor);

        public static Color BackColor = Color.FromArgb(45, 45, 48);
        public static SolidBrush BackBrush = new(BackColor);
        public static Pen BackPen = new(BackColor);

        public static Color AccentColor = Color.DodgerBlue;
        public static SolidBrush AccentBrush = new(AccentColor);
        public static Pen AccentPen = new(AccentColor);

        public static int ShadowSize = 8;
        public static Color ShadowColor = Color.FromArgb(30, 30, 30);

        public static void SetFont(string fontName, int bodySize, int titleSize)
        {
            TitleFont = new(fontName, titleSize);
            HeaderFont = new(fontName, bodySize, FontStyle.Bold);
            BodyFont = new(fontName, bodySize);
        }

        public static void SetFontColor(Color c)
        {
            FontColor = c;
            FontBrush = new(c);
            FontPen = new(c);
        }

        public static void SetForeColor(Color c)
        {
            ForeColor = c;
            ForeBrush = new(c);
            ForePen = new(c);
        }

        public static void SetBackColor(Color c)
        {
            BackColor = c;
            BackBrush = new(c);
            BackPen = new(c);
        }

        public static void SetAccentColor(Color c)
        {
            AccentColor = c;
            AccentBrush = new(c);
            AccentPen = new(c);
        }

        public static void SetShadowSize(int size)
        {
            ShadowSize = size;
        }

        public static void SetShadowColor(Color c)
        {
            ShadowColor = c;
        }
    }

    public abstract class ControlLostBase : Control
    {
        public bool HasShadow = false;
        public int ShadowLevel = 0;
        public bool MouseOver = false;
        public bool IsMouseDown = false;

        private readonly Timer _ticker = new();

        public ControlLostBase()
        {
            DoubleBuffered = true;
            BackColor = ThemeLost.BackBrush.Color;
            ForeColor = ThemeLost.ForeColor;
            _ticker.Interval = 16;
            _ticker.Tick += _ticker_Tick;
        }

        public virtual Rectangle ShadeRect(int index)
        {
            return new Rectangle(Location.X - index, Location.Y - index, Width + (index * 2), Height + (index * 2));
        }

        public virtual void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(new(ThemeLost.ShadowColor.Shade(ThemeLost.ShadowSize, i)), ShadeRect(i));
                }
            }
        }

        private void _ticker_Tick(object sender, EventArgs e)
        {
            try
            {
                ShadowLevel++;

                if (ShadowLevel >= ThemeLost.ShadowSize || ShadowLevel == 0 || Disposing)
                {
                    _ticker.Stop();
                }

                Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            }
            catch
            {
                _ticker.Stop();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsMouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsMouseDown = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseOver = true;
            ShadowLevel = 1;
            _ticker.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseOver = false;
            ShadowLevel = 0;
        }
    }

    public class FormLostBase : Form
    {
        public FormLostBase()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            FormBorderStyle = FormBorderStyle.None;
            BackColor = ThemeLost.BackBrush.Color;
            DoubleBuffered = true;
        }

        public virtual void DrawShadow(Graphics g)
        {
            for (int i = 0; i < ThemeLost.ShadowSize; i++)
            {
                g.DrawRectangle(new(ThemeLost.ShadowColor.Shade(ThemeLost.ShadowSize, i)), ShadeRect(i));
            }
        }

        public virtual Rectangle ShadeRect(int index)
        {
            return new Rectangle(Location.X - index, Location.Y - index, Width + (index * 2), Height + (index * 2));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Control c in Controls)
            {
                if ((c is ControlLostBase || c is FormLostBase) && c.Visible)
                {
                    (c as dynamic).DrawShadow(e.Graphics);
                }
            }
        }
    }

    public class ToolFrameLost : FrameLost
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public ToolFrameLost()
        {
            Padding = new Padding(10, 40, 10, 45);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left && Parent != null && Parent is not ToolFrameLost && e.X <= Width && e.Y <= 30)
            {
                ReleaseCapture();
                _ = SendMessage(Parent.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(ThemeLost.AccentBrush, 0, 0, Width, 30);
            e.Graphics.DrawString(Text, ThemeLost.HeaderFont, ThemeLost.FontBrush, 4, 6);
            e.Graphics.FillRectangle(ThemeLost.ForeBrush, 0, Height - 34, Width, 34);
            DrawShadow(e.Graphics, new Rectangle(0, 0, Width - 1, 30));
            DrawShadow(e.Graphics, new Rectangle(0, Height - 34, Width, 34));
            //DrawShadow(e.Graphics, new Rectangle(Width, 29 + ThemeLost.ShadowSize, 1, Height));

            base.OnPaint(e);
        }

        private static Rectangle ShadeRect(Rectangle origin, int index)
        {
            return new Rectangle(origin.X - index, origin.Y - index, origin.Width + (index * 2), origin.Height + (index * 2));
        }

        private void DrawShadow(Graphics g, Rectangle rect)
        {
            for (int i = 0; i < ThemeLost.ShadowSize; i++)
            {
                g.DrawRectangle(new(ThemeLost.ShadowColor.Shade(ThemeLost.ShadowSize, i)), ShadeRect(rect, i));
            }
        }
    }

    public class FrameLost : FormLostBase
    {
        private const int AW_HOR_POSITIVE = 0X1;
        private const int AW_HOR_NEGATIVE = 0X2;
        private const int AW_VER_POSITIVE = 0X4;
        private const int AW_VER_NEGATIVE = 0X8;
        private const int AW_CENTER = 0X10;
        private const int AW_HIDE = 0X10000;
        private const int AW_ACTIVATE = 0X20000;
        private const int AW_SLIDE = 0X40000;
        private const int AW_BLEND = 0X80000;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

        public FrameLost Present(FormLostBase parent)
        {
            Present(parent, Dock);
            return this;
        }

        public FrameLost Present(FormLostBase parent, DockStyle dock)
        {
            Dock = dock;
            Attach(parent);
            return this;
        }

        public void PresentReplacement(FormLostBase replacee, DockStyle dock)
        {
            replacee.Hide();
            Dock = dock;
            Attach(replacee.Parent as FormLostBase);
            FormClosed += (s, e) =>
            {
                replacee.Show();
            };
        }

        public void OnExit(System.Action a)
        {
            FormClosed += (s, e) => a();
        }

        public void Attach(FormLostBase parent)
        {
            ResizeRedraw = true;
            TopLevel = false;
            parent.Controls.Add(this);
            Parent = parent;
            PerformLayout();
            //Animate();

            (this as Control).Show();
            BringToFront();
            Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
        }

        public void Hide()
        {
            base.Hide();

            if (Parent != null)
            {
                Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            }
        }

        public void Show()
        {
            //Animate();
            base.Show();

            if (Parent != null)
            {
                Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            }
        }

        private static void Animate()
        {
            return;
            /*
                if (Dock == DockStyle.Top)
                {
                    Size = new(Parent.ClientRectangle.Width - Parent.Padding.Top * 2, Height);
                    Location = new(Parent.Padding.Left, Parent.Padding.Top);
                    AnimateWindow(Handle, 200, AW_VER_POSITIVE);
                }
                else if (Dock == DockStyle.Bottom)
                {
                    Size = new(Parent.ClientRectangle.Width - Parent.Padding.Left * 2, Height);
                    Location = new(Parent.Padding.Left, Parent.ClientRectangle.Height - Height - Parent.Padding.Bottom);
                    AnimateWindow(Handle, 200, AW_VER_NEGATIVE);
                }
                else if (Dock == DockStyle.Left)
                {
                    Size = new(Width, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                    Location = new(Parent.Padding.Left, Parent.Padding.Top);
                    AnimateWindow(Handle, 200, AW_HOR_POSITIVE);
                }
                else if (Dock == DockStyle.Right)
                {
                    Size = new(Width, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                    Location = new(Parent.ClientRectangle.Width - Width, Parent.Padding.Top);
                    AnimateWindow(Handle, 200, AW_HOR_NEGATIVE);
                }
                else if (Dock == DockStyle.Fill)
                {
                    Size = new(Parent.ClientRectangle.Width - Parent.Padding.Left * 2, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                    Location = new(Parent.Padding.Left, Parent.Padding.Top);
                    AnimateWindow(Handle, 200, AW_HOR_POSITIVE);
                }
            */
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Parent != null)
            {
                Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (Parent != null)
            {
                Parent.Invalidate(ShadeRect(ThemeLost.ShadowSize), false);
            }
        }
    }

    #endregion
}