using ReaLTaiizor.Controls;
using ReaLTaiizor.UI.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;
using Panel = ReaLTaiizor.Controls.Panel;

namespace ReaLTaiizor.UI.Forms
{
    public partial class FormManager : Form
    {
        public FormManager()
        {
            InitializeComponent();

            MaximizeBox = false;
            Text = "Form Manager";
            Size = new Size(616, 520);
            BackColor = Color.FromArgb(42, 42, 42);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;

            Panel buttonPanel = new()
            {
                AutoScroll = false,
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };

            FlowLayoutPanel flowLayout = new()
            {
                AutoScroll = false,
                WrapContents = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };

            string[] formNames = {
                "Air", "Dungeon", "Dream", "Ribbon",
                "Space", "Thunder", "Sky", "Moon",
                "Alone", "Fox", "Forever", "Hope",
                "Lost", "Royal", "Material", "Night",
                "Metro", "Poison", "Crown", "Parrot",
                "Cyber", "Form1", "Form2"
            };

            for (int i = 0; i < formNames.Length; i++)
            {
                ForeverButton btn = new()
                {
                    Tag = formNames[i],
                    Text = formNames[i],
                    TextColor = Color.White,
                    Margin = new Padding(10),
                    Size = new Size(180, 40),
                    BaseColor = Color.FromArgb(60, 60, 60)
                };

                btn.Click += Button_Click;
                flowLayout.Controls.Add(btn);
            }

            buttonPanel.Controls.Add(flowLayout);
            Controls.Add(buttonPanel);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is ForeverButton btn)
            {
                Form newForm = FormHelper.Open(new[] { btn.Tag.ToString(), "Manager" });
                newForm.ShowDialog();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "FormManager";
            this.ClientSize = new Size(800, 600);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.ResumeLayout(false);
        }
    }
}