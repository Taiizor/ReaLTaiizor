namespace ReaLTaiizor.Nerator.UC.THEME
{
    partial class LT
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
            LTPL = new ReaLTaiizor.Controls.Panel();
            LTP2 = new ReaLTaiizor.Controls.Panel();
            LTP3 = new ReaLTaiizor.Controls.Panel();
            LTP1 = new ReaLTaiizor.Controls.Panel();
            LTB1 = new ReaLTaiizor.Controls.Badge();
            LTB2 = new ReaLTaiizor.Controls.Badge();
            SNPB = new System.Windows.Forms.PictureBox();
            TETT = new ReaLTaiizor.Controls.MetroToolTip();
            LTPL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(SNPB)).BeginInit();
            SuspendLayout();
            // 
            // LTPL
            // 
            LTPL.BackColor = System.Drawing.Color.White;
            LTPL.Controls.Add(LTP2);
            LTPL.Controls.Add(LTP3);
            LTPL.Controls.Add(LTP1);
            LTPL.Controls.Add(LTB1);
            LTPL.Controls.Add(LTB2);
            LTPL.Controls.Add(SNPB);
            LTPL.Cursor = System.Windows.Forms.Cursors.Hand;
            LTPL.Dock = System.Windows.Forms.DockStyle.Fill;
            LTPL.EdgeColor = System.Drawing.SystemColors.Control;
            LTPL.Location = new System.Drawing.Point(0, 0);
            LTPL.Margin = new System.Windows.Forms.Padding(0);
            LTPL.MaximumSize = new System.Drawing.Size(103, 97);
            LTPL.MinimumSize = new System.Drawing.Size(103, 97);
            LTPL.Name = "LTPL";
            LTPL.Padding = new System.Windows.Forms.Padding(5);
            LTPL.Size = new System.Drawing.Size(103, 97);
            LTPL.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            LTPL.TabIndex = 11;
            LTPL.Text = "LTPL";
            TETT.SetToolTip(LTPL, "Light Theme");
            LTPL.Click += new System.EventHandler(LTPL_Click);
            LTPL.MouseEnter += new System.EventHandler(LTPL_MouseEnter);
            LTPL.MouseLeave += new System.EventHandler(LTPL_MouseLeave);
            // 
            // LTP2
            // 
            LTP2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            LTP2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTP2.EdgeColor = System.Drawing.Color.White;
            LTP2.Enabled = false;
            LTP2.Location = new System.Drawing.Point(34, 34);
            LTP2.Name = "LTP2";
            LTP2.Padding = new System.Windows.Forms.Padding(5);
            LTP2.Size = new System.Drawing.Size(35, 55);
            LTP2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            LTP2.TabIndex = 15;
            LTP2.Text = "LTP2";
            TETT.SetToolTip(LTP2, "Light Theme");
            // 
            // LTP3
            // 
            LTP3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            LTP3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTP3.EdgeColor = System.Drawing.Color.White;
            LTP3.Enabled = false;
            LTP3.Location = new System.Drawing.Point(75, 34);
            LTP3.Name = "LTP3";
            LTP3.Padding = new System.Windows.Forms.Padding(5);
            LTP3.Size = new System.Drawing.Size(20, 55);
            LTP3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            LTP3.TabIndex = 14;
            LTP3.Text = "LTP3";
            TETT.SetToolTip(LTP3, "Light Theme");
            // 
            // LTP1
            // 
            LTP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            LTP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTP1.EdgeColor = System.Drawing.Color.White;
            LTP1.Enabled = false;
            LTP1.Location = new System.Drawing.Point(8, 34);
            LTP1.Name = "LTP1";
            LTP1.Padding = new System.Windows.Forms.Padding(5);
            LTP1.Size = new System.Drawing.Size(20, 55);
            LTP1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            LTP1.TabIndex = 11;
            LTP1.Text = "LTP1";
            TETT.SetToolTip(LTP1, "Light Theme");
            // 
            // LTB1
            // 
            LTB1.BackColor = System.Drawing.Color.White;
            LTB1.BGColorA = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB1.BGColorB = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB1.Enabled = false;
            LTB1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            LTB1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB1.Location = new System.Drawing.Point(8, 8);
            LTB1.Maximum = 0;
            LTB1.Name = "LTB1";
            LTB1.Size = new System.Drawing.Size(20, 20);
            LTB1.TabIndex = 13;
            LTB1.Text = "LTB1";
            TETT.SetToolTip(LTB1, "Light Theme");
            LTB1.Value = 0;
            // 
            // LTB2
            // 
            LTB2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            LTB2.BackColor = System.Drawing.Color.White;
            LTB2.BGColorA = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB2.BGColorB = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB2.Enabled = false;
            LTB2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            LTB2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(243)))), ((int)(((byte)(245)))));
            LTB2.Location = new System.Drawing.Point(75, 8);
            LTB2.Maximum = 0;
            LTB2.Name = "LTB2";
            LTB2.Size = new System.Drawing.Size(20, 20);
            LTB2.TabIndex = 12;
            LTB2.Text = "LTB2";
            TETT.SetToolTip(LTB2, "Light Theme");
            LTB2.Value = 0;
            // 
            // SNPB
            // 
            SNPB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            SNPB.Enabled = false;
            SNPB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.Sun;
            SNPB.Location = new System.Drawing.Point(41, 8);
            SNPB.Name = "SNPB";
            SNPB.Size = new System.Drawing.Size(20, 20);
            SNPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            SNPB.TabIndex = 11;
            SNPB.TabStop = false;
            TETT.SetToolTip(SNPB, "Light Theme");
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
            // LT
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(LTPL);
            Margin = new System.Windows.Forms.Padding(5);
            MaximumSize = new System.Drawing.Size(103, 97);
            MinimumSize = new System.Drawing.Size(103, 97);
            Name = "LT";
            Size = new System.Drawing.Size(103, 97);
            LTPL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(SNPB)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.Panel LTPL;
        private ReaLTaiizor.Controls.Panel LTP2;
        private ReaLTaiizor.Controls.Panel LTP3;
        private ReaLTaiizor.Controls.Panel LTP1;
        private ReaLTaiizor.Controls.Badge LTB1;
        private ReaLTaiizor.Controls.Badge LTB2;
        private System.Windows.Forms.PictureBox SNPB;
        private ReaLTaiizor.Controls.MetroToolTip TETT;
    }
}
