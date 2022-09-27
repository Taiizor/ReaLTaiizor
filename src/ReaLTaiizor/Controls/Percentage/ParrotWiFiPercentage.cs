#region Imports

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotWiFiPercentage

    public class ParrotWiFiPercentage : Timer
    {
        public ParrotWiFiPercentage()
        {
            Enabled = true;
            base.Interval = 3000;
            backgroundThread.DoWork += BackgroundThread_DoWork;
            backgroundThread.RunWorkerAsync();
        }

        private void BackgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundThread.IsBusy)
            {
                Process process = new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = "cmd.exe",
                        Arguments = "/C \"@echo off && for /f \"tokens=3 delims= \" %a in ('netsh wlan show interfaces ^| findstr /r \" ^....SSID\"') do echo %a && for /f \"tokens=3 delims= \" %a in ('netsh wlan show interfaces ^| findstr /r \" ^....Signal\"') do echo %a\""
                    }
                };
                process.Start();
                SSID = "Not connected";
                int percentage = 0;
                try
                {
                    string[] array = process.StandardOutput.ReadToEnd().Split(new char[]
                    {
                        ' '
                    });
                    SSID = array[0];
                    percentage = int.Parse(array[1].Remove(0, 2).Replace("%", ""));
                    process.WaitForExit();
                }
                catch
                {
                }
                Value = percentage;
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Returns the wifi percentage")]
        public int Value { get; private set; } = 100;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Returns the SSID")]
        public string SSID { get; private set; } = "Not connected";

        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);
            backgroundThread.RunWorkerAsync();
        }

        private readonly BackgroundWorker backgroundThread = new();
    }

    #endregion
}