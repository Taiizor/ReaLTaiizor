#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

        #region Variables
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color NormalBGColorA { get; set; } = Color.FromArgb(245, 245, 245);

        public Color NormalBGColorB { get; set; } = Color.FromArgb(230, 230, 230);

        public Color HoverBGColorA { get; set; } = Color.FromArgb(70, 153, 205);

        public Color HoverBGColorB { get; set; } = Color.FromArgb(53, 124, 170);

        public Color DownBGColorA { get; set; } = Color.FromArgb(70, 153, 205);

        public Color DownBGColorB { get; set; } = Color.FromArgb(53, 124, 170);

        public Color NormalForeColor { get; set; } = Color.FromArgb(27, 94, 137);

        public Color HoverForeColor { get; set; } = Color.White;

        public Color DownForeColor { get; set; } = Color.White;

        public Color NormalShadowForeColor { get; set; } = Color.FromArgb(200, Color.White);

        public Color HoverShadowForeColor { get; set; } = Color.FromArgb(200, Color.Black);

        public Color DownShadowForeColor { get; set; } = Color.FromArgb(200, Color.Black);

        public Color NormalBorderColorA { get; set; } = Color.FromArgb(252, 252, 252);

        public Color NormalBorderColorB { get; set; } = Color.FromArgb(249, 249, 249);

        public Color NormalBorderColorC { get; set; } = Color.FromArgb(189, 189, 189);

        public Color NormalBorderColorD { get; set; } = Color.FromArgb(200, 168, 168, 168);

        public Color HoverBorderColorA { get; set; } = Color.FromArgb(88, 168, 221);

        public Color HoverBorderColorB { get; set; } = Color.FromArgb(76, 149, 194);

        public Color HoverBorderColorC { get; set; } = Color.FromArgb(38, 93, 131);

        public Color HoverBorderColorD { get; set; } = Color.FromArgb(200, 25, 73, 109);

        public Color DownBorderColorA { get; set; } = Color.FromArgb(88, 168, 221);

        public Color DownBorderColorB { get; set; } = Color.FromArgb(76, 149, 194);

        public Color DownBorderColorC { get; set; } = Color.FromArgb(38, 93, 131);

        public Color DownBorderColorD { get; set; } = Color.FromArgb(200, 25, 73, 109);
        #endregion

        public SkyButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Size = new(75, 23);
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            //object ClientRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            base.OnPaint(e);

            G.SmoothingMode = SmoothingType;
            G.Clear(BackColor);

            switch (State)
            {
                case MouseStateSky.None:
                    //Mouse None
                    LinearGradientBrush bodyGrad = new(new Rectangle(0, 0, Width - 1, Height - 2), NormalBGColorA, NormalBGColorB, 90);
                    G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
                    LinearGradientBrush bodyInBorder = new(new Rectangle(1, 1, Width - 3, Height - 4), NormalBorderColorA, NormalBorderColorB, 90);
                    G.DrawRectangle(new(bodyInBorder), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new(NormalBorderColorC), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new(NormalBorderColorD), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = NormalForeColor;
                    G.DrawString(Text, Font, new SolidBrush(NormalShadowForeColor), new Rectangle(-1, 0, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseStateSky.Over:
                    //Mouse Hover
                    LinearGradientBrush bodyGradOver = new(new Rectangle(0, 0, Width - 1, Height - 2), HoverBGColorA, HoverBGColorB, 90);
                    G.FillRectangle(bodyGradOver, bodyGradOver.Rectangle);
                    LinearGradientBrush bodyInBorderOver = new(new Rectangle(1, 1, Width - 3, Height - 4), HoverBorderColorA, HoverBorderColorB, 90);
                    G.DrawRectangle(new(bodyInBorderOver), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new(HoverBorderColorC), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new(HoverBorderColorD), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = HoverForeColor;
                    G.DrawString(Text, Font, new SolidBrush(HoverShadowForeColor), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                    break;
                case MouseStateSky.Down:
                    //Mouse Down
                    LinearGradientBrush bodyGradDown = new(new Rectangle(0, 0, Width - 1, Height - 2), DownBGColorA, DownBGColorB, 270);
                    G.FillRectangle(bodyGradDown, bodyGradDown.Rectangle);
                    LinearGradientBrush bodyInBorderDown = new(new Rectangle(1, 1, Width - 3, Height - 4), DownBorderColorA, DownBorderColorB, 270);
                    G.DrawRectangle(new(bodyInBorderDown), new Rectangle(1, 1, Width - 3, Height - 4));
                    G.DrawRectangle(new(DownBorderColorC), new Rectangle(0, 0, Width - 1, Height - 2));
                    G.DrawLine(new(DownBorderColorD), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
                    ForeColor = DownForeColor;
                    G.DrawString(Text, Font, new SolidBrush(DownShadowForeColor), new Rectangle(-1, -2, Width - 1, Height - 1), new StringFormat
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