namespace ReaLTaiizor_CR
{
    partial class Material
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Material));
            this.materialTextBox1 = new ReaLTaiizor.Controls.TextBox.MaterialTextBox();
            this.materialTextBox2 = new ReaLTaiizor.Controls.TextBox.MaterialTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.materialRichTextBox1 = new ReaLTaiizor.Controls.RichTextBox.MaterialRichTextBox();
            this.SuspendLayout();
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.materialTextBox1.Hint = "53X";
            this.materialTextBox1.Location = new System.Drawing.Point(12, 85);
            this.materialTextBox1.MaxLength = 50;
            this.materialTextBox1.MouseState = ReaLTaiizor.Helpers.MaterialDrawHelper.MaterialMouseState.OUT;
            this.materialTextBox1.Multiline = false;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.Size = new System.Drawing.Size(291, 50);
            this.materialTextBox1.TabIndex = 0;
            this.materialTextBox1.Text = "deneme";
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.materialTextBox2.Hint = "53X";
            this.materialTextBox2.Location = new System.Drawing.Point(12, 141);
            this.materialTextBox2.MaxLength = 50;
            this.materialTextBox2.MouseState = ReaLTaiizor.Helpers.MaterialDrawHelper.MaterialMouseState.OUT;
            this.materialTextBox2.Multiline = false;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.Password = true;
            this.materialTextBox2.Size = new System.Drawing.Size(291, 50);
            this.materialTextBox2.TabIndex = 1;
            this.materialTextBox2.Text = "deneme";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 197);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(291, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "deneme";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 223);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(291, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "deneme";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 249);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '●';
            this.textBox3.Size = new System.Drawing.Size(291, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "deneme";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(12, 275);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '●';
            this.textBox4.Size = new System.Drawing.Size(291, 20);
            this.textBox4.TabIndex = 5;
            this.textBox4.Text = "deneme";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(12, 301);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(291, 55);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "deneme";
            // 
            // materialRichTextBox1
            // 
            this.materialRichTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialRichTextBox1.Depth = 0;
            this.materialRichTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialRichTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialRichTextBox1.Hint = "";
            this.materialRichTextBox1.Location = new System.Drawing.Point(309, 88);
            this.materialRichTextBox1.MouseState = ReaLTaiizor.Helpers.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialRichTextBox1.Name = "materialRichTextBox1";
            this.materialRichTextBox1.Size = new System.Drawing.Size(291, 106);
            this.materialRichTextBox1.TabIndex = 8;
            this.materialRichTextBox1.Text = "This is a test message!";
            // 
            // Material
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 376);
            this.Controls.Add(this.materialRichTextBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.materialTextBox2);
            this.Controls.Add(this.materialTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Material";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.TextBox.MaterialTextBox materialTextBox1;
        private ReaLTaiizor.Controls.TextBox.MaterialTextBox materialTextBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ReaLTaiizor.Controls.RichTextBox.MaterialRichTextBox materialRichTextBox1;
    }
}