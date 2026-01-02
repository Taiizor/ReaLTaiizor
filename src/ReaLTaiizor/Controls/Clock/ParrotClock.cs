#region Imports

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotClock

    public class ParrotClock : Control
    {
        public ParrotClock()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Size = new Size(120, 130);
            Font = new Font("Impact", 15f);
            RefreshUI.Interval = 1000;
            RefreshUI.Tick += RefreshUI_Tick;
            RefreshUI.Enabled = true;
        }

        private void RefreshUI_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The circle thickness")]
        public int CircleThickness
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 6;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled hour circle color")]
        public Color UnfilledHourColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(75, 70, 85);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled hour circle color")]
        public Color FilledHourColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(105, 190, 155);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled minute circle color")]
        public Color UnfilledMinuteColor
        {
            get => unfilledMinuteColor;
            set
            {
                unfilledMinuteColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled minute circle color")]
        public Color FilledMinuteColor
        {
            get => unfilledMinuteColor;
            set
            {
                unfilledMinuteColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled second circle color")]
        public Color UnfilledSecondColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(60, 60, 70);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled second circle color")]
        public Color FilledSecondColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DarkOrchid;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The hexagon color")]
        public Color HexagonColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(60, 60, 70);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The time color")]
        public Color TimeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(220, 220, 220);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the seconds circle")]
        public bool ShowSecondsCircle
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
        [Description("Show the minutes circle")]
        public bool ShowMinutesCircle
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
        [Description("Show the hexagon")]
        public bool ShowHexagon
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
        [Description("Show AM/PM")]
        public bool ShowAmPm
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
        [Description("Time (AM) format")]
        public string TimeAMFormat
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "hh:mm";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Time (PM) format")]
        public string TimePMFormat
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "HH:mm";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The time display format")]
        public HourFormat DisplayFormat
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HourFormat.TwentyFourHour;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(Width + 1, Height + 1);

            bufferedGraphics = bufferedGraphicsContext.Allocate(CreateGraphics(), ClientRectangle);

            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;
            bufferedGraphics.Graphics.TextRenderingHint = TextRenderingType;

            bufferedGraphics.Graphics.Clear(BackColor);

            if (ShowHexagon)
            {
                List<Point> list =
                [
                    new Point(0, Height / 4),
                    new Point(Width / 2, 0),
                    new Point(Width, Height / 4),
                    new Point(Width, Height / 4 * 3),
                    new Point(Width / 2, Height),
                    new Point(0, Height / 4 * 3),
                    new Point(0, Height / 4)
                ];
                bufferedGraphics.Graphics.FillPolygon(new SolidBrush(HexagonColor), list.ToArray());
            }

            int num = (int)Math.Round(DateTime.Now.Hour * 100 / 24.0);
            int num2 = (int)Math.Round(DateTime.Now.Minute * 100 / 60.0);
            int num3 = (int)Math.Round(DateTime.Now.Second * 100 / 60.0);

            Rectangle rectangle;

            if (ShowSecondsCircle && ShowMinutesCircle)
            {
                rectangle = new Rectangle((Width / 8) + (CircleThickness * 2) - 2, (Height / 6) + (CircleThickness * 2) - 1, (Width / 8 * 6) - (CircleThickness * 4) + 4, (Height / 6 * 4) - (CircleThickness * 4) + 2);
                bufferedGraphics.Graphics.DrawArc(new Pen(UnfilledSecondColor, CircleThickness), rectangle, 270f, 360f);
                bufferedGraphics.Graphics.DrawArc(new Pen(FilledSecondColor, CircleThickness), rectangle, 270f, (int)(num3 * 3.6));
            }
            if (ShowMinutesCircle)
            {
                rectangle = new Rectangle((Width / 8) + CircleThickness - 1, (Height / 6) + CircleThickness - 1, (Width / 8 * 6) - (CircleThickness * 2) + 2, (Height / 6 * 4) - (CircleThickness * 2) + 2);
                bufferedGraphics.Graphics.DrawArc(new Pen(unfilledMinuteColor, CircleThickness), rectangle, 270f, 360f);
                bufferedGraphics.Graphics.DrawArc(new Pen(filledMinuteColor, CircleThickness), rectangle, 270f, (int)(num2 * 3.6));
            }

            rectangle = new Rectangle(Width / 8, Height / 6, Width / 8 * 6, Height / 6 * 4);

            bufferedGraphics.Graphics.DrawArc(new Pen(UnfilledHourColor, CircleThickness), rectangle, 270f, 360f);
            bufferedGraphics.Graphics.DrawArc(new Pen(FilledHourColor, CircleThickness), rectangle, 270f, (int)(num * 3.6));

            rectangle.Inflate(0, -5);

            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            if (DisplayFormat == HourFormat.TwelveHour)
            {
                if (ShowAmPm)
                {
                    bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString(TimeAMFormat) + "\n" + DateTime.Now.ToString("tt", CultureInfo.InvariantCulture), Font, new SolidBrush(TimeColor), rectangle, stringFormat);
                }
                else
                {
                    bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString(TimeAMFormat), Font, new SolidBrush(TimeColor), rectangle, stringFormat);
                }
            }
            else
            {
                bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString(TimePMFormat), Font, new SolidBrush(TimeColor), rectangle, stringFormat);
            }

            bufferedGraphics.Render(e.Graphics);
            base.OnPaint(e);
        }

        private readonly Timer RefreshUI = new();

        private BufferedGraphics bufferedGraphics;
        private Color unfilledMinuteColor = Color.FromArgb(60, 60, 70);

        private readonly Color filledMinuteColor = Color.DodgerBlue;

        public enum HourFormat
        {
            TwelveHour,
            TwentyFourHour
        }
    }

    #endregion
}