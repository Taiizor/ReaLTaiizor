using System;
using System.Globalization;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.TextInfo.CultureName);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = cultureInfo;
            Application.Run(new Form17());
        }
    }
}