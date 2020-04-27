#region Imports

using System.Drawing;

#endregion

namespace ReaLTaiizor
{
    #region ChromeSeperator

    public class ChromeSeparator : ChromeControl
    {

        public ChromeSeparator()
        {
            LockHeight = 1;
            BackColor = Color.FromArgb(238, 238, 238);
        }


        protected override void ColorHook()
        {
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
        }
    }

    #endregion
}