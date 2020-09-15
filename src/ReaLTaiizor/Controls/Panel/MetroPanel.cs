#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Extension.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroPanel

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroPanel), "Bitmaps.Panel.bmp")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroPanel : System.Windows.Forms.Panel, iControl
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

        public MetroPanel()
        {
            SetStyle
            (
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            BorderStyle = BorderStyle.None;
            UpdateStyles();
            _utl = new Utilites();
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    BorderColor = Color.FromArgb(150, 150, 150);
                    BackgroundColor = Color.White;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    BorderColor = Color.FromArgb(110, 110, 110);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.LabelDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "BorderColor":
                                    BorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
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

        #endregion Theme Changing

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var r = new Rectangle(BorderThickness, BorderThickness, Width - (BorderThickness * 2 + 1), Height - ((BorderThickness * 2) + 1));

            using (var bg = new SolidBrush(BackgroundColor))
            {
                using (var p = new Pen(BorderColor, BorderThickness))
                {
                    G.FillRectangle(bg, r);
                    G.DrawRectangle(p, r);
                }
            }

        }

        #endregion

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor => Color.Transparent;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle = BorderStyle.None;

        [Category("Metro"), Description("Gets or sets the border thickness the control.")]
        public int BorderThickness { get; set; } = 1;

        [Category("Metro"), Description("Gets or sets bordercolor used by the control.")]
        public Color BorderColor { get; set; }

        [Category("Metro"), Description("Gets or sets backcolor used by the control.")]
        [DisplayName("BackColor")]
        public Color BackgroundColor { get; set; }

        #endregion
    }

    #endregion
}