#region Imports

using ReaLTaiizor.Enum.Crown;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockRegionStateDocking

    public class DockRegionState
    {
        #region Property Region

        public DockArea Area { get; set; }

        public Size Size { get; set; }

        public List<DockGroupState> Groups { get; set; }

        #endregion

        #region Constructor Region

        public DockRegionState()
        {
            Groups = new List<DockGroupState>();
        }

        public DockRegionState(DockArea area) : this()
        {
            Area = area;
        }

        public DockRegionState(DockArea area, Size size) : this(area)
        {
            Size = size;
        }

        #endregion
    }

    #endregion
}