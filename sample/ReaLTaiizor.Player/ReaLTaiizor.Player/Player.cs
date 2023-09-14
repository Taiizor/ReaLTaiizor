using ReaLTaiizor.Controls;
using System;
using System.Windows.Forms;

namespace ReaLTaiizor.Player
{
    public partial class Player : Form
    {
        private readonly ParrotObjectEllipse Ellipse1 = new();
        private readonly ParrotObjectEllipse Ellipse2 = new();
        private readonly ParrotObjectEllipse Ellipse3 = new();

        public Player()
        {
            InitializeComponent();
            Ellipse1.EffectedControl = pictureBox7;
            Ellipse2.EffectedControl = pictureBox8;
            Ellipse3.EffectedControl = pictureBox9;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (hopeTrackBar1.Value < hopeTrackBar1.MaxValue)
            {
                hopeTrackBar1.Value += 1;
            }
            else
            {
                hopeTrackBar1.Value = hopeTrackBar1.MinValue;
            }
        }

        private void MetroEllipse2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                metroEllipse2.Image = Properties.Resources.pause_64px;
            }
            else
            {
                metroEllipse2.Image = Properties.Resources.play_64px;
            }
        }
    }
}