using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor.Payment
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            foreverComboBox1.SelectedIndex = 0;
            foreverComboBox2.SelectedIndex = 0;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(3, pictureBox1.Location.Y);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(3, pictureBox2.Location.Y);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(3, pictureBox3.Location.Y);
        }
    }
}