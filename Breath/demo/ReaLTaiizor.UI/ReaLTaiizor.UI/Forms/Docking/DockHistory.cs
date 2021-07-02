using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockHistory : CrownToolWindow
    {
        #region Constructor Region

        public DockHistory()
        {
            InitializeComponent();

            // Build dummy list data
            for (int i = 0; i < 100; i++)
            {
                CrownListItem item = new($"List item #{i}");
                lstHistory.Items.Add(item);
            }
        }

        #endregion
    }
}