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
            parrotForm1 = new ReaLTaiizor.Forms.ParrotForm();
            SuspendLayout();
            // 
            // parrotForm1
            // 
            parrotForm1.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            parrotForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            parrotForm1.ExitApplication = true;
            parrotForm1.FormStyle = ReaLTaiizor.Forms.ParrotForm.Style.MacOS;
            parrotForm1.Location = new System.Drawing.Point(0, 0);
            parrotForm1.MacOSForeColor = System.Drawing.Color.FromArgb(40, 40, 40);
            parrotForm1.MacOSLeftBackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            parrotForm1.MacOSRightBackColor = System.Drawing.Color.FromArgb(210, 210, 210);
            parrotForm1.MacOSSeparatorColor = System.Drawing.Color.FromArgb(173, 173, 173);
            parrotForm1.MaterialBackColor = System.Drawing.Color.DodgerBlue;
            parrotForm1.MaterialForeColor = System.Drawing.Color.White;
            parrotForm1.Name = "parrotForm1";
            parrotForm1.ShowMaximize = true;
            parrotForm1.ShowMinimize = true;
            parrotForm1.Size = new System.Drawing.Size(800, 450);
            parrotForm1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            parrotForm1.TabIndex = 0;
            parrotForm1.TitleText = "Parrot Form";
            parrotForm1.UbuntuForeColor = System.Drawing.Color.FromArgb(220, 220, 210);
            parrotForm1.UbuntuLeftBackColor = System.Drawing.Color.FromArgb(90, 85, 80);
            parrotForm1.UbuntuRightBackColor = System.Drawing.Color.FromArgb(65, 65, 60);
            // 
            // 
            // 
            parrotForm1.WorkingArea.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            parrotForm1.WorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            parrotForm1.WorkingArea.Location = new System.Drawing.Point(0, 39);
            parrotForm1.WorkingArea.Name = "WorkingArea";
            parrotForm1.WorkingArea.Size = new System.Drawing.Size(800, 411);
            parrotForm1.WorkingArea.TabIndex = 0;
            // 
            // ParrotForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(parrotForm1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = Properties.Resources.ICO;
            Name = "ParrotForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ParrotForm";
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.ParrotForm parrotForm1;
    }
}