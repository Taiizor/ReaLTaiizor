#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Button

    public class Button : Control
    {
        #region Variables

        private int MouseState;
        private GraphicsPath Shape;
        private LinearGradientBrush InactiveGB;
        private LinearGradientBrush PressedGB;
        private LinearGradientBrush EnteredGB;
        private Rectangle R1;
        private Image _Image;
        private Size _ImageSize;
        private StringAlignment _TextAlignment = StringAlignment.Center;
        private Color _InactiveColor = Color.FromArgb(32, 34, 37);
        private Color _PressedColor = Color.FromArgb(165, 37, 37);
        private Color _EnteredColor = Color.FromArgb(32, 34, 37);
        private Color _BorderColor = Color.FromArgb(32, 34, 37);
        private Color _EnteredBorderColor = Color.FromArgb(165, 37, 37);
        private Color _PressedBorderColor = Color.FromArgb(165, 37, 37);
        private Color _TextColor; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.
        private ContentAlignment _ImageAlign = ContentAlignment.MiddleLeft;

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
                    MyPoint.Y = (float)((Area.Height - ImageArea.Height) / 2);
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

        private StringFormat GetStringFormat(ContentAlignment _ContentAlignment)
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

        public Color InactiveColor
        {
            get => _InactiveColor;
            set
            {
                _InactiveColor = value;
                Invalidate();
            }
        }

        public Color PressedColor
        {
            get => _PressedColor;
            set
            {
                _PressedColor = value;
                Invalidate();
            }
        }

        public Color EnteredColor
        {
            get => _EnteredColor;
            set
            {
                _EnteredColor = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        public Color EnteredBorderColor
        {
            get => _EnteredBorderColor;
            set
            {
                _EnteredBorderColor = value;
                Invalidate();
            }
        }

        public Color PressedBorderColor
        {
            get => _PressedBorderColor;
            set
            {
                _PressedBorderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region EventArgs

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseState = 2;
            Invalidate();
            base.OnMouseUp(e);
        }

        /*protected override void OnMouseMove(MouseEventArgs e)
        {
            if (Enabled == true)
                Cursor = Cursors.Hand;
            else
                Cursor = Cursors.No;
            base.OnMouseMove(e);
        }*/

        protected override void OnMouseEnter(EventArgs e)
        {
            MouseState = 2;
            Invalidate();
            base.OnMouseEnter(e);
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

        public Button()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            Font = new("Microsoft Sans Serif", 12);
            ForeColor = Color.FromArgb(255, 255, 255);
            Size = new(120, 40);
            _TextAlignment = StringAlignment.Center;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Width > 0 && Height > 0)
            {
                Shape = new();
                R1 = new(0, 0, Width, Height);

                InactiveGB = new(new Rectangle(0, 0, Width, Height), InactiveColor, InactiveColor, 90.0F);
                PressedGB = new(new Rectangle(0, 0, Width, Height), PressedColor, PressedColor, 90.0F);
                EnteredGB = new(new Rectangle(0, 0, Width, Height), EnteredColor, EnteredColor, 90.0F);
            }

            Shape.AddArc(0, 0, 10, 10, 180, 90);
            Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            Shape.CloseAllFigures();
            Invalidate();
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
                    G.DrawPath(new(BorderColor), Shape);
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
                    G.DrawPath(new(PressedBorderColor), Shape);
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
                case 2:
                    //Entered
                    G.FillPath(EnteredGB, Shape);
                    // Fill button body with EnteredGB color gradient
                    G.DrawPath(new(EnteredBorderColor), Shape);
                    // Draw button border [EnteredGB]

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