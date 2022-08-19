using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Forms;
using System;

namespace ReaLTaiizor.Rufus
{
    public partial class Rufus : MetroForm
    {
        public Rufus()
        {
            InitializeComponent();
            metroComboBox1.SelectedIndex = 0;
            metroComboBox2.SelectedIndex = 1;
            metroComboBox3.SelectedIndex = 0;
            metroComboBox4.SelectedIndex = 0;
            metroTextBox1.Text = metroComboBox1.SelectedItem.ToString().Split(' ')[0];
            metroComboBox5.SelectedIndex = 0;
            metroComboBox6.SelectedIndex = 0;
        }

        private void MetroSwitch1_SwitchedChanged(object sender)
        {
            if (metroSwitch1.Switched)
            {
                metroStyleManager1.Style = Style.Dark;
            }
            else
            {
                metroStyleManager1.Style = Style.Light;
            }
        }

        private void MetroDefaultButton1_Click(object sender, EventArgs e)
        {
            metroProgressBar1.Value = 0;
            metroLabel11.Text = "READY";
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (metroProgressBar1.Value < metroProgressBar1.Maximum)
            {
                metroProgressBar1.Value += 1;
            }
            else
            {
                timer1.Stop();
                metroLabel11.Text = "FINISH";
            }
        }
    }
}