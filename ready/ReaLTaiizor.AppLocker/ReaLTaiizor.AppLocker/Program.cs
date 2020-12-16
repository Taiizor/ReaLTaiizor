using System;
using System.Threading;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    internal static class Program
    {
        private static readonly Mutex MTX = new Mutex(true, "{ReaLTaiizor AppLocker - Application Locker}");

        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (MTX.WaitOne(TimeSpan.Zero, true))
            {
                MTX.ReleaseMutex();
                Application.Run(new AppLocker());
            }
            else
            {
                MessageBox.Show("Already Open!", "AppLocker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                Environment.Exit(1);
            }
        }
    }
}