#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SocialButton

    public class SocialButton : Control
    {
        #region Variables

        private Image _Image;
        private Size _ImageSize;
        private Color EllipseColor = Color.FromArgb(66, 76, 85); // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        private Color _HoverEllipseColor = Color.FromArgb(32, 34, 37);
        private Color _NormalEllipseColor = Color.FromArgb(66, 76, 85);
        private Color _DownEllipseColor = Color.FromArgb(153, 34, 34);

        #endregion

        #region Properties

        public Image Image
        {
            get => _Image;
            set
            {
                if (value == null)
                {
                    _ImageSize = Size.Empty;
                }
                else
                {
                    _ImageSize = value.Size;
                }

                _Image = value;
                Invalidate();
            }
        }

        protected Size ImageSize => _ImageSize;

        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color HoverEllipseColor
        {
            get => _HoverEllipseColor;
            set
            {
                _HoverEllipseColor = value;
                Invalidate();
            }
        }

        public Color NormalEllipseColor
        {
            get => _NormalEllipseColor;
            set
            {
                _NormalEllipseColor = value;
                Invalidate();
            }
        }

        public Color DownEllipseColor
        {
            get => _DownEllipseColor;
            set
            {
                _DownEllipseColor = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(54, 54);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            EllipseColor = HoverEllipseColor;
            Refresh();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            EllipseColor = NormalEllipseColor;
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            EllipseColor = DownEllipseColor;
            Focus();
            Refresh();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            EllipseColor = HoverEllipseColor;
            Refresh();
        }

        #endregion

        #region Image Designer

        private static PointF ImageLocation(StringFormat SF, SizeF Area, SizeF ImageArea)
        {
            PointF MyPoint = new();
            switch (SF.Alignment)
            {
                case StringAlignment.Center:
                    MyPoint.X = (float)((Area.Width - ImageArea.Width) / 2);
                    break;
            }

            switch (SF.LineAlignment)
            {
                case StringAlignment.Center:
                    MyPoint.Y = (float)((Area.Height - ImageArea.Height) / 2);
                    break;
            }
            return MyPoint;
        }

        private static StringFormat GetStringFormat(ContentAlignment _ContentAlignment)
        {
            StringFormat SF = new();
            switch (_ContentAlignment)
            {
                case ContentAlignment.MiddleCenter:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Center;
                    break;
            }
            return SF;
        }

        #endregion

        public SocialButton()
        {
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.Clear(Parent.BackColor);
            G.SmoothingMode = SmoothingType;

            PointF ImgPoint = ImageLocation(GetStringFormat(ContentAlignment.MiddleCenter), Size, ImageSize);
            G.FillEllipse(new SolidBrush(EllipseColor), new Rectangle(0, 0, 53, 53));

            // HINTS:
            // The best size for the drawn image is 32x32\
            // The best matching color of drawn image is (RGB: 31, 40, 49)
            if (Image != null)
            {
                G.DrawImage(_Image, (int)ImgPoint.X, (int)ImgPoint.Y, ImageSize.Width, ImageSize.Height);
            }
        }
    }

    #endregion
}