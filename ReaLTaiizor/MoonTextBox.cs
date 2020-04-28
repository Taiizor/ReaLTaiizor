#region Imports

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor
{
    #region MoonTextBox

    public class MoonTextBox : TextBox
    {

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 15:
                    Invalidate();
                    base.WndProc(ref m);
                    this.CustomPaint();
                    break; // TODO: might not be correct. Was : Exit Select
                default:
                    base.WndProc(ref m);
                    break; // TODO: might not be correct. Was : Exit Select
            }
        }

        public MoonTextBox()
        {
            Font = new Font("Microsoft Sans Serif", 8);
            ForeColor = Color.Gray;
            BackColor = Color.FromArgb(235, 235, 235);
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(76, 20);
        }

        private void CustomPaint()
        {
            Pen p = Pens.LightGray;
            CreateGraphics().DrawLine(p, 0, 0, Width, 0);
            CreateGraphics().DrawLine(p, 0, Height - 1, Width, Height - 1);
            CreateGraphics().DrawLine(p, 0, 0, 0, Height - 1);
            CreateGraphics().DrawLine(p, Width - 1, 0, Width - 1, Height - 1);
        }
    }

    #endregion
}