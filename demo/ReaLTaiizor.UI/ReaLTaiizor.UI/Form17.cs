using System;
using System.Text;
using ReaLTaiizor.Util;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Colors;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI
{
    public partial class Form17 : MaterialForm
    {
        private readonly MaterialManager materialManager;

        public Form17()
        {
            InitializeComponent();

            // Initialize MaterialManager
            materialManager = MaterialManager.Instance;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialManager.EnforceBackcolorOnAllComponents = true;

            // MaterialManager properties
            materialManager.AddFormToManage(this);
            materialManager.Theme = MaterialManager.Themes.LIGHT;
            materialManager.ColorScheme = new MaterialColorScheme(MaterialPrimary.Indigo500, MaterialPrimary.Indigo700, MaterialPrimary.Indigo100, MaterialAccent.Pink200, MaterialTextShade.WHITE);

            // Add dummy data to the listview
            seedListView();
            materialCheckListBox11.Items.Add("Item1", false);
            materialCheckListBox11.Items.Add("Item2", true);
            materialCheckListBox11.Items.Add("Item3", true);
            materialCheckListBox11.Items.Add("Item4", false);
            materialCheckListBox11.Items.Add("Item5", true);

            materialComboBox6.SelectedIndex = 0;
        }

        private void seedListView()
        {
            //Define
            string[][] data = new[]
            {
                new [] {"Lollipop", "392", "0.2", "0"},
                new [] {"KitKat", "518", "26.0", "7"},
                new [] {"Ice cream sandwich", "237", "9.0", "4.3"},
                new [] {"Jelly Bean", "375", "0.0", "0.0"},
                new [] {"Honeycomb", "408", "3.2", "6.5"}
            };

            //Add
            foreach (string[] version in data)
            {
                ListViewItem item = new ListViewItem(version);
                materialListView1.Items.Add(item);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            materialManager.Theme = materialManager.Theme == MaterialManager.Themes.DARK ? MaterialManager.Themes.LIGHT : MaterialManager.Themes.DARK;
            updateColor();
        }

        private int colorSchemeIndex;

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2)
            {
                colorSchemeIndex = 0;
            }

            updateColor();
        }

        private void updateColor()
        {
            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialManager.ColorScheme = new MaterialColorScheme(
                        materialManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal500 : MaterialPrimary.Indigo500,
                        materialManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal700 : MaterialPrimary.Indigo700,
                        materialManager.Theme == MaterialManager.Themes.DARK ? MaterialPrimary.Teal200 : MaterialPrimary.Indigo100,
                        MaterialAccent.Pink200,
                        MaterialTextShade.WHITE);
                    break;
                case 1:
                    materialManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Green600,
                        MaterialPrimary.Green700,
                        MaterialPrimary.Green200,
                        MaterialAccent.Red100,
                        MaterialTextShade.WHITE);
                    break;
                case 2:
                    materialManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.BlueGrey800,
                        MaterialPrimary.BlueGrey900,
                        MaterialPrimary.BlueGrey500,
                        MaterialAccent.LightBlue200,
                        MaterialTextShade.WHITE);
                    break;
            }
            Invalidate();
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
            StringBuilder builder = new StringBuilder("Batch operation report:\n\n");
            Random random = new Random();
            int result = 0;

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

            string batchOperationResults = builder.ToString();
            DialogResult mresult = MaterialMessageBox.Show(batchOperationResults, "Batch Operation");
            materialComboBox1.Items.Add("this is a very long string");
        }

        private void MaterialButton25_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Normal;
        }

        private void MaterialButton6_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Fast;
        }
    }
}