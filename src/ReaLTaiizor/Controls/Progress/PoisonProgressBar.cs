#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Extension.Poison;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonProgressBar

    [Designer(typeof(PoisonProgressBarDesigner))]
    [ToolboxBitmap(typeof(ProgressBar))]
    public class PoisonProgressBar : ProgressBar, IPoisonControl
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
        public new ColorStyle Style
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

        private PoisonStyleManager poisonStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get => poisonStyleManager;
            set => poisonStyleManager = value;
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get => useCustomBackColor;
            set => useCustomBackColor = value;
        }

        private bool useCustomForeColor = false;
        [Browsable(false)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseCustomForeColor
        {
            get => useCustomForeColor;
            set => useCustomForeColor = value;
        }

        private bool useStyleColors = true;
        [Browsable(false)]
        [DefaultValue(true)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseStyleColors
        {
            get => useStyleColors;
            set => useStyleColors = value;
        }

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

        private PoisonProgressBarSize poisonLabelSize = PoisonProgressBarSize.Medium;
        [DefaultValue(PoisonProgressBarSize.Medium)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonProgressBarSize FontSize
        {
            get => poisonLabelSize;
            set => poisonLabelSize = value;
        }

        private PoisonProgressBarWeight poisonLabelWeight = PoisonProgressBarWeight.Light;
        [DefaultValue(PoisonProgressBarWeight.Light)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonProgressBarWeight FontWeight
        {
            get => poisonLabelWeight;
            set => poisonLabelWeight = value;
        }

        private ContentAlignment textAlign = ContentAlignment.MiddleRight;
        [DefaultValue(ContentAlignment.MiddleRight)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ContentAlignment TextAlign
        {
            get => textAlign;
            set => textAlign = value;
        }

        private bool hideProgressText = true;
        [DefaultValue(true)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HideProgressText
        {
            get => hideProgressText;
            set => hideProgressText = value;
        }

        private ProgressBarStyle progressBarStyle = ProgressBarStyle.Continuous;
        [DefaultValue(ProgressBarStyle.Continuous)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ProgressBarStyle ProgressBarStyle
        {
            get => progressBarStyle;
            set => progressBarStyle = value;
        }

        public new int Value
        {
            get => base.Value;
            set { if (value > Maximum) { return; } base.Value = value; Invalidate(); }
        }

        [Browsable(false)]
        public double ProgressTotalPercent => ((1 - (double)(Maximum - Value) / Maximum) * 100);

        [Browsable(false)]
        public double ProgressTotalValue => (1 - (double)(Maximum - Value) / Maximum);

        [Browsable(false)]
        public string ProgressPercentText => (string.Format("{0}%", Math.Round(ProgressTotalPercent)));

        private double ProgressBarWidth => (((double)Value / Maximum) * ClientRectangle.Width);

        private int ProgressBarMarqueeWidth => (ClientRectangle.Width / 3);

        #endregion

        #region Constructor

        public PoisonProgressBar()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!useCustomBackColor)
                {
                    if (!Enabled)
                    {
                        backColor = PoisonPaint.BackColor.ProgressBar.Bar.Disabled(Theme);
                    }
                    else
                    {
                        backColor = PoisonPaint.BackColor.ProgressBar.Bar.Normal(Theme);
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
            if (progressBarStyle == ProgressBarStyle.Continuous)
            {
                if (!DesignMode)
                {
                    StopTimer();
                }

                DrawProgressContinuous(e.Graphics);
            }
            else if (progressBarStyle == ProgressBarStyle.Blocks)
            {
                if (!DesignMode)
                {
                    StopTimer();
                }

                DrawProgressContinuous(e.Graphics);
            }
            else if (progressBarStyle == ProgressBarStyle.Marquee)
            {
                if (!DesignMode && Enabled)
                {
                    StartTimer();
                }

                if (!Enabled)
                {
                    StopTimer();
                }

                if (Value == Maximum)
                {
                    StopTimer();
                    DrawProgressContinuous(e.Graphics);
                }
                else
                {
                    DrawProgressMarquee(e.Graphics);
                }
            }

            DrawProgressText(e.Graphics);

            using (Pen p = new(PoisonPaint.BorderColor.ProgressBar.Normal(Theme)))
            {
                Rectangle borderRect = new(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(p, borderRect);
            }

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
        }

        private void DrawProgressContinuous(Graphics graphics)
        {
            graphics.FillRectangle(PoisonPaint.GetStyleBrush(Style), 0, 0, (int)ProgressBarWidth, ClientRectangle.Height);
        }

        private int marqueeX = 0;

        private void DrawProgressMarquee(Graphics graphics)
        {
            graphics.FillRectangle(PoisonPaint.GetStyleBrush(Style), marqueeX, 0, ProgressBarMarqueeWidth, ClientRectangle.Height);
        }

        private void DrawProgressText(Graphics graphics)
        {
            if (HideProgressText)
            {
                return;
            }

            Color foreColor;

            if (!Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ProgressBar.Disabled(Theme);
            }
            else
            {
                foreColor = PoisonPaint.ForeColor.ProgressBar.Normal(Theme);
            }

            TextRenderer.DrawText(graphics, ProgressPercentText, PoisonFonts.ProgressBar(poisonLabelSize, poisonLabelWeight), ClientRectangle, foreColor, PoisonPaint.GetTextFormatFlags(TextAlign));
        }

        #endregion

        #region Overridden Methods

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (Graphics g = CreateGraphics())
            {
                proposedSize = new(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, ProgressPercentText, PoisonFonts.ProgressBar(poisonLabelSize, poisonLabelWeight), proposedSize, PoisonPaint.GetTextFormatFlags(TextAlign));
            }

            return preferredSize;
        }

        #endregion

        #region Private Methods

        private Timer marqueeTimer;
        private bool marqueeTimerEnabled => marqueeTimer != null && marqueeTimer.Enabled;

        private void StartTimer()
        {
            if (marqueeTimerEnabled)
            {
                return;
            }

            if (marqueeTimer == null)
            {
                marqueeTimer = new Timer { Interval = 10 };
                marqueeTimer.Tick += marqueeTimer_Tick;
            }

            marqueeX = -ProgressBarMarqueeWidth;

            marqueeTimer.Stop();
            marqueeTimer.Start();

            marqueeTimer.Enabled = true;

            Invalidate();
        }

        private void StopTimer()
        {
            if (marqueeTimer == null)
            {
                return;
            }

            marqueeTimer.Stop();

            Invalidate();
        }

        private void marqueeTimer_Tick(object sender, EventArgs e)
        {
            marqueeX++;

            if (marqueeX > ClientRectangle.Width)
            {
                marqueeX = -ProgressBarMarqueeWidth;
            }

            Invalidate();
        }

        #endregion
    }

    #endregion
}