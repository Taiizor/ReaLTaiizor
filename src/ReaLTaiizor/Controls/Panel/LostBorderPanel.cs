#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostBorderPanel

    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class LostBorderPanel : LostPanel
    {
        public Color BorderColor
        {
            get;
            set { field = value; Invalidate(); }
        } = ThemeLost.AccentPen.Color;

        public override void DrawShadow(Graphics g)
        {
            return;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new(BorderColor), 0, 0, Width - 1, Height - 1);
        }
    }

    #endregion
}