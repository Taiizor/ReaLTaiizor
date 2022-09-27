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
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroToolTip

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroToolTip), "Bitmaps.ToolTip.bmp")]
    [Designer(typeof(MetroToolTipDesigner))]
    [DefaultEvent("Popup")]
    public class MetroToolTip : ToolTip, IMetroControl
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
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager StyleManager { get; set; }

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

        #endregion Internal Vars

        #region Constructors

        public MetroToolTip()
        {
            OwnerDraw = true;
            Draw += OnDraw;
            Popup += ToolTip_Popup;
            _mth = new Methods();
            _utl = new Utilites();
            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Rectangle rect = new(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            using SolidBrush bg = new(BackColor);
            using Pen stroke = new(BorderColor);
            using SolidBrush tb = new(ForeColor);
            g.FillRectangle(bg, rect);
            g.DrawString(e.ToolTipText, MetroFonts.Light(11), tb, rect, _mth.SetPosition());
            g.DrawRectangle(stroke, rect);
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
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(204, 204, 204);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackColor = Color.FromArgb(32, 32, 32);
                    BorderColor = Color.FromArgb(64, 64, 64);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ToolTipDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;

                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                default:
                                    return;
                            }
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        #endregion ApplyTheme

        #region Properties

        [Browsable(false)]
        public new bool ShowAlways { get; } = false;

        [Browsable(false)]
        public new bool OwnerDraw
        {
            get => base.OwnerDraw;
            set => base.OwnerDraw = true;
        }

        [Browsable(false)]
        public new bool IsBalloon { get; } = false;

        [Browsable(false)]
        public new Color BackColor { get; set; }

        [Category("Metro"), Description("Gets or sets the foreground color for the ToolTip.")]
        public new Color ForeColor { get; set; }

        [Category("Metro"), Description("Gets or sets a title for the ToolTip window.")]
        public new string ToolTipTitle { get; } = string.Empty;

        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon { get; } = ToolTipIcon.None;

        [Category("Metro"), Description("Gets or sets the border color for the ToolTip.")]
        public Color BorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
                     "Set it to false if you want the style of this control be independent. ")]
        public bool IsDerivedStyle { get; set; } = true;

        #endregion

        #region Methods

        public new void SetToolTip(Control control, string caption)
        {
            base.SetToolTip(control, caption);
            foreach (Control c in control.Controls)
            {
                SetToolTip(c, caption);
            }
        }

        #endregion

        #region Events 

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            Control control = e.AssociatedControl;
            if (control is IMetroControl iControl)
            {
                Style = iControl.Style;
                ThemeAuthor = iControl.ThemeAuthor;
                ThemeName = iControl.ThemeName;
                StyleManager = iControl.StyleManager;
            }
            else if (control is IMetroForm form)
            {
                Style = form.Style;
                ThemeAuthor = form.ThemeAuthor;
                ThemeName = form.ThemeName;
                StyleManager = form.StyleManager;
            }
            e.ToolTipSize = new(e.ToolTipSize.Width + 30, e.ToolTipSize.Height + 6);
        }

        #endregion
    }

    #endregion
}