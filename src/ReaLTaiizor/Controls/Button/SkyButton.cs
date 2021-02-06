#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
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

        #region Variables
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;

        private Color _NormalBGColorA = Color.FromArgb(245, 245, 245);
        private Color _NormalBGColorB = Color.FromArgb(230, 230, 230);
        private Color _HoverBGColorA = Color.FromArgb(70, 153, 205);
        private Color _HoverBGColorB = Color.FromArgb(53, 124, 170);
        private Color _DownBGColorA = Color.FromArgb(70, 153, 205);
        private Color _DownBGColorB = Color.FromArgb(53, 124, 170);
        private Color _NormalForeColor = Color.FromArgb(27, 94, 137);
        private Color _HoverForeColor = Color.White;
        private Color _DownForeColor = Color.White;
        private Color _NormalShadowForeColor = Color.FromArgb(200, Color.White);
        private Color _HoverShadowForeColor = Color.FromArgb(200, Color.Black);
        private Color _DownShadowForeColor = Color.FromArgb(200, Color.Black);
        private Color _NormalBorderColorA = Color.FromArgb(252, 252, 252);
        private Color _NormalBorderColorB = Color.FromArgb(249, 249, 249);
        private Color _NormalBorderColorC = Color.FromArgb(189, 189, 189);
        private Color _NormalBorderColorD = Color.FromArgb(200, 168, 168, 168);
        private Color _HoverBorderColorA = Color.FromArgb(88, 168, 221);
        private Color _HoverBorderColorB = Color.FromArgb(76, 149, 194);
        private Color _HoverBorderColorC = Color.FromArgb(38, 93, 131);
        private Color _HoverBorderColorD = Color.FromArgb(200, 25, 73, 109);
        private Color _DownBorderColorA = Color.FromArgb(88, 168, 221);
        private Color _DownBorderColorB = Color.FromArgb(76, 149, 194);
        private Color _DownBorderColorC = Color.FromArgb(38, 93, 131);
        private Color _DownBorderColorD = Color.FromArgb(200, 25, 73, 109);
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

        public Color NormalBGColorA
        {
            get => _NormalBGColorA;
            set => _NormalBGColorA = value;
        }

        public Color NormalBGColorB
        {
            get => _NormalBGColorB;
            set => _NormalBGColorB = value;
        }

        public Color HoverBGColorA
        {
            get => _HoverBGColorA;
            set => _HoverBGColorA = value;
        }

        public Color HoverBGColorB
        {
            get => _HoverBGColorB;
            set => _HoverBGColorB = value;
        }

        public Color DownBGColorA
        {
            get => _DownBGColorA;
            set => _DownBGColorA = value;
        }

        public Color DownBGColorB
        {
            get => _DownBGColorB;
            set => _DownBGColorB = value;
        }

        public Color NormalForeColor
        {
            get => _NormalForeColor;
            set => _NormalForeColor = value;
        }

        public Color HoverForeColor
        {
            get => _HoverForeColor;
            set => _HoverForeColor = value;
        }

        public Color DownForeColor
        {
            get => _DownForeColor;
            set => _DownForeColor = value;
        }

        public Color NormalShadowForeColor
        {
            get => _NormalShadowForeColor;
            set => _NormalShadowForeColor = value;
        }

        public Color HoverShadowForeColor
        {
            get => _HoverShadowForeColor;
            set => _HoverShadowForeColor = value;
        }

        public Color DownShadowForeColor
        {
            get => _DownShadowForeColor;
            set => _DownShadowForeColor = value;
        }

        public Color NormalBorderColorA
        {
            get => _NormalBorderColorA;
            set => _NormalBorderColorA = value;
        }

        public Color NormalBorderColorB
        {
            get => _NormalBorderColorB;
            set => _NormalBorderColorB = value;
        }

        public Color NormalBorderColorC
        {
            get => _NormalBorderColorC;
            set => _NormalBorderColorC = value;
        }

        public Color NormalBorderColorD
        {
            get => _NormalBorderColorD;
            set => _NormalBorderColorD = value;
        }

        public Color HoverBorderColorA
        {
            get => _HoverBorderColorA;
            set => _HoverBorderColorA = value;
        }

        public Color HoverBorderColorB
        {
            get => _HoverBorderColorB;
            set => _HoverBorderColorB = value;
        }

        public Color HoverBorderColorC
        {
            get => _HoverBorderColorC;
            set => _HoverBorderColorC = value;
        }

        public Color HoverBorderColorD
        {
            get => _HoverBorderColorD;
            set => _HoverBorderColorD = value;
        }

        public Color DownBorderColorA
        {
            get => _DownBorderColorA;
            set => _DownBorderColorA = value;
        }

        public Color DownBorderColorB
        {
            get => _DownBorderColorB;
            set => _DownBorderColorB = value;
        }

        public Color DownBorderColorC
        {
            get => _DownBorderColorC;
            set => _DownBorderColorC = value;
        }

        public Color DownBorderColorD
        {
            get => _DownBorderColorD;
            set => _DownBorderColorD = value;
        }
        #endregion

        public SkyButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            DoubleBuffered = true;
            Size = new Size(75, 23);
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