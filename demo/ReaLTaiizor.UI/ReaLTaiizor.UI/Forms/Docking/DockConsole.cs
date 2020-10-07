using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockConsole : ToolWindow
    {
        #region Constructor Region

        public DockConsole()
        {
            InitializeComponent();

            // Build dummy list data
            for (var i = 0; i < 100; i++)
            {
                var item = new CrownListItem($"List item #{i}");
                lstConsole.Items.Add(item);
            }
        }

        #endregion
    }
}