#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSuperButton

    public class ParrotSuperButton : Control
    {
        public ParrotSuperButton()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            Cursor = Cursors.Hand;
            CurrentBackColor = backColor;
            CurrentForeColor = foreColor;
            base.Size = new Size(100, 40);
            NormalRegion = base.Region;
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ButtonImage = new Bitmap(base.Height - 2, base.Height - 2);
            Graphics graphics = Graphics.FromImage(ButtonImage);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawArc(new Pen(Color.White, 2f), new Rectangle(1, 1, buttonImage.Width - 3, buttonImage.Height - 3), 0f, 360f);
            graphics.DrawLine(new Pen(Color.White, 2f), buttonImage.Width / 3, buttonImage.Height / 4, buttonImage.Width / 3 * 2, buttonImage.Height / 2);
            graphics.DrawLine(new Pen(Color.White, 2f), buttonImage.Width / 3, buttonImage.Height / 4 * 3, buttonImage.Width / 3 * 2, buttonImage.Height / 2);
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
        [Description("Is the SuperButton selected")]
        public bool SuperSelected
        {
            get => superSelected;
            set
            {
                superSelected = value;
                if (!superSelected)
                {
                    CurrentForeColor = foreColor;
                    CurrentBackColor = backColor;
                }
                else
                {
                    CurrentForeColor = selectedForecolor;
                    CurrentBackColor = selectedBackcolor;
                }
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
        [Description("The text color of the button when selected")]
        public Color SelectedTextColor
        {
            get => selectedForecolor;
            set
            {
                selectedForecolor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button when selected")]
        public Color SelectedBackColor
        {
            get => selectedBackcolor;
            set
            {
                selectedBackcolor = value;
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
        [Description("The smoothing mode of the graphics")]
        public SmoothingMode ButtonSmoothing
        {
            get => buttonSmoothing;
            set
            {
                buttonSmoothing = value;
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

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
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
            if (!superSelected)
            {
                CurrentForeColor = foreColor;
                CurrentBackColor = backColor;
            }
            else
            {
                CurrentForeColor = selectedForecolor;
                CurrentBackColor = selectedBackcolor;
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CurrentForeColor = selectedForecolor;
            CurrentBackColor = selectedBackcolor;
            SuperSelected = true;
            Invalidate();
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (superSelected)
            {
                foreach (object obj in base.Parent.Controls)
                {
                    Control control = (Control)obj;
                    if (control is ParrotSuperButton button && control.Name != base.Name)
                    {
                        button.SuperSelected = false;
                    }
                }
            }

            e.Graphics.SmoothingMode = buttonSmoothing;

            e.Graphics.FillRectangle(new SolidBrush(CurrentBackColor), 0, 0, base.Width, base.Height);

            Rectangle r = new(0, 0, base.Width, base.Height);

            if (buttonImage != null)
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
            }

            StringFormat stringFormat = new()
            {
                LineAlignment = verticlAlignment,
                Alignment = horizontalAlignment
            };

            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            e.Graphics.DrawString(buttonText, Font, new SolidBrush(Color.White), r, stringFormat);

            if (buttonStyle == Style.Elliptical)
            {
                base.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, base.Width, base.Height, base.Width, base.Height));
            }
            else if (buttonStyle == Style.RoundedEdges)
            {
                base.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, base.Width, base.Height, cornerRadius, cornerRadius));
            }
            else
            {
                base.Region = NormalRegion;
            }

            base.OnPaint(e);
        }

        private Color CurrentBackColor;

        private Color CurrentForeColor;

        private readonly Region NormalRegion;

        private Style buttonStyle = Style.RoundedEdges;

        private Color foreColor = Color.White;

        private Color backColor = Color.FromArgb(24, 202, 142);

        private bool superSelected;

        private Color hoverForeColor = Color.White;

        private Color hoverBackgroundColor = Color.FromArgb(102, 217, 174);

        private Color selectedForecolor = Color.White;

        private Color selectedBackcolor = Color.LimeGreen;

        private int cornerRadius = 5;

        private string buttonText = "SuperButton";

        private StringAlignment horizontalAlignment = StringAlignment.Center;

        private StringAlignment verticlAlignment = StringAlignment.Center;

        private Image buttonImage;

        private SmoothingMode buttonSmoothing = SmoothingMode.HighSpeed;

        public ImgPosition imagePosition;

        public enum Style
        {
            Flat,
            Elliptical,
            RoundedEdges
        }

        public enum ImgPosition
        {
            Left,
            Right
        }
    }

    #endregion
}