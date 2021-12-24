using System.Windows.Forms;

namespace ReaLTaiizor.Stopwatch
{
    public partial class Flag : UserControl
    {
        public Flag(int Laps, string Time, string Total)
        {
            InitializeComponent();

            materialLabel1.Text = $"#{string.Format("{0:000}", Laps)}";
            materialLabel2.Text = $"{Time}";
            materialLabel3.Text = $"{Total}";
        }
    }
}