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
            SuspendLayout();
            // 
            // aloneTextBox1
            // 
            aloneTextBox1.BackColor = System.Drawing.Color.White;
            aloneTextBox1.EnabledCalc = true;
            aloneTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            aloneTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            aloneTextBox1.Location = new System.Drawing.Point(104, 201);
            aloneTextBox1.MaxLength = 32767;
            aloneTextBox1.MultiLine = false;
            aloneTextBox1.Name = "aloneTextBox1";
            aloneTextBox1.ReadOnly = false;
            aloneTextBox1.Size = new System.Drawing.Size(159, 54);
            aloneTextBox1.TabIndex = 0;
            aloneTextBox1.Text = "aloneTextBox1";
            aloneTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            aloneTextBox1.UseSystemPasswordChar = false;
            // 
            // Catcher
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            ClientSize = new System.Drawing.Size(398, 473);
            Controls.Add(aloneTextBox1);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Catcher";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Catcher";
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.AloneTextBox aloneTextBox1;
    }
}