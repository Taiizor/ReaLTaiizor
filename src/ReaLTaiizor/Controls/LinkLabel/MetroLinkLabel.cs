#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
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
    #region MetroLinkLabel

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroLinkLabel), "Bitmaps.LinkLabel.bmp")]
    [Designer(typeof(MetroLinkDesigner))]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroLinkLabel : LinkLabel, iControl
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

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _metroStyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroLinkLabel()
        {
            SetStyle
            (
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Cursor = Cursors.Hand;
            Font = MetroFonts.Light(10);
            _utl = new Utilites();
            _style = Style.Dark;
            ApplyTheme();
            LinkBehavior = LinkBehavior.HoverUnderline;
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    ForeColor = Color.Black;
                    BackColor = Color.Transparent;
                    ActiveLinkColor = Color.FromArgb(85, 197, 245);
                    LinkColor = Color.FromArgb(65, 177, 225);
                    VisitedLinkColor = Color.FromArgb(45, 157, 205);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForeColor = Color.FromArgb(170, 170, 170);
                    BackColor = Color.Transparent;
                    ActiveLinkColor = Color.FromArgb(85, 197, 245);
                    LinkColor = Color.FromArgb(65, 177, 225);
                    VisitedLinkColor = Color.FromArgb(45, 157, 205);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.LinkLabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "LinkColor":
                                    LinkColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "ActiveLinkColor":
                                    ActiveLinkColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "VisitedLinkColor":
                                    VisitedLinkColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "LinkBehavior":
                                    switch ((string)varkey.Value)
                                    {
                                        case "HoverUnderline":
                                            LinkBehavior = LinkBehavior.HoverUnderline;
                                            break;
                                        case "AlwaysUnderline":
                                            LinkBehavior = LinkBehavior.AlwaysUnderline;
                                            break;
                                        case "NeverUnderline":
                                            LinkBehavior = LinkBehavior.NeverUnderline;
                                            break;
                                        case "SystemDefault":
                                            LinkBehavior = LinkBehavior.SystemDefault;
                                            break;
                                    }
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
        }

        #endregion ApplyTheme

        #region Events

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

        #endregion

        #region Properties

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; } = Color.Black;

        [Category("Metro"), Description("Gets or sets the form backcolor.")]
        public override Color BackColor { get; set; } = Color.Transparent;

        [Category("Metro"), Description("Gets or sets LinkColor used by the control.")]
        public new Color LinkColor { get; set; } = Color.FromArgb(65, 177, 225);

        [Category("Metro"), Description("Gets or sets ActiveLinkColor used by the control.")]
        public new Color ActiveLinkColor { get; set; } = Color.FromArgb(85, 197, 245);

        [Category("Metro"), Description("Gets or sets VisitedLinkColor used by the control.")]
        public new Color VisitedLinkColor { get; set; } = Color.FromArgb(45, 157, 205);

        [Category("Metro"), Description("Gets or sets LinkBehavior used by the control.")]
        public new LinkBehavior LinkBehavior { get; set; }

        [Category("Metro"), Description("Gets or sets DisabledLinkColor used by the control.")]
        public new Color DisabledLinkColor { get; set; } = Color.FromArgb(133, 133, 133);

        #endregion Properties
    }

    #endregion
}