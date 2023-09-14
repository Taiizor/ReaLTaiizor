using System;
using System.Threading;
using System.Windows.Forms;
using static ReaLTaiizor.Nerator.CS.Setting;
using static ReaLTaiizor.Nerator.CS.Window;

namespace ReaLTaiizor.Nerator.CS
{
    public class Engine
    {
        private static readonly Mutex MTX = new(true, "{ReaLTaiizor Nerator - New Generation Password Generator}");

        public Engine()
        {
            try
            {
                Application.EnableVisualStyles();
#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
                Application.SetCompatibleTextRenderingDefault(false);
                if (MTX.WaitOne(TimeSpan.Zero, true))
                {
                    MTX.ReleaseMutex();
                    _ = new Setting(ConfigFileName);
                    Application.Run(OpenWindowMode(WindowMode));
                }
                else
                {
                    MessageBox.Show("Already Open!", "Nerator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n" + Ex.StackTrace);
            }
        }
    }
}