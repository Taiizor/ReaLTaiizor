#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ThunderButton

    public class ThunderButton : Control
    {
        private MouseStateThunder State = MouseStateThunder.None;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateThunder.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateThunder.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateThunder.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateThunder.None;
            Invalidate();
        }
        public ThunderButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Size = new(120, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle ClientRectangle = new(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);
            G.Clear(BackColor);
            Font drawFont = new("Tahoma", 8, FontStyle.Bold);
            G.SmoothingMode = SmoothingMode.HighQuality;
            Rectangle R1 = new(0, 0, Width - 125, 35 / 2);
            Rectangle R2 = new(5, Height - 10, Width - 11, 5);
            Rectangle R3 = new(6, Height - 9, Width - 13, 3);
            Rectangle R4 = new(1, 1, Width - 3, Height - 3);
            Rectangle R5 = new(1, 0, Width - 1, Height - 1);
            Rectangle R6 = new(0, -1, Width - 1, Height - 1);
            LinearGradientBrush lgb = new(ClientRectangle, Color.FromArgb(66, 67, 70), Color.FromArgb(43, 44, 48), 90);
            LinearGradientBrush botbar = new(R2, Color.FromArgb(44, 45, 49), Color.FromArgb(45, 46, 50), 90);
            LinearGradientBrush fill = new(R3, Color.FromArgb(174, 195, 30), Color.FromArgb(141, 153, 16), 90);
            Pen o = new(Color.FromArgb(50, 50, 50), 1);
            StringFormat format = new() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            LinearGradientBrush gloss;
            if (State == MouseStateThunder.Over)
            {
                gloss = new(R1, Color.FromArgb(15, Color.FromArgb(26, 26, 26)), Color.FromArgb(1, 255, 255, 255), 90);
            }
            else if (State == MouseStateThunder.Down)
            {
                gloss = new(R1, Color.FromArgb(100, Color.FromArgb(26, 26, 26)), Color.FromArgb(1, 255, 255, 255), 90);
            }
            else
            {
                gloss = new(R1, Color.FromArgb(75, Color.FromArgb(26, 26, 26)), Color.FromArgb(3, 255, 255, 255), 90);
            }

            G.FillPath(lgb, DrawThunder.RoundRect(ClientRectangle, 2));
            G.FillPath(gloss, DrawThunder.RoundRect(ClientRectangle, 2));
            G.FillPath(botbar, DrawThunder.RoundRect(R2, 1));
            G.FillPath(fill, DrawThunder.RoundRect(R3, 1));
            G.DrawPath(o, DrawThunder.RoundRect(ClientRectangle, 2));
            G.DrawPath(Pens.Black, DrawThunder.RoundRect(R4, 2));
            G.DrawString(Text, drawFont, new SolidBrush(Color.FromArgb(5, 5, 5)), R5, format);
            G.DrawString(Text, drawFont, new SolidBrush(ForeColor), R6, format);

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}