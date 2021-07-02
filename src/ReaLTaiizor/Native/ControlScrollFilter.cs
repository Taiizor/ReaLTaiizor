#region Imports

using ReaLTaiizor.Enum.Crown;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Native
{
    #region ControlScrollFilterNative

    public class ControlScrollFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)WM.MOUSEWHEEL:
                case (int)WM.MOUSEHWHEEL:
                    System.IntPtr hControlUnderMouse = Native.WindowFromPoint(new Point((int)m.LParam));

                    if (hControlUnderMouse == m.HWnd)
                    {
                        return false;
                    }

                    Native.SendMessage(hControlUnderMouse, (uint)m.Msg, m.WParam, m.LParam);
                    return true;
            }

            return false;
        }
    }

    #endregion
}