namespace ReaLTaiizor.Nerator.UC.THEME
{
    partial class DK
    {
        /// <summary> 
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Bileşen Tasarımcısı üretimi kod

        /// <summary> 
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            TETT = new ReaLTaiizor.Controls.MetroToolTip();
            DKPL = new ReaLTaiizor.Controls.Panel();
            DKP2 = new ReaLTaiizor.Controls.Panel();
            DKP3 = new ReaLTaiizor.Controls.Panel();
            DKP1 = new ReaLTaiizor.Controls.Panel();
            DKB1 = new ReaLTaiizor.Controls.Badge();
            DKB2 = new ReaLTaiizor.Controls.Badge();
            MNPB = new System.Windows.Forms.PictureBox();
            DKPL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(MNPB)).BeginInit();
            SuspendLayout();
            // 
            // TETT
            // 
            TETT.BackColor = System.Drawing.Color.White;
            TETT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            TETT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            TETT.IsDerivedStyle = true;
            TETT.OwnerDraw = true;
            TETT.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            TETT.StyleManager = null;
            TETT.ThemeAuthor = "Taiizor";
            TETT.ThemeName = "MetroLight";
            // 
            // DKPL
            // 
            DKPL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKPL.Controls.Add(DKP2);
            DKPL.Controls.Add(DKP3);
            DKPL.Controls.Add(DKP1);
            DKPL.Controls.Add(DKB1);
            DKPL.Controls.Add(DKB2);
            DKPL.Controls.Add(MNPB);
            DKPL.Cursor = System.Windows.Forms.Cursors.Hand;
            DKPL.Dock = System.Windows.Forms.DockStyle.Fill;
            DKPL.EdgeColor = System.Drawing.SystemColors.Control;
            DKPL.Location = new System.Drawing.Point(0, 0);
            DKPL.Margin = new System.Windows.Forms.Padding(0);
            DKPL.MaximumSize = new System.Drawing.Size(103, 97);
            DKPL.MinimumSize = new System.Drawing.Size(103, 97);
            DKPL.Name = "DKPL";
            DKPL.Padding = new System.Windows.Forms.Padding(5);
            DKPL.Size = new System.Drawing.Size(103, 97);
            DKPL.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DKPL.TabIndex = 12;
            DKPL.Text = "DKPL";
            TETT.SetToolTip(DKPL, "Dark Theme");
            DKPL.Click += new System.EventHandler(DKPL_Click);
            DKPL.MouseEnter += new System.EventHandler(DKPL_MouseEnter);
            DKPL.MouseLeave += new System.EventHandler(DKPL_MouseLeave);
            // 
            // DKP2
            // 
            DKP2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            DKP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKP2.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKP2.Enabled = false;
            DKP2.Location = new System.Drawing.Point(34, 34);
            DKP2.Name = "DKP2";
            DKP2.Padding = new System.Windows.Forms.Padding(5);
            DKP2.Size = new System.Drawing.Size(35, 55);
            DKP2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DKP2.TabIndex = 15;
            DKP2.Text = "DKP2";
            TETT.SetToolTip(DKP2, "Dark Theme");
            // 
            // DKP3
            // 
            DKP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            DKP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKP3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKP3.Enabled = false;
            DKP3.Location = new System.Drawing.Point(75, 34);
            DKP3.Name = "DKP3";
            DKP3.Padding = new System.Windows.Forms.Padding(5);
            DKP3.Size = new System.Drawing.Size(20, 55);
            DKP3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DKP3.TabIndex = 14;
            DKP3.Text = "DKP3";
            TETT.SetToolTip(DKP3, "Dark Theme");
            // 
            // DKP1
            // 
            DKP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            DKP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKP1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKP1.Enabled = false;
            DKP1.Location = new System.Drawing.Point(8, 34);
            DKP1.Name = "DKP1";
            DKP1.Padding = new System.Windows.Forms.Padding(5);
            DKP1.Size = new System.Drawing.Size(20, 55);
            DKP1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DKP1.TabIndex = 11;
            DKP1.Text = "DKP1";
            TETT.SetToolTip(DKP1, "Dark Theme");
            // 
            // DKB1
            // 
            DKB1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKB1.BGColorA = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB1.BGColorB = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB1.Enabled = false;
            DKB1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            DKB1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB1.Location = new System.Drawing.Point(8, 8);
            DKB1.Maximum = 0;
            DKB1.Name = "DKB1";
            DKB1.Size = new System.Drawing.Size(20, 20);
            DKB1.TabIndex = 13;
            DKB1.Text = "DKB1";
            TETT.SetToolTip(DKB1, "Dark Theme");
            DKB1.Value = 0;
            // 
            // DKB2
            // 
            DKB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            DKB2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(73)))), ((int)(((byte)(80)))));
            DKB2.BGColorA = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB2.BGColorB = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB2.Enabled = false;
            DKB2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            DKB2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(103)))), ((int)(((byte)(112)))));
            DKB2.Location = new System.Drawing.Point(75, 8);
            DKB2.Maximum = 0;
            DKB2.Name = "DKB2";
            DKB2.Size = new System.Drawing.Size(20, 20);
            DKB2.TabIndex = 12;
            DKB2.Text = "DKB2";
            TETT.SetToolTip(DKB2, "Dark Theme");
            DKB2.Value = 0;
            // 
            // MNPB
            // 
            MNPB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            MNPB.Enabled = false;
            MNPB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.Moon;
            MNPB.Location = new System.Drawing.Point(41, 8);
            MNPB.Name = "MNPB";
            MNPB.Size = new System.Drawing.Size(20, 20);
            MNPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            MNPB.TabIndex = 11;
            MNPB.TabStop = false;
            TETT.SetToolTip(MNPB, "Dark Theme");
            // 
            // DK
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(DKPL);
            Margin = new System.Windows.Forms.Padding(5);
            MaximumSize = new System.Drawing.Size(103, 97);
            MinimumSize = new System.Drawing.Size(103, 97);
            Name = "DK";
            Size = new System.Drawing.Size(103, 97);
            DKPL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(MNPB)).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.MetroToolTip TETT;
        private ReaLTaiizor.Controls.Panel DKPL;
        private ReaLTaiizor.Controls.Panel DKP2;
        private ReaLTaiizor.Controls.Panel DKP3;
        private ReaLTaiizor.Controls.Panel DKP1;
        private ReaLTaiizor.Controls.Badge DKB1;
        private ReaLTaiizor.Controls.Badge DKB2;
        private System.Windows.Forms.PictureBox MNPB;
    }
}
