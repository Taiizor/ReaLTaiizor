using System;
using ReaLTaiizor;
using System.Windows.Forms;

namespace ReaLTaiizor_UI
{
    public partial class Form16 : RoyalForm //Summon RoyalForm
    {
        public Form16()
        {
            InitializeComponent();
        }

        private void RoyalButton1_Click(object sender, EventArgs e)
        {
            //                   Form            Text                      Title                         Button                      Icon           Mode
            RoyalMessageBox.Show(this, "This is a test content.", "This is a test caption.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, true);
        }

        private void RoyalButton2_Click(object sender, EventArgs e)
        {
            //                   Form            Text                      Title                         Button                      Icon           Mode
            RoyalMessageBox.Show(this, "This is a test content.", "This is a test caption.", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, false);
        }

        private void RoyalButton3_Click(object sender, EventArgs e)
        {
            //                   Form            Text                      Title                        Button
            RoyalMessageBox.Show(this, "This is a test content.", "This is a test caption.", MessageBoxButtons.RetryCancel);
        }
    }
}