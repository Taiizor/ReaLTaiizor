namespace ReaLTaiizor_CR
{
    partial class Tester
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
            this.parrotCard1 = new ReaLTaiizor.Controls.ParrotCard();
            this.parrotBanner1 = new ReaLTaiizor.Controls.ParrotBanner();
            this.parrotObjectEllipse1 = new ReaLTaiizor.Controls.ParrotObjectEllipse();
            this.SuspendLayout();
            // 
            // parrotCard1
            // 
            this.parrotCard1.BackColor = System.Drawing.Color.Transparent;
            this.parrotCard1.Color1 = System.Drawing.Color.DodgerBlue;
            this.parrotCard1.Color2 = System.Drawing.Color.LimeGreen;
            this.parrotCard1.ForeColor = System.Drawing.Color.White;
            this.parrotCard1.Location = new System.Drawing.Point(143, 12);
            this.parrotCard1.Name = "parrotCard1";
            this.parrotCard1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotCard1.Size = new System.Drawing.Size(320, 170);
            this.parrotCard1.TabIndex = 1;
            this.parrotCard1.Text = "parrotCard1";
            this.parrotCard1.Text1 = "Savings Card";
            this.parrotCard1.Text2 = "1234 5678 9101 1121";
            this.parrotCard1.Text3 = "Exp: 01/02 - 03/04";
            this.parrotCard1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // parrotBanner1
            // 
            this.parrotBanner1.BackColor = System.Drawing.Color.Transparent;
            this.parrotBanner1.BannerColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(71)))), ((int)(((byte)(89)))));
            this.parrotBanner1.BorderColor = System.Drawing.Color.White;
            this.parrotBanner1.ForeColor = System.Drawing.Color.White;
            this.parrotBanner1.Location = new System.Drawing.Point(12, 12);
            this.parrotBanner1.Name = "parrotBanner1";
            this.parrotBanner1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotBanner1.Size = new System.Drawing.Size(125, 261);
            this.parrotBanner1.TabIndex = 0;
            this.parrotBanner1.Text = "parrotBanner1";
            this.parrotBanner1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            // 
            // parrotObjectEllipse1
            // 
            this.parrotObjectEllipse1.CornerRadius = 10;
            this.parrotObjectEllipse1.EffectedControl = null;
            this.parrotObjectEllipse1.EffectedForm = this;
            // 
            // Tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 282);
            this.Controls.Add(this.parrotCard1);
            this.Controls.Add(this.parrotBanner1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tester";
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotBanner parrotBanner1;
        private ReaLTaiizor.Controls.ParrotCard parrotCard1;
        private ReaLTaiizor.Controls.ParrotObjectEllipse parrotObjectEllipse1;
    }
}