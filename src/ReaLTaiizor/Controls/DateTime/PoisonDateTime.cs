#region Imports

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
    #region PoisonDateTime

    [ToolboxBitmap(typeof(DateTimePicker))]
    public class PoisonDateTime : DateTimePicker, IPoisonControl
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
        [DefaultValue(true)]
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
        [DefaultValue(PoisonDateTimeSize.Medium)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonDateTimeSize FontSize { get; set; } = PoisonDateTimeSize.Medium;
        [DefaultValue(PoisonDateTimeWeight.Regular)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonDateTimeWeight FontWeight { get; set; } = PoisonDateTimeWeight.Regular;

        [DefaultValue(false)]
        [Browsable(false)]
        public new bool ShowUpDown
        {
            get => base.ShowUpDown;
            set => base.ShowUpDown = false;
        }

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
        public PoisonDateTime()
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
            MinimumSize = new(0, GetPreferredSize(Size.Empty).Height);

            Color borderColor, foreColor;

            if (isHovered && !isPressed && Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Hover(Theme);
                borderColor = PoisonPaint.GetStyleColor(Style);
            }
            else if (isHovered && isPressed && Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Press(Theme);
                borderColor = PoisonPaint.GetStyleColor(Style);
            }
            else if (!Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Disabled(Theme);
                borderColor = PoisonPaint.BorderColor.ComboBox.Disabled(Theme);
            }
            else
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Normal(Theme);
                borderColor = PoisonPaint.BorderColor.ComboBox.Normal(Theme);
            }

            using (Pen p = new(borderColor))
            {
                Rectangle boxRect = new(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(p, boxRect);
            }

            using (SolidBrush b = new(foreColor))
            {
                e.Graphics.FillPolygon(b, new Point[] { new Point(Width - 20, (Height / 2) - 2), new Point(Width - 9, (Height / 2) - 2), new Point(Width - 15, (Height / 2) + 4) });
                //e.Graphics.FillPolygon(b, new Point[] { new Point(Width - 15, (Height / 2) - 5), new Point(Width - 21, (Height / 2) + 2), new Point(Width - 9, (Height / 2) + 2) });
            }

            int _check = 0;

            if (ShowCheckBox)
            {
                _check = 15;
                using (Pen p = new(borderColor))
                {
                    Rectangle boxRect = new(3, (Height / 2) - 6, 12, 12);
                    e.Graphics.DrawRectangle(p, boxRect);
                }

                if (Checked)
                {

                    Color fillColor = PoisonPaint.GetStyleColor(Style);

                    using SolidBrush b = new(fillColor);
                    Rectangle boxRect = new(5, (Height / 2) - 4, 9, 9);
                    e.Graphics.FillRectangle(b, boxRect);
                }
                else
                {
                    foreColor = PoisonPaint.ForeColor.ComboBox.Disabled(Theme);
                }
            }

            Rectangle textRect = new(2 + _check, 2, Width - 20, Height - 4);

            TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.DateTime(FontSize, FontWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));

            if (DisplayFocus && isFocused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            base.OnValueChanged(eventargs);
            Invalidate();
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

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (Graphics g = CreateGraphics())
            {
                string measureText = Text.Length > 0 ? Text : "MeasureText";
                proposedSize = new(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, measureText, PoisonFonts.DateTime(FontSize, FontWeight), proposedSize, TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter);
                preferredSize.Height += 10;
            }

            return preferredSize;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        #endregion
    }

    #endregion
}