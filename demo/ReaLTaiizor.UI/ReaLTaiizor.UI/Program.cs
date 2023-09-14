using ReaLTaiizor.UI.Helpers;
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
        private static void Main(string[] Arg)
        {
            try
            {
                Application.EnableVisualStyles();
#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
                CultureInfo cultureInfo = new(CultureInfo.CurrentCulture.TextInfo.CultureName);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.CurrentCulture = cultureInfo;
                Application.Run(FormHelper.Open(Arg));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace + "\n" + ex.Source + "\n" + ex.InnerException + "\n" + ex.Data + "\n" + ex.TargetSite);
            }
        }
    }
}