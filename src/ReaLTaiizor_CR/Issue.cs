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
#if NET40
                materialTabSelector1.SelectorHideTabName = new string[0];
#else
                materialTabSelector1.SelectorHideTabName = Array.Empty<string>();
#endif
            }
            else
            {
                materialTabSelector1.SelectorHideTabName = new string[] { tabPage2.Name };
            }


            if (DrawerHideTabName.Any())
            {
#if NET40
                DrawerHideTabName = new string[0];
#else
                DrawerHideTabName = Array.Empty<string>();
#endif
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
#if NET40
                materialTabSelector1.SelectorNonClickTabPage = new TabPage[0];
#else
                materialTabSelector1.SelectorNonClickTabPage = Array.Empty<TabPage>();
#endif
            }
            else
            {
                materialTabSelector1.SelectorNonClickTabPage = new TabPage[] { tabPage3 };
            }


            if (DrawerNonClickTabPage.Any())
            {
#if NET40
                DrawerNonClickTabPage = new TabPage[0];
#else
                DrawerNonClickTabPage = Array.Empty<TabPage>();
#endif
            }
            else
            {
                DrawerNonClickTabPage = new TabPage[] { tabPage3 };
            }
        }
    }
}
