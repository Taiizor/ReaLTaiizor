#region Imports

using System.Drawing;
using ReaLTaiizor.Util;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Extension.Poison;

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
                    return StyleManager.Style;

                return PoisonStyle;
            }
            set { PoisonStyle = value; }
        }

        private ThemeStyle PoisonTheme = ThemeStyle.Light;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public ThemeStyle Theme
        {
            get
            {
                if (StyleManager != null)
                    return StyleManager.Theme;

                return PoisonTheme;
            }
            set { PoisonTheme = value; }
        }

        private PoisonStyleManager PoisonStyleManager = null;
        [Browsable(false)]
        public PoisonStyleManager StyleManager
        {
            get { return PoisonStyleManager; }
            set { PoisonStyleManager = value; }
        }

        #endregion

        #region Fields

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool ShowAlways
        {
            get { return base.ShowAlways; }
            set { base.ShowAlways = true; }
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public new bool OwnerDraw
        {
            get { return base.OwnerDraw; }
            set { base.OwnerDraw = true; }
        }

        [Browsable(false)]
        public new bool IsBalloon
        {
            get { return base.IsBalloon; }
            set { base.IsBalloon = false; }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false)]
        public new string ToolTipTitle
        {
            get { return base.ToolTipTitle; }
            set { base.ToolTipTitle = ""; }
        }

        [Browsable(false)]
        public new ToolTipIcon ToolTipIcon
        {
            get { return base.ToolTipIcon; }
            set { base.ToolTipIcon = ToolTipIcon.None; }
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
                    SetToolTip(c, caption);
            }
        }

        private void PoisonToolTip_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedWindow is IPoisonForm)
            {
                Style = ((IPoisonForm)e.AssociatedWindow).Style;
                Theme = ((IPoisonForm)e.AssociatedWindow).Theme;
                StyleManager = ((IPoisonForm)e.AssociatedWindow).StyleManager;
            }
            else if (e.AssociatedControl is IPoisonControl)
            {
                Style = ((IPoisonControl)e.AssociatedControl).Style;
                Theme = ((IPoisonControl)e.AssociatedControl).Theme;
                StyleManager = ((IPoisonControl)e.AssociatedControl).StyleManager;
            }

            e.ToolTipSize = new Size(e.ToolTipSize.Width + 24, e.ToolTipSize.Height + 9);
        }

        private void PoisonToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ThemeStyle displayTheme = (Theme == ThemeStyle.Light) ? ThemeStyle.Dark : ThemeStyle.Light;

            Color backColor = PoisonPaint.BackColor.Form(displayTheme);
            Color borderColor = PoisonPaint.BorderColor.Button.Normal(displayTheme);
            Color foreColor = PoisonPaint.ForeColor.Label.Normal(displayTheme);

            using (SolidBrush b = new SolidBrush(backColor))
                e.Graphics.FillRectangle(b, e.Bounds);
            using (Pen p = new Pen(borderColor))
                e.Graphics.DrawRectangle(p, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));

            Font f = PoisonFonts.Default(13f);
            TextRenderer.DrawText(e.Graphics, e.ToolTipText, f, e.Bounds, foreColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        #endregion
    }

    #endregion
}