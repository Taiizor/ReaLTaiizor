using ReaLTaiizor.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ReaLTaiizor_CR
{
    public partial class Issue : MaterialForm
    {
        public Issue()
        {
            InitializeComponent();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            if (materialTabSelector1.SelectorHideTabName.Any())
            {
                materialTabSelector1.SelectorHideTabName = Array.Empty<string>();
            }
            else
            {
                materialTabSelector1.SelectorHideTabName = new string[] { tabPage2.Name };
            }


            if (DrawerHideTabName.Any())
            {
                DrawerHideTabName = Array.Empty<string>();
            }
            else
            {
                DrawerHideTabName = new string[] { tabPage2.Name };
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            if (materialTabSelector1.SelectorNonClickTabPage.Any())
            {
                materialTabSelector1.SelectorNonClickTabPage = Array.Empty<TabPage>();
            }
            else
            {
                materialTabSelector1.SelectorNonClickTabPage = new TabPage[] { tabPage3 };
            }


            if (DrawerNonClickTabPage.Any())
            {
                DrawerNonClickTabPage = Array.Empty<TabPage>();
            }
            else
            {
                DrawerNonClickTabPage = new TabPage[] { tabPage3 };
            }
        }
    }
}
