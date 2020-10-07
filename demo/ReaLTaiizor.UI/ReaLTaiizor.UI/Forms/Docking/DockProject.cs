using ReaLTaiizor.Controls;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockProject : ToolWindow
    {
        #region Constructor Region

        public DockProject()
        {
            InitializeComponent();

            // Build dummy nodes
            var childCount = 0;
            for (var i = 0; i < 20; i++)
            {
                var node = new CrownTreeNode($"Root node #{i}");
                node.ExpandedIcon = Properties.Resources.folder_16x;
                node.Icon = Properties.Resources.folder_Closed_16xLG;

                for (var x = 0; x < 10; x++)
                {
                    var childNode = new CrownTreeNode($"Child node #{childCount}");
                    childNode.Icon = Properties.Resources.Files_7954;
                    childCount++;
                    node.Nodes.Add(childNode);
                }

                treeProject.Nodes.Add(node);
            }
        }

        #endregion
    }
}