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
    #region CyberProgressBar

    [ToolboxBitmap(typeof(ProgressBar))]
    [Description("Displays a progress bar.")]
    public partial class CyberProgressBar : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private Rectangle rectangle_value = new();
        private GraphicsPath graphicsPath = new();
        private Size size_cyberprogressbar = new();
        private readonly StringFormat stringFormat = new();
        private int value_progressbar_for_draw = 0;

        #endregion

        #region Property Region

        private int tmp_value_progressbar;
        [Category("Value")]
        [Description("Value [%]")]
        public int Value
        {
            get => tmp_value_progressbar;
            set
            {
                if (value <= Maximum && value >= Minimum)
                {
                    tmp_value_progressbar = value;
                    Refresh();
                }
            }
        }

        private int tmp_min_progressbar;
        [Category("Value")]
        [Description("MIN value")]
        public int Minimum
        {
            get => tmp_min_progressbar;
            set
            {
                if (value < Maximum)
                {
                    tmp_min_progressbar = value;
                    Refresh();
                }
            }
        }

        private int tmp_max_progressbar;
        [Category("Value")]
        [Description("MAX Value")]
        public int Maximum
        {
            get => tmp_max_progressbar;
            set
            {
                if (value > Minimum)
                {
                    tmp_max_progressbar = value;
                    Refresh();
                }
            }
        }

        private int tmp_start_drawing_value;
        [Category("Value")]
        [Description(
        "Value at which Value (if (Value >= value) Drawing occurs).n" +
        "(use when Rounding == true to fix the defect with small values)"
        )]
        public int StartDrawingValue
        {
            get => tmp_start_drawing_value;
            set
            {
                tmp_start_drawing_value = value;
                Refresh();
            }
        }

        private bool progress_text;
        [Category("Cyber")]
        [Description("On/Off text")]
        public bool ProgressText
        {
            get => progress_text;
            set
            {
                progress_text = value;
                Refresh();
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
        [Description("RGB mode refresh rate (redrawing in effect)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
        }

        private Color tmp_color_progressbar;
        [Category("Cyber")]
        [Description("Fill color progressbar")]
        public Color ColorProgressBar
        {
            get => tmp_color_progressbar;
            set
            {
                tmp_color_progressbar = value;
                Refresh();
            }
        }

        private int tmp_color_progressbar_transparency;
        [Category("Value")]
        [Description("Transparency of filling progressbar")]
        public int ColorValue_Transparency
        {
            get => tmp_color_progressbar_transparency;
            set
            {
                if (value is >= 5 and <= 255)
                {
                    tmp_color_progressbar_transparency = value;
                    Refresh();
                }
            }
        }

        private bool tmp_rounding_status;
        [Category("Cyber")]
        [Description("On/Off rounding progressbar")]
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

        private Color tmp_color_Background_pen;
        [Category("BorderStyle")]
        [Description("Border color")]
        public Color ColorBackground_Pen
        {
            get => tmp_color_Background_pen;
            set
            {
                tmp_color_Background_pen = value;
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

        private bool tmp_lineargradient_Background_status;
        [Category("LinearGradient")]
        [Description("On/Off background gradient")]
        public bool LinearGradient_Background
        {
            get => tmp_lineargradient_Background_status;
            set
            {
                tmp_lineargradient_Background_status = value;
                Refresh();
            }
        }

        private Color tmp_color_1_for_gradient_Background;
        [Category("LinearGradient")]
        [Description("Color #1 for background gradient")]
        public Color ColorBackground_1
        {
            get => tmp_color_1_for_gradient_Background;
            set
            {
                tmp_color_1_for_gradient_Background = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient_Background;
        [Category("LinearGradient")]
        [Description("Color #2 for background gradient")]
        public Color ColorBackground_2
        {
            get => tmp_color_2_for_gradient_Background;
            set
            {
                tmp_color_2_for_gradient_Background = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_value_status;
        [Category("LinearGradient")]
        [Description("On/Off Filling Gradient")]
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
        [Description("Color #1 for filling gradient")]
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
        [Description("Color #2 for filling gradient")]
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
        [Description("Color #1 for border gradient")]
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

        private StateStyle tmp_cyberprogressbar_style = StateStyle.Default;
        [Category("Cyber")]
        [Description("ProgressBar style")]
        public StateStyle CyberProgressBarStyle
        {
            get => tmp_cyberprogressbar_style;
            set
            {
                tmp_cyberprogressbar_style = value;
                switch (tmp_cyberprogressbar_style)
                {
                    case StateStyle.Default:
                        Size = new Size(300, 34);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);

                        Value = 0;
                        Minimum = 0;
                        Maximum = 100;
                        StartDrawingValue = 0; //6
                        ProgressText = true;
                        RGB = false;
                        Background = true;
                        Rounding = true;
                        RoundingInt = 70;
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        Timer_RGB = 300;
                        BackgroundPen = true;
                        Background_WidthPen = 3F;
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
                        ColorValue_Transparency = 200;
                        ColorProgressBar = Color.FromArgb(29, 200, 238);
                        SmoothingMode = SmoothingMode.HighQuality;
                        TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        Font = HelpEngine.GetDefaultFont();
                        break;
                    case StateStyle.Custom:
                        break;
                    case StateStyle.Random:
                        HelpEngine.GetRandom random = new();
                        Background = random.Bool();
                        Rounding = random.Bool();

                        if (Rounding)
                        {
                            RoundingInt = random.Int(5, 90);
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

                        ProgressText = random.Bool();
                        ColorProgressBar = random.ColorArgb();
                        ColorValue_Transparency = random.Int(0, 255);
                        break;
                }

                Refresh();
            }
        }

        #endregion

        #region Constructor Region

        public CyberProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberProgressBarStyle = StateStyle.Default;
            CyberProgressBarStyle = StateStyle.Custom;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

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
                if (progress_text)
                {
                    Draw_Text(e.Graphics);
                }

                graphicsPath.ClearMarkers();
                graphicsPath.Dispose();
            }
            catch (Exception Ex)
            {
                HelpEngine.Error($"[{Name}] Error: \n{Ex}");
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
            size_cyberprogressbar = new Size(Width - (tmp * 2), Height - (tmp * 2));
            rectangle_region = new Rectangle(tmp, tmp, size_cyberprogressbar.Width, size_cyberprogressbar.Height);
        }

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
                    if (LinearGradient_Background)
                    {
                        graphics.FillPath(new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360), graphicsPath);
                    }
                    else
                    {
                        graphics.FillPath(new SolidBrush(ColorBackground), graphicsPath);
                    }
                }

                //Additionally
                if (Value >= StartDrawingValue)
                {
                    Draw_Value(graphics, roundingValue);
                }

                return bitmap;
            }

            BaseLoading();
            graphics_form.DrawImage(Layer_1(), new PointF(0, 0));
            graphics_form.DrawImage(Layer_2(), new PointF(0, 0));
        }

        private void Draw_Text(Graphics graphics)
        {
            graphics.DrawString(
                (Value / (Maximum / 100)).ToString() + "%",
                Font,
                new SolidBrush(ForeColor),
                new Rectangle(rectangle_region.X, rectangle_region.Y, rectangle_region.Width, rectangle_region.Height),
                stringFormat);
        }

        private void Draw_Value(Graphics graphics, float roundingValue)
        {
            if (Value != 0)
            {
                value_progressbar_for_draw = Convert.ToInt32(graphicsPath.GetBounds().Width / 100 * (Value / (Maximum / 100)));
                rectangle_value = new Rectangle(
                    rectangle_region.X,
                    rectangle_region.Y,
                    value_progressbar_for_draw,
                    rectangle_region.Height);

                int tmp = 1;
                rectangle_value.X -= tmp;
                rectangle_value.Y -= tmp;
                rectangle_value.Width += tmp * 2;
                rectangle_value.Height += tmp * 2;
                roundingValue += tmp * 2;

                GraphicsPath graphicsPath_value = DrawEngine.RoundedRectangle(rectangle_value, roundingValue);
                if (LinearGradient_Value)
                {
                    graphics.FillPath(new LinearGradientBrush(rectangle_value,
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_Value_1),
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_Value_2), 360),
                    graphicsPath_value);
                }
                else
                {
                    graphics.FillPath(new SolidBrush(
                    Color.FromArgb(ColorValue_Transparency, RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorProgressBar)),
                    graphicsPath_value);
                }
            }
        }

        #endregion
    }

    #endregion
}