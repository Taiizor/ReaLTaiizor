#region Imports

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotGauge

    public class ParrotGauge : Control
    {
        public ParrotGauge()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.Size = new Size(140, 70);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge style")]
        public Style GaugeStyle
        {
            get => gaugeStyle;
            set
            {
                gaugeStyle = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge thickness")]
        public int Thickness
        {
            get => thickness;
            set
            {
                thickness = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge dial thickness")]
        public int DialThickness
        {
            get => dialThickness;
            set
            {
                dialThickness = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge percentage")]
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
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge percentage")]
        public Color DialColor
        {
            get => dialColor;
            set
            {
                dialColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge unfilled color")]
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
        [Description("The gauge filled color")]
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
        [Description("The gauge consumption color")]
        public Color ConsumptionColor
        {
            get => consumptionColor;
            set
            {
                consumptionColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gauge bar color")]
        public List<Color> BarColor
        {
            get => barColor;
            set
            {
                barColor = value;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;
            if (BarColor.Count == 7)
            {
                if (gaugeStyle == Style.Standard)
                {
                    Rectangle rect = new(1 + (thickness / 2), 1 + (thickness / 2), base.Width - 2 - thickness, (base.Height * 2) - thickness);
                    e.Graphics.DrawArc(new Pen(BarColor[0], thickness / 4), rect, 180f, 44f);
                    e.Graphics.DrawArc(new Pen(BarColor[1], thickness / 4 * 2), rect, 226f, 44f);
                    e.Graphics.DrawArc(new Pen(BarColor[2], thickness / 4 * 3), rect, 272f, 44f);
                    e.Graphics.DrawArc(new Pen(BarColor[3], thickness), rect, 318f, 44f);
                    rect.Inflate(0 - thickness, 0 - thickness);
                    e.Graphics.FillPie(new SolidBrush(dialColor), new Rectangle((base.Width / 2) - thickness, base.Height - thickness, thickness * 2, thickness * 2), 0f, 360f);
                    if (percentage <= 5)
                    {
                        e.Graphics.FillPie(new SolidBrush(ConsumptionColor), rect, 180 + (dialThickness * 2) - 2, dialThickness);
                    }
                    else if (percentage >= 95)
                    {
                        e.Graphics.FillPie(new SolidBrush(ConsumptionColor), rect, 360 - (dialThickness * 2), dialThickness);
                    }
                    else
                    {
                        e.Graphics.FillPie(new SolidBrush(dialColor), rect, 180 + (int)(percentage * 1.8) - (dialThickness / 2), dialThickness);
                    }
                }
                if (gaugeStyle == Style.Material)
                {
                    Rectangle rect2 = new(1 + (thickness / 2), 1 + (thickness / 2), base.Width - 2 - thickness, (base.Height * 2) - thickness);
                    e.Graphics.DrawArc(new Pen(new LinearGradientBrush(new Rectangle(0, 0, base.Width, base.Height), BarColor[4], BarColor[5], 1f), thickness), rect2, 180f, (int)(percentage * 1.8) - 1);
                    e.Graphics.DrawArc(new Pen(BarColor[6], thickness), rect2, 180 + (int)(percentage * 1.8) + 1, 180 - (int)(percentage * 1.8) + 5);
                }
                if (gaugeStyle == Style.Flat)
                {
                    Rectangle rect3 = new(1 + (thickness / 2), 1 + (thickness / 2), base.Width - 2 - thickness, (base.Height * 2) - thickness);
                    e.Graphics.DrawArc(new Pen(filledColor, thickness), rect3, 180f, (int)(percentage * 1.8) - 1);
                    e.Graphics.DrawArc(new Pen(unfilledColor, thickness), rect3, 180 + (int)(percentage * 1.8) + 1, 180 - (int)(percentage * 1.8) + 5);
                }
            }
            base.OnPaint(e);
        }

        public Style gaugeStyle = Style.Material;

        public int thickness = 8;

        public int dialThickness = 5;

        public int percentage = 75;

        public Color dialColor = Color.Gray;

        public Color unfilledColor = Color.Gray;

        public Color consumptionColor = Color.Black;

        public List<Color> barColor = new()
        {
            Color.FromArgb(255, 220, 0),
            Color.FromArgb(255, 150, 0),
            Color.FromArgb(250, 90, 0),
            Color.FromArgb(255, 0, 0),
            Color.FromArgb(249, 55, 98),
            Color.FromArgb(0, 162, 250),
            Color.FromArgb(0, 162, 250)
        };

        public Color filledColor = Color.FromArgb(0, 162, 250);

        public enum Style
        {
            Standard,
            Material,
            Flat
        }
    }

    #endregion
}