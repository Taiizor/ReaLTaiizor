namespace ReaLTaiizor_CR
{
    partial class Ribbon
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
            this.ribbonForm1 = new ReaLTaiizor.RibbonForm();
            this.SuspendLayout();
            // 
            // ribbonForm1
            // 
            this.ribbonForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ribbonForm1.BaseColor = System.Drawing.Color.Fuchsia;
            this.ribbonForm1.BottomLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.ribbonForm1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.ribbonForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonForm1.HatchType = System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal;
            this.ribbonForm1.HeaderLineColorA = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ribbonForm1.HeaderLineColorB = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ribbonForm1.HeaderLineColorC = System.Drawing.Color.Black;
            this.ribbonForm1.Location = new System.Drawing.Point(0, 0);
            this.ribbonForm1.Name = "ribbonForm1";
            this.ribbonForm1.RibbonEdgeColorA = System.Drawing.Color.Black;
            this.ribbonForm1.RibbonEdgeColorB = System.Drawing.Color.Black;
            this.ribbonForm1.RibbonEdgeColorC = System.Drawing.Color.Black;
            this.ribbonForm1.RibbonEdgeColorD = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ribbonForm1.RibbonEdgeColorE = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(50)))));
            this.ribbonForm1.Size = new System.Drawing.Size(800, 450);
            this.ribbonForm1.SubTitle = "Ribbon";
            this.ribbonForm1.SubTitleColor = System.Drawing.Color.WhiteSmoke;
            this.ribbonForm1.SubTitleFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ribbonForm1.TabIndex = 0;
            this.ribbonForm1.Text = "ribbonForm1";
            // 
            // Ribbon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ribbonForm1);
            this.Name = "Ribbon";
            this.ShowIcon = false;
            this.Text = "Ribbon";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.RibbonForm ribbonForm1;
    }
}