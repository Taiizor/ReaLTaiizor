#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Manager;
using System.Drawing.Text;
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
    #region MetroDefaultButton

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroDefaultButton), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroDefaultButtonDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class MetroDefaultButton : Control, iControl
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

        private readonly Methods _mth;
        private readonly Utilites _utl;

        #endregion Global Vars

        #region Internal Vars

        private MouseMode _state;
        private Style _style;
        private MetroStyleManager _metroStyleManager;

        #endregion Internal Vars

        #region Constructors

        public MetroDefaultButton()
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                    true
            );
            UpdateStyles();
            Font = MetroFonts.Light(10);
            _utl = new Utilites();
            _mth = new Methods();
            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            var G = e.Graphics;
            var r = new Rectangle(0, 0, Width - 1, Height - 1);
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (_state)
            {
                case MouseMode.Normal:
                    using (var bg = new SolidBrush(NormalColor))
                    using (var p = new Pen(NormalBorderColor))
                    using (var tb = new SolidBrush(NormalTextColor))
                    {
                        G.FillRectangle(bg, r);
                        G.DrawRectangle(p, r);
                        G.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), _mth.SetPosition());
                    }
                    break;
                case MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    using (var bg = new SolidBrush(HoverColor))
                    using (var p = new Pen(HoverBorderColor))
                    using (var tb = new SolidBrush(HoverTextColor))
                    {
                        G.FillRectangle(bg, r);
                        G.DrawRectangle(p, r);
                        G.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), _mth.SetPosition());
                    }
                    break;

                case MouseMode.Pushed:
                    using (var bg = new SolidBrush(PressColor))
                    using (var p = new Pen(PressBorderColor))
                    using (var tb = new SolidBrush(PressTextColor))
                    {
                        G.FillRectangle(bg, r);
                        G.DrawRectangle(p, r);
                        G.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), _mth.SetPosition());
                    }
                    break;
                case MouseMode.Disabled:
                    using (var bg = new SolidBrush(DisabledBackColor))
                    using (var p = new Pen(DisabledBorderColor))
                    using (var tb = new SolidBrush(DisabledForeColor))
                    {
                        G.FillRectangle(bg, r);
                        G.DrawRectangle(p, r);
                        G.DrawString(Text, Font, tb, new Rectangle(0, 0, Width, Height), _mth.SetPosition());
                    }
                    break;
            }
        }

        #endregion Draw Control

        #region ApplyTheme

        private void ApplyTheme(Style style = Style.Light)
        {
            switch (style)
            {
                case Style.Light:
                    NormalColor = Color.FromArgb(238, 238, 238);
                    NormalBorderColor = Color.FromArgb(204, 204, 204);
                    NormalTextColor = Color.Black;
                    HoverColor = Color.FromArgb(102, 102, 102);
                    HoverBorderColor = Color.FromArgb(102, 102, 102);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(51, 51, 51);
                    PressBorderColor = Color.FromArgb(51, 51, 51);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(204, 204, 204);
                    DisabledBorderColor = Color.FromArgb(155, 155, 155);
                    DisabledForeColor = Color.FromArgb(136, 136, 136);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLite";
                    break;
                case Style.Dark:
                    NormalColor = Color.FromArgb(32, 32, 32);
                    NormalBorderColor = Color.FromArgb(64, 64, 64);
                    NormalTextColor = Color.FromArgb(204, 204, 204);
                    HoverColor = Color.FromArgb(170, 170, 170);
                    HoverBorderColor = Color.FromArgb(170, 170, 170);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(240, 240, 240);
                    PressBorderColor = Color.FromArgb(240, 240, 240);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(80, 80, 80);
                    DisabledBorderColor = Color.FromArgb(109, 109, 109);
                    DisabledForeColor = Color.FromArgb(109, 109, 109);
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;

                case Style.Custom:
                    if (MetroStyleManager != null)
                        foreach (var varkey in MetroStyleManager.DefaultButtonDictionary)
                        {
                            if (varkey.Key == null)
                                return;

                            switch (varkey.Key)
                            {
                                case "NormalColor":
                                    NormalColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "NormalBorderColor":
                                    NormalBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "NormalTextColor":
                                    NormalTextColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "HoverColor":
                                    HoverColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "HoverBorderColor":
                                    HoverBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "HoverTextColor":
                                    HoverTextColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "PressColor":
                                    PressColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "PressBorderColor":
                                    PressBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "PressTextColor":
                                    PressTextColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBackColor":
                                    DisabledBackColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledBorderColor":
                                    DisabledBorderColor = _utl.HexColor((string)varkey.Value);
                                    break;
                                case "DisabledForeColor":
                                    DisabledForeColor = _utl.HexColor((string)varkey.Value);
                                    break;
                            }
                        }
                    Refresh();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(style), style, null);
            }
        }

        #endregion Theme Changing

        #region Properties

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor => Color.Transparent;

        [Category("Metro")]
        public new bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                if (!value)
                    _state = MouseMode.Disabled;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button background color in normal mouse sate.")]
        public Color NormalColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button border color in normal mouse sate.")]
        public Color NormalBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in normal mouse sate.")]
        public Color NormalTextColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button background color in hover mouse sate.")]
        public Color HoverColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button border color in hover mouse sate.")]
        public Color HoverBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in hover mouse sate.")]
        public Color HoverTextColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button background color in pushed mouse sate.")]
        public Color PressColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button border color in pushed mouse sate.")]
        public Color PressBorderColor { get; set; }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in pushed mouse sate.")]
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