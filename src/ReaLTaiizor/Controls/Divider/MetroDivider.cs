﻿#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroDivider

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroDivider), "Bitmaps.Divider.bmp")]
    [Designer(typeof(MetroDividerDesigner))]
    [DefaultProperty("Orientation")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroDivider : Control, iControl
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

        private Utilites utl;

        #endregion Global Vars

        #region Internal Vars

        private Style _style;
        private MetroStyleManager _metroStyleManager;

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
            utl = new Utilites();
            ApplyTheme();
            Orientation = DividerStyle.Horizontal;
        }

        #endregion Constructors

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    Thickness = 1;
                    ForeColor = Color.Black;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
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
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.DividerDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "Orientation":
                                    if ((string)varkey.Value == "Horizontal")
                                        Orientation = DividerStyle.Horizontal;
                                    else if ((string)varkey.Value == "Vertical")
                                        Orientation = DividerStyle.Vertical;
                                    break;
                                case "Thickness":
                                    Thickness = ((int)varkey.Value);
                                    break;
                                case "ForeColor":
                                    ForeColor = utl.HexColor((string)varkey.Value);
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

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            using (var p = new Pen(ForeColor, Thickness))
            {
                if (Orientation == DividerStyle.Horizontal)
                    G.DrawLine(p, 0, Thickness, Width, Thickness);
                else
                    G.DrawLine(p, Thickness, 0, Thickness, Height);
            }
        }

        #endregion Draw Control

        #region Properties

        [Category("Metro"), Description("Gets or sets Orientation of the control.")]
        public DividerStyle Orientation { get; set; }

        [Category("Metro"), Description("Gets or sets the divider thickness.")]
        public int Thickness { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets the form forecolor.")]
        public override Color ForeColor { get; set; }

        #endregion Properties

        #region Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (Orientation == DividerStyle.Horizontal)
                Height = Thickness + 3;
            else
                Width = Thickness + 3;
        }

        #endregion Events
    }

    #endregion
}