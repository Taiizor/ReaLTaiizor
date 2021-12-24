using ReaLTaiizor.Stopwatch.Properties;
using System;
using System.Windows.Forms;

namespace ReaLTaiizor.Stopwatch
{
    public partial class Area : UserControl
    {
        private int L, D, DD, H, HH, M, MM, S, SS, MS, MSMS = 0;

        private readonly Head HD = new("Laps", "Time", "Total") { Dock = DockStyle.Top };

        public Area()
        {
            InitializeComponent();
            panel1.Controls.Add(HD);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (MS < 99)
            {
                MS++;
            }
            else
            {
                MS = 0;
                S++;

                if (S == 60)
                {
                    S = 0;
                    M++;
                }

                if (M == 60)
                {
                    M = 0;
                    H++;
                }

                if (H == 24)
                {
                    H = 0;
                    D++;
                }
            }

            materialLabel1.Text = $"{string.Format("{0:00}", D)}:{string.Format("{0:00}", H)}:{string.Format("{0:00}", M)}:{string.Format("{0:00}", S)}.{string.Format("{0:00}", MS)}";
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (MSMS < 99)
            {
                MSMS++;
            }
            else
            {
                MSMS = 0;
                SS++;

                if (SS == 60)
                {
                    SS = 0;
                    MM++;
                }

                if (MM == 60)
                {
                    MM = 0;
                    HH++;
                }

                if (HH == 24)
                {
                    HH = 0;
                    DD++;
                }
            }
        }

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                timer2.Stop();

                materialButton1.Icon = Resources.Start;
                metroToolTip1.SetToolTip(materialButton1, "Start");

                materialButton2.Enabled = false;

                materialLabel1.UseAccent = false;

                materialLabel2.UseAccent = true;
                materialLabel3.UseAccent = true;
                materialLabel4.UseAccent = true;
                materialLabel5.UseAccent = true;
                materialLabel7.UseAccent = true;
            }
            else
            {
                timer1.Start();
                timer2.Start();

                materialButton1.Icon = Resources.Pause;
                metroToolTip1.SetToolTip(materialButton1, "Pause");

                materialButton2.Enabled = true;

                materialLabel1.UseAccent = true;

                materialLabel2.UseAccent = false;
                materialLabel3.UseAccent = false;
                materialLabel4.UseAccent = false;
                materialLabel5.UseAccent = false;
                materialLabel7.UseAccent = false;

                if (!materialButton3.Enabled)
                {
                    materialButton3.Enabled = true;
                }
            }

            Refresh();
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(new Flag(++L, $"{string.Format("{0:00}", DD)}:{string.Format("{0:00}", HH)}:{string.Format("{0:00}", MM)}:{string.Format("{0:00}", SS)}.{string.Format("{0:00}", MSMS)}", materialLabel1.Text) { Dock = DockStyle.Top });

            panel1.Controls.Remove(HD);
            panel1.Controls.Add(HD);

            DD = 0;
            HH = 0;
            MM = 0;
            SS = 0;
            MSMS = 0;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            materialButton1.Icon = Resources.Start;
            metroToolTip1.SetToolTip(materialButton1, "Start");

            materialButton2.Enabled = false;
            materialButton3.Enabled = false;

            panel1.Controls.Clear();
            panel1.Controls.Add(HD);

            L = 0;
            D = 0;
            DD = 0;
            H = 0;
            HH = 0;
            M = 0;
            MM = 0;
            S = 0;
            SS = 0;
            MS = 0;
            MSMS = 0;

            materialLabel1.UseAccent = false;

            materialLabel2.UseAccent = true;
            materialLabel3.UseAccent = true;
            materialLabel4.UseAccent = true;
            materialLabel5.UseAccent = true;
            materialLabel7.UseAccent = true;

            materialLabel1.Text = "00:00:00:00.00";

            Refresh();
        }
    }
}