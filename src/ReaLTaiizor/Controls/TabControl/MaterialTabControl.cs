#region Imports

using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Windows.Forms;
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
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public MaterialTabControl()
        {
            Multiline = true;
        }
    }

    #endregion
}