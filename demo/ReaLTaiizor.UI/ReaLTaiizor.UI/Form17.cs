﻿using System;
using System.Text;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    public partial class Form17 : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public Form17()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.Indigo500, MaterialPrimary.Indigo700, MaterialPrimary.Indigo100, MaterialAccent.Pink200, MaterialTextShade.WHITE);

            // Add dummy data to the listview
            seedListView();
            materialCheckedListBox1.Items.Add("Item1", false);
            materialCheckedListBox1.Items.Add("Item2", true);
            materialCheckedListBox1.Items.Add("Item3", true);
            materialCheckedListBox1.Items.Add("Item4", false);
            materialCheckedListBox1.Items.Add("Item5", true);

            materialComboBox6.SelectedIndex = 0;
        }

        private void seedListView()
        {
            //Define
            var data = new[]
            {
                new []{"Lollipop", "392", "0.2", "0"},
                new []{"KitKat", "518", "26.0", "7"},
                new []{"Ice cream sandwich", "237", "9.0", "4.3"},
                new []{"Jelly Bean", "375", "0.0", "0.0"},
                new []{"Honeycomb", "408", "3.2", "6.5"}
            };

            //Add
            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
                materialListView1.Items.Add(item);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
            updateColor();
        }

        private int colorSchemeIndex;

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2)
                colorSchemeIndex = 0;
            updateColor();
        }

        private void updateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal500 : MaterialPrimary.Indigo500,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal700 : MaterialPrimary.Indigo700,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialPrimary.Teal200 : MaterialPrimary.Indigo100,
                        MaterialAccent.Pink200,
                        MaterialTextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Green600,
                        MaterialPrimary.Green700,
                        MaterialPrimary.Green200,
                        MaterialAccent.Red100,
                        MaterialTextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.WHITE);
                    break;
            }
            Invalidate();
            foreach (System.Windows.Forms.TabPage TP in materialTabControl1.TabPages)
            {
                if (materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
                    TP.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
                else
                    TP.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            }
            foreach (System.Windows.Forms.TabPage TP in materialTabControl2.TabPages)
            {
                if (materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
                    TP.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
                else
                    TP.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            }
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Min(materialProgressBar1.Value + 10, 100);
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Max(materialProgressBar1.Value - 10, 0);
        }

        private void materialSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            DrawerUseColors = materialSwitch4.Checked;
        }

        private void MaterialSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            DrawerHighlightWithAccent = materialSwitch5.Checked;
        }

        private void MaterialSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            DrawerBackgroundWithAccent = materialSwitch6.Checked;
        }

        private void materialSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            DrawerShowIconsWhenHidden = materialSwitch8.Checked;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            var builder = new StringBuilder("Batch operation report:\n\n");
            var random = new Random();
            var result = 0;

            for (int i = 0; i < 200; i++)
            {
                result = random.Next(1000);

                if (result < 950)
                {
                    builder.AppendFormat(" - Task {0}: Operation completed sucessfully.\n", i);
                }
                else
                {
                    builder.AppendFormat(" - Task {0}: Operation failed! A very very very very very very very very very very very very serious error has occured during this sub-operation. The errorcode is: {1}).\n", i, result);
                }
            }

            var batchOperationResults = builder.ToString();
            var mresult = MaterialMessageBox.Show(batchOperationResults, "Batch Operation");
            materialComboBox1.Items.Add("this is a very long string");
        }
    }
}