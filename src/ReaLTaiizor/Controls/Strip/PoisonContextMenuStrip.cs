#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Interface.Poison;

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
                CustomPaintBackground(this, e);
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
                CustomPaint(this, e);
        }

        [Category("Poison Appearance")]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
                CustomPaintForeground(this, e);
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category("Poison Appearance")]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                    return poisonStyle;

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                    return StyleManager.Style;
                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                    return ColorStyle.Blue;

                return poisonStyle;
            }
            set { poisonStyle = value; }
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category("Poison Appearance")]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                    return poisonTheme;

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                    return StyleManager.Theme;
                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                    return ThemeStyle.Light;

                return poisonTheme;
            }
            set { poisonTheme = value; }
        }

        private PoisonStyleManager poisonStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get { return poisonStyleManager; }
            set
            {
                poisonStyleManager = value;
                settheme();
            }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category("Poison Appearance")]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category("Poison Behaviour")]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        #endregion

        #region Constructor

        public PoisonContextMenuStrip(IContainer Container)
        {
            if (Container != null)
                Container.Add(this);
        }

        private void settheme()
        {
            BackColor = PoisonPaint.BackColor.Form(Theme);
            ForeColor = PoisonPaint.ForeColor.Button.Normal(Theme);
            Renderer = new PoisonCTXRenderer(Theme, Style);
        }

        private class PoisonCTXRenderer : ToolStripProfessionalRenderer
        {
            ThemeStyle _theme;
            public PoisonCTXRenderer(ThemeStyle Theme, ColorStyle Style) : base(new contextcolors(Theme, Style))
            {
                _theme = Theme;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = PoisonPaint.ForeColor.Button.Normal(_theme);
                base.OnRenderItemText(e);
            }
        }

        private class contextcolors : ProfessionalColorTable
        {
            ThemeStyle _theme = ThemeStyle.Light;
            ColorStyle _style = ColorStyle.Blue;

            public contextcolors(ThemeStyle Theme, ColorStyle Style)
            {
                _theme = Theme;
                _style = Style;
            }

            public override Color MenuItemSelected
            {
                get { return PoisonPaint.GetStyleColor(_style); }
            }

            public override Color MenuBorder
            {
                get { return PoisonPaint.BackColor.Form(_theme); }
            }

            public override Color ToolStripBorder
            {
                get { return PoisonPaint.GetStyleColor(_style); }
            }

            public override Color MenuItemBorder
            {
                get { return PoisonPaint.GetStyleColor(_style); }
            }

            public override Color ToolStripDropDownBackground
            {
                get { return PoisonPaint.BackColor.Form(_theme); }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return PoisonPaint.BackColor.Form(_theme); }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return PoisonPaint.BackColor.Form(_theme); }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return PoisonPaint.BackColor.Form(_theme); }
            }
        }

        #endregion
    }

    #endregion
}