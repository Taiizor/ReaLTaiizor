#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
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

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get { return _SmoothingType; }
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private Color _BaseColorA = Color.FromArgb(123, 190, 216);
        public Color BaseColorA
        {
            get { return _BaseColorA; }
            set
            {
                _BaseColorA = value;
                Invalidate();
            }
        }

        private Color _BaseColorB = Color.FromArgb(108, 175, 201);
        public Color BaseColorB
        {
            get { return _BaseColorB; }
            set
            {
                _BaseColorB = value;
                Invalidate();
            }
        }

        private Color _BorderColorA = Color.FromArgb(60, 113, 132);
        public Color BorderColorA
        {
            get { return _BorderColorA; }
            set
            {
                _BorderColorA = value;
                Invalidate();
            }
        }

        private Color _BorderColorB = Color.FromArgb(75, Color.White);
        public Color BorderColorB
        {
            get { return _BorderColorB; }
            set
            {
                _BorderColorB = value;
                Invalidate();
            }
        }

        private Color _HoverBaseColorA = Color.FromArgb(113, 180, 206);
        public Color HoverBaseColorA
        {
            get { return _HoverBaseColorA; }
            set
            {
                _HoverBaseColorA = value;
                Invalidate();
            }
        }

        private Color _HoverBaseColorB = Color.FromArgb(114, 181, 207);
        public Color HoverBaseColorB
        {
            get { return _HoverBaseColorB; }
            set
            {
                _HoverBaseColorB = value;
                Invalidate();
            }
        }

        private Color _HoverBorderColorA = Color.FromArgb(60, 113, 132);
        public Color HoverBorderColorA
        {
            get { return _HoverBorderColorA; }
            set
            {
                _HoverBorderColorA = value;
                Invalidate();
            }
        }

        private Color _HoverBorderColorB = Color.FromArgb(75, Color.White);
        public Color HoverBorderColorB
        {
            get { return _HoverBorderColorB; }
            set
            {
                _HoverBorderColorB = value;
                Invalidate();
            }
        }

        private Color _DownBaseColorA = Color.FromArgb(123, 190, 216);
        public Color DownBaseColorA
        {
            get { return _DownBaseColorA; }
            set
            {
                _DownBaseColorA = value;
                Invalidate();
            }
        }

        private Color _DownBaseColorB = Color.FromArgb(108, 175, 201);
        public Color DownBaseColorB
        {
            get { return _DownBaseColorB; }
            set
            {
                _DownBaseColorB = value;
                Invalidate();
            }
        }

        private Color _DownBorderColorA = Color.FromArgb(60, 113, 132);
        public Color DownBorderColorA
        {
            get { return _DownBorderColorA; }
            set
            {
                _DownBorderColorA = value;
                Invalidate();
            }
        }

        private Color _DownBorderColorB = Color.FromArgb(75, Color.White);
        public Color DownBorderColorB
        {
            get { return _DownBorderColorB; }
            set
            {
                _DownBorderColorB = value;
                Invalidate();
            }
        }

        public RibbonButtonRight()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Font = new Font("Tahoma", 8, FontStyle.Bold);
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

            //G.CompositingQuality = CompositingQuality.HighQuality;
            G.SmoothingMode = SmoothingType;

            switch (State)
            {
                case MouseStateRibbon.None:
                    LinearGradientBrush lgb = new LinearGradientBrush(ClientRectangle, BaseColorA, BaseColorB, 90);
                    G.FillPath(lgb, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p = new Pen(new SolidBrush(BorderColorA));
                    G.DrawPath(p, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip = new Pen(BorderColorB);
                    G.DrawPath(Ip, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Over:
                    LinearGradientBrush lgb2 = new LinearGradientBrush(ClientRectangle, HoverBaseColorA, HoverBaseColorB, 90);
                    G.FillPath(lgb2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p2 = new Pen(new SolidBrush(HoverBorderColorA));
                    G.DrawPath(p2, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip2 = new Pen(HoverBorderColorB);
                    G.DrawPath(Ip2, DrawRibbon.RoundRect(InnerRect, 2));
                    break;
                case MouseStateRibbon.Down:
                    LinearGradientBrush lgb3 = new LinearGradientBrush(ClientRectangle, DownBaseColorA, DownBaseColorB, 90);
                    G.FillPath(lgb3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen p3 = new Pen(new SolidBrush(DownBorderColorA));
                    G.DrawPath(p3, DrawRibbon.RoundRect(ClientRectangle, 2));
                    Pen Ip3 = new Pen(DownBorderColorB);
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