#region Imports

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Native
{
    #region NativeNative

    internal sealed class Native
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }

    #endregion
}