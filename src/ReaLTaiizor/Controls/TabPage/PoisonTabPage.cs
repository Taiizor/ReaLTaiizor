#region Imports

using ReaLTaiizor.Design.Poison;
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
    #region PoisonTabPage

    [ToolboxItem(false)]
    [Designer(typeof(PoisonTabPageDesigner))]
    public class PoisonTabPage : System.Windows.Forms.TabPage, IPoisonControl
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

        private readonly PoisonScrollBar verticalScrollbar = new(ScrollOrientationType.Vertical);
        private readonly PoisonScrollBar horizontalScrollbar = new(ScrollOrientationType.Horizontal);

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbar { get; set; } = false;

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int HorizontalScrollbarSize
        {
            get => horizontalScrollbar.ScrollbarSize;
            set => horizontalScrollbar.ScrollbarSize = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbarBarColor
        {
            get => horizontalScrollbar.UseBarColor;
            set => horizontalScrollbar.UseBarColor = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HorizontalScrollbarHighlightOnWheel
        {
            get => horizontalScrollbar.HighlightOnWheel;
            set => horizontalScrollbar.HighlightOnWheel = value;
        }
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbar { get; set; } = false;

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int VerticalScrollbarSize
        {
            get => verticalScrollbar.ScrollbarSize;
            set => verticalScrollbar.ScrollbarSize = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbarBarColor
        {
            get => verticalScrollbar.UseBarColor;
            set => verticalScrollbar.UseBarColor = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool VerticalScrollbarHighlightOnWheel
        {
            get => verticalScrollbar.HighlightOnWheel;
            set => verticalScrollbar.HighlightOnWheel = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public new bool AutoScroll
        {
            get => base.AutoScroll;
            set
            {
                if (value)
                {
                    HorizontalScrollbar = true;
                    VerticalScrollbar = true;
                }

                base.AutoScroll = value;
            }
        }

        #endregion

        #region Constructor

        public PoisonTabPage()
        {
            SetStyle
            (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );

            Controls.Add(verticalScrollbar);
            Controls.Add(horizontalScrollbar);

            verticalScrollbar.UseBarColor = true;
            horizontalScrollbar.UseBarColor = true;

            verticalScrollbar.Scroll += VerticalScrollbarScroll;
            horizontalScrollbar.Scroll += HorizontalScrollbarScroll;
        }

        #endregion

        #region Scroll Events

        private void HorizontalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new(e.NewValue, verticalScrollbar.Value);
            UpdateScrollBarPositions();
        }

        private void VerticalScrollbarScroll(object sender, ScrollEventArgs e)
        {
            AutoScrollPosition = new(horizontalScrollbar.Value, e.NewValue);
            UpdateScrollBarPositions();
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
            base.OnPaint(e);

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
                horizontalScrollbar.Visible = false;
                verticalScrollbar.Visible = false;
                return;
            }

            UpdateScrollBarPositions();

            if (HorizontalScrollbar)
            {
                horizontalScrollbar.Visible = HorizontalScroll.Visible;
            }

            if (HorizontalScroll.Visible)
            {
                horizontalScrollbar.Minimum = HorizontalScroll.Minimum;
                horizontalScrollbar.Maximum = HorizontalScroll.Maximum;
                horizontalScrollbar.SmallChange = HorizontalScroll.SmallChange;
                horizontalScrollbar.LargeChange = HorizontalScroll.LargeChange;
            }

            if (VerticalScrollbar)
            {
                verticalScrollbar.Visible = VerticalScroll.Visible;
            }

            if (VerticalScroll.Visible)
            {
                verticalScrollbar.Minimum = VerticalScroll.Minimum;
                verticalScrollbar.Maximum = VerticalScroll.Maximum;
                verticalScrollbar.SmallChange = VerticalScroll.SmallChange;
                verticalScrollbar.LargeChange = VerticalScroll.LargeChange;
            }

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            verticalScrollbar.Value = VerticalScroll.Value;
            horizontalScrollbar.Value = HorizontalScroll.Value;
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
                verticalScrollbar.Visible = false;
                horizontalScrollbar.Visible = false;
                return;
            }

            verticalScrollbar.Location = new(ClientRectangle.Width - verticalScrollbar.Width, ClientRectangle.Y);
            verticalScrollbar.Height = ClientRectangle.Height;

            if (!VerticalScrollbar)
            {
                verticalScrollbar.Visible = false;
            }

            horizontalScrollbar.Location = new(ClientRectangle.X, ClientRectangle.Height - horizontalScrollbar.Height);
            horizontalScrollbar.Width = ClientRectangle.Width;

            if (!HorizontalScrollbar)
            {
                horizontalScrollbar.Visible = false;
            }
        }

        #endregion
    }

    #endregion
}