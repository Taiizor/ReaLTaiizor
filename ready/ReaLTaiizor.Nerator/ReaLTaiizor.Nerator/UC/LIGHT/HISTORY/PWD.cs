using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Nerator.CS;
using System;
using System.Windows.Forms;
using static ReaLTaiizor.Colors.PoisonColors;
using static ReaLTaiizor.Nerator.CS.History;
using static ReaLTaiizor.Nerator.CS.Strength;

namespace ReaLTaiizor.Nerator.UC.LIGHT.HISTORY
{
    public partial class PWD : UserControl
    {
        private bool CheckRemove = true;

        public PWD(string Password = null, string Time = null, string Date = null, bool Check = true)
        {
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrEmpty(Time) && !string.IsNullOrWhiteSpace(Time) && !string.IsNullOrEmpty(Date) && !string.IsNullOrWhiteSpace(Date))
            {
                InitializeComponent();

                PASSWORD.Text = Password;
                PASSWORD.Style = StyleMode(StrengthMode(CheckScore2(Password)));
                PASSWORD.ForeColor = PASSWORD.Style switch
                {
                    ColorStyle.Red => Red,
                    ColorStyle.Orange => Orange,
                    ColorStyle.Yellow => Yellow,
                    ColorStyle.Blue => Blue,
                    ColorStyle.Green => Green,
                    _ => White,
                };
                TIMEDATE.Text = Time + "\n" + Date;
                CheckRemove = Check;
            }
            else
            {
                Dispose();
            }
        }

        private void COPY_Click(object sender, EventArgs e)
        {
            IDataObject IData = Clipboard.GetDataObject();
            string Board = string.Empty;

            if (IData.GetDataPresent(DataFormats.Text))
            {
                Board = IData.GetData(DataFormats.Text) as string;
            }

            if (Board != PASSWORD.Text)
            {
                Clipboard.SetDataObject(PASSWORD.Text, true);
                if (PASSWORD.Text == Board)
                {
                    Status.Message = PASSWORD.Text + " copied!";
                }
                else
                {
                    Status.Message = PASSWORD.Text + " could not be copied!";
                }
            }
        }

        private void REMOVE_Click(object sender, EventArgs e)
        {
            if (CheckRemove)
            {
                CheckRemove = false;
                Status.Message = "Click again to delete from history.";
            }
            else
            {
                if (Remove(HistoryFileName, PASSWORD.Text))
                {
                    Status.Message = PASSWORD.Text + " deleted!";
                    Dispose(); //Hide - Visible - etc.
                }
                else
                {
                    Status.Message = PASSWORD.Text + " could not be deleted!";
                }
            }
        }
    }
}