using ReaLTaiizor.Nerator.CS;
using System;
using System.Windows.Forms;
using static ReaLTaiizor.Nerator.CS.Setting;
using static ReaLTaiizor.Nerator.CS.Window;

namespace ReaLTaiizor.Nerator.UC.THEME
{
    public partial class LT : UserControl
    {
        public LT()
        {
            InitializeComponent();
        }

        private void LTPL_MouseEnter(object sender, EventArgs e)
        {
            LTPL.EdgeColor = System.Drawing.Color.DodgerBlue;
        }

        private void LTPL_MouseLeave(object sender, EventArgs e)
        {
            LTPL.EdgeColor = System.Drawing.SystemColors.Control;
        }

        private void LTPL_Click(object sender, EventArgs e)
        {
            if (WindowMode == WindowType.DARK)
            {
                WindowMode = WindowType.LIGHT;
                Application.Restart();
            }
            else
            {
                Status.Message = "Light theme already selected!";
            }
        }
    }
}