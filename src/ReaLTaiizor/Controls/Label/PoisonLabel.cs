#region Imports

using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Extension.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonLabel

    [Designer(typeof(PoisonLabelDesigner))]
    [ToolboxBitmap(typeof(Label))]
    public class PoisonLabel : Label, IPoisonControl
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

        private readonly DoubleBufferedTextBox baseTextBox;

        private PoisonLabelSize poisonLabelSize = PoisonLabelSize.Medium;
        [DefaultValue(PoisonLabelSize.Medium)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonLabelSize FontSize
        {
            get => poisonLabelSize;
            set { poisonLabelSize = value; Refresh(); }
        }

        private PoisonLabelWeight poisonLabelWeight = PoisonLabelWeight.Light;
        [DefaultValue(PoisonLabelWeight.Light)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonLabelWeight FontWeight
        {
            get => poisonLabelWeight;
            set { poisonLabelWeight = value; Refresh(); }
        }

        [DefaultValue(PoisonLabelModeType.Default)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonLabelModeType LabelMode { get; set; } = PoisonLabelModeType.Default;

        private bool wrapToLine;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        public bool WrapToLine
        {
            get => wrapToLine;
            set { wrapToLine = value; Refresh(); }
        }

        #endregion

        #region Constructor

        public PoisonLabel()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );

            baseTextBox = new DoubleBufferedTextBox
            {
                Visible = false
            };
            Controls.Add(baseTextBox);
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
                    backColor = PoisonPaint.BackColor.Form(Theme);
                    if (Parent is PoisonTile)
                    {
                        backColor = PoisonPaint.GetStyleColor(Style);
                    }
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
            Color foreColor;

            if (UseCustomForeColor)
            {
                foreColor = ForeColor;
            }
            else
            {
                if (!Enabled)
                {
                    if (Parent != null)
                    {
                        if (Parent is PoisonTile)
                        {
                            foreColor = PoisonPaint.ForeColor.Tile.Disabled(Theme);
                        }
                        else
                        {
                            foreColor = PoisonPaint.ForeColor.Label.Normal(Theme);
                        }
                    }
                    else
                    {
                        foreColor = PoisonPaint.ForeColor.Label.Disabled(Theme);
                    }
                }
                else
                {
                    if (Parent != null)
                    {
                        if (Parent is PoisonTile)
                        {
                            foreColor = PoisonPaint.ForeColor.Tile.Normal(Theme);
                        }
                        else
                        {
                            if (UseStyleColors)
                            {
                                foreColor = PoisonPaint.GetStyleColor(Style);
                            }
                            else
                            {
                                foreColor = PoisonPaint.ForeColor.Label.Normal(Theme);
                            }
                        }
                    }
                    else
                    {
                        if (UseStyleColors)
                        {
                            foreColor = PoisonPaint.GetStyleColor(Style);
                        }
                        else
                        {
                            foreColor = PoisonPaint.ForeColor.Label.Normal(Theme);
                        }
                    }
                }
            }

            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                CreateBaseTextBox();
                UpdateBaseTextBox();

                if (!baseTextBox.Visible)
                {
                    TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.Label(poisonLabelSize, poisonLabelWeight), ClientRectangle, foreColor, PoisonPaint.GetTextFormatFlags(TextAlign));
                }
            }
            else
            {
                DestroyBaseTextbox();
                TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.Label(poisonLabelSize, poisonLabelWeight), ClientRectangle, foreColor, PoisonPaint.GetTextFormatFlags(TextAlign, wrapToLine));
                OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));
            }
        }

        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                UpdateBaseTextBox();
            }

            base.Refresh();
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (Graphics g = CreateGraphics())
            {
                proposedSize = new(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, Text, PoisonFonts.Label(poisonLabelSize, poisonLabelWeight), proposedSize, PoisonPaint.GetTextFormatFlags(TextAlign));
            }

            return preferredSize;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                HideBaseTextBox();
            }

            base.OnResize(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                ShowBaseTextBox();
            }
        }

        #endregion

        #region Label Selection Mode

        private class DoubleBufferedTextBox : TextBox
        {
            public DoubleBufferedTextBox()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            }
        }

        private bool firstInitialization = true;

        private void CreateBaseTextBox()
        {
            if (baseTextBox.Visible && !firstInitialization)
            {
                return;
            }

            if (!firstInitialization)
            {
                return;
            }

            firstInitialization = false;

            if (!DesignMode)
            {
                Form parentForm = FindForm();
                if (parentForm != null)
                {
                    parentForm.ResizeBegin += new EventHandler(parentForm_ResizeBegin);
                    parentForm.ResizeEnd += new EventHandler(parentForm_ResizeEnd);
                }
            }

            baseTextBox.BackColor = Color.Transparent;
            baseTextBox.Visible = true;
            baseTextBox.BorderStyle = BorderStyle.None;
            baseTextBox.Font = PoisonFonts.Label(poisonLabelSize, poisonLabelWeight);
            baseTextBox.Location = new(1, 0);
            baseTextBox.Text = Text;
            baseTextBox.ReadOnly = true;

            baseTextBox.Size = GetPreferredSize(Size.Empty);
            baseTextBox.Multiline = true;

            baseTextBox.DoubleClick += BaseTextBoxOnDoubleClick;
            baseTextBox.Click += BaseTextBoxOnClick;

            Controls.Add(baseTextBox);
        }

        private void parentForm_ResizeEnd(object sender, EventArgs e)
        {
            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                ShowBaseTextBox();
            }
        }

        private void parentForm_ResizeBegin(object sender, EventArgs e)
        {
            if (LabelMode == PoisonLabelModeType.Selectable)
            {
                HideBaseTextBox();
            }
        }

        private void DestroyBaseTextbox()
        {
            if (!baseTextBox.Visible)
            {
                return;
            }

            baseTextBox.DoubleClick -= BaseTextBoxOnDoubleClick;
            baseTextBox.Click -= BaseTextBoxOnClick;
            baseTextBox.Visible = false;
        }

        private void UpdateBaseTextBox()
        {
            if (!baseTextBox.Visible)
            {
                return;
            }

            SuspendLayout();
            baseTextBox.SuspendLayout();

            if (UseCustomBackColor)
            {
                baseTextBox.BackColor = BackColor;
            }
            else
            {
                baseTextBox.BackColor = PoisonPaint.BackColor.Form(Theme);
            }

            if (!Enabled)
            {
                if (Parent != null)
                {
                    if (Parent is PoisonTile)
                    {
                        baseTextBox.ForeColor = PoisonPaint.ForeColor.Tile.Disabled(Theme);
                    }
                    else
                    {
                        if (UseStyleColors)
                        {
                            baseTextBox.ForeColor = PoisonPaint.GetStyleColor(Style);
                        }
                        else
                        {
                            baseTextBox.ForeColor = PoisonPaint.ForeColor.Label.Disabled(Theme);
                        }
                    }
                }
                else
                {
                    if (UseStyleColors)
                    {
                        baseTextBox.ForeColor = PoisonPaint.GetStyleColor(Style);
                    }
                    else
                    {
                        baseTextBox.ForeColor = PoisonPaint.ForeColor.Label.Disabled(Theme);
                    }
                }
            }
            else
            {
                if (Parent != null)
                {
                    if (Parent is PoisonTile)
                    {
                        baseTextBox.ForeColor = PoisonPaint.ForeColor.Tile.Normal(Theme);
                    }
                    else
                    {
                        if (UseStyleColors)
                        {
                            baseTextBox.ForeColor = PoisonPaint.GetStyleColor(Style);
                        }
                        else
                        {
                            baseTextBox.ForeColor = PoisonPaint.ForeColor.Label.Normal(Theme);
                        }
                    }
                }
                else
                {
                    if (UseStyleColors)
                    {
                        baseTextBox.ForeColor = PoisonPaint.GetStyleColor(Style);
                    }
                    else
                    {
                        baseTextBox.ForeColor = PoisonPaint.ForeColor.Label.Normal(Theme);
                    }
                }
            }

            baseTextBox.Font = PoisonFonts.Label(poisonLabelSize, poisonLabelWeight);
            baseTextBox.Text = Text;
            baseTextBox.BorderStyle = BorderStyle.None;

            Size = GetPreferredSize(Size.Empty);

            baseTextBox.ResumeLayout();
            ResumeLayout();
        }

        private void HideBaseTextBox()
        {
            baseTextBox.Visible = false;
        }

        private void ShowBaseTextBox()
        {
            baseTextBox.Visible = true;
        }

        [SecuritySafeCritical]
        private void BaseTextBoxOnClick(object sender, EventArgs eventArgs)
        {
            Native.WinCaret.HideCaret(baseTextBox.Handle);
        }

        [SecuritySafeCritical]
        private void BaseTextBoxOnDoubleClick(object sender, EventArgs eventArgs)
        {
            baseTextBox.SelectAll();
            Native.WinCaret.HideCaret(baseTextBox.Handle);
        }

        #endregion
    }

    #endregion
}