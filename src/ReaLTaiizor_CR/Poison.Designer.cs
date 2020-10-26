
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Poison));
            poisonStyleExtender1 = new ReaLTaiizor.Controls.PoisonStyleExtender(components);
            poisonToolTip1 = new ReaLTaiizor.Controls.PoisonToolTip();
            poisonStyleManager1 = new ReaLTaiizor.Manager.PoisonStyleManager(components);
            ((System.ComponentModel.ISupportInitialize)(poisonStyleManager1)).BeginInit();
            SuspendLayout();
            // 
            // poisonStyleExtender1
            // 
            poisonStyleExtender1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            // 
            // poisonToolTip1
            // 
            poisonToolTip1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Blue;
            poisonToolTip1.StyleManager = null;
            poisonToolTip1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // poisonStyleManager1
            // 
            poisonStyleManager1.Owner = this;
            poisonStyleManager1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            // 
            // Poison
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(488, 321);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "Poison";
            ShadowType = ReaLTaiizor.Enum.Poison.FormShadowType.AeroShadow;
            Text = "Poison";
            Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(poisonStyleManager1)).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.PoisonStyleExtender poisonStyleExtender1;
        private ReaLTaiizor.Controls.PoisonToolTip poisonToolTip1;
        private ReaLTaiizor.Manager.PoisonStyleManager poisonStyleManager1;
    }
}