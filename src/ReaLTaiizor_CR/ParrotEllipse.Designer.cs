namespace ReaLTaiizor_CR
{
    partial class ParrotEllipse
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
            panel1 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panel5 = new System.Windows.Forms.Panel();
            panel6 = new System.Windows.Forms.Panel();
            parrotControlEllipse1 = new ReaLTaiizor.Controls.ParrotControlEllipse();
            parrotControlEllipse2 = new ReaLTaiizor.Controls.ParrotControlEllipse();
            parrotFormEllipse1 = new ReaLTaiizor.Controls.ParrotFormEllipse();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel1.Location = new System.Drawing.Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(200, 100);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel2.Location = new System.Drawing.Point(218, 12);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(200, 100);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel3.Location = new System.Drawing.Point(424, 12);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(200, 100);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel4.Location = new System.Drawing.Point(424, 118);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(200, 100);
            panel4.TabIndex = 5;
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel5.Location = new System.Drawing.Point(218, 118);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(200, 100);
            panel5.TabIndex = 4;
            // 
            // panel6
            // 
            panel6.BackColor = System.Drawing.Color.FromArgb(66, 67, 68);
            panel6.Location = new System.Drawing.Point(12, 118);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(200, 100);
            panel6.TabIndex = 3;
            // 
            // parrotControlEllipse1
            // 
            parrotControlEllipse1.CornerRadius = 25;
            parrotControlEllipse1.EffectedControl = panel1;
            // 
            // parrotControlEllipse2
            // 
            parrotControlEllipse2.CornerRadius = 25;
            parrotControlEllipse2.EffectedControl = panel4;
            // 
            // parrotFormEllipse1
            // 
            parrotFormEllipse1.CornerRadius = 10;
            parrotFormEllipse1.EffectedForm = this;
            // 
            // ParrotEllipse
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(33, 34, 35);
            ClientSize = new System.Drawing.Size(636, 235);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(panel6);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = Properties.Resources.ICO;
            MaximizeBox = false;
            Name = "ParrotEllipse";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Parrot Ellipse";
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.ParrotControlEllipse parrotControlEllipse1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private ReaLTaiizor.Controls.ParrotControlEllipse parrotControlEllipse2;
        private ReaLTaiizor.Controls.ParrotFormEllipse parrotFormEllipse1;
    }
}