#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotFlatProgressBar

    public class ParrotFlatProgressBar : Control
    {
        public ParrotFlatProgressBar()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.Size = new Size(300, 5);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress bar style")]
        public Style BarStyle
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Style.Material;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress value")]
        public int Value
        {
            get;
            set
            {
                field = value;
                if (field < 0)
                {
                    field = 0;
                }
                if (field > MaxValue)
                {
                    field = MaxValue;
                }
                Invalidate();
            }
        } = 50;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress complete color")]
        public Color CompleteColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(1, 119, 215);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress complete ios back color")]
        public Color CompleteBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(0, 120, 250);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress bar border color")]
        public Color BorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Black;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the progress bar border")]
        public bool ShowBorder
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progress incompleted color")]
        public Color InocmpletedColor
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
        [Description("The progress incompleted ios back color")]
        public Color IncompletedBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(180, 180, 180);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The maximum value")]
        public int MaxValue
        {
            get;
            set
            {
                field = value;
                if (Value > field)
                {
                    Value = field;
                }
                Invalidate();
            }
        } = 100;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The positions")]
        public List<float> Positions
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } =
        [
            0f,
            0.2f,
            0.4f,
            0.6f,
            0.8f,
            1f
        ];

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The colors")]
        public List<Color> Colors
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } =
        [
            Color.FromArgb(76, 217, 100),
            Color.FromArgb(85, 205, 205),
            Color.FromArgb(2, 124, 255),
            Color.FromArgb(130, 75, 180),
            Color.FromArgb(255, 0, 150),
            Color.FromArgb(255, 45, 85)
        ];

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
        } = SmoothingMode.HighQuality;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width + 1, base.Height + 1);
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);

            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;
            bufferedGraphics.Graphics.TextRenderingHint = TextRenderingType;

            bufferedGraphics.Graphics.Clear(BackColor);

            if (BarStyle == Style.Flat)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(InocmpletedColor), 0, 0, base.Width, base.Height);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(CompleteColor), 0, 0, Value * base.Width / MaxValue, base.Height);
            }

            if (BarStyle == Style.IOS)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(IncompletedBackColor), 0, 0, base.Width, base.Height);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(CompleteBackColor), 0, 0, Value * base.Width / MaxValue, base.Height);
            }

            if (BarStyle == Style.Material && Positions.Count == Colors.Count)
            {
                LinearGradientBrush linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), Color.Black, Color.Black, 0f, false)
                {
                    InterpolationColors = new ColorBlend
                    {
                        Positions = Positions.ToArray(),
                        Colors = Colors.ToArray()
                    }
                };

                linearGradientBrush.RotateTransform(1f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, new Rectangle(0, 0, base.Width, base.Height));
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(InocmpletedColor), Value * base.Width / MaxValue, 0, base.Width - (Value * base.Width / MaxValue), base.Height);
            }

            if (ShowBorder)
            {
                bufferedGraphics.Graphics.DrawRectangle(new Pen(BorderColor, 1f), new Rectangle(1, 1, base.Width - 2, base.Height - 2));
            }

            bufferedGraphics.Render(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
        }

        private BufferedGraphics bufferedGraphics;

        public enum Style
        {
            Flat,
            Material,
            IOS
        }
    }

    #endregion
}