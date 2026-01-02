#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Separator

    public class Separator : Control
    {
        #region Properties

        public Color LineColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        #endregion

        public Separator()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            Size = new(120, 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new(LineColor), 0, 5, Width, 5);
        }
    }

    #endregion
}