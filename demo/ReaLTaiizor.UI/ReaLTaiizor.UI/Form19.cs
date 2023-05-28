using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Forms;
using System;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    public partial class Form19 : MetroForm
    {
        public Form19()
        {
            InitializeComponent();
        }

        private void MetroSwitch2_SwitchedChanged(object sender)
        {
            if (metroStyleManager1.Style == Style.Light)
            {
                metroStyleManager1.Style = Style.Dark;
            }
            else if (metroStyleManager1.Style == Style.Dark)
            {
                metroStyleManager1.Style = Style.Light;
            }
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo);
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        }

        private void MetroButton5_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void MetroButton6_Click(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private void MetroButton7_Click_1(object sender, EventArgs e)
        {
            MetroMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void MetroDefaultButton1_Click(object sender, EventArgs e)
        {
            metroStyleManager1.OpenTheme();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            metroStyleManager1.Style = Style.Light;
        }
    }
}
