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
    #region CyberButton

    [DefaultEvent("Click")]
    [ToolboxBitmap(typeof(System.Windows.Forms.Button))]
    [Description("When clicked, an event is fired.")]
    public partial class CyberButton : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private GraphicsPath graphicsPath = new();
        private Point ClickLocation = new();
        private readonly StringFormat stringFormat = new();
        private int temp = 0;
        private bool Mouse_Enter = false;
        private Size size_cyberbutton = new();

        #endregion

        #region Property Region

        [Category("Cyber")]
        [Description("Text on button")]
        public string TextButton
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Cyber")]
        [DefaultValue(false)]
        [Description("On/Off RGB mode")]
        public bool RGB
        {
            get;
            set
            {
                field = value;

                if (field == true)
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

        [Category("Cyber")]
        [Description("On/Off Background")]
        public bool Background
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Rounding")]
        [Description("On/Off Rounded button")]
        public bool Rounding
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Rounding")]
        [Description("Percentage rounding")]
        public int RoundingInt
        {
            get;
            set
            {
                if (value is >= 0 and <= 100)
                {
                    field = value;
                    Refresh();
                }
            }
        }

        [Category("Effects")]
        [Description("Click animation color")]
        public Color Effect_1_ColorBackground { get; set; }

        [Category("Cyber")]
        [Description("Background color")]
        public Color ColorBackground
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Effects")]
        [Description("On/Off the circular effect when clicking")]
        public bool Effect_1 { get; set; }

        [Category("Effects")]
        [Description("Transparency effect_1")]
        public int Effect_1_Transparency
        {
            get;
            set
            {
                if (value is > 0 and <= 255)
                {
                    field = value;
                }
            }
        }

        [Category("Effects")]
        [Description("On/Off the white background effect on the button")]
        public bool Effect_2 { get; set; }

        [Category("Effects")]
        [Description("Transparency effect_2")]
        public int Effect_2_Transparency
        {
            get;
            set
            {
                if (value is > 0 and <= 255)
                {
                    field = value;
                }
            }
        }

        [Category("Effects")]
        [Description("Effect color")]
        public Color Effect_2_ColorBackground { get; set; }

        private readonly Timer timer_effect_1 = new();
        [Category("Timers")]
        [Description("Effect speed <effect_1> (redrawing is in effect)")]
        public int Timer_Effect_1
        {
            get => timer_effect_1.Interval;
            set => timer_effect_1.Interval = value;
        }

        private readonly Timer timer_rgb = new();
        [Category("Timers")]
        [Description("RGB mode refresh rate (redrawing in effect)")]
        public int Timer_RGB
        {
            get => timer_rgb.Interval;
            set => timer_rgb.Interval = value;
        }

        [Category("BorderStyle")]
        [Description("On/Off Border")]
        public bool BackgroundPen
        {
            get;
            set
            {
                field = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        [Category("BorderStyle")]
        [Description("Border size")]
        public float Background_WidthPen
        {
            get;
            set
            {
                field = value;
                OnSizeChanged(null);
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

        [Category("Lighting")]
        [Description("On/Off Backlight")]
        public bool Lighting
        {
            get;
            set
            {
                field = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        [Category("Lighting")]
        [Description("Backlight / Shadow Color")]
        public Color ColorLighting
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Lighting")]
        [Description("Lighting alpha")]
        public int Alpha
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Lighting")]
        [Description("Lighting width")]
        public int PenWidth
        {
            get;
            set
            {
                field = value;
                OnSizeChanged(null);
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("On/Off background gradient")]
        public bool LinearGradient_Background
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("Color #1 for gradient")]
        public Color ColorBackground_1
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("Color #2 for gradient")]
        public Color ColorBackground_2
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("On/Off border gradient")]
        public bool LinearGradientPen
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("Color #1 for border gradient")]
        public Color ColorPen_1
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("LinearGradient")]
        [Description("Color #2 for border gradient")]
        public Color ColorPen_2
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Cyber")]
        [Description("Mode <graphics.SmoothingMode>")]
        public SmoothingMode SmoothingMode
        {
            get;
            set
            {
                if (value != SmoothingMode.Invalid)
                {
                    field = value;
                }

                Refresh();
            }
        }

        [Category("Cyber")]
        [Description("Mode <graphics.TextRenderingHint>")]
        public TextRenderingHint TextRenderingHint
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        }

        [Category("Cyber")]
        [Description("Control style")]
        public StateStyle CyberButtonStyle
        {
            get;
            set
            {
                field = value;
                switch (field)
                {
                    case StateStyle.Default:
                        Size = new Size(130, 50);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);

                        TextButton = "CyberButton";
                        RGB = false;
                        Background = true;
                        Rounding = true;
                        RoundingInt = 70;
                        Effect_1_ColorBackground = Color.FromArgb(29, 200, 238);
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        Effect_1 = true;
                        Effect_1_Transparency = 25;
                        Effect_2 = true;
                        Effect_2_Transparency = 20;
                        Effect_2_ColorBackground = Color.White;
                        Timer_Effect_1 = 5;
                        Timer_RGB = 300;
                        BackgroundPen = true;
                        Background_WidthPen = 4F;
                        ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                        Lighting = false;
                        ColorLighting = Color.FromArgb(29, 200, 238);
                        Alpha = 20;
                        PenWidth = 15;
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
        } = StateStyle.Default;

        #endregion

        #region Constructor Region

        public CyberButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberButtonStyle = StateStyle.Default;
            CyberButtonStyle = StateStyle.Custom;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            OnSizeChanged(null);
        }

        #endregion

        #region Event Handler Region

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

        protected override void OnMouseUp(MouseEventArgs e)
        {
            timer_effect_1.Stop();
            timer_effect_1.Dispose();
            if (e.Button == MouseButtons.Left && Effect_1 == true)
            {
                ClickLocation = e.Location;
                temp = 2;

                timer_effect_1.Tick += (Sender, EventArgs) =>
                {
                    temp += 20;
                    Refresh();
                };
                timer_effect_1.Start();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
            size_cyberbutton = new Size(Width - (tmp * 2), Height - (tmp * 2));
            rectangle_region = new Rectangle(tmp, tmp, size_cyberbutton.Width, size_cyberbutton.Height);
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
            float roundingValue = 0.1F;
            void BaseLoading()
            {
                //Rounding
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
                int смещение_2 = 1;
                graphics.Clip = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                    rectangle_region.X - смещение_2,
                    rectangle_region.Y - смещение_2,
                    rectangle_region.Width + (смещение_2 * 2),
                    rectangle_region.Height + (смещение_2 * 2)), Rounding ? roundingValue : 0.1F));

                //Background
                if (Background == true)
                {
                    Brush brush = new LinearGradientBrush(rectangle_region, ColorBackground_1, ColorBackground_2, 360);
                    graphics.FillPath(LinearGradient_Background ? brush : new SolidBrush(ColorBackground), graphicsPath);
                }

                //Effects
                if (Effect_1)
                {
                    Draw_Animation_Circles(graphics);
                }

                if (Effect_2 && Mouse_Enter)
                {
                    Draw_Animation_WhiteBackground(graphics);
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
                new Rectangle(rectangle_region.X, rectangle_region.Y, rectangle_region.Width, rectangle_region.Height),
                stringFormat);
        }

        private void Draw_Animation_Circles(Graphics graphics)
        {
            if (temp < ((size_cyberbutton.Width >= size_cyberbutton.Height) ? size_cyberbutton.Width * 2 : size_cyberbutton.Height * 2))
            {
                Rectangle rectangle_circles = new(ClickLocation.X - (temp / 2), ClickLocation.Y - (temp / 2), temp, temp);
                if (rectangle_circles.Width != 0 && rectangle_circles.Height != 0)
                {
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(Effect_1_Transparency, Effect_1_ColorBackground)), rectangle_circles);
                }
            }
        }

        private void Draw_Animation_WhiteBackground(Graphics graphics)
        {
            graphics.FillPath(new SolidBrush(Color.FromArgb(Effect_2_Transparency, Effect_2_ColorBackground)), graphicsPath);
        }

        #endregion
    }

    #endregion
}