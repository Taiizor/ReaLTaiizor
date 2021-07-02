#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Child.Metro
{
    #region MetroTabPageChild

    [Designer(typeof(MetroTabPageDesigner))]
    public class MetroTabPage : TabPage, IMetroControl
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

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _styleManager;

        private bool _isDerivedStyle = true;

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
            base.Font = MetroFonts.Light(10);
            UpdateStyles();
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
                    BaseColor = Color.White;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
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

        [Category("Metro")]
        public override string Text { get; set; }

        [Category("Metro")]
        public new Font Font { get; set; }

        [Category("Metro")]
        public new int ImageIndex { get; set; }

        [Category("Metro")]
        public new string ImageKey { get; set; }

        [Category("Metro")]
        public new string ToolTipText { get; set; }

        [Category("Metro")]
        [Bindable(false)]
        public Color BaseColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the whether this control reflect to parent form style. \n " +
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

        #region DrawControl

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using SolidBrush bg = new(BaseColor);
            g.FillRectangle(bg, ClientRectangle);
        }

        #endregion
    }

    #endregion
}