#region Imports

using System;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockContentEventArgsDocking

    public class DockContentEventArgs : EventArgs
    {
        public CrownDockContent Content { get; private set; }

        public DockContentEventArgs(CrownDockContent content)
        {
            Content = content;
        }
    }

    #endregion
}