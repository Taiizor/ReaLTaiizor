
namespace ReaLTaiizor.Nerator.UC.LIGHT.HISTORY
{
    partial class PWD
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
            COPY = new ReaLTaiizor.Controls.MaterialButton();
            REMOVE = new ReaLTaiizor.Controls.MaterialButton();
            FILL = new System.Windows.Forms.Panel();
            PASSWORD = new ReaLTaiizor.Controls.PoisonLabel();
            TIMEDATE = new ReaLTaiizor.Controls.PoisonLabel();
            FILL.SuspendLayout();
            SuspendLayout();
            // 
            // COPY
            // 
            COPY.AutoSize = false;
            COPY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            COPY.BackColor = System.Drawing.Color.Transparent;
            COPY.Cursor = System.Windows.Forms.Cursors.Hand;
            COPY.Depth = 0;
            COPY.Dock = System.Windows.Forms.DockStyle.Right;
            COPY.DrawShadows = false;
            COPY.HighEmphasis = true;
            COPY.Icon = global::ReaLTaiizor.Nerator.Properties.Resources.Copy;
            COPY.Location = new System.Drawing.Point(224, 0);
            COPY.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            COPY.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            COPY.Name = "COPY";
            COPY.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Default;
            COPY.Size = new System.Drawing.Size(44, 36);
            COPY.TabIndex = 3;
            COPY.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Text;
            COPY.UseAccentColor = false;
            COPY.UseVisualStyleBackColor = false;
            COPY.Click += new System.EventHandler(COPY_Click);
            // 
            // REMOVE
            // 
            REMOVE.AutoSize = false;
            REMOVE.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            REMOVE.BackColor = System.Drawing.Color.Transparent;
            REMOVE.Cursor = System.Windows.Forms.Cursors.Hand;
            REMOVE.Depth = 0;
            REMOVE.Dock = System.Windows.Forms.DockStyle.Right;
            REMOVE.DrawShadows = false;
            REMOVE.HighEmphasis = true;
            REMOVE.Icon = global::ReaLTaiizor.Nerator.Properties.Resources.Delete;
            REMOVE.Location = new System.Drawing.Point(268, 0);
            REMOVE.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            REMOVE.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            REMOVE.Name = "REMOVE";
            REMOVE.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Default;
            REMOVE.Size = new System.Drawing.Size(44, 36);
            REMOVE.TabIndex = 4;
            REMOVE.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Text;
            REMOVE.UseAccentColor = true;
            REMOVE.UseVisualStyleBackColor = false;
            REMOVE.Click += new System.EventHandler(REMOVE_Click);
            // 
            // FILL
            // 
            FILL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            FILL.Controls.Add(PASSWORD);
            FILL.Controls.Add(TIMEDATE);
            FILL.Controls.Add(COPY);
            FILL.Controls.Add(REMOVE);
            FILL.Dock = System.Windows.Forms.DockStyle.Fill;
            FILL.Location = new System.Drawing.Point(0, 1);
            FILL.Margin = new System.Windows.Forms.Padding(0);
            FILL.Name = "FILL";
            FILL.Size = new System.Drawing.Size(312, 36);
            FILL.TabIndex = 0;
            // 
            // PASSWORD
            // 
            PASSWORD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            PASSWORD.BackColor = System.Drawing.Color.Transparent;
            PASSWORD.FontWeight = ReaLTaiizor.Extension.Poison.PoisonLabelWeight.Bold;
            PASSWORD.ForeColor = System.Drawing.Color.SeaGreen;
            PASSWORD.Location = new System.Drawing.Point(74, 0);
            PASSWORD.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            PASSWORD.Name = "PASSWORD";
            PASSWORD.Size = new System.Drawing.Size(148, 36);
            PASSWORD.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Green;
            PASSWORD.TabIndex = 6;
            PASSWORD.Text = "PASSWORD PASSWORD PASSWORD PASSWORD";
            PASSWORD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            PASSWORD.UseCustomBackColor = true;
            PASSWORD.UseCustomForeColor = true;
            // 
            // TIMEDATE
            // 
            TIMEDATE.BackColor = System.Drawing.Color.Transparent;
            TIMEDATE.Dock = System.Windows.Forms.DockStyle.Left;
            TIMEDATE.FontSize = ReaLTaiizor.Extension.Poison.PoisonLabelSize.Small;
            TIMEDATE.FontWeight = ReaLTaiizor.Extension.Poison.PoisonLabelWeight.Bold;
            TIMEDATE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            TIMEDATE.Location = new System.Drawing.Point(0, 0);
            TIMEDATE.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            TIMEDATE.Name = "TIMEDATE";
            TIMEDATE.Size = new System.Drawing.Size(72, 36);
            TIMEDATE.TabIndex = 5;
            TIMEDATE.Text = "00:00:00\r\n00.00.0000";
            TIMEDATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            TIMEDATE.UseCustomBackColor = true;
            TIMEDATE.UseCustomForeColor = true;
            // 
            // PWD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(FILL);
            Margin = new System.Windows.Forms.Padding(0);
            MaximumSize = new System.Drawing.Size(329, 38);
            MinimumSize = new System.Drawing.Size(312, 38);
            Name = "PWD";
            Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            Size = new System.Drawing.Size(312, 38);
            FILL.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.MaterialButton COPY;
        private ReaLTaiizor.Controls.MaterialButton REMOVE;
        private System.Windows.Forms.Panel FILL;
        private ReaLTaiizor.Controls.PoisonLabel PASSWORD;
        private ReaLTaiizor.Controls.PoisonLabel TIMEDATE;
    }
}
