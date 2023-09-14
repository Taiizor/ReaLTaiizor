using ReaLTaiizor.Enum.Cyber;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ReaLTaiizor.UI
{
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message msg = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref msg);
        }

        private void CyberColorPicker1_ColorChanged(Color color)
        {
            cyberButton1.ColorBackground_Pen = color;
            cyberCheckBox1.ColorBackground_Pen = color;
            cyberCheckBox1.ColorChecked = color;
            cyberRadioButton1.ColorBackground_Pen = color;
            cyberRadioButton1.ColorChecked = color;
            cyberProgressBar1.ColorProgressBar = color;
            cyberProgressBar1.ColorBackground_Pen = color;
            cyberScrollBar1.ColorBackground_Pen = color;
            cyberScrollBar1.ColorScrollBar = color;
            cyberGroupBox1.ColorBackground_Pen = color;
            cyberRichTextBox1.ColorBackground_Pen = color;
            cyberTextBox1.ColorBackground_Pen = color;
            cyberTextBox2.ColorBackground_Pen = color;
            cyberSwitch1.ColorBackground_Pen = color;
            cyberSwitch2.ColorBackground_Pen = color;
            cyberSwitch3.ColorBackground_Pen = color;
        }

        private void CyberSwitch1_CheckedChanged()
        {
            if (cyberSwitch1.Checked)
            {
                new Thread(delegate ()
                {
                    int tmp_sleep = 150;
                    cyberButton1.Invoke((MethodInvoker)delegate { cyberButton1.CyberButtonStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberCheckBox1.Invoke((MethodInvoker)delegate { cyberCheckBox1.CyberCheckBoxStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberRadioButton1.Invoke((MethodInvoker)delegate { cyberRadioButton1.CyberRadioButtonStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberProgressBar1.Invoke((MethodInvoker)delegate { cyberProgressBar1.CyberProgressBarStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberScrollBar1.Invoke((MethodInvoker)delegate { cyberScrollBar1.CyberScrollBarStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberGroupBox1.Invoke((MethodInvoker)delegate { cyberGroupBox1.CyberGroupBoxStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberRichTextBox1.Invoke((MethodInvoker)delegate { cyberRichTextBox1.CyberRichTextBoxStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberTextBox1.Invoke((MethodInvoker)delegate { cyberTextBox1.CyberTextBoxStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberTextBox2.Invoke((MethodInvoker)delegate { cyberTextBox2.CyberTextBoxStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberSwitch2.Invoke((MethodInvoker)delegate { cyberSwitch2.CyberSwitchStyle = StateStyle.Random; });
                    Thread.Sleep(tmp_sleep);
                    cyberSwitch3.Invoke((MethodInvoker)delegate { cyberSwitch3.CyberSwitchStyle = StateStyle.Random; });
                }).Start();
            }
            else
            {
                new Thread(delegate ()
                {
                    int tmp_sleep = 100;
                    cyberButton1.Invoke((MethodInvoker)delegate { cyberButton1.CyberButtonStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberCheckBox1.Invoke((MethodInvoker)delegate { cyberCheckBox1.CyberCheckBoxStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberRadioButton1.Invoke((MethodInvoker)delegate { cyberRadioButton1.CyberRadioButtonStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberProgressBar1.Invoke((MethodInvoker)delegate { cyberProgressBar1.CyberProgressBarStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberScrollBar1.Invoke((MethodInvoker)delegate
                    {
                        cyberScrollBar1.CyberScrollBarStyle = StateStyle.Default; cyberScrollBar1.OrientationValue = Orientation.Horizontal;
                    });
                    Thread.Sleep(tmp_sleep);
                    cyberGroupBox1.Invoke((MethodInvoker)delegate { cyberGroupBox1.CyberGroupBoxStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberRichTextBox1.Invoke((MethodInvoker)delegate { cyberRichTextBox1.CyberRichTextBoxStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberTextBox1.Invoke((MethodInvoker)delegate { cyberTextBox1.CyberTextBoxStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberTextBox2.Invoke((MethodInvoker)delegate { cyberTextBox2.CyberTextBoxStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberSwitch2.Invoke((MethodInvoker)delegate { cyberSwitch2.CyberSwitchStyle = StateStyle.Default; });
                    Thread.Sleep(tmp_sleep);
                    cyberSwitch3.Invoke((MethodInvoker)delegate { cyberSwitch3.CyberSwitchStyle = StateStyle.Default; });
                }).Start();
            }
        }

        private void CyberSwitch2_CheckedChanged()
        {
            bool tmp = cyberSwitch2.Checked;
            new Thread(delegate ()
            {
                cyberButton1.Invoke((MethodInvoker)delegate { cyberButton1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberCheckBox1.Invoke((MethodInvoker)delegate { cyberCheckBox1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberRadioButton1.Invoke((MethodInvoker)delegate { cyberRadioButton1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberProgressBar1.Invoke((MethodInvoker)delegate { cyberProgressBar1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberScrollBar1.Invoke((MethodInvoker)delegate { cyberScrollBar1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberGroupBox1.Invoke((MethodInvoker)delegate { cyberGroupBox1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberRichTextBox1.Invoke((MethodInvoker)delegate { cyberRichTextBox1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberTextBox1.Invoke((MethodInvoker)delegate { cyberTextBox1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberTextBox2.Invoke((MethodInvoker)delegate { cyberTextBox2.RGB = tmp; });
                Thread.Sleep(1000);
                cyberSwitch1.Invoke((MethodInvoker)delegate { cyberSwitch1.RGB = tmp; });
                Thread.Sleep(1000);
                cyberSwitch2.Invoke((MethodInvoker)delegate { cyberSwitch2.RGB = tmp; });
                Thread.Sleep(1000);
                cyberSwitch3.Invoke((MethodInvoker)delegate { cyberSwitch3.RGB = tmp; });
            }).Start();
        }

        private void CyberSwitch3_CheckedChanged()
        {
            cyberController1.Status = cyberSwitch3.Checked;
        }

        private void CyberScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            cyberProgressBar1.Value = cyberScrollBar1.Value;
        }
    }
}