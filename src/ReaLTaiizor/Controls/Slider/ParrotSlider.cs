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
            get => barThickness;
            set
            {
                barThickness = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The increment incresed or decreased when not clicking in the handle")]
        public int BigStepIncrement
        {
            get => bigStepIncrement;
            set
            {
                bigStepIncrement = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The default percentage")]
        public int Percentage
        {
            get => percentage;
            set
            {
                percentage = value;
                OnScroll();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The max percentage")]
        public int Max
        {
            get => max;
            set
            {
                max = value;
                OnScroll();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled color")]
        public Color FilledColor
        {
            get => filledColor;
            set
            {
                filledColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled color")]
        public Color UnfilledColor
        {
            get => unfilledColor;
            set
            {
                unfilledColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The knob color")]
        public Color KnobColor
        {
            get => knobColor;
            set
            {
                knobColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The knob image")]
        public Image KnobImage
        {
            get => knobImage;
            set
            {
                knobImage = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Allow instantly jumping to the position clicked")]
        public bool QuickHopping
        {
            get => quickHopping;
            set
            {
                quickHopping = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The slider style")]
        public Style SliderStyle
        {
            get => sliderStyle;
            set
            {
                sliderStyle = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The positions")]
        public List<float> Positions
        {
            get => _Positions;
            set
            {
                _Positions = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The colors")]
        public List<Color> Colors
        {
            get => _Colors;
            set
            {
                _Colors = value;
                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
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

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get => _CompositingQualityType;
            set
            {
                _CompositingQualityType = value;
                Invalidate();
            }
        }

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBilinear;
        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get => _InterpolationType;
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        public event EventHandler Scroll;

        protected virtual void OnScroll()
        {
            Scroll?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (quickHopping)
            {
                Percentage = (int)Math.Round(max * e.X / (double)base.Width);
                onHandle = true;
                return;
            }

            int num = Percentage * base.Width / max;

            if (e.X > num - (base.Height / 2) && e.X < num + (base.Height / 2))
            {
                onHandle = true;
                return;
            }

            if (e.X < num - (base.Height / 2))
            {
                Percentage -= bigStepIncrement;
                if (Percentage < 0)
                {
                    Percentage = 0;
                }
                Invalidate();
                return;
            }

            if (e.X > num + (base.Height / 2))
            {
                Percentage += bigStepIncrement;
                if (Percentage > max)
                {
                    Percentage = max;
                }
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (onHandle)
            {
                Percentage = (int)Math.Round(max * e.X / (double)base.Width);
                if (Percentage < 0)
                {
                    Percentage = 0;
                }
                if (Percentage > max)
                {
                    Percentage = max;
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

            int num = Percentage * base.Width / max;
            int num2 = Percentage * barRectangle.Width / max;

            bufferedGraphics.Graphics.Clear(BackColor);

            if (sliderStyle == Style.Flat)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(unfilledColor), (base.Height / 2) + 1, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(filledColor), 1 + (base.Height / 2), (base.Height / 2) - (barThickness / 2), num2 - 2, barThickness);

                if (knobImage == null)
                {
                    bufferedGraphics.Graphics.FillEllipse(new SolidBrush(knobColor), num2 + 1, 1, base.Height - 2, base.Height - 2);
                }
                else
                {
                    bufferedGraphics.Graphics.DrawImage(new Bitmap(knobImage, base.Height - 2, base.Height - 2), num2 + 1, 1);
                }
            }

            if (sliderStyle == Style.MacOS)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(185, 185, 185)), (base.Height / 2) + 1, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, 150, 230)), 1 + (base.Height / 2), (base.Height / 2) - (barThickness / 2), num2 - 2, barThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(190, 200, 200)), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            if (sliderStyle == Style.Windows10)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(146, 147, 148)), (base.Height / 2) + 1, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(65, 155, 225)), 1 + (base.Height / 2), (base.Height / 2) - (barThickness / 2), num2 - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), 3, (base.Height / 2) - 2, base.Height - 6);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), 0, (base.Height / 2) - 2, 4);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 120, 215)), num2 + 1 + (base.Height / 3), base.Height - 5, (base.Height / 2) - 2, 4);
            }

            if (sliderStyle == Style.Android)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 100, 100)), (base.Height / 2) + 1, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 180, 230)), 1 + (base.Height / 2), (base.Height / 2) - (barThickness / 2), num2 - 2, barThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(50, 180, 230)), num2 + 1 + (barThickness / 3 * 5), (base.Height / 2) - (barThickness / 3 * 4), barThickness * 2, barThickness * 2);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(100, 50, 180, 230)), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(50, 180, 230), 2f), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            if (sliderStyle == Style.Material && Positions.Count == Colors.Count)
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

                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, (base.Height / 2) + 1, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2, barThickness);
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 1 + (base.Height / 2) + num2, (base.Height / 2) - (barThickness / 2), base.Width - base.Height - 2 - num2, barThickness);
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(Color.White), num2 + 1, 1, base.Height - 2, base.Height - 2);
                bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(200, 200, 200)), num2 + 1, 1, base.Height - 2, base.Height - 2);
            }

            bufferedGraphics.Render(e.Graphics);
        }

        private Rectangle barRectangle;

        private BufferedGraphics bufferedGraphics;

        private bool onHandle;

        private int barThickness = 4;

        private int bigStepIncrement = 10;

        private int max = 100;

        private int percentage = 50;

        private Color filledColor = Color.FromArgb(1, 119, 215);

        private Color unfilledColor = Color.FromArgb(26, 169, 219);

        private Color knobColor = Color.Gray;

        private Image knobImage;

        private bool quickHopping;

        private Style sliderStyle = Style.Windows10;

        private List<float> _Positions = new()
        {
            0f,
            0.2f,
            0.4f,
            0.6f,
            0.8f,
            1f
        };

        private List<Color> _Colors = new()
        {
            Color.FromArgb(76, 217, 100),
            Color.FromArgb(85, 205, 205),
            Color.FromArgb(2, 124, 255),
            Color.FromArgb(130, 75, 180),
            Color.FromArgb(255, 0, 150),
            Color.FromArgb(255, 45, 85)
        };

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