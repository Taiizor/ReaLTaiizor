using System;
using System.Globalization;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
                CultureInfo cultureInfo = new(CultureInfo.CurrentCulture.TextInfo.CultureName);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.CurrentCulture = cultureInfo;
                Application.Run(new Form17());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException + "\n" + ex.Data + "\n" + ex.TargetSite);
            }
        }
    }
}