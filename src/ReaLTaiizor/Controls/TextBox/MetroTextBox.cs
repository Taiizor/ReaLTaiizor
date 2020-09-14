#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using System.Drawing.Drawing2D;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroTextBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroTextBox), "Bitmaps.TextBox.bmp")]
    [Designer(typeof(MetroTextBoxDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroTextBox : Control, iControl
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
        public StyleManager StyleManager
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
        private StyleManager _styleManager;
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

        #region Base TextBox

        private TextBox T = new TextBox();

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
                Size = new Size(135, 30);
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
            T.Multiline = _multiline;
            T.Cursor = Cursors.IBeam;
            T.BackColor = BackColor;
            T.ForeColor = ForeColor;
            T.BorderStyle = BorderStyle.None;
            T.Location = new Point(7, 8);
            T.Font = Font;
            T.UseSystemPasswordChar = UseSystemPasswordChar;
            if (Multiline)
            {
                T.Height = Height - 11;
            }
            else
            {
                Height = T.Height + 11;
            }

            T.MouseHover += T_MouseHover;
            T.Leave += T_Leave;
            T.Enter += T_Enter;
            T.KeyDown += T_KeyDown;
            T.TextChanged += T_TextChanged;
            T.KeyPress += T_KeyPress;

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
                            if (_state == MouseMode.Normal)
                                G.DrawRectangle(p, rect);
                            else if (_state == MouseMode.Hovered)
                            {
                                G.DrawRectangle(ph, rect);
                            }
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
            if (Image != null)
            {
                T.Location = new Point(31, 4);
                T.Width = Width - 60;
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                G.DrawImage(Image, new Rectangle(7, 6, 18, 18));
            }
            else
            {
                T.Location = new Point(7, 4);
                T.Width = Width - 10;
            }

        }

        #endregion

        #region ApplyTheme

        internal void ApplyTheme(Style style = Style.Light)
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
                        foreach (var varkey in StyleManager.TextBoxDictionary)
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

        public new event TextChangedEventHandler TextChanged;
        public delegate void TextChangedEventHandler(object sender);

        public virtual event KeyPressEventHandler KeyPressed;
        public delegate void KeyPressEventHandler(object sender);

        public void T_Leave(object sender, EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        public void T_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPressed?.Invoke(this);
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
            //if (!Multiline)
            //{
            T.Size = new Size(Width - 10, Height - 10);
            //}
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
            if (!e.Control || e.KeyCode != Keys.C) return;
            T.Copy();
            e.SuppressKeyPress = true;
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
            T?.AppendText(text);
        }

        public void Undo()
        {
            if (T == null) return;
            if (T.CanUndo)
                T.Undo();
        }

        public int GetLineFromCharIndex(int index)
        {
            return T?.GetLineFromCharIndex(index) ?? 0;
        }

        public Point GetPositionFromCharIndex(int index)
        {
            return T.GetPositionFromCharIndex(index);
        }

        public int GetCharIndexFromPosition(Point pt)
        {
            return T?.GetCharIndexFromPosition(pt) ?? 0;
        }

        public void ClearUndo()
        {
            T?.ClearUndo();
        }

        public void Copy()
        {
            T?.Copy();
        }

        public void Cut()
        {
            T?.Cut();
        }

        public void SelectAll()
        {
            T?.SelectAll();
        }

        public void DeselectAll()
        {
            T?.DeselectAll();
        }

        public void Paste(string clipFormat)
        {
            T?.Paste(clipFormat);
        }

        public void Select(int start, int length)
        {
            T?.Select(start, length);
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
                var text = T;
                if (text != null)
                    text.TextAlign = value;
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

        [Category("Metro"), Description("Gets or sets a value indicating whether text in the text box is read-only.")]
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

        [Category("Metro"), Description("Gets or sets a value indicating whether the text in the TextBox control should appear as the default password character.")]
        public bool UseSystemPasswordChar
        {
            get => _useSystemPasswordChar;
            set
            {
                _useSystemPasswordChar = value;
                if (T != null)
                    T.UseSystemPasswordChar = value;
            }
        }

        [Category("Metro"), Description("Gets or sets a value indicating whether this is a multiline TextBox control.")]
        public bool Multiline
        {
            get => _multiline;
            set
            {
                _multiline = value;
                if (T == null)
                { return; }
                T.Multiline = value;
                if (value)
                    T.Height = Height - 10;
                else
                    Height = T.Height + 10;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage => null;

        [Category("Metro"), Description("Gets or sets the current text in the TextBox.")]
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

        [Category("Metro"), Description("Gets or sets the text in the TextBox while being empty.")]
        public string WatermarkText
        {
            get => _watermarkText;
            set
            {
                _watermarkText = value;
                User32.SendMessage(T.Handle, 5377, 0, value);
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
                if (T != null)
                    T.AutoCompleteSource = value;
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
                if (T != null)
                    T.AutoCompleteCustomSource = value;
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
                if (T != null)
                    T.AutoCompleteMode = value;
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
                if (T == null)
                    return;
                T.Font = value;
                T.Location = new Point(5, 5);
                T.Width = Width - 8;
                if (!Multiline)
                    Height = T.Height + 11;
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

        [Category("Metro"), Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets disabled backcolor used by the control.")]
        public Color DisabledBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets the border color while the control disabled.")]
        public Color DisabledBorderColor { get; set; }

        #endregion
    }

    #endregion
}