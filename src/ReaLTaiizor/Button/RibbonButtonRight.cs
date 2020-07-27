#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region RibbonButtonRight

    public class RibbonButtonRight : Control
    {
        #region " MouseStates "
        MouseStateRibbon State = MouseStateRibbon.None;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateRibbon.Down;
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateRibbon.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateRibbon.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateRibbon.None;
            Invalidate();
        }
        #endregion

        public RibbonButtonRight()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Size = new Size(140, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
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
                case MouseStateRibbon.None:
                    LinearGradientBrush lgb = new LinearGradientBrush(ClientRectangle, Color.FromArgb(123, 190, 216), Color.FromArgb(108, 175, 201), 90);
                    G.FillPath(lgb, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p = new Pen(new SolidBrush(Color.FromArgb(60, 113, 132)));
                    G.DrawPath(p, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Over:
                    LinearGradientBrush lgb2 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(113, 180, 206), Color.FromArgb(114, 181, 207), 90);
                    G.FillPath(lgb2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p2 = new Pen(new SolidBrush(Color.FromArgb(60, 113, 132)));
                    G.DrawPath(p2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip2 = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip2, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Down:
                    LinearGradientBrush lgb3 = new LinearGradientBrush(ClientRectangle, Color.FromArgb(123, 190, 216), Color.FromArgb(108, 175, 201), 90);
                    G.FillPath(lgb3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p3 = new Pen(new SolidBrush(Color.FromArgb(60, 113, 132)));
                    G.DrawPath(p3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip3 = new Pen(Color.FromArgb(75, Color.White));
                    G.DrawPath(Ip3, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
            }

            G.DrawString(Text, drawFont, new SolidBrush(ForeColor), new Rectangle(0, 1, Width - 1, Height - 1), new StringFormat
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