using System;
using System.Windows.Forms;
using ReaLTaiizor.Forms.Form;

namespace ReaLTaiizor.Instagram
{
    public partial class Instagram : Royal
    {
        public Instagram()
        {
            InitializeComponent();
        }

        private void LostCancelButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}