namespace ReaLTaiizor.UI
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            formTheme1 = new ReaLTaiizor.Forms.FormTheme();
            controlBoxEdit1 = new Controls.ControlBoxEdit();
            formTheme1.SuspendLayout();
            SuspendLayout();
            // 
            // formTheme1
            // 
            formTheme1.BackColor = System.Drawing.Color.FromArgb(32, 41, 50);
            formTheme1.Controls.Add(controlBoxEdit1);
            formTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            formTheme1.Font = new System.Drawing.Font("Segoe UI", 8F);
            formTheme1.ForeColor = System.Drawing.Color.FromArgb(142, 142, 142);
            formTheme1.Location = new System.Drawing.Point(0, 0);
            formTheme1.Name = "formTheme1";
            formTheme1.Padding = new System.Windows.Forms.Padding(3, 28, 3, 28);
            formTheme1.Sizable = true;
            formTheme1.Size = new System.Drawing.Size(512, 256);
            formTheme1.SmartBounds = true;
            formTheme1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            formTheme1.TabIndex = 0;
            formTheme1.Text = "formTheme1";
            // 
            // controlBoxEdit1
            // 
            controlBoxEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            controlBoxEdit1.BackColor = System.Drawing.Color.Transparent;
            controlBoxEdit1.Cursor = System.Windows.Forms.Cursors.Hand;
            controlBoxEdit1.DefaultLocation = true;
            controlBoxEdit1.Location = new System.Drawing.Point(431, -1);
            controlBoxEdit1.Name = "controlBoxEdit1";
            controlBoxEdit1.Size = new System.Drawing.Size(77, 19);
            controlBoxEdit1.TabIndex = 0;
            controlBoxEdit1.Text = "controlBoxEdit1";
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(512, 256);
            Controls.Add(formTheme1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(126, 50);
            Name = "Form2";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "formTheme1";
            TransparencyKey = System.Drawing.Color.Fuchsia;
            formTheme1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.FormTheme formTheme1;
        private ReaLTaiizor.Controls.ControlBoxEdit controlBoxEdit1;
    }
}