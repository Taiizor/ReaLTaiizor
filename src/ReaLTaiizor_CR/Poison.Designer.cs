namespace ReaLTaiizor_CR
{
    partial class Poison
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Poison));
            SuspendLayout();
            // 
            // Poison
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Poison";
            ShadowType = ReaLTaiizor.Enum.Poison.FormShadowType.AeroShadow;
            Style = ReaLTaiizor.Enum.Poison.ColorStyle.Yellow;
            Text = "Poison";
            TextAlign = ReaLTaiizor.Enum.Poison.FormTextAlignType.Center;
            Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            ResumeLayout(false);

        }

        #endregion
    }
}