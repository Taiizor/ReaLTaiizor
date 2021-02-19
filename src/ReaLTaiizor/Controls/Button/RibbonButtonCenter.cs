#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RibbonButtonCenter

    public class RibbonButtonCenter : Control
    {
        #region " MouseStates "
        private MouseStateRibbon State = MouseStateRibbon.None;
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

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private Color _BaseColorA = Color.FromArgb(214, 162, 68);
        public Color BaseColorA
        {
            get => _BaseColorA;
            set
            {
                _BaseColorA = value;
                Invalidate();
            }
        }

        private Color _BaseColorB = Color.FromArgb(199, 147, 53);
        public Color BaseColorB
        {
            get => _BaseColorB;
            set
            {
                _BaseColorB = value;
                Invalidate();
            }
        }

        private Color _BorderColorA = Color.FromArgb(142, 107, 46);
        public Color BorderColorA
        {
            get => _BorderColorA;
            set
            {
                _BorderColorA = value;
                Invalidate();
            }
        }

        private Color _BorderColorB = Color.FromArgb(75, Color.White);
        public Color BorderColorB
        {
            get => _BorderColorB;
            set
            {
                _BorderColorB = value;
                Invalidate();
            }
        }

        private Color _HoverBaseColorA = Color.FromArgb(204, 152, 58);
        public Color HoverBaseColorA
        {
            get => _HoverBaseColorA;
            set
            {
                _HoverBaseColorA = value;
                Invalidate();
            }
        }

        private Color _HoverBaseColorB = Color.FromArgb(205, 153, 59);
        public Color HoverBaseColorB
        {
            get => _HoverBaseColorB;
            set
            {
                _HoverBaseColorB = value;
                Invalidate();
            }
        }

        private Color _HoverBorderColorA = Color.FromArgb(142, 107, 46);
        public Color HoverBorderColorA
        {
            get => _HoverBorderColorA;
            set
            {
                _HoverBorderColorA = value;
                Invalidate();
            }
        }

        private Color _HoverBorderColorB = Color.FromArgb(75, Color.White);
        public Color HoverBorderColorB
        {
            get => _HoverBorderColorB;
            set
            {
                _HoverBorderColorB = value;
                Invalidate();
            }
        }

        private Color _DownBaseColorA = Color.FromArgb(214, 162, 68);
        public Color DownBaseColorA
        {
            get => _DownBaseColorA;
            set
            {
                _DownBaseColorA = value;
                Invalidate();
            }
        }

        private Color _DownBaseColorB = Color.FromArgb(199, 147, 53);
        public Color DownBaseColorB
        {
            get => _DownBaseColorB;
            set
            {
                _DownBaseColorB = value;
                Invalidate();
            }
        }

        private Color _DownBorderColorA = Color.FromArgb(142, 107, 46);
        public Color DownBorderColorA
        {
            get => _DownBorderColorA;
            set
            {
                _DownBorderColorA = value;
                Invalidate();
            }
        }

        private Color _DownBorderColorB = Color.FromArgb(75, Color.White);
        public Color DownBorderColorB
        {
            get => _DownBorderColorB;
            set
            {
                _DownBorderColorB = value;
                Invalidate();
            }
        }

        public RibbonButtonCenter()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Font = new("Tahoma", 8, FontStyle.Bold);
            Size = new(140, 40);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle ClientRectangle = new(0, 0, Width - 1, Height - 1);
            Rectangle InnerRect = new(1, 1, Width - 3, Height - 3);

            base.OnPaint(e);

            G.Clear(BackColor);

            //G.CompositingQuality = CompositingQuality.HighQuality;
            G.SmoothingMode = SmoothingType;

            switch (State)
            {
                case MouseStateRibbon.None:
                    LinearGradientBrush lgb = new(ClientRectangle, BaseColorA, BaseColorB, 90);
                    G.FillPath(lgb, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p = new(new SolidBrush(BorderColorA));
                    G.DrawPath(p, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip = new(BorderColorB);
                    G.DrawPath(Ip, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Over:
                    LinearGradientBrush lgb2 = new(ClientRectangle, HoverBaseColorA, HoverBaseColorB, 90);
                    G.FillPath(lgb2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p2 = new(new SolidBrush(HoverBorderColorA));
                    G.DrawPath(p2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip2 = new(HoverBorderColorB);
                    G.DrawPath(Ip2, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Down:
                    LinearGradientBrush lgb3 = new(ClientRectangle, DownBaseColorA, DownBaseColorB, 90);
                    G.FillPath(lgb3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p3 = new(new SolidBrush(DownBorderColorA));
                    G.DrawPath(p3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip3 = new(DownBorderColorB);
                    G.DrawPath(Ip3, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(0, 1, Width - 1, Height - 1), new StringFormat
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