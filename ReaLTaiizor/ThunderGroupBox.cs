#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region ThunderGroupBox

    public class ThunderGroupBox : ContainerControl
    {
        public ThunderGroupBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            DoubleBuffered = true;
            Size = new Size(132, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle Body = new Rectangle(4, 25, Width - 9, Height - 30);
            Rectangle Body2 = new Rectangle(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
            G.Clear(Color.Transparent);
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.CompositingQuality = CompositingQuality.HighQuality;

            Pen P1 = new Pen(Color.Black);
            LinearGradientBrush BodyBrush = new LinearGradientBrush(Body2, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            LinearGradientBrush BodyBrush2 = new LinearGradientBrush(Body, Color.FromArgb(46, 46, 46), Color.FromArgb(50, 55, 58), 120);
            Font drawFont = new Font("Tahoma", 9, FontStyle.Bold);
            G.FillPath(BodyBrush, DrawThunder.RoundRect(Body2, 3));
            G.DrawPath(P1, DrawThunder.RoundRect(Body2, 3));

            G.FillPath(BodyBrush2, DrawThunder.RoundRect(Body, 3));
            G.DrawPath(P1, DrawThunder.RoundRect(Body, 3));

            G.DrawString(Text, drawFont, new SolidBrush(ForeColor), 67, 14, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}