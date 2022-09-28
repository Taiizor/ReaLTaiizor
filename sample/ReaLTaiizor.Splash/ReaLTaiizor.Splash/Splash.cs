using System;
using System.Threading;
using System.Windows.Forms;

namespace ReaLTaiizor.Splash
{
    public partial class Splash : Form
    {
        private bool State = true;

        public Splash()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Process));
        }

        private void Process(object Test)
        {
            try
            {
                Random Random = new();

                if (State)
                {
                    poisonProgressSpinner1.Value++;

                    if (poisonProgressSpinner1.Value == 100)
                    {
                        State = false;

                        poisonProgressSpinner1.Style = (Enum.Poison.ColorStyle)Random.Next(3, 15);
                    }
                }
                else
                {
                    poisonProgressSpinner1.Value--;

                    if (poisonProgressSpinner1.Value == 0)
                    {
                        State = true;

                        poisonProgressSpinner1.Style = (Enum.Poison.ColorStyle)Random.Next(3, 15);
                    }
                }

                poisonLabel1.Style = poisonProgressSpinner1.Style;
                poisonLabel1.Text = $"Wait A Moments.. {poisonProgressSpinner1.Value}%";

                if (poisonProgressSpinner1.Value % 2 == 0)
                {
                    parrotPictureBox1.FilterAlpha = poisonProgressSpinner1.Value * 2;
                }
            }
            catch
            {
                //
            }
        }
    }
}