using ReaLTaiizor.Forms;
using System.Windows.Forms;

namespace ReaLTaiizor_CR
{
    public partial class Poison : PoisonForm
    {
        public Poison()
        {
            InitializeComponent();
        }

        private void poisonButton1_Click(object sender, System.EventArgs e)
        {
            ReaLTaiizor.Controls.PoisonMessageBox.Show(this, "aaa", "bbb", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
        }
    }
}
