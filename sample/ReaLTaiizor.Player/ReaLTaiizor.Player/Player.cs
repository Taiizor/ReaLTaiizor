using System.Windows.Forms;

namespace ReaLTaiizor.Player
{
    public partial class Player : Form
    {
        private readonly Controls.ParrotObjectEllipse Ellipse1 = new();
        private readonly Controls.ParrotObjectEllipse Ellipse2 = new();
        private readonly Controls.ParrotObjectEllipse Ellipse3 = new();

        public Player()
        {
            InitializeComponent();
            Ellipse1.EffectedControl = pictureBox7;
            Ellipse2.EffectedControl = pictureBox8;
            Ellipse3.EffectedControl = pictureBox9;
        }

        private void Timer1_Tick(object sender, System.EventArgs e)
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

        private void PictureBox3_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                pictureBox3.Image = Properties.Resources.pause_64px;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.play_64px;
            }
        }
    }
}