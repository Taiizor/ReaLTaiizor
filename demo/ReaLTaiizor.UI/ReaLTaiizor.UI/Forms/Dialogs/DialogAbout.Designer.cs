using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Dialogs
{
    partial class DialogAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAbout));
            pnlMain = new System.Windows.Forms.Panel();
            lblVersion = new CrownLabel();
            CrownLabel3 = new CrownLabel();
            CrownLabel2 = new CrownLabel();
            CrownLabel1 = new CrownLabel();
            lblHeader = new CrownLabel();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(lblVersion);
            pnlMain.Controls.Add(CrownLabel3);
            pnlMain.Controls.Add(CrownLabel2);
            pnlMain.Controls.Add(CrownLabel1);
            pnlMain.Controls.Add(lblHeader);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            pnlMain.Size = new System.Drawing.Size(343, 229);
            pnlMain.TabIndex = 2;
            // 
            // lblVersion
            // 
            lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            lblVersion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            lblVersion.Location = new System.Drawing.Point(15, 192);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(313, 36);
            lblVersion.TabIndex = 7;
            lblVersion.Text = "Version: [version]";
            lblVersion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // CrownLabel3
            // 
            CrownLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            CrownLabel3.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CrownLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            CrownLabel3.Location = new System.Drawing.Point(15, 152);
            CrownLabel3.Name = "CrownLabel3";
            CrownLabel3.Size = new System.Drawing.Size(313, 40);
            CrownLabel3.TabIndex = 6;
            CrownLabel3.Text = "(Also with a hardcoded crown theme because I totally could not figure out a clean " +
    "way to have application-wide theme settings... so, you know, if you\'ve got an id" +
    "ea, pull request me.)\r\n";
            CrownLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CrownLabel2
            // 
            CrownLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            CrownLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CrownLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            CrownLabel2.Location = new System.Drawing.Point(15, 101);
            CrownLabel2.Name = "CrownLabel2";
            CrownLabel2.Size = new System.Drawing.Size(313, 51);
            CrownLabel2.TabIndex = 5;
            CrownLabel2.Text = "Created because of all the little annoyances and issues with the default .NET con" +
    "trol library.";
            CrownLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CrownLabel1
            // 
            CrownLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            CrownLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CrownLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            CrownLabel1.Location = new System.Drawing.Point(15, 47);
            CrownLabel1.Name = "CrownLabel1";
            CrownLabel1.Size = new System.Drawing.Size(313, 54);
            CrownLabel1.TabIndex = 4;
            CrownLabel1.Text = "Crown themed control and docking library for .NET WinForms.";
            CrownLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeader
            // 
            lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            lblHeader.Location = new System.Drawing.Point(15, 15);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new System.Drawing.Size(313, 32);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Crown";
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DialogAbout
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(343, 274);
            Controls.Add(pnlMain);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DialogAbout";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "About Crown";
            Controls.SetChildIndex(pnlMain, 0);
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private CrownLabel lblHeader;
        private CrownLabel CrownLabel1;
        private CrownLabel CrownLabel3;
        private CrownLabel CrownLabel2;
        private CrownLabel lblVersion;
    }
}