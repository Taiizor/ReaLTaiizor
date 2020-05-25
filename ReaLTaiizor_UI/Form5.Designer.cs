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
            dreamForm1 = new ReaLTaiizor.DreamForm();
            dungeonControlBox1 = new ReaLTaiizor.DungeonControlBox();
            dreamProgressBar1 = new ReaLTaiizor.DreamProgressBar();
            dreamTextBox1 = new ReaLTaiizor.DreamTextBox();
            dreamButton1 = new ReaLTaiizor.DreamButton();
            dreamForm1.SuspendLayout();
            SuspendLayout();
            // 
            // dreamForm1
            // 
            dreamForm1.BackColor = System.Drawing.Color.Transparent;
            dreamForm1.Controls.Add(dungeonControlBox1);
            dreamForm1.Controls.Add(dreamProgressBar1);
            dreamForm1.Controls.Add(dreamTextBox1);
            dreamForm1.Controls.Add(dreamButton1);
            dreamForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            dreamForm1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            dreamForm1.Location = new System.Drawing.Point(0, 0);
            dreamForm1.MinimumSize = new System.Drawing.Size(136, 50);
            dreamForm1.Name = "dreamForm1";
            dreamForm1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            dreamForm1.Size = new System.Drawing.Size(256, 177);
            dreamForm1.TabIndex = 0;
            dreamForm1.Text = "dreamForm1";
            dreamForm1.TitleAlign = System.Windows.Forms.HorizontalAlignment.Center;
            dreamForm1.TitleHeight = 25;
            // 
            // dungeonControlBox1
            // 
            dungeonControlBox1.BackColor = System.Drawing.Color.Transparent;
            dungeonControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            dungeonControlBox1.DefaultLocation = false;
            dungeonControlBox1.EnableMaximize = false;
            dungeonControlBox1.EnableMinimize = false;
            dungeonControlBox1.Font = new System.Drawing.Font("Marlett", 7F);
            dungeonControlBox1.Location = new System.Drawing.Point(224, 2);
            dungeonControlBox1.Name = "dungeonControlBox1";
            dungeonControlBox1.Size = new System.Drawing.Size(23, 22);
            dungeonControlBox1.TabIndex = 4;
            dungeonControlBox1.Text = "dungeonControlBox1";
            // 
            // dreamProgressBar1
            // 
            dreamProgressBar1.Location = new System.Drawing.Point(8, 116);
            dreamProgressBar1.Name = "dreamProgressBar1";
            dreamProgressBar1.Size = new System.Drawing.Size(236, 41);
            dreamProgressBar1.TabIndex = 1;
            dreamProgressBar1.Value = 50;
            // 
            // dreamTextBox1
            // 
            dreamTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            dreamTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            dreamTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dreamTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            dreamTextBox1.Location = new System.Drawing.Point(8, 31);
            dreamTextBox1.Name = "dreamTextBox1";
            dreamTextBox1.Size = new System.Drawing.Size(236, 32);
            dreamTextBox1.TabIndex = 1;
            dreamTextBox1.Text = "dreamTextBox1";
            dreamTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dreamButton1
            // 
            dreamButton1.BackColor = System.Drawing.Color.Transparent;
            dreamButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            dreamButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dreamButton1.Image = null;
            dreamButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            dreamButton1.Location = new System.Drawing.Point(8, 69);
            dreamButton1.Name = "dreamButton1";
            dreamButton1.Size = new System.Drawing.Size(236, 41);
            dreamButton1.TabIndex = 0;
            dreamButton1.Text = "dreamButton1";
            // 
            // Form5
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(256, 177);
            Controls.Add(dreamForm1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Form5";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form5";
            dreamForm1.ResumeLayout(false);
            dreamForm1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.DreamForm dreamForm1;
        private ReaLTaiizor.DreamTextBox dreamTextBox1;
        private ReaLTaiizor.DreamButton dreamButton1;
        private ReaLTaiizor.DreamProgressBar dreamProgressBar1;
        private ReaLTaiizor.DungeonControlBox dungeonControlBox1;
    }
}