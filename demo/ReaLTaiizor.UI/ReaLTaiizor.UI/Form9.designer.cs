namespace ReaLTaiizor.UI
{
    partial class Form9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form9));
            skyStatusBar1 = new ReaLTaiizor.Controls.SkyStatusBar();
            skyNumeric1 = new ReaLTaiizor.Controls.SkyNumeric();
            skyLabel1 = new ReaLTaiizor.Controls.SkyLabel();
            skyTextBox1 = new ReaLTaiizor.Controls.SkyTextBox();
            skyComboBox1 = new ReaLTaiizor.Controls.SkyComboBox();
            skyCheckBox1 = new ReaLTaiizor.Controls.SkyCheckBox();
            skyRadioButton2 = new ReaLTaiizor.Controls.SkyRadioButton();
            skyRadioButton1 = new ReaLTaiizor.Controls.SkyRadioButton();
            skyButton1 = new ReaLTaiizor.Controls.SkyButton();
            SuspendLayout();
            // 
            // skyStatusBar1
            // 
            skyStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            skyStatusBar1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyStatusBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyStatusBar1.Location = new System.Drawing.Point(0, 107);
            skyStatusBar1.Name = "skyStatusBar1";
            skyStatusBar1.Size = new System.Drawing.Size(263, 23);
            skyStatusBar1.TabIndex = 8;
            skyStatusBar1.Text = "skyStatusBar1";
            // 
            // skyNumeric1
            // 
            skyNumeric1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            skyNumeric1.Cursor = System.Windows.Forms.Cursors.IBeam;
            skyNumeric1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyNumeric1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyNumeric1.Location = new System.Drawing.Point(174, 65);
            skyNumeric1.Maximum = ((long)(100));
            skyNumeric1.Minimum = ((long)(0));
            skyNumeric1.Name = "skyNumeric1";
            skyNumeric1.Size = new System.Drawing.Size(75, 30);
            skyNumeric1.TabIndex = 7;
            skyNumeric1.Text = "skyNumeric1";
            skyNumeric1.Value = ((long)(50));
            // 
            // skyLabel1
            // 
            skyLabel1.AutoSize = true;
            skyLabel1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyLabel1.Location = new System.Drawing.Point(194, 41);
            skyLabel1.Name = "skyLabel1";
            skyLabel1.Size = new System.Drawing.Size(55, 12);
            skyLabel1.TabIndex = 6;
            skyLabel1.Text = "skyLabel1";
            // 
            // skyTextBox1
            // 
            skyTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
            skyTextBox1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyTextBox1.Location = new System.Drawing.Point(174, 12);
            skyTextBox1.MaxLength = 32767;
            skyTextBox1.MultiLine = false;
            skyTextBox1.Name = "skyTextBox1";
            skyTextBox1.Size = new System.Drawing.Size(75, 21);
            skyTextBox1.TabIndex = 5;
            skyTextBox1.Text = "skyTextBox1";
            skyTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            skyTextBox1.UseSystemPasswordChar = false;
            // 
            // skyComboBox1
            // 
            skyComboBox1.BackColor = System.Drawing.Color.Transparent;
            skyComboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            skyComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            skyComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            skyComboBox1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyComboBox1.FormattingEnabled = true;
            skyComboBox1.ItemHeight = 16;
            skyComboBox1.ItemHighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(176)))), ((int)(((byte)(214)))));
            skyComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4",
            "Item 5"});
            skyComboBox1.Location = new System.Drawing.Point(93, 12);
            skyComboBox1.Name = "skyComboBox1";
            skyComboBox1.Size = new System.Drawing.Size(75, 22);
            skyComboBox1.StartIndex = 0;
            skyComboBox1.TabIndex = 4;
            // 
            // skyCheckBox1
            // 
            skyCheckBox1.BackColor = System.Drawing.Color.Transparent;
            skyCheckBox1.Checked = false;
            skyCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            skyCheckBox1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyCheckBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyCheckBox1.Location = new System.Drawing.Point(12, 41);
            skyCheckBox1.Name = "skyCheckBox1";
            skyCheckBox1.Size = new System.Drawing.Size(90, 14);
            skyCheckBox1.TabIndex = 3;
            skyCheckBox1.Text = "skyCheckBox1";
            // 
            // skyRadioButton2
            // 
            skyRadioButton2.BackColor = System.Drawing.Color.Transparent;
            skyRadioButton2.Checked = true;
            skyRadioButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            skyRadioButton2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyRadioButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyRadioButton2.Location = new System.Drawing.Point(12, 81);
            skyRadioButton2.Name = "skyRadioButton2";
            skyRadioButton2.Size = new System.Drawing.Size(105, 14);
            skyRadioButton2.TabIndex = 2;
            skyRadioButton2.Text = "skyRadioButton2";
            // 
            // skyRadioButton1
            // 
            skyRadioButton1.BackColor = System.Drawing.Color.Transparent;
            skyRadioButton1.Checked = false;
            skyRadioButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            skyRadioButton1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyRadioButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyRadioButton1.Location = new System.Drawing.Point(12, 61);
            skyRadioButton1.Name = "skyRadioButton1";
            skyRadioButton1.Size = new System.Drawing.Size(105, 14);
            skyRadioButton1.TabIndex = 1;
            skyRadioButton1.Text = "skyRadioButton1";
            // 
            // skyButton1
            // 
            skyButton1.BackColor = System.Drawing.Color.Transparent;
            skyButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            skyButton1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            skyButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(137)))));
            skyButton1.Location = new System.Drawing.Point(12, 12);
            skyButton1.Name = "skyButton1";
            skyButton1.Size = new System.Drawing.Size(75, 23);
            skyButton1.TabIndex = 0;
            skyButton1.Text = "skyButton1";
            // 
            // Form9
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(263, 130);
            Controls.Add(skyStatusBar1);
            Controls.Add(skyNumeric1);
            Controls.Add(skyLabel1);
            Controls.Add(skyTextBox1);
            Controls.Add(skyComboBox1);
            Controls.Add(skyCheckBox1);
            Controls.Add(skyRadioButton2);
            Controls.Add(skyRadioButton1);
            Controls.Add(skyButton1);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            Name = "Form9";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form9";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.SkyButton skyButton1;
        private ReaLTaiizor.Controls.SkyRadioButton skyRadioButton1;
        private ReaLTaiizor.Controls.SkyRadioButton skyRadioButton2;
        private ReaLTaiizor.Controls.SkyCheckBox skyCheckBox1;
        private ReaLTaiizor.Controls.SkyComboBox skyComboBox1;
        private ReaLTaiizor.Controls.SkyTextBox skyTextBox1;
        private ReaLTaiizor.Controls.SkyLabel skyLabel1;
        private ReaLTaiizor.Controls.SkyNumeric skyNumeric1;
        private ReaLTaiizor.Controls.SkyStatusBar skyStatusBar1;
    }
}