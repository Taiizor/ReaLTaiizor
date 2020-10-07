#region Imports

using System;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockContentEventArgsDocking

    public class DockContentEventArgs : EventArgs
    {
        public DockContent Content { get; private set; }

        public DockContentEventArgs(DockContent content)
        {
            Content = content;
        }
    }

    #endregion
}