using ReaLTaiizor.Forms;
using System;
using System.Windows.Forms;

namespace ReaLTaiizor.Instagram
{
    public partial class Instagram : RoyalForm
    {
        public Instagram()
        {
            InitializeComponent();
        }

        private void LostCancelButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }
    }
}