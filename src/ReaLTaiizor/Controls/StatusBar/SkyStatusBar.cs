#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyStatusBar

    public class SkyStatusBar : Control
    {

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Dock = DockStyle.Bottom;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        public SkyStatusBar() : base()
        {
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new Size(Width, 20);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            base.OnPaint(e);

            LinearGradientBrush bodyGradNone = new LinearGradientBrush(new Rectangle(0, 1, Width, Height - 1), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 3), Color.FromArgb(200, 252, 252, 252), Color.FromArgb(200, 249, 249, 249), 90);
            G.DrawRectangle(new Pen(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 3));
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 1));

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(4, 4), StringFormat.GenericDefault);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}