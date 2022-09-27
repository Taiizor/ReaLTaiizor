#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownTreeNode

    public class CrownTreeNode
    {
        #region Event Region

        public event EventHandler<ObservableListModified<CrownTreeNode>> ItemsAdded;
        public event EventHandler<ObservableListModified<CrownTreeNode>> ItemsRemoved;

        public event EventHandler TextChanged;
        public event EventHandler NodeExpanded;
        public event EventHandler NodeCollapsed;

        #endregion

        #region Field Region

        private string _text;
        private CrownTreeView _parentTree;
        private ObservableList<CrownTreeNode> _nodes;

        private bool _expanded;

        #endregion

        #region Property Region

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;

                OnTextChanged();
            }
        }

        public Rectangle ExpandArea { get; set; }

        public Rectangle IconArea { get; set; }

        public Rectangle TextArea { get; set; }

        public Rectangle FullArea { get; set; }

        public bool ExpandAreaHot { get; set; }

        public Bitmap Icon { get; set; }

        public Bitmap ExpandedIcon { get; set; }

        public bool Expanded
        {
            get => _expanded;
            set
            {
                if (_expanded == value)
                {
                    return;
                }

                if (value == true && Nodes.Count == 0)
                {
                    return;
                }

                _expanded = value;

                if (_expanded)
                {
                    NodeExpanded?.Invoke(this, null);
                }
                else
                {
                    NodeCollapsed?.Invoke(this, null);
                }
            }
        }

        public ObservableList<CrownTreeNode> Nodes
        {
            get => _nodes;
            set
            {
                if (_nodes != null)
                {
                    _nodes.ItemsAdded -= Nodes_ItemsAdded;
                    _nodes.ItemsRemoved -= Nodes_ItemsRemoved;
                }

                _nodes = value;

                _nodes.ItemsAdded += Nodes_ItemsAdded;
                _nodes.ItemsRemoved += Nodes_ItemsRemoved;
            }
        }

        public bool IsRoot { get; set; }

        public CrownTreeView ParentTree
        {
            get => _parentTree;
            set
            {
                if (_parentTree == value)
                {
                    return;
                }

                _parentTree = value;

                foreach (CrownTreeNode node in Nodes)
                {
                    node.ParentTree = _parentTree;
                }
            }
        }

        public CrownTreeNode ParentNode { get; set; }

        public bool Odd { get; set; }

        public object NodeType { get; set; }

        public object Tag { get; set; }

        public string FullPath
        {
            get
            {
                CrownTreeNode parent = ParentNode;
                string path = Text;

                while (parent != null)
                {
                    path = string.Format("{0}{1}{2}", parent.Text, "\\", path);
                    parent = parent.ParentNode;
                }

                return path;
            }
        }

        public CrownTreeNode PrevVisibleNode { get; set; }

        public CrownTreeNode NextVisibleNode { get; set; }

        public int VisibleIndex { get; set; }

        public bool IsNodeAncestor(CrownTreeNode node)
        {
            CrownTreeNode parent = ParentNode;
            while (parent != null)
            {
                if (parent == node)
                {
                    return true;
                }

                parent = parent.ParentNode;
            }

            return false;
        }

        #endregion

        #region Constructor Region

        public CrownTreeNode()
        {
            Nodes = new ObservableList<CrownTreeNode>();
        }

        public CrownTreeNode(string text) : this()
        {
            Text = text;
        }

        #endregion

        #region Method Region

        public void Remove()
        {
            if (ParentNode != null)
            {
                ParentNode.Nodes.Remove(this);
            }
            else
            {
                ParentTree.Nodes.Remove(this);
            }
        }

        public void EnsureVisible()
        {
            CrownTreeNode parent = ParentNode;

            while (parent != null)
            {
                parent.Expanded = true;
                parent = parent.ParentNode;
            }
        }

        #endregion

        #region Event Handler Region

        private void OnTextChanged()
        {
            if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
            {
                if (ParentNode != null)
                {
                    ParentNode.Nodes.Sort(ParentTree.TreeViewNodeSorter);
                }
                else
                {
                    ParentTree.Nodes.Sort(ParentTree.TreeViewNodeSorter);
                }
            }

            TextChanged?.Invoke(this, null);
        }

        private void Nodes_ItemsAdded(object sender, ObservableListModified<CrownTreeNode> e)
        {
            foreach (CrownTreeNode node in e.Items)
            {
                node.ParentNode = this;
                node.ParentTree = ParentTree;
            }

            if (ParentTree != null && ParentTree.TreeViewNodeSorter != null)
            {
                Nodes.Sort(ParentTree.TreeViewNodeSorter);
            }

            ItemsAdded?.Invoke(this, e);
        }

        private void Nodes_ItemsRemoved(object sender, ObservableListModified<CrownTreeNode> e)
        {
            if (Nodes.Count == 0)
            {
                Expanded = false;
            }

            ItemsRemoved?.Invoke(this, e);
        }

        #endregion
    }

    #endregion
}