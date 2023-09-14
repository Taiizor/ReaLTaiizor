#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Extension.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonToolTip

    [ToolboxBitmap(typeof(ToolTip))]
    public class PoisonToolTip : ToolTip, IPoisonComponent
    {
        #region Interface

        private ColorStyle PoisonStyle = ColorStyle.Blue;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ColorStyle Style
        {
            get
            {
                if (StyleManager != null)
                {
                    return StyleManager.Style;
                }

                return PoisonStyle;
            }
            set => PoisonStyle = value;
        }

        private ThemeStyle PoisonTheme = ThemeStyle.Light;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ThemeStyle Theme
        {
            get
            {
                if (StyleManager != null)
                {
                    return StyleManager.Theme;
                }

                return PoisonTheme;
            }
            set => PoisonTheme = value;
        }

        [Browsable(false)]
        public PoisonStyleManager StyleManager { get; set; } = null;

        #endregion

        #region Fields

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool ShowAlways
        {
            get => base.ShowAlways;
            set => base.ShowAlways = true;
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get => base.OwnerDraw;
            set => base.OwnerDraw = true;
        }

        [Browsable(false)]
        public new bool IsBalloon
        {
            get => base.IsBalloon;
            set => base.IsBalloon = false;
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [Browsable(false)]
        public new Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        [Browsable(false)]
        public new string ToolTipTitle
        {
            get => base.ToolTipTitle;
            set => base.ToolTipTitle = "";
        }

        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon
        {
            get => base.ToolTipIcon;
            set => base.ToolTipIcon = ToolTipIcon.None;
        }

        #endregion

        #region Constructor

        public PoisonToolTip()
        {
            OwnerDraw = true;
            ShowAlways = true;

            Draw += new DrawToolTipEventHandler(PoisonToolTip_Draw);
            Popup += new PopupEventHandler(PoisonToolTip_Popup);
        }

        #endregion

        #region Management Methods

        public new void SetToolTip(Control control, string caption)
        {
            base.SetToolTip(control, caption);

            if (control is IPoisonControl)
            {
                foreach (Control c in control.Controls)
                {
                    SetToolTip(c, caption);
                }
            }
        }

        private void PoisonToolTip_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedWindow is IPoisonForm form)
            {
                Style = form.Style;
                Theme = form.Theme;
                StyleManager = form.StyleManager;
            }
            else if (e.AssociatedControl is IPoisonControl control)
            {
                Style = control.Style;
                Theme = control.Theme;
                StyleManager = control.StyleManager;
            }

            e.ToolTipSize = new(e.ToolTipSize.Width + 24, e.ToolTipSize.Height + 9);
        }

        private void PoisonToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ThemeStyle displayTheme = (Theme == ThemeStyle.Light) ? ThemeStyle.Dark : ThemeStyle.Light;

            Color backColor = PoisonPaint.BackColor.Form(displayTheme);
            Color borderColor = PoisonPaint.BorderColor.Button.Normal(displayTheme);
            Color foreColor = PoisonPaint.ForeColor.Label.Normal(displayTheme);

            using (SolidBrush b = new(backColor))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }

            using (Pen p = new(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
            }

            Font f = PoisonFonts.Default(13f);
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, f, e.Bounds, foreColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        #endregion
    }

    #endregion
}