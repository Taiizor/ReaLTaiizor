#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyButton

    public class SkyButton : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private MouseStateSky State = MouseStateSky.None;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateSky.Over;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateSky.Down;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateSky.None;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateSky.Over;
            Invalidate();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        #endregion

        public SkyButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Size = new Size(75, 23);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            object ClientRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(BackColor);

            switch (State)
            {
                case MouseStateSky.None:
                    //Mouse None
                    LinearGradientBrush bodyGrad = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
                    G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
                    LinearGradientBrush bodyInBorder = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(252, 252, 252), Color.FromArgb(249, 249, 249), 90);
                    G.DrawRectangle(new Pen(bodyInBorder), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 168, 168, 168)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.FromArgb(27, 94, 137);
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.White)), new Rectangle(-1, 0, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseStateSky.Over:
                    //Mouse Hover
                    LinearGradientBrush bodyGradOver = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(70, 153, 205), Color.FromArgb(53, 124, 170), 90);
                    G.FillRectangle(bodyGradOver, bodyGradOver.Rectangle);
                    LinearGradientBrush bodyInBorderOver = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(88, 168, 221), Color.FromArgb(76, 149, 194), 90);
                    G.DrawRectangle(new Pen(bodyInBorderOver), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(38, 93, 131)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 25, 73, 109)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.White;
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.Black)), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseStateSky.Down:
                    //Mouse Down
                    LinearGradientBrush bodyGradDown = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(70, 153, 205), Color.FromArgb(53, 124, 170), 270);
                    G.FillRectangle(bodyGradDown, bodyGradDown.Rectangle);
                    LinearGradientBrush bodyInBorderDown = new LinearGradientBrush(new Rectangle(1, 1, Width - 3, Height - 4), Color.FromArgb(88, 168, 221), Color.FromArgb(76, 149, 194), 270);
                    G.DrawRectangle(new Pen(bodyInBorderDown), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new Pen(Color.FromArgb(38, 93, 131)), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new Pen(Color.FromArgb(200, 25, 73, 109)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = Color.White;
                    G.DrawString(Text, Font, new SolidBrush(Color.FromArgb(200, Color.Black)), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
            }
            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(-1, -1, Width - 1, Height - 1), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}