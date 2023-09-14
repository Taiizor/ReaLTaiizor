#region Imports

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

#endregion

namespace ReaLTaiizor.Native
{
    #region TaskBarNative

    #region General

    public enum TaskBarPosition
    {
        Unknown = -1,
        Left,
        Top,
        Right,
        Bottom,
    }

    #endregion

    internal class TaskBar
    {
        private const string ClassName = "Shell_TrayWnd";

        private Rectangle bounds = Rectangle.Empty;
        public Rectangle Bounds
        {
            get => bounds;
            private set => bounds = value;
        }
        public TaskBarPosition Position { get; private set; } = TaskBarPosition.Unknown;

        public Point Location => Bounds.Location;

        public Size Size => Bounds.Size;

        public bool AlwaysOnTop { get; private set; } = false;
        public bool AutoHide { get; private set; } = false;

        [SecuritySafeCritical]
        public TaskBar()
        {
            IntPtr taskbarHandle = WinApi.FindWindow(ClassName, null);

            WinApi.APPBARDATA data = new()
            {
                cbSize = (uint)Marshal.SizeOf(typeof(WinApi.APPBARDATA)),
                hWnd = taskbarHandle
            };
            IntPtr result = WinApi.SHAppBarMessage(WinApi.ABM.GetTaskbarPos, ref data);
            if (result == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }

            Position = (TaskBarPosition)data.uEdge;
            Bounds = Rectangle.FromLTRB(data.rc.Left, data.rc.Top, data.rc.Right, data.rc.Bottom);

            data.cbSize = (uint)Marshal.SizeOf(typeof(WinApi.APPBARDATA));
            result = WinApi.SHAppBarMessage(WinApi.ABM.GetState, ref data);
            int state = result.ToInt32();
            AlwaysOnTop = (state & WinApi.AlwaysOnTop) == WinApi.AlwaysOnTop;
            AutoHide = (state & WinApi.Autohide) == WinApi.Autohide;
        }

    }

    #endregion
}