#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonButtonLeft

    public class DungeonButtonLeft : Control
    {

        #region Variables

        private int MouseState;
        private GraphicsPath Shape;
        private LinearGradientBrush InactiveGB;
        private LinearGradientBrush PressedGB;
        private LinearGradientBrush PressedContourGB;
        private Rectangle R1;
        private readonly Pen P1;
        private Pen P3;
        private Image _Image;
        private Size _ImageSize;
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private Color _TextColor = Color.FromArgb(150, 150, 150);
        private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;

        #endregion
        #region Image Designer

        private static PointF ImageLocation(StringFormat SF, SizeF Area, SizeF ImageArea)
        {
            PointF MyPoint = default;
            switch (SF.Alignment)
            {
                case StringAlignment.Center:
                    MyPoint.X = Convert.ToSingle((Area.Width - ImageArea.Width) / 2);
                    break;
                case StringAlignment.Near:
                    MyPoint.X = 2;
                    break;
                case StringAlignment.Far:
                    MyPoint.X = Area.Width - ImageArea.Width - 2;

                    break;
            }

            switch (SF.LineAlignment)
            {
                case StringAlignment.Center:
                    MyPoint.Y = Convert.ToSingle((Area.Height - ImageArea.Height) / 2);
                    break;
                case StringAlignment.Near:
                    MyPoint.Y = 2;
                    break;
                case StringAlignment.Far:
                    MyPoint.Y = Area.Height - ImageArea.Height - 2;
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
                case ContentAlignment.MiddleLeft:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    SF.LineAlignment = StringAlignment.Center;
                    SF.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    SF.LineAlignment = StringAlignment.Near;
                    SF.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    SF.LineAlignment = StringAlignment.Far;
                    SF.Alignment = StringAlignment.Far;
                    break;
            }
            return SF;
        }

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

        public ContentAlignment ImageAlign
        {
            get => _ImageAlign;
            set
            {
                _ImageAlign = value;
                Invalidate();
            }
        }

        public StringAlignment TextAlignment
        {
            get => _TextAlignment;
            set
            {
                _TextAlignment = value;
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => _TextColor;
            set
            {
                _TextColor = value;
                Invalidate();
            }
        }

        public Color InactiveColorA { get; set; } = Color.FromArgb(253, 252, 252);

        public Color InactiveColorB { get; set; } = Color.FromArgb(239, 237, 236);

        public Color PressedColorA { get; set; } = Color.FromArgb(226, 226, 226);

        public Color PressedColorB { get; set; } = Color.FromArgb(237, 237, 237);

        public Color PressedContourColorA { get; set; } = Color.FromArgb(167, 167, 167);

        public Color PressedContourColorB { get; set; } = Color.FromArgb(167, 167, 167);

        public Color BorderColor { get; set; } = Color.FromArgb(180, 180, 180);

        #endregion
        #region EventArgs

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseState = 0;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseState = 1;
            Focus();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            MouseState = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        #endregion

        public DungeonButtonLeft()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            ForeColor = Color.FromArgb(76, 76, 76);
            Size = new(177, 30);
            _TextAlignment = StringAlignment.Center;
            P1 = new(BorderColor);
            // P1 = Border color
            Cursor = Cursors.Hand;
        }

        protected override void OnResize(EventArgs e)
        {

            if (Width > 0 && Height > 0)
            {
                Shape = new();
                R1 = new(0, 0, Width, Height);

                InactiveGB = new(new Rectangle(0, 0, Width, Height), InactiveColorA, InactiveColorB, 90f);
                PressedGB = new(new Rectangle(0, 0, Width, Height), PressedColorA, PressedColorB, 90f);
                PressedContourGB = new(new Rectangle(0, 0, Width, Height), PressedContourColorA, PressedContourColorB, 90f);

                P3 = new(PressedContourGB);
            }

            GraphicsPath MyDrawer = Shape;
            MyDrawer.AddArc(0, 0, 10, 10, 180, 90);
            MyDrawer.AddArc(Width - 11, 0, 10, 10, -90, 90);
            MyDrawer.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            MyDrawer.AddArc(0, Height - 11, 10, 10, 90, 90);
            MyDrawer.CloseAllFigures();
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);

            switch (MouseState)
            {
                case 0:
                    //Inactive
                    G.FillPath(InactiveGB, Shape);
                    // Fill button body with InactiveGB color gradient
                    G.DrawPath(P1, Shape);
                    // Draw button border [InactiveGB]
                    if (Image == null)
                    {
                        G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                        {
                            Alignment = _TextAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                        {
                            Alignment = _TextAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
                case 1:
                    //Pressed
                    G.FillPath(PressedGB, Shape);
                    // Fill button body with PressedGB color gradient
                    G.DrawPath(P3, Shape);
                    // Draw button border [PressedGB]

                    if (Image == null)
                    {
                        G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                        {
                            Alignment = _TextAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        G.DrawImage(_Image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        G.DrawString(Text, Font, new SolidBrush(ForeColor), R1, new StringFormat
                        {
                            Alignment = _TextAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
            }
            base.OnPaint(e);
        }
    }

    #endregion
}