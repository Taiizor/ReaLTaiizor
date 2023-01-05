#region Imports

using ReaLTaiizor.Enum.Cyber;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static ReaLTaiizor.Util.CyberLibrary;
using Timer = System.Windows.Forms.Timer;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CyberScrollBar

    [DefaultEvent("ValueChanged")]
    [ToolboxBitmap(typeof(VScrollBar))]
    [Description("Enables the component to scroll horizontally/vertically through content (use event generation).")]
    public partial class CyberScrollBar : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private Rectangle rectangle_value = new();
        private GraphicsPath graphicsPath = new();
        private Size size_cyberscrollbar = new();

        #endregion

        #region Property Region

        private int tmp_value_scroll;
        [Category("Value")]
        [Description("Meaning")]
        public int Value
        {
            get => tmp_value_scroll;
            set
            {
                if (tmp_value_scroll == value)
                {
                    return;
                }

                tmp_value_scroll = value;
                Refresh();
                OnScroll();
            }
        }

        private Orientation tmp_orientation;
        [Category("Value")]
        [Description("System.Windows.Forms.Orientation")]
        public Orientation OrientationValue
        {
            get => tmp_orientation;
            set
            {
                if (value == Orientation.Vertical)
                {
                    Size = new Size(Size.Width, Size.Height);
                    if (RoundingInt != 0)
                    {
                        RoundingInt /= 10;
                    }
                }
                else
                {
                    Size = new Size(Size.Height, Size.Width);
                    if (RoundingInt != 0)
                    {
                        RoundingInt *= 10;
                    }
                }
                tmp_orientation = value;
                Refresh();
            }
        }

        [Category("Value")]
        [Description("Values for how much it will scroll")]
        [DefaultValue(1)]
        public int SmallStep { get; set; }

        private int tmp_thumbSize;
        [Category("Value")]
        [Description("Slider size")]
        public int ThumbSize
        {
            get => tmp_thumbSize;
            set
            {
                tmp_thumbSize = value;
                Refresh();
            }
        }

        private int tmp_value_maximum;
        [Category("Value")]
        [Description("MAX Value")]
        public int Maximum
        {
            get => tmp_value_maximum;
            set
            {
                if (value > Minimum)
                {
                    tmp_value_maximum = value;
                    Value = 0;
                    Refresh();
                }
            }
        }

        private int tmp_value_minimum;
        [Category("Value")]
        [Description("MIN Value")]
        public int Minimum
        {
            get => tmp_value_minimum;
            set
            {
                if (value < Minimum)
                {
                    tmp_value_minimum = value;
                    Value = 0;
                    Refresh();
                }
            }
        }

        private bool tmp_rgb_status;
        [Category("Cyber")]
        [Description("RGB On/Off")]
        public bool RGB
        {
            get => tmp_rgb_status;
            set
            {
                tmp_rgb_status = value;

                if (tmp_rgb_status == true)
                {
                    timer_rgb.Stop();
                    if (!DrawEngine.GlobalRGB.Enabled)
                    {
                        timer_rgb.Tick += (Sender, EventArgs) =>
                        {
                            h += 4;
                            if (h >= 360)
                            {
                                h = 0;
                            }

                            Refresh();
                        };
                        timer_rgb.Start();
                    }
                }
                else
                {
                    timer_rgb.Stop();
                    Refresh();
                }
            }
        }

        private readonly Timer timer_rgb = new() { Interval = 300 };
        [Category("Timers")]
        [Description("RGB refresh rate value (redrawing in effect)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
        }

        private Color tmp_color_fscrollbar;
        [Category("Cyber")]
        [Description("Fill color")]
        public Color ColorScrollBar
        {
            get => tmp_color_fscrollbar;
            set
            {
                tmp_color_fscrollbar = value;
                Refresh();

            }
        }

        private int tmp_color_fscrollbar_transparency;
        [Category("Value")]
        [Description("Fill transparency value")]
        public int ColorScrollBar_Transparency
        {
            get => tmp_color_fscrollbar_transparency;
            set
            {
                if (value is >= 10 and <= 255)
                {
                    tmp_color_fscrollbar_transparency = value;
                    Refresh();
                }
            }
        }

        private bool tmp_rounding_status;
        [Category("Cyber")]
        [Description("On/Off Rounding")]
        public bool Rounding
        {
            get => tmp_rounding_status;
            set
            {
                tmp_rounding_status = value;
                Refresh();

            }
        }

        private int tmp_rounding_int;
        [Category("Cyber")]
        [Description("Percentage rounding")]
        public int RoundingInt
        {
            get => tmp_rounding_int;
            set
            {
                if (value is >= 0 and <= 100)
                {
                    tmp_rounding_int = value;
                    Refresh();

                }
            }
        }

        private Color tmp_color_background;
        [Category("Cyber")]
        [Description("Background color")]
        public Color ColorBackground
        {
            get => tmp_color_background;
            set
            {
                tmp_color_background = value;
                Refresh();

            }
        }

        private bool tmp_background;
        [Category("Cyber")]
        [Description("Background On/Off")]
        public bool Background
        {
            get => tmp_background;
            set
            {
                tmp_background = value;
                Refresh();

            }
        }

        private bool tmp_background_pen;
        [Category("BorderStyle")]
        [Description("On/Off Border")]
        public bool BackgroundPen
        {
            get => tmp_background_pen;
            set
            {
                tmp_background_pen = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private float tmp_background_width_pen;
        [Category("BorderStyle")]
        [Description("Border size")]
        public float Background_WidthPen
        {
            get => tmp_background_width_pen;
            set
            {
                tmp_background_width_pen = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private Color tmp_color_background_pen;
        [Category("BorderStyle")]
        [Description("Border color")]
        public Color ColorBackground_Pen
        {
            get => tmp_color_background_pen;
            set
            {
                tmp_color_background_pen = value;
                Refresh();
            }
        }

        private bool tmp_lighting;
        [Category("Lighting")]
        [Description("On/Off backlight")]
        public bool Lighting
        {
            get => tmp_lighting;
            set
            {
                tmp_lighting = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private Color tmp_color_lighting;
        [Category("Lighting")]
        [Description("Backlight / Shadow Color")]
        public Color ColorLighting
        {
            get => tmp_color_lighting;
            set
            {
                tmp_color_lighting = value;
                Refresh();
            }
        }

        private int tmp_alpha;
        [Category("Lighting")]
        [Description("Lighting alpha")]
        public int Alpha
        {
            get => tmp_alpha;
            set
            {
                tmp_alpha = value;
                Refresh();
            }
        }

        private int tmp_pen_width;
        [Category("Lighting")]
        [Description("Lighting width")]
        public int PenWidth
        {
            get => tmp_pen_width;
            set
            {
                tmp_pen_width = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        private bool tmp_lineargradient_background_status;
        [Category("LinearGradient")]
        [Description("On/Off background gradient")]
        public bool LinearGradient_Background
        {
            get => tmp_lineargradient_background_status;
            set
            {
                tmp_lineargradient_background_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_background;
        [Category("LinearGradient")]
        [Description("Color #1 for background gradient")]
        public Color ColorBackground_1
        {
            get => tmp_color_1_for_gradient_background;
            set
            {
                tmp_color_1_for_gradient_background = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_background;
        [Category("LinearGradient")]
        [Description("Color #2 for background gradient")]
        public Color ColorBackground_2
        {
            get => tmp_color_2_for_gradient_background;
            set
            {
                tmp_color_2_for_gradient_background = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_value_status;
        [Category("LinearGradient")]
        [Description("On/Off slider gradient")]
        public bool LinearGradient_Value
        {
            get => tmp_lineargradient_value_status;
            set
            {
                tmp_lineargradient_value_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_value;
        [Category("LinearGradient")]
        [Description("Color #1 for slider gradient")]
        public Color ColorBackground_Value_1
        {
            get => tmp_color_1_for_gradient_value;
            set
            {
                tmp_color_1_for_gradient_value = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_value;
        [Category("LinearGradient")]
        [Description("Color #2 for slider gradient")]
        public Color ColorBackground_Value_2
        {
            get => tmp_color_2_for_gradient_value;
            set
            {
                tmp_color_2_for_gradient_value = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_pen_status;
        [Category("LinearGradient")]
        [Description("On/Off border gradient")]
        public bool LinearGradientPen
        {
            get => tmp_lineargradient_pen_status;
            set
            {
                tmp_lineargradient_pen_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_pen;
        [Category("LinearGradient")]
        [Description("Color #1 for border gradient")]
        public Color ColorPen_1
        {
            get => tmp_color_1_for_gradient_pen;
            set
            {
                tmp_color_1_for_gradient_pen = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_pen;
        [Category("LinearGradient")]
        [Description("Color #2 for border gradient")]
        public Color ColorPen_2
        {
            get => tmp_color_2_for_gradient_pen;
            set
            {
                tmp_color_2_for_gradient_pen = value;
                Refresh();
            }
        }

        private SmoothingMode tmp_smoothing_mode;
        [Category("Cyber")]
        [Description("Mode <graphics.SmoothingMode>")]
        public SmoothingMode SmoothingMode
        {
            get => tmp_smoothing_mode;
            set
            {
                if (value != SmoothingMode.Invalid)
                {
                    tmp_smoothing_mode = value;
                }

                Refresh();
            }
        }

        private TextRenderingHint tmp_text_rendering_hint;
        [Category("Cyber")]
        [Description("Mode <graphics.TextRenderingHint>")]
        public TextRenderingHint TextRenderingHint
        {
            get => tmp_text_rendering_hint;
            set
            {
                tmp_text_rendering_hint = value;
                Refresh();
            }
        }

        private StateStyle tmp_cyberscrollbar_style = StateStyle.Default;
        [Category("Cyber")]
        [Description("ScrollBar style")]
        public StateStyle CyberScrollBarStyle
        {
            get => tmp_cyberscrollbar_style;
            set
            {
                tmp_cyberscrollbar_style = value;
                switch (tmp_cyberscrollbar_style)
                {
                    case StateStyle.Default:
                        Size = new Size(26, 300);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);

                        Value = 0;
                        Minimum = 0;
                        Maximum = 100;
                        OrientationValue = Orientation.Vertical;
                        ThumbSize = 60;
                        SmallStep = 1;
                        RGB = false;
                        Background = true;
                        BackgroundPen = true;
                        Background_WidthPen = 3F;
                        Rounding = true;
                        RoundingInt = 7;
                        Timer_RGB = 300;
                        ColorScrollBar = Color.FromArgb(29, 200, 238);
                        ColorScrollBar_Transparency = 255;
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                        Lighting = false;
                        ColorLighting = Color.FromArgb(29, 200, 238);
                        Alpha = 50;
                        PenWidth = 10;
                        LinearGradient_Background = false;
                        ColorBackground_1 = Color.FromArgb(37, 52, 68);
                        ColorBackground_2 = Color.FromArgb(41, 63, 86);
                        LinearGradientPen = false;
                        ColorPen_1 = Color.FromArgb(37, 52, 68);
                        ColorPen_2 = Color.FromArgb(41, 63, 86);
                        LinearGradient_Value = false;
                        ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
                        ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
                        SmoothingMode = SmoothingMode.HighQuality;
                        TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        break;
                    case StateStyle.Custom:
                        break;
                    case StateStyle.Random:
                        HelpEngine.GetRandom random = new();
                        Background = random.Bool();
                        Rounding = random.Bool();
                        if (Rounding)
                        {
                            RoundingInt = random.Int(2, 10);
                        }

                        if (Background)
                        {
                            ColorBackground = random.ColorArgb(random.Int(0, 255));
                        }

                        BackgroundPen = random.Bool();

                        if (BackgroundPen)
                        {
                            Background_WidthPen = random.Float(1, 3);
                            ColorBackground_Pen = random.ColorArgb(random.Int(0, 255));
                        }

                        Lighting = random.Bool();
                        if (Lighting)
                        {
                            ColorLighting = random.ColorArgb();
                        }

                        LinearGradient_Background = random.Bool();
                        if (LinearGradient_Background)
                        {
                            ColorBackground_1 = random.ColorArgb();
                            ColorBackground_2 = random.ColorArgb();
                        }

                        LinearGradient_Value = random.Bool();
                        if (LinearGradient_Value)
                        {
                            ColorBackground_Value_1 = random.ColorArgb();
                            ColorBackground_Value_2 = random.ColorArgb();
                        }

                        LinearGradientPen = random.Bool();
                        if (LinearGradientPen)
                        {
                            ColorPen_1 = random.ColorArgb();
                            ColorPen_2 = random.ColorArgb();
                        }

                        ColorScrollBar = random.ColorArgb();
                        ColorScrollBar_Transparency = random.Int(0, 255);
                        break;
                }

                Refresh();
            }
        }

        #endregion

        #region Constructor Region

        public CyberScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberScrollBarStyle = StateStyle.Default;
            CyberScrollBarStyle = StateStyle.Custom;

            OnSizeChanged(null);
        }

        #endregion

        #region Event Region

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                Settings_Load(e.Graphics);
                Draw_Background(e.Graphics);
            }
            catch (Exception Ex)
            {
                HelpEngine.Error($"[{Name}] Error: \n{Ex}");
            }
        }

        public virtual void OnScroll(ScrollEventType type = ScrollEventType.ThumbPosition)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseScroll(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseScroll(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
            size_cyberscrollbar = new Size(Width - (tmp * 2), Height - (tmp * 2));
            rectangle_region = new Rectangle(tmp, tmp, size_cyberscrollbar.Width, size_cyberscrollbar.Height);
        }
        private void MouseScroll(MouseEventArgs e)
        {
            int value = Value;

            switch (OrientationValue)
            {
                case Orientation.Vertical:
                    if (e.Y < 0)
                    {
                        value -= SmallStep;
                    }
                    else if (e.Y > rectangle_region.Height)
                    {
                        value += SmallStep;
                    }
                    else
                    {
                        value = Maximum * (e.Y - (ThumbSize / 2)) / (rectangle_region.Height - ThumbSize);
                    }

                    rectangle_value = new Rectangle(
                    rectangle_region.X,
                    rectangle_region.Y + (Value * (rectangle_region.Height - ThumbSize) / Maximum),
                    rectangle_region.Width,
                    ThumbSize);
                    break;
                case Orientation.Horizontal:
                    if (e.X < 0)
                    {
                        value -= SmallStep;
                    }
                    else if (e.X > rectangle_region.Width)
                    {
                        value += SmallStep;
                    }
                    else
                    {
                        value = Maximum * (e.X - (ThumbSize / 2)) / (rectangle_region.Width - ThumbSize);
                    }

                    rectangle_value = new Rectangle(
                    rectangle_region.X + (Value * (rectangle_region.Width - ThumbSize) / Maximum),
                    rectangle_region.Y,
                    ThumbSize,
                    rectangle_region.Height);
                    break;
            }
            Value = Math.Max(0, Math.Min(Maximum, value));
        }

        #endregion

        #region Event Handler Region

        [Category("Cyber")]
        [Description("Occurs whenever the Value property changes.")]
        public event EventHandler ValueChanged;

        #endregion

        #region Method Region

        private void Settings_Load(Graphics graphics)
        {
            BackColor = Color.Transparent;

            graphics.SmoothingMode = SmoothingMode;
            graphics.TextRenderingHint = TextRenderingHint;
        }

        private void Draw_Background(Graphics graphics_form)
        {
            float roundingValue;
            void BaseLoading()
            {
                //Rounding
                roundingValue = 0.1F;
                if (Rounding && RoundingInt > 0)
                {
                    roundingValue = Height / 100F * RoundingInt;
                }

                //RoundedRectangle
                graphicsPath = DrawEngine.RoundedRectangle(rectangle_region, roundingValue);

                //Region
                Region = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                0, 0,
                Width, Height),
                roundingValue));
            }
            Bitmap Layer_1()
            {
                Bitmap bitmap = new(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Shadow
                if (Lighting)
                {
                    DrawEngine.DrawBlurred(graphics, ColorLighting, DrawEngine.RoundedRectangle(rectangle_region, roundingValue), Alpha, PenWidth);
                }

                //Background border
                if (Background_WidthPen != 0 && BackgroundPen == true)
                {
                    Pen pen;
                    if (LinearGradientPen)
                    {
                        pen = new Pen(new LinearGradientBrush(rectangle_region, ColorPen_1, ColorPen_2, 360), Background_WidthPen);
                    }
                    else
                    {
                        pen = new Pen(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Pen, Background_WidthPen);
                    }

                    pen.LineJoin = LineJoin.Round;
                    pen.DashCap = DashCap.Round;
                    graphics.DrawPath(pen, graphicsPath);
                }

                return bitmap;
            }
            Bitmap Layer_2()
            {
                Bitmap bitmap = new(Width, Height);
                Graphics graphics = HelpEngine.GetGraphics(ref bitmap, SmoothingMode, TextRenderingHint);

                //Region_Clip
                graphics.Clip = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                    rectangle_region.X - (int)(2 + Background_WidthPen),
                    rectangle_region.Y - (int)(2 + Background_WidthPen),
                    rectangle_region.Width + ((int)(2 + Background_WidthPen) * 2),
                    rectangle_region.Height + ((int)(2 + Background_WidthPen) * 2)), Rounding ? roundingValue : 0.1F));

                //Background
                if (Background == true)
                {
                    Brush brush = new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(LinearGradient_Background ? brush : new SolidBrush(ColorBackground), graphicsPath);
                }

                //Additionally
                Draw_Bar(graphics, roundingValue);

                return bitmap;
            }

            BaseLoading();
            graphics_form.DrawImage(Layer_1(), new PointF(0, 0));
            graphics_form.DrawImage(Layer_2(), new PointF(0, 0));
        }

        private void Draw_Bar(Graphics graphics, float roundingValue)
        {
            if (Maximum <= 0)
            {
                return;
            }

            rectangle_value = new Rectangle(2, 2, rectangle_region.Width, ThumbSize);

            switch (OrientationValue)
            {
                case Orientation.Vertical:
                    rectangle_value = new Rectangle(
                    rectangle_region.X,
                    rectangle_region.Y + (Value * (rectangle_region.Height - ThumbSize) / Maximum),
                    rectangle_region.Width,
                    ThumbSize);
                    break;
                case Orientation.Horizontal:
                    rectangle_value = new Rectangle(
                    rectangle_region.X + (Value * (rectangle_region.Width - ThumbSize) / Maximum),
                    rectangle_region.Y,
                    ThumbSize,
                    rectangle_region.Height);
                    break;
            }

            int tmp = 1;
            rectangle_value.X -= tmp;
            rectangle_value.Y -= tmp;
            rectangle_value.Width += tmp * 2;
            rectangle_value.Height += tmp * 2;
            roundingValue += tmp * 2;

            if (LinearGradient_Value)
            {
                graphics.FillPath(new LinearGradientBrush(rectangle_region,
                    Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1),
                    Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2), 360),
                    DrawEngine.RoundedRectangle(rectangle_value, roundingValue));
            }
            else
            {
                graphics.FillPath(new SolidBrush(
                Color.FromArgb(ColorScrollBar_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorScrollBar)),
                DrawEngine.RoundedRectangle(rectangle_value, roundingValue));
            }
        }

        #endregion
    }

    #endregion
}