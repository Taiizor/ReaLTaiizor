#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region RibbonButtonCenter

    public class RibbonButtonCenter : Control
    {
        #region " MouseStates "
        MouseState State = MouseState.None;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }
        #endregion

        public RibbonButtonCenter()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(205, 205, 205);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Size = new Size(140, 40);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle ClientRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new Rectangle(1, 1, Width - 3, Height - 3);

            base.OnPaint(e);

            G.Clear(BackColor);
            Font drawFont = new Font("Tahoma", 8, FontStyle.Bold);

            //G.CompositingQuality = CompositingQuality.HighQuality
            G.SmoothingMode = SmoothingMode.HighQuality;

            switch (State)
            {
                case MouseState.None:
                    LinearGradientBrush lgb = new LinearGradientBrush(ClientRectangle, Color.FromArgb(214, 162, 68), Color.FromArgb(199, 147, 53), 90);
                    G.FillPath(lgb, Draw.RoundRect(ClientRectangle, 2));

                    Pen p = new Pen(new SolidBrush(Color.FromArgb(142, 107, 46)));
                    G.DrawPath(p, Draw.RoundRect(ClientRectangle, 2));
                    Pen Ip = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip, Draw.RoundRect(InnerRect, 2));
                    break;
                case MouseState.Over:
                    LinearGradientBrush lgb2 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(204, 152, 58), Color.FromArgb(205, 153, 59), 90);
                    G.FillPath(lgb2, Draw.RoundRect(ClientRectangle, 2));
                    Pen p2 = new Pen(new SolidBrush(Color.FromArgb(142, 107, 46)));
                    G.DrawPath(p2, Draw.RoundRect(ClientRectangle, 2));
                    Pen Ip2 = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip2, Draw.RoundRect(InnerRect, 2));
                    break;
                case MouseState.Down:
                    LinearGradientBrush lgb3 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(214, 162, 68), Color.FromArgb(199, 147, 53), 90);
                    G.FillPath(lgb3, Draw.RoundRect(ClientRectangle, 2));
                    Pen p3 = new Pen(new SolidBrush(Color.FromArgb(142, 107, 46)));
                    G.DrawPath(p3, Draw.RoundRect(ClientRectangle, 2));
                    Pen Ip3 = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip3, Draw.RoundRect(InnerRect, 2));
                    break;
            }

            G.DrawString(Text, drawFont, new SolidBrush(Color.White), new Rectangle(0, 1, Width - 1, Height - 1), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}