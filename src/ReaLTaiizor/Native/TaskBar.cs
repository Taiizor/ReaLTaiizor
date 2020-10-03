#region Imports

using System;
using System.Drawing;
using System.Security;
using System.Runtime.InteropServices;

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
            get { return bounds; }
            private set { bounds = value; }
        }

        private TaskBarPosition position = TaskBarPosition.Unknown;
        public TaskBarPosition Position
        {
            get { return position; }
            private set { position = value; }
        }

        public Point Location
        {
            get
            {
                return Bounds.Location;
            }
        }

        public Size Size
        {
            get
            {
                return Bounds.Size;
            }
        }

        private bool alwaysOnTop = false;
        public bool AlwaysOnTop
        {
            get { return alwaysOnTop; }
            private set { alwaysOnTop = value; }
        }

        private bool autoHide = false;
        public bool AutoHide
        {
            get { return autoHide; }
            private set { autoHide = value; }
        }

        [SecuritySafeCritical]
        public TaskBar()
        {
            IntPtr taskbarHandle = WinApi.FindWindow(ClassName, null);

            WinApi.APPBARDATA data = new WinApi.APPBARDATA();
            data.cbSize = (uint)Marshal.SizeOf(typeof(WinApi.APPBARDATA));
            data.hWnd = taskbarHandle;
            IntPtr result = WinApi.SHAppBarMessage(WinApi.ABM.GetTaskbarPos, ref data);
            if (result == IntPtr.Zero)
                throw new InvalidOperationException();

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