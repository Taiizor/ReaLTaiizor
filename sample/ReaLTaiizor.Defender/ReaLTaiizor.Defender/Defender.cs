using System;
using System.Drawing;
using ReaLTaiizor.Forms.Form;

namespace ReaLTaiizor.Defender
{
    public partial class Defender : Lost
    {
        public Defender()
        {
            InitializeComponent();
        }

        private void HopeSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            hopeRoundProgressBar2.IsError = !hopeSwitch1.Checked;
        }

        private void HopeSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            hopeRoundProgressBar1.IsError = !hopeSwitch2.Checked;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, pictureBox2.Location.Y);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, pictureBox3.Location.Y);
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, pictureBox4.Location.Y);
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, pictureBox5.Location.Y);
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, pictureBox6.Location.Y);
        }
    }
}