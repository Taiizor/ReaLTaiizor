#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonPanel

    [ToolboxBitmap(typeof(Panel))]
    public class PoisonPanel : System.Windows.Forms.Panel, IPoisonControl
    {
        #region Interface

        [Category("Action")]
        public event EventHandler<ScrollEventArgs> VerticalScrolled;

        [Category("Action")]
        public event EventHandler<ScrollEventArgs> HorizontalScrolled;

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

        public PoisonScrollBar VerticalPoisonScrollbar = new(ScrollOrientationType.Vertical);
        private readonly PoisonScrollBar HorizontalPoisonScrollbar = new(ScrollOrientationType.Horizontal);

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbar { get; set; } = false;

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int HorizontalScrollbarSize
        {
            get => HorizontalPoisonScrollbar.ScrollbarSize;
            set => HorizontalPoisonScrollbar.ScrollbarSize = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbarBarColor
        {
            get => HorizontalPoisonScrollbar.UseBarColor;
            set => HorizontalPoisonScrollbar.UseBarColor = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbarHighlightOnWheel
        {
            get => HorizontalPoisonScrollbar.HighlightOnWheel;
            set => HorizontalPoisonScrollbar.HighlightOnWheel = value;
        }
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbar { get; set; } = false;

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int VerticalScrollbarSize
        {
            get => VerticalPoisonScrollbar.ScrollbarSize;
            set => VerticalPoisonScrollbar.ScrollbarSize = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbarBarColor
        {
            get => VerticalPoisonScrollbar.UseBarColor;
            set => VerticalPoisonScrollbar.UseBarColor = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbarHighlightOnWheel
        {
            get => VerticalPoisonScrollbar.HighlightOnWheel;
            set => VerticalPoisonScrollbar.HighlightOnWheel = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set
            {
                HorizontalScrollbar = value;
                VerticalScrollbar = value;

                base.AutoScroll = value;
            }
        }

        #endregion

        #region Constructor

        public PoisonPanel()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                     true
            );

            Controls.Add(VerticalPoisonScrollbar);
            Controls.Add(HorizontalPoisonScrollbar);

            VerticalPoisonScrollbar.UseBarColor = true;
            HorizontalPoisonScrollbar.UseBarColor = true;

            VerticalPoisonScrollbar.Visible = false;
            HorizontalPoisonScrollbar.Visible = false;

            VerticalPoisonScrollbar.Scroll += VerticalScrollbarScroll;
            HorizontalPoisonScrollbar.Scroll += HorizontalScrollbarScroll;
        }

        #endregion

        #region Scroll Events

        private void HorizontalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new(e.NewValue, VerticalPoisonScrollbar.Value);
            UpdateScrollBarPositions();
            HorizontalScrolled?.Invoke(this, e);
        }

        private void VerticalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new(HorizontalPoisonScrollbar.Value, e.NewValue);
            UpdateScrollBarPositions();
            VerticalScrolled?.Invoke(this, e);
        }

        #endregion

        #region Overridden Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                }

                if (backColor.A == 255 && BackgroundImage == null)
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
            if (DesignMode)
            {
                HorizontalPoisonScrollbar.Visible = false;
                VerticalPoisonScrollbar.Visible = false;
                return;
            }

            UpdateScrollBarPositions();

            if (HorizontalScrollbar)
            {
                HorizontalPoisonScrollbar.Visible = HorizontalScroll.Visible;
            }

            if (HorizontalScroll.Visible)
            {
                HorizontalPoisonScrollbar.Minimum = HorizontalScroll.Minimum;
                HorizontalPoisonScrollbar.Maximum = HorizontalScroll.Maximum;
                HorizontalPoisonScrollbar.SmallChange = HorizontalScroll.SmallChange;
                HorizontalPoisonScrollbar.LargeChange = HorizontalScroll.LargeChange;
            }

            if (VerticalScrollbar)
            {
                VerticalPoisonScrollbar.Visible = VerticalScroll.Visible;
            }

            if (VerticalScroll.Visible)
            {
                VerticalPoisonScrollbar.Minimum = VerticalScroll.Minimum;
                VerticalPoisonScrollbar.Maximum = VerticalScroll.Maximum;
                VerticalPoisonScrollbar.SmallChange = VerticalScroll.SmallChange;
                VerticalPoisonScrollbar.LargeChange = VerticalScroll.LargeChange;
            }

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            VerticalPoisonScrollbar.Value = Math.Abs(VerticalScroll.Value);
            HorizontalPoisonScrollbar.Value = Math.Abs(HorizontalScroll.Value);
        }

        [SecuritySafeCritical]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!DesignMode)
            {
                WinApi.ShowScrollBar(Handle, (int)WinApi.ScrollBar.SB_BOTH, 0);
            }
        }

        #endregion

        #region Management Methods

        private void UpdateScrollBarPositions()
        {
            if (DesignMode)
            {
                return;
            }

            if (!AutoScroll)
            {
                VerticalPoisonScrollbar.Visible = false;
                HorizontalPoisonScrollbar.Visible = false;
                return;
            }

            int _horizontalScrollHeight = HorizontalPoisonScrollbar.Height;
            int _verticalScrollWidth = VerticalPoisonScrollbar.Width;

            VerticalPoisonScrollbar.Location = new(ClientRectangle.Width - VerticalPoisonScrollbar.Width, ClientRectangle.Y);

            if (!VerticalScrollbar || !VerticalScroll.Visible)
            {
                VerticalPoisonScrollbar.Visible = false;
                _verticalScrollWidth = 0;
            }

            HorizontalPoisonScrollbar.Location = new(ClientRectangle.X, ClientRectangle.Height - HorizontalPoisonScrollbar.Height);

            if (!HorizontalScrollbar || !HorizontalScroll.Visible)
            {
                HorizontalPoisonScrollbar.Visible = false;
                _horizontalScrollHeight = 0;
            }

            VerticalPoisonScrollbar.Height = ClientRectangle.Height - _horizontalScrollHeight;
            HorizontalPoisonScrollbar.Width = ClientRectangle.Width - _verticalScrollWidth;
        }

        #endregion
    }

    #endregion
}