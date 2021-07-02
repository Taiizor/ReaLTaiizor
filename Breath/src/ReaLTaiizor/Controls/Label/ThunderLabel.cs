#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ThunderLabel

    public class ThunderLabel : Control
    {
        public ThunderLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            DoubleBuffered = true;
            Size = new(96, 16);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            //Rectangle ClientRectangle = new(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
            G.Clear(BackColor);
            Font drawFont = new("Tahoma", 9, FontStyle.Bold);
            StringFormat format = new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            G.CompositingQuality = CompositingQuality.HighQuality;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.DrawString(Text, drawFont, new SolidBrush(Color.FromArgb(5, 5, 5)), new Rectangle(1, 0, Width - 1, Height - 1), format);
            G.DrawString(Text, drawFont, new SolidBrush(ForeColor), new Rectangle(0, -1, Width - 1, Height - 1), format);
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}