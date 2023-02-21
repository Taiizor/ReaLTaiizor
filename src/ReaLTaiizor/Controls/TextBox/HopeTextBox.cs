#region Imports

using ReaLTaiizor.Colors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeTextBox

    public class HopeTextBox : Control
    {
        private readonly TextBoxHopeBase _baseTextBox = new()
        {
            BorderStyle = BorderStyle.None,
            ForeColor = HopeColors.MainText,
            BackColor = Color.White
        };

        public override string Text { get => _baseTextBox.Text; set => _baseTextBox.Text = value; }
        public new object Tag { get => _baseTextBox.Tag; set => _baseTextBox.Tag = value; }
        public int MaxLength { get => _baseTextBox.MaxLength; set => _baseTextBox.MaxLength = value; }

        public string SelectedText { get => _baseTextBox.SelectedText; set => _baseTextBox.SelectedText = value; }
        public string Hint { get => _baseTextBox.Hint; set => _baseTextBox.Hint = value; }

        public int SelectionStart { get => _baseTextBox.SelectionStart; set => _baseTextBox.SelectionStart = value; }
        public int SelectionLength { get => _baseTextBox.SelectionLength; set => _baseTextBox.SelectionLength = value; }
        public int TextLength => _baseTextBox.TextLength;

        public bool UseSystemPasswordChar { get => _baseTextBox.UseSystemPasswordChar; set => _baseTextBox.UseSystemPasswordChar = value; }
        public char PasswordChar { get => _baseTextBox.PasswordChar; set => _baseTextBox.PasswordChar = value; }

        public bool Multiline { get => _baseTextBox.Multiline; set => _baseTextBox.Multiline = value; }

        public ScrollBars ScrollBars { get => _baseTextBox.ScrollBars; set => _baseTextBox.ScrollBars = value; }

        public void SelectAll() { _baseTextBox.SelectAll(); }
        public void Clear() { _baseTextBox.Clear(); }
        public new void Focus() { _baseTextBox.Focus(); }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                _baseTextBox.Font = value;
                Invalidate();
            }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                _baseTextBox.ForeColor = value;
                Invalidate();
            }
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                _baseTextBox.BackColor = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.FromArgb(44, 55, 66);
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _BorderColorA = HopeColors.PrimaryColor;
        public Color BorderColorA
        {
            get => _BorderColorA;
            set
            {
                _BorderColorA = value;
                Invalidate();
            }
        }

        private Color _BorderColorB = HopeColors.OneLevelBorder;
        public Color BorderColorB
        {
            get => _BorderColorB;
            set
            {
                _BorderColorB = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _baseTextBox.Location = new(12, 8);
            _baseTextBox.Width = Width - 24;
            _baseTextBox.Height = (Height - 16) > 0 ? (Height - 16) : 0;
            Height = _baseTextBox.Height + 16;

            Graphics g = e.Graphics;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(_BaseColor);

            GraphicsPath bg = RoundRectangle.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, 3);
            g.FillPath(new SolidBrush(BackColor), bg);
            g.DrawPath(new(_baseTextBox.Focused ? _BorderColorA : _BorderColorB, 0.5f), bg);
        }

        public HopeTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 12);
            ForeColor = HopeColors.MainText;
            BackColor = Color.White;

            if (!Controls.Contains(_baseTextBox) && !DesignMode)
            {
                Controls.Add(_baseTextBox);
            }

            _baseTextBox.GotFocus += _baseTextBox_GotFocus;
            _baseTextBox.LostFocus += _baseTextBox_LostFocus;
            _baseTextBox.KeyPress += _baseTextBox_KeyPress;
            _baseTextBox.TabStop = true;
            TabStop = false;
            Width = 120;
        }

        private void _baseTextBox_LostFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void _baseTextBox_GotFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void _baseTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        #region baseTextBox Event
        public event EventHandler AcceptsTabChanged
        {
            add => _baseTextBox.AcceptsTabChanged += value;
            remove => _baseTextBox.AcceptsTabChanged -= value;
        }

        public new event EventHandler AutoSizeChanged
        {
            add => _baseTextBox.AutoSizeChanged += value;
            remove => _baseTextBox.AutoSizeChanged -= value;
        }

        public new event EventHandler BackgroundImageChanged
        {
            add => _baseTextBox.BackgroundImageChanged += value;
            remove => _baseTextBox.BackgroundImageChanged -= value;
        }

        public new event EventHandler BackgroundImageLayoutChanged
        {
            add => _baseTextBox.BackgroundImageLayoutChanged += value;
            remove => _baseTextBox.BackgroundImageLayoutChanged -= value;
        }

        public new event EventHandler BindingContextChanged
        {
            add => _baseTextBox.BindingContextChanged += value;
            remove => _baseTextBox.BindingContextChanged -= value;
        }

        public event EventHandler BorderStyleChanged
        {
            add => _baseTextBox.BorderStyleChanged += value;
            remove => _baseTextBox.BorderStyleChanged -= value;
        }

        public new event EventHandler CausesValidationChanged
        {
            add => _baseTextBox.CausesValidationChanged += value;
            remove => _baseTextBox.CausesValidationChanged -= value;
        }

        public new event UICuesEventHandler ChangeUICues
        {
            add => _baseTextBox.ChangeUICues += value;
            remove => _baseTextBox.ChangeUICues -= value;
        }

        public new event EventHandler Click
        {
            add => _baseTextBox.Click += value;
            remove => _baseTextBox.Click -= value;
        }

        public new event EventHandler ClientSizeChanged
        {
            add => _baseTextBox.ClientSizeChanged += value;
            remove => _baseTextBox.ClientSizeChanged -= value;
        }

#if !NETCOREAPP3_1 && !NET7_0 && !NET8_0
        public new event EventHandler ContextMenuChanged
        {
            add => _baseTextBox.ContextMenuChanged += value;
            remove => _baseTextBox.ContextMenuChanged -= value;
        }
#endif

        public new event EventHandler ContextMenuStripChanged
        {
            add => _baseTextBox.ContextMenuStripChanged += value;
            remove => _baseTextBox.ContextMenuStripChanged -= value;
        }

        public new event ControlEventHandler ControlAdded
        {
            add => _baseTextBox.ControlAdded += value;
            remove => _baseTextBox.ControlAdded -= value;
        }

        public new event ControlEventHandler ControlRemoved
        {
            add => _baseTextBox.ControlRemoved += value;
            remove => _baseTextBox.ControlRemoved -= value;
        }

        public new event EventHandler CursorChanged
        {
            add => _baseTextBox.CursorChanged += value;
            remove => _baseTextBox.CursorChanged -= value;
        }

        public new event EventHandler Disposed
        {
            add => _baseTextBox.Disposed += value;
            remove => _baseTextBox.Disposed -= value;
        }

        public new event EventHandler DockChanged
        {
            add => _baseTextBox.DockChanged += value;
            remove => _baseTextBox.DockChanged -= value;
        }

        public new event EventHandler DoubleClick
        {
            add => _baseTextBox.DoubleClick += value;
            remove => _baseTextBox.DoubleClick -= value;
        }

        public new event DragEventHandler DragDrop
        {
            add => _baseTextBox.DragDrop += value;
            remove => _baseTextBox.DragDrop -= value;
        }

        public new event DragEventHandler DragEnter
        {
            add => _baseTextBox.DragEnter += value;
            remove => _baseTextBox.DragEnter -= value;
        }

        public new event EventHandler DragLeave
        {
            add => _baseTextBox.DragLeave += value;
            remove => _baseTextBox.DragLeave -= value;
        }

        public new event DragEventHandler DragOver
        {
            add => _baseTextBox.DragOver += value;
            remove => _baseTextBox.DragOver -= value;
        }

        public new event EventHandler EnabledChanged
        {
            add => _baseTextBox.EnabledChanged += value;
            remove => _baseTextBox.EnabledChanged -= value;
        }

        public new event EventHandler Enter
        {
            add => _baseTextBox.Enter += value;
            remove => _baseTextBox.Enter -= value;
        }

        public new event EventHandler FontChanged
        {
            add => _baseTextBox.FontChanged += value;
            remove => _baseTextBox.FontChanged -= value;
        }

        public new event EventHandler ForeColorChanged
        {
            add => _baseTextBox.ForeColorChanged += value;
            remove => _baseTextBox.ForeColorChanged -= value;
        }

        public new event GiveFeedbackEventHandler GiveFeedback
        {
            add => _baseTextBox.GiveFeedback += value;
            remove => _baseTextBox.GiveFeedback -= value;
        }

        public new event EventHandler GotFocus
        {
            add => _baseTextBox.GotFocus += value;
            remove => _baseTextBox.GotFocus -= value;
        }

        public new event EventHandler HandleCreated
        {
            add => _baseTextBox.HandleCreated += value;
            remove => _baseTextBox.HandleCreated -= value;
        }

        public new event EventHandler HandleDestroyed
        {
            add => _baseTextBox.HandleDestroyed += value;
            remove => _baseTextBox.HandleDestroyed -= value;
        }

        public new event HelpEventHandler HelpRequested
        {
            add => _baseTextBox.HelpRequested += value;
            remove => _baseTextBox.HelpRequested -= value;
        }

        public event EventHandler HideSelectionChanged
        {
            add => _baseTextBox.HideSelectionChanged += value;
            remove => _baseTextBox.HideSelectionChanged -= value;
        }

        public new event EventHandler ImeModeChanged
        {
            add => _baseTextBox.ImeModeChanged += value;
            remove => _baseTextBox.ImeModeChanged -= value;
        }

        public new event InvalidateEventHandler Invalidated
        {
            add => _baseTextBox.Invalidated += value;
            remove => _baseTextBox.Invalidated -= value;
        }

        public new event KeyEventHandler KeyDown
        {
            add => _baseTextBox.KeyDown += value;
            remove => _baseTextBox.KeyDown -= value;
        }

        public new event KeyPressEventHandler KeyPress
        {
            add => _baseTextBox.KeyPress += value;
            remove => _baseTextBox.KeyPress -= value;
        }

        public new event KeyEventHandler KeyUp
        {
            add => _baseTextBox.KeyUp += value;
            remove => _baseTextBox.KeyUp -= value;
        }

        public new event LayoutEventHandler Layout
        {
            add => _baseTextBox.Layout += value;
            remove => _baseTextBox.Layout -= value;
        }

        public new event EventHandler Leave
        {
            add => _baseTextBox.Leave += value;
            remove => _baseTextBox.Leave -= value;
        }

        public new event EventHandler LocationChanged
        {
            add => _baseTextBox.LocationChanged += value;
            remove => _baseTextBox.LocationChanged -= value;
        }

        public new event EventHandler LostFocus
        {
            add => _baseTextBox.LostFocus += value;
            remove => _baseTextBox.LostFocus -= value;
        }

        public new event EventHandler MarginChanged
        {
            add => _baseTextBox.MarginChanged += value;
            remove => _baseTextBox.MarginChanged -= value;
        }

        public event EventHandler ModifiedChanged
        {
            add => _baseTextBox.ModifiedChanged += value;
            remove => _baseTextBox.ModifiedChanged -= value;
        }

        public new event EventHandler MouseCaptureChanged
        {
            add => _baseTextBox.MouseCaptureChanged += value;
            remove => _baseTextBox.MouseCaptureChanged -= value;
        }

        public new event MouseEventHandler MouseClick
        {
            add => _baseTextBox.MouseClick += value;
            remove => _baseTextBox.MouseClick -= value;
        }

        public new event MouseEventHandler MouseDoubleClick
        {
            add => _baseTextBox.MouseDoubleClick += value;
            remove => _baseTextBox.MouseDoubleClick -= value;
        }

        public new event MouseEventHandler MouseDown
        {
            add => _baseTextBox.MouseDown += value;
            remove => _baseTextBox.MouseDown -= value;
        }

        public new event EventHandler MouseEnter
        {
            add => _baseTextBox.MouseEnter += value;
            remove => _baseTextBox.MouseEnter -= value;
        }

        public new event EventHandler MouseHover
        {
            add => _baseTextBox.MouseHover += value;
            remove => _baseTextBox.MouseHover -= value;
        }

        public new event EventHandler MouseLeave
        {
            add => _baseTextBox.MouseLeave += value;
            remove => _baseTextBox.MouseLeave -= value;
        }

        public new event MouseEventHandler MouseMove
        {
            add => _baseTextBox.MouseMove += value;
            remove => _baseTextBox.MouseMove -= value;
        }

        public new event MouseEventHandler MouseUp
        {
            add => _baseTextBox.MouseUp += value;
            remove => _baseTextBox.MouseUp -= value;
        }

        public new event MouseEventHandler MouseWheel
        {
            add => _baseTextBox.MouseWheel += value;
            remove => _baseTextBox.MouseWheel -= value;
        }

        public new event EventHandler Move
        {
            add => _baseTextBox.Move += value;
            remove => _baseTextBox.Move -= value;
        }

        public event EventHandler MultilineChanged
        {
            add => _baseTextBox.MultilineChanged += value;
            remove => _baseTextBox.MultilineChanged -= value;
        }

        public new event EventHandler PaddingChanged
        {
            add => _baseTextBox.PaddingChanged += value;
            remove => _baseTextBox.PaddingChanged -= value;
        }

        public new event PaintEventHandler Paint
        {
            add => _baseTextBox.Paint += value;
            remove => _baseTextBox.Paint -= value;
        }

        public new event EventHandler ParentChanged
        {
            add => _baseTextBox.ParentChanged += value;
            remove => _baseTextBox.ParentChanged -= value;
        }

        public new event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add => _baseTextBox.PreviewKeyDown += value;
            remove => _baseTextBox.PreviewKeyDown -= value;
        }

        public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add => _baseTextBox.QueryAccessibilityHelp += value;
            remove => _baseTextBox.QueryAccessibilityHelp -= value;
        }

        public new event QueryContinueDragEventHandler QueryContinueDrag
        {
            add => _baseTextBox.QueryContinueDrag += value;
            remove => _baseTextBox.QueryContinueDrag -= value;
        }

        public event EventHandler ReadOnlyChanged
        {
            add => _baseTextBox.ReadOnlyChanged += value;
            remove => _baseTextBox.ReadOnlyChanged -= value;
        }

        public new event EventHandler RegionChanged
        {
            add => _baseTextBox.RegionChanged += value;
            remove => _baseTextBox.RegionChanged -= value;
        }

        public new event EventHandler Resize
        {
            add => _baseTextBox.Resize += value;
            remove => _baseTextBox.Resize -= value;
        }

        public new event EventHandler RightToLeftChanged
        {
            add => _baseTextBox.RightToLeftChanged += value;
            remove => _baseTextBox.RightToLeftChanged -= value;
        }

        public new event EventHandler SizeChanged
        {
            add => _baseTextBox.SizeChanged += value;
            remove => _baseTextBox.SizeChanged -= value;
        }

        public new event EventHandler StyleChanged
        {
            add => _baseTextBox.StyleChanged += value;
            remove => _baseTextBox.StyleChanged -= value;
        }

        public new event EventHandler SystemColorsChanged
        {
            add => _baseTextBox.SystemColorsChanged += value;
            remove => _baseTextBox.SystemColorsChanged -= value;
        }

        public new event EventHandler TabIndexChanged
        {
            add => _baseTextBox.TabIndexChanged += value;
            remove => _baseTextBox.TabIndexChanged -= value;
        }

        public new event EventHandler TabStopChanged
        {
            add => _baseTextBox.TabStopChanged += value;
            remove => _baseTextBox.TabStopChanged -= value;
        }

        public event EventHandler TextAlignChanged
        {
            add => _baseTextBox.TextAlignChanged += value;
            remove => _baseTextBox.TextAlignChanged -= value;
        }

        public new event EventHandler TextChanged
        {
            add => _baseTextBox.TextChanged += value;
            remove => _baseTextBox.TextChanged -= value;
        }

        public new event EventHandler Validated
        {
            add => _baseTextBox.Validated += value;
            remove => _baseTextBox.Validated -= value;
        }

        public new event CancelEventHandler Validating
        {
            add => _baseTextBox.Validating += value;
            remove => _baseTextBox.Validating -= value;
        }

        public new event EventHandler VisibleChanged
        {
            add => _baseTextBox.VisibleChanged += value;
            remove => _baseTextBox.VisibleChanged -= value;
        }
        #endregion

        private class TextBoxHopeBase : TextBox
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            private const int EM_SETCUEBANNER = 0x1501;
            private const char EmptyChar = (char)0;
            private const char VisualStylePasswordChar = '\u25CF';
            private const char NonVisualStylePasswordChar = '\u002A';

            private string hint = string.Empty;
            public string Hint
            {
                get => hint;
                set
                {
                    hint = value;
                    SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
                }
            }

            private char _passwordChar = EmptyChar;
            public new char PasswordChar
            {
                get => _passwordChar;
                set
                {
                    _passwordChar = value;
                    SetBasePasswordChar();
                }
            }

            public new void SelectAll()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                    base.SelectAll();
                });
            }

            public new void Focus()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                });
            }

            private char _useSystemPasswordChar = EmptyChar;
            public new bool UseSystemPasswordChar
            {
                get => _useSystemPasswordChar != EmptyChar;
                set
                {
                    if (value)
                    {
                        _useSystemPasswordChar = Application.RenderWithVisualStyles ? VisualStylePasswordChar : NonVisualStylePasswordChar;
                    }
                    else
                    {
                        _useSystemPasswordChar = EmptyChar;
                    }

                    SetBasePasswordChar();
                }
            }

#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
            //public EventHandler ContextMenuChanged { get; internal set; }
            public event EventHandler ContextMenuChanged;
#endif

            private void SetBasePasswordChar()
            {
                base.PasswordChar = UseSystemPasswordChar ? _useSystemPasswordChar : _passwordChar;
            }

            public TextBoxHopeBase()
            {

            }
        }
    }

    #endregion
}