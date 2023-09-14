namespace ReaLTaiizor.Splash
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.parrotObjectEllipse1 = new ReaLTaiizor.Controls.ParrotObjectEllipse();
            this.poisonProgressSpinner1 = new ReaLTaiizor.Controls.PoisonProgressSpinner();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.poisonStyleManager1 = new ReaLTaiizor.Manager.PoisonStyleManager(this.components);
            this.poisonLabel1 = new ReaLTaiizor.Controls.PoisonLabel();
            this.spaceSeparatorVertical1 = new ReaLTaiizor.Controls.SpaceSeparatorVertical();
            this.spaceSeparatorHorizontal21 = new ReaLTaiizor.Controls.SpaceSeparatorHorizontal();
            this.spaceSeparatorVertical2 = new ReaLTaiizor.Controls.SpaceSeparatorVertical();
            this.spaceSeparatorHorizontal22 = new ReaLTaiizor.Controls.SpaceSeparatorHorizontal();
            this.spaceSeparatorHorizontal23 = new ReaLTaiizor.Controls.SpaceSeparatorHorizontal();
            this.parrotPictureBox1 = new ReaLTaiizor.Controls.ParrotPictureBox();
            this.parrotFormHandle1 = new ReaLTaiizor.Controls.ParrotFormHandle();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // parrotObjectEllipse1
            // 
            this.parrotObjectEllipse1.CornerRadius = 10;
            this.parrotObjectEllipse1.EffectedControl = null;
            this.parrotObjectEllipse1.EffectedForm = this;
            // 
            // poisonProgressSpinner1
            // 
            this.poisonProgressSpinner1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.poisonProgressSpinner1.BackColor = System.Drawing.Color.Black;
            this.poisonProgressSpinner1.Backwards = true;
            this.poisonProgressSpinner1.CustomBackground = true;
            this.poisonProgressSpinner1.EnsureVisible = false;
            this.poisonProgressSpinner1.Location = new System.Drawing.Point(21, 250);
            this.poisonProgressSpinner1.Maximum = 100;
            this.poisonProgressSpinner1.Name = "poisonProgressSpinner1";
            this.poisonProgressSpinner1.Size = new System.Drawing.Size(128, 128);
            this.poisonProgressSpinner1.Speed = 2F;
            this.poisonProgressSpinner1.TabIndex = 1;
            this.poisonProgressSpinner1.UseCustomBackColor = true;
            this.poisonProgressSpinner1.UseSelectable = true;
            this.poisonProgressSpinner1.UseStyleColors = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 8;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::ReaLTaiizor.Splash.Properties.Resources.Moon;
            this.pictureBox1.Location = new System.Drawing.Point(61, 290);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // poisonStyleManager1
            // 
            this.poisonStyleManager1.Owner = this;
            this.poisonStyleManager1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            // 
            // poisonLabel1
            // 
            this.poisonLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.poisonLabel1.BackColor = System.Drawing.Color.Black;
            this.poisonLabel1.FontSize = ReaLTaiizor.Extension.Poison.PoisonLabelSize.Tall;
            this.poisonLabel1.FontWeight = ReaLTaiizor.Extension.Poison.PoisonLabelWeight.Regular;
            this.poisonLabel1.Location = new System.Drawing.Point(267, 356);
            this.poisonLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.poisonLabel1.Name = "poisonLabel1";
            this.poisonLabel1.Size = new System.Drawing.Size(267, 25);
            this.poisonLabel1.TabIndex = 4;
            this.poisonLabel1.Text = "Wait A Moments.. 0%";
            this.poisonLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.poisonLabel1.UseCustomBackColor = true;
            this.poisonLabel1.UseStyleColors = true;
            // 
            // spaceSeparatorVertical1
            // 
            this.spaceSeparatorVertical1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spaceSeparatorVertical1.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            this.spaceSeparatorVertical1.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceSeparatorVertical1.Image = null;
            this.spaceSeparatorVertical1.Location = new System.Drawing.Point(11, 250);
            this.spaceSeparatorVertical1.Name = "spaceSeparatorVertical1";
            this.spaceSeparatorVertical1.NoRounding = false;
            this.spaceSeparatorVertical1.Size = new System.Drawing.Size(4, 128);
            this.spaceSeparatorVertical1.TabIndex = 5;
            this.spaceSeparatorVertical1.Text = "spaceSeparatorVertical1";
            this.spaceSeparatorVertical1.Transparent = false;
            // 
            // spaceSeparatorHorizontal21
            // 
            this.spaceSeparatorHorizontal21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spaceSeparatorHorizontal21.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            this.spaceSeparatorHorizontal21.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceSeparatorHorizontal21.Image = null;
            this.spaceSeparatorHorizontal21.Location = new System.Drawing.Point(21, 384);
            this.spaceSeparatorHorizontal21.Name = "spaceSeparatorHorizontal21";
            this.spaceSeparatorHorizontal21.NoRounding = false;
            this.spaceSeparatorHorizontal21.Size = new System.Drawing.Size(128, 4);
            this.spaceSeparatorHorizontal21.TabIndex = 6;
            this.spaceSeparatorHorizontal21.Text = "spaceSeparatorHorizontal21";
            this.spaceSeparatorHorizontal21.Transparent = false;
            // 
            // spaceSeparatorVertical2
            // 
            this.spaceSeparatorVertical2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spaceSeparatorVertical2.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            this.spaceSeparatorVertical2.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceSeparatorVertical2.Image = null;
            this.spaceSeparatorVertical2.Location = new System.Drawing.Point(155, 250);
            this.spaceSeparatorVertical2.Name = "spaceSeparatorVertical2";
            this.spaceSeparatorVertical2.NoRounding = false;
            this.spaceSeparatorVertical2.Size = new System.Drawing.Size(4, 128);
            this.spaceSeparatorVertical2.TabIndex = 7;
            this.spaceSeparatorVertical2.Text = "spaceSeparatorVertical2";
            this.spaceSeparatorVertical2.Transparent = false;
            // 
            // spaceSeparatorHorizontal22
            // 
            this.spaceSeparatorHorizontal22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.spaceSeparatorHorizontal22.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            this.spaceSeparatorHorizontal22.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceSeparatorHorizontal22.Image = null;
            this.spaceSeparatorHorizontal22.Location = new System.Drawing.Point(21, 240);
            this.spaceSeparatorHorizontal22.Name = "spaceSeparatorHorizontal22";
            this.spaceSeparatorHorizontal22.NoRounding = false;
            this.spaceSeparatorHorizontal22.Size = new System.Drawing.Size(128, 4);
            this.spaceSeparatorHorizontal22.TabIndex = 8;
            this.spaceSeparatorHorizontal22.Text = "spaceSeparatorHorizontal22";
            this.spaceSeparatorHorizontal22.Transparent = false;
            // 
            // spaceSeparatorHorizontal23
            // 
            this.spaceSeparatorHorizontal23.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spaceSeparatorHorizontal23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(16)))));
            this.spaceSeparatorHorizontal23.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            this.spaceSeparatorHorizontal23.Font = new System.Drawing.Font("Verdana", 8F);
            this.spaceSeparatorHorizontal23.Image = null;
            this.spaceSeparatorHorizontal23.Location = new System.Drawing.Point(300, 384);
            this.spaceSeparatorHorizontal23.Name = "spaceSeparatorHorizontal23";
            this.spaceSeparatorHorizontal23.NoRounding = false;
            this.spaceSeparatorHorizontal23.Size = new System.Drawing.Size(200, 4);
            this.spaceSeparatorHorizontal23.TabIndex = 9;
            this.spaceSeparatorHorizontal23.Text = "spaceSeparatorHorizontal23";
            this.spaceSeparatorHorizontal23.Transparent = false;
            // 
            // parrotPictureBox1
            // 
            this.parrotPictureBox1.BackColor = System.Drawing.Color.Black;
            this.parrotPictureBox1.ColorLeft = System.Drawing.Color.Black;
            this.parrotPictureBox1.ColorRight = System.Drawing.Color.Black;
            this.parrotPictureBox1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.parrotPictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotPictureBox1.FilterAlpha = 0;
            this.parrotPictureBox1.FilterEnabled = true;
            this.parrotPictureBox1.Image = global::ReaLTaiizor.Splash.Properties.Resources.Nebula;
            this.parrotPictureBox1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.parrotPictureBox1.IsElipse = false;
            this.parrotPictureBox1.IsParallax = false;
            this.parrotPictureBox1.Location = new System.Drawing.Point(0, 0);
            this.parrotPictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.parrotPictureBox1.Name = "parrotPictureBox1";
            this.parrotPictureBox1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotPictureBox1.Size = new System.Drawing.Size(800, 400);
            this.parrotPictureBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotPictureBox1.TabIndex = 11;
            this.parrotPictureBox1.Text = "parrotPictureBox1";
            this.parrotPictureBox1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // parrotFormHandle1
            // 
            this.parrotFormHandle1.DockAtTop = true;
            this.parrotFormHandle1.HandleControl = this.parrotPictureBox1;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(16)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.ControlBox = false;
            this.Controls.Add(this.spaceSeparatorHorizontal23);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.spaceSeparatorHorizontal22);
            this.Controls.Add(this.spaceSeparatorVertical2);
            this.Controls.Add(this.spaceSeparatorHorizontal21);
            this.Controls.Add(this.spaceSeparatorVertical1);
            this.Controls.Add(this.poisonLabel1);
            this.Controls.Add(this.poisonProgressSpinner1);
            this.Controls.Add(this.parrotPictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ParrotObjectEllipse parrotObjectEllipse1;
        private Controls.PoisonProgressSpinner poisonProgressSpinner1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Manager.PoisonStyleManager poisonStyleManager1;
        private Controls.PoisonLabel poisonLabel1;
        private Controls.SpaceSeparatorHorizontal spaceSeparatorHorizontal22;
        private Controls.SpaceSeparatorVertical spaceSeparatorVertical2;
        private Controls.SpaceSeparatorHorizontal spaceSeparatorHorizontal21;
        private Controls.SpaceSeparatorVertical spaceSeparatorVertical1;
        private Controls.SpaceSeparatorHorizontal spaceSeparatorHorizontal23;
        private Controls.ParrotPictureBox parrotPictureBox1;
        private Controls.ParrotFormHandle parrotFormHandle1;
    }
}