using ReaLTaiizor.Forms;
using System;
using System.Drawing;

namespace ReaLTaiizor.Defender
{
    public partial class Defender : LostForm
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
            panel1.Location = new Point(0, 3);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 51);
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 147);
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 99);
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(0, 195);
        }
    }
}