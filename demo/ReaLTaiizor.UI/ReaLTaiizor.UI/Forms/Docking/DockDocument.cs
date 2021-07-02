using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Docking.Crown;
using ReaLTaiizor.Enum.Crown;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor.UI.Forms.Docking
{
    public partial class DockDocument : CrownDocument
    {
        #region Constructor Region

        public DockDocument()
        {
            InitializeComponent();

            // Workaround to stop the textbox from highlight all text.
            txtDocument.SelectionStart = txtDocument.Text.Length;

            // Build dummy dropdown data
            cmbOptions.Items.Add(new CrownDropDownItem("25%"));
            cmbOptions.Items.Add(new CrownDropDownItem("50%"));
            cmbOptions.Items.Add(new CrownDropDownItem("100%"));
            cmbOptions.Items.Add(new CrownDropDownItem("200%"));
            cmbOptions.Items.Add(new CrownDropDownItem("300%"));
            cmbOptions.Items.Add(new CrownDropDownItem("400%"));
        }

        public DockDocument(string text, Image icon) : this()
        {
            DockText = text;
            Icon = icon;
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            DialogResult result = CrownMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", DialogButton.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            base.Close();
        }

        #endregion
    }
}