#region Imports

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotBatteryPercentage

    public class ParrotBatteryPercentage : Timer
    {
        public ParrotBatteryPercentage()
        {
            Enabled = true;
            base.Interval = 3000;
            backgroundThread.DoWork += BackgroundThread_DoWork;
            backgroundThread.RunWorkerAsync();
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Returns the battery percentage")]
        public int Value => Percentage;

        private void BackgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            string text = SystemInformation.PowerStatus.BatteryLifePercent.ToString("#.#########", CultureInfo.InvariantCulture);
            Percentage = int.Parse(text.Substring(text.IndexOf(".") + 1));
            if (Percentage == 1)
            {
                Percentage = 100;
            }
        }

        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);
            backgroundThread.RunWorkerAsync();
        }

        private readonly BackgroundWorker backgroundThread = new();

        private int Percentage;
    }

    #endregion
}