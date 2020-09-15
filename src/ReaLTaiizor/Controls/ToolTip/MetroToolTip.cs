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

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroToolTip

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroToolTip), "Bitmaps.ToolTip.bmp")]
    [Designer(typeof(MetroToolTipDesigner))]
    [DefaultEvent("Popup")]
    public class MetroToolTip : ToolTip, iControl
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
            }
        }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager MetroStyleManager
        {
            get => _metroStyleManager;
            set => _metroStyleManager = value;
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

        private MetroStyleManager _metroStyleManager;
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
            var G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            var rect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            using (var bg = new SolidBrush(BackColor))
            {
                using (var stroke = new Pen(BorderColor))
                {
                    using (var tb = new SolidBrush(ForeColor))
                    {
                        G.FillRectangle(bg, rect);
                        G.DrawString(e.ToolTipText, MetroFonts.Light(11), tb, rect, _mth.SetPosition());
                        G.DrawRectangle(stroke, rect);
                    }
                }
            }

        }

        #endregion

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.White;
                    BorderColor = Color.FromArgb(204, 204, 204);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(204, 204, 204);
                    BackColor = Color.FromArgb(32, 32, 32);
                    BorderColor = Color.FromArgb(64, 64, 64);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.ToolTipDictionary)
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

        #endregion

        #region Methods

        public new void SetToolTip(Control control, string caption)
        {
            //This Method is useful at runtime.
            base.SetToolTip(control, caption);
            foreach (Control c in control.Controls)
                SetToolTip(c, caption);
        }

        #endregion

        #region Events 

        private void ToolTip_Popup(object sender, PopupEventArgs e)
        {
            var control = e.AssociatedControl;
            if (control is iControl iControl)
            {
                Style = iControl.Style;
                ThemeAuthor = iControl.ThemeAuthor;
                ThemeName = iControl.ThemeName;
                MetroStyleManager = iControl.MetroStyleManager;
            }
            else if (control is iForm)
            {
                Style = ((iForm)control).Style;
                ThemeAuthor = ((iForm)control).ThemeAuthor;
                ThemeName = ((iForm)control).ThemeName;
                MetroStyleManager = ((iForm)control).MetroStyleManager;
            }
            e.ToolTipSize = new Size(e.ToolTipSize.Width + 30, e.ToolTipSize.Height + 6);
        }

        #endregion
    }

    #endregion
}