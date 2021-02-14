#region Imports

using ReaLTaiizor.Animate.Metro;
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
    #region MetroRadioButton

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroRadioButton), "Bitmaps.RadioButton.bmp")]
    [Designer(typeof(MetroRadioButtonDesigner))]
    [DefaultEvent("CheckedChanged")]
    [DefaultProperty("Checked")]
    [ComVisible(true)]
    public class MetroRadioButton : Control, IMetroControl, IDisposable
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

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private bool _checked;
        private readonly IntAnimate _animator;

        private bool _isDerivedStyle = true;
        private int _group;
        private Color _backgroundColor;
        private Color _borderColor;
        private Color _disabledBorderColor;
        private Color _checkSignColor;

        #endregion Internal Vars

        #region Constructors

        public MetroRadioButton()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            base.Font = MetroFonts.SemiBold(10);
            _utl = new Utilites();
            _animator = new IntAnimate();
            _animator.Setting(100, 0, 255);
            _animator.Update = (alpha) => Invalidate();
            ApplyTheme();
        }

        #endregion Constructors

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
                    ForeColor = Color.Black;
                    BackgroundColor = Color.White;
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(205, 205, 205);
                    CheckSignColor = Color.FromArgb(65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    BorderColor = Color.FromArgb(155, 155, 155);
                    DisabledBorderColor = Color.FromArgb(85, 85, 85);
                    CheckSignColor = Color.FromArgb(65, 177, 225);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.RadioButtonDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "CheckColor":
                                    CheckSignColor = _utl.HexColor((string)varkey.Value);
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
            try
            {
                ForeColor = ForeColor;
                Invalidate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new(0, 0, 17, 16);
            int alpha = _animator.Value;

            using (SolidBrush backBrush = new(Enabled ? BackgroundColor : Color.FromArgb(238, 238, 238)))
            {
                using SolidBrush checkMarkBrush = new(Enabled ? Checked || _animator.Active ? Color.FromArgb(alpha, CheckSignColor) : BackgroundColor : Checked || _animator.Active ? Color.FromArgb(alpha, DisabledBorderColor) : Color.FromArgb(238, 238, 238));
                using Pen p = new(Enabled ? BorderColor : DisabledBorderColor);
                g.FillEllipse(backBrush, rect);
                if (Enabled)
                {
                    g.DrawEllipse(p, rect);
                    g.FillEllipse(checkMarkBrush, new Rectangle(3, 3, 11, 10));
                }
                else
                {
                    g.FillEllipse(checkMarkBrush, new Rectangle(3, 3, 11, 10));
                    g.DrawEllipse(p, rect);
                }

            }
            g.SmoothingMode = SmoothingMode.Default;
            using SolidBrush tb = new(ForeColor);
            using StringFormat sf = new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            g.DrawString(Text, Font, tb, new Rectangle(19, 2, Width, Height - 4), sf);
        }

        #endregion Draw Control

        #region Events

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = !Checked;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 17;
            Invalidate();
        }

        private void UpdateState()
        {
            if (!IsHandleCreated || !Checked)
            {
                return;
            }

            foreach (Control c in Parent.Controls)
            {
                if (!ReferenceEquals(c, this) && c is MetroRadioButton button && button.Group == Group)
                {
                    button.Checked = false;
                }
            }
            CheckedChanged?.Invoke(this);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == User32.WM_SETCURSOR)
            {
                User32.SetCursor(User32.LoadCursor(IntPtr.Zero, User32.IDC_HAND));
                m.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref m);
        }

        #endregion Events

        #region Properties

        [Category("Metro"), Description("Gets or sets a value indicating whether the control is checked.")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                CheckedChanged?.Invoke(this);
                _animator.Reverse(!value);
                UpdateState();
                CheckState = value ? Enum.Metro.CheckState.Checked : Enum.Metro.CheckState.Unchecked;
                Invalidate();
            }
        }

        [Browsable(false)]
        public Enum.Metro.CheckState CheckState { get; set; }

        [Category("Metro")]
        [DefaultValue(1)]
        public int Group
        {
            get => _group;
            set
            {
                _group = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the control forecolor.")]
        public override Color ForeColor { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the control backcolor.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the border color.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
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

        [Category("Metro"), Description("Gets or sets the color of the check symbol.")]
        public Color CheckSignColor
        {
            get => _checkSignColor;
            set
            {
                _checkSignColor = value;
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

        #region Disposing

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }

    #endregion
}