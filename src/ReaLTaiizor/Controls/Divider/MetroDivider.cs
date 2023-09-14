#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroDivider

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroDivider), "Bitmaps.Divider.bmp")]
    [Designer(typeof(MetroDividerDesigner))]
    [DefaultProperty("Orientation")]
    [ComVisible(true)]
    public class MetroDivider : Control, IMetroControl
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

        private bool _isDerivedStyle = true;
        private DividerStyle _orientation;
        private int _thickness;

        #endregion Internal Vars

        #region Constructors

        public MetroDivider()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            _utl = new Utilites();
            ApplyTheme();
            Orientation = DividerStyle.Horizontal;
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
                    Thickness = 1;
                    ForeColor = Color.Black;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    Thickness = 1;
                    ForeColor = Color.FromArgb(170, 170, 170);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.DividerDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Orientation":
                                    if ((string)varkey.Value == "Horizontal")
                                    {
                                        Orientation = DividerStyle.Horizontal;
                                    }
                                    else if ((string)varkey.Value == "Vertical")
                                    {
                                        Orientation = DividerStyle.Vertical;
                                    }

                                    break;
                                case "Thickness":
                                    Thickness = (int)varkey.Value;
                                    break;
                                case "ForeColor":
                                    ForeColor = _utl.HexColor((string)varkey.Value);
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

        #endregion ApplyTheme

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using Pen p = new(ForeColor, Thickness);
            if (Orientation == DividerStyle.Horizontal)
            {
                g.DrawLine(p, 0, Thickness, Width, Thickness);
            }
            else
            {
                g.DrawLine(p, Thickness, 0, Thickness, Height);
            }
        }

        #endregion Draw Control

        #region Properties

        [Category("Metro"), Description("Gets or sets Orientation of the control.")]
        public DividerStyle Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                Refresh();
            }
        }

        [Category("Metro"), Description("Gets or sets the divider thickness.")]
        public int Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                Refresh();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

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

        #region Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Orientation == DividerStyle.Horizontal)
            {
                Height = Thickness + 3;
            }
            else
            {
                Width = Thickness + 3;
            }
        }

        #endregion Events
    }

    #endregion
}