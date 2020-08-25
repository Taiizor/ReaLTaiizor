#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostBorderPanel

    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class LostBorderPanel : LostPanel
    {
        private Color _bordercolor = ThemeLost.AccentPen.Color;
        public Color BorderColor
        {
            get { return _bordercolor; }
            set { _bordercolor = value; Invalidate(); }
        }

        public override void DrawShadow(Graphics g)
        {
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(_bordercolor), 0, 0, Width - 1, Height - 1);
        }
    }

    #endregion
}