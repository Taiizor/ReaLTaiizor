using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReaLTaiizor.Stopwatch
{
    public partial class Stopwatch : Form
    {
        public Stopwatch()
        {
            InitializeComponent();
            parrotSplashScreen1.InitializeLoader(this);
        }

        private void Stopwatch_Load(object sender, EventArgs e)
        {
            tabPage1.Controls.Add(new Area());
        }

        private void ParrotSegment1_IndexChanged(object sender, EventArgs e)
        {
            string[] Items = parrotSegment1.Items.Split(',');
            int Count = Items.Length;

            if (parrotSegment1.SelectedIndex == Count - 1)
            {
                if (Count <= 4)
                {
                    parrotSegment1.Items = parrotSegment1.Items.Replace("Add Tab", $"Tab {Count++},Add Tab");
                }
                else
                {
                    parrotSegment1.Items = parrotSegment1.Items.Replace("Add Tab", $"Tab {Count++}");
                }

                TabPage TP = new() { BackColor = Color.FromArgb(236, 236, 236) };
                TP.Controls.Add(new Area());
                materialTabControl1.TabPages.Add(TP);

            }

            materialTabControl1.SelectedIndex = parrotSegment1.SelectedIndex;
        }
    }
}