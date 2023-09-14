#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region ThunderForm

    public class ThunderForm : ContainerControl
    {
        public Image Image { get; set; } = Properties.Resources.Taiizor;
        public Color BodyColorA { get; set; } = Color.FromArgb(25, 25, 25);
        public Color BodyColorB { get; set; } = Color.FromArgb(30, 35, 48);
        public Color BodyColorC { get; set; } = Color.FromArgb(46, 46, 46);
        public Color BodyColorD { get; set; } = Color.FromArgb(50, 55, 58);

        public ThunderForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            ForeColor = Color.WhiteSmoke;
            Padding = new Padding(11, 29, 11, 6);
            MinimumSize = new(270, 50);
            DoubleBuffered = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle TopLeft = new(0, 0, Width - 125, 28);
            Rectangle TopRight = new(Width - 82, 0, 81, 28);
            Rectangle Body = new(10, 10, Width - 21, Height - 16);
            Rectangle Body2 = new(5, 5, Width - 11, Height - 6);
            base.OnPaint(e);
            LinearGradientBrush BodyBrush = new(Body2, BodyColorA, BodyColorB, 90);
            LinearGradientBrush BodyBrush2 = new(Body, BodyColorC, BodyColorD, 120);
            LinearGradientBrush gloss = new(new Rectangle(0, 0, Width - 128, 28 / 2), Color.FromArgb(240, Color.FromArgb(26, 26, 26)), Color.FromArgb(5, 255, 255, 255), 90);
            LinearGradientBrush gloss2 = new(new Rectangle(Width - 82, 0, Width - 205, 28 / 2), Color.FromArgb(240, Color.FromArgb(26, 26, 26)), Color.FromArgb(5, 255, 255, 255), 90);
            LinearGradientBrush mainbrush1 = new(TopLeft, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            LinearGradientBrush mainbrush2 = new(TopRight, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            Pen P1 = new(Color.FromArgb(174, 195, 30), 2);
            Font drawFont = new("Tahoma", 10, FontStyle.Bold);

            G.Clear(Color.Fuchsia);
            G.FillPath(BodyBrush, DrawThunder.RoundRect(Body2, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(Body2, 3));

            G.FillPath(BodyBrush2, DrawThunder.RoundRect(Body, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(Body, 3));

            G.FillPath(mainbrush1, DrawThunder.RoundRect(TopLeft, 3));
            G.FillPath(gloss, DrawThunder.RoundRect(TopLeft, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(TopLeft, 3));

            G.FillPath(mainbrush2, DrawThunder.RoundRect(TopRight, 3));
            G.FillPath(gloss2, DrawThunder.RoundRect(TopRight, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(TopRight, 3));

            if (Image == null)
            {
                G.DrawLine(P1, 14, 9, 14, 22);
                G.DrawLine(P1, 17, 6, 17, 25);
                G.DrawLine(P1, 20, 9, 20, 22);
                G.DrawLine(P1, 11, 12, 11, 19);
                G.DrawLine(P1, 23, 12, 23, 19);
                G.DrawLine(P1, 8, 14, 8, 17);
                G.DrawLine(P1, 26, 14, 26, 17);
                G.DrawString(base.Text, drawFont, new SolidBrush(ForeColor), new Rectangle(32, 1, Width - 1, 27), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            }
            else
            {
                G.DrawImage(Image, 11, 2, 25, 25);
                G.DrawString(base.Text, drawFont, new SolidBrush(ForeColor), new Rectangle(45, 1, Width - 1, 27), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);

            G.Dispose();
            B.Dispose();
        }

        private Point MouseP = new(0, 0);
        private bool cap = false;
        private readonly int moveheight = 29;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left && new Rectangle(0, 0, Width, moveheight).Contains(e.Location))
            {
                cap = true;
                MouseP = e.Location;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            cap = false;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (cap)
            {
                Point p = new()
                {
                    X = MousePosition.X - MouseP.X,
                    Y = MousePosition.Y - MouseP.Y
                };
                Parent.Location = p;
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.None;
            ParentForm.TransparencyKey = Color.Fuchsia;
            Dock = DockStyle.Fill;
        }
    }

    #endregion
}