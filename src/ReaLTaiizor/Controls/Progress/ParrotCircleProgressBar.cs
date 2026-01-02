#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotCircleProgressBar

    public class ParrotCircleProgressBar : Control
    {
        public ParrotCircleProgressBar()
        {
            base.Size = new Size(200, 200);
            UpdateUI.Tick += Animate;
            UpdateUI.Interval = 200 / animationSpeed;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Animate(object sender, EventArgs e)
        {
            if (StartPoint == 360)
            {
                StartPoint = 0;
            }
            StartPoint += animationSpeed;
            Refresh();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string Text { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Unfilled circle color")]
        public Color UnFilledColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(114, 114, 114);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filled circle color")]
        public Color FilledColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(60, 220, 210);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filled colors alpha value")]
        public int FilledColorAlpha
        {
            get;
            set
            {
                field = value;
                if (value > 255)
                {
                    field = 255;
                }
                if (value < 1)
                {
                    field = 1;
                }
                Invalidate();
            }
        } = 130;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Unfilled circle thickness")]
        public int UnfilledThickness
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 24;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Unfilled circle thickness")]
        public int FilledThickness
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 40;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress circle percentage")]
        public int Percentage
        {
            get => percentage;
            set
            {
                percentage = value;
                if (value < 0)
                {
                    percentage = 0;
                }
                if (value > 100)
                {
                    percentage = 100;
                }
                OnPercentageChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The animation speed")]
        public int AnimationSpeed
        {
            get => animationSpeed;
            set
            {
                animationSpeed = value;
                if (value < 1)
                {
                    animationSpeed = 1;
                }
                if (animationSpeed > 10)
                {
                    animationSpeed = 10;
                }
                UpdateUI.Interval = 200 / animationSpeed;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the control animated")]
        public bool IsAnimated
        {
            get => isAnimated;
            set
            {
                isAnimated = value;
                if (value)
                {
                    UpdateUI.Enabled = true;
                }
                else
                {
                    UpdateUI.Enabled = false;
                }
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text size")]
        public int TextSize
        {
            get => textSize;
            set
            {
                textSize = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Text color")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show text")]
        public bool ShowText
        {
            get => showText;
            set
            {
                showText = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.AntiAlias;

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

        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

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
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

        public event EventHandler PercentageChanged;

        protected virtual void OnPercentageChanged()
        {
            PercentageChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width + 1, base.Height + 1);
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), new Rectangle(0, 0, 1, 1));
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);
            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;
            bufferedGraphics.Graphics.TextRenderingHint = TextRenderingType;

            if (BackgroundImage == null)
            {
                bufferedGraphics.Graphics.Clear(BackColor);
            }
            else
            {
                bufferedGraphics.Graphics.DrawImage(BackgroundImage, 0, 0);
            }

            Rectangle rect = new((FilledThickness / 2) + 1, (FilledThickness / 2) + 1, base.Width - FilledThickness - 2, base.Height - FilledThickness - 2);
            bufferedGraphics.Graphics.DrawArc(new Pen(UnFilledColor, UnfilledThickness), rect, StartPoint, 360f);
            bufferedGraphics.Graphics.DrawArc(new Pen(Color.FromArgb(FilledColorAlpha, FilledColor.R, FilledColor.G, FilledColor.B), FilledThickness), rect, StartPoint, (int)(Percentage * 3.6));

            if (ShowText)
            {
                Rectangle r = new(0, 0, base.Width, base.Height);
                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };
                bufferedGraphics.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                bufferedGraphics.Graphics.DrawString(Percentage.ToString() + "%", new Font("Ariel", textSize), new SolidBrush(textColor), r, stringFormat);
            }

            bufferedGraphics.Render(e.Graphics);
            base.OnPaint(e);
        }

        private BufferedGraphics bufferedGraphics;

        private readonly Timer UpdateUI = new();

        private int StartPoint = 270;
        public int percentage = 50;

        public int animationSpeed = 5;

        public bool isAnimated;

        public int textSize = 25;

        public Color textColor = Color.Gray;

        public bool showText = true;
    }

    #endregion
}