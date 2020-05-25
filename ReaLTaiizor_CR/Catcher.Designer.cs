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
            this.aloneTextBox1 = new ReaLTaiizor.AloneTextBox();
            this.bigTextBox1 = new ReaLTaiizor.BigTextBox();
            this.SuspendLayout();
            // 
            // aloneTextBox1
            // 
            this.aloneTextBox1.BackColor = System.Drawing.Color.White;
            this.aloneTextBox1.EnabledCalc = true;
            this.aloneTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.aloneTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.aloneTextBox1.Location = new System.Drawing.Point(5, 39);
            this.aloneTextBox1.MaxLength = 32767;
            this.aloneTextBox1.MultiLine = false;
            this.aloneTextBox1.Name = "aloneTextBox1";
            this.aloneTextBox1.ReadOnly = false;
            this.aloneTextBox1.Size = new System.Drawing.Size(96, 29);
            this.aloneTextBox1.TabIndex = 0;
            this.aloneTextBox1.Text = "aloneTextBox1";
            this.aloneTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.aloneTextBox1.UseSystemPasswordChar = false;
            // 
            // bigTextBox1
            // 
            this.bigTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.bigTextBox1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.bigTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.bigTextBox1.Image = null;
            this.bigTextBox1.Location = new System.Drawing.Point(107, 39);
            this.bigTextBox1.MaxLength = 32767;
            this.bigTextBox1.Multiline = false;
            this.bigTextBox1.Name = "bigTextBox1";
            this.bigTextBox1.ReadOnly = false;
            this.bigTextBox1.Size = new System.Drawing.Size(100, 41);
            this.bigTextBox1.TabIndex = 1;
            this.bigTextBox1.Text = "bigTextBox1";
            this.bigTextBox1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.bigTextBox1.UseSystemPasswordChar = false;
            // 
            // Catcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(398, 473);
            this.Controls.Add(this.bigTextBox1);
            this.Controls.Add(this.aloneTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Catcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catcher";
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.AloneTextBox aloneTextBox1;
        private ReaLTaiizor.BigTextBox bigTextBox1;
    }
}