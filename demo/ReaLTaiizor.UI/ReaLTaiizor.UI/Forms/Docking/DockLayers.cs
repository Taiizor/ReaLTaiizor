using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockLayers : CrownToolWindow
    {
        #region Constructor Region

        public DockLayers()
        {
            InitializeComponent();

            // Build dummy list data
            for (int i = 0; i < 100; i++)
            {
                CrownListItem item = new($"List item #{i}")
                {
                    Icon = Properties.Resources.application_16x
                };
                lstLayers.Items.Add(item);
            }

            // Build dropdown list data
            for (int i = 0; i < 5; i++)
            {
                cmbList.Items.Add(new CrownDropDownItem($"Dropdown item #{i}"));
            }
        }

        #endregion
    }
}