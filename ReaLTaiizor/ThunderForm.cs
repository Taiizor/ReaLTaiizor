#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region ThunderForm

    public class ThunderTheme : ContainerControl
    {
        public ThunderTheme()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.FromArgb(25, 25, 25);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle TopLeft = new Rectangle(0, 0, Width - 125, 28);
            Rectangle TopRight = new Rectangle(Width - 82, 0, 81, 28);
            Rectangle Body = new Rectangle(10, 10, Width - 21, Height - 16);
            Rectangle Body2 = new Rectangle(5, 5, Width - 11, Height - 6);
            base.OnPaint(e);
            LinearGradientBrush BodyBrush = new LinearGradientBrush(Body2, Color.FromArgb(25, 25, 25), Color.FromArgb(30, 35, 48), 90);
            LinearGradientBrush BodyBrush2 = new LinearGradientBrush(Body, Color.FromArgb(46, 46, 46), Color.FromArgb(50, 55, 58), 120);
            LinearGradientBrush gloss = new LinearGradientBrush(new Rectangle(0, 0, Width - 128, 28 / 2), Color.FromArgb(240, Color.FromArgb(26, 26, 26)), Color.FromArgb(5, 255, 255, 255), 90);
            LinearGradientBrush gloss2 = new LinearGradientBrush(new Rectangle(Width - 82, 0, Width - 205, 28 / 2), Color.FromArgb(240, Color.FromArgb(26, 26, 26)), Color.FromArgb(5, 255, 255, 255), 90);
            LinearGradientBrush mainbrush = new LinearGradientBrush(TopLeft, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            LinearGradientBrush mainbrush2 = new LinearGradientBrush(TopRight, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            Pen P1 = new Pen(Color.FromArgb(174, 195, 30), 2);
            Font drawFont = new Font("Tahoma", 10, FontStyle.Bold);

            G.Clear(Color.Fuchsia);
            G.FillPath(BodyBrush, DrawThunder.RoundRect(Body2, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(Body2, 3));

            G.FillPath(BodyBrush2, DrawThunder.RoundRect(Body, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(Body, 3));

            G.FillPath(mainbrush, DrawThunder.RoundRect(TopLeft, 3));
            G.FillPath(gloss, DrawThunder.RoundRect(TopLeft, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(TopLeft, 3));

            G.FillPath(mainbrush, DrawThunder.RoundRect(TopRight, 3));
            G.FillPath(gloss2, DrawThunder.RoundRect(TopRight, 3));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(TopRight, 3));

            G.DrawLine(P1, 14, 9, 14, 22);
            G.DrawLine(P1, 17, 6, 17, 25);
            G.DrawLine(P1, 20, 9, 20, 22);
            G.DrawLine(P1, 11, 12, 11, 19);
            G.DrawLine(P1, 23, 12, 23, 19);
            G.DrawLine(P1, 8, 14, 8, 17);
            G.DrawLine(P1, 26, 14, 26, 17);
            G.DrawString(Text, drawFont, new SolidBrush(Color.WhiteSmoke), new Rectangle(32, 1, Width - 1, 27), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
        private Point MouseP = new Point(0, 0);
        private bool cap = false;
        private int moveheight = 29;
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
                Point p = new Point();
                p.X = MousePosition.X - MouseP.X;
                p.Y = MousePosition.Y - MouseP.Y;
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