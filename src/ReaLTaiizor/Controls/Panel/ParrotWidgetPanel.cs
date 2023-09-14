#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotWidgetPanel

    public class ParrotWidgetPanel : System.Windows.Forms.Panel
    {
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (!ControlsAsWidgets)
            {
                foreach (object obj in base.Controls)
                {
                    Control control = (Control)obj;
                    control.MouseDown += WidgetDown;
                    control.MouseUp += WidgetUp;
                    control.MouseMove += WidgetMove;
                }
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Reat controls as widgets")]
        public bool ControlsAsWidgets { get; set; }

        private void WidgetDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
        }

        private void WidgetUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void WidgetMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                ((Control)sender).Location = new Point(e.X, e.Y);
            }
        }

        private bool isDragging;
    }

    #endregion
}