using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    public partial class Form20 : PoisonForm
    {
        public Form20()
        {
            InitializeComponent();

            BorderStyle = Enum.Poison.FormBorderStyle.FixedSingle;
            ShadowType = FormShadowType.AeroShadow;

            DataTable _table = new();
            _table.ReadXml(Application.StartupPath + @"\Data\Books.xml");
            poisonGrid1.DataSource = _table;

            poisonGrid1.Font = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
            poisonGrid1.AllowUserToAddRows = false;

            poisonComboBox4.DataSource = _table;
            poisonComboBox4.ValueMember = "Id";
            poisonComboBox4.DisplayMember = "title";
        }

        private void poisonTileSwitch_Click(object sender, EventArgs e)
        {
            Random m = new();
            int next = m.Next(0, 13);
            poisonStyleManager.Style = (ColorStyle)next;
        }

        private void poisonTile1_Click(object sender, EventArgs e)
        {
            poisonStyleManager.Theme = poisonStyleManager.Theme == ThemeStyle.Light ? ThemeStyle.Dark : ThemeStyle.Light;
        }

        private void poisonButton1_Click(object sender, EventArgs e)
        {
            PoisonTaskWindow.ShowTaskWindow(this, "SubControl in TaskWindow", new TaskWindowControl(), 10);
        }

        private void poisonButton2_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "Do you like this poison message box?", "Poison Title", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
        }

        private void poisonButton5_Click(object sender, EventArgs e)
        {
            poisonContextMenu1.Show(poisonButton5, new Point(0, poisonButton5.Height));
        }

        private void poisonButton6_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `OK` only button", "PoisonMessageBox", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void poisonButton10_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `OK` and `Cancel` button", "PoisonMessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void poisonButton7_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `Yes` and `No` button", "PoisonMessageBox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void poisonButton8_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `Yes`, `No` and `Cancel` button", "PoisonMessageBox", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        private void poisonButton11_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `Retry` and `Cancel` button.  With warning style.", "PoisonMessageBox", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
        }

        private void poisonButton9_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample PoisonMessageBox `Abort`, `Retry` and `Ignore` button.  With Error style.", "PoisonMessageBox", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
        }

        private void poisonButton12_Click(object sender, EventArgs e)
        {
            PoisonMessageBox.Show(this, "This is a sample `default` PoisonMessageBox ", "PoisonMessageBox");
        }

        private void poisonButton4_Click(object sender, EventArgs e)
        {
            poisonTextBox2.Focus();
        }
    }
}