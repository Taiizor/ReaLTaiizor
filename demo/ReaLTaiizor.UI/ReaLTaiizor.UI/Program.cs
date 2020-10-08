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
                CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.TextInfo.CultureName);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.CurrentCulture = cultureInfo;
                Application.Run(new Form21());
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n" + Ex.StackTrace + "\n" + Ex.Source + "\n" + Ex.InnerException + "\n" + Ex.Data + "\n" + Ex.TargetSite);
            }
        }
    }
}