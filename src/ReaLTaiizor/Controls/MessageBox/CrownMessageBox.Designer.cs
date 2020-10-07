using ReaLTaiizor.Controls;

namespace ReaLTaiizor.Controls
{
    partial class CrownMessageBox
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
            picIcon = new System.Windows.Forms.PictureBox();
            lblText = new CrownLabel();
            ((System.ComponentModel.ISupportInitialize)(picIcon)).BeginInit();
            SuspendLayout();
            // 
            // picIcon
            // 
            picIcon.Location = new System.Drawing.Point(10, 10);
            picIcon.Name = "picIcon";
            picIcon.Size = new System.Drawing.Size(32, 32);
            picIcon.TabIndex = 3;
            picIcon.TabStop = false;
            // 
            // lblText
            // 
            lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            lblText.Location = new System.Drawing.Point(50, 9);
            lblText.Name = "lblText";
            lblText.Size = new System.Drawing.Size(185, 15);
            lblText.TabIndex = 4;
            lblText.Text = "Something something something";
            // 
            // CrownMessageBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(244, 86);
            Controls.Add(lblText);
            Controls.Add(picIcon);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CrownMessageBox";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Message box";
            Controls.SetChildIndex(picIcon, 0);
            Controls.SetChildIndex(lblText, 0);
            ((System.ComponentModel.ISupportInitialize)(picIcon)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picIcon;
        private CrownLabel lblText;
    }
}