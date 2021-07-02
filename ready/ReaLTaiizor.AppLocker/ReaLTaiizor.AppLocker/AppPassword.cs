using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    public partial class AppPassword : Form
    {
        private readonly string PN, PW;
        private readonly Type PT;

        public enum Type
        {
            G,
            S
        }

        public AppPassword(string PN, string PW, Type PT)
        {
            InitializeComponent();
            this.PN = PN;
            this.PW = PW;
            this.PT = PT;
            Text = PN + " - Locking Operations";
            nightForm1.Text = Text;
        }

        private void NightButton1_Click(object sender, EventArgs e)
        {
            if (PT == Type.G)
            {
                if (materialTextBox1.Text == PW)
                {
                    AppLocker.BPProcs[PN] = true;
                    Close();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(materialTextBox1.Text))
                {
                    AppLocker.Procs[PN] = materialTextBox1.Text;
                    AppLocker.PProcs[PN] = true;
                    AppLocker.BPProcs[PN] = false;

                    foreach (Process Process in Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList())
                    {
                        if (Process.ProcessName == PN)
                        {
                            Process.Kill();
                            break;
                        }
                    }
                }
                Close();
            }
        }

        private void NightButton2_Click(object sender, EventArgs e)
        {
            if (PT == Type.G)
            {
                if (!AppLocker.BPProcs[PN])
                {
                    AppLocker.PProcs[PN] = true;
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void AppPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PT == Type.G)
            {
                if (!AppLocker.BPProcs[PN])
                {
                    AppLocker.PProcs[PN] = true;
                }
            }
        }

        private void AppPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PT == Type.G)
            {
                if (!AppLocker.BPProcs[PN])
                {
                    AppLocker.PProcs[PN] = true;
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Process Process in Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle)).ToList())
            {
                string PN = Process.ProcessName;
                if (this.PN == PN)
                {
                    Process.Kill();
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (PT == Type.G)
            {
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }
    }
}
