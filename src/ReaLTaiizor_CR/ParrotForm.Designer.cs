namespace ReaLTaiizor_CR
{
    partial class ParrotForm
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
            this.parrotForm1 = new ReaLTaiizor.Forms.ParrotForm();
            this.parrotForm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parrotForm1
            // 
            this.parrotForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.ExitApplication = true;
            this.parrotForm1.FormStyle = ReaLTaiizor.Forms.ParrotForm.Style.MacOS;
            this.parrotForm1.Location = new System.Drawing.Point(0, 0);
            this.parrotForm1.MacOSForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.parrotForm1.MacOSLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.parrotForm1.MacOSRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.parrotForm1.MacOSSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            this.parrotForm1.MaterialBackColor = System.Drawing.Color.DodgerBlue;
            this.parrotForm1.MaterialForeColor = System.Drawing.Color.White;
            this.parrotForm1.Name = "parrotForm1";
            this.parrotForm1.ShowMaximize = true;
            this.parrotForm1.ShowMinimize = true;
            this.parrotForm1.Size = new System.Drawing.Size(800, 450);
            this.parrotForm1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotForm1.TabIndex = 0;
            this.parrotForm1.TitleText = "Parrot Form";
            this.parrotForm1.UbuntuForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(210)))));
            this.parrotForm1.UbuntuLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(85)))), ((int)(((byte)(80)))));
            this.parrotForm1.UbuntuRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            // 
            // parrotForm1.WorkingArea
            // 
            this.parrotForm1.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.WorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.WorkingArea.Location = new System.Drawing.Point(0, 39);
            this.parrotForm1.WorkingArea.Name = "WorkingArea";
            this.parrotForm1.WorkingArea.Size = new System.Drawing.Size(800, 411);
            this.parrotForm1.WorkingArea.TabIndex = 0;
            // 
            // ParrotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.parrotForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::ReaLTaiizor_CR.Properties.Resources.ICO;
            this.Name = "ParrotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParrotForm";
            this.parrotForm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.ParrotForm parrotForm1;
    }
}