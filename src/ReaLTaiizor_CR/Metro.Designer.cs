
namespace ReaLTaiizor_CR
{
    partial class Metro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Metro));
            this.metroControlBox1 = new ReaLTaiizor.Controls.MetroControlBox();
            this.metroStyleManager1 = new ReaLTaiizor.Manager.MetroStyleManager();
            this.SuspendLayout();
            // 
            // metroControlBox1
            // 
            this.metroControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroControlBox1.CloseHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.metroControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.metroControlBox1.CloseNormalForeColor = System.Drawing.Color.Gray;
            this.metroControlBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroControlBox1.DefaultLocation = ReaLTaiizor.Enum.Metro.LocationType.Space;
            this.metroControlBox1.DisabledForeColor = System.Drawing.Color.Silver;
            this.metroControlBox1.IsDerivedStyle = true;
            this.metroControlBox1.Location = new System.Drawing.Point(520, 13);
            this.metroControlBox1.MaximizeBox = true;
            this.metroControlBox1.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroControlBox1.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            this.metroControlBox1.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            this.metroControlBox1.MinimizeBox = true;
            this.metroControlBox1.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.metroControlBox1.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            this.metroControlBox1.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            this.metroControlBox1.Name = "metroControlBox1";
            this.metroControlBox1.Size = new System.Drawing.Size(100, 25);
            this.metroControlBox1.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            this.metroControlBox1.StyleManager = this.metroStyleManager1;
            this.metroControlBox1.TabIndex = 0;
            this.metroControlBox1.Text = "metroControlBox1";
            this.metroControlBox1.ThemeAuthor = "Taiizor";
            this.metroControlBox1.ThemeName = "MetroDark";
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.CustomTheme = "C:\\Users\\Taiizor\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            this.metroStyleManager1.OwnerForm = this;
            this.metroStyleManager1.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            this.metroStyleManager1.ThemeAuthor = "Taiizor";
            this.metroStyleManager1.ThemeName = "MetroDark";
            // 
            // Metro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(632, 296);
            this.Controls.Add(this.metroControlBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Metro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Metro";
            this.TextColor = System.Drawing.Color.White;
            this.ThemeName = "MetroDark";
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.MetroControlBox metroControlBox1;
        private ReaLTaiizor.Manager.MetroStyleManager metroStyleManager1;
    }
}