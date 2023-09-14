#region Imports

using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroContextMenuStrip

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroContextMenuStrip), "Bitmaps.ContextMenu.bmp")]
    [DefaultEvent("Opening")]
    [ComVisible(true)]
    public class MetroContextMenuStrip : ContextMenuStrip, IMetroControl
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
        private ToolStripItemClickedEventArgs _clickedEventArgs;

        private bool _isDerivedStyle = true;

        #endregion Internal Vars

        #region Constructors

        public MetroContextMenuStrip()
        {
            _utl = new Utilites();
            ApplyTheme();
            Renderer = new MetroToolStripRender();
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
                    ForegroundColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.White;
                    ArrowColor = Color.Gray;
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    SeparatorColor = Color.LightGray;
                    DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    UpdateProperties();
                    break;
                case Style.Dark:
                    ForegroundColor = Color.FromArgb(170, 170, 170);
                    BackgroundColor = Color.FromArgb(30, 30, 30);
                    ArrowColor = Color.Gray;
                    SelectedItemBackColor = Color.FromArgb(65, 177, 225);
                    SelectedItemColor = Color.White;
                    SeparatorColor = Color.Gray;
                    DisabledForeColor = Color.Silver;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    UpdateProperties();
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ContextMenuDictionary)
                        {
                            switch (varkey.Key)
                            {
                                case "ForeColor":
                                    ForegroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "BackColor":
                                    BackgroundColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "ArrowColor":
                                    ArrowColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SeparatorColor":
                                    SeparatorColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SelectedItemColor":
                                    SelectedItemColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "SelectedItemBackColor":
                                    SelectedItemBackColor = _utl.HexColor((string)varkey.Value);
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

        #region Properties

        [Category("Metro"), Description("Gets or sets forecolor used by the control.")]
        [DisplayName("ForeColor")]
        public static Color ForegroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets backcolor used by the control.")]
        [DisplayName("BackColor")]
        public static Color BackgroundColor { get; set; }

        [Category("Metro"), Description("Gets or sets separator color used by the control.")]
        public static Color SeparatorColor { get; set; }

        [Category("Metro"), Description("Gets or sets arrowcolor used by the control.")]
        public static Color ArrowColor { get; set; }

        [Category("Metro"), Description("Gets or sets selecteditem color used by the control.")]
        public static Color SelectedItemColor { get; set; }

        [Category("Metro"), Description("Gets or sets selecteditem backcolor used by the control.")]
        public static Color SelectedItemBackColor { get; set; }

        [Category("Metro"), Description("Gets or sets disabled forecolor used by the control.")]
        public static Color DisabledForeColor { get; set; }

        public static new Font Font => MetroFonts.UIRegular(10);

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

        #region Events

        public event ClickedEventHandler Clicked;
        public delegate void ClickedEventHandler(object sender);

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is null or ToolStripSeparator)
            {
                return;
            }

            if (ReferenceEquals(e, _clickedEventArgs))
            {
                OnItemClicked(e);
            }
            else
            {
                _clickedEventArgs = e;
                Clicked?.Invoke(this);
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cursor = Cursors.Hand;
            Invalidate();
        }

        #endregion

        #region Child

        private sealed class MetroToolStripRender : ToolStripProfessionalRenderer
        {
            #region Drawing Text

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Rectangle textRect = new(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);
                using SolidBrush b = new(e.Item.Enabled ? e.Item.Selected ? SelectedItemColor : ForegroundColor : DisabledForeColor);
                e.Graphics.DrawString(e.Text, Font, b, textRect);
            }

            #endregion Drawing Text

            #region Drawing Backgrounds

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                base.OnRenderToolStripBackground(e);
                e.Graphics.Clear(BackgroundColor);
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                e.Graphics.InterpolationMode = InterpolationMode.High;
                e.Graphics.Clear(BackgroundColor);
                Rectangle r = new(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);
                using SolidBrush b = new(e.Item.Selected && e.Item.Enabled ? SelectedItemBackColor : BackgroundColor);
                e.Graphics.FillRectangle(b, r);
            }

            #endregion Drawing Backgrounds

            #region Set Image Margin

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                //MyBase.OnRenderImageMargin(e)
                //I Make above line comment which makes users to be able to add images to ToolStrips
            }

            #endregion Set Image Margin

            #region Drawing Separators & Borders

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                using Pen p = new(SeparatorColor);
                e.Graphics.DrawLine(p, new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
            }

            #endregion Drawing Separators & Borders

            #region Drawing DropDown Arrows

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                int arrowX = e.ArrowRectangle.X + (e.ArrowRectangle.Width / 2);
                int arrowY = e.ArrowRectangle.Y + (e.ArrowRectangle.Height / 2);
                Point[] arrowPoints = new[]
                {
                    new Point(arrowX - 5, arrowY - 5),
                    new Point(arrowX, arrowY),
                    new Point(arrowX - 5, arrowY + 5)
                };

                using SolidBrush arrowBrush = new(e.Item.Enabled ? ArrowColor : DisabledForeColor);
                e.Graphics.FillPolygon(arrowBrush, arrowPoints);
            }

            #endregion Drawing DropDown Arrows
        }

        #endregion
    }

    #endregion
}