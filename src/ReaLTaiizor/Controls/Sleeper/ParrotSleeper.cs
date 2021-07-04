#region Imports

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSleeper

    public class ParrotSleeper : Component
    {
        public void Sleep(int Milliseconds)
        {
            DateTime Time = DateTime.Now.AddMilliseconds(Milliseconds);

            while (DateTime.Now < Time)
            {
                Application.DoEvents();
            }
        }
    }

    #endregion
}