#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ThunderGroupBox

    public class ThunderGroupBox : ContainerControl
    {
        [Category("Colors")]
        public Color BodyColorA { get; set; } = Color.FromArgb(26, 26, 26);

        [Category("Colors")]
        public Color BodyColorB { get; set; } = Color.FromArgb(30, 30, 30);

        [Category("Colors")]
        public Color BodyColorC { get; set; } = Color.FromArgb(46, 46, 46);

        [Category("Colors")]
        public Color BodyColorD { get; set; } = Color.FromArgb(50, 55, 58);

        public ThunderGroupBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            DoubleBuffered = true;
            Size = new(132, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle Body = new(4, 25, Width - 9, Height - 30);
            Rectangle Body2 = new(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
            G.Clear(Color.Transparent);
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.CompositingQuality = CompositingQuality.HighQuality;

            Pen P1 = new(Color.Black);
            LinearGradientBrush BodyBrush = new(Body2, BodyColorA, BodyColorB, 90);
            LinearGradientBrush BodyBrush2 = new(Body, BodyColorC, BodyColorD, 120);
            Font drawFont = new("Tahoma", 9, FontStyle.Bold);
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