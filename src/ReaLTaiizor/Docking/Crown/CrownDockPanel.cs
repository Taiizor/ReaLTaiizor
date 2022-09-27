#region Imports

using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockPanelDocking

    public class CrownDockPanel : UserControl
    {
        #region Event Region

        public event EventHandler<DockContentEventArgs> ActiveContentChanged;
        public event EventHandler<DockContentEventArgs> ContentAdded;
        public event EventHandler<DockContentEventArgs> ContentRemoved;

        #endregion

        #region Field Region

        private readonly List<CrownDockContent> _contents;
        private CrownDockContent _activeContent;
        private bool _switchingContent = false;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockContent ActiveContent
        {
            get => _activeContent;
            set
            {
                // Don't let content visibility changes re-trigger event
                if (_switchingContent)
                {
                    return;
                }

                _switchingContent = true;

                _activeContent = value;

                ActiveGroup = _activeContent.DockGroup;
                ActiveRegion = ActiveGroup.DockRegion;

                foreach (CrownDockRegion region in Regions.Values)
                {
                    region.Redraw();
                }

                ActiveContentChanged?.Invoke(this, new DockContentEventArgs(_activeContent));

                _switchingContent = false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockRegion ActiveRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockGroup ActiveGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CrownDockContent ActiveDocument => Regions[DockArea.Document].ActiveDocument;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContentDragFilter DockContentDragFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockResizeFilter DockResizeFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CrownDockSplitter> Splitters { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseButtons MouseButtonState
        {
            get
            {
                MouseButtons buttonState = MouseButtons;
                return buttonState;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<DockArea, CrownDockRegion> Regions { get; }

        #endregion

        #region Constructor Region

        public CrownDockPanel()
        {
            Splitters = new List<CrownDockSplitter>();
            DockContentDragFilter = new DockContentDragFilter(this);
            DockResizeFilter = new DockResizeFilter(this);

            Regions = new Dictionary<DockArea, CrownDockRegion>();
            _contents = new List<CrownDockContent>();

            BackColor = ThemeProvider.Theme.Colors.GreyBackground;

            CreateRegions();
        }

        #endregion

        #region Method Region

        public void AddContent(CrownDockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        public void AddContent(CrownDockContent dockContent, CrownDockGroup dockGroup)
        {
            if (_contents.Contains(dockContent))
            {
                RemoveContent(dockContent);
            }

            dockContent.DockPanel = this;
            _contents.Add(dockContent);

            if (dockGroup != null)
            {
                dockContent.DockArea = dockGroup.DockArea;
            }

            if (dockContent.DockArea == DockArea.None)
            {
                dockContent.DockArea = dockContent.DefaultDockArea;
            }

            CrownDockRegion region = Regions[dockContent.DockArea];
            region.AddContent(dockContent, dockGroup);

            ContentAdded?.Invoke(this, new DockContentEventArgs(dockContent));

            dockContent.Select();
        }

        public void InsertContent(CrownDockContent dockContent, CrownDockGroup dockGroup, DockInsertType insertType)
        {
            if (_contents.Contains(dockContent))
            {
                RemoveContent(dockContent);
            }

            dockContent.DockPanel = this;
            _contents.Add(dockContent);

            dockContent.DockArea = dockGroup.DockArea;

            CrownDockRegion region = Regions[dockGroup.DockArea];
            region.InsertContent(dockContent, dockGroup, insertType);

            ContentAdded?.Invoke(this, new DockContentEventArgs(dockContent));

            dockContent.Select();
        }

        public void RemoveContent(CrownDockContent dockContent)
        {
            if (!_contents.Contains(dockContent))
            {
                return;
            }

            dockContent.DockPanel = null;
            _contents.Remove(dockContent);

            CrownDockRegion region = Regions[dockContent.DockArea];
            region.RemoveContent(dockContent);

            ContentRemoved?.Invoke(this, new DockContentEventArgs(dockContent));
        }

        public bool ContainsContent(CrownDockContent dockContent)
        {
            return _contents.Contains(dockContent);
        }

        public List<CrownDockContent> GetDocuments()
        {
            return Regions[DockArea.Document].GetContents();
        }

        private void CreateRegions()
        {
            CrownDockRegion documentRegion = new(this, DockArea.Document);
            Regions.Add(DockArea.Document, documentRegion);

            CrownDockRegion leftRegion = new(this, DockArea.Left);
            Regions.Add(DockArea.Left, leftRegion);

            CrownDockRegion rightRegion = new(this, DockArea.Right);
            Regions.Add(DockArea.Right, rightRegion);

            CrownDockRegion bottomRegion = new(this, DockArea.Bottom);
            Regions.Add(DockArea.Bottom, bottomRegion);

            // Add the regions in this order to force the bottom region to be positioned
            // between the left and right regions properly.
            Controls.Add(documentRegion);
            Controls.Add(bottomRegion);
            Controls.Add(leftRegion);
            Controls.Add(rightRegion);

            // Create tab index for intuitive tabbing order
            documentRegion.TabIndex = 0;
            rightRegion.TabIndex = 1;
            bottomRegion.TabIndex = 2;
            leftRegion.TabIndex = 3;
        }

        public void DragContent(CrownDockContent content)
        {
            DockContentDragFilter.StartDrag(content);
        }

        #endregion

        #region Serialization Region

        public DockPanelState GetDockPanelState()
        {
            DockPanelState state = new();

            state.Regions.Add(new DockRegionState(DockArea.Document));
            state.Regions.Add(new DockRegionState(DockArea.Left, Regions[DockArea.Left].Size));
            state.Regions.Add(new DockRegionState(DockArea.Right, Regions[DockArea.Right].Size));
            state.Regions.Add(new DockRegionState(DockArea.Bottom, Regions[DockArea.Bottom].Size));

            Dictionary<CrownDockGroup, DockGroupState> _groupStates = new();

            IOrderedEnumerable<CrownDockContent> orderedContent = _contents.OrderBy(c => c.Order);
            foreach (CrownDockContent content in orderedContent)
            {
                foreach (DockRegionState region in state.Regions)
                {
                    if (region.Area == content.DockArea)
                    {
                        DockGroupState groupState;

                        if (_groupStates.ContainsKey(content.DockGroup))
                        {
                            groupState = _groupStates[content.DockGroup];
                        }
                        else
                        {
                            groupState = new DockGroupState();
                            region.Groups.Add(groupState);
                            _groupStates.Add(content.DockGroup, groupState);
                        }

                        groupState.Contents.Add(content.SerializationKey);

                        groupState.VisibleContent = content.DockGroup.VisibleContent.SerializationKey;
                    }
                }
            }

            return state;
        }

        public void RestoreDockPanelState(DockPanelState state, Func<string, CrownDockContent> getContentBySerializationKey)
        {
            foreach (DockRegionState region in state.Regions)
            {
                switch (region.Area)
                {
                    case DockArea.Left:
                        Regions[DockArea.Left].Size = region.Size;
                        break;
                    case DockArea.Right:
                        Regions[DockArea.Right].Size = region.Size;
                        break;
                    case DockArea.Bottom:
                        Regions[DockArea.Bottom].Size = region.Size;
                        break;
                }

                foreach (DockGroupState group in region.Groups)
                {
                    CrownDockContent previousContent = null;
                    CrownDockContent visibleContent = null;

                    foreach (string contentKey in group.Contents)
                    {
                        CrownDockContent content = getContentBySerializationKey(contentKey);

                        if (content == null)
                        {
                            continue;
                        }

                        content.DockArea = region.Area;

                        if (previousContent == null)
                        {
                            AddContent(content);
                        }
                        else
                        {
                            AddContent(content, previousContent.DockGroup);
                        }

                        previousContent = content;

                        if (group.VisibleContent == contentKey)
                        {
                            visibleContent = content;
                        }
                    }

                    if (visibleContent != null)
                    {
                        visibleContent.Select();
                    }
                }
            }
        }

        #endregion
    }

    #endregion
}