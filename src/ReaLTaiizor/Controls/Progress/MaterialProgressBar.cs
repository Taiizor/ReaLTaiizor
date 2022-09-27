#region Imports

using ReaLTaiizor.Manager;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialProgressBar

    public class MaterialProgressBar : ProgressBar, MaterialControlI
    {
        public MaterialProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private bool useAccentColor;

        [Category("Material")]
        public bool UseAccentColor
        {
            get => useAccentColor;
            set
            {
                useAccentColor = value;
                Invalidate();
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int doneProgress = (int)(Width * ((double)Value / Maximum));

            e.Graphics.FillRectangle(Enabled ?
                UseAccentColor ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush :
                new SolidBrush(BlendColor(SkinManager.ColorScheme.PrimaryColor, SkinManager.SwitchOffDisabledThumbColor, 197)),
                0, 0, doneProgress, Height);

            e.Graphics.FillRectangle(SkinManager.BackgroundFocusBrush, doneProgress, 0, Width - doneProgress, Height);
        }
    }

    #endregion
}