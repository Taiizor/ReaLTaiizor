using System.Windows.Forms;

namespace ReaLTaiizor_UI
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            try
            {
                InitializeComponent();
            }
            catch (System.Exception Hata)
            {
                MessageBox.Show(Hata.Message + "\n\n\n" + Hata.StackTrace + "\n\n\n" + Hata.Source + "\n" + Hata.TargetSite + "\n" + Hata.Data + "\n" + Hata.HResult + "\n" + Hata.HelpLink);
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}