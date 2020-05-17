using ReaLTaiizor;
using System.Windows.Forms;

namespace ReaLTaiizor_UI
{
    public partial class Form15 : LostForm
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void LostAcceptButton1_Click(object sender, System.EventArgs e)
        {
            FrameLost FL = new FrameLost
            {
                Width = 150
            };
            FL.Present(this, DockStyle.Left);
        }

        private void LostCancelButton1_Click(object sender, System.EventArgs e)
        {
            ToolFrameLost TFL = new ToolFrameLost
            {
                Width = 150,
                Text = "ToolFrameLost1"
            };
            TFL.Present(this, DockStyle.Right);
        }
    }
}