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
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.Size = new Size(120, 130);
            Font = new Font("Impact", 15f);
            RefreshUI.Interval = 1000;
            RefreshUI.Tick += RefreshUI_Tick;
            RefreshUI.Enabled = true;
        }

        private void RefreshUI_Tick(object sender, EventArgs e)
        {
            base.Invalidate();
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The circle thickness")]
        public int CircleThickness
        {
            get => circleThickness;
            set
            {
                circleThickness = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled hour circle color")]
        public Color UnfilledHourColor
        {
            get => unfilledHourColor;
            set
            {
                unfilledHourColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled hour circle color")]
        public Color FilledHourColor
        {
            get => filledHourColor;
            set
            {
                filledHourColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled minute circle color")]
        public Color UnfilledMinuteColor
        {
            get => unfilledMinuteColor;
            set
            {
                unfilledMinuteColor = value;
                base.Invalidate();
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
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The unfilled second circle color")]
        public Color UnfilledSecondColor
        {
            get => unfilledSecondColor;
            set
            {
                unfilledSecondColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The filled second circle color")]
        public Color FilledSecondColor
        {
            get => filledSecondColor;
            set
            {
                filledSecondColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The hexagon color")]
        public Color HexagonColor
        {
            get => hexagonColor;
            set
            {
                hexagonColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The time color")]
        public Color TimeColor
        {
            get => timeColor;
            set
            {
                timeColor = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the seconds circle")]
        public bool ShowSecondsCircle
        {
            get => showSecondsCircle;
            set
            {
                showSecondsCircle = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the minutes circle")]
        public bool ShowMinutesCircle
        {
            get => showMinutesCircle;
            set
            {
                showMinutesCircle = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the hexagon")]
        public bool ShowHexagon
        {
            get => showHexagon;
            set
            {
                showHexagon = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show AM/PM")]
        public bool ShowAmPm
        {
            get => showAMPM;
            set
            {
                showAMPM = value;
                base.Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The time display format")]
        public HourFormat DisplayFormat
        {
            get => displayFormat;
            set
            {
                displayFormat = value;
                base.Invalidate();
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

            if (ShowHexagon)
            {
                List<Point> list = new()
                {
                    new Point(0, base.Height / 4),
                    new Point(base.Width / 2, 0),
                    new Point(base.Width, base.Height / 4),
                    new Point(base.Width, base.Height / 4 * 3),
                    new Point(base.Width / 2, base.Height),
                    new Point(0, base.Height / 4 * 3),
                    new Point(0, base.Height / 4)
                };
                bufferedGraphics.Graphics.FillPolygon(new SolidBrush(hexagonColor), list.ToArray());
            }

            int num = (int)Math.Round((double)(DateTime.Now.Hour * 100) / 24.0);
            int num2 = (int)Math.Round((double)(DateTime.Now.Minute * 100) / 60.0);
            int num3 = (int)Math.Round((double)(DateTime.Now.Second * 100) / 60.0);

            Rectangle rectangle;

            if (showSecondsCircle && showMinutesCircle)
            {
                rectangle = new Rectangle(base.Width / 8 + circleThickness * 2 - 2, base.Height / 6 + circleThickness * 2 - 1, base.Width / 8 * 6 - circleThickness * 4 + 4, base.Height / 6 * 4 - circleThickness * 4 + 2);
                bufferedGraphics.Graphics.DrawArc(new Pen(unfilledSecondColor, (float)circleThickness), rectangle, 270f, 360f);
                bufferedGraphics.Graphics.DrawArc(new Pen(filledSecondColor, (float)circleThickness), rectangle, 270f, (float)((int)((double)num3 * 3.6)));
            }
            if (showMinutesCircle)
            {
                rectangle = new Rectangle(base.Width / 8 + circleThickness - 1, base.Height / 6 + circleThickness - 1, base.Width / 8 * 6 - circleThickness * 2 + 2, base.Height / 6 * 4 - circleThickness * 2 + 2);
                bufferedGraphics.Graphics.DrawArc(new Pen(unfilledMinuteColor, (float)circleThickness), rectangle, 270f, 360f);
                bufferedGraphics.Graphics.DrawArc(new Pen(filledMinuteColor, (float)circleThickness), rectangle, 270f, (float)((int)((double)num2 * 3.6)));
            }

            rectangle = new Rectangle(base.Width / 8, base.Height / 6, base.Width / 8 * 6, base.Height / 6 * 4);

            bufferedGraphics.Graphics.DrawArc(new Pen(unfilledHourColor, (float)circleThickness), rectangle, 270f, 360f);
            bufferedGraphics.Graphics.DrawArc(new Pen(filledHourColor, (float)circleThickness), rectangle, 270f, (float)((int)((double)num * 3.6)));

            rectangle.Inflate(0, -5);

            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            if (displayFormat == HourFormat.TwelveHour)
            {
                if (showAMPM)
                {
                    bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString("hh:mm") + "\n" + DateTime.Now.ToString("tt", CultureInfo.InvariantCulture), Font, new SolidBrush(timeColor), rectangle, stringFormat);
                }
                else
                {
                    bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString("hh:mm"), Font, new SolidBrush(timeColor), rectangle, stringFormat);
                }
            }
            else
            {
                bufferedGraphics.Graphics.DrawString(DateTime.Now.ToString("HH:mm"), Font, new SolidBrush(timeColor), rectangle, stringFormat);
            }

            bufferedGraphics.Render(e.Graphics);
            base.OnPaint(e);
        }

        private readonly Timer RefreshUI = new();

        private BufferedGraphics bufferedGraphics;

        private int circleThickness = 6;

        private Color unfilledHourColor = Color.FromArgb(75, 70, 85);

        private Color filledHourColor = Color.FromArgb(105, 190, 155);

        private Color unfilledMinuteColor = Color.FromArgb(60, 60, 70);

        private readonly Color filledMinuteColor = Color.DodgerBlue;

        private Color unfilledSecondColor = Color.FromArgb(60, 60, 70);

        private Color filledSecondColor = Color.DarkOrchid;

        private Color hexagonColor = Color.FromArgb(60, 60, 70);

        private Color timeColor = Color.FromArgb(220, 220, 220);

        private bool showSecondsCircle = true;

        private bool showMinutesCircle = true;

        private bool showHexagon = true;

        private bool showAMPM;

        private HourFormat displayFormat;

        public enum HourFormat
        {
            TwelveHour,
            TwentyFourHour
        }
    }

    #endregion
}