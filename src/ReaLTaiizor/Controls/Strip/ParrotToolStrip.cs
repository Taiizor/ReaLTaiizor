#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotCustomToolStrip

    public class ParrotToolStrip : ToolStrip
    {
        public ParrotToolStrip()
        {
            Dock = DockStyle.Top;
            base.Renderer = new StripeRemoval(borderColor);
            base.BackColor = Color.White;
            base.ForeColor = Color.Black;
            base.GripStyle = ToolStripGripStyle.Hidden;
        }

        private void RefreshUI()
        {
            base.Renderer = new StripeRemoval(borderColor);
        }

        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                RefreshUI();
            }
        }

        private Color borderColor = Color.DodgerBlue;
    }

    #endregion
}