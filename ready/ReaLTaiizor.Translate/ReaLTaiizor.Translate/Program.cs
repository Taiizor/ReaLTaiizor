using System;
using System.Windows.Forms;

namespace ReaLTaiizor.Translate
{
    internal static class Program
    {
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
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.Run(new Translate());
        }
    }
}