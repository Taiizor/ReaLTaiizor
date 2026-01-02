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
            CurrentBackColor = BackgroundColor;
            CurrentForeColor = TextColor;
            base.Size = new Size(100, 40);
            NormalRegion = base.Region;
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ButtonImage = new Bitmap(base.Height - 2, base.Height - 2);
            Graphics graphics = Graphics.FromImage(ButtonImage);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawArc(new Pen(Color.White, 2f), new Rectangle(1, 1, ButtonImage.Width - 3, ButtonImage.Height - 3), 0f, 360f);
            graphics.DrawLine(new Pen(Color.White, 2f), ButtonImage.Width / 3, ButtonImage.Height / 4, ButtonImage.Width / 3 * 2, ButtonImage.Height / 2);
            graphics.DrawLine(new Pen(Color.White, 2f), ButtonImage.Width / 3, ButtonImage.Height / 4 * 3, ButtonImage.Width / 3 * 2, ButtonImage.Height / 2);
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
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Style.RoundedEdges;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button")]
        public Color TextColor
        {
            get;
            set
            {
                field = value;
                CurrentForeColor = field;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button")]
        public Color BackgroundColor
        {
            get;
            set
            {
                field = value;
                CurrentBackColor = field;
                Invalidate();
            }
        } = Color.FromArgb(24, 202, 142);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the SuperButton selected")]
        public bool SuperSelected
        {
            get;
            set
            {
                field = value;
                if (!field)
                {
                    CurrentForeColor = TextColor;
                    CurrentBackColor = BackgroundColor;
                }
                else
                {
                    CurrentForeColor = SelectedTextColor;
                    CurrentBackColor = SelectedBackColor;
                }
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button while the mouse is over it")]
        public Color HoverTextColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button while the mouse is over it")]
        public Color HoverBackgroundColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(102, 217, 174);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text color of the button when selected")]
        public Color SelectedTextColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The background color of the button when selected")]
        public Color SelectedBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LimeGreen;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The corner radius if rounded edges")]
        public int CornerRadius
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 5;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the button")]
        public string ButtonText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "SuperButton";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text horizontal alignment")]
        public StringAlignment Horizontal_Alignment
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = StringAlignment.Center;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text vertical alignment")]
        public StringAlignment Vertical_Alignment
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = StringAlignment.Center;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The buttons image")]
        public Image ButtonImage
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The smoothing mode of the graphics")]
        public SmoothingMode ButtonSmoothing
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighSpeed;

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

        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = PixelOffsetMode.HighQuality;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            CurrentForeColor = HoverTextColor;
            CurrentBackColor = HoverBackgroundColor;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!SuperSelected)
            {
                CurrentForeColor = TextColor;
                CurrentBackColor = BackgroundColor;
            }
            else
            {
                CurrentForeColor = SelectedTextColor;
                CurrentBackColor = SelectedBackColor;
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CurrentForeColor = SelectedTextColor;
            CurrentBackColor = SelectedBackColor;
            SuperSelected = true;
            Invalidate();
        }

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        protected override void OnPaint(PaintEventArgs e)
        {
            if (SuperSelected)
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

            e.Graphics.SmoothingMode = ButtonSmoothing;

            e.Graphics.FillRectangle(new SolidBrush(CurrentBackColor), 0, 0, base.Width, base.Height);

            Rectangle r = new(0, 0, base.Width, base.Height);

            if (ButtonImage != null)
            {
                if (imagePosition == ImgPosition.Left)
                {
                    r = new Rectangle(base.Height, 0, base.Width - base.Height, base.Height);
                    e.Graphics.DrawImage(new Bitmap(ButtonImage, base.Height - 2, base.Height - 2), 1, 1);
                }
                if (imagePosition == ImgPosition.Right)
                {
                    r = new Rectangle(0, 0, base.Width - base.Height, base.Height);
                    e.Graphics.DrawImage(new Bitmap(ButtonImage, base.Height - 2, base.Height - 2), base.Width - base.Height, 1);
                }
            }

            StringFormat stringFormat = new()
            {
                LineAlignment = Vertical_Alignment,
                Alignment = Horizontal_Alignment
            };

            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            e.Graphics.DrawString(ButtonText, Font, new SolidBrush(Color.White), r, stringFormat);

            if (ButtonStyle == Style.Elliptical)
            {
                base.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, base.Width, base.Height, base.Width, base.Height));
            }
            else if (ButtonStyle == Style.RoundedEdges)
            {
                base.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, base.Width, base.Height, CornerRadius, CornerRadius));
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