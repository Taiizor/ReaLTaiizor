#region Imports

using ReaLTaiizor.Child.Material;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialMultiLineTextBoxEdit

    public class MaterialMultiLineTextBoxEdit : Control, MaterialControlI
    {
        MaterialContextMenuStrip cms = new MaterialBaseTextBoxContextMenuStrip();
        ContextMenuStrip _lastContextMenuStrip = new();

        //Properties for managing the material design properties

        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        public bool Focus()
        {
            return baseTextBox.Focus();
        }

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        //Unused properties

        [Browsable(false)]
        public override Image BackgroundImage { get; set; }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout { get; set; }

        [Browsable(false)]
        public string SelectedText { get => baseTextBox.SelectedText; set => baseTextBox.SelectedText = value; }

        [Browsable(false)]
        public int SelectionStart { get => baseTextBox.SelectionStart; set => baseTextBox.SelectionStart = value; }
        [Browsable(false)]
        public int SelectionLength { get => baseTextBox.SelectionLength; set => baseTextBox.SelectionLength = value; }
        [Browsable(false)]
        public int TextLength => baseTextBox.TextLength;

        [Browsable(false)]
        public override Color ForeColor { get; set; }

        //Material Skin properties


        [Category("Material"), DefaultValue(""), Localizable(true)]
        public string Hint
        {
            get => baseTextBox.Hint;
            set
            {
                baseTextBox.Hint = value;
                hasHint = !string.IsNullOrEmpty(baseTextBox.Hint);
                Invalidate();
            }
        }

        [Category("Material"), DefaultValue(true)]
        public bool UseAccent { get; set; }



        [Browsable(true)]
        [Category("Material"), DefaultValue(true), Description("Defines whether MaterialMultiLineTextBox allows scrolling of text. This property is independent of the ScrollBars property")]
        public bool AllowScroll { get; set; }



        //TextBox properties

        public override ContextMenuStrip ContextMenuStrip
        {
            get => baseTextBox.ContextMenuStrip;
            set
            {
                if (value != null)
                {
                    baseTextBox.ContextMenuStrip = value;
                    base.ContextMenuStrip = value;
                }
                else
                {
                    baseTextBox.ContextMenuStrip = cms;
                    base.ContextMenuStrip = cms;
                }
                _lastContextMenuStrip = base.ContextMenuStrip;
            }
        }

        [Browsable(false)]
        public override Color BackColor => Parent == null ? SkinManager.BackgroundColor : Parent.BackColor;

        public override string Text { get => baseTextBox.Text; set => baseTextBox.Text = value; }

        [Category("Appearance")]
        public HorizontalAlignment TextAlign { get => baseTextBox.TextAlign; set => baseTextBox.TextAlign = value; }

        [Category("Appearance")]
        public ScrollBars ScrollBars { get => baseTextBox.ScrollBars; set => baseTextBox.ScrollBars = value; }

        [Category("Behavior")]
        public CharacterCasing CharacterCasing { get => baseTextBox.CharacterCasing; set => baseTextBox.CharacterCasing = value; }

        [Category("Behavior")]
        public bool HideSelection { get => baseTextBox.HideSelection; set => baseTextBox.HideSelection = value; }

        [Category("Behavior")]
        public int MaxLength { get => baseTextBox.MaxLength; set => baseTextBox.MaxLength = value; }

        [Category("Behavior")]
        public char PasswordChar { get => baseTextBox.PasswordChar; set => baseTextBox.PasswordChar = value; }

        [Category("Behavior")]
        public bool ShortcutsEnabled
        {
            get => baseTextBox.ShortcutsEnabled;
            set
            {
                baseTextBox.ShortcutsEnabled = value;
                if (value == false)
                {
                    baseTextBox.ContextMenuStrip = null;
                    base.ContextMenuStrip = null;
                }
                else
                {
                    baseTextBox.ContextMenuStrip = _lastContextMenuStrip;
                    base.ContextMenuStrip = _lastContextMenuStrip;
                }
            }
        }

        [Category("Behavior")]
        public bool UseSystemPasswordChar { get => baseTextBox.UseSystemPasswordChar; set => baseTextBox.UseSystemPasswordChar = value; }

        public new object Tag { get => baseTextBox.Tag; set => baseTextBox.Tag = value; }

        private bool _readonly;
        [Category("Behavior")]
        public bool ReadOnly
        {
            get => _readonly;
            set
            {
                _readonly = value;
                if (Enabled == true)
                {
                    baseTextBox.ReadOnly = _readonly;
                }
                this.Invalidate();
            }
        }

        private bool _animateReadOnly;

        [Category("Material")]
        [Browsable(true)]
        public bool AnimateReadOnly
        {
            get => _animateReadOnly;
            set
            {
                _animateReadOnly = value;
                Invalidate();
            }
        }

        private bool _leaveOnEnterKey;

        [Category("Material"), DefaultValue(false), Description("Select next control which have TabStop property set to True when enter key is pressed. To add enter in text, the user must press CTRL+Enter")]
        public bool LeaveOnEnterKey
        {
            get => _leaveOnEnterKey;
            set
            {
                _leaveOnEnterKey = value;
                if (value)
                {
                    baseTextBox.KeyDown += new KeyEventHandler(LeaveOnEnterKey_KeyDown);
                }
                else
                {
                    baseTextBox.KeyDown -= LeaveOnEnterKey_KeyDown;
                }
                Invalidate();
            }
        }

        public void SelectAll() { baseTextBox.SelectAll(); }

        public void Clear() { baseTextBox.Clear(); }

        public void Copy() { baseTextBox.Copy(); }

        public void Cut() { baseTextBox.Cut(); }

        public void Undo() { baseTextBox.Undo(); }

        public void Paste() { baseTextBox.Paste(); }


        #region Forwarding events to baseTextBox

        public event EventHandler AcceptsTabChanged
        {
            add => baseTextBox.AcceptsTabChanged += value;
            remove => baseTextBox.AcceptsTabChanged -= value;
        }

        public new event EventHandler AutoSizeChanged
        {
            add => baseTextBox.AutoSizeChanged += value;
            remove => baseTextBox.AutoSizeChanged -= value;
        }

        public new event EventHandler BackgroundImageChanged
        {
            add => baseTextBox.BackgroundImageChanged += value;
            remove => baseTextBox.BackgroundImageChanged -= value;
        }

        public new event EventHandler BackgroundImageLayoutChanged
        {
            add => baseTextBox.BackgroundImageLayoutChanged += value;
            remove => baseTextBox.BackgroundImageLayoutChanged -= value;
        }

        public new event EventHandler BindingContextChanged
        {
            add => baseTextBox.BindingContextChanged += value;
            remove => baseTextBox.BindingContextChanged -= value;
        }

        public event EventHandler BorderStyleChanged
        {
            add => baseTextBox.BorderStyleChanged += value;
            remove => baseTextBox.BorderStyleChanged -= value;
        }

        public new event EventHandler CausesValidationChanged
        {
            add => baseTextBox.CausesValidationChanged += value;
            remove => baseTextBox.CausesValidationChanged -= value;
        }

        public new event UICuesEventHandler ChangeUICues
        {
            add => baseTextBox.ChangeUICues += value;
            remove => baseTextBox.ChangeUICues -= value;
        }

        public new event EventHandler Click
        {
            add => baseTextBox.Click += value;
            remove => baseTextBox.Click -= value;
        }

        public new event EventHandler ClientSizeChanged
        {
            add => baseTextBox.ClientSizeChanged += value;
            remove => baseTextBox.ClientSizeChanged -= value;
        }

#if NETFRAMEWORK
        public new event EventHandler ContextMenuChanged
        {
            add => baseTextBox.ContextMenuChanged += value;
            remove => baseTextBox.ContextMenuChanged -= value;
        }
#endif

        public new event EventHandler ContextMenuStripChanged
        {
            add => baseTextBox.ContextMenuStripChanged += value;
            remove => baseTextBox.ContextMenuStripChanged -= value;
        }

        public new event ControlEventHandler ControlAdded
        {
            add => baseTextBox.ControlAdded += value;
            remove => baseTextBox.ControlAdded -= value;
        }

        public new event ControlEventHandler ControlRemoved
        {
            add => baseTextBox.ControlRemoved += value;
            remove => baseTextBox.ControlRemoved -= value;
        }

        public new event EventHandler CursorChanged
        {
            add => baseTextBox.CursorChanged += value;
            remove => baseTextBox.CursorChanged -= value;
        }

        public new event EventHandler Disposed
        {
            add => baseTextBox.Disposed += value;
            remove => baseTextBox.Disposed -= value;
        }

        public new event EventHandler DockChanged
        {
            add => baseTextBox.DockChanged += value;
            remove => baseTextBox.DockChanged -= value;
        }

        public new event EventHandler DoubleClick
        {
            add => baseTextBox.DoubleClick += value;
            remove => baseTextBox.DoubleClick -= value;
        }

        public new event DragEventHandler DragDrop
        {
            add => baseTextBox.DragDrop += value;
            remove => baseTextBox.DragDrop -= value;
        }

        public new event DragEventHandler DragEnter
        {
            add => baseTextBox.DragEnter += value;
            remove => baseTextBox.DragEnter -= value;
        }

        public new event EventHandler DragLeave
        {
            add => baseTextBox.DragLeave += value;
            remove => baseTextBox.DragLeave -= value;
        }

        public new event DragEventHandler DragOver
        {
            add => baseTextBox.DragOver += value;
            remove => baseTextBox.DragOver -= value;
        }

        public new event EventHandler EnabledChanged
        {
            add => baseTextBox.EnabledChanged += value;
            remove => baseTextBox.EnabledChanged -= value;
        }

        public new event EventHandler Enter
        {
            add => baseTextBox.Enter += value;
            remove => baseTextBox.Enter -= value;
        }

        public new event EventHandler FontChanged
        {
            add => baseTextBox.FontChanged += value;
            remove => baseTextBox.FontChanged -= value;
        }

        public new event EventHandler ForeColorChanged
        {
            add => baseTextBox.ForeColorChanged += value;
            remove => baseTextBox.ForeColorChanged -= value;
        }

        public new event GiveFeedbackEventHandler GiveFeedback
        {
            add => baseTextBox.GiveFeedback += value;
            remove => baseTextBox.GiveFeedback -= value;
        }

        public new event EventHandler GotFocus
        {
            add => baseTextBox.GotFocus += value;
            remove => baseTextBox.GotFocus -= value;
        }

        public new event EventHandler HandleCreated
        {
            add => baseTextBox.HandleCreated += value;
            remove => baseTextBox.HandleCreated -= value;
        }

        public new event EventHandler HandleDestroyed
        {
            add => baseTextBox.HandleDestroyed += value;
            remove => baseTextBox.HandleDestroyed -= value;
        }

        public new event HelpEventHandler HelpRequested
        {
            add => baseTextBox.HelpRequested += value;
            remove => baseTextBox.HelpRequested -= value;
        }

        public event EventHandler HideSelectionChanged
        {
            add => baseTextBox.HideSelectionChanged += value;
            remove => baseTextBox.HideSelectionChanged -= value;
        }

        public new event EventHandler ImeModeChanged
        {
            add => baseTextBox.ImeModeChanged += value;
            remove => baseTextBox.ImeModeChanged -= value;
        }

        public new event InvalidateEventHandler Invalidated
        {
            add => baseTextBox.Invalidated += value;
            remove => baseTextBox.Invalidated -= value;
        }

        public new event KeyEventHandler KeyDown
        {
            add => baseTextBox.KeyDown += value;
            remove => baseTextBox.KeyDown -= value;
        }

        public new event KeyPressEventHandler KeyPress
        {
            add => baseTextBox.KeyPress += value;
            remove => baseTextBox.KeyPress -= value;
        }

        public new event KeyEventHandler KeyUp
        {
            add => baseTextBox.KeyUp += value;
            remove => baseTextBox.KeyUp -= value;
        }

        public new event LayoutEventHandler Layout
        {
            add => baseTextBox.Layout += value;
            remove => baseTextBox.Layout -= value;
        }

        public new event EventHandler Leave
        {
            add => baseTextBox.Leave += value;
            remove => baseTextBox.Leave -= value;
        }

        public new event EventHandler LocationChanged
        {
            add => baseTextBox.LocationChanged += value;
            remove => baseTextBox.LocationChanged -= value;
        }

        public new event EventHandler LostFocus
        {
            add => baseTextBox.LostFocus += value;
            remove => baseTextBox.LostFocus -= value;
        }

        public new event EventHandler MarginChanged
        {
            add => baseTextBox.MarginChanged += value;
            remove => baseTextBox.MarginChanged -= value;
        }

        public event EventHandler ModifiedChanged
        {
            add => baseTextBox.ModifiedChanged += value;
            remove => baseTextBox.ModifiedChanged -= value;
        }

        public new event EventHandler MouseCaptureChanged
        {
            add => baseTextBox.MouseCaptureChanged += value;
            remove => baseTextBox.MouseCaptureChanged -= value;
        }

        public new event MouseEventHandler MouseClick
        {
            add => baseTextBox.MouseClick += value;
            remove => baseTextBox.MouseClick -= value;
        }

        public new event MouseEventHandler MouseDoubleClick
        {
            add => baseTextBox.MouseDoubleClick += value;
            remove => baseTextBox.MouseDoubleClick -= value;
        }

        public new event MouseEventHandler MouseDown
        {
            add => baseTextBox.MouseDown += value;
            remove => baseTextBox.MouseDown -= value;
        }

        public new event EventHandler MouseEnter
        {
            add => baseTextBox.MouseEnter += value;
            remove => baseTextBox.MouseEnter -= value;
        }

        public new event EventHandler MouseHover
        {
            add => baseTextBox.MouseHover += value;
            remove => baseTextBox.MouseHover -= value;
        }

        public new event EventHandler MouseLeave
        {
            add => baseTextBox.MouseLeave += value;
            remove => baseTextBox.MouseLeave -= value;
        }

        public new event MouseEventHandler MouseMove
        {
            add => baseTextBox.MouseMove += value;
            remove => baseTextBox.MouseMove -= value;
        }

        public new event MouseEventHandler MouseUp
        {
            add => baseTextBox.MouseUp += value;
            remove => baseTextBox.MouseUp -= value;
        }

        public new event MouseEventHandler MouseWheel
        {
            add => baseTextBox.MouseWheel += value;
            remove => baseTextBox.MouseWheel -= value;
        }

        public new event EventHandler Move
        {
            add => baseTextBox.Move += value;
            remove => baseTextBox.Move -= value;
        }

        public event EventHandler MultilineChanged
        {
            add => baseTextBox.MultilineChanged += value;
            remove => baseTextBox.MultilineChanged -= value;
        }

        public new event EventHandler PaddingChanged
        {
            add => baseTextBox.PaddingChanged += value;
            remove => baseTextBox.PaddingChanged -= value;
        }

        public new event PaintEventHandler Paint
        {
            add => baseTextBox.Paint += value;
            remove => baseTextBox.Paint -= value;
        }

        public new event EventHandler ParentChanged
        {
            add => baseTextBox.ParentChanged += value;
            remove => baseTextBox.ParentChanged -= value;
        }

        public new event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add => baseTextBox.PreviewKeyDown += value;
            remove => baseTextBox.PreviewKeyDown -= value;
        }

        public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add => baseTextBox.QueryAccessibilityHelp += value;
            remove => baseTextBox.QueryAccessibilityHelp -= value;
        }

        public new event QueryContinueDragEventHandler QueryContinueDrag
        {
            add => baseTextBox.QueryContinueDrag += value;
            remove => baseTextBox.QueryContinueDrag -= value;
        }

        public event EventHandler ReadOnlyChanged
        {
            add => baseTextBox.ReadOnlyChanged += value;
            remove => baseTextBox.ReadOnlyChanged -= value;
        }

        public new event EventHandler RegionChanged
        {
            add => baseTextBox.RegionChanged += value;
            remove => baseTextBox.RegionChanged -= value;
        }

        public new event EventHandler Resize
        {
            add => baseTextBox.Resize += value;
            remove => baseTextBox.Resize -= value;
        }

        public new event EventHandler RightToLeftChanged
        {
            add => baseTextBox.RightToLeftChanged += value;
            remove => baseTextBox.RightToLeftChanged -= value;
        }

        public new event EventHandler SizeChanged
        {
            add => baseTextBox.SizeChanged += value;
            remove => baseTextBox.SizeChanged -= value;
        }

        public new event EventHandler StyleChanged
        {
            add => baseTextBox.StyleChanged += value;
            remove => baseTextBox.StyleChanged -= value;
        }

        public new event EventHandler SystemColorsChanged
        {
            add => baseTextBox.SystemColorsChanged += value;
            remove => baseTextBox.SystemColorsChanged -= value;
        }

        public new event EventHandler TabIndexChanged
        {
            add => baseTextBox.TabIndexChanged += value;
            remove => baseTextBox.TabIndexChanged -= value;
        }

        public new event EventHandler TabStopChanged
        {
            add => baseTextBox.TabStopChanged += value;
            remove => baseTextBox.TabStopChanged -= value;
        }

        public event EventHandler TextAlignChanged
        {
            add => baseTextBox.TextAlignChanged += value;
            remove => baseTextBox.TextAlignChanged -= value;
        }

        public new event EventHandler TextChanged
        {
            add => baseTextBox.TextChanged += value;
            remove => baseTextBox.TextChanged -= value;
        }

        public new event EventHandler Validated
        {
            add => baseTextBox.Validated += value;
            remove => baseTextBox.Validated -= value;
        }

        public new event CancelEventHandler Validating
        {
            add => baseTextBox.Validating += value;
            remove => baseTextBox.Validating -= value;
        }

        public new event EventHandler VisibleChanged
        {
            add => baseTextBox.VisibleChanged += value;
            remove => baseTextBox.VisibleChanged -= value;
        }
        # endregion

        //private readonly AnimationManager animationManager;
        private readonly AnimationManager _animationManager;

        public bool isFocused = false;
        private const int HINT_TEXT_SMALL_SIZE = 18;
        private const int HINT_TEXT_SMALL_Y = 4;
        private const int LINE_BOTTOM_PADDING = 3;
        private const int TOP_PADDING = 10;
        private const int BOTTOM_PADDING = 10;
        private const int LEFT_PADDING = 16;
        private const int RIGHT_PADDING = 12;
        private int LINE_Y;
        private bool hasHint;
        private readonly int SB_LINEUP = 0;
        private readonly int SB_LINEDOWN = 1;
        private readonly uint WM_VSCROLL = 277;
        private readonly IntPtr ptrLparam = new(0);

        protected readonly MaterialBaseTextBox baseTextBox;
        public MaterialMultiLineTextBoxEdit()
        {
            AllowScroll = true;
            // Material Properties
            UseAccent = true;
            MouseState = MaterialMouseState.OUT;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);

            // Animations
            _animationManager = new AnimationManager
            {
                Increment = 0.06,
                AnimationType = AnimationType.EaseInOut,
                InterruptAnimation = false
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            baseTextBox = new MaterialBaseTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = SkinManager.GetFontByType(MaterialSkinManager.FontType.Subtitle1),
                ForeColor = SkinManager.TextHighEmphasisColor,
                Multiline = true
            };

            Cursor = Cursors.IBeam;
            Enabled = true;
            ReadOnly = false;
            ScrollBars = ScrollBars.None;
            Size = new Size(250, 100);

            if (!Controls.Contains(baseTextBox) && !DesignMode)
            {
                Controls.Add(baseTextBox);
            }

            baseTextBox.GotFocus += (sender, args) =>
            {
                if (Enabled)
                {
                    isFocused = true;
                    _animationManager.StartNewAnimation(AnimationDirection.In);
                }
                else
                {
                    base.Focus();
                }
            };
            baseTextBox.LostFocus += (sender, args) =>
            {
                isFocused = false;
                _animationManager.StartNewAnimation(AnimationDirection.Out);
            };

            baseTextBox.TextChanged += new EventHandler(Redraw);
            baseTextBox.BackColorChanged += new EventHandler(Redraw);

            baseTextBox.TabStop = true;
            this.TabStop = false;

            cms.Opening += ContextMenuStripOnOpening;
            cms.OnItemClickStart += ContextMenuStripOnItemClickStart;
            ContextMenuStrip = cms;
            this.MouseWheel += OnMouseWheel;
        }

        private void Redraw(object sencer, EventArgs e)
        {
            SuspendLayout();
            Invalidate();
            ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);
            SolidBrush backBrush = new(BlendColor(Parent.BackColor, SkinManager.BackgroundAlternativeColor, SkinManager.BackgroundAlternativeColor.A));

            //backColor
            g.FillRectangle(
                !Enabled ? SkinManager.BackgroundDisabledBrush : // Disabled
                isFocused ? SkinManager.BackgroundFocusBrush :  // Focused
                MouseState == MaterialMouseState.HOVER && (!ReadOnly || (ReadOnly && !AnimateReadOnly)) ? SkinManager.BackgroundHoverBrush : // Hover
                backBrush, // Normal
                ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, LINE_Y);

            baseTextBox.BackColor = !Enabled ? MaterialColorHelper.RemoveAlpha(SkinManager.BackgroundDisabledColor, BackColor) : //Disabled
                isFocused ? BlendColor(BackColor, SkinManager.BackgroundFocusColor, SkinManager.BackgroundFocusColor.A) : //Focused
                MouseState == MaterialMouseState.HOVER && (!ReadOnly || (ReadOnly && !AnimateReadOnly)) ? BlendColor(BackColor, SkinManager.BackgroundHoverColor, SkinManager.BackgroundHoverColor.A) : // Hover
                BlendColor(BackColor, SkinManager.BackgroundAlternativeColor, SkinManager.BackgroundAlternativeColor.A); // Normal

            // bottom line base
            g.FillRectangle(SkinManager.DividersAlternativeBrush, 0, LINE_Y, Width, 1);

            if (ReadOnly == false || (ReadOnly && AnimateReadOnly))
            {
                if (!_animationManager.IsAnimating())
                {
                    // bottom line
                    if (isFocused)
                    {
                        //No animation
                        g.FillRectangle(isFocused ? UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush : SkinManager.DividersBrush, 0, LINE_Y, Width, isFocused ? 2 : 1);
                    }
                }
                else
                {
                    // Animate - Focus got/lost
                    double animationProgress = _animationManager.GetProgress();

                    // Line Animation
                    int LineAnimationWidth = (int)(Width * animationProgress);
                    int LineAnimationX = (Width / 2) - (LineAnimationWidth / 2);
                    g.FillRectangle(UseAccent ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, LineAnimationX, LINE_Y, LineAnimationWidth, 2);
                }
            }
        }


        [DllImport("User32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        protected static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        protected void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (AllowScroll)
            {
                if (DesignMode)
                {
                    return;
                }
                //Calculate number of notches mouse wheel moved
                int v = e.Delta / 120;
                //Down Movement
                if (v < 0)
                {
                    IntPtr ptrWparam = new(SB_LINEDOWN);
                    SendMessage(baseTextBox.Handle, WM_VSCROLL, ptrWparam, ptrLparam);
                }
                //Up Movement
                else if (v > 0)
                {
                    IntPtr ptrWparam = new(SB_LINEUP);
                    SendMessage(baseTextBox.Handle, WM_VSCROLL, ptrWparam, ptrLparam);
                }

                baseTextBox?.Focus();
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            baseTextBox?.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            base.OnMouseEnter(e);
            MouseState = MaterialMouseState.HOVER;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                return;
            }
            else
            {
                base.OnMouseLeave(e);
                MouseState = MaterialMouseState.OUT;
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            baseTextBox.Location = new Point(LEFT_PADDING, TOP_PADDING);
            baseTextBox.Width = Width - (LEFT_PADDING + RIGHT_PADDING);
            baseTextBox.Height = Height - (TOP_PADDING + BOTTOM_PADDING);

            LINE_Y = Height - LINE_BOTTOM_PADDING;

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // events
            MouseState = MaterialMouseState.OUT;
        }

        private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
        {
            switch (toolStripItemClickedEventArgs.ClickedItem.Text)
            {
                case "Undo":
                    Undo();
                    break;
                case "Cut":
                    Cut();
                    break;
                case "Copy":
                    Copy();
                    break;
                case "Paste":
                    Paste();
                    break;
                case "Delete":
                    SelectedText = string.Empty;
                    break;
                case "Select All":
                    SelectAll();
                    break;
            }
        }

        private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
        {
            if (sender is MaterialBaseTextBoxContextMenuStrip strip)
            {
                strip.undo.Enabled = baseTextBox.CanUndo && !ReadOnly;
                strip.cut.Enabled = !string.IsNullOrEmpty(SelectedText) && !ReadOnly;
                strip.copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                strip.paste.Enabled = Clipboard.ContainsText() && !ReadOnly;
                strip.delete.Enabled = !string.IsNullOrEmpty(SelectedText) && !ReadOnly;
                strip.selectAll.Enabled = !string.IsNullOrEmpty(Text);
            }
        }

        private void LeaveOnEnterKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && e.Control == false)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
    }

    #endregion
}