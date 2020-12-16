using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    public partial class RunApp : UserControl
    {
        private readonly string ANAME, ADIR;

        public RunApp(Image I, string N, string T, string D)
        {
            InitializeComponent();
            ICO.Image = I == null ? Properties.Resources.application_window_96px : I;
            NAME.Text = N + " - " + T;
            ANAME = N;
            ADIR = D;
        }

        private void LOCK_Click(object sender, EventArgs e)
        {
            if (!AppLocker.PProcs.ContainsKey(ANAME))
            {
                AppPassword AP = new AppPassword(ANAME, null, AppPassword.Type.S);
                AP.ShowDialog();
            }
        }

        private void DIRECTORY_Click(object sender, EventArgs e)
        {
            Process.Start(ADIR);
        }
    }
}