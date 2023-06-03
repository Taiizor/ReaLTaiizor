
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
            metroControlBox1 = new ReaLTaiizor.Controls.MetroControlBox();
            metroStyleManager1 = new ReaLTaiizor.Manager.MetroStyleManager();
            SuspendLayout();
            // 
            // metroControlBox1
            // 
            metroControlBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            metroControlBox1.CloseHoverBackColor = System.Drawing.Color.FromArgb(183, 40, 40);
            metroControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            metroControlBox1.CloseNormalForeColor = System.Drawing.Color.Gray;
            metroControlBox1.DefaultLocation = ReaLTaiizor.Enum.Metro.LocationType.Space;
            metroControlBox1.DisabledForeColor = System.Drawing.Color.Silver;
            metroControlBox1.IsDerivedStyle = true;
            metroControlBox1.Location = new System.Drawing.Point(520, 13);
            metroControlBox1.MaximizeBox = true;
            metroControlBox1.MaximizeHoverBackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            metroControlBox1.MaximizeHoverForeColor = System.Drawing.Color.Gray;
            metroControlBox1.MaximizeNormalForeColor = System.Drawing.Color.Gray;
            metroControlBox1.MinimizeBox = true;
            metroControlBox1.MinimizeHoverBackColor = System.Drawing.Color.FromArgb(238, 238, 238);
            metroControlBox1.MinimizeHoverForeColor = System.Drawing.Color.Gray;
            metroControlBox1.MinimizeNormalForeColor = System.Drawing.Color.Gray;
            metroControlBox1.Name = "metroControlBox1";
            metroControlBox1.Size = new System.Drawing.Size(100, 25);
            metroControlBox1.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            metroControlBox1.StyleManager = metroStyleManager1;
            metroControlBox1.TabIndex = 0;
            metroControlBox1.Text = "metroControlBox1";
            metroControlBox1.ThemeAuthor = "Taiizor";
            metroControlBox1.ThemeName = "MetroDark";
            // 
            // metroStyleManager1
            // 
            metroStyleManager1.CustomTheme = "C:\\Users\\Taiizor\\AppData\\Roaming\\Microsoft\\Windows\\Templates\\ThemeFile.xml";
            metroStyleManager1.OwnerForm = this;
            metroStyleManager1.Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            metroStyleManager1.ThemeAuthor = "Taiizor";
            metroStyleManager1.ThemeName = "MetroDark";
            // 
            // Metro
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(632, 296);
            Controls.Add(metroControlBox1);
            Icon = Properties.Resources.ICO;
            Name = "Metro";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            StyleManager = metroStyleManager1;
            Text = "Metro";
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.MetroControlBox metroControlBox1;
        private ReaLTaiizor.Manager.MetroStyleManager metroStyleManager1;
    }
}