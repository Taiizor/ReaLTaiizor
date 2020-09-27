#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
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
    #region MetroTile

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroTile), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroTileDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroTile : Control, iControl
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

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager MetroStyleManager
        {
            get => _metroStyleManager;
            set
            {
                _metroStyleManager = value;
                Invalidate();
            }
        }

        #endregion Interfaces

        #region Global Vars

        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private MouseMode _state;
        private Style _style;
        private MetroStyleManager _metroStyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroTile()
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Font = MetroFonts.Light(10);
            _utl = new Utilites();
            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var r = new Rectangle(1, 1, Width - 2, Height - 2);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            StringFormat sf;

            switch (TileAlign)
            {
                case TileAlign.BottmLeft:
                    sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
                    break;
                case TileAlign.BottomRight:
                    sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
                    break;
                case TileAlign.Topleft:
                    sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.TopRight:
                    sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.TopCenter:
                    sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
                    break;
                case TileAlign.BottomCenter:
                    sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (_state)
            {
                case MouseMode.Normal:
                    using (var bg = new SolidBrush(NormalColor))
                    {
                        using (var p = new Pen(NormalBorderColor, 2))
                        {
                            using (var tb = new SolidBrush(NormalTextColor))
                            {
                                if (BackgroundImage != null)
                                    G.DrawImage(BackgroundImage, r);
                                else
                                {
                                    G.FillRectangle(bg, r);
                                    G.DrawRectangle(p, r);
                                }
                                G.DrawString(Text, Font, tb, r, sf);
                            }
                        }
                    }
                    break;
                case MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    using (var bg = new SolidBrush(HoverColor))
                    {
                        using (var p = new Pen(HoverBorderColor, 2))
                        {
                            using (var tb = new SolidBrush(HoverTextColor))
                            {
                                if (BackgroundImage != null)
                                    G.DrawImage(BackgroundImage, r);
                                else
                                    G.FillRectangle(bg, r);
                                G.DrawString(Text, Font, tb, r, sf);
                                G.DrawRectangle(p, r);
                            }
                        }
                    }
                    break;
                case MouseMode.Pushed:
                    using (var bg = new SolidBrush(PressColor))
                    {
                        using (var p = new Pen(PressBorderColor, 2))
                        {
                            using (var tb = new SolidBrush(PressTextColor))
                            {
                                if (BackgroundImage != null)
                                    G.DrawImage(BackgroundImage, r);
                                else
                                    G.FillRectangle(bg, r);
                                G.DrawString(Text, Font, tb, r, sf);
                                G.DrawRectangle(p, r);

                            }
                        }
                    }
                    break;
                case MouseMode.Disabled:
                    using (var bg = new SolidBrush(DisabledBackColor))
                    {
                        using (var p = new Pen(DisabledBorderColor))
                        {
                            using (var tb = new SolidBrush(DisabledForeColor))
                            {
                                G.FillRectangle(bg, r);
                                G.DrawString(Text, Font, tb, r, sf);
                                G.DrawRectangle(p, r);
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion Draw Control

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(65, 177, 225);
                    HoverBorderColor = Color.FromArgb(230, 230, 230);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(65, 177, 225);
                    PressBorderColor = Color.FromArgb(65, 177, 225);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    break;
                case Style.Dark:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(65, 177, 225);
                    HoverBorderColor = Color.FromArgb(102, 102, 102);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(65, 177, 225);
                    PressBorderColor = Color.FromArgb(51, 51, 51);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;
                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.TileDictionary)
                        {
                            if ((varkey.Key == null) || varkey.Key == null)
                                return;

                            if (varkey.Key == "NormalColor")
                                NormalColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "NormalBorderColor")
                                NormalBorderColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "NormalTextColor")
                                NormalTextColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "HoverColor")
                                HoverColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "HoverBorderColor")
                                HoverBorderColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "HoverTextColor")
                                HoverTextColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "PressColor")
                                PressColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "PressBorderColor")
                                PressBorderColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "PressTextColor")
                                PressTextColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "DisabledBackColor")
                                DisabledBackColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "DisabledBorderColor")
                                DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                            else if (varkey.Key == "DisabledForeColor")
                                DisabledForeColor = _utl.HexColor((string)varkey.Value);
                        }
                    Refresh();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        #endregion Theme Changing

        #region Properties

        [Browsable(false)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (value == false)
                    _state = MouseMode.Disabled;
                Invalidate();
            }
        }

        [Category("Metro"), Description("Gets or sets the BackgroundImage associated with the control.")]
        public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

        [Category("Metro"), Description("Gets or sets the TileAlign associated with the control.")]
        public TileAlign TileAlign { get; set; } = TileAlign.BottmLeft;

        [Category("Metro")]
        [Description("Gets or sets the control background color in normal mouse sate.")]
        public Color NormalColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control border color in normal mouse sate.")]
        public Color NormalBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control Text color in normal mouse sate.")]
        public Color NormalTextColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control background color in hover mouse sate.")]
        public Color HoverColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control border color in hover mouse sate.")]
        public Color HoverBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control Text color in hover mouse sate.")]
        public Color HoverTextColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control background color in pushed mouse sate.")]
        public Color PressColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control border color in pushed mouse sate.")]
        public Color PressBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the control Text color in pushed mouse sate.")]
        public Color PressTextColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets backcolor used by the control while disabled.")]
        public Color DisabledBackColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the border color of the control while disabled.")]
        public Color DisabledBorderColor { get; set; }

        #endregion

        #region Events

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = MouseMode.Pushed;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseMode.Hovered;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseMode.Normal;
            Invalidate();
        }

        #endregion Events
    }

    #endregion
}