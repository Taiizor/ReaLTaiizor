#region Imports

using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonProgressSpinner

    [Designer(typeof(PoisonProgressSpinnerDesigner))]
    [ToolboxBitmap(typeof(ProgressBar))]
    public class PoisonProgressSpinner : Control, IPoisonControl
    {
        #region Interface

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                {
                    return poisonStyle;
                }

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                {
                    return StyleManager.Style;
                }

                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                {
                    return PoisonDefaults.Style;
                }

                return poisonStyle;
            }
            set => poisonStyle = value;
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                {
                    return poisonTheme;
                }

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }

                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                {
                    return PoisonDefaults.Theme;
                }

                return poisonTheme;
            }
            set => poisonTheme = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager { get; set; } = null;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor { get; set; } = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

        #region Fields

        private readonly Timer timer;
        private int progress;
        private float angle = 270;

        [DefaultValue(true)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        public bool Spinning
        {
            get => timer.Enabled;
            set => timer.Enabled = value;
        }

        [DefaultValue(0)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int Value
        {
            get => progress;
            set
            {
                if (value != -1 && (value < minimum || value > maximum))
                {
                    throw new ArgumentOutOfRangeException("Progress value must be -1 or between Minimum and Maximum.", (Exception)null);
                }

                progress = value;
                Refresh();
            }
        }

        private int minimum = 0;
        [DefaultValue(0)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int Minimum
        {
            get => minimum;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Minimum value must be >= 0.", (Exception)null);
                }

                if (value >= maximum)
                {
                    throw new ArgumentOutOfRangeException("Minimum value must be < Maximum.", (Exception)null);
                }

                minimum = value;
                if (progress != -1 && progress < minimum)
                {
                    progress = minimum;
                }

                Refresh();
            }
        }

        private int maximum = 100;
        [DefaultValue(0)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int Maximum
        {
            get => maximum;
            set
            {
                if (value <= minimum)
                {
                    throw new ArgumentOutOfRangeException("Maximum value must be > Minimum.", (Exception)null);
                }

                maximum = value;
                if (progress > maximum)
                {
                    progress = maximum;
                }

                Refresh();
            }
        }

        private bool ensureVisible = true;
        [DefaultValue(true)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool EnsureVisible
        {
            get => ensureVisible;
            set { ensureVisible = value; Refresh(); }
        }

        private float speed;
        [DefaultValue(1f)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        public float Speed
        {
            get => speed;
            set
            {
                if (value is <= 0 or > 10)
                {
                    throw new ArgumentOutOfRangeException("Speed value must be > 0 and <= 10.", (Exception)null);
                }

                speed = value;
            }
        }

        private bool backwards;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        public bool Backwards
        {
            get => backwards;
            set { backwards = value; Refresh(); }
        }

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool CustomBackground { get; set; } = false;

        #endregion

        #region Constructor

        public PoisonProgressSpinner()
        {
            timer = new Timer
            {
                Interval = 20
            };
            timer.Tick += timer_Tick;
            timer.Enabled = true;

            Width = 16;
            Height = 16;
            speed = 1;
            DoubleBuffered = true;
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            progress = minimum;
            angle = 270;
            Refresh();
        }

        #endregion

        #region Management Methods

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                angle += 6f * speed * (backwards ? -1 : 1);
                Refresh();
            }
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    if (Parent is PoisonTile)
                    {
                        backColor = PoisonPaint.GetStyleColor(Style);
                    }
                    else
                    {
                        backColor = PoisonPaint.BackColor.Form(Theme);
                    }
                }

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new PoisonPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            Color foreColor;

            if (CustomBackground)
            {
                foreColor = PoisonPaint.GetStyleColor(Style);
            }
            else
            {
                if (Parent is PoisonTile)
                {
                    foreColor = PoisonPaint.ForeColor.Tile.Normal(Theme);
                }
                else
                {
                    foreColor = PoisonPaint.GetStyleColor(Style);
                }
            }

            using (Pen forePen = new(foreColor, (float)Width / 5))
            {
                int padding = (int)Math.Ceiling((float)Width / 10);

                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                if (progress != -1)
                {
                    float sweepAngle;
                    float progFrac = (progress - minimum) / (float)(maximum - minimum);

                    if (ensureVisible)
                    {
                        sweepAngle = 30 + (300f * progFrac);
                    }
                    else
                    {
                        sweepAngle = 360f * progFrac;
                    }

                    if (backwards)
                    {
                        sweepAngle = -sweepAngle;
                    }

                    e.Graphics.DrawArc(forePen, padding, padding, Width - (2 * padding) - 1, Height - (2 * padding) - 1, angle, sweepAngle);
                }
                else
                {
                    const int maxOffset = 180;
                    for (int offset = 0; offset <= maxOffset; offset += 15)
                    {
                        int alpha = 290 - (offset * 290 / maxOffset);

                        if (alpha > 255)
                        {
                            alpha = 255;
                        }

                        if (alpha < 0)
                        {
                            alpha = 0;
                        }

                        Color col = Color.FromArgb(alpha, forePen.Color);
                        using Pen gradPen = new(col, forePen.Width);
                        float startAngle = angle + ((offset - (ensureVisible ? 30 : 0)) * (backwards ? 1 : -1));
                        float sweepAngle = 15 * (backwards ? 1 : -1);
                        e.Graphics.DrawArc(gradPen, padding, padding, Width - (2 * padding) - 1, Height - (2 * padding) - 1, startAngle, sweepAngle);
                    }
                }
            }

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));
        }

        #endregion
    }

    #endregion
}