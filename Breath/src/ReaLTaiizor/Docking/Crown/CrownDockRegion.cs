#region Imports

using ReaLTaiizor.Enum.Crown;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockRegionDocking

    [ToolboxItem(false)]
    public class CrownDockRegion : Panel
    {
        #region Field Region

        private readonly List<CrownDockGroup> _groups;

        private Form _parentForm;
        private CrownDockSplitter _splitter;

        #endregion

        #region Property Region

        public CrownDockPanel DockPanel { get; private set; }

        public DockArea DockArea { get; private set; }

        public CrownDockContent ActiveDocument
        {
            get
            {
                if (DockArea != DockArea.Document || _groups.Count == 0)
                {
                    return null;
                }

                return _groups[0].VisibleContent;
            }
        }

        public List<CrownDockGroup> Groups => _groups.ToList();

        #endregion

        #region Constructor Region

        public CrownDockRegion(CrownDockPanel dockPanel, DockArea dockArea)
        {
            _groups = new List<CrownDockGroup>();

            DockPanel = dockPanel;
            DockArea = dockArea;

            BuildProperties();
        }

        #endregion

        #region Method Region

        internal void AddContent(CrownDockContent dockContent)
        {
            AddContent(dockContent, null);
        }

        internal void AddContent(CrownDockContent dockContent, CrownDockGroup dockGroup)
        {
            // If no existing group is specified then create a new one
            if (dockGroup == null)
            {
                // If this is the document region, then default to first group if it exists
                if (DockArea == DockArea.Document && _groups.Count > 0)
                {
                    dockGroup = _groups[0];
                }
                else
                {
                    dockGroup = CreateGroup();
                }
            }

            dockContent.DockRegion = this;
            dockGroup.AddContent(dockContent);

            if (!Visible)
            {
                Visible = true;
                CreateSplitter();
            }

            PositionGroups();
        }

        internal void InsertContent(CrownDockContent dockContent, CrownDockGroup dockGroup, DockInsertType insertType)
        {
            int order = dockGroup.Order;

            if (insertType == DockInsertType.After)
            {
                order++;
            }

            CrownDockGroup newGroup = InsertGroup(order);

            dockContent.DockRegion = this;
            newGroup.AddContent(dockContent);

            if (!Visible)
            {
                Visible = true;
                CreateSplitter();
            }

            PositionGroups();
        }

        internal void RemoveContent(CrownDockContent dockContent)
        {
            dockContent.DockRegion = null;

            CrownDockGroup group = dockContent.DockGroup;
            group.RemoveContent(dockContent);

            dockContent.DockArea = DockArea.None;

            // If that was the final content in the group then remove the group
            if (group.ContentCount == 0)
            {
                RemoveGroup(group);
            }

            // If we just removed the final group, and this isn't the document region, then hide
            if (_groups.Count == 0 && DockArea != DockArea.Document)
            {
                Visible = false;
                RemoveSplitter();
            }

            PositionGroups();
        }

        public List<CrownDockContent> GetContents()
        {
            List<CrownDockContent> result = new();

            foreach (CrownDockGroup group in _groups)
            {
                result.AddRange(group.GetContents());
            }

            return result;
        }

        private CrownDockGroup CreateGroup()
        {
            int order = 0;

            if (_groups.Count >= 1)
            {
                order = -1;
                foreach (CrownDockGroup group in _groups)
                {
                    if (group.Order >= order)
                    {
                        order = group.Order + 1;
                    }
                }
            }

            CrownDockGroup newGroup = new(DockPanel, this, order);
            _groups.Add(newGroup);
            Controls.Add(newGroup);

            return newGroup;
        }

        private CrownDockGroup InsertGroup(int order)
        {
            foreach (CrownDockGroup group in _groups)
            {
                if (group.Order >= order)
                {
                    group.Order++;
                }
            }

            CrownDockGroup newGroup = new(DockPanel, this, order);
            _groups.Add(newGroup);
            Controls.Add(newGroup);

            return newGroup;
        }

        private void RemoveGroup(CrownDockGroup group)
        {
            int lastOrder = group.Order;

            _groups.Remove(group);
            Controls.Remove(group);

            foreach (CrownDockGroup otherGroup in _groups)
            {
                if (otherGroup.Order > lastOrder)
                {
                    otherGroup.Order--;
                }
            }
        }

        private void PositionGroups()
        {
            DockStyle dockStyle = DockArea switch
            {
                DockArea.Left or DockArea.Right => DockStyle.Top,
                DockArea.Bottom => DockStyle.Left,
                _ => DockStyle.Fill,
            };
            if (_groups.Count == 1)
            {
                _groups[0].Dock = DockStyle.Fill;
                return;
            }

            if (_groups.Count > 1)
            {
                CrownDockGroup lastGroup = _groups.OrderByDescending(g => g.Order).First();

                foreach (CrownDockGroup group in _groups.OrderByDescending(g => g.Order))
                {
                    group.SendToBack();

                    if (group.Order == lastGroup.Order)
                    {
                        group.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        group.Dock = dockStyle;
                    }
                }

                SizeGroups();
            }
        }

        private void SizeGroups()
        {
            if (_groups.Count <= 1)
            {
                return;
            }

            Size size = new(0, 0);

            switch (DockArea)
            {
                default:
                case DockArea.Document:
                    return;
                case DockArea.Left:
                case DockArea.Right:
                    size = new(ClientRectangle.Width, ClientRectangle.Height / _groups.Count);
                    break;
                case DockArea.Bottom:
                    size = new(ClientRectangle.Width / _groups.Count, ClientRectangle.Height);
                    break;
            }

            foreach (CrownDockGroup group in _groups)
            {
                group.Size = size;
            }
        }

        private void BuildProperties()
        {
            MinimumSize = new(50, 50);

            switch (DockArea)
            {
                default:
                case DockArea.Document:
                    Dock = DockStyle.Fill;
                    Padding = new Padding(0, 1, 0, 0);
                    break;
                case DockArea.Left:
                    Dock = DockStyle.Left;
                    Padding = new Padding(0, 0, 1, 0);
                    Visible = false;
                    break;
                case DockArea.Right:
                    Dock = DockStyle.Right;
                    Padding = new Padding(1, 0, 0, 0);
                    Visible = false;
                    break;
                case DockArea.Bottom:
                    Dock = DockStyle.Bottom;
                    Padding = new Padding(0, 0, 0, 0);
                    Visible = false;
                    break;
            }
        }

        private void CreateSplitter()
        {
            if (_splitter != null && DockPanel.Splitters.Contains(_splitter))
            {
                DockPanel.Splitters.Remove(_splitter);
            }

            switch (DockArea)
            {
                case DockArea.Left:
                    _splitter = new CrownDockSplitter(DockPanel, this, SplitterType.Right);
                    break;
                case DockArea.Right:
                    _splitter = new CrownDockSplitter(DockPanel, this, SplitterType.Left);
                    break;
                case DockArea.Bottom:
                    _splitter = new CrownDockSplitter(DockPanel, this, SplitterType.Top);
                    break;
                default:
                    return;
            }

            DockPanel.Splitters.Add(_splitter);
        }

        private void RemoveSplitter()
        {
            if (DockPanel.Splitters.Contains(_splitter))
            {
                DockPanel.Splitters.Remove(_splitter);
            }
        }

        #endregion

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            _parentForm = FindForm();
            _parentForm.ResizeEnd += ParentForm_ResizeEnd;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            SizeGroups();
        }

        private void ParentForm_ResizeEnd(object sender, EventArgs e)
        {
            if (_splitter != null)
            {
                _splitter.UpdateBounds();
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (_splitter != null)
            {
                _splitter.UpdateBounds();
            }
        }

        #endregion

        #region Paint Region

        public void Redraw()
        {
            Invalidate();

            foreach (CrownDockGroup group in _groups)
            {
                group.Redraw();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!Visible)
            {
                return;
            }

            // Fill body
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            // Draw border
            using Pen p = new(ThemeProvider.Theme.Colors.DarkBorder);
            // Top border
            if (DockArea == DockArea.Document)
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Right, 0);
            }

            // Left border
            if (DockArea == DockArea.Right)
            {
                g.DrawLine(p, ClientRectangle.Left, 0, ClientRectangle.Left, ClientRectangle.Height);
            }

            // Right border
            if (DockArea == DockArea.Left)
            {
                g.DrawLine(p, ClientRectangle.Right - 1, 0, ClientRectangle.Right - 1, ClientRectangle.Height);
            }
        }

        #endregion
    }

    #endregion
}