using System.Windows.Forms;

namespace ReaLTaiizor.Stopwatch
{
    public partial class Head : UserControl
    {
        public Head(string Laps, string Time, string Total)
        {
            InitializeComponent();

            materialLabel1.Text = $"{Laps}";
            materialLabel2.Text = $"{Time}";
            materialLabel3.Text = $"{Total}";
        }
    }
}