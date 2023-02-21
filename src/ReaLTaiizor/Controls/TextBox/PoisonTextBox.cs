#region Imports

using ReaLTaiizor.Colors;
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
    #region PoisonTextBox

    [Designer(typeof(PoisonTextBoxDesigner))]
    public class PoisonTextBox : Control, IPoisonControl
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

        private PromptedTextBox baseTextBox;

        private PoisonTextBoxSize poisonTextBoxSize = PoisonTextBoxSize.Small;
        [DefaultValue(PoisonTextBoxSize.Small)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonTextBoxSize FontSize
        {
            get => poisonTextBoxSize;
            set { poisonTextBoxSize = value; UpdateBaseTextBox(); }
        }

        private PoisonTextBoxWeight poisonTextBoxWeight = PoisonTextBoxWeight.Regular;
        [DefaultValue(PoisonTextBoxWeight.Regular)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonTextBoxWeight FontWeight
        {
            get => poisonTextBoxWeight;
            set { poisonTextBoxWeight = value; UpdateBaseTextBox(); }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [Obsolete("Use watermark")]
        public string PromptText
        {
            get => baseTextBox.WaterMark;
            set => baseTextBox.WaterMark = value;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public string WaterMark
        {
            get => baseTextBox.WaterMark;
            set => baseTextBox.WaterMark = value;
        }

        private Image textBoxIcon = null;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(null)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public Image Icon
        {
            get => textBoxIcon;
            set
            {
                textBoxIcon = value;
                Refresh();
            }
        }

        private bool textBoxIconRight = false;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool IconRight
        {
            get => textBoxIconRight;
            set
            {
                textBoxIconRight = value;
                Refresh();
            }
        }

        private bool displayIcon = false;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool DisplayIcon
        {
            get => displayIcon;
            set
            {
                displayIcon = value;
                Refresh();
            }
        }

        protected Size iconSize
        {
            get
            {
                if (displayIcon && textBoxIcon != null)
                {
                    int _height = textBoxIcon.Height > ClientRectangle.Height ? ClientRectangle.Height : textBoxIcon.Height;

                    Size originalSize = textBoxIcon.Size;
                    double resizeFactor = _height / (double)originalSize.Height;

                    //Point iconLocation = new(1, 1);
                    return new Size((int)(originalSize.Width * resizeFactor), (int)(originalSize.Height * resizeFactor));
                }

                return new Size(-1, -1);
            }
        }

        private PoisonTextButton _button;
        private bool _showbutton = false;

        protected int ButtonWidth
        {
            get
            {
                int _butwidth = 0;
                if (_button != null)
                {
                    _butwidth = _showbutton ? _button.Width : 0;
                }

                return _butwidth;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool ShowButton
        {
            get => _showbutton;
            set
            {
                _showbutton = value;
                Refresh();
            }
        }

        private PoisonLinkLabel lnkClear;
        private bool _showclear = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool ShowClearButton
        {
            get => _showclear;
            set
            {
                _showclear = value;
                Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonTextButton CustomButton
        {
            get => _button;
            set
            {
                _button = value;
                Refresh();
            }
        }

        private bool _witherror = false;

        [DefaultValue(false)]
        [Browsable(false)]
        public bool WithError
        {
            get => _witherror;
            set
            {
                _witherror = value;
                Invalidate();
            }
        }
        #endregion

        #region Routing Fields

#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
        public override ContextMenu ContextMenu
        {
            get => baseTextBox.ContextMenu;
            set
            {
                ContextMenu = value;
                baseTextBox.ContextMenu = value;
            }
        }
#endif

        public override ContextMenuStrip ContextMenuStrip
        {
            get => baseTextBox.ContextMenuStrip;
            set
            {
                ContextMenuStrip = value;
                baseTextBox.ContextMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get => baseTextBox.Multiline;
            set => baseTextBox.Multiline = value;
        }

        public override string Text
        {
            get => baseTextBox.Text;
            set => baseTextBox.Text = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public Color WaterMarkColor
        {
            get => baseTextBox.WaterMarkColor;
            set => baseTextBox.WaterMarkColor = value;
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public Font WaterMarkFont
        {
            get => baseTextBox.WaterMarkFont;
            set => baseTextBox.WaterMarkFont = value;
        }

        public string[] Lines
        {
            get => baseTextBox.Lines;
            set => baseTextBox.Lines = value;
        }

        [Browsable(false)]
        public string SelectedText
        {
            get => baseTextBox.SelectedText;
            set => baseTextBox.Text = value;
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => baseTextBox.ReadOnly;
            set => baseTextBox.ReadOnly = value;
        }

        public char PasswordChar
        {
            get => baseTextBox.PasswordChar;
            set => baseTextBox.PasswordChar = value;
        }

        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get => baseTextBox.UseSystemPasswordChar;
            set => baseTextBox.UseSystemPasswordChar = value;
        }

        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get => baseTextBox.TextAlign;
            set => baseTextBox.TextAlign = value;
        }

        public int SelectionStart
        {
            get => baseTextBox.SelectionStart;
            set => baseTextBox.SelectionStart = value;
        }

        public int SelectionLength
        {
            get => baseTextBox.SelectionLength;
            set => baseTextBox.SelectionLength = value;
        }

        [DefaultValue(true)]
        public new bool TabStop
        {
            get => baseTextBox.TabStop;
            set => baseTextBox.TabStop = value;
        }

        public int MaxLength
        {
            get => baseTextBox.MaxLength;
            set => baseTextBox.MaxLength = value;
        }

        public ScrollBars ScrollBars
        {
            get => baseTextBox.ScrollBars;
            set => baseTextBox.ScrollBars = value;
        }

        [DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AutoCompleteMode
        {
            get => baseTextBox.AutoCompleteMode;
            set => baseTextBox.AutoCompleteMode = value;
        }

        [DefaultValue(AutoCompleteSource.None)]
        public AutoCompleteSource AutoCompleteSource
        {
            get => baseTextBox.AutoCompleteSource;
            set => baseTextBox.AutoCompleteSource = value;
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => baseTextBox.AutoCompleteCustomSource;
            set => baseTextBox.AutoCompleteCustomSource = value;
        }

        public bool ShortcutsEnabled
        {
            get => baseTextBox.ShortcutsEnabled;
            set => baseTextBox.ShortcutsEnabled = value;
        }

        #endregion

        #region Constructor

        public PoisonTextBox()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            GotFocus += PoisonTextBox_GotFocus;
            base.TabStop = false;

            CreateBaseTextBox();
            UpdateBaseTextBox();
            AddEventHandler();
        }

        #endregion

        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void BaseTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            AcceptsTabChanged?.Invoke(this, e);
        }

        private void BaseTextBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseTextBoxCursorChanged(object sender, EventArgs e)
        {
            base.OnCursorChanged(e);
        }

        private void BaseTextBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseTextBoxContextMenuChanged(object sender, EventArgs e)
        {
#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
            base.OnContextMenuChanged(e);
#endif
        }

        private void BaseTextBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseTextBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseTextBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseTextBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void BaseTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void BaseTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        private bool _cleared = false;
        private bool _withtext = false;

        private void BaseTextBoxTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);

            if (baseTextBox.Text != "" && !_withtext)
            {
                _withtext = true;
                _cleared = false;
                Invalidate();
            }

            if (baseTextBox.Text == "" && !_cleared)
            {
                _withtext = false;
                _cleared = true;
                Invalidate();
            }
        }

        public void Select(int start, int length)
        {
            baseTextBox.Select(start, length);
        }

        public void SelectAll()
        {
            baseTextBox.SelectAll();
        }

        public void Clear()
        {
            baseTextBox.Clear();
        }

        private void PoisonTextBox_GotFocus(object sender, EventArgs e)
        {
            baseTextBox.Focus();
        }

        public void AppendText(string text)
        {
            baseTextBox.AppendText(text);
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                baseTextBox.BackColor = backColor;

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                    baseTextBox.BackColor = backColor;
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
            if (UseCustomForeColor)
            {
                baseTextBox.ForeColor = ForeColor;
            }
            else
            {
                baseTextBox.ForeColor = PoisonPaint.ForeColor.Button.Normal(Theme);
            }

            Color borderColor = PoisonPaint.BorderColor.ComboBox.Normal(Theme);

            if (UseStyleColors)
            {
                borderColor = PoisonPaint.GetStyleColor(Style);
            }

            if (_witherror)
            {
                borderColor = PoisonColors.Red;
                if (Style == ColorStyle.Red)
                {
                    borderColor = PoisonColors.Orange;
                }
            }

            using (Pen p = new(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 2, Height - 1));
            }

            DrawIcon(e.Graphics);
        }

        private void DrawIcon(Graphics g)
        {
            if (displayIcon && textBoxIcon != null)
            {
                Point iconLocation = new(5, 5);
                if (textBoxIconRight)
                {
                    iconLocation = new(ClientRectangle.Width - iconSize.Width - 1, 1);
                }

                g.DrawImage(textBoxIcon, new Rectangle(iconLocation, iconSize));

                UpdateBaseTextBox();
            }
            else
            {
                _button.Visible = _showbutton;
                if (_showbutton && _button != null)
                {
                    UpdateBaseTextBox();
                }
            }

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, baseTextBox.ForeColor, g));
        }

        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            base.Refresh();
            UpdateBaseTextBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateBaseTextBox();
        }

        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get => baseTextBox.CharacterCasing;
            set => baseTextBox.CharacterCasing = value;
        }
        #endregion

        #region Private Methods

        private void CreateBaseTextBox()
        {
            if (baseTextBox != null)
            {
                return;
            }

            baseTextBox = new PromptedTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = PoisonFonts.TextBox(poisonTextBoxSize, poisonTextBoxWeight),
                Location = new(3, 3),
                Size = new(Width - 6, Height - 6)
            };

            Size = new(baseTextBox.Width + 6, baseTextBox.Height + 6);

            baseTextBox.TabStop = true;
            //baseTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            Controls.Add(baseTextBox);

            if (_button != null)
            {
                return;
            }

            _button = new PoisonTextButton
            {
                Theme = Theme,
                Style = Style,
                Location = new(3, 1),
                Size = new(Height - 4, Height - 4)
            };
            _button.TextChanged += _button_TextChanged;
            _button.MouseEnter += _button_MouseEnter;
            _button.MouseLeave += _button_MouseLeave;
            _button.Click += _button_Click;

            if (!Controls.Contains(_button))
            {
                Controls.Add(_button);
            }

            if (lnkClear != null)
            {
                return;
            }

            InitializeComponent();
        }

        public delegate void ButClick(object sender, EventArgs e);
        public event ButClick ButtonClick;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        private void _button_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }

        private void _button_MouseLeave(object sender, EventArgs e)
        {
            UseStyleColors = baseTextBox.Focused;
            Invalidate();
        }

        private void _button_MouseEnter(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
        }

        private void _button_TextChanged(object sender, EventArgs e)
        {
            _button.Invalidate();
        }

        private void AddEventHandler()
        {
            baseTextBox.AcceptsTabChanged += BaseTextBoxAcceptsTabChanged;

            baseTextBox.CausesValidationChanged += BaseTextBoxCausesValidationChanged;
            baseTextBox.ChangeUICues += BaseTextBoxChangeUiCues;
            baseTextBox.Click += BaseTextBoxClick;
            baseTextBox.ClientSizeChanged += BaseTextBoxClientSizeChanged;
#if !NETCOREAPP3_1 && !NET6_0 && !NET7_0 && !NET8_0
            baseTextBox.ContextMenuChanged += BaseTextBoxContextMenuChanged;
#endif
            baseTextBox.ContextMenuStripChanged += BaseTextBoxContextMenuStripChanged;
            baseTextBox.CursorChanged += BaseTextBoxCursorChanged;

            baseTextBox.KeyDown += BaseTextBoxKeyDown;
            baseTextBox.KeyPress += BaseTextBoxKeyPress;
            baseTextBox.KeyUp += BaseTextBoxKeyUp;

            baseTextBox.SizeChanged += BaseTextBoxSizeChanged;

            baseTextBox.TextChanged += BaseTextBoxTextChanged;
            baseTextBox.GotFocus += baseTextBox_GotFocus;
            baseTextBox.LostFocus += baseTextBox_LostFocus;
        }

        private void baseTextBox_LostFocus(object sender, EventArgs e)
        {
            UseStyleColors = false;
            Invalidate();
            InvokeLostFocus(this, e);
        }

        private void baseTextBox_GotFocus(object sender, EventArgs e)
        {
            _witherror = false;
            UseStyleColors = true;
            Invalidate();
            InvokeGotFocus(this, e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            base.Cursor = Cursors.IBeam;
            UpdateTextBoxCursor();

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Cursor = Cursors.Default;
            UpdateTextBoxCursor();
        }

        private void UpdateTextBoxCursor()
        {
            baseTextBox.Cursor = base.Cursor;
        }

        private void UpdateBaseTextBox()
        {
            UpdateTextBoxCursor();

            if (_button != null)
            {
                if ((Height % 2) > 0)
                {
                    _button.Size = new(Height - 2, Height - 2);
                    _button.Location = new(Width - (_button.Width + 1), 1);
                }
                else
                {
                    _button.Size = new(Height - 5, Height - 5);
                    _button.Location = new(Width - _button.Width - 3, 2);
                }

                _button.Visible = _showbutton;
            }

            int _clearloc = 0;
            if (lnkClear != null)
            {
                lnkClear.Visible = false;
                if (_showclear && Text != "" && !ReadOnly && Enabled)
                {
                    _clearloc = 16;
                    lnkClear.Location = new(Width - (ButtonWidth + 17), (Height - 14) / 2);
                    lnkClear.Visible = true;
                }
            }


            if (baseTextBox == null)
            {
                return;
            }

            baseTextBox.Font = PoisonFonts.TextBox(poisonTextBoxSize, poisonTextBoxWeight);

            if (displayIcon)
            {
                Point textBoxLocation = new(iconSize.Width + 10, 5);
                if (textBoxIconRight)
                {
                    textBoxLocation = new(3, 3);
                }

                baseTextBox.Location = textBoxLocation;
                baseTextBox.Size = new(Width - (20 + ButtonWidth + _clearloc) - iconSize.Width, Height - 6);
            }
            else
            {
                baseTextBox.Location = new(3, 3);
                baseTextBox.Size = new(Width - (6 + ButtonWidth + _clearloc), Height - 6);
            }
        }

        #endregion

        #region PromptedTextBox

        private class PromptedTextBox : TextBox
        {
            private const int OCM_COMMAND = 0x2111;
            private const int WM_PAINT = 15;

            private bool drawPrompt;

            private string promptText = "";
            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DefaultValue("")]
            public string WaterMark
            {
                get => promptText;
                set
                {
                    promptText = value.Trim();
                    Invalidate();
                }
            }

            private Color _waterMarkColor = PoisonPaint.ForeColor.Button.Disabled(ThemeStyle.Dark);
            public Color WaterMarkColor
            {
                get => _waterMarkColor;
                set
                {
                    _waterMarkColor = value; Invalidate();
                }
            }

            public Font WaterMarkFont { get; set; } = PoisonFonts.WaterMark(PoisonLabelSize.Small, PoisonWaterMarkWeight.Italic);

            public PromptedTextBox()
            {
                SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
                drawPrompt = Text.Trim().Length == 0;
            }

            private void DrawTextPrompt()
            {
                using Graphics graphics = CreateGraphics();
                DrawTextPrompt(graphics);
            }

            private void DrawTextPrompt(Graphics g)
            {
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis;
                Rectangle clientRectangle = ClientRectangle;

                switch (TextAlign)
                {
                    case HorizontalAlignment.Left:
                        clientRectangle.Offset(1, 0);
                        break;
                    case HorizontalAlignment.Right:
                        flags |= TextFormatFlags.Right;
                        clientRectangle.Offset(-2, 0);
                        break;
                    case HorizontalAlignment.Center:
                        flags |= TextFormatFlags.HorizontalCenter;
                        clientRectangle.Offset(1, 0);
                        break;
                }

                //SolidBrush drawBrush = new(WaterMarkColor);

                TextRenderer.DrawText(g, promptText, WaterMarkFont, clientRectangle, _waterMarkColor, BackColor, flags);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (drawPrompt)
                {
                    DrawTextPrompt(e.Graphics);
                }
            }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
            }

            protected override void OnTextAlignChanged(EventArgs e)
            {
                base.OnTextAlignChanged(e);
                Invalidate();
            }

            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);
                drawPrompt = Text.Trim().Length == 0;
                Invalidate();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (((m.Msg == WM_PAINT) || (m.Msg == OCM_COMMAND)) && drawPrompt && !GetStyle(ControlStyles.UserPaint))
                {
                    DrawTextPrompt();
                }
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
                base.Cursor = Cursors.IBeam;

            }

            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
                base.Cursor = Cursors.Default;
            }

            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
            }

        }

        #endregion

        private void InitializeComponent()
        {
            lnkClear = new PoisonLinkLabel();
            SuspendLayout();
            // 
            // lnkClear
            // 
            lnkClear.FontSize = PoisonLinkLabelSize.Medium;
            lnkClear.FontWeight = PoisonLinkLabelWeight.Regular;
            lnkClear.Image = Properties.Resources.lnkClearImage;
            lnkClear.ImageSize = 10;
            lnkClear.Location = new(654, 96);
            lnkClear.Name = "lnkClear";
            lnkClear.Image = Properties.Resources.lnkClearNoFocusImage;
            lnkClear.Size = new(12, 12);
            lnkClear.TabIndex = 2;
            lnkClear.UseSelectable = true;
            lnkClear.Click += new EventHandler(lnkClear_Click);
            ResumeLayout(false);
            Controls.Add(lnkClear);
        }

        public delegate void LUClear();
        public event LUClear ClearClicked;

        private void lnkClear_Click(object sender, EventArgs e)
        {
            Focus();
            Clear();
            baseTextBox.Focus();

            ClearClicked?.Invoke();
        }

        #region PoisonTextButton
        [ToolboxItem(false)]
        public class PoisonTextButton : System.Windows.Forms.Button, IPoisonControl
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

            private bool isHovered = false;
            private bool isPressed = false;

            #endregion

            #region Constructor

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                SetStyle
                (
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.UserPaint,
                         true
                );
            }

            #endregion

            #region Paint Methods

            protected override void OnPaint(PaintEventArgs e)
            {
                Color backColor, foreColor;

                ThemeStyle _Theme = Theme;
                ColorStyle _Style = Style;

                if (Parent != null)
                {
                    if (Parent is IPoisonForm form)
                    {
                        _Theme = form.Theme;
                        _Style = form.Style;
                        foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                        backColor = PoisonPaint.GetStyleColor(_Style);
                    }
                    else if (Parent is IPoisonControl control)
                    {
                        _Theme = control.Theme;
                        _Style = control.Style;
                        foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                        backColor = PoisonPaint.GetStyleColor(_Style);
                    }
                    else
                    {
                        foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                        backColor = PoisonPaint.GetStyleColor(_Style);
                    }
                }
                else
                {
                    foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                    backColor = PoisonPaint.BackColor.Form(_Theme);
                }

                if (isHovered && !isPressed && Enabled)
                {
                    int _r = backColor.R;
                    int _g = backColor.G;
                    int _b = backColor.B;

                    backColor = ControlPaint.Light(backColor, 0.25f);
                }
                else if (isHovered && isPressed && Enabled)
                {
                    foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                    backColor = PoisonPaint.GetStyleColor(_Style);
                }
                else if (!Enabled)
                {
                    foreColor = PoisonPaint.ForeColor.Button.Disabled(_Theme);
                    backColor = PoisonPaint.BackColor.Button.Disabled(_Theme);
                }
                else
                {
                    foreColor = PoisonPaint.ForeColor.Button.Press(_Theme);
                }

                e.Graphics.Clear(backColor);
                Font buttonFont = PoisonFonts.Button(PoisonButtonSize.Small, PoisonButtonWeight.Bold);
                TextRenderer.DrawText(e.Graphics, Text, buttonFont, ClientRectangle, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                DrawIcon(e.Graphics);
            }

            private Bitmap _image = null;

            public new Image Image
            {
                get => base.Image;
                set
                {
                    base.Image = value;
                    if (value == null)
                    {
                        return;
                    }

                    _image = ApplyInvert(new Bitmap(value));
                }
            }

            public static Bitmap ApplyInvert(Bitmap bitmapImage)
            {
                byte A, R, G, B;
                Color pixelColor;

                for (int y = 0; y < bitmapImage.Height; y++)
                {
                    for (int x = 0; x < bitmapImage.Width; x++)
                    {
                        pixelColor = bitmapImage.GetPixel(x, y);
                        A = pixelColor.A;
                        R = (byte)(255 - pixelColor.R);
                        G = (byte)(255 - pixelColor.G);
                        B = (byte)(255 - pixelColor.B);
                        bitmapImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                    }
                }

                return bitmapImage;
            }

            protected Size iconSize
            {
                get
                {
                    if (Image != null)
                    {
                        Size originalSize = Image.Size;
                        double resizeFactor = 14 / (double)originalSize.Height;

                        //Point iconLocation = new(1, 1);
                        return new Size((int)(originalSize.Width * resizeFactor), (int)(originalSize.Height * resizeFactor));
                    }

                    return new Size(-1, -1);
                }
            }

            private void DrawIcon(Graphics g)
            {
                if (Image != null)
                {
                    Point iconLocation = new(2, (ClientRectangle.Height - iconSize.Height) / 2);
                    int _filler = 5;

                    switch (ImageAlign)
                    {
                        case ContentAlignment.BottomCenter:
                            iconLocation = new((ClientRectangle.Width - iconSize.Width) / 2, ClientRectangle.Height - iconSize.Height - _filler);
                            break;
                        case ContentAlignment.BottomLeft:
                            iconLocation = new(_filler, ClientRectangle.Height - iconSize.Height - _filler);
                            break;
                        case ContentAlignment.BottomRight:
                            iconLocation = new(ClientRectangle.Width - iconSize.Width - _filler, ClientRectangle.Height - iconSize.Height - _filler);
                            break;
                        case ContentAlignment.MiddleCenter:
                            iconLocation = new((ClientRectangle.Width - iconSize.Width) / 2, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.MiddleLeft:
                            iconLocation = new(_filler, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.MiddleRight:
                            iconLocation = new(ClientRectangle.Width - iconSize.Width - _filler, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.TopCenter:
                            iconLocation = new((ClientRectangle.Width - iconSize.Width) / 2, _filler);
                            break;
                        case ContentAlignment.TopLeft:
                            iconLocation = new(_filler, _filler);
                            break;
                        case ContentAlignment.TopRight:
                            iconLocation = new(ClientRectangle.Width - iconSize.Width - _filler, _filler);
                            break;
                    }

                    g.DrawImage((Theme == ThemeStyle.Dark) ? (isPressed ? _image : Image) : isPressed ? Image : _image, new Rectangle(iconLocation, iconSize));
                }
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
                isHovered = false;
                Invalidate();

                base.OnMouseLeave(e);
            }

            #endregion
        }
        #endregion
    }

    #endregion
}