using ReaLTaiizor.Controls;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReaLTaiizor.Portscan
{
    public partial class Portscan : Form
    {
        private bool Start = true;
        private int EIS = 0;
        private double Time = 0;
        private int TTL = 0;
        private Task TASK = null;

        public Portscan()
        {
            InitializeComponent();
        }

        private void Portscan_Load(object sender, EventArgs e)
        {
            try
            {
                Socket UDPC = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
                UDPC.Dispose();
            }
            catch
            {
                UDP.Visible = false;
                TCP.Visible = false;
                DOMAIN.Size = new Size(250, 29);
                ForeverStatusBar INFO = new()
                {
                    Text = "TCP/UDP Information : Selection Disabled Because UDP Is Not Supported!",
                    TextColor = Color.FromArgb(136, 140, 155),
                    TimeColor = Color.DodgerBlue,
                    RectColor = Color.Crimson,
                    TimeFormat = "HH:mm:ss",
                    ShowTimeDate = true,
                };
                Controls.Add(INFO);
                INFO.Dock = DockStyle.Top;
                INFO.Visible = true;
                Height += INFO.Height;
            }
        }

        private void TCP_CheckedChanged(object sender, EventArgs e)
        {
            if (Start)
            {
                UDP.Checked = !TCP.Checked;
            }
        }

        private void UDP_CheckedChanged(object sender, EventArgs e)
        {
            if (Start)
            {
                TCP.Checked = !UDP.Checked;
            }
        }

        private void SFAST_CheckedChanged(object sender)
        {
            if (!Start)
            {
                SFAST.Checked = !SFAST.Checked;
            }
        }

        private void PORT1T_TextChanged(object sender, EventArgs e)
        {
            PORT1T.Text = PORT1T.Text.Replace(" ", "");
            if (PORT1T.Text.Length > 0 && Convert.ToInt32(PORT1T.Text) > 65535)
            {
                PORT1T.Text = "65535";
            }

            if (PORT1T.Text == "0")
            {
                PORT1T.Text = null;
            }
            else if (PORT1T.Text.StartsWith("0"))
            {
                PORT1T.Text = PORT1T.Text.Substring(1, PORT1T.Text.Length - 1);
            }
        }

        private void PORT2T_TextChanged(object sender, EventArgs e)
        {
            PORT2T.Text = PORT2T.Text.Replace(" ", "");
            if (PORT2T.Text.Length > 0 && Convert.ToInt32(PORT2T.Text) > 65535)
            {
                PORT2T.Text = "65535";
            }

            if (PORT2T.Text == "0")
            {
                PORT2T.Text = null;
            }
            else if (PORT2T.Text.StartsWith("0"))
            {
                PORT2T.Text = PORT2T.Text.Substring(1, PORT2T.Text.Length - 1);
            }
        }

        private void SCAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (Start)
                {
                    if (string.IsNullOrEmpty(DOMAIN.Text) || string.IsNullOrWhiteSpace(DOMAIN.Text))
                    {
                        MessageBox.Show("IP Address - Domain is Empty!");
                    }
                    else if (DOMAIN.Text.StartsWith("http"))
                    {
                        MessageBox.Show("Domain Address Does Not Start With 'http://' or 'https://'!");
                    }
                    else if (string.IsNullOrEmpty(PORT1T.Text))
                    {
                        MessageBox.Show("PORT 1 is Empty!");
                    }
                    else if (!string.IsNullOrEmpty(PORT2T.Text) && (PORT1T.Text == PORT2T.Text || Convert.ToInt32(PORT1T.Text) == Convert.ToInt32(PORT2T.Text)))
                    {
                        MessageBox.Show("PORT 1 Cannot Be The Same As PORT 2!");
                    }
                    else if (!string.IsNullOrEmpty(PORT2T.Text) && Convert.ToInt32(PORT1T.Text) > Convert.ToInt32(PORT2T.Text))
                    {
                        MessageBox.Show("PORT 1 Must Be Less Than PORT 2!");
                    }
                    else
                    {
                        FIX(false);
                    }
                }
                else
                {
                    FIX(true);
                }
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message + "\n\n" + Error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void RC(bool EXV = false)
        {
            try
            {
                ROL.Refresh();
                if (ROL.Items.Count > 0)
                {
                    ROL.Items.Clear();
                }

                RCL.Refresh();
                if (RCL.Items.Count > 0)
                {
                    RCL.Items.Clear();
                }
            }
            catch (Exception EX)
            {
                if (EXV)
                {
                    MessageBox.Show(EX.Message + "\n\n" + EX.StackTrace + "\n\n" + EX.Data);
                }
            }
        }

        private void Times_Tick(object sender, EventArgs e)
        {
            try
            {
                Time += 0.1;
                PTIME.Text = "The Passing Time: " + Math.Round(Time, 1) + "s";
                PRT();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message + "\n\n" + Error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void FIX(bool State)
        {
            try
            {
                Start = State;
                DOMAIN.Enabled = State;
                PORT1T.Enabled = State;
                PORT2T.Enabled = State;
                TCP.Enabled = State;
                UDP.Enabled = State;
                if (State)
                {
                    SCAN.ButtonType = HopeButtonType.Primary;
                    SCAN.Text = "START SCAN";
                    SCAN.Refresh();
                    Scanner.CancelAsync();
                    Times.Stop();
                    AOPC.Text = ROL.Items.Count.ToString();
                    KOPC.Text = RCL.Items.Count.ToString();
                }
                else
                {
                    RC();
                    SCAN.Text = "STOP SCAN";
                    SCAN.ButtonType = HopeButtonType.Warning;
                    RESULT.Text = "";
                    AOPC.Text = "0";
                    KOPC.Text = "0";
                    TTL = 0;
                    Time = 0;
                    PTIME.Text = "The Passing Time: 0s";
                    if (string.IsNullOrEmpty(PORT2T.Text))
                    {
                        EIS = 1;
                    }
                    else
                    {
                        EIS = int.Parse(PORT2T.Text) - int.Parse(PORT1T.Text) + 1;
                    }

                    Times.Start();
                    Scanner.RunWorkerAsync();
                }
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message + "\n\n" + Error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void PRT()
        {
            try
            {
                int R = 0;
                if (!string.IsNullOrEmpty(PORT2T.Text))
                {
                    R = EIS - (ROL.Items.Count + RCL.Items.Count); //TTL
                }

                if (R > 0)
                {
                    RESULT.Text = "REMAINING: " + R;
                }
                else
                {
                    RESULT.Text = "";
                    FIX(true);
                }
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message + "\n\n" + Error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private ProtocolType GTU()
        {
            if (TCP.Checked)
            {
                return ProtocolType.Tcp;
            }
            else
            {
                return ProtocolType.Udp;
            }
        }

        private void SCN_S(int P, string IP)
        {
            Socket Socket = null;
            try
            {
                TTL += 1;
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, GTU());
                Socket.Connect(IP, P);
                ROL.Items.Add(P);
            }
            catch
            {
                RCL.Items.Add(P);
            }
            finally
            {
                AOPC.Text = ROL.Items.Count.ToString();
                KOPC.Text = RCL.Items.Count.ToString();
                Socket.Dispose();
                PRT();
            }
        }

        private async void SCN_F(int P, string IP)
        {
            Socket Socket = null;
            try
            {
                TTL += 1;
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, GTU());
                await Socket.ConnectAsync(IP, P);
                ROL.Items.Add(P);
            }
            catch
            {
                RCL.Items.Add(P);
            }
            finally
            {
                AOPC.Text = ROL.Items.Count.ToString();
                KOPC.Text = RCL.Items.Count.ToString();
                Socket.Dispose();
                PRT();
            }
        }

        private void Scanner_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(PORT2T.Text))
                {
                    if (SFAST.Checked)
                    {
                        SCN_F(Convert.ToInt32(PORT1T.Text), DOMAIN.Text);
                    }
                    else
                    {
                        SCN_S(Convert.ToInt32(PORT1T.Text), DOMAIN.Text);
                    }
                }
                else
                {
                    int E = int.Parse(PORT2T.Text);
                    if (SFAST.Checked)
                    {
                        Parallel.For(int.Parse(PORT1T.Text), ++E, C =>
                        {
                            int P = C;
                            TASK = new Task(delegate ()
                            {
                                SCN_F(P, DOMAIN.Text);
                            });
                            TASK.Start();
                        });
                    }
                    else
                    {
                        Parallel.For(int.Parse(PORT1T.Text), ++E, C =>
                        {
                            int P = C;
                            TASK = new Task(delegate ()
                            {
                                SCN_S(P, DOMAIN.Text);
                            });
                            TASK.Start();
                        });
                    }
                    /*
                        for (int C = Int32.Parse(PORT1T.Text); C <= Int32.Parse(PORT2T.Text); C++)
                        {
                            if (!Start)
                            {
                                int P = C;
                                Task TASK = new Task(delegate ()
                                {
                                    if (SFAST.Checked)
                                        SCN_F(P, DOMAIN.Text);
                                    else
                                        SCN_S(P, DOMAIN.Text);
                                });
                                TASK.Start();
                            }
                        }
                    */
                    Task.WaitAll(TASK);
                }
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message + "\n\n" + Error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}