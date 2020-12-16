
namespace ReaLTaiizor.AppLocker
{
    partial class AppPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nightForm1 = new ReaLTaiizor.Forms.NightForm();
            this.nightButton1 = new ReaLTaiizor.Controls.NightButton();
            this.nightTextBox1 = new ReaLTaiizor.Controls.NightTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nightButton2 = new ReaLTaiizor.Controls.NightButton();
            this.nightForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.nightForm1.Controls.Add(this.nightButton2);
            this.nightForm1.Controls.Add(this.nightButton1);
            this.nightForm1.Controls.Add(this.nightTextBox1);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = true;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(539, 94);
            this.nightForm1.TabIndex = 0;
            this.nightForm1.Text = "nightForm1";
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // nightButton1
            // 
            this.nightButton1.BackColor = System.Drawing.Color.Transparent;
            this.nightButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nightButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nightButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.HoverForeColor = System.Drawing.Color.White;
            this.nightButton1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.nightButton1.Location = new System.Drawing.Point(154, 34);
            this.nightButton1.MinimumSize = new System.Drawing.Size(144, 47);
            this.nightButton1.Name = "nightButton1";
            this.nightButton1.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.nightButton1.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.PressedForeColor = System.Drawing.Color.White;
            this.nightButton1.Radius = 20;
            this.nightButton1.Size = new System.Drawing.Size(144, 50);
            this.nightButton1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.nightButton1.TabIndex = 2;
            this.nightButton1.Text = "nightButton1";
            this.nightButton1.Click += new System.EventHandler(this.NightButton1_Click);
            // 
            // nightTextBox1
            // 
            this.nightTextBox1.ActiveBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.nightTextBox1.BaseBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(48)))), ((int)(((byte)(67)))));
            this.nightTextBox1.ColorBordersOnEnter = true;
            this.nightTextBox1.DisableBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(80)))));
            this.nightTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nightTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(131)))), ((int)(((byte)(140)))));
            this.nightTextBox1.Image = null;
            this.nightTextBox1.Location = new System.Drawing.Point(3, 34);
            this.nightTextBox1.MaxLength = 32767;
            this.nightTextBox1.Multiline = false;
            this.nightTextBox1.Name = "nightTextBox1";
            this.nightTextBox1.ReadOnly = false;
            this.nightTextBox1.ShortcutsEnabled = true;
            this.nightTextBox1.ShowBottomBorder = true;
            this.nightTextBox1.ShowTopBorder = true;
            this.nightTextBox1.Size = new System.Drawing.Size(145, 50);
            this.nightTextBox1.TabIndex = 1;
            this.nightTextBox1.Text = "nightTextBox1";
            this.nightTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.nightTextBox1.UseSystemPasswordChar = false;
            this.nightTextBox1.Watermark = "";
            this.nightTextBox1.WatermarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(120)))), ((int)(((byte)(129)))));
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // nightButton2
            // 
            this.nightButton2.BackColor = System.Drawing.Color.Transparent;
            this.nightButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nightButton2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nightButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.HoverForeColor = System.Drawing.Color.White;
            this.nightButton2.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.nightButton2.Location = new System.Drawing.Point(304, 34);
            this.nightButton2.MinimumSize = new System.Drawing.Size(144, 47);
            this.nightButton2.Name = "nightButton2";
            this.nightButton2.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.nightButton2.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.PressedForeColor = System.Drawing.Color.White;
            this.nightButton2.Radius = 20;
            this.nightButton2.Size = new System.Drawing.Size(144, 50);
            this.nightButton2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.nightButton2.TabIndex = 3;
            this.nightButton2.Text = "nightButton2";
            this.nightButton2.Click += new System.EventHandler(this.NightButton2_Click);
            // 
            // AppPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 94);
            this.Controls.Add(this.nightForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1366, 768);
            this.Name = "AppPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppPassword";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppPassword_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AppPassword_FormClosed);
            this.nightForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.NightForm nightForm1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Controls.NightButton nightButton1;
        private Controls.NightTextBox nightTextBox1;
        private System.Windows.Forms.Timer timer1;
        private Controls.NightButton nightButton2;
    }
}