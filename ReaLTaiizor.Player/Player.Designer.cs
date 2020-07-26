namespace ReaLTaiizor.Player
{
    partial class Player
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Player));
            dreamForm1 = new ReaLTaiizor.DreamForm();
            foreverClose1 = new ReaLTaiizor.ForeverClose();
            dreamForm1.SuspendLayout();
            SuspendLayout();
            // 
            // dreamForm1
            // 
            dreamForm1.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            dreamForm1.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            dreamForm1.ColorC = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            dreamForm1.ColorD = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            dreamForm1.ColorE = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dreamForm1.ColorF = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dreamForm1.Controls.Add(foreverClose1);
            dreamForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            dreamForm1.Location = new System.Drawing.Point(0, 0);
            dreamForm1.Name = "dreamForm1";
            dreamForm1.Size = new System.Drawing.Size(300, 500);
            dreamForm1.TabIndex = 0;
            dreamForm1.TabStop = false;
            dreamForm1.Text = "MP3 Player";
            dreamForm1.TitleAlign = System.Windows.Forms.HorizontalAlignment.Center;
            dreamForm1.TitleHeight = 25;
            // 
            // foreverClose1
            // 
            foreverClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            foreverClose1.BackColor = System.Drawing.Color.White;
            foreverClose1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            foreverClose1.Cursor = System.Windows.Forms.Cursors.Hand;
            foreverClose1.DefaultLocation = false;
            foreverClose1.DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            foreverClose1.Font = new System.Drawing.Font("Marlett", 10F);
            foreverClose1.Location = new System.Drawing.Point(278, 4);
            foreverClose1.Name = "foreverClose1";
            foreverClose1.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            foreverClose1.Size = new System.Drawing.Size(18, 18);
            foreverClose1.TabIndex = 0;
            foreverClose1.Text = "foreverClose1";
            foreverClose1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            // 
            // Player
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(300, 500);
            Controls.Add(dreamForm1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Player";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "MP3 Player";
            dreamForm1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private DreamForm dreamForm1;
        private ForeverClose foreverClose1;
    }
}