#region Imports

using System;
using System.Linq;
using ReaLTaiizor.Native;
using ReaLTaiizor.Colors;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Crown;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region DockPanelDocking

    public class DockPanel : UserControl
    {
        #region Event Region

        public event EventHandler<DockContentEventArgs> ActiveContentChanged;
        public event EventHandler<DockContentEventArgs> ContentAdded;
        public event EventHandler<DockContentEventArgs> ContentRemoved;

        #endregion

        #region Field Region

        private readonly List<DockContent> _contents;
        private readonly Dictionary<DockArea, DockRegion> _regions;

        private DockContent _activeContent;
        private bool _switchingContent = false;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContent ActiveContent
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

                foreach (DockRegion region in _regions.Values)
                {
                    region.Redraw();
                }

                if (ActiveContentChanged != null)
                {
                    ActiveContentChanged(this, new DockContentEventArgs(_activeContent));
                }

                _switchingContent = false;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockRegion ActiveRegion { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockGroup ActiveGroup { get; internal set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContent ActiveDocument => _regions[DockArea.Document].ActiveDocument;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockContentDragFilter DockContentDragFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockResizeFilter DockResizeFilter { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<DockSplitter> Splitters { get; private set; }

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
        public Dictionary<DockArea, DockRegion> Regions => _regions;

        #endregion

        #region Constructor Region

        public DockPanel()
        {
            Splitters = new List<DockSplitter>();
            DockContentDragFilter = new DockContentDragFilter(this);
            DockResizeFilter = new DockResizeFilter(this);

            _regions = new Dictionary<DockArea, DockRegion>();
            _contents = new List<DockContent>();

            BackColor = CrownColors.GreyBackground;

            CreateRegions();
        }

        #endregion

        #region Method Region

        public void AddContent(DockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        public void AddContent(DockContent dockContent, DockGroup dockGroup)
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

            DockRegion region = _regions[dockContent.DockArea];
            region.AddContent(dockContent, dockGroup);

            if (ContentAdded != null)
            {
                ContentAdded(this, new DockContentEventArgs(dockContent));
            }

            dockContent.Select();
        }

        public void InsertContent(DockContent dockContent, DockGroup dockGroup, DockInsertType insertType)
        {
            if (_contents.Contains(dockContent))
            {
                RemoveContent(dockContent);
            }

            dockContent.DockPanel = this;
            _contents.Add(dockContent);

            dockContent.DockArea = dockGroup.DockArea;

            DockRegion region = _regions[dockGroup.DockArea];
            region.InsertContent(dockContent, dockGroup, insertType);

            if (ContentAdded != null)
            {
                ContentAdded(this, new DockContentEventArgs(dockContent));
            }

            dockContent.Select();
        }

        public void RemoveContent(DockContent dockContent)
        {
            if (!_contents.Contains(dockContent))
            {
                return;
            }

            dockContent.DockPanel = null;
            _contents.Remove(dockContent);

            DockRegion region = _regions[dockContent.DockArea];
            region.RemoveContent(dockContent);

            if (ContentRemoved != null)
            {
                ContentRemoved(this, new DockContentEventArgs(dockContent));
            }
        }

        public bool ContainsContent(DockContent dockContent)
        {
            return _contents.Contains(dockContent);
        }

        public List<DockContent> GetDocuments()
        {
            return _regions[DockArea.Document].GetContents();
        }

        private void CreateRegions()
        {
            DockRegion documentRegion = new DockRegion(this, DockArea.Document);
            _regions.Add(DockArea.Document, documentRegion);

            DockRegion leftRegion = new DockRegion(this, DockArea.Left);
            _regions.Add(DockArea.Left, leftRegion);

            DockRegion rightRegion = new DockRegion(this, DockArea.Right);
            _regions.Add(DockArea.Right, rightRegion);

            DockRegion bottomRegion = new DockRegion(this, DockArea.Bottom);
            _regions.Add(DockArea.Bottom, bottomRegion);

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

        public void DragContent(DockContent content)
        {
            DockContentDragFilter.StartDrag(content);
        }

        #endregion

        #region Serialization Region

        public DockPanelState GetDockPanelState()
        {
            DockPanelState state = new DockPanelState();

            state.Regions.Add(new DockRegionState(DockArea.Document));
            state.Regions.Add(new DockRegionState(DockArea.Left, _regions[DockArea.Left].Size));
            state.Regions.Add(new DockRegionState(DockArea.Right, _regions[DockArea.Right].Size));
            state.Regions.Add(new DockRegionState(DockArea.Bottom, _regions[DockArea.Bottom].Size));

            Dictionary<DockGroup, DockGroupState> _groupStates = new Dictionary<DockGroup, DockGroupState>();

            IOrderedEnumerable<DockContent> orderedContent = _contents.OrderBy(c => c.Order);
            foreach (DockContent content in orderedContent)
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

        public void RestoreDockPanelState(DockPanelState state, Func<string, DockContent> getContentBySerializationKey)
        {
            foreach (DockRegionState region in state.Regions)
            {
                switch (region.Area)
                {
                    case DockArea.Left:
                        _regions[DockArea.Left].Size = region.Size;
                        break;
                    case DockArea.Right:
                        _regions[DockArea.Right].Size = region.Size;
                        break;
                    case DockArea.Bottom:
                        _regions[DockArea.Bottom].Size = region.Size;
                        break;
                }

                foreach (DockGroupState group in region.Groups)
                {
                    DockContent previousContent = null;
                    DockContent visibleContent = null;

                    foreach (string contentKey in group.Contents)
                    {
                        DockContent content = getContentBySerializationKey(contentKey);

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