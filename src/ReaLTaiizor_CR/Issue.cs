using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaLTaiizor_CR
{
    public partial class Issue : MaterialForm
    {
        public Issue()
        {
            InitializeComponent();
        }

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            if (materialButton1.Text == "TEST")
            {
                materialButton1.Text = "TESTER MORUQ";
            }
            else
            {
                materialButton1.Text = "TEST";
            }
        }
    }
}
