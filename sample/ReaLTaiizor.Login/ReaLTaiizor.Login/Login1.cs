using ReaLTaiizor.Forms;
using System;

namespace ReaLTaiizor.Login
{
    public partial class Login1 : CrownForm
    {
        public Login1()
        {
            InitializeComponent();
        }

        private void MaterialLabel1_Click(object sender, EventArgs e)
        {
            hopeSwitch1.Checked = !hopeSwitch1.Checked;
        }
    }
}