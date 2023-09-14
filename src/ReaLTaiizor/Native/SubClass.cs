#region Imports

using System;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Native
{
    #region SubClassNative

    [SuppressUnmanagedCodeSecurity]
    internal class SubClass : NativeWindow
    {
        public delegate int SubClassWndProcEventHandler(ref Message m);
        public event SubClassWndProcEventHandler SubClassedWndProc;

        public SubClass(IntPtr Handle, bool _SubClass)
        {
            base.AssignHandle(Handle);
            SubClassed = _SubClass;
        }

        public bool SubClassed { get; set; } = false;

        protected override void WndProc(ref Message m)
        {
            if (SubClassed)
            {
                if (OnSubClassedWndProc(ref m) != 0)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public void CallDefaultWndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        #region HiWord Message Cracker

        public static int HiWord(int Number)
        {
            return (Number >> 16) & 0xffff;
        }

        #endregion

        #region LoWord Message Cracker

        public static int LoWord(int Number)
        {
            return Number & 0xffff;
        }

        #endregion

        #region MakeLong Message Cracker

        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        #endregion

        #region MakeLParam Message Cracker

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        #endregion

        private int OnSubClassedWndProc(ref Message m)
        {
            if (SubClassedWndProc != null)
            {
                return SubClassedWndProc(ref m);
            }

            return 0;
        }
    }

    #endregion
}