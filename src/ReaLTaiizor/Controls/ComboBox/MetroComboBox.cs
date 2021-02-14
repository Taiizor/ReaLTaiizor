#region Imports

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
    #region MetroComboBox

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroComboBox), "Bitmaps.ComboBox.bmp")]
    [DefaultEvent("SelectedIndexChanged")]
    [DefaultProperty("Items")]
    [ComVisible(true)]
    public class MetroComboBox : ComboBox, IMetroControl
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
                Refresh();
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

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;
        private int _startIndex;

        private bool _isDerivedStyle = true;
        private Color _backgroundColor;
        private Color _borderColor;
        private Color _arrowColor;
        private Color _selectedItemForeColor;
        private Color _selectedItemBackColor;
        private Color _disabledBackColor;
        private Color _disabledForeColor;
        private Color _disabledBorderColor;

        #endregion Internal Vars

        #region Constructors

        public MetroComboBox()
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            base.Font = MetroFonts.Regular(11);
            base.BackColor = Color.Transparent;
            base.AllowDrop = true;
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = 20;
            _startIndex = 0;
            CausesValidation = false;
            DropDownStyle = ComboBoxStyle.DropDownList;
            _mth = new Methods();
            _utl = new Utilites();

            ApplyTheme();
        }

        #endregion Constructors

        #region Properties

        [Category("Metro")]
        [Description("Gets or sets the index specifying the currently selected item.")]
        private int StartIndex
        {
            get => _startIndex;
            set
            {
                _startIndex = value;
                try
                {
                    SelectedIndex = value;
                }
                catch
                {
                    //
                }
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the form backcolor.")]
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

        [Category("Metro")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        public Color ArrowColor
        {
            get => _arrowColor;
            set
            {
                _arrowColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        public Color SelectedItemForeColor
        {
            get => _selectedItemForeColor;
            set
            {
                _selectedItemForeColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        public Color SelectedItemBackColor
        {
            get => _selectedItemBackColor;
            set
            {
                _selectedItemBackColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
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

        #region Draw Control

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (e.Index == -1)
            {
                return;
            }

            bool itemState = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            using SolidBrush bg = new(itemState ? SelectedItemBackColor : BackgroundColor);
            using SolidBrush tc = new(itemState ? SelectedItemForeColor : ForeColor);
            using Font f = new(Font.Name, 9);
            g.FillRectangle(bg, e.Bounds);
            g.DrawString(GetItemText(Items[e.Index]), f, tc, e.Bounds, _mth.SetPosition(StringAlignment.Near));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new(0, 0, Width - 1, Height - 1);
            char downArrow = '▼';
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using SolidBrush bg = new(Enabled ? BackgroundColor : DisabledBackColor);
            using Pen p = new(Enabled ? BorderColor : DisabledBorderColor);
            using SolidBrush s = new(Enabled ? ArrowColor : DisabledForeColor);
            using SolidBrush tb = new(Enabled ? ForeColor : DisabledForeColor);
            using Font f = MetroFonts.SemiBold(8);
            g.FillRectangle(bg, rect);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(downArrow.ToString(), f, s, new Point(Width - 22, 8));
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.DrawString(Text, f, tb, new Rectangle(7, 0, Width - 1, Height - 1), _mth.SetPosition(StringAlignment.Near));
            g.DrawRectangle(p, rect);
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
                    BackgroundColor = Color.FromArgb(238, 238, 238);
                    BorderColor = Color.FromArgb(150, 150, 150);
                    ArrowColor = Color.FromArgb(150, 150, 150);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackgroundColor = Color.FromArgb(34, 34, 34);
                    BorderColor = Color.FromArgb(110, 110, 110);
                    ArrowColor = Color.FromArgb(110, 110, 110);
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemForeColor = Color.White;
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
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ComboBoxDictionary)
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
                                case "ArrowColor":
                                    ArrowColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SelectedItemForeColor":
                                    SelectedItemForeColor = _utl.HexColor((string)varkey.Value);
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
        }

        #endregion Theme Changing

    }

    #endregion
}