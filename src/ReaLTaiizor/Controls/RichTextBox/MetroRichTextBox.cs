#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Extension.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroRichTextBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroRichTextBox), "Bitmaps.RichTextBox.bmp")]
    [Designer(typeof(MetroRichTextBoxDesigner))]
    [DefaultProperty("Text")]
    [DefaultEvent("TextChanged")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroRichTextBox : Control, iControl
    {
        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => MetroStyleManager?.Style ?? _style;
            set
            {
                _style = value;
                switch (value)
                {
                    case Style.Light:
                        ApplyTheme();
                        break;
                    case Style.Dark:
                        ApplyTheme(Style.Dark);
                        break;
                    case Style.Custom:
                        ApplyTheme(Style.Custom);
                        break;
                    default:
                        ApplyTheme();
                        break;
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager MetroStyleManager
        {
            get => _metroStyleManager;
            set { _metroStyleManager = value; Invalidate(); }
        }

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        #endregion Interfaces

        #region Global Vars

        private Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _metroStyleManager;
        private int _maxLength;
        private bool _readOnly;
        private MouseMode _state;
        private bool _wordWrap;
        private bool _autoWordSelection;
        private string[] _lines;
        private Color _foreColor;
        private Color _backColor;
        private Color _borderColor;
        private Color _hoverColor;

        #region Base RichTextBox

        private RichTextBox T = new RichTextBox();

        #endregion Base RichTextBox

        #endregion Internal Vars

        #region Constructors

        public MetroRichTextBox()
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Font = MetroFonts.Regular(10);
            EvaluateVars();
            ApplyTheme();
            T_Defaults();
        }

        private void EvaluateVars()
        {
            _utl = new Utilites();
        }

        private void T_Defaults()
        {
            _wordWrap = true;
            _autoWordSelection = false;
            _foreColor = Color.FromArgb(20, 20, 20); ;
            _borderColor = Color.FromArgb(155, 155, 155);
            _hoverColor = Color.FromArgb(102, 102, 102);
            _backColor = Color.FromArgb(238, 238, 238);
            T.BackColor = BackColor;
            T.ForeColor = ForeColor;
            _readOnly = false;
            _maxLength = 32767;
            _state = MouseMode.Normal;
            _lines = null;
            T.Cursor = Cursors.IBeam;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 8);
            T.Font = Font;
            T.Size = new Size(Width, Height);

            T.MouseHover += T_MouseHover;
            T.MouseUp += T_MouseUp;
            T.Leave += T_Leave;
            T.Enter += T_Enter;
            T.KeyDown += T_KeyDown;
            T.TextChanged += T_TextChanged;

        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (var bg = new SolidBrush(BackColor))
                {
                    using (var p = new Pen(BorderColor))
                    {
                        using (var ph = new Pen(HoverColor))
                        {
                            G.FillRectangle(bg, rect);
                            switch (_state)
                            {
                                case MouseMode.Normal:
                                    G.DrawRectangle(p, rect);
                                    break;
                                case MouseMode.Hovered:
                                    G.DrawRectangle(ph, rect);
                                    break;
                            }

                            T.BackColor = BackColor;
                            T.ForeColor = ForeColor;
                        }
                    }
                }
            }
            else
            {
                using (var bg = new SolidBrush(DisabledBackColor))
                {
                    using (var p = new Pen(DisabledBorderColor))
                    {
                        G.FillRectangle(bg, rect);
                        G.DrawRectangle(p, rect);
                        T.BackColor = DisabledBackColor;
                        T.ForeColor = DisabledForeColor;
                    }
                }
            }

            T.Location = new Point(7, 4);
            T.Width = Width - 10;

        }

        #endregion Draw Control

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.FromArgb(20, 20, 20);
                    BackColor = Color.FromArgb(238, 238, 238);
                    HoverColor = Color.FromArgb(102, 102, 102);
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackColor = Color.FromArgb(34, 34, 34);
                    HoverColor = Color.FromArgb(170, 170, 170);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.RichTextBoxDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "HoverColor":
                                    HoverColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                default:
                                    return;
                            }
                        }
                    UpdateProperties();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        private void UpdateProperties()
        {
            Invalidate();
            T.Invalidate();
        }

        #endregion ApplyTheme

        #region Events

        public new event TextChangedEventHandler TextChanged;

        public delegate void TextChangedEventHandler(object sender);

        public event SelectionChangedEventHandler SelectionChanged;

        public delegate void SelectionChangedEventHandler(object sender, EventArgs e);

        public event LinkClickedEventHandler LinkClicked;

        public delegate void LinkClickedEventHandler(object sender, EventArgs e);

        public event ProtectedEventHandler Protected;

        public delegate void ProtectedEventHandler(object sender, EventArgs e);

        private void T_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged?.Invoke(sender, e);
        }

        private void T_LinkClicked(object sender, EventArgs e)
        {
            LinkClicked?.Invoke(sender, e);
        }

        private void T_Protected(object sender, EventArgs e)
        {
            Protected?.Invoke(sender, e);
        }

        public void T_Leave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _state = MouseMode.Normal;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        public void T_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
            {
                if (ContextMenuStrip != null)
                    ContextMenuStrip.Show(T, new Point(e.X, e.Y));
            }
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        public void T_MouseHover(object sender, EventArgs e)
        {
            base.OnMouseHover(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            T.Size = new Size(Width - 10, Height - 10);
        }

        public void T_Enter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.SuppressKeyPress = true;
            if (e.Control && e.KeyCode == Keys.C)
            {
                T.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            Text = T.Text;
            TextChanged?.Invoke(this);
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(T))
                Controls.Add(T);
        }

        public void AppendText(string text)
        {
            if (T != null)
                T.AppendText(text);
        }

        public void Undo()
        {
            if (T != null)
            {
                if (T.CanUndo)
                    T.Undo();
            }
        }

        public int GetLineFromCharIndex(int index)
        {
            if (T != null)
                return T.GetLineFromCharIndex(index);
            else
                return 0;
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return T.GetPositionFromCharIndex(index);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            if (T == null)
                return 0;
            return T.GetCharIndexFromPosition(pt);
        }

        public void ClearUndo()
        {
            if (T == null)
                return;
            T.ClearUndo();
        }

        public void Copy()
        {
            if (T == null)
                return;
            T.Copy();
        }

        public void Cut()
        {
            if (T == null)
                return;
            T.Cut();
        }

        public void SelectAll()
        {
            if (T == null)
                return;
            T.SelectAll();
        }

        public void DeselectAll()
        {
            if (T == null)
                return;
            T.DeselectAll();
        }

        public void Select(int start, int length)
        {
            if (T == null)
                return;
            T.Select(start, length);
        }

        public void Paste(DataFormats.Format clipFormat)
        {
            if (T == null)
                return;
            T.Paste(clipFormat);
        }

        public void LoadFile(string path)
        {
            if (T == null)
                return;
            T.LoadFile(path);
        }

        public void LoadFile(string path, RichTextBoxStreamType fileType)
        {
            if (T == null)
                return;
            T.LoadFile(path, fileType);
        }

        public void LoadFile(System.IO.Stream data, RichTextBoxStreamType fileType)
        {
            if (T == null)
                return;
            T.LoadFile(data, fileType);
        }

        public void SaveFile(string path)
        {
            if (T == null)
                return;
            T.SaveFile(path);
        }
        public void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            if (T == null)
                return;
            T.SaveFile(path, fileType);
        }

        public void SaveFile(System.IO.Stream data, RichTextBoxStreamType fileType)
        {
            if (T == null)
                return;
            T.SaveFile(data, fileType);
        }

        public bool CanPaste(DataFormats.Format clipFormat)
        {
            return T.CanPaste(clipFormat);
        }

        public int Find(char[] characterSet)
        {
            if (T == null)
                return 0;
            return T.Find(characterSet);
        }

        public int Find(char[] characterSet, int start)
        {
            if (T == null)
                return 0;
            return T.Find(characterSet, start);
        }

        public int Find(char[] characterSet, int start, int ends)
        {
            if (T == null)
                return 0;
            return T.Find(characterSet, start, ends);
        }

        public int Find(string str)
        {
            if (T == null)
                return 0;
            return T.Find(str);
        }

        public int Find(string str, int start, int ends, RichTextBoxFinds options)
        {
            if (T == null)
                return 0;
            return T.Find(str, start, ends, options);
        }

        public int Find(string str, RichTextBoxFinds options)
        {
            if (T == null)
                return 0;
            return T.Find(str, options);
        }

        public int Find(string str, int start, RichTextBoxFinds options)
        {
            if (T == null)
                return 0;
            return T.Find(str, start, options);
        }

        #endregion Events

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle => BorderStyle.None;

        [Category("Metro"), Description("Gets or sets how text is aligned in a RichTextBox control.")]
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;
                if (T != null)
                    T.MaxLength = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the background color of the control.")]
        public override Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                T.BackColor = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the color of the control whenever hovered.")]
        public Color HoverColor
        {
            get => _hoverColor;
            set
            {
                _hoverColor = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the border color of the control.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the foreground color of the control.")]
        [Browsable(false)]
        public override Color ForeColor
        {
            get => _foreColor;
            set
            {
                _foreColor = value;
                T.ForeColor = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether text in the RichTextBox is read-only.")]
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                if (T != null)
                    T.ReadOnly = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage => null;

        [Category("Metro"), Description("Gets or sets the current text in the RichTextBox.")]
        public override string Text
        {
            get => T.Text;
            set
            {
                base.Text = value;
                if (T != null)
                    T.Text = value;
            }
        }

        [Category("Metro"), Description("Gets or sets the font of the text displayed by the control.")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (T == null)
                    return;
                T.Font = value;
                T.Location = new Point(5, 5);
                T.Width = Width - 8;
            }
        }

        [Category("Metro"), Description("Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.")]
        public bool WordWrap
        {
            get => _wordWrap;
            set
            {
                _wordWrap = value;
                if (T != null)
                    T.WordWrap = value;
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether automatic word selection is enabled.")]
        public bool AutoWordSelection
        {
            get => _autoWordSelection;
            set
            {
                _autoWordSelection = value;
                if (T != null)
                    T.AutoWordSelection = value;
            }
        }

        [Category("Metro"), Description("Gets or sets the lines of text in the control.")]
        public string[] Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                if (T != null)
                    T.Lines = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
        public override ContextMenuStrip ContextMenuStrip
        {
            get => base.ContextMenuStrip;
            set
            {
                base.ContextMenuStrip = value;
                if (T == null)
                    return;
                T.ContextMenuStrip = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        public Color DisabledBackColor { get; set; } = Color.FromArgb(204, 204, 204);

        [Category("Metro")]
        public Color DisabledForeColor { get; set; } = Color.FromArgb(136, 136, 136);

        [Category("Metro")]
        public Color DisabledBorderColor { get; set; } = Color.FromArgb(155, 155, 155);

        #endregion Properties
    }

    #endregion
}