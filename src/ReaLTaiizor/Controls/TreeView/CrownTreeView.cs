#region Imports

using ReaLTaiizor.Extension.Crown;
using ReaLTaiizor.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTreeView

    public class CrownTreeView : CrownScrollView
    {
        #region Event Region

        public event EventHandler SelectedNodesChanged;
        public event EventHandler AfterNodeExpand;
        public event EventHandler AfterNodeCollapse;

        #endregion

        #region Field Region

        private bool _disposed;

        private readonly int _expandAreaSize = 16;
        private readonly int _iconSize = 16;

        private int _itemHeight = 20;
        private int _indent = 20;

        private ObservableList<CrownTreeNode> _nodes;
        private CrownTreeNode _anchoredNodeStart;
        private CrownTreeNode _anchoredNodeEnd;

        private Bitmap _nodeClosed;
        private Bitmap _nodeClosedHover;
        private Bitmap _nodeClosedHoverSelected;
        private Bitmap _nodeOpen;
        private Bitmap _nodeOpenHover;
        private Bitmap _nodeOpenHoverSelected;

        private CrownTreeNode _provisionalNode;
        private CrownTreeNode _dropNode;
        private bool _provisionalDragging;
        private List<CrownTreeNode> _dragNodes;
        private Point _dragPos;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableList<CrownTreeNode> Nodes
        {
            get => _nodes;
            set
            {
                if (_nodes != null)
                {
                    _nodes.ItemsAdded -= Nodes_ItemsAdded;
                    _nodes.ItemsRemoved -= Nodes_ItemsRemoved;

                    foreach (CrownTreeNode node in _nodes)
                    {
                        UnhookNodeEvents(node);
                    }
                }

                _nodes = value;

                _nodes.ItemsAdded += Nodes_ItemsAdded;
                _nodes.ItemsRemoved += Nodes_ItemsRemoved;

                foreach (CrownTreeNode node in _nodes)
                {
                    HookNodeEvents(node);
                }

                UpdateNodes();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<CrownTreeNode> SelectedNodes { get; }

        [Category("Appearance")]
        [Description("Determines the height of tree nodes.")]
        [DefaultValue(20)]
        public int ItemHeight
        {
            get => _itemHeight;
            set
            {
                _itemHeight = value;
                MaxDragChange = _itemHeight;
                UpdateNodes();
            }
        }

        [Category("Appearance")]
        [Description("Determines the amount of horizontal space given by parent node.")]
        [DefaultValue(20)]
        public int Indent
        {
            get => _indent;
            set
            {
                _indent = value;
                UpdateNodes();
            }
        }

        [Category("Behavior")]
        [Description("Determines whether multiple tree nodes can be selected at once.")]
        [DefaultValue(false)]
        public bool MultiSelect { get; set; }

        [Category("Behavior")]
        [Description("Determines whether nodes can be moved within this tree view.")]
        [DefaultValue(false)]
        public bool AllowMoveNodes { get; set; }

        [Category("Appearance")]
        [Description("Determines whether icons are rendered with the tree nodes.")]
        [DefaultValue(false)]
        public bool ShowIcons { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VisibleNodeCount { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IComparer<CrownTreeNode> TreeViewNodeSorter { get; set; }

        #endregion

        #region Constructor Region

        public CrownTreeView()
        {
            Nodes = new ObservableList<CrownTreeNode>();
            SelectedNodes = new ObservableCollection<CrownTreeNode>();
            SelectedNodes.CollectionChanged += SelectedNodes_CollectionChanged;

            MaxDragChange = _itemHeight;

            LoadIcons();
        }

        #endregion

        #region Dispose Region

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                DisposeIcons();

                if (SelectedNodesChanged != null)
                {
                    SelectedNodesChanged = null;
                }

                if (AfterNodeExpand != null)
                {
                    AfterNodeExpand = null;
                }

                if (AfterNodeCollapse != null)
                {
                    AfterNodeExpand = null;
                }

                if (_nodes != null)
                {
                    _nodes.Dispose();
                }

                if (SelectedNodes != null)
                {
                    SelectedNodes.CollectionChanged -= SelectedNodes_CollectionChanged;
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler Region

        private void Nodes_ItemsAdded(object sender, ObservableListModified<CrownTreeNode> e)
        {
            foreach (CrownTreeNode node in e.Items)
            {
                node.ParentTree = this;
                node.IsRoot = true;

                HookNodeEvents(node);
            }

            if (TreeViewNodeSorter != null)
            {
                Nodes.Sort(TreeViewNodeSorter);
            }

            UpdateNodes();
        }

        private void Nodes_ItemsRemoved(object sender, ObservableListModified<CrownTreeNode> e)
        {
            foreach (CrownTreeNode node in e.Items)
            {
                node.ParentTree = this;
                node.IsRoot = true;

                HookNodeEvents(node);
            }

            UpdateNodes();
        }

        private void ChildNodes_ItemsAdded(object sender, ObservableListModified<CrownTreeNode> e)
        {
            foreach (CrownTreeNode node in e.Items)
            {
                HookNodeEvents(node);
            }

            UpdateNodes();
        }

        private void ChildNodes_ItemsRemoved(object sender, ObservableListModified<CrownTreeNode> e)
        {
            foreach (CrownTreeNode node in e.Items)
            {
                if (SelectedNodes.Contains(node))
                {
                    SelectedNodes.Remove(node);
                }

                UnhookNodeEvents(node);
            }

            UpdateNodes();
        }

        private void SelectedNodes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SelectedNodesChanged?.Invoke(this, null);
        }

        private void Nodes_TextChanged(object sender, EventArgs e)
        {
            UpdateNodes();
        }

        private void Nodes_NodeExpanded(object sender, EventArgs e)
        {
            UpdateNodes();

            AfterNodeExpand?.Invoke(this, null);
        }

        private void Nodes_NodeCollapsed(object sender, EventArgs e)
        {
            UpdateNodes();

            AfterNodeCollapse?.Invoke(this, null);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_provisionalDragging)
            {
                if (OffsetMousePosition != _dragPos)
                {
                    StartDrag();
                    HandleDrag();
                    return;
                }
            }

            if (IsDragging)
            {
                if (_dropNode != null)
                {
                    Rectangle rect = GetNodeFullRowArea(_dropNode);
                    if (!rect.Contains(OffsetMousePosition))
                    {
                        _dropNode = null;
                        Invalidate();
                    }
                }
            }

            CheckHover();

            if (IsDragging)
            {
                HandleDrag();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            CheckHover();

            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button is MouseButtons.Left or MouseButtons.Right)
            {
                foreach (CrownTreeNode node in Nodes)
                {
                    CheckNodeClick(node, OffsetMousePosition, e.Button);
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (IsDragging)
            {
                HandleDrop();
            }

            if (_provisionalDragging)
            {

                if (_provisionalNode != null)
                {
                    Point pos = _dragPos;
                    if (OffsetMousePosition == pos)
                    {
                        SelectNode(_provisionalNode);
                    }
                }

                _provisionalDragging = false;
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                foreach (CrownTreeNode node in Nodes)
                {
                    CheckNodeDoubleClick(node, OffsetMousePosition);
                }
            }

            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            foreach (CrownTreeNode node in Nodes)
            {
                NodeMouseLeave(node);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (IsDragging)
            {
                return;
            }

            if (Nodes.Count == 0)
            {
                return;
            }

            if (e.KeyCode is not Keys.Down and not Keys.Up and not Keys.Left and not Keys.Right)
            {
                return;
            }

            if (_anchoredNodeEnd == null)
            {
                if (Nodes.Count > 0)
                {
                    SelectNode(Nodes[0]);
                }

                return;
            }

            if (e.KeyCode is Keys.Down or Keys.Up)
            {
                if (MultiSelect && ModifierKeys == Keys.Shift)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (_anchoredNodeEnd.PrevVisibleNode != null)
                        {
                            SelectAnchoredRange(_anchoredNodeEnd.PrevVisibleNode);
                            EnsureVisible();
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (_anchoredNodeEnd.NextVisibleNode != null)
                        {
                            SelectAnchoredRange(_anchoredNodeEnd.NextVisibleNode);
                            EnsureVisible();
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (_anchoredNodeEnd.PrevVisibleNode != null)
                        {
                            SelectNode(_anchoredNodeEnd.PrevVisibleNode);
                            EnsureVisible();
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (_anchoredNodeEnd.NextVisibleNode != null)
                        {
                            SelectNode(_anchoredNodeEnd.NextVisibleNode);
                            EnsureVisible();
                        }
                    }
                }
            }

            if (e.KeyCode is Keys.Left or Keys.Right)
            {
                if (e.KeyCode == Keys.Left)
                {
                    if (_anchoredNodeEnd.Expanded && _anchoredNodeEnd.Nodes.Count > 0)
                    {
                        _anchoredNodeEnd.Expanded = false;
                    }
                    else
                    {
                        if (_anchoredNodeEnd.ParentNode != null)
                        {
                            SelectNode(_anchoredNodeEnd.ParentNode);
                            EnsureVisible();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (!_anchoredNodeEnd.Expanded)
                    {
                        _anchoredNodeEnd.Expanded = true;
                    }
                    else
                    {
                        if (_anchoredNodeEnd.Nodes.Count > 0)
                        {
                            SelectNode(_anchoredNodeEnd.Nodes[0]);
                            EnsureVisible();
                        }
                    }
                }
            }
        }

        private void DragTimer_Tick(object sender, EventArgs e)
        {
            if (!IsDragging)
            {
                StopDrag();
                return;
            }

            if (MouseButtons != MouseButtons.Left)
            {
                StopDrag();
                return;
            }

            Point pos = PointToClient(MousePosition);

            if (_vScrollBar.Visible)
            {
                // Scroll up
                if (pos.Y < ClientRectangle.Top)
                {
                    int difference = (pos.Y - ClientRectangle.Top) * -1;

                    if (difference > ItemHeight)
                    {
                        difference = ItemHeight;
                    }

                    _vScrollBar.Value -= difference;
                }

                // Scroll down
                if (pos.Y > ClientRectangle.Bottom)
                {
                    int difference = pos.Y - ClientRectangle.Bottom;

                    if (difference > ItemHeight)
                    {
                        difference = ItemHeight;
                    }

                    _vScrollBar.Value += difference;
                }
            }

            if (_hScrollBar.Visible)
            {
                // Scroll left
                if (pos.X < ClientRectangle.Left)
                {
                    int difference = (pos.X - ClientRectangle.Left) * -1;

                    if (difference > ItemHeight)
                    {
                        difference = ItemHeight;
                    }

                    _hScrollBar.Value -= difference;
                }

                // Scroll right
                if (pos.X > ClientRectangle.Right)
                {
                    int difference = pos.X - ClientRectangle.Right;

                    if (difference > ItemHeight)
                    {
                        difference = ItemHeight;
                    }

                    _hScrollBar.Value += difference;
                }
            }
        }

        #endregion

        #region Method Region

        private void HookNodeEvents(CrownTreeNode node)
        {
            node.Nodes.ItemsAdded += ChildNodes_ItemsAdded;
            node.Nodes.ItemsRemoved += ChildNodes_ItemsRemoved;

            node.TextChanged += Nodes_TextChanged;
            node.NodeExpanded += Nodes_NodeExpanded;
            node.NodeCollapsed += Nodes_NodeCollapsed;

            foreach (CrownTreeNode childNode in node.Nodes)
            {
                HookNodeEvents(childNode);
            }
        }

        private void UnhookNodeEvents(CrownTreeNode node)
        {
            node.Nodes.ItemsAdded -= ChildNodes_ItemsAdded;
            node.Nodes.ItemsRemoved -= ChildNodes_ItemsRemoved;

            node.TextChanged -= Nodes_TextChanged;
            node.NodeExpanded -= Nodes_NodeExpanded;
            node.NodeCollapsed -= Nodes_NodeCollapsed;

            foreach (CrownTreeNode childNode in node.Nodes)
            {
                UnhookNodeEvents(childNode);
            }
        }

        private void UpdateNodes()
        {
            if (IsDragging)
            {
                return;
            }

            ContentSize = new(0, 0);

            if (Nodes.Count == 0)
            {
                return;
            }

            int yOffset = 0;
            bool isOdd = false;
            int index = 0;
            CrownTreeNode prevNode = null;

            for (int i = 0; i <= Nodes.Count - 1; i++)
            {
                CrownTreeNode node = Nodes[i];
                UpdateNode(node, ref prevNode, 0, ref yOffset, ref isOdd, ref index);
            }

            ContentSize = new(ContentSize.Width, yOffset);

            VisibleNodeCount = index;

            Invalidate();
        }

        private void UpdateNode(CrownTreeNode node, ref CrownTreeNode prevNode, int indent, ref int yOffset,
                                ref bool isOdd, ref int index)
        {
            UpdateNodeBounds(node, yOffset, indent);

            yOffset += ItemHeight;

            node.Odd = isOdd;
            isOdd = !isOdd;

            node.VisibleIndex = index;
            index++;

            node.PrevVisibleNode = prevNode;

            if (prevNode != null)
            {
                prevNode.NextVisibleNode = node;
            }

            prevNode = node;

            if (node.Expanded)
            {
                foreach (CrownTreeNode childNode in node.Nodes)
                {
                    UpdateNode(childNode, ref prevNode, indent + Indent, ref yOffset, ref isOdd, ref index);
                }
            }
        }

        private void UpdateNodeBounds(CrownTreeNode node, int yOffset, int indent)
        {
            int expandTop = yOffset + (ItemHeight / 2) - (_expandAreaSize / 2);
            node.ExpandArea = new(indent + 3, expandTop, _expandAreaSize, _expandAreaSize);

            int iconTop = yOffset + (ItemHeight / 2) - (_iconSize / 2);

            if (ShowIcons)
            {
                node.IconArea = new(node.ExpandArea.Right + 2, iconTop, _iconSize, _iconSize);
            }
            else
            {
                node.IconArea = new(node.ExpandArea.Right, iconTop, 0, 0);
            }

            using (Graphics g = CreateGraphics())
            {
                int textSize = (int)g.MeasureString(node.Text, Font).Width;
                node.TextArea = new(node.IconArea.Right + 2, yOffset, textSize + 1, ItemHeight);
            }

            node.FullArea = new(indent, yOffset, node.TextArea.Right - indent, ItemHeight);

            if (ContentSize.Width < node.TextArea.Right + 2)
            {
                ContentSize = new(node.TextArea.Right + 2, ContentSize.Height);
            }
        }

        private void LoadIcons()
        {
            DisposeIcons();

            _nodeClosed = Properties.Resources.node_closed_empty.SetColor(ThemeProvider.Theme.Colors.LightText);
            _nodeClosedHover = Properties.Resources.node_closed_empty.SetColor(ThemeProvider.Theme.Colors.BlueHighlight);
            _nodeClosedHoverSelected = Properties.Resources.node_closed_full.SetColor(ThemeProvider.Theme.Colors.LightText);
            _nodeOpen = Properties.Resources.node_open.SetColor(ThemeProvider.Theme.Colors.LightText);
            _nodeOpenHover = Properties.Resources.node_open.SetColor(ThemeProvider.Theme.Colors.BlueHighlight);
            _nodeOpenHoverSelected = Properties.Resources.node_open_empty.SetColor(ThemeProvider.Theme.Colors.LightText);
        }

        private void DisposeIcons()
        {
            if (_nodeClosed != null)
            {
                _nodeClosed.Dispose();
            }

            if (_nodeClosedHover != null)
            {
                _nodeClosedHover.Dispose();
            }

            if (_nodeClosedHoverSelected != null)
            {
                _nodeClosedHoverSelected.Dispose();
            }

            if (_nodeOpen != null)
            {
                _nodeOpen.Dispose();
            }

            if (_nodeOpenHover != null)
            {
                _nodeOpenHover.Dispose();
            }

            if (_nodeOpenHoverSelected != null)
            {
                _nodeOpenHoverSelected.Dispose();
            }
        }

        private void CheckHover()
        {
            if (!ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                if (IsDragging)
                {
                    if (_dropNode != null)
                    {
                        _dropNode = null;
                        Invalidate();
                    }
                }

                return;
            }

            foreach (CrownTreeNode node in Nodes)
            {
                CheckNodeHover(node, OffsetMousePosition);
            }
        }

        private void NodeMouseLeave(CrownTreeNode node)
        {
            node.ExpandAreaHot = false;

            foreach (CrownTreeNode childNode in node.Nodes)
            {
                NodeMouseLeave(childNode);
            }

            Invalidate();
        }

        private void CheckNodeHover(CrownTreeNode node, Point location)
        {
            if (IsDragging)
            {
                Rectangle rect = GetNodeFullRowArea(node);
                if (rect.Contains(OffsetMousePosition))
                {
                    CrownTreeNode newDropNode = _dragNodes.Contains(node) ? null : node;

                    if (_dropNode != newDropNode)
                    {
                        _dropNode = newDropNode;
                        Invalidate();
                    }
                }
            }
            else
            {
                bool hot = node.ExpandArea.Contains(location);
                if (node.ExpandAreaHot != hot)
                {
                    node.ExpandAreaHot = hot;
                    Invalidate();
                }
            }

            foreach (CrownTreeNode childNode in node.Nodes)
            {
                CheckNodeHover(childNode, location);
            }
        }

        private void CheckNodeClick(CrownTreeNode node, Point location, MouseButtons button)
        {
            Rectangle rect = GetNodeFullRowArea(node);
            if (rect.Contains(location))
            {
                if (node.ExpandArea.Contains(location))
                {
                    if (button == MouseButtons.Left)
                    {
                        node.Expanded = !node.Expanded;
                    }
                }
                else
                {
                    if (button == MouseButtons.Left)
                    {
                        if (MultiSelect && ModifierKeys == Keys.Shift)
                        {
                            SelectAnchoredRange(node);
                        }
                        else if (MultiSelect && ModifierKeys == Keys.Control)
                        {
                            ToggleNode(node);
                        }
                        else
                        {
                            if (!SelectedNodes.Contains(node))
                            {
                                SelectNode(node);
                            }

                            _dragPos = OffsetMousePosition;
                            _provisionalDragging = true;
                            _provisionalNode = node;
                        }

                        return;
                    }
                    else if (button == MouseButtons.Right)
                    {
                        if (MultiSelect && ModifierKeys == Keys.Shift)
                        {
                            return;
                        }

                        if (MultiSelect && ModifierKeys == Keys.Control)
                        {
                            return;
                        }

                        if (!SelectedNodes.Contains(node))
                        {
                            SelectNode(node);
                        }

                        return;
                    }
                }
            }

            if (node.Expanded)
            {
                foreach (CrownTreeNode childNode in node.Nodes)
                {
                    CheckNodeClick(childNode, location, button);
                }
            }
        }

        private void CheckNodeDoubleClick(CrownTreeNode node, Point location)
        {
            Rectangle rect = GetNodeFullRowArea(node);
            if (rect.Contains(location))
            {
                if (!node.ExpandArea.Contains(location))
                {
                    node.Expanded = !node.Expanded;
                }

                return;
            }

            if (node.Expanded)
            {
                foreach (CrownTreeNode childNode in node.Nodes)
                {
                    CheckNodeDoubleClick(childNode, location);
                }
            }
        }

        public void SelectNode(CrownTreeNode node)
        {
            SelectedNodes.Clear();
            SelectedNodes.Add(node);

            _anchoredNodeStart = node;
            _anchoredNodeEnd = node;

            Invalidate();
        }

        public void SelectNodes(CrownTreeNode startNode, CrownTreeNode endNode)
        {
            List<CrownTreeNode> nodes = new();

            if (startNode == endNode)
            {
                nodes.Add(startNode);
            }

            if (startNode.VisibleIndex < endNode.VisibleIndex)
            {
                CrownTreeNode node = startNode;
                nodes.Add(node);
                while (node != endNode && node != null)
                {
                    node = node.NextVisibleNode;
                    nodes.Add(node);
                }
            }
            else if (startNode.VisibleIndex > endNode.VisibleIndex)
            {
                CrownTreeNode node = startNode;
                nodes.Add(node);
                while (node != endNode && node != null)
                {
                    node = node.PrevVisibleNode;
                    nodes.Add(node);
                }
            }

            SelectNodes(nodes, false);
        }

        public void SelectNodes(List<CrownTreeNode> nodes, bool updateAnchors = true)
        {
            SelectedNodes.Clear();

            foreach (CrownTreeNode node in nodes)
            {
                SelectedNodes.Add(node);
            }

            if (updateAnchors && SelectedNodes.Count > 0)
            {
                _anchoredNodeStart = SelectedNodes[SelectedNodes.Count - 1];
                _anchoredNodeEnd = SelectedNodes[SelectedNodes.Count - 1];
            }

            Invalidate();
        }

        private void SelectAnchoredRange(CrownTreeNode node)
        {
            _anchoredNodeEnd = node;
            SelectNodes(_anchoredNodeStart, _anchoredNodeEnd);
        }

        public void ToggleNode(CrownTreeNode node)
        {
            if (SelectedNodes.Contains(node))
            {
                SelectedNodes.Remove(node);

                // If we just removed both the anchor start AND end then reset them
                if (_anchoredNodeStart == node && _anchoredNodeEnd == node)
                {
                    if (SelectedNodes.Count > 0)
                    {
                        _anchoredNodeStart = SelectedNodes[0];
                        _anchoredNodeEnd = SelectedNodes[0];
                    }
                    else
                    {
                        _anchoredNodeStart = null;
                        _anchoredNodeEnd = null;
                    }
                }

                // If we just removed the anchor start then update it accordingly
                if (_anchoredNodeStart == node)
                {
                    if (_anchoredNodeEnd.VisibleIndex < node.VisibleIndex)
                    {
                        _anchoredNodeStart = node.PrevVisibleNode;
                    }
                    else if (_anchoredNodeEnd.VisibleIndex > node.VisibleIndex)
                    {
                        _anchoredNodeStart = node.NextVisibleNode;
                    }
                    else
                    {
                        _anchoredNodeStart = _anchoredNodeEnd;
                    }
                }

                // If we just removed the anchor end then update it accordingly
                if (_anchoredNodeEnd == node)
                {
                    if (_anchoredNodeStart.VisibleIndex < node.VisibleIndex)
                    {
                        _anchoredNodeEnd = node.PrevVisibleNode;
                    }
                    else if (_anchoredNodeStart.VisibleIndex > node.VisibleIndex)
                    {
                        _anchoredNodeEnd = node.NextVisibleNode;
                    }
                    else
                    {
                        _anchoredNodeEnd = _anchoredNodeStart;
                    }
                }
            }
            else
            {
                SelectedNodes.Add(node);

                _anchoredNodeStart = node;
                _anchoredNodeEnd = node;
            }

            Invalidate();
        }

        public Rectangle GetNodeFullRowArea(CrownTreeNode node)
        {
            if (node.ParentNode != null && !node.ParentNode.Expanded)
            {
                return new Rectangle(-1, -1, -1, -1);
            }

            int width = Math.Max(ContentSize.Width, Viewport.Width);
            Rectangle rect = new(0, node.FullArea.Top, width, ItemHeight);
            return rect;
        }

        public void EnsureVisible()
        {
            if (SelectedNodes.Count == 0)
            {
                return;
            }

            foreach (CrownTreeNode node in SelectedNodes)
            {
                node.EnsureVisible();
            }

            int itemTop = -1;

            if (!MultiSelect)
            {
                itemTop = SelectedNodes[0].FullArea.Top;
            }
            else
            {
                itemTop = _anchoredNodeEnd.FullArea.Top;
            }

            int itemBottom = itemTop + ItemHeight;

            if (itemTop < Viewport.Top)
            {
                VScrollTo(itemTop);
            }

            if (itemBottom > Viewport.Bottom)
            {
                VScrollTo(itemBottom - Viewport.Height);
            }
        }

        public void Sort()
        {
            if (TreeViewNodeSorter == null)
            {
                return;
            }

            Nodes.Sort(TreeViewNodeSorter);

            foreach (CrownTreeNode node in Nodes)
            {
                SortChildNodes(node);
            }
        }

        private void SortChildNodes(CrownTreeNode node)
        {
            node.Nodes.Sort(TreeViewNodeSorter);

            foreach (CrownTreeNode childNode in node.Nodes)
            {
                SortChildNodes(childNode);
            }
        }

        public CrownTreeNode FindNode(string path)
        {
            foreach (CrownTreeNode node in Nodes)
            {
                CrownTreeNode compNode = FindNode(node, path);
                if (compNode != null)
                {
                    return compNode;
                }
            }

            return null;
        }

        private CrownTreeNode FindNode(CrownTreeNode parentNode, string path, bool recursive = true)
        {
            if (parentNode.FullPath == path)
            {
                return parentNode;
            }

            foreach (CrownTreeNode node in parentNode.Nodes)
            {
                if (node.FullPath == path)
                {
                    return node;
                }

                if (recursive)
                {
                    CrownTreeNode compNode = FindNode(node, path);
                    if (compNode != null)
                    {
                        return compNode;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Drag & Drop Region

        protected override void StartDrag()
        {
            if (!AllowMoveNodes)
            {
                _provisionalDragging = false;
                return;
            }

            // Create initial list of nodes to drag
            _dragNodes = new List<CrownTreeNode>();
            foreach (CrownTreeNode node in SelectedNodes)
            {
                _dragNodes.Add(node);
            }

            // Clear out any nodes with a parent that is being dragged
            foreach (CrownTreeNode node in _dragNodes.ToList())
            {
                if (node.ParentNode == null)
                {
                    continue;
                }

                if (_dragNodes.Contains(node.ParentNode))
                {
                    _dragNodes.Remove(node);
                }
            }

            _provisionalDragging = false;

            Cursor = Cursors.SizeAll;

            base.StartDrag();
        }

        private void HandleDrag()
        {
            if (!AllowMoveNodes)
            {
                return;
            }

            CrownTreeNode dropNode = _dropNode;

            if (dropNode == null)
            {
                if (Cursor != Cursors.No)
                {
                    Cursor = Cursors.No;
                }

                return;
            }

            if (ForceDropToParent(dropNode))
            {
                dropNode = dropNode.ParentNode;
            }

            if (!CanMoveNodes(_dragNodes, dropNode))
            {
                if (Cursor != Cursors.No)
                {
                    Cursor = Cursors.No;
                }

                return;
            }

            if (Cursor != Cursors.SizeAll)
            {
                Cursor = Cursors.SizeAll;
            }
        }

        private void HandleDrop()
        {
            if (!AllowMoveNodes)
            {
                return;
            }

            CrownTreeNode dropNode = _dropNode;

            if (dropNode == null)
            {
                StopDrag();
                return;
            }

            if (ForceDropToParent(dropNode))
            {
                dropNode = dropNode.ParentNode;
            }

            if (CanMoveNodes(_dragNodes, dropNode, true))
            {
                List<CrownTreeNode> cachedSelectedNodes = SelectedNodes.ToList();

                MoveNodes(_dragNodes, dropNode);

                foreach (CrownTreeNode node in _dragNodes)
                {
                    if (node.ParentNode == null)
                    {
                        Nodes.Remove(node);
                    }
                    else
                    {
                        node.ParentNode.Nodes.Remove(node);
                    }

                    dropNode.Nodes.Add(node);
                }

                if (TreeViewNodeSorter != null)
                {
                    dropNode.Nodes.Sort(TreeViewNodeSorter);
                }

                dropNode.Expanded = true;

                NodesMoved(_dragNodes);

                foreach (CrownTreeNode node in cachedSelectedNodes)
                {
                    SelectedNodes.Add(node);
                }
            }

            StopDrag();
            UpdateNodes();
        }

        protected override void StopDrag()
        {
            _dragNodes = null;
            _dropNode = null;

            Cursor = Cursors.Default;

            Invalidate();

            base.StopDrag();
        }

        protected virtual bool ForceDropToParent(CrownTreeNode node)
        {
            return false;
        }

        protected virtual bool CanMoveNodes(List<CrownTreeNode> dragNodes, CrownTreeNode dropNode, bool isMoving = false)
        {
            if (dropNode == null)
            {
                return false;
            }

            foreach (CrownTreeNode node in dragNodes)
            {
                if (node == dropNode)
                {
                    if (isMoving)
                    {
                        CrownMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is the same as the source folder.", Application.ProductName);
                    }

                    return false;
                }

                if (node.ParentNode != null && node.ParentNode == dropNode)
                {
                    if (isMoving)
                    {
                        CrownMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is the same as the source folder.", Application.ProductName);
                    }

                    return false;
                }

                CrownTreeNode parentNode = dropNode.ParentNode;
                while (parentNode != null)
                {
                    if (node == parentNode)
                    {
                        if (isMoving)
                        {
                            CrownMessageBox.ShowError($"Cannot move {node.Text}. The destination folder is a subfolder of the source folder.", Application.ProductName);
                        }

                        return false;
                    }

                    parentNode = parentNode.ParentNode;
                }
            }

            return true;
        }

        protected virtual void MoveNodes(List<CrownTreeNode> dragNodes, CrownTreeNode dropNode)
        { }

        protected virtual void NodesMoved(List<CrownTreeNode> nodesMoved)
        { }

        #endregion

        #region Paint Region

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            LoadIcons();
        }

        protected override void PaintContent(Graphics g)
        {
            // Fill body
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }

            foreach (CrownTreeNode node in Nodes)
            {
                DrawNode(node, g);
            }
        }

        private void DrawNode(CrownTreeNode node, Graphics g)
        {
            Rectangle rect = GetNodeFullRowArea(node);

            // 1. Draw background
            Color bgColor = node.Odd ? ThemeProvider.Theme.Colors.HeaderBackground : ThemeProvider.Theme.Colors.GreyBackground;

            if (SelectedNodes.Count > 0 && SelectedNodes.Contains(node))
            {
                bgColor = Focused ? ThemeProvider.Theme.Colors.BlueSelection : ThemeProvider.Theme.Colors.GreySelection;
            }

            if (IsDragging && _dropNode == node)
            {
                bgColor = Focused ? ThemeProvider.Theme.Colors.BlueSelection : ThemeProvider.Theme.Colors.GreySelection;
            }

            using (SolidBrush b = new(bgColor))
            {
                g.FillRectangle(b, rect);
            }

            // 2. Draw plus/minus icon
            if (node.Nodes.Count > 0)
            {
                Point pos = new(node.ExpandArea.Location.X - 1, node.ExpandArea.Location.Y - 1);

                Bitmap icon = _nodeOpen;

                if (node.Expanded && !node.ExpandAreaHot)
                {
                    icon = _nodeOpen;
                }
                else if (node.Expanded && node.ExpandAreaHot && !SelectedNodes.Contains(node))
                {
                    icon = _nodeOpenHover;
                }
                else if (node.Expanded && node.ExpandAreaHot && SelectedNodes.Contains(node))
                {
                    icon = _nodeOpenHoverSelected;
                }
                else if (!node.Expanded && !node.ExpandAreaHot)
                {
                    icon = _nodeClosed;
                }
                else if (!node.Expanded && node.ExpandAreaHot && !SelectedNodes.Contains(node))
                {
                    icon = _nodeClosedHover;
                }
                else if (!node.Expanded && node.ExpandAreaHot && SelectedNodes.Contains(node))
                {
                    icon = _nodeClosedHoverSelected;
                }

                g.DrawImageUnscaled(icon, pos);
            }

            // 3. Draw icon
            if (ShowIcons && node.Icon != null)
            {
                if (node.Expanded && node.ExpandedIcon != null)
                {
                    g.DrawImageUnscaled(node.ExpandedIcon, node.IconArea.Location);
                }
                else
                {
                    g.DrawImageUnscaled(node.Icon, node.IconArea.Location);
                }
            }

            // 4. Draw text
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.LightText))
            {
                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(node.Text, Font, b, node.TextArea, stringFormat);
            }

            // 5. Draw child nodes
            if (node.Expanded)
            {
                foreach (CrownTreeNode childNode in node.Nodes)
                {
                    DrawNode(childNode, g);
                }
            }
        }

        #endregion
    }

    #endregion
}