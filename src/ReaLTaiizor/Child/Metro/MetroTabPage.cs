#region Imports

using System.Drawing;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;

#endregion

namespace ReaLTaiizor.Child.Metro
{
    #region MetroTabPageChild

    [Designer(typeof(MetroTabPageDesigner))]
    public class MetroTabPage : TabPage, iControl
    {
        #region Interfaces

        [Browsable(false)]
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

        [Browsable(false)]
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

        #region Internal Vars

        private Style _style;
        private StyleManager _styleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroTabPage()
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
            UpdateStyles();
            Font = MetroFonts.Light(10);
            ApplyTheme();
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    BaseColor = Color.White;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    BaseColor = Color.FromArgb(32, 32, 32);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
            }
        }

        public void UpdateProperties()
        {
            Invalidate();
        }

        #endregion ApplyTheme

        #region Properties

        [Browsable(false)]
        public new Color BackColor { get; set; } = Color.Transparent;

        // I dont' want to recreate the following properties for specific reason but for helping
        // user to find usage properties easily under Metro category in propertygrid.

        [Category("Metro")]
        public override string Text { get; set; }

        [Category("Metro")]
        public override Font Font { get; set; }

        [Category("Metro")]
        public new int ImageIndex { get; set; }

        [Category("Metro")]
        public new string ImageKey { get; set; }

        [Category("Metro")]
        public new string ToolTipText { get; set; }

        [Category("Metro")]
        [Bindable(false)]
        public Color BaseColor { get; set; }

        #endregion Properties

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (var bg = new SolidBrush(BaseColor))
                G.FillRectangle(bg, ClientRectangle);
        }

        #endregion
    }

    #endregion
}