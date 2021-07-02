#region Imports

using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockPanelStateDocking

    public class DockPanelState
    {
        #region Property Region

        public List<DockRegionState> Regions { get; set; }

        #endregion

        #region Constructor Region

        public DockPanelState()
        {
            Regions = new List<DockRegionState>();
        }

        #endregion
    }

    #endregion
}