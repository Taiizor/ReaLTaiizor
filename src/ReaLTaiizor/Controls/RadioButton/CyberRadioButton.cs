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
    #region CyberRadioButton

    [ToolboxBitmap(typeof(System.Windows.Forms.RadioButton))]
    [Description("Allows the user to select or remove the appropriate option.")]
    public partial class CyberRadioButton : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private GraphicsPath graphicsPath = new();
        private int temp = 0;
        private bool Mouse_Enter = false;
        private Size size_cyberradiobutton = new();

        #endregion

        #region Property Region

        private bool tmp_checked_status;
        [Category("Cyber")]
        [Description("On/Off checked status")]
        public bool Checked
        {
            get => tmp_checked_status;
            set
            {
                tmp_checked_status = value;
                CheckedChanged();
                Refresh();
            }
        }

        private int tmp_size_checked;
        [Category("Cyber")]
        [Description(
        "Activation size - small circle (the < value, the larger the" +
        "circle [the value should be even], default - 8)")]
        public int SizeChecked
        {
            get => tmp_size_checked;
            set
            {
                if (value % 2 == 0)
                {
                    tmp_size_checked = value;
                    Refresh();
                }
            }
        }

        private string tmp_text_button;
        [Category("Cyber")]
        [Description("Text on radiobutton")]
        public string TextButton
        {
            get => tmp_text_button;
            set
            {
                tmp_text_button = value;
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

        private Color tmp_color_click_circle;
        [Category("Effects")]
        [Description("Click animation color")]
        public Color Effect_1_ColorBackground
        {
            get => tmp_color_click_circle;
            set
            {
                tmp_color_click_circle = value;
                Refresh();
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

        private float background_width_pen;
        [Category("BorderStyle")]
        [Description("Border size")]
        public float Background_WidthPen
        {
            get => background_width_pen;
            set
            {
                background_width_pen = value;
                Refresh();
            }
        }

        public static Color tmp_color_background_pen;
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

        private Color color_checked;
        [Category("Cyber")]
        [Description("Checkmark color")]
        public Color ColorChecked
        {
            get => color_checked;
            set
            {
                color_checked = value;
                Refresh();
            }
        }

        private bool tmp_lineargradient_value_status;
        [Category("LinearGradient")]
        [Description("On/Off Gradient Value")]
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
        [Description("Color #1 for the value")]
        public Color Color_1_Background_value
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
        [Description("Color #2 for the value")]
        public Color Color_2_Background_value
        {
            get => tmp_color_2_for_gradient_value;
            set
            {
                tmp_color_2_for_gradient_value = value;
                Refresh();
            }
        }

        [Category("Effects")]
        [DefaultValue(true)]
        [Description("On/Off circle effect when hovering/activating")]
        public bool Effect_1 { get; set; }

        private int effect1_transparency;
        [Category("Effects")]
        [Description("Transparency effect_1")]
        public int Effect_1_Transparency
        {
            get => effect1_transparency;
            set
            {
                if (value is > 0 and <= 255)
                {
                    effect1_transparency = value;
                }
            }
        }

        [Category("Effects")]
        [Description("On/Off the white background effect on the button")]
        public bool Effect_2 { get; set; }

        private int effect2_transparency;
        [Category("Effects")]
        [Description("Transparency effect_2")]
        public int Effect_2_Transparency
        {
            get => effect2_transparency;
            set
            {
                if (value is > 0 and <= 255)
                {
                    effect2_transparency = value;
                }
            }
        }

        [Category("Effects")]
        [Description("Effect color")]
        public Color Effect_2_ColorBackground { get; set; }

        private readonly Timer timer_effect_1 = new() { Interval = 1 };
        [Category("Timers")]
        [Description("Effect speed <effect_1> (redrawing is in effect)")]
        public int Timer_Effect_1
        {
            get => timer_effect_1.Interval;
            set => timer_effect_1.Interval = value;
        }

        private readonly Timer timer_rgb = new() { Interval = 300 };
        [Category("Timers")]
        [Description("RGB mode refresh rate (redrawing in effect)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
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

        private Color tmp_color_1_for_gradient;
        [Category("LinearGradient")]
        [Description("Color #1 for gradient")]
        public Color ColorBackground_1
        {
            get => tmp_color_1_for_gradient;
            set
            {
                tmp_color_1_for_gradient = value;
                Refresh();
            }
        }

        private Color tmp_color_2_for_gradient;
        [Category("LinearGradient")]
        [Description("Color #2 for gradient")]
        public Color ColorBackground_2
        {
            get => tmp_color_2_for_gradient;
            set
            {
                tmp_color_2_for_gradient = value;
                Refresh();
            }
        }

        private StateStyle tmp_cyberradiobutton_style = StateStyle.Default;
        [Category("Cyber")]
        [Description("RadioButton style")]
        public StateStyle CyberRadioButtonStyle
        {
            get => tmp_cyberradiobutton_style;
            set
            {
                tmp_cyberradiobutton_style = value;
                switch (tmp_cyberradiobutton_style)
                {
                    case StateStyle.Default:
                        Size = new Size(180, 45);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);

                        Checked = false;
                        SizeChecked = 8;
                        TextButton = "CyberRadioButton";
                        RGB = false;
                        Background = true;
                        Rounding = true;
                        RoundingInt = 100;
                        Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        BackgroundPen = true;
                        Background_WidthPen = 2F;
                        ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                        ColorChecked = Color.FromArgb(29, 200, 238);
                        Effect_1 = true;
                        Effect_1_Transparency = 25;
                        Effect_2 = true;
                        Effect_2_Transparency = 15;
                        Effect_2_ColorBackground = Color.White;
                        Timer_Effect_1 = 1;
                        Timer_RGB = 300;
                        LinearGradient_Background = false;
                        ColorBackground_1 = Color.FromArgb(37, 52, 68);
                        ColorBackground_2 = Color.FromArgb(41, 63, 86);
                        LinearGradientPen = false;
                        ColorPen_1 = Color.FromArgb(37, 52, 68);
                        ColorPen_2 = Color.FromArgb(41, 63, 86);
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
                            ColorChecked = random.ColorArgb(random.Int(0, 255));
                        }

                        LinearGradient_Background = random.Bool();
                        if (LinearGradient_Background)
                        {
                            ColorBackground_1 = random.ColorArgb();
                            ColorBackground_2 = random.ColorArgb();
                        }

                        LinearGradientPen = random.Bool();
                        if (LinearGradientPen)
                        {
                            ColorPen_1 = random.ColorArgb();
                            ColorPen_2 = random.ColorArgb();
                        }
                        break;
                }

                Refresh();
            }
        }

        #endregion

        #region Constructor Region

        public CyberRadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberRadioButtonStyle = StateStyle.Default;
            CyberRadioButtonStyle = StateStyle.Custom;

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
                Draw_Text(e.Graphics);

                graphicsPath.ClearMarkers();
                graphicsPath.Dispose();
            }
            catch (Exception Ex)
            {
                HelpEngine.Error($"[{Name}] Error: \n{Ex}");
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Checked = !Checked;

            timer_effect_1.Stop();
            timer_effect_1.Dispose();
            if (e.Button == MouseButtons.Left)
            {
                temp = size_cyberradiobutton.Width;

                if (Checked)
                {
                    timer_effect_1.Tick += (Sender, EventArgs) =>
                    {
                        temp += 1;
                        Refresh();
                    };
                    timer_effect_1.Start();
                }
                else
                {
                    Refresh();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            Mouse_Enter = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            timer_effect_1.Stop();
            timer_effect_1.Dispose();
            Mouse_Enter = false;
            temp = 0;

            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Size = new Size(Size.Width, 45);
            size_cyberradiobutton = new Size(21, 21);
            rectangle_region = new Rectangle(15, (base.Size.Height / 2) - 12, size_cyberradiobutton.Width, size_cyberradiobutton.Height);
        }

        protected override CreateParams CreateParams //WS_CLIPCHILDREN
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x02000000;
                return createParams;
            }
        }

        #endregion

        #region Event Handler Region

        public delegate void EventHandler();
        [Category("Cyber")]
        [Description("Occurs whenever the Checked property is changed.")]
        public event EventHandler CheckedChanged = delegate { };

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
            float roundingValue = 0.1F;
            void BaseLoading()
            {
                //Rounding
                if (Rounding && RoundingInt > 0)
                {
                    roundingValue = size_cyberradiobutton.Height / 100F * RoundingInt;
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

                //Effects
                if (Effect_1)
                {
                    Draw_Animation_Circles(graphics);
                }

                if (Effect_2 && Mouse_Enter)
                {
                    Draw_Animation_WhiteBackground_CirclesStyle(graphics);
                }

                //Background
                if (Background == true)
                {
                    Brush brush = new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(LinearGradient_Background ? brush : new SolidBrush(ColorBackground), graphicsPath);
                }

                //Additionally
                if (Checked)
                {
                    Draw_Checked(graphics);
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
                TextButton,
                Font,
                new SolidBrush(ForeColor),
                new Rectangle((int)(25 + graphicsPath.GetBounds().Width), (Size.Height / 2) - (Font.Height / 2), 0, 0));
        }

        private void Draw_Checked(Graphics graphics)
        {
            Rectangle rectangle_temp = rectangle_region;

            rectangle_temp.Width -= SizeChecked;
            rectangle_temp.Height -= SizeChecked;
            rectangle_temp.X = rectangle_temp.X + (rectangle_temp.Width / 2) - (10 - SizeChecked);
            rectangle_temp.Y = rectangle_temp.Y + (rectangle_temp.Height / 2) - (10 - SizeChecked);

            GraphicsPath graphicsPath_temp = DrawEngine.RoundedRectangle(rectangle_temp, Rounding ? rectangle_temp.Height / 100F * RoundingInt : 0.1F);

            if (LinearGradient_Value)
            {
                graphics.FillPath(new LinearGradientBrush(rectangle_region,
                RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorBackground_1,
                RGB ? DrawEngine.HSV_To_RGB(h + 20, 1f, 1f) : ColorBackground_2,
                360), graphicsPath_temp);
            }
            else
            {
                graphics.FillPath(new SolidBrush(RGB ? DrawEngine.HSV_To_RGB(h, 1f, 1f) : ColorChecked), graphicsPath_temp);
            }
        }

        private void Draw_Animation_Circles(Graphics graphics)
        {
            int size_circles = 40;
            if (temp < size_circles)
            {
                Rectangle rectangle_circles = new(
                    15 + (25 / 2) - (temp / 2),
                    (Size.Height / 2) - (25 / 2) + (25 / 2) - (temp / 2),
                    temp, temp);
                rectangle_circles.X -= 2;
                rectangle_circles.Y -= 2;
                if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
                {
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(Effect_1_Transparency, Effect_1_ColorBackground)), rectangle_circles);
                }
            }
        }

        private void Draw_Animation_WhiteBackground_CirclesStyle(Graphics graphics)
        {
            int size_circles = 40;

            Rectangle rectangle_circles = new(
                15 + (25 / 2) - (size_circles / 2),
                (Size.Height / 2) - (25 / 2) + (25 / 2) - (size_circles / 2),
                size_circles, size_circles);
            rectangle_circles.X -= 2;
            rectangle_circles.Y -= 2;
            if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
            {
                graphics.FillEllipse(new SolidBrush(Color.FromArgb(Effect_2_Transparency, Effect_2_ColorBackground)), rectangle_circles);
            }
        }

        #endregion
    }

    #endregion
}