#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AirSeparator

    public class AirSeparator : AirControl
    {

        public AirSeparator()
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