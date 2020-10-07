using ReaLTaiizor.Controls;
using ReaLTaiizor.Child.Crown;

namespace ReaLTaiizor.UI.Forms.Dialogs
{
    public partial class DialogControls : CrownDialog
    {
        public DialogControls()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new CrownListItem($"List item #{i}");
                lstTest.Items.Add(item);
            }

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

                treeTest.Nodes.Add(node);
            }

            // Hook dialog button events
            btnDialog.Click += delegate
            {
                CrownMessageBox.ShowError("This is an error", "Crown Theme");
            };

            btnMessageBox.Click += delegate
            {
                CrownMessageBox.ShowInformation("This is some information, except it is much bigger, so there we go. I wonder how this is going to go. I hope it resizes properly. It probably will.", "Crown Theme");
            };
        }
    }
}
