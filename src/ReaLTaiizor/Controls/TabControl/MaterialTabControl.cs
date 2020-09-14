#region Imports

using System;
using ReaLTaiizor.Util;
using System.Windows.Forms;
using System.ComponentModel;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialTabControl

    public class MaterialTabControl : TabControl, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }

    #endregion
}