namespace ReaLTaiizor_CR
{
    partial class Catcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catcher));
            aloneTextBox1 = new ReaLTaiizor.AloneTextBox();
            bigTextBox1 = new ReaLTaiizor.BigTextBox();
            comboBoxEdit1 = new ReaLTaiizor.ComboBoxEdit();
            SuspendLayout();
            // 
            // aloneTextBox1
            // 
            aloneTextBox1.BackColor = System.Drawing.Color.White;
            aloneTextBox1.EnabledCalc = true;
            aloneTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            aloneTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            aloneTextBox1.Location = new System.Drawing.Point(5, 39);
            aloneTextBox1.MaxLength = 32767;
            aloneTextBox1.MultiLine = false;
            aloneTextBox1.Name = "aloneTextBox1";
            aloneTextBox1.ReadOnly = false;
            aloneTextBox1.Size = new System.Drawing.Size(96, 29);
            aloneTextBox1.TabIndex = 0;
            aloneTextBox1.Text = "aloneTextBox1";
            aloneTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            aloneTextBox1.UseSystemPasswordChar = false;
            // 
            // bigTextBox1
            // 
            bigTextBox1.BackColor = System.Drawing.Color.Transparent;
            bigTextBox1.Font = new System.Drawing.Font("Tahoma", 11F);
            bigTextBox1.ForeColor = System.Drawing.Color.DimGray;
            bigTextBox1.Image = null;
            bigTextBox1.Location = new System.Drawing.Point(107, 39);
            bigTextBox1.MaxLength = 32767;
            bigTextBox1.Multiline = false;
            bigTextBox1.Name = "bigTextBox1";
            bigTextBox1.ReadOnly = false;
            bigTextBox1.Size = new System.Drawing.Size(100, 41);
            bigTextBox1.TabIndex = 1;
            bigTextBox1.Text = "bigTextBox1";
            bigTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            bigTextBox1.UseSystemPasswordChar = false;
            // 
            // comboBoxEdit1
            // 
            comboBoxEdit1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            comboBoxEdit1.Cursor = System.Windows.Forms.Cursors.Hand;
            comboBoxEdit1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            comboBoxEdit1.DropDownHeight = 100;
            comboBoxEdit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxEdit1.Font = new System.Drawing.Font("Segoe UI", 10F);
            comboBoxEdit1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(142)))));
            comboBoxEdit1.FormattingEnabled = true;
            comboBoxEdit1.HoverSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            comboBoxEdit1.IntegralHeight = false;
            comboBoxEdit1.ItemHeight = 20;
            comboBoxEdit1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3"});
            comboBoxEdit1.Location = new System.Drawing.Point(213, 39);
            comboBoxEdit1.Name = "comboBoxEdit1";
            comboBoxEdit1.Size = new System.Drawing.Size(72, 26);
            comboBoxEdit1.StartIndex = 0;
            comboBoxEdit1.TabIndex = 2;
            // 
            // Catcher
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ClientSize = new System.Drawing.Size(398, 473);
            Controls.Add(comboBoxEdit1);
            Controls.Add(bigTextBox1);
            Controls.Add(aloneTextBox1);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Catcher";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Catcher";
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.AloneTextBox aloneTextBox1;
        private ReaLTaiizor.BigTextBox bigTextBox1;
        private ReaLTaiizor.ComboBoxEdit comboBoxEdit1;
    }
}