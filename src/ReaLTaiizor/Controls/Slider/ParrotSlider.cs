#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSlider

    public class ParrotSlider : Control
    {
        public ParrotSlider()
        {
            base.Size = new Size(250, 20);
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            barRectangle = new((base.Height / 2) + 1, 1, base.Width - base.Height, base.Height - 1);
            Cursor = Cursors.Hand;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bar thickness")]
        public int BarThickness
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 4;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The increment incresed or decreased when not clicking in the handle")]
        public int BigStepIncrement
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 10;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The default percentage")]
        public int Percentage
        {
            get;
            set
            {
                field = value;
                OnScroll();
                Invalidate();
            }
        } = 50;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The max percentage")]
        public int Max
        {
            get;
            set
            {
                field = value;
                OnScroll();
                Invalidate();
            }
        } = 100;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled color")]
        public Color FilledColor
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
        [Description("The unfilled color")]
        public Color UnfilledColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(26, 169, 219);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The knob color")]
        public Color KnobColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The knob image")]
        public Image KnobImage
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
        [Description("Allow instantly jumping to the position clicked")]
        public bool QuickHopping
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
        [Description("The slider style")]
        public Style SliderStyle
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Style.Windows10;

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
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

        public event EventHandler Scroll;

        protected virtual void OnScroll()
        {
            Scroll?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (QuickHopping)
            {
                Percentage = (int)Math.Round(Max * e.X / (double)base.Width);
                onHandle = true;
                return;
            }

            int num = Percentage * base.Width / Max;

            if (e.X > num - (base.Height / 2) && e.X < num + (base.Height / 2))
            {
                onHandle = true;
                return;
            }

            if (e.X < num - (base.Height / 2))
            {
                Percentage -= BigStepIncrement;
                if (Percentage < 0)
                {
                    Percentage = 0;
                }
                Invalidate();
                return;
            }

            if (e.X > num + (base.Height / 2))
            {
                Percentage += BigStepIncrement;
                if (Percentage > Max)
                {
                    Percentage = Max;
                }
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (onHandle)
            {
                Percentage = (int)Math.Round(Max * e.X / (double)base.Width);
                if (Percentage < 0)
                {
                    Percentage = 0;
                }
                if (Percentage > Max)
                {
                    Percentage = Max;
                }
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            onHandle = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width + 1, base.Height + 1);

            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);

            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;

            int num = Percentage * base.Width / Max;
            int num2 = Percentage * barRectangle.Width / Max;

            bufferedGraphics.Graphics.Clear(BackColor);

            if (SliderStyle == Style.Flat)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(UnfilledColor), (base.Height / 2) + 1, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(FilledColor), 1 + (base.Height / 2), (base.Height / 2) - (BarThickness / 2), num2 - 2, BarThickness);

                if (KnobImage == null)
                {
                    bufferedGraphics.Graphics.FillEllipse(new SolidBrush(KnobColor), num2 + 1, 1, base.Height - 2, base.Height - 2);
                }
                else
                {
                    bufferedGraphics.Graphics.DrawImage(new Bitmap(KnobImage, base.Height - 2, base.Height - 2), num2 + 1, 1);
                }
            }

            if (SliderStyle == Style.MacOS)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(185, 185, 185)), (base.Height / 2) + 1, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, 150, 230)), 1 + (base.Height / 2), (base.Height / 2) - (BarThickness / 2), num2 - 2, BarThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(190, 200, 200)), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            if (SliderStyle == Style.Windows10)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(146, 147, 148)), (base.Height / 2) + 1, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(65, 155, 225)), 1 + (base.Height / 2), (base.Height / 2) - (BarThickness / 2), num2 - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), 3, (base.Height / 2) - 2, base.Height - 6);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), 0, (base.Height / 2) - 2, 4);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), base.Height - 5, (base.Height / 2) - 2, 4);
            }

            if (SliderStyle == Style.Android)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 100, 100)), (base.Height / 2) + 1, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 180, 230)), 1 + (base.Height / 2), (base.Height / 2) - (BarThickness / 2), num2 - 2, BarThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(50, 180, 230)), num2 + 1 + (BarThickness / 3 * 5), (base.Height / 2) - (BarThickness / 3 * 4), BarThickness * 2, BarThickness * 2);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(100, 50, 180, 230)), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(50, 180, 230), 2f), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            if (SliderStyle == Style.Material && Positions.Count == Colors.Count)
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

                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, (base.Height / 2) + 1, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2, BarThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 1 + (base.Height / 2) + num2, (base.Height / 2) - (BarThickness / 2), base.Width - base.Height - 2 - num2, BarThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(200, 200, 200)), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            bufferedGraphics.Render(e.Graphics);
        }

        private Rectangle barRectangle;

        private BufferedGraphics bufferedGraphics;

        private bool onHandle;

        public enum Style
        {
            Flat,
            Material,
            MacOS,
            Android,
            Windows10
        }
    }

    #endregion
}