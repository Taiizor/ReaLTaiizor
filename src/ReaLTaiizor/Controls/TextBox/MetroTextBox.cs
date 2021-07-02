#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
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
    #region MetroTextBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroTextBox), "Bitmaps.TextBox.bmp")]
    [Designer(typeof(MetroTextBoxDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    public class MetroTextBox : Control, IMetroControl
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
        private HorizontalAlignment _textAlign;
        private int _maxLength;
        private bool _readOnly;
        private bool _useSystemPasswordChar;
        private string _watermarkText;
        private Image _image;
        private MouseMode _state;
        private AutoCompleteSource _autoCompleteSource;
        private AutoCompleteMode _autoCompleteMode;
        private AutoCompleteStringCollection _autoCompleteCustomSource;
        private bool _multiline;
        private string[] _lines;
        private Color _backColor;
        private Color _foreColor;
        private Color _borderColor;
        private Color _hoverColor;

        private bool _isDerivedStyle = true;
        private Color _disabledForeColor;
        private Color _disabledBackColor;
        private Color _disabledBorderColor;

        #region Base TextBox

        private readonly TextBox _textBox = new();

        #endregion

        #endregion Internal Vars

        #region Constructors

        public MetroTextBox()
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                    true
            );
            UpdateStyles();
            Font = MetroFonts.Regular(10);
            EvaluateVars();
            ApplyTheme();
            T_Defaults();
            if (!Multiline)
            {
                Size = new(135, 30);
            }
        }

        private void EvaluateVars()
        {
            _utl = new Utilites();
        }

        private void T_Defaults()
        {
            _watermarkText = string.Empty;
            _useSystemPasswordChar = false;
            _readOnly = false;
            _maxLength = 32767;
            _textAlign = HorizontalAlignment.Left;
            _state = MouseMode.Normal;
            _autoCompleteMode = AutoCompleteMode.None;
            _autoCompleteSource = AutoCompleteSource.None;
            _lines = null;
            _multiline = false;
            _textBox.Multiline = _multiline;
            _textBox.Cursor = Cursors.IBeam;
            _textBox.BackColor = BackColor;
            _textBox.ForeColor = ForeColor;
            _textBox.BorderStyle = BorderStyle.None;
            _textBox.Location = new(7, 8);
            _textBox.Font = Font;
            _textBox.UseSystemPasswordChar = UseSystemPasswordChar;
            if (Multiline)
            {
                _textBox.Height = Height - 11;
            }
            else
            {
                Height = _textBox.Height + 11;
            }

            _textBox.MouseHover += T_MouseHover;
            _textBox.Leave += T_Leave;
            _textBox.Enter += T_Enter;
            _textBox.KeyDown += T_KeyDown;
            _textBox.TextChanged += T_TextChanged;
            _textBox.KeyPress += T_KeyPress;
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
                if (_state == MouseMode.Normal)
                {
                    g.DrawRectangle(p, rect);
                }
                else if (_state == MouseMode.Hovered)
                {
                    g.DrawRectangle(ph, rect);
                }
            }
            else
            {
                using SolidBrush bg = new(DisabledBackColor);
                using Pen p = new(DisabledBorderColor);
                g.FillRectangle(bg, rect);
                g.DrawRectangle(p, rect);
                _textBox.BackColor = DisabledBackColor;
                _textBox.ForeColor = DisabledForeColor;
            }
            if (Image != null)
            {
                _textBox.Location = new(31, 4);
                _textBox.Width = Width - 60;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(Image, new Rectangle(7, 6, 18, 18));
            }
            else
            {
                _textBox.Location = new(7, 4);
                _textBox.Width = Width - 10;
            }

        }

        #endregion

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
                    HoverColor = Color.FromArgb(65, 177, 225);
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
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.TextBoxDictionary)
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
                                case "WatermarkText":
                                    WatermarkText = (string)varkey.Value;
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
            }
        }

        public void UpdateProperties()
        {
            Invalidate();
        }

        #endregion Theme Changing

        #region Events

        public new event EventHandler TextChanged;
        public event KeyPressEventHandler KeyPressed;
        public new event EventHandler Leave;

        public void T_Leave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
            Leave?.Invoke(sender, e);
        }

        public void T_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _state = MouseMode.Normal;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseMode.Pushed;
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
            //if (!Multiline)
            //{
            _textBox.Size = new(Width - 10, Height - 10);
            //}
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _textBox.Focus();
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

            if (!e.Control || e.KeyCode != Keys.C)
            {
                return;
            }

            _textBox.Copy();
            e.SuppressKeyPress = true;
        }

        private void T_TextChanged(object sender, EventArgs e)
        {
            Text = _textBox.Text;
            TextChanged?.Invoke(this, e);
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!Controls.Contains(_textBox))
            {
                Controls.Add(_textBox);
            }
        }

        public void AppendText(string text)
        {
            _textBox?.AppendText(text);
        }

        public void Undo()
        {
            if (_textBox == null)
            {
                return;
            }

            if (_textBox.CanUndo)
            {
                _textBox.Undo();
            }
        }

        public int GetLineFromCharIndex(int index)
        {
            return _textBox?.GetLineFromCharIndex(index) ?? 0;
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return _textBox.GetPositionFromCharIndex(index);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            return _textBox?.GetCharIndexFromPosition(pt) ?? 0;
        }

        public void ClearUndo()
        {
            _textBox?.ClearUndo();
        }

        public void Copy()
        {
            _textBox?.Copy();
        }

        public void Cut()
        {
            _textBox?.Cut();
        }

        public void SelectAll()
        {
            _textBox?.SelectAll();
        }

        public void DeselectAll()
        {
            _textBox?.DeselectAll();
        }

        public void Paste(string clipFormat)
        {
            _textBox?.Paste(clipFormat);
        }

        public void Select(int start, int length)
        {
            _textBox?.Select(start, length);
        }

        #endregion

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BorderStyle BorderStyle => BorderStyle.None;

        [Category("Metro"), Description("Gets or sets how text is aligned in a TextBox control.")]
        public HorizontalAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                _textAlign = value;
                TextBox text = _textBox;
                if (text != null)
                {
                    text.TextAlign = value;
                }

                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets how text is aligned in a TextBox control.")]
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                _maxLength = value;
                if (_textBox != null)
                {
                    _textBox.MaxLength = value;
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
                _textBox.BackColor = value;
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
                _textBox.ForeColor = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                if (_textBox != null)
                {
                    _textBox.ReadOnly = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get => _useSystemPasswordChar;
            set
            {
                _useSystemPasswordChar = value;
                if (_textBox != null)
                {
                    _textBox.UseSystemPasswordChar = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                if (_textBox == null)
                {
                    return;
                }

                _textBox.Multiline = value;
                if (value)
                {
                    _textBox.Height = Height - 10;
                }
                else
                {
                    Height = _textBox.Height + 10;
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage => null;

        [Category("Metro"), Description("Gets or sets the current text in the TextBox.")]
        public override string Text
        {
            get => _textBox.Text;
            set
            {
                base.Text = value;
                if (_textBox != null)
                {
                    _textBox.Text = value;
                }
            }
        }

        [Category("Metro"), Description("Gets or sets the text in the TextBox while being empty.")]
        public string WatermarkText
        {
            get => _watermarkText;
            set
            {
                _watermarkText = value;
                User32.SendMessage(_textBox.Handle, 5377, 0, value);
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the image of the control.")]
        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteSource AutoCompleteSource
        {
            get => _autoCompleteSource;
            set
            {
                _autoCompleteSource = value;
                if (_textBox != null)
                {
                    _textBox.AutoCompleteSource = value;
                }

                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets a value specifying the source of complete strings used for automatic completion.")]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get => _autoCompleteCustomSource;
            set
            {
                _autoCompleteCustomSource = value;
                if (_textBox != null)
                {
                    _textBox.AutoCompleteCustomSource = value;
                }

                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets an option that controls how automatic completion works for the TextBox.")]
        public AutoCompleteMode AutoCompleteMode
        {
            get => _autoCompleteMode;
            set
            {
                _autoCompleteMode = value;
                if (_textBox != null)
                {
                    _textBox.AutoCompleteMode = value;
                }

                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the font of the text displayed by the control.")]
        public sealed override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                if (_textBox == null)
                {
                    return;
                }

                _textBox.Font = value;
                _textBox.Location = new(5, 5);
                _textBox.Width = Width - 8;
                if (!Multiline)
                {
                    Height = _textBox.Height + 11;
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
                if (_textBox != null)
                {
                    _textBox.Lines = value;
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
                if (_textBox == null)
                {
                    return;
                }

                _textBox.ContextMenuStrip = value;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set
            {
                _disabledForeColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set
            {
                _disabledBackColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
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

        #endregion
    }

    #endregion
}