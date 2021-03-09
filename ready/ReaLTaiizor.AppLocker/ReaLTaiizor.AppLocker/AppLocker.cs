using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    public partial class AppLocker : Form
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public static Dictionary<string, string> Procs = new();
        private readonly Dictionary<string, string> LProcs = new();
        public static Dictionary<string, bool> PProcs = new();
        public Dictionary<string, bool> MBProcs = new();
        public static Dictionary<string, bool> BPProcs = new();

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
                        AppPassword AP = new(Process.ProcessName, Procs[Process.ProcessName], AppPassword.Type.G);
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