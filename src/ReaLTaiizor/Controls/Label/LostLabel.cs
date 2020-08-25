#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostLabel

    public class LostLabel : ControlLostBase
    {
        public LostLabel() : base()
        {
            ForeColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), pevent.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), 0, 0);
        }
    }

    #endregion
}