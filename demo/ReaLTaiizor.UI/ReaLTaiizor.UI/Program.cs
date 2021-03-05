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
#if NET5_0 || NET6_0
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
                CultureInfo cultureInfo = new(CultureInfo.CurrentCulture.TextInfo.CultureName);
                Application.SetCompatibleTextRenderingDefault(false);
                Application.CurrentCulture = cultureInfo;
                Application.Run(new Form19());
                Application.Run(new Form1());
                Application.Run(new Form2());
                Application.Run(new Form3());
                Application.Run(new Form4());
                Application.Run(new Form5());
                Application.Run(new Form6());
                Application.Run(new Form7());
                Application.Run(new Form8());
                Application.Run(new Form9());
                Application.Run(new Form10());
                Application.Run(new Form11());
                Application.Run(new Form12());
                Application.Run(new Form13());
                Application.Run(new Form14());
                Application.Run(new Form15());
                Application.Run(new Form16());
                Application.Run(new Form17());
                Application.Run(new Form18());
                Application.Run(new Form19());
                Application.Run(new Form20());
                Application.Run(new Form21());
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n" + Ex.StackTrace + "\n" + Ex.Source + "\n" + Ex.InnerException + "\n" + Ex.Data + "\n" + Ex.TargetSite);
            }
        }
    }
}