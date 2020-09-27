#region Imports

using System;
using System.Runtime.InteropServices;

#endregion

namespace ReaLTaiizor.Util
{
    #region NightUtil

    internal class NativeConstants
    {
        internal const int IDC_HAND = 0x7F89;
        internal const int WM_SETCURSOR = 0x0020;
    }

    internal static class NativeMethods
    {
        #region Cursor Functions

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetCursor(IntPtr hCursor);

        #endregion
    }

    #endregion
}