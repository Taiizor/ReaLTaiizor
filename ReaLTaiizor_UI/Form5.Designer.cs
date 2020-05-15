namespace ReaLTaiizor_UI
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.dreamForm1 = new ReaLTaiizor.DreamForm();
            this.dungeonControlBox1 = new ReaLTaiizor.DungeonControlBox();
            this.dreamProgressBar1 = new ReaLTaiizor.DreamProgressBar();
            this.dreamTextBox1 = new ReaLTaiizor.DreamTextBox();
            this.dreamButton1 = new ReaLTaiizor.DreamButton();
            this.dreamForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dreamForm1
            // 
            this.dreamForm1.BackColor = System.Drawing.Color.Transparent;
            this.dreamForm1.Controls.Add(this.dungeonControlBox1);
            this.dreamForm1.Controls.Add(this.dreamProgressBar1);
            this.dreamForm1.Controls.Add(this.dreamTextBox1);
            this.dreamForm1.Controls.Add(this.dreamButton1);
            this.dreamForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dreamForm1.Location = new System.Drawing.Point(0, 0);
            this.dreamForm1.MinimumSize = new System.Drawing.Size(136, 50);
            this.dreamForm1.Name = "dreamForm1";
            this.dreamForm1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.dreamForm1.Size = new System.Drawing.Size(256, 177);
            this.dreamForm1.TabIndex = 0;
            this.dreamForm1.Text = "dreamForm1";
            this.dreamForm1.TitleAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dreamForm1.TitleHeight = 25;
            // 
            // dungeonControlBox1
            // 
            this.dungeonControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.dungeonControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dungeonControlBox1.DefaultLocation = false;
            this.dungeonControlBox1.EnableMaximize = false;
            this.dungeonControlBox1.EnableMinimize = false;
            this.dungeonControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            this.dungeonControlBox1.Location = new System.Drawing.Point(224, 2);
            this.dungeonControlBox1.Name = "dungeonControlBox1";
            this.dungeonControlBox1.Size = new System.Drawing.Size(24, 22);
            this.dungeonControlBox1.TabIndex = 4;
            this.dungeonControlBox1.Text = "dungeonControlBox1";
            // 
            // dreamProgressBar1
            // 
            this.dreamProgressBar1.Location = new System.Drawing.Point(8, 116);
            this.dreamProgressBar1.Name = "dreamProgressBar1";
            this.dreamProgressBar1.Size = new System.Drawing.Size(236, 41);
            this.dreamProgressBar1.TabIndex = 1;
            this.dreamProgressBar1.Value = 50;
            // 
            // dreamTextBox1
            // 
            this.dreamTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.dreamTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dreamTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dreamTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            this.dreamTextBox1.Location = new System.Drawing.Point(8, 31);
            this.dreamTextBox1.Name = "dreamTextBox1";
            this.dreamTextBox1.Size = new System.Drawing.Size(236, 32);
            this.dreamTextBox1.TabIndex = 1;
            this.dreamTextBox1.Text = "dreamTextBox1";
            this.dreamTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dreamButton1
            // 
            this.dreamButton1.BackColor = System.Drawing.Color.Transparent;
            this.dreamButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dreamButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dreamButton1.Image = null;
            this.dreamButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dreamButton1.Location = new System.Drawing.Point(8, 69);
            this.dreamButton1.Name = "dreamButton1";
            this.dreamButton1.Size = new System.Drawing.Size(236, 41);
            this.dreamButton1.TabIndex = 0;
            this.dreamButton1.Text = "dreamButton1";
            this.dreamButton1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 177);
            this.Controls.Add(this.dreamForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.dreamForm1.ResumeLayout(false);
            this.dreamForm1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.DreamForm dreamForm1;
        private ReaLTaiizor.DreamTextBox dreamTextBox1;
        private ReaLTaiizor.DreamButton dreamButton1;
        private ReaLTaiizor.DreamProgressBar dreamProgressBar1;
        private ReaLTaiizor.DungeonControlBox dungeonControlBox1;
    }
}