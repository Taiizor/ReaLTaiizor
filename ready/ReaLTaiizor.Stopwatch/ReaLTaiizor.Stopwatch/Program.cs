using System;
using System.Windows.Forms;

namespace ReaLTaiizor.Stopwatch
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanýn ana girdi noktasý.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Stopwatch());
        }
    }
}