﻿#region Imports

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
    #region CyberTextBox

    [ToolboxBitmap(typeof(TextBox))]
    [Description("Allows the user to enter text and provides editing of multiple strings and masking of password characters.")]
    public partial class CyberTextBox : UserControl
    {
        #region Variables

        private float h = 0;
        private Rectangle rectangle_region = new();
        private GraphicsPath graphicsPath = new();
        private readonly StringFormat stringFormat = new();
        private Size size_cybertextbox = new();
        public TextBox textBox = new();

        #endregion

        #region Property Region

        [Category("Cyber")]
        [Description("Text on textbox")]
        [DefaultValue("CyberTextBox")]
        public string TextButton
        {
            get => textBox.Text;
            set
            {
                textBox.Text = value;
                TextChanged();
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

        private bool tmp_password;
        [Category("Cyber")]
        [DefaultValue(false)]
        [Description("On/Off password display")]
        public bool Password
        {
            get => tmp_password;
            set
            {
                tmp_password = value;
                Update_TextBox(true);
            }
        }

        private char tmp_passwordchar;
        [Category("Cyber")]
        [DefaultValue('●')]
        [Description("Specifies the character to display the password when typing in a single-line edit field")]
        public char PasswordChar
        {
            get => tmp_passwordchar;
            set
            {
                tmp_passwordchar = value;
                Update_TextBox(true);
            }
        }

        private bool tmp_rounding_status;
        [Category("Cyber")]
        [Description("On/Off Rounded Button")]
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

        private Color tmp_color_background;
        [Category("Cyber")]
        [Description("Background color")]
        public Color ColorBackground
        {
            get => tmp_color_background;
            set
            {
                if (value != Color.Transparent && value.A >= 255)
                {
                    tmp_color_background = value;
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

        private StateStyle tmp_cybertextbox_style = StateStyle.Default;
        [Category("Cyber")]
        [Description("TextBox style")]
        public StateStyle CyberTextBoxStyle
        {
            get => tmp_cybertextbox_style;
            set
            {
                tmp_cybertextbox_style = value;
                switch (tmp_cybertextbox_style)
                {
                    case StateStyle.Default:
                        Size = new Size(200, 40);
                        BackColor = Color.Transparent;
                        ForeColor = Color.FromArgb(245, 245, 245);

                        TextButton = "CyberTextBox";
                        RGB = false;
                        Password = false;
                        PasswordChar = '●';
                        Rounding = true;
                        RoundingInt = 60;
                        BackgroundPen = true;
                        Background_WidthPen = 3F;
                        ColorBackground_Pen = Color.FromArgb(29, 200, 238);
                        ColorBackground = Color.FromArgb(37, 52, 68);
                        Timer_RGB = 300;
                        Lighting = false;
                        ColorLighting = Color.FromArgb(29, 200, 238);
                        Alpha = 20;
                        PenWidth = 15;
                        LinearGradientPen = false;
                        ColorPen_1 = Color.FromArgb(29, 200, 238);
                        ColorPen_2 = Color.FromArgb(37, 52, 68);
                        SmoothingMode = SmoothingMode.HighQuality;
                        TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                        Font = HelpEngine.GetDefaultFont();
                        break;
                    case StateStyle.Custom:
                        break;
                    case StateStyle.Random:
                        HelpEngine.GetRandom random = new();
                        ColorBackground = random.ColorArgb();
                        Password = random.Bool();
                        Rounding = random.Bool();

                        if (Rounding)
                        {
                            RoundingInt = random.Int(5, 90);
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

        public CyberTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.StandardDoubleClick, true);
            DoubleBuffered = true;

            Tag = "Cyber";
            CyberTextBoxStyle = StateStyle.Default;
            CyberTextBoxStyle = StateStyle.Custom;

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            textBox.Text = "Cyber Text";
            textBox.TextChanged += TextBox_TextChanged; //
            Update_TextBox(false);
            Controls.Add(textBox);

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

                Update_TextBox(true);
            }
            catch (Exception Ex)
            {
                HelpEngine.Error($"[{Name}] Error: \n{Ex}");
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            int tmp = (int)((BackgroundPen ? Background_WidthPen : 0) + (Lighting ? PenWidth / 4 : 0));
            size_cybertextbox = new Size(Width - (tmp * 2), Height - (tmp * 2));
            rectangle_region = new Rectangle(tmp, tmp, size_cybertextbox.Width, size_cybertextbox.Height);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextButton = textBox.Text;
        }

        public void Update_TextBox(bool Visible)
        {
            textBox.Visible = Visible;
            textBox.Size = new Size((int)(size_cybertextbox.Width - (RoundingInt / 2) - (Background_WidthPen / 2)), size_cybertextbox.Height / 2);
            textBox.Location = new Point((Width / 2) - (textBox.Size.Width / 2), (Height / 2) - (textBox.Size.Height / 2));

            if (ColorBackground.Name != "Transparent")
            {
                textBox.BackColor = ColorBackground;
            }

            textBox.ForeColor = ForeColor;
            textBox.BorderStyle = BorderStyle.None;
            Font = new Font(Font.Name, Height / 4, Font.Style);
            textBox.Font = Font;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.MaxLength = 10000;
            textBox.PasswordChar = Password ? textBox.PasswordChar = PasswordChar : textBox.PasswordChar = '\0';
        }
        #endregion

        #region Event Handler Region

        public delegate void EventHandler();
        [Category("Cyber")]
        [Description("The event is raised when the value of the Text property changes in Control.")]
        public event EventHandler TextChanged = delegate { };

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
                graphics.Clip = new Region(DrawEngine.RoundedRectangle(new Rectangle(
                    rectangle_region.X - (int)(2 + Background_WidthPen),
                    rectangle_region.Y - (int)(2 + Background_WidthPen),
                    rectangle_region.Width + ((int)(2 + Background_WidthPen) * 2),
                    rectangle_region.Height + ((int)(2 + Background_WidthPen) * 2)), Rounding ? roundingValue : 0.1F));

                //Background
                graphics.FillPath(new SolidBrush(ColorBackground), graphicsPath);

                return bitmap;
            }

            BaseLoading();

            graphics_form.DrawImage(Layer_1(), new PointF(0, 0));
            graphics_form.DrawImage(Layer_2(), new PointF(0, 0));
        }

        #endregion
    }

    #endregion
}