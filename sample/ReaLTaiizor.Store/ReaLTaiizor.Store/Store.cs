using ReaLTaiizor.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Panel = ReaLTaiizor.Controls.Panel;

namespace ReaLTaiizor.Store
{
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            NightPanel Menu = sender as NightPanel;

            if (Menu.Name == "HomeMenu")
            {
                HomeSeparator.Customization = "08aq/9PGqv/Txqr/08aq/w==";
                Menu.Side = NightPanel.PanelSide.Right;
                Pages.SelectedTab = HomePage;
            }
            else
            {
                HomeSeparator.Customization = "ICAg/yAgIP8gICD/ICAg/w==";
                HomeMenu.Side = NightPanel.PanelSide.Left;
            }

            if (Menu.Name == "AppMenu")
            {
                AppSeparator.Customization = "08aq/9PGqv/Txqr/08aq/w==";
                Menu.Side = NightPanel.PanelSide.Right;
                Pages.SelectedTab = AppPage;
            }
            else
            {
                AppSeparator.Customization = "ICAg/yAgIP8gICD/ICAg/w==";
                AppMenu.Side = NightPanel.PanelSide.Left;
            }

            if (Menu.Name == "GameMenu")
            {
                GameSeparator.Customization = "08aq/9PGqv/Txqr/08aq/w==";
                Menu.Side = NightPanel.PanelSide.Right;
                Pages.SelectedTab = GamePage;
            }
            else
            {
                GameSeparator.Customization = "ICAg/yAgIP8gICD/ICAg/w==";
                GameMenu.Side = NightPanel.PanelSide.Left;
            }

            if (Menu.Name == "LibraryMenu")
            {
                LibrarySeparator.Customization = "08aq/9PGqv/Txqr/08aq/w==";
                Menu.Side = NightPanel.PanelSide.Right;
                Pages.SelectedTab = LibraryPage;
            }
            else
            {
                LibrarySeparator.Customization = "ICAg/yAgIP8gICD/ICAg/w==";
                LibraryMenu.Side = NightPanel.PanelSide.Left;
            }

            if (Menu.Name == "HelpMenu")
            {
                HelpSeparator.Customization = "08aq/9PGqv/Txqr/08aq/w==";
                HelpImage.Image = Properties.Resources.HelpBackground;
                Menu.Side = NightPanel.PanelSide.Right;
                Pages.SelectedTab = HelpPage;
            }
            else
            {
                HelpSeparator.Customization = "ICAg/yAgIP8gICD/ICAg/w==";
                HelpMenu.Side = NightPanel.PanelSide.Left;
            }

            Menu.Refresh();
            Menu.Invalidate();
        }

        private void UserPicture_MouseEnter(object sender, EventArgs e)
        {
            UserPicture.FilterEnabled = true;
        }

        private void UserPicture_MouseLeave(object sender, EventArgs e)
        {
            UserPicture.FilterEnabled = false;
        }

        private void LibraryApp_MouseEnter(object sender, EventArgs e)
        {
            Panel App = sender as Panel;

            App.BackColor = Color.FromArgb(57, 57, 57);
        }

        private void LibraryApp_MouseLeave(object sender, EventArgs e)
        {
            Panel App = sender as Panel;

            App.BackColor = Color.FromArgb(50, 50, 52);
        }

        private void GetHelp_Click(object sender, EventArgs e)
        {
            HelpImage.Image = Properties.Resources.HelpBackground2;

            string Uri = "https://github.com/Taiizor/ReaLTaiizor";

            Process.Start(new ProcessStartInfo
            {
                FileName = Uri,
                UseShellExecute = true
            });

            //try
            //{
            //    Process.Start(Uri);
            //}
            //catch (System.ComponentModel.Win32Exception noBrowser)
            //{
            //    if (noBrowser.ErrorCode == -2147467259)
            //    {
            //        MessageBox.Show(noBrowser.Message);
            //    }
            //}
            //catch (Exception other)
            //{
            //    MessageBox.Show(other.Message);
            //}
        }

        private void PageChanger_Tick(object sender, EventArgs e)
        {
            if (HomePages.SelectedTab == Windows)
            {
                HomePages.SelectedTab = Cloud;
            }
            else
            {
                HomePages.SelectedTab = Windows;
            }

            if (AppPages.SelectedTab == Photo)
            {
                AppPages.SelectedTab = Teams;
            }
            else
            {
                AppPages.SelectedTab = Photo;
            }

            if (GamePages.SelectedTab == Halo)
            {
                GamePages.SelectedTab = Witcher;
            }
            else
            {
                GamePages.SelectedTab = Halo;
            }
        }
    }
}