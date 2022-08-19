using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor_CR
{
    public partial class Poison : PoisonForm
    {
        public Poison()
        {
            InitializeComponent();
        }

        private void poisonButton1_Click(object sender, System.EventArgs e)
        {
            ReportError(LogType.A, "Title", "Message", this, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Hand, null);
        }

        private void poisonButton2_Click(object sender, System.EventArgs e)
        {
            PoisonTaskWindow PTW = new()
            {
                Text = "Test",

                Movable = true,
                Resizable = false,
                MaximizeBox = false,
                MinimizeBox = false,

                StartLocation = false,
                StartPosition = FormStartPosition.CenterScreen,

                Theme = ThemeStyle.Dark,
                Style = ColorStyle.Magenta,

                CustomSize = true,
                Size = new Size(300, 75)
            };

            PTW.Show();
        }

        public enum LogType
        {
            A,
            B,
            C
        }

        public static string WriteLog(LogType _action, string _message, object[] _arguments = null)
        {
            return _message;
        }

        public static void ReportError(LogType _action, string _title, string _message, Form _parent, MessageBoxButtons _buttons, MessageBoxIcon _icon, object[] _arguments = null)
        {
            int index = 0;

            if (_arguments != null)
            {
                foreach (string arg in _arguments)
                {
                    _arguments[index++] = arg.Replace("\r\n", string.Empty);
                }
            }

            _message = WriteLog(_action, _message, _arguments);

            DialogResult Result = PoisonMessageBox.Show(_parent, _message, _title, _buttons, _icon);
            MessageBox.Show(Result.ToString());
        }
    }
}