#region Imports

using ReaLTaiizor.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotButton

    public class ParrotButton : Control
    {
        public ParrotButton()
        {
            base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            base.Size = new Size(200, 50);
            CurrentBackColor = backColor;
            CurrentForeColor = foreColor;
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The button style")]
        public Style ButtonStyle
        {
            get => buttonStyle;
            set
            {
                buttonStyle = value;
                if (buttonStyle == Style.Dark)
                {
                    CurrentBackColor = Color.FromArgb(65, 70, 75);
                    CurrentForeColor = Color.FromArgb(195, 200, 185);
                }
                if (buttonStyle == Style.MacOS)
                {
                    CurrentBackColor = Color.White;
                    CurrentForeColor = Color.Black;
                }
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button")]
        public Color TextColor
        {
            get => foreColor;
            set
            {
                foreColor = value;
                CurrentForeColor = foreColor;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button")]
        public Color BackgroundColor
        {
            get => backColor;
            set
            {
                backColor = value;
                CurrentBackColor = backColor;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button while the mouse is over it")]
        public Color HoverTextColor
        {
            get => hoverForeColor;
            set
            {
                hoverForeColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button while the mouse is over it")]
        public Color HoverBackgroundColor
        {
            get => hoverBackgroundColor;
            set
            {
                hoverBackgroundColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button when clicked")]
        public Color ClickTextColor
        {
            get => clickForecolor;
            set
            {
                clickForecolor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button when clicked")]
        public Color ClickBackColor
        {
            get => clickBackcolor;
            set
            {
                clickBackcolor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The corner radius if rounded edges")]
        public int CornerRadius
        {
            get => cornerRadius;
            set
            {
                cornerRadius = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the button")]
        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text horizontal alignment")]
        public StringAlignment Horizontal_Alignment
        {
            get => horizontalAlignment;
            set
            {
                horizontalAlignment = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text vertical alignment")]
        public StringAlignment Vertical_Alignment
        {
            get => verticlAlignment;
            set
            {
                verticlAlignment = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The buttons image")]
        public Image ButtonImage
        {
            get => buttonImage;
            set
            {
                buttonImage = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Button image position")]
        public ImgPosition ImagePosition
        {
            get => imagePosition;
            set
            {
                imagePosition = value;
                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            CurrentForeColor = hoverForeColor;
            CurrentBackColor = hoverBackgroundColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            CurrentForeColor = foreColor;
            CurrentBackColor = backColor;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CurrentForeColor = clickForecolor;
            CurrentBackColor = clickBackcolor;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            CurrentForeColor = foreColor;
            CurrentBackColor = backColor;
            Invalidate();
        }

        private void FillRoundedRectangle(Graphics Gfx, Color ButtonColor, int CornerRadius)
        {
            using GraphicsPath graphicsPath = new();
            graphicsPath.AddArc(base.Width - (CornerRadius * 2) - 2, 0, CornerRadius * 2, CornerRadius * 2, 270f, 90f);
            graphicsPath.AddArc(base.Width - (CornerRadius * 2) - 2, base.Height - (CornerRadius * 2), CornerRadius * 2, (CornerRadius * 2) - 2, 0f, 90f);
            graphicsPath.AddArc(0, base.Height - (CornerRadius * 2) - 2, (CornerRadius * 2) - 2, CornerRadius * 2, 90f, 90f);
            graphicsPath.AddArc(0, 0, CornerRadius * 2, CornerRadius * 2, 180f, 90f);
            graphicsPath.CloseFigure();
            Gfx.FillPath(new SolidBrush(ButtonColor), graphicsPath);
        }

        private void DrawRoundedRectangle(Graphics Gfx, Color borderColor, int CornerRadius, int borderThickness)
        {
            using GraphicsPath graphicsPath = new();
            graphicsPath.AddArc(base.Width - (CornerRadius * 2) - 2, 0, CornerRadius * 2, CornerRadius * 2, 270f, 90f);
            graphicsPath.AddArc(base.Width - (CornerRadius * 2) - 2, base.Height - (CornerRadius * 2), CornerRadius * 2, (CornerRadius * 2) - 2, 0f, 90f);
            graphicsPath.AddArc(0, base.Height - (CornerRadius * 2) - 2, (CornerRadius * 2) - 2, CornerRadius * 2, 90f, 90f);
            graphicsPath.AddArc(0, 0, CornerRadius * 2, CornerRadius * 2, 180f, 90f);
            graphicsPath.CloseFigure();
            Gfx.DrawPath(new Pen(borderColor, borderThickness), graphicsPath);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            if (buttonStyle == Style.MaterialEllipse)
            {
                e.Graphics.FillPie(new SolidBrush(CurrentBackColor), 0, 0, base.Width - 1, base.Height - 1, 0, 360);
            }
            if (buttonStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(CurrentBackColor), 0, 0, base.Width, base.Height);
            }
            if (buttonStyle == Style.MaterialRounded)
            {
                if ((Height / 2) - 1 != 0)
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, cornerRadius);
                }
                else
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, cornerRadius);
                }
            }
            if (buttonStyle == Style.Invert)
            {
                if ((Height / 2) - 1 != 0)
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, (Height / 2) - 1);
                }
                else
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, Height / 2);
                }
                if ((Height / 2) - 1 != 0)
                {
                    DrawRoundedRectangle(e.Graphics, CurrentForeColor, (Height / 2) - 1, 2);
                }
                else
                {
                    DrawRoundedRectangle(e.Graphics, CurrentForeColor, Height / 2, 2);
                }
                hoverBackgroundColor = foreColor;
                hoverForeColor = backColor;
                clickBackcolor = foreColor;
                clickForecolor = foreColor;
            }
            if (buttonStyle == Style.Dark)
            {
                backColor = Color.FromArgb(65, 70, 75);
                foreColor = Color.FromArgb(195, 200, 185);
                hoverBackgroundColor = Color.FromArgb(75, 80, 90);
                hoverForeColor = Color.FromArgb(235, 235, 215);
                clickBackcolor = Color.FromArgb(65, 75, 80);
                clickForecolor = Color.FromArgb(125, 130, 140);
                if ((Height / 2) - 1 != 0)
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, (Height / 2) - 1);
                }
                else
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, Height / 2);
                }
                if ((Height / 2) - 1 != 0)
                {
                    DrawRoundedRectangle(e.Graphics, Color.Black, (Height / 2) - 1, 1);
                }
                else
                {
                    DrawRoundedRectangle(e.Graphics, Color.Black, Height / 2, 1);
                }
            }
            if (buttonStyle == Style.MacOS)
            {
                backColor = Color.White;
                foreColor = Color.Black;
                Font = new Font("Microsoft Sans Serif", 14f);
                if ((Height / 2) - 1 != 0)
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, 8);
                }
                else
                {
                    FillRoundedRectangle(e.Graphics, CurrentBackColor, 8);
                }
                if ((Height / 2) - 1 != 0)
                {
                    DrawRoundedRectangle(e.Graphics, Color.FromArgb(163, 163, 163), 8, 2);
                }
                else
                {
                    DrawRoundedRectangle(e.Graphics, Color.FromArgb(163, 163, 163), 8, 2);
                }
            }
            Rectangle r = new(0, 0, base.Width, base.Height);
            if (buttonStyle != Style.Dark && buttonStyle != Style.MaterialEllipse && buttonStyle != Style.MacOS && buttonStyle != Style.Invert && buttonImage != null)
            {
                if (imagePosition == ImgPosition.Left)
                {
                    r = new Rectangle(base.Height, 0, base.Width - base.Height, base.Height);
                    e.Graphics.DrawImage(new Bitmap(buttonImage, base.Height - 2, base.Height - 2), 1, 1);
                }
                if (imagePosition == ImgPosition.Right)
                {
                    r = new Rectangle(0, 0, base.Width - base.Height, base.Height);
                    e.Graphics.DrawImage(new Bitmap(buttonImage, base.Height - 2, base.Height - 2), base.Width - base.Height, 1);
                }
                if (imagePosition == ImgPosition.Center)
                {
                    e.Graphics.DrawImage(new Bitmap(buttonImage, base.Height - 2, base.Height - 2), (base.Width / 2) - (Height / 2), 1);
                }
            }
            if (imagePosition != ImgPosition.Center || buttonImage == null)
            {
                StringFormat stringFormat = new()
                {
                    LineAlignment = verticlAlignment,
                    Alignment = horizontalAlignment
                };
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawString(buttonText, Font, new SolidBrush(CurrentForeColor), r, stringFormat);
            }
        }

        private Color CurrentBackColor;

        private Color CurrentForeColor;

        private Style buttonStyle = Style.MaterialRounded;

        private Color foreColor = Color.DodgerBlue;

        private Color backColor = Color.FromArgb(255, 255, 255);

        private Color hoverForeColor = Color.DodgerBlue;

        private Color hoverBackgroundColor = Color.FromArgb(225, 225, 225);

        private Color clickForecolor = Color.DodgerBlue;

        private Color clickBackcolor = Color.FromArgb(195, 195, 195);

        private int cornerRadius = 5;

        private string buttonText = "Button";

        private StringAlignment horizontalAlignment = StringAlignment.Center;

        private StringAlignment verticlAlignment = StringAlignment.Center;

        private Image buttonImage = Resources.mini_button;

        public ImgPosition imagePosition;

        public enum ImgPosition
        {
            Left,
            Right,
            Center
        }

        public enum Style
        {
            Material,
            Dark,
            MacOS,
            Invert,
            MaterialRounded,
            MaterialEllipse
        }
    }

    #endregion
}