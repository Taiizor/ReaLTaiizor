using ReaLTaiizor.Colors;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Material;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
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
            SeedListView();
            MaterialCheckListBox1.Items.Add("Item1", false);
            MaterialCheckListBox1.Items.Add("Item2", true);
            MaterialCheckListBox1.Items.Add("Item3", true);
            MaterialCheckListBox1.Items.Add("Item4", false);
            MaterialCheckListBox1.Items.Add("Item5", true);
            MaterialCheckListBox1.Items.Add("Item6", false);
            MaterialCheckListBox1.Items.Add("Item7", false);

            materialComboBox6.SelectedIndex = 0;

            materialListBoxFormStyle.Clear();
            foreach (string FormStyleItem in System.Enum.GetNames(typeof(FormStyles)))
            {
                materialListBoxFormStyle.AddItem(FormStyleItem);
                if (FormStyleItem == this.FormStyle.ToString())
                {
                    materialListBoxFormStyle.SelectedIndex = materialListBoxFormStyle.Items.Count - 1;
                }
            }

            materialListBoxFormStyle.SelectedIndexChanged += (sender, args) =>
            {
                FormStyles SelectedStyle = (FormStyles)System.Enum.Parse(typeof(FormStyles), args.Text);
                if (this.FormStyle != SelectedStyle)
                {
                    this.FormStyle = SelectedStyle;
                }
            };

            materialMaskedTextBox1.ValidatingType = typeof(short);
        }

        private void SeedListView()
        {
            //Define
            string[][] data = new[]
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
                ListViewItem item = new(version);
                materialListView1.Items.Add(item);
            }
        }

        private void MaterialButton1_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;
            UpdateColor();
        }

        private int colorSchemeIndex;

        private void ColorScheme_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 6)
            {
                colorSchemeIndex = 0;
            }

            UpdateColor();
        }

        private void UpdateColor()
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
                case 3:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Red800,
                        MaterialPrimary.Red900,
                        MaterialPrimary.Red500,
                        MaterialAccent.Green200,
                        MaterialTextShade.WHITE);
                    break;
                case 4:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Yellow800,
                        MaterialPrimary.Yellow900,
                        MaterialPrimary.Yellow500,
                        MaterialAccent.DeepOrange200,
                        MaterialTextShade.WHITE);
                    break;
                case 5:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.DeepOrange800,
                        MaterialPrimary.DeepOrange900,
                        MaterialPrimary.DeepOrange500,
                        MaterialAccent.Yellow200,
                        MaterialTextShade.WHITE);
                    break;
                case 6:
                    materialSkinManager.ColorScheme = new MaterialColorScheme(
                        MaterialPrimary.Lime800,
                        MaterialPrimary.Lime900,
                        MaterialPrimary.Lime500,
                        MaterialAccent.Green200,
                        MaterialTextShade.WHITE);
                    break;
            }
            Invalidate();
        }

        private void MaterialButton2_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Min(materialProgressBar1.Value + 10, 100);
            materialProgressBar2.Value = materialProgressBar1.Value;
        }

        private void MaterialFlatButton4_Click(object sender, EventArgs e)
        {
            materialProgressBar1.Value = Math.Max(materialProgressBar1.Value - 10, 0);
            materialProgressBar2.Value = materialProgressBar1.Value;
        }

        private void MaterialSwitch4_CheckedChanged(object sender, EventArgs e)
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

        private void MaterialSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            DrawerShowIconsWhenHidden = materialSwitch8.Checked;
        }

        private void MaterialButton3_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new("Batch operation report:\n\n");
            Random random = new();
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
            batchOperationResults = "Simple text";
            DialogResult mresult = MaterialMessageBox.Show(batchOperationResults, "Batch Operation", MessageBoxButtons.YesNoCancel, MaterialFlexibleForm.ButtonsPosition.Center);
            materialComboBox1.Items.Add("this is a very long string");
        }

        private void MaterialSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            DrawerAutoShow = materialSwitch9.Checked;
        }

        private void MaterialTextBox2_LeadingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new("Leading Icon Click");
            SnackBarMessage.Show(this);

        }

        private void MaterialButton6_Click(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new("SnackBar started succesfully", "OK", true);
            SnackBarMessage.Show(this);
        }

        private void MaterialSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseAccent = materialSwitch10.Checked;
        }

        private void MaterialSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseTallSize = materialSwitch11.Checked;
        }

        private void MaterialSwitch12_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch12.Checked)
            {
                materialTextBox21.Hint = "Hint text";
            }
            else
            {
                materialTextBox21.Hint = "";
            }
        }

        private void MaterialComboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (materialComboBox7.SelectedIndex == 1)
            {
                materialTextBox21.PrefixSuffix = MaterialTextBoxEdit.PrefixSuffixTypes.Prefix;
            }
            else if (materialComboBox7.SelectedIndex == 2)
            {
                materialTextBox21.PrefixSuffix = MaterialTextBoxEdit.PrefixSuffixTypes.Suffix;
            }
            else
            {
                materialTextBox21.PrefixSuffix = MaterialTextBoxEdit.PrefixSuffixTypes.None;
            }
        }

        private void MaterialSwitch13_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.UseSystemPasswordChar = materialSwitch13.Checked;

        }

        private void MaterialSwitch14_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch14.Checked)
            {
                materialTextBox21.LeadingIcon = Properties.Resources.baseline_fingerprint_black_24dp;
            }
            else
            {
                materialTextBox21.LeadingIcon = null;
            }
        }

        private void MaterialSwitch15_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch15.Checked)
            {
                materialTextBox21.TrailingIcon = Properties.Resources.baseline_build_black_24dp;
            }
            else
            {
                materialTextBox21.TrailingIcon = null;
            }
        }

        private void MaterialTextBox21_LeadingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new("Leading Icon Click");
            SnackBarMessage.Show(this);
        }

        private void MaterialTextBox21_TrailingIconClick(object sender, EventArgs e)
        {
            MaterialSnackBar SnackBarMessage = new("Trailing Icon Click");
            SnackBarMessage.Show(this);
        }

        private void MsReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            materialCheckbox1.ReadOnly = msReadOnly.Checked;
        }

        private void MaterialButton25_Click(object sender, EventArgs e)
        {
            MaterialDialog materialDialog = new(this, "Dialog Title", "Dialogs inform users about a task and can contain critical information, require decisions, or involve multiple tasks.", "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);

            MaterialSnackBar SnackBarMessage = new(result.ToString(), 750);
            SnackBarMessage.Show(this);

        }

        private void MaterialSwitch16_CheckedChanged(object sender, EventArgs e)
        {
            materialTextBox21.ShowAssistiveText = materialSwitch16.Checked;
        }

        private void MaterialButton26_Click(object sender, EventArgs e)
        {
            DrawerNonClickTabPage = new System.Windows.Forms.TabPage[] { tabPage6 };
        }

        private void MaterialButton27_Click(object sender, EventArgs e)
        {
            DrawerNonClickTabPage = Array.Empty<System.Windows.Forms.TabPage>();
        }

        private void MaterialButton28_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Normal;
        }

        private void MaterialButton29_Click(object sender, EventArgs e)
        {
            MaterialAnimations.AnimationRun = MaterialAnimations.AnimationRunType.Fast;
        }

        private void MaterialButton31_Click(object sender, EventArgs e)
        {
            materialTabSelector1.SelectorNonClickTabPage = new System.Windows.Forms.TabPage[] { tabPage9 };
        }

        private void MaterialButton30_Click(object sender, EventArgs e)
        {
            materialTabSelector1.SelectorNonClickTabPage = Array.Empty<System.Windows.Forms.TabPage>();
        }
    }
}