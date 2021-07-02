
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
            this.materialTextBox1 = new ReaLTaiizor.Controls.MaterialTextBox();
            this.nightButton2 = new ReaLTaiizor.Controls.NightButton();
            this.nightButton1 = new ReaLTaiizor.Controls.NightButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nightForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.nightForm1.Controls.Add(this.materialTextBox1);
            this.nightForm1.Controls.Add(this.nightButton2);
            this.nightForm1.Controls.Add(this.nightButton1);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = true;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(500, 94);
            this.nightForm1.TabIndex = 0;
            this.nightForm1.Text = "{0} - Locking Operations";
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialTextBox1.Hint = "Application Password";
            this.materialTextBox1.Location = new System.Drawing.Point(12, 38);
            this.materialTextBox1.MaxLength = 50;
            this.materialTextBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.materialTextBox1.Multiline = false;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Size = new System.Drawing.Size(180, 50);
            this.materialTextBox1.TabIndex = 4;
            this.materialTextBox1.Text = "";
            // 
            // nightButton2
            // 
            this.nightButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nightButton2.BackColor = System.Drawing.Color.Transparent;
            this.nightButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nightButton2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nightButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton2.HoverForeColor = System.Drawing.Color.White;
            this.nightButton2.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.nightButton2.Location = new System.Drawing.Point(353, 38);
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
            this.nightButton2.Text = "CLOSE";
            this.nightButton2.Click += new System.EventHandler(this.NightButton2_Click);
            // 
            // nightButton1
            // 
            this.nightButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nightButton1.BackColor = System.Drawing.Color.Transparent;
            this.nightButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.nightButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nightButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.HoverForeColor = System.Drawing.Color.White;
            this.nightButton1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            this.nightButton1.Location = new System.Drawing.Point(203, 38);
            this.nightButton1.MinimumSize = new System.Drawing.Size(144, 47);
            this.nightButton1.Name = "nightButton1";
            this.nightButton1.NormalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.nightButton1.PressedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(242)))), ((int)(((byte)(93)))), ((int)(((byte)(89)))));
            this.nightButton1.PressedForeColor = System.Drawing.Color.White;
            this.nightButton1.Radius = 1;
            this.nightButton1.Size = new System.Drawing.Size(144, 50);
            this.nightButton1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.nightButton1.TabIndex = 2;
            this.nightButton1.Text = "JUST DO IT";
            this.nightButton1.Click += new System.EventHandler(this.NightButton1_Click);
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
            // AppPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(500, 94);
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
        private System.Windows.Forms.Timer timer1;
        private Controls.NightButton nightButton2;
        private Controls.MaterialTextBox materialTextBox1;
    }
}