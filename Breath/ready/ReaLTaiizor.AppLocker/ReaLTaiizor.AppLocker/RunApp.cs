using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    public partial class RunApp : UserControl
    {
        private readonly string ANAME, ADIR;

        public RunApp(Image I, string N, string T, string D)
        {
            InitializeComponent();
            ICO.Image = I ?? Properties.Resources.application_window_96px;
            NAME.Text = N + " - " + T;
            PTT.SetToolTip(NAME, T);
            ANAME = N;
            ADIR = D;
        }

        private void LOCK_Click(object sender, EventArgs e)
        {
            if (!AppLocker.PProcs.ContainsKey(ANAME))
            {
                AppPassword AP = new(ANAME, null, AppPassword.Type.S);
                AP.ShowDialog();
            }
        }

        private void DIRECTORY_Click(object sender, EventArgs e)
        {
            Process.Start(ADIR);
        }
    }
}