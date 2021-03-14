using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Dialogs
{
    public partial class DialogControls : CrownDialog
    {
        public DialogControls()
        {
            InitializeComponent();

            // Build dummy list data
            for (int i = 0; i < 100; i++)
            {
                CrownListItem item = new($"List item #{i}");
                lstTest.Items.Add(item);
            }

            // Build dummy nodes
            int childCount = 0;
            for (int i = 0; i < 20; i++)
            {
                CrownTreeNode node = new($"Root node #{i}")
                {
                    ExpandedIcon = Properties.Resources.folder_16x,
                    Icon = Properties.Resources.folder_Closed_16xLG
                };

                for (int x = 0; x < 10; x++)
                {
                    CrownTreeNode childNode = new($"Child node #{childCount}")
                    {
                        Icon = Properties.Resources.Files_7954
                    };
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