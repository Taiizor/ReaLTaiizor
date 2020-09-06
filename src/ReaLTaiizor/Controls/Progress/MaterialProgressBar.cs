#region Imports

using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helpers.MaterialDrawHelper;

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
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 5, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var doneProgress = (int)(Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, 0, 0, doneProgress, Height);
            e.Graphics.FillRectangle(SkinManager.BackgroundFocusBrush, doneProgress, 0, Width - doneProgress, Height);
        }
    }

    #endregion
}