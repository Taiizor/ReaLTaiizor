using System;
using System.Threading;
using System.Windows.Forms;

namespace ReaLTaiizor.AppLocker
{
    internal static class Program
    {
        private static readonly Mutex MTX = new(true, "{ReaLTaiizor AppLocker - Application Locker}");

        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.SetCompatibleTextRenderingDefault(false);
            if (MTX.WaitOne(TimeSpan.Zero, true))
            {
                MTX.ReleaseMutex();
                Application.Run(new AppLocker());
            }
            else
            {
                MessageBox.Show("Already Open!", "AppLocker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}