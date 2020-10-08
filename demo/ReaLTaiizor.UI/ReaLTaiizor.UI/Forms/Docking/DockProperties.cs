using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockProperties : CrownToolWindow
    {
        #region Constructor Region

        public DockProperties()
        {
            InitializeComponent();

            panel1.BackColor = panel2.BackColor = panel3.BackColor = System.Drawing.Color.Transparent;

            // Build dummy dropdown data
            cmbList.Items.Add(new CrownDropDownItem("Item1"));
            cmbList.Items.Add(new CrownDropDownItem("Item2"));
            cmbList.Items.Add(new CrownDropDownItem("Item3"));
            cmbList.Items.Add(new CrownDropDownItem("Item4"));
            cmbList.Items.Add(new CrownDropDownItem("Item5"));
            cmbList.Items.Add(new CrownDropDownItem("Item6"));

            cmbList.SelectedItemChanged += delegate { System.Console.WriteLine($"Item changed to {cmbList.SelectedItem.Text}"); };
        }

        #endregion
    }
}