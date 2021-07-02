using ReaLTaiizor.Enum.Metro;
using ReaLTaiizor.Forms;

namespace ReaLTaiizor.Rufus
{
    public partial class Rufus : MetroForm
    {
        public Rufus()
        {
            InitializeComponent();
            metroComboBox1.SelectedIndex = 0;
            metroComboBox2.SelectedIndex = 1;
            metroComboBox3.SelectedIndex = 0;
            metroComboBox4.SelectedIndex = 0;
            metroTextBox1.Text = metroComboBox1.SelectedItem.ToString().Split(' ')[0];
            metroComboBox5.SelectedIndex = 0;
            metroComboBox6.SelectedIndex = 0;
        }

        private void MetroSwitch1_SwitchedChanged(object sender)
        {
            if (metroSwitch1.Switched)
            {
                metroStyleManager1.Style = Style.Dark;
            }
            else
            {
                metroStyleManager1.Style = Style.Light;
            }
        }
    }
}