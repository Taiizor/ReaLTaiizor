using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ReaLTaiizor.AppLocker
{
    public partial class AppLocker : Form
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static Dictionary<string, string> Procs = new Dictionary<string, string>();
        private readonly Dictionary<string, string> LProcs = new Dictionary<string, string>();
        public static Dictionary<string, bool> PProcs = new Dictionary<string, bool>();
        public Dictionary<string, bool> MBProcs = new Dictionary<string, bool>();
        public static Dictionary<string, bool> BPProcs = new Dictionary<string, bool>();

        public AppLocker()
        {
            InitializeComponent();
        }

        private void RAL_Tick(object sender, EventArgs e)
        {
            RAL.Stop();
            foreach (Process Process in Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle) && Text != p.MainWindowTitle).ToList())
            {
                if (LProcs.ContainsKey(Process.ProcessName))
                {
                    if (BPProcs.ContainsKey(Process.ProcessName) && !BPProcs[Process.ProcessName] && PProcs.ContainsKey(Process.ProcessName) && PProcs[Process.ProcessName])
                    {
                        PProcs[Process.ProcessName] = false;
                        MBProcs[Process.ProcessName] = true;
                        AppPassword AP = new AppPassword(Process.ProcessName, Procs[Process.ProcessName], AppPassword.Type.G);
                        AP.Show();
                    }
                }
                else
                {
                    LProcs[Process.ProcessName] = Process.ProcessName;
                    panel1.Controls.Add(new RunApp(null, Process.ProcessName, Process.MainWindowTitle, Process.StartInfo.WorkingDirectory) { Dock = DockStyle.Top });
                }
            }

            RAL.Start();
        }
    }
}