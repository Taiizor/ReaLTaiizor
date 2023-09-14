using System;
using System.Windows.Forms;

namespace ReaLTaiizor.GenshinImpact
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }

        private void LostButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LostButton2_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.Launch2;
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.Launch1;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex < materialTabControl1.TabCount - 1)
            {
                materialTabControl1.SelectedIndex++;
            }
            else
            {
                materialTabControl1.SelectedIndex = 0;
            }
        }
    }
}