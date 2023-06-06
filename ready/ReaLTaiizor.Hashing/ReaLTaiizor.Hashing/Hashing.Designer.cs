namespace ReaLTaiizor.Hashing
{
    partial class Hashing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hashing));
            this.CopyMD5 = new ReaLTaiizor.Controls.CrownButton();
            this.CopySHA1 = new ReaLTaiizor.Controls.CrownButton();
            this.CopySHA256 = new ReaLTaiizor.Controls.CrownButton();
            this.CopySHA384 = new ReaLTaiizor.Controls.CrownButton();
            this.CopySHA512 = new ReaLTaiizor.Controls.CrownButton();
            this.nightForm1 = new ReaLTaiizor.Forms.NightForm();
            this.Separator = new ReaLTaiizor.Controls.Separator();
            this.HashAsyncFile = new ReaLTaiizor.Controls.CrownButton();
            this.HashFile = new ReaLTaiizor.Controls.CrownButton();
            this.OpenFile = new ReaLTaiizor.Controls.CrownButton();
            this.ResultSHA512 = new ReaLTaiizor.Controls.CrownTextBox();
            this.ResultSHA384 = new ReaLTaiizor.Controls.CrownTextBox();
            this.ResultSHA256 = new ReaLTaiizor.Controls.CrownTextBox();
            this.ResultSHA1 = new ReaLTaiizor.Controls.CrownTextBox();
            this.ResultMD5 = new ReaLTaiizor.Controls.CrownTextBox();
            this.FilePath = new ReaLTaiizor.Controls.CrownTextBox();
            this.nightLabel6 = new ReaLTaiizor.Controls.NightLabel();
            this.nightLabel5 = new ReaLTaiizor.Controls.NightLabel();
            this.nightLabel4 = new ReaLTaiizor.Controls.NightLabel();
            this.nightLabel3 = new ReaLTaiizor.Controls.NightLabel();
            this.nightLabel2 = new ReaLTaiizor.Controls.NightLabel();
            this.nightLabel1 = new ReaLTaiizor.Controls.NightLabel();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.nightForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CopyMD5
            // 
            this.CopyMD5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyMD5.Enabled = false;
            this.CopyMD5.Location = new System.Drawing.Point(732, 90);
            this.CopyMD5.Name = "CopyMD5";
            this.CopyMD5.Padding = new System.Windows.Forms.Padding(5);
            this.CopyMD5.Size = new System.Drawing.Size(48, 23);
            this.CopyMD5.TabIndex = 23;
            this.CopyMD5.Text = "Copy";
            this.CopyMD5.Click += new System.EventHandler(this.CopyMD5_Click);
            // 
            // CopySHA1
            // 
            this.CopySHA1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopySHA1.Enabled = false;
            this.CopySHA1.Location = new System.Drawing.Point(732, 115);
            this.CopySHA1.Name = "CopySHA1";
            this.CopySHA1.Padding = new System.Windows.Forms.Padding(5);
            this.CopySHA1.Size = new System.Drawing.Size(48, 23);
            this.CopySHA1.TabIndex = 24;
            this.CopySHA1.Text = "Copy";
            this.CopySHA1.Click += new System.EventHandler(this.CopySHA1_Click);
            // 
            // CopySHA256
            // 
            this.CopySHA256.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopySHA256.Enabled = false;
            this.CopySHA256.Location = new System.Drawing.Point(732, 140);
            this.CopySHA256.Name = "CopySHA256";
            this.CopySHA256.Padding = new System.Windows.Forms.Padding(5);
            this.CopySHA256.Size = new System.Drawing.Size(48, 23);
            this.CopySHA256.TabIndex = 25;
            this.CopySHA256.Text = "Copy";
            this.CopySHA256.Click += new System.EventHandler(this.CopySHA256_Click);
            // 
            // CopySHA384
            // 
            this.CopySHA384.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopySHA384.Enabled = false;
            this.CopySHA384.Location = new System.Drawing.Point(732, 165);
            this.CopySHA384.Name = "CopySHA384";
            this.CopySHA384.Padding = new System.Windows.Forms.Padding(5);
            this.CopySHA384.Size = new System.Drawing.Size(48, 23);
            this.CopySHA384.TabIndex = 26;
            this.CopySHA384.Text = "Copy";
            this.CopySHA384.Click += new System.EventHandler(this.CopySHA384_Click);
            // 
            // CopySHA512
            // 
            this.CopySHA512.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopySHA512.Enabled = false;
            this.CopySHA512.Location = new System.Drawing.Point(732, 190);
            this.CopySHA512.Name = "CopySHA512";
            this.CopySHA512.Padding = new System.Windows.Forms.Padding(5);
            this.CopySHA512.Size = new System.Drawing.Size(48, 23);
            this.CopySHA512.TabIndex = 27;
            this.CopySHA512.Text = "Copy";
            this.CopySHA512.Click += new System.EventHandler(this.CopySHA512_Click);
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.nightForm1.Controls.Add(this.CopySHA512);
            this.nightForm1.Controls.Add(this.CopySHA384);
            this.nightForm1.Controls.Add(this.CopySHA256);
            this.nightForm1.Controls.Add(this.CopySHA1);
            this.nightForm1.Controls.Add(this.CopyMD5);
            this.nightForm1.Controls.Add(this.Separator);
            this.nightForm1.Controls.Add(this.HashAsyncFile);
            this.nightForm1.Controls.Add(this.HashFile);
            this.nightForm1.Controls.Add(this.OpenFile);
            this.nightForm1.Controls.Add(this.ResultSHA512);
            this.nightForm1.Controls.Add(this.ResultSHA384);
            this.nightForm1.Controls.Add(this.ResultSHA256);
            this.nightForm1.Controls.Add(this.ResultSHA1);
            this.nightForm1.Controls.Add(this.ResultMD5);
            this.nightForm1.Controls.Add(this.FilePath);
            this.nightForm1.Controls.Add(this.nightLabel6);
            this.nightForm1.Controls.Add(this.nightLabel5);
            this.nightForm1.Controls.Add(this.nightLabel4);
            this.nightForm1.Controls.Add(this.nightLabel3);
            this.nightForm1.Controls.Add(this.nightLabel2);
            this.nightForm1.Controls.Add(this.nightLabel1);
            this.nightForm1.Controls.Add(this.nightControlBox1);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = true;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(792, 224);
            this.nightForm1.TabIndex = 0;
            this.nightForm1.Text = "Hashing";
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // Separator
            // 
            this.Separator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Separator.LineColor = System.Drawing.Color.Crimson;
            this.Separator.Location = new System.Drawing.Point(-5, 72);
            this.Separator.Name = "Separator";
            this.Separator.Size = new System.Drawing.Size(810, 10);
            this.Separator.TabIndex = 22;
            this.Separator.Text = "separator1";
            // 
            // HashAsyncFile
            // 
            this.HashAsyncFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HashAsyncFile.Location = new System.Drawing.Point(695, 41);
            this.HashAsyncFile.Name = "HashAsyncFile";
            this.HashAsyncFile.Padding = new System.Windows.Forms.Padding(5);
            this.HashAsyncFile.Size = new System.Drawing.Size(85, 23);
            this.HashAsyncFile.TabIndex = 21;
            this.HashAsyncFile.Text = "Hash Async";
            this.HashAsyncFile.Click += new System.EventHandler(this.HashAsyncFile_Click);
            // 
            // HashFile
            // 
            this.HashFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HashFile.Location = new System.Drawing.Point(641, 41);
            this.HashFile.Name = "HashFile";
            this.HashFile.Padding = new System.Windows.Forms.Padding(5);
            this.HashFile.Size = new System.Drawing.Size(48, 23);
            this.HashFile.TabIndex = 20;
            this.HashFile.Text = "Hash";
            this.HashFile.Click += new System.EventHandler(this.HashFile_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenFile.Location = new System.Drawing.Point(605, 41);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Padding = new System.Windows.Forms.Padding(5);
            this.OpenFile.Size = new System.Drawing.Size(30, 23);
            this.OpenFile.TabIndex = 19;
            this.OpenFile.Text = "•••";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // ResultSHA512
            // 
            this.ResultSHA512.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultSHA512.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.ResultSHA512.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultSHA512.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ResultSHA512.Location = new System.Drawing.Point(62, 190);
            this.ResultSHA512.MaxLength = 2500;
            this.ResultSHA512.Name = "ResultSHA512";
            this.ResultSHA512.ReadOnly = true;
            this.ResultSHA512.Size = new System.Drawing.Size(664, 23);
            this.ResultSHA512.TabIndex = 18;
            this.ResultSHA512.Text = "Result of SHA512";
            this.ResultSHA512.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResultSHA384
            // 
            this.ResultSHA384.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultSHA384.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.ResultSHA384.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultSHA384.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ResultSHA384.Location = new System.Drawing.Point(62, 165);
            this.ResultSHA384.MaxLength = 2500;
            this.ResultSHA384.Name = "ResultSHA384";
            this.ResultSHA384.ReadOnly = true;
            this.ResultSHA384.Size = new System.Drawing.Size(664, 23);
            this.ResultSHA384.TabIndex = 17;
            this.ResultSHA384.Text = "Result of SHA384";
            this.ResultSHA384.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResultSHA256
            // 
            this.ResultSHA256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultSHA256.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.ResultSHA256.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultSHA256.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ResultSHA256.Location = new System.Drawing.Point(62, 140);
            this.ResultSHA256.MaxLength = 2500;
            this.ResultSHA256.Name = "ResultSHA256";
            this.ResultSHA256.ReadOnly = true;
            this.ResultSHA256.Size = new System.Drawing.Size(664, 23);
            this.ResultSHA256.TabIndex = 16;
            this.ResultSHA256.Text = "Result of SHA256";
            this.ResultSHA256.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResultSHA1
            // 
            this.ResultSHA1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultSHA1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.ResultSHA1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultSHA1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ResultSHA1.Location = new System.Drawing.Point(62, 115);
            this.ResultSHA1.MaxLength = 2500;
            this.ResultSHA1.Name = "ResultSHA1";
            this.ResultSHA1.ReadOnly = true;
            this.ResultSHA1.Size = new System.Drawing.Size(664, 23);
            this.ResultSHA1.TabIndex = 15;
            this.ResultSHA1.Text = "Result of SHA1";
            this.ResultSHA1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResultMD5
            // 
            this.ResultMD5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResultMD5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.ResultMD5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultMD5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ResultMD5.Location = new System.Drawing.Point(62, 90);
            this.ResultMD5.MaxLength = 2500;
            this.ResultMD5.Name = "ResultMD5";
            this.ResultMD5.ReadOnly = true;
            this.ResultMD5.Size = new System.Drawing.Size(664, 23);
            this.ResultMD5.TabIndex = 14;
            this.ResultMD5.Text = "Result of MD5";
            this.ResultMD5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FilePath
            // 
            this.FilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.FilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.FilePath.Location = new System.Drawing.Point(62, 41);
            this.FilePath.MaxLength = 2500;
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(537, 23);
            this.FilePath.TabIndex = 13;
            this.FilePath.Text = "File Path";
            this.FilePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nightLabel6
            // 
            this.nightLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nightLabel6.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel6.Location = new System.Drawing.Point(5, 194);
            this.nightLabel6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.nightLabel6.Name = "nightLabel6";
            this.nightLabel6.Size = new System.Drawing.Size(51, 15);
            this.nightLabel6.TabIndex = 6;
            this.nightLabel6.Text = "SHA512:";
            this.nightLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightLabel5
            // 
            this.nightLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nightLabel5.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel5.Location = new System.Drawing.Point(5, 169);
            this.nightLabel5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.nightLabel5.Name = "nightLabel5";
            this.nightLabel5.Size = new System.Drawing.Size(51, 15);
            this.nightLabel5.TabIndex = 5;
            this.nightLabel5.Text = "SHA384:";
            this.nightLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightLabel4
            // 
            this.nightLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nightLabel4.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel4.Location = new System.Drawing.Point(5, 144);
            this.nightLabel4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.nightLabel4.Name = "nightLabel4";
            this.nightLabel4.Size = new System.Drawing.Size(51, 15);
            this.nightLabel4.TabIndex = 4;
            this.nightLabel4.Text = "SHA256:";
            this.nightLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightLabel3
            // 
            this.nightLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nightLabel3.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel3.Location = new System.Drawing.Point(5, 119);
            this.nightLabel3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.nightLabel3.Name = "nightLabel3";
            this.nightLabel3.Size = new System.Drawing.Size(51, 15);
            this.nightLabel3.TabIndex = 3;
            this.nightLabel3.Text = "SHA1:";
            this.nightLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightLabel2
            // 
            this.nightLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nightLabel2.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel2.Location = new System.Drawing.Point(5, 94);
            this.nightLabel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.nightLabel2.Name = "nightLabel2";
            this.nightLabel2.Size = new System.Drawing.Size(51, 15);
            this.nightLabel2.TabIndex = 2;
            this.nightLabel2.Text = "MD5:";
            this.nightLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightLabel1
            // 
            this.nightLabel1.BackColor = System.Drawing.Color.Transparent;
            this.nightLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(118)))), ((int)(((byte)(127)))));
            this.nightLabel1.Location = new System.Drawing.Point(5, 45);
            this.nightLabel1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.nightLabel1.Name = "nightLabel1";
            this.nightLabel1.Size = new System.Drawing.Size(51, 15);
            this.nightLabel1.TabIndex = 1;
            this.nightLabel1.Text = "File:";
            this.nightLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = false;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(653, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 0;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            this.OpenFileDialog.RestoreDirectory = true;
            this.OpenFileDialog.ShowHelp = true;
            // 
            // Hashing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(792, 224);
            this.ControlBox = false;
            this.Controls.Add(this.nightForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1920, 1032);
            this.MinimizeBox = false;
            this.Name = "Hashing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hashing";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.nightForm1.ResumeLayout(false);
            this.nightForm1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.NightForm nightForm1;
        private Controls.NightControlBox nightControlBox1;
        private Controls.NightLabel nightLabel6;
        private Controls.NightLabel nightLabel5;
        private Controls.NightLabel nightLabel4;
        private Controls.NightLabel nightLabel3;
        private Controls.NightLabel nightLabel2;
        private Controls.NightLabel nightLabel1;
        private Controls.CrownTextBox ResultSHA512;
        private Controls.CrownTextBox ResultSHA384;
        private Controls.CrownTextBox ResultSHA256;
        private Controls.CrownTextBox ResultSHA1;
        private Controls.CrownTextBox ResultMD5;
        private Controls.CrownTextBox FilePath;
        private Controls.CrownButton HashAsyncFile;
        private Controls.CrownButton HashFile;
        private Controls.CrownButton OpenFile;
        private Controls.Separator Separator;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private Controls.CrownButton CopyMD5;
        private Controls.CrownButton CopySHA1;
        private Controls.CrownButton CopySHA256;
        private Controls.CrownButton CopySHA384;
        private Controls.CrownButton CopySHA512;
    }
}