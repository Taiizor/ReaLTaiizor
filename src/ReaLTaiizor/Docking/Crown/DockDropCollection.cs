#region Imports

using ReaLTaiizor.Enum.Crown;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockDropCollectionDocking

    internal class DockDropCollection
    {
        #region Property Region

        internal DockDropArea DropArea { get; private set; }

        internal DockDropArea InsertBeforeArea { get; private set; }

        internal DockDropArea InsertAfterArea { get; private set; }

        #endregion

        #region Constructor Region

        internal DockDropCollection(CrownDockPanel dockPanel, CrownDockGroup group)
        {
            DropArea = new DockDropArea(dockPanel, group, DockInsertType.None);
            InsertBeforeArea = new DockDropArea(dockPanel, group, DockInsertType.Before);
            InsertAfterArea = new DockDropArea(dockPanel, group, DockInsertType.After);
        }

        #endregion
    }

    #endregion
}