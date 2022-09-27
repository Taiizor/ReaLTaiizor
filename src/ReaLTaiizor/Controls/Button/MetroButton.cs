#region Imports

using ReaLTaiizor.Design.Metro;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Interface.Metro;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroButton

    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(MetroButton), "Bitmaps.Button.bmp")]
    [Designer(typeof(MetroButtonDesigner))]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class MetroButton : Control, IMetroControl
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

        [Category("Metro"), Description("Gets or sets the The Author name associated with the theme.")]
        public string ThemeAuthor { get; set; }

        [Category("Metro"), Description("Gets or sets the The Theme name associated with the theme.")]
        public string ThemeName { get; set; }

        [Category("Metro"), Description("Gets or sets the Style Manager associated with the control.")]
        public MetroStyleManager StyleManager
        {
            get => _styleManager;
            set
            {
                _styleManager = value;
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
        private MetroStyleManager _styleManager;

        private bool _isDerivedStyle = true;
        private Color _normalColor;
        private Color _normalBorderColor;
        private Color _normalTextColor;
        private Color _hoverColor;
        private Color _hoverBorderColor;
        private Color _hoverTextColor;
        private Color _pressColor;
        private Color _pressBorderColor;
        private Color _pressTextColor;
        private Color _disabledBackColor;
        private Color _disabledForeColor;
        private Color _disabledBorderColor;

        #endregion Internal Vars

        #region Constructors

        public MetroButton()
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
            base.Font = MetroFonts.Light(10);
            _utl = new Utilites();
            _mth = new Methods();

            ApplyTheme();
        }

        #endregion Constructors

        #region Draw Control

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = new(0, 0, Width - 1, Height - 1);
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            switch (_state)
            {
                case MouseMode.Normal:

                    using (SolidBrush bg = new(NormalColor))
                    using (Pen p = new(NormalBorderColor))
                    using (SolidBrush tb = new(NormalTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
                case MouseMode.Hovered:
                    Cursor = Cursors.Hand;
                    using (SolidBrush bg = new(HoverColor))
                    using (Pen p = new(HoverBorderColor))
                    using (SolidBrush tb = new(HoverTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
                case MouseMode.Pushed:
                    using (SolidBrush bg = new(PressColor))
                    using (Pen p = new(PressBorderColor))
                    using (SolidBrush tb = new(PressTextColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
                case MouseMode.Disabled:
                    using (SolidBrush bg = new(DisabledBackColor))
                    using (Pen p = new(DisabledBorderColor))
                    using (SolidBrush tb = new(DisabledForeColor))
                    {
                        g.FillRectangle(bg, r);
                        g.DrawRectangle(p, r);
                        g.DrawString(Text, Font, tb, r, _mth.SetPosition());
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion Draw Control

        #region ApplyTheme

        /// <summary>
        /// Gets or sets the style provided by the user.
        /// </summary>
        /// <param name="style">The Style.</param>
        private void ApplyTheme(Style style = Style.Light)
        {
            if (!IsDerivedStyle)
            {
                return;
            }

            switch (style)
            {
                case Style.Light:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(95, 207, 255);
                    HoverBorderColor = Color.FromArgb(95, 207, 255);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(35, 147, 195);
                    PressBorderColor = Color.FromArgb(35, 147, 195);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledForeColor = Color.Gray;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroLight";
                    break;
                case Style.Dark:
                    NormalColor = Color.FromArgb(65, 177, 225);
                    NormalBorderColor = Color.FromArgb(65, 177, 225);
                    NormalTextColor = Color.White;
                    HoverColor = Color.FromArgb(95, 207, 255);
                    HoverBorderColor = Color.FromArgb(95, 207, 255);
                    HoverTextColor = Color.White;
                    PressColor = Color.FromArgb(35, 147, 195);
                    PressBorderColor = Color.FromArgb(35, 147, 195);
                    PressTextColor = Color.White;
                    DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
                    DisabledForeColor = Color.Gray;
                    ThemeAuthor = "Taiizor";
                    ThemeName = "MetroDark";
                    break;
                case Style.Custom:
                    if (StyleManager != null)
                    {
                        foreach (System.Collections.Generic.KeyValuePair<string, object> varkey in StyleManager.ButtonDictionary)
                        {
                            if (varkey.Key is null)
                            {
                                return;
                            }

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
                    }

                    Invalidate();
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
                _state = value ? MouseMode.Normal : MouseMode.Disabled;
                Invalidate();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button background color in normal mouse sate.")]
        public Color NormalColor
        {
            get => _normalColor;
            set
            {
                _normalColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button border color in normal mouse sate.")]
        public Color NormalBorderColor
        {
            get => _normalBorderColor;
            set
            {
                _normalBorderColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in normal mouse sate.")]
        public Color NormalTextColor
        {
            get => _normalTextColor;
            set
            {
                _normalTextColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button background color in hover mouse sate.")]
        public Color HoverColor
        {
            get => _hoverColor;
            set
            {
                _hoverColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button border color in hover mouse sate.")]
        public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set
            {
                _hoverBorderColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in hover mouse sate.")]
        public Color HoverTextColor
        {
            get => _hoverTextColor;
            set
            {
                _hoverTextColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button background color in pushed mouse sate.")]
        public Color PressColor
        {
            get => _pressColor;
            set
            {
                _pressColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button border color in pushed mouse sate.")]
        public Color PressBorderColor
        {
            get => _pressBorderColor;
            set
            {
                _pressBorderColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the button Text color in pushed mouse sate.")]
        public Color PressTextColor
        {
            get => _pressTextColor;
            set
            {
                _pressTextColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets backcolor used by the control while disabled.")]
        public Color DisabledBackColor
        {
            get => _disabledBackColor;
            set
            {
                _disabledBackColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the forecolor of the control whenever while disabled.")]
        public Color DisabledForeColor
        {
            get => _disabledForeColor;
            set
            {
                _disabledForeColor = value;
                Refresh();
            }
        }

        [Category("Metro")]
        [Description("Gets or sets the border color of the control while disabled.")]
        public Color DisabledBorderColor
        {
            get => _disabledBorderColor;
            set
            {
                _disabledBorderColor = value;
                Refresh();
            }
        }

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