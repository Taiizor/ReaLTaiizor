#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonContextMenuStrip

    public class PoisonContextMenuStrip : ContextMenuStrip, IPoisonControl
    {
        #region Interface

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        [Category("Poison Appearance")]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || field != ColorStyle.Default)
                {
                    return field;
                }

                if (StyleManager != null && field == ColorStyle.Default)
                {
                    return StyleManager.Style;
                }

                if (StyleManager == null && field == ColorStyle.Default)
                {
                    return ColorStyle.Blue;
                }

                return field;
            }
            set;
        } = ColorStyle.Default;

        [Category("Poison Appearance")]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || field != ThemeStyle.Default)
                {
                    return field;
                }

                if (StyleManager != null && field == ThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }

                if (StyleManager == null && field == ThemeStyle.Default)
                {
                    return ThemeStyle.Light;
                }

                return field;
            }
            set;
        } = ThemeStyle.Default;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get;
            set
            {
                field = value;
                settheme();
            }
        } = null;

        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomBackColor { get; set; } = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomForeColor { get; set; } = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category("Poison Behaviour")]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

        #region Constructor

        public PoisonContextMenuStrip(IContainer Container)
        {
            if (Container != null)
            {
                Container.Add(this);
            }
        }

        private void settheme()
        {
            BackColor = PoisonPaint.BackColor.Form(Theme);
            ForeColor = PoisonPaint.ForeColor.Button.Normal(Theme);
            Renderer = new PoisonCTXRenderer(Theme, Style);
        }

        private class PoisonCTXRenderer : ToolStripProfessionalRenderer
        {
            private readonly ThemeStyle _theme;
            public PoisonCTXRenderer(ThemeStyle Theme, ColorStyle Style) : base(new ContextColors(Theme, Style))
            {
                _theme = Theme;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = PoisonPaint.ForeColor.Button.Normal(_theme);
                base.OnRenderItemText(e);
            }
        }

        private class ContextColors : ProfessionalColorTable
        {
            private readonly ThemeStyle _theme = ThemeStyle.Light;
            private readonly ColorStyle _style = ColorStyle.Blue;

            public ContextColors(ThemeStyle Theme, ColorStyle Style)
            {
                _theme = Theme;
                _style = Style;
            }

            public override Color MenuItemSelected => PoisonPaint.GetStyleColor(_style);

            public override Color MenuBorder => PoisonPaint.BackColor.Form(_theme);

            public override Color ToolStripBorder => PoisonPaint.GetStyleColor(_style);

            public override Color MenuItemBorder => PoisonPaint.GetStyleColor(_style);

            public override Color ToolStripDropDownBackground => PoisonPaint.BackColor.Form(_theme);

            public override Color ImageMarginGradientBegin => PoisonPaint.BackColor.Form(_theme);

            public override Color ImageMarginGradientMiddle => PoisonPaint.BackColor.Form(_theme);

            public override Color ImageMarginGradientEnd => PoisonPaint.BackColor.Form(_theme);
        }

        #endregion
    }

    #endregion
}