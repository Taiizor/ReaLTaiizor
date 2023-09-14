namespace ReaLTaiizor_CR
{
    partial class King
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
            this.parrotControlEllipse1 = new ReaLTaiizor.Controls.ParrotControlEllipse();
            this.SuspendLayout();
            // 
            // parrotControlEllipse1
            // 
            this.parrotControlEllipse1.CornerRadius = 10;
            this.parrotControlEllipse1.EffectedControl = null;
            // 
            // King
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(819, 526);
            this.Name = "King";
            this.Text = "King";
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.ParrotControlEllipse parrotControlEllipse1;
    }
}