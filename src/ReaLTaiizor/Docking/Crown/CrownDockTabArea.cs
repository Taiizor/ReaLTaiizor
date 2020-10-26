﻿#region Imports

using System.Drawing;
using System.Windows.Forms;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Crown;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Docking.Crown
{
    #region CrownDockTabAreaDocking

    internal class CrownDockTabArea
    {
        #region Field Region

        private readonly Dictionary<CrownDockContent, CrownDockTab> _tabs = new Dictionary<CrownDockContent, CrownDockTab>();

        private readonly List<ToolStripMenuItem> _menuItems = new List<ToolStripMenuItem>();
        private readonly CrownContextMenuStrip _tabMenu = new CrownContextMenuStrip();

        #endregion

        #region Property Region

        public DockArea DockArea { get; private set; }

        public Rectangle ClientRectangle { get; set; }

        public Rectangle DropdownRectangle { get; set; }

        public bool DropdownHot { get; set; }

        public int Offset { get; set; }

        public int TotalTabSize { get; set; }

        public bool Visible { get; set; }

        public CrownDockTab ClickedCloseButton { get; set; }

        #endregion

        #region Constructor Region

        public CrownDockTabArea(DockArea dockArea)
        {
            DockArea = dockArea;
        }

        #endregion

        #region Method Region

        public void ShowMenu(Control control, Point location)
        {
            _tabMenu.Show(control, location);
        }

        public void AddMenuItem(ToolStripMenuItem menuItem)
        {
            _menuItems.Add(menuItem);
            RebuildMenu();
        }

        public void RemoveMenuItem(ToolStripMenuItem menuItem)
        {
            _menuItems.Remove(menuItem);
            RebuildMenu();
        }

        public ToolStripMenuItem GetMenuItem(CrownDockContent content)
        {
            ToolStripMenuItem menuItem = null;
            foreach (ToolStripMenuItem item in _menuItems)
            {
                CrownDockContent menuContent = item.Tag as CrownDockContent;
                if (menuContent == null)
                {
                    continue;
                }

                if (menuContent == content)
                {
                    menuItem = item;
                }
            }

            return menuItem;
        }

        public void RebuildMenu()
        {
            _tabMenu.Items.Clear();

            List<ToolStripMenuItem> orderedItems = new List<ToolStripMenuItem>();

            int index = 0;
            for (int i = 0; i < _menuItems.Count; i++)
            {
                foreach (ToolStripMenuItem item in _menuItems)
                {
                    CrownDockContent content = (CrownDockContent)item.Tag;
                    if (content.Order == index)
                    {
                        orderedItems.Add(item);
                    }
                }
                index++;
            }

            foreach (ToolStripMenuItem item in orderedItems)
            {
                _tabMenu.Items.Add(item);
            }
        }

        #endregion
    }

    #endregion
}