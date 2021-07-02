#region Imports

using ReaLTaiizor.Enum.Crown;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockDropAreaDocking

    internal class DockDropArea
    {
        #region Property Region

        internal CrownDockPanel DockPanel { get; private set; }

        internal Rectangle DropArea { get; private set; }

        internal Rectangle HighlightArea { get; private set; }

        internal CrownDockRegion DockRegion { get; private set; }

        internal CrownDockGroup DockGroup { get; private set; }

        internal DockInsertType InsertType { get; private set; }

        #endregion

        #region Constructor Region

        internal DockDropArea(CrownDockPanel dockPanel, CrownDockRegion region)
        {
            DockPanel = dockPanel;
            DockRegion = region;
            InsertType = DockInsertType.None;

            BuildAreas();
        }

        internal DockDropArea(CrownDockPanel dockPanel, CrownDockGroup group, DockInsertType insertType)
        {
            DockPanel = dockPanel;
            DockGroup = group;
            InsertType = insertType;

            BuildAreas();
        }

        #endregion

        #region Method Region

        internal void BuildAreas()
        {
            if (DockRegion != null)
            {
                BuildRegionAreas();
            }
            else if (DockGroup != null)
            {
                BuildGroupAreas();
            }
        }

        private void BuildRegionAreas()
        {
            switch (DockRegion.DockArea)
            {
                case DockArea.Left:

                    Rectangle leftRect = new()
                    {
                        X = DockPanel.PointToScreen(Point.Empty).X,
                        Y = DockPanel.PointToScreen(Point.Empty).Y,
                        Width = 50,
                        Height = DockPanel.Height
                    };

                    DropArea = leftRect;
                    HighlightArea = leftRect;

                    break;

                case DockArea.Right:

                    Rectangle rightRect = new()
                    {
                        X = DockPanel.PointToScreen(Point.Empty).X + DockPanel.Width - 50,
                        Y = DockPanel.PointToScreen(Point.Empty).Y,
                        Width = 50,
                        Height = DockPanel.Height
                    };

                    DropArea = rightRect;
                    HighlightArea = rightRect;

                    break;

                case DockArea.Bottom:

                    int x = DockPanel.PointToScreen(Point.Empty).X;
                    int width = DockPanel.Width;

                    if (DockPanel.Regions[DockArea.Left].Visible)
                    {
                        x += DockPanel.Regions[DockArea.Left].Width;
                        width -= DockPanel.Regions[DockArea.Left].Width;
                    }

                    if (DockPanel.Regions[DockArea.Right].Visible)
                    {
                        width -= DockPanel.Regions[DockArea.Right].Width;
                    }

                    Rectangle bottomRect = new()
                    {
                        X = x,
                        Y = DockPanel.PointToScreen(Point.Empty).Y + DockPanel.Height - 50,
                        Width = width,
                        Height = 50
                    };

                    DropArea = bottomRect;
                    HighlightArea = bottomRect;

                    break;
            }
        }

        private void BuildGroupAreas()
        {
            switch (InsertType)
            {
                case DockInsertType.None:
                    Rectangle dropRect = new()
                    {
                        X = DockGroup.PointToScreen(Point.Empty).X,
                        Y = DockGroup.PointToScreen(Point.Empty).Y,
                        Width = DockGroup.Width,
                        Height = DockGroup.Height
                    };

                    DropArea = dropRect;
                    HighlightArea = dropRect;

                    break;

                case DockInsertType.Before:
                    int beforeDropWidth = DockGroup.Width;
                    int beforeDropHeight = DockGroup.Height;

                    switch (DockGroup.DockArea)
                    {
                        case DockArea.Left:
                        case DockArea.Right:
                            beforeDropHeight = DockGroup.Height / 4;
                            break;

                        case DockArea.Bottom:
                            beforeDropWidth = DockGroup.Width / 4;
                            break;
                    }

                    Rectangle beforeDropRect = new()
                    {
                        X = DockGroup.PointToScreen(Point.Empty).X,
                        Y = DockGroup.PointToScreen(Point.Empty).Y,
                        Width = beforeDropWidth,
                        Height = beforeDropHeight
                    };

                    DropArea = beforeDropRect;
                    HighlightArea = beforeDropRect;

                    break;

                case DockInsertType.After:
                    int afterDropX = DockGroup.PointToScreen(Point.Empty).X;
                    int afterDropY = DockGroup.PointToScreen(Point.Empty).Y;
                    int afterDropWidth = DockGroup.Width;
                    int afterDropHeight = DockGroup.Height;

                    switch (DockGroup.DockArea)
                    {
                        case DockArea.Left:
                        case DockArea.Right:
                            afterDropHeight = DockGroup.Height / 4;
                            afterDropY = DockGroup.PointToScreen(Point.Empty).Y + DockGroup.Height - afterDropHeight;
                            break;

                        case DockArea.Bottom:
                            afterDropWidth = DockGroup.Width / 4;
                            afterDropX = DockGroup.PointToScreen(Point.Empty).X + DockGroup.Width - afterDropWidth;
                            break;
                    }

                    Rectangle afterDropRect = new()
                    {
                        X = afterDropX,
                        Y = afterDropY,
                        Width = afterDropWidth,
                        Height = afterDropHeight
                    };

                    DropArea = afterDropRect;
                    HighlightArea = afterDropRect;

                    break;
            }
        }

        #endregion
    }

    #endregion
}