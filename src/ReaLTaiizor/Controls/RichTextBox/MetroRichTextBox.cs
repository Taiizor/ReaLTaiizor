#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
    public class MetroRichTextBox : Control, IMetroControl
    {
        #region Interfaces

        [Category("Metro"), Description("Gets or sets the style associated with the control.")]
        public Style Style
        {
            get => StyleManager?.Style ?? _style;
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
        public MetroStyleManager StyleManager
        {
            get => _styleManager;
            set { _styleManager = value; Invalidate(); }
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
        private MetroStyleManager _styleManager;
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

        private bool _isDerivedStyle = true;
        private Color _disabledBackColor = Color.FromArgb(204, 204, 204);
        private Color _disabledForeColor = Color.FromArgb(136, 136, 136);
        private Color _disabledBorderColor = Color.FromArgb(155, 155, 155);

        #region Base RichTextBox

        private readonly RichTextBox _richTextBox = new();

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
            base.Font = MetroFonts.Regular(10);
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
            _foreColor = Color.FromArgb(20, 20, 20);

            _borderColor = Color.FromArgb(155, 155, 155);
            _hoverColor = Color.FromArgb(102, 102, 102);
            _backColor = Color.FromArgb(238, 238, 238);
            _richTextBox.BackColor = BackColor;
            _richTextBox.ForeColor = ForeColor;
            _readOnly = false;
            _maxLength = 32767;
            _state = MouseMode.Normal;
            _lines = null;
            _richTextBox.Cursor = Cursors.IBeam;
            _richTextBox.BorderStyle = BorderStyle.None;
            _richTextBox.Location = new(7, 8);
            _richTextBox.Font = Font;
            _richTextBox.Size = new(Width, Height);

            _richTextBox.MouseHover += T_MouseHover;
            _richTextBox.MouseUp += T_MouseUp;
            _richTextBox.Leave += T_Leave;
            _richTextBox.Enter += T_Enter;
            _richTextBox.KeyDown += T_KeyDown;
            _richTextBox.TextChanged += T_TextChanged;
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using SolidBrush bg = new(BackColor);
                using Pen p = new(BorderColor);
                using Pen ph = new(HoverColor);
                g.FillRectangle(bg, rect);
                switch (_state)
                {
                    case MouseMode.Normal:
                        g.DrawRectangle(p, rect);
                        break;
                    case MouseMode.Hovered:
                        g.DrawRectangle(ph, rect);
                        break;
                }

                _richTextBox.BackColor = BackColor;
                _richTextBox.ForeColor = ForeColor;
            }
            else
            {
                using SolidBrush bg = new(DisabledBackColor);
                using Pen p = new(DisabledBorderColor);
                g.FillRectangle(bg, rect);
                g.DrawRectangle(p, rect);
                _richTextBox.BackColor = DisabledBackColor;
                _richTextBox.ForeColor = DisabledForeColor;
            }

            _richTextBox.Location = new(7, 4);
            _richTextBox.Width = Width - 10;

        }

        #endregion Draw Control

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            if (!IsDerivedStyle)
            {
                return;
            }

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
                    ThemeName = "MetroLight";
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
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.RichTextBoxDictionary)
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
            _richTextBox.Invalidate();
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
                {
                    ContextMenuStrip.Show(_richTextBox, new Point(e.X, e.Y));
                }
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
            _richTextBox.Size = new(Width - 10, Height - 10);
        }

        public void T_Enter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        private void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                _richTextBox.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            Text = _richTextBox.Text;
            TextChanged?.Invoke(this);
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(_richTextBox))
            {
                Controls.Add(_richTextBox);
            }
        }

        public void AppendText(string text)
        {
            if (_richTextBox != null)
            {
                _richTextBox.AppendText(text);
            }
        }

        public void Undo()
        {
            if (_richTextBox != null)
            {
                if (_richTextBox.CanUndo)
                {
                    _richTextBox.Undo();
                }
            }
        }

        public int GetLineFromCharIndex(int index)
        {
            if (_richTextBox != null)
            {
                return _richTextBox.GetLineFromCharIndex(index);
            }
            else
            {
                return 0;
            }
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return _richTextBox.GetPositionFromCharIndex(index);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.GetCharIndexFromPosition(pt);
        }

        public void ClearUndo()
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.ClearUndo();
        }

        public void Copy()
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.Copy();
        }

        public void Cut()
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.Cut();
        }

        public void SelectAll()
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.SelectAll();
        }

        public void DeselectAll()
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.DeselectAll();
        }

        public void Select(int start, int length)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.Select(start, length);
        }

        public void Paste(DataFormats.Format clipFormat)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.Paste(clipFormat);
        }

        public void LoadFile(string path)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.LoadFile(path);
        }

        public void LoadFile(string path, RichTextBoxStreamType fileType)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.LoadFile(path, fileType);
        }

        public void LoadFile(System.IO.Stream data, RichTextBoxStreamType fileType)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.LoadFile(data, fileType);
        }

        public void SaveFile(string path)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.SaveFile(path);
        }

        public void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.SaveFile(path, fileType);
        }

        public void SaveFile(System.IO.Stream data, RichTextBoxStreamType fileType)
        {
            if (_richTextBox == null)
            {
                return;
            }

            _richTextBox.SaveFile(data, fileType);
        }

        public bool CanPaste(DataFormats.Format clipFormat)
        {
            return _richTextBox.CanPaste(clipFormat);
        }

        public int Find(char[] characterSet)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(characterSet);
        }

        public int Find(char[] characterSet, int start)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(characterSet, start);
        }

        public int Find(char[] characterSet, int start, int ends)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(characterSet, start, ends);
        }

        public int Find(string str)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(str);
        }

        public int Find(string str, int start, int ends, RichTextBoxFinds options)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(str, start, ends, options);
        }

        public int Find(string str, RichTextBoxFinds options)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(str, options);
        }

        public int Find(string str, int start, RichTextBoxFinds options)
        {
            if (_richTextBox == null)
            {
                return 0;
            }

            return _richTextBox.Find(str, start, options);
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
                if (_richTextBox != null)
                {
                    _richTextBox.MaxLength = value;
                }

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
                _richTextBox.BackColor = value;
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
                _richTextBox.ForeColor = value;
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
                if (_richTextBox != null)
                {
                    _richTextBox.ReadOnly = value;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage => null;

        [Category("Metro"), Description("Gets or sets the current text in the RichTextBox.")]
        public override string Text
        {
            get => _richTextBox.Text;
            set
            {
                base.Text = value;
                if (_richTextBox != null)
                {
                    _richTextBox.Text = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets the font of the text displayed by the control.")]
        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (_richTextBox == null)
                {
                    return;
                }

                _richTextBox.Font = value;
                _richTextBox.Location = new(5, 5);
                _richTextBox.Width = Width - 8;
            }
        }

        [Category("Metro"), Description("Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.")]
        public bool WordWrap
        {
            get => _wordWrap;
            set
            {
                _wordWrap = value;
                if (_richTextBox != null)
                {
                    _richTextBox.WordWrap = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether automatic word selection is enabled.")]
        public bool AutoWordSelection
        {
            get => _autoWordSelection;
            set
            {
                _autoWordSelection = value;
                if (_richTextBox != null)
                {
                    _richTextBox.AutoWordSelection = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets the lines of text in the control.")]
        public string[] Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                if (_richTextBox != null)
                {
                    _richTextBox.Lines = value;
                }

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
                if (_richTextBox == null)
                {
                    return;
                }

                _richTextBox.ContextMenuStrip = value;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets back color used by the control while disabled.")]
        public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set
            {
                _disabledBackColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the fore color of the control whenever while disabled")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set
            {
                _disabledForeColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the border color of the control while disabled.")]
        public Color DisabledBorderColor
        {
            get => _disabledBorderColor;
            set
            {
                _disabledBorderColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
                     "Set it to false if you want the style of this control be independent. ")]
        public bool IsDerivedStyle
        {
            get => _isDerivedStyle;
            set
            {
                _isDerivedStyle = value;
                Refresh();
            }
        }

        #endregion Properties
    }

    #endregion
}