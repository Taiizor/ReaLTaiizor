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
    #region PoisonComboBox

    [ToolboxBitmap(typeof(ComboBox))]
    public class PoisonComboBox : ComboBox, IPoisonControl
    {
        #region Edit Control
        private readonly PoisonTextBox textBox = new();
        #endregion

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

        [Browsable(true)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue("")]
        [Description("Gets or sets the text associated with this control.")]
        public override string Text
        {
            get => textBox.Text;
            set { textBox.Text = value; base.Text = textBox.Text; OnTextChanged(EventArgs.Empty); }
        }

        #endregion

        #region Fields

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool DisplayFocus { get; set; } = false;

        [DefaultValue(DrawMode.OwnerDrawFixed)]
        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get => DrawMode.OwnerDrawFixed;
            set => base.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private ComboBoxStyle dropDownStyle = ComboBoxStyle.DropDownList;
        [DefaultValue(ComboBoxStyle.DropDownList)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [Browsable(true)]
        public new ComboBoxStyle DropDownStyle
        {
            get => dropDownStyle;
            set
            {
                // we don't support the Simple style
                if (value == ComboBoxStyle.Simple)
                {
                    value = ComboBoxStyle.DropDownList;
                }

                dropDownStyle = value;
                // fake out the base
                base.DropDownStyle = ComboBoxStyle.DropDownList;
                // if we are a dropdown and have focus, then show the edit box
                if ((value == ComboBoxStyle.DropDown) && Focused)
                {
                    textBox.Visible = true;
                }
                else
                {
                    textBox.Visible = false;
                }

                Invalidate();
            }
        }

        [DefaultValue(PoisonComboBoxSize.Medium)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonComboBoxSize FontSize { get; set; } = PoisonComboBoxSize.Medium;
        [DefaultValue(PoisonComboBoxWeight.Regular)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonComboBoxWeight FontWeight { get; set; } = PoisonComboBoxWeight.Regular;

        private string promptText = "";
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public string PromptText
        {
            get => promptText;
            set
            {
                promptText = value.Trim();
                // when we are drop down, use the watermark property of the textbox to show the prompt text
                if (DropDownStyle == ComboBoxStyle.DropDown)
                {
                    textBox.WaterMark = promptText;
                }

                Invalidate();
            }
        }

        private bool drawPrompt = false;

        [Browsable(false)]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        private AutoCompleteMode autoCompleteMode = AutoCompleteMode.None;
        public new AutoCompleteMode AutoCompleteMode
        {
            get => autoCompleteMode;
            set
            {
                autoCompleteMode = value;
                textBox.AutoCompleteMode = value;
                if (value != AutoCompleteMode.None)
                {
                    // if using autocomplete, then the source will be the item list.
                    textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                    foreach (object item in Items)
                    {
                        textBox.AutoCompleteCustomSource.Add(item.ToString());
                    }
                }
                else
                {
                    textBox.AutoCompleteSource = AutoCompleteSource.None;
                    textBox.AutoCompleteCustomSource = null;
                }
            }
        }

        [Browsable(false)]
        public new AutoCompleteSource AutoCompleteSource
        {
            get => base.AutoCompleteSource;
            set => base.AutoCompleteSource = value;
        }

        [Browsable(false)]
        public new AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => base.AutoCompleteCustomSource;
            set => base.AutoCompleteCustomSource = value;
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public PoisonComboBox()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );
            DropDownStyle = ComboBoxStyle.DropDownList;

            base.DrawMode = DrawMode.OwnerDrawFixed;

            drawPrompt = SelectedIndex == -1;

            // setup the textbox
            SuspendLayout();
            textBox.Location = new System.Drawing.Point(0, 0);
            textBox.FontSize = (PoisonTextBoxSize)FontSize;
            textBox.FontWeight = (PoisonTextBoxWeight)FontWeight;
            textBox.WaterMarkFont = PoisonFonts.ComboBox(FontSize, FontWeight);
            textBox.Size = Size;
            textBox.TabIndex = 0;
            textBox.Margin = new Padding(0);
            textBox.Padding = new Padding(0);
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.Resize += TextBox_Resize;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.UseSelectable = true;
            textBox.Enter += TextBox_Enter;
            textBox.Visible = false;

            Controls.Add(textBox);
            ResumeLayout(true);
            AdjustControls();
        }

        #endregion

        #region TextBox Methods

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (autoCompleteMode != AutoCompleteMode.None)
            {
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();

                for (int i = 0; i < Items.Count; i++)
                {
                    textBox.AutoCompleteCustomSource.Add(GetItemText(Items[i]));
                }
            }
        }

        private void TextBox_Resize(object sender, EventArgs e)
        {
            AdjustControls();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            promptText = textBox.Text;
            OnTextChanged(e);
        }
        #endregion

        #region Paint Methods

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustControls();
            base.OnSizeChanged(e);
        }

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
            base.ItemHeight = GetPreferredSize(Size.Empty).Height;

            Color borderColor, foreColor;

            if (isHovered && !isPressed && Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Hover(Theme);
                borderColor = PoisonPaint.BorderColor.ComboBox.Hover(Theme);
            }
            else if (isHovered && isPressed && Enabled)
            {
                foreColor = PoisonPaint.ForeColor.ComboBox.Press(Theme);
                borderColor = PoisonPaint.BorderColor.ComboBox.Press(Theme);
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


            if (DropDownStyle != ComboBoxStyle.Simple)
            {
                using (SolidBrush b = new(foreColor))
                {
                    e.Graphics.FillPolygon(b, new Point[] { new Point(Width - 20, (Height / 2) - 2), new Point(Width - 9, (Height / 2) - 2), new Point(Width - 15, (Height / 2) + 4) });
                }

                Rectangle textRect = new(2, 2, Width - 40, Height - 4);

                if (Enabled)
                {
                    TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.ComboBox(FontSize, FontWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    ControlPaint.DrawStringDisabled(e.Graphics, Text, PoisonFonts.ComboBox(FontSize, FontWeight), PoisonPaint.ForeColor.ComboBox.Disabled(Theme), textRect, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }

                OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));
                if (DisplayFocus && isFocused)
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
                }

                if (drawPrompt)
                {
                    DrawTextPrompt(e.Graphics);
                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color foreColor = PoisonPaint.ForeColor.Link.Normal(Theme);
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    using (SolidBrush b = new(PoisonPaint.GetStyleColor(Style)))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }

                    foreColor = PoisonPaint.ForeColor.Tile.Normal(Theme);
                }
                else
                {
                    using SolidBrush b = new(backColor);
                    e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                }

                if (DropDownStyle != ComboBoxStyle.DropDown)
                {
                    Rectangle textRect = new(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), PoisonFonts.ComboBox(FontSize, FontWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    Rectangle textRect = new(0, e.Bounds.Top, textBox.Width, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), PoisonFonts.ComboBox(FontSize, FontWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
            }
            else
            {
                base.OnDrawItem(e);
            }
        }

        private void DrawTextPrompt()
        {

            using Graphics graphics = CreateGraphics();
            DrawTextPrompt(graphics);
        }

        private void DrawTextPrompt(Graphics g)
        {
            Color backColor = BackColor;

            if (!UseCustomBackColor)
            {
                backColor = PoisonPaint.BackColor.Form(Theme);
            }

            Rectangle textRect = new(2, 2, Width - 20, Height - 4);
            TextRenderer.DrawText(g, promptText, PoisonFonts.ComboBox(FontSize, FontWeight), textRect, SystemColors.GrayText, backColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
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
            if (DropDownStyle == ComboBoxStyle.DropDown)
            {
                textBox.Visible = false;
            }

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
                if (DropDownStyle == ComboBoxStyle.DropDown)
                {
                    textBox.Visible = true;
                }

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
                preferredSize = TextRenderer.MeasureText(g, measureText, PoisonFonts.ComboBox(FontSize, FontWeight), proposedSize, TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter);
                preferredSize.Height += 4;
            }

            return preferredSize;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            drawPrompt = SelectedIndex == -1;
            Invalidate();
        }

        private const int OCM_COMMAND = 0x2111;
        private const int WM_PAINT = 15;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (((m.Msg == WM_PAINT) || (m.Msg == OCM_COMMAND)) && drawPrompt)
            {
                DrawTextPrompt();
            }
        }

        #endregion

        #region Private Methods
        private void AdjustControls()
        {
            if (DropDownStyle == ComboBoxStyle.DropDownList)
            {
                return;
            }

            SuspendLayout();
            textBox.Width = ClientRectangle.Width - 26;
            textBox.Height = ClientRectangle.Height;
            ResumeLayout();
            Invalidate(false);
        }


        #endregion
    }

    #endregion
}