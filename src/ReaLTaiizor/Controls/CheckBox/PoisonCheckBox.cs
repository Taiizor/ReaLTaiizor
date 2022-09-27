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
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonCheckBox

    [Designer(typeof(PoisonCheckBoxDesigner))]
    [ToolboxBitmap(typeof(System.Windows.Forms.CheckBox))]
    public class PoisonCheckBox : System.Windows.Forms.CheckBox, IPoisonControl
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

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool DisplayFocus { get; set; } = false;
        [DefaultValue(PoisonCheckBoxSize.Small)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonCheckBoxSize FontSize { get; set; } = PoisonCheckBoxSize.Small;
        [DefaultValue(PoisonCheckBoxWeight.Regular)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonCheckBoxWeight FontWeight { get; set; } = PoisonCheckBoxWeight.Regular;

        [Browsable(false)]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public PoisonCheckBox()
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

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                    if (Parent is PoisonTile)
                    {
                        backColor = PoisonPaint.GetStyleColor(Style);
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
            Color borderColor, foreColor;

            if (UseCustomForeColor)
            {
                foreColor = ForeColor;

                if (isHovered && !isPressed && Enabled)
                {
                    borderColor = PoisonPaint.BorderColor.CheckBox.Hover(Theme);
                }
                else if (isHovered && isPressed && Enabled)
                {
                    borderColor = PoisonPaint.BorderColor.CheckBox.Press(Theme);
                }
                else if (!Enabled)
                {
                    borderColor = PoisonPaint.BorderColor.CheckBox.Disabled(Theme);
                }
                else
                {
                    borderColor = PoisonPaint.BorderColor.CheckBox.Normal(Theme);
                }
            }
            else
            {
                if (isHovered && !isPressed && Enabled)
                {
                    foreColor = PoisonPaint.ForeColor.CheckBox.Hover(Theme);
                    borderColor = PoisonPaint.BorderColor.CheckBox.Hover(Theme);
                }
                else if (isHovered && isPressed && Enabled)
                {
                    foreColor = PoisonPaint.ForeColor.CheckBox.Press(Theme);
                    borderColor = PoisonPaint.BorderColor.CheckBox.Press(Theme);
                }
                else if (!Enabled)
                {
                    foreColor = PoisonPaint.ForeColor.CheckBox.Disabled(Theme);
                    borderColor = PoisonPaint.BorderColor.CheckBox.Disabled(Theme);
                }
                else
                {
                    foreColor = !UseStyleColors ? PoisonPaint.ForeColor.CheckBox.Normal(Theme) : PoisonPaint.GetStyleColor(Style);
                    borderColor = PoisonPaint.BorderColor.CheckBox.Normal(Theme);
                }
            }

            Rectangle textRect = new(16, 0, Width - 16, Height);
            Rectangle boxRect = new(0, (Height / 2) - 6, 12, 12);
            using (Pen p = new(borderColor))
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                        boxRect = new(0, 0, 12, 12);
                        break;
                    case ContentAlignment.MiddleLeft:
                        boxRect = new(0, (Height / 2) - 6, 12, 12);
                        break;
                    case ContentAlignment.BottomLeft:
                        boxRect = new(0, Height - 13, 12, 12);
                        break;
                    case ContentAlignment.TopCenter:
                        boxRect = new((Width / 2) - 6, 0, 12, 12);
                        textRect = new(16, boxRect.Top + boxRect.Height - 5, Width - (16 / 2), Height);
                        break;
                    case ContentAlignment.BottomCenter:
                        boxRect = new((Width / 2) - 6, Height - 13, 12, 12);
                        textRect = new(16, -10, Width - (16 / 2), Height);
                        break;
                    case ContentAlignment.MiddleCenter:
                        boxRect = new((Width / 2) - 6, (Height / 2) - 6, 12, 12);
                        break;
                    case ContentAlignment.TopRight:
                        boxRect = new(Width - 13, 0, 12, 12);
                        textRect = new(0, 0, Width - 16, Height);
                        break;
                    case ContentAlignment.MiddleRight:
                        boxRect = new(Width - 13, (Height / 2) - 6, 12, 12);
                        textRect = new(0, 0, Width - 16, Height);
                        break;
                    case ContentAlignment.BottomRight:
                        boxRect = new(Width - 13, Height - 13, 12, 12);
                        textRect = new(0, 0, Width - 16, Height);
                        break;
                }

                e.Graphics.DrawRectangle(p, boxRect);
            }

            if (Checked)
            {
                Color fillColor = CheckState == CheckState.Indeterminate ? borderColor : PoisonPaint.GetStyleColor(Style);

                using SolidBrush b = new(fillColor);
                Rectangle boxCheck = new(boxRect.Left + 2, boxRect.Top + 2, 9, 9);
                e.Graphics.FillRectangle(b, boxCheck);
            }


            TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.CheckBox(FontSize, FontWeight), textRect, foreColor, PoisonPaint.GetTextFormatFlags(TextAlign, !AutoSize));

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));

            if (DisplayFocus && isFocused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
            }
        }

        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //Remove this code cause this prevents the focus color
            //isHovered = false;
            //isPressed = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!isFocused)
            {
                isHovered = false;
            }

            Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            Invalidate();
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (Graphics g = CreateGraphics())
            {
                proposedSize = new(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, Text, PoisonFonts.CheckBox(FontSize, FontWeight), proposedSize, PoisonPaint.GetTextFormatFlags(TextAlign));
                preferredSize.Width += 16;

                if (CheckAlign is ContentAlignment.TopCenter or ContentAlignment.BottomCenter)
                {
                    preferredSize.Height += 16;
                }
            }

            return preferredSize;
        }

        #endregion
    }

    #endregion
}