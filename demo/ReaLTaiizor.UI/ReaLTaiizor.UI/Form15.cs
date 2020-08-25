using System;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using ReaLTaiizor.Forms.Form;

namespace ReaLTaiizor.UI
{
    public partial class Form15 : Lost //Summon LostForm
    {
        public Form15()
        {
            InitializeComponent();
        }

        private void LostAcceptButton1_Click(object sender, EventArgs e)
        {
            FrameLost FL = new FrameLost
            {
                Width = 150
            };
            FL.Present(this, DockStyle.Left);
        }

        private void LostCancelButton1_Click(object sender, EventArgs e)
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