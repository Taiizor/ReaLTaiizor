#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;

#endregion

namespace ReaLTaiizor.Controls
{
    #region LostSeparator

    public class LostSeparator : ControlLostBase
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Horizontal { get; set; }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), pevent.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Horizontal)
                e.Graphics.DrawLine(new Pen(ForeColor), 0, Height / 2, Width, Height / 2);
            else
                e.Graphics.DrawLine(new Pen(ForeColor), Width / 2, 0, Width / 2, Height);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            //return;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //return;
        }
    }

    #endregion
}