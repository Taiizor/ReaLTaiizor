using System;
using System.Globalization;
using System.Windows.Forms;

namespace ReaLTaiizor_CR
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
            CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.TextInfo.CultureName);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = cultureInfo;
            Application.Run(new Poison());
        }
    }
}