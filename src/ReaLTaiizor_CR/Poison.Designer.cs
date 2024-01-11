
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
            this.components = new System.ComponentModel.Container();
            this.poisonStyleExtender1 = new ReaLTaiizor.Controls.PoisonStyleExtender(this.components);
            this.poisonToolTip1 = new ReaLTaiizor.Controls.PoisonToolTip();
            this.poisonStyleManager1 = new ReaLTaiizor.Manager.PoisonStyleManager(this.components);
            this.poisonButton1 = new ReaLTaiizor.Controls.PoisonButton();
            this.poisonButton2 = new ReaLTaiizor.Controls.PoisonButton();
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // poisonStyleExtender1
            // 
            this.poisonStyleExtender1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            // 
            // poisonToolTip1
            // 
            this.poisonToolTip1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Blue;
            this.poisonToolTip1.StyleManager = null;
            this.poisonToolTip1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            // 
            // poisonStyleManager1
            // 
            this.poisonStyleManager1.Owner = this;
            this.poisonStyleManager1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            // 
            // poisonButton1
            // 
            this.poisonButton1.Location = new System.Drawing.Point(23, 87);
            this.poisonButton1.Name = "poisonButton1";
            this.poisonButton1.Size = new System.Drawing.Size(122, 34);
            this.poisonButton1.TabIndex = 0;
            this.poisonButton1.Text = "poisonButton1";
            this.poisonButton1.UseSelectable = true;
            // 
            // poisonButton2
            // 
            this.poisonButton2.Location = new System.Drawing.Point(23, 127);
            this.poisonButton2.Name = "poisonButton2";
            this.poisonButton2.Size = new System.Drawing.Size(122, 34);
            this.poisonButton2.TabIndex = 1;
            this.poisonButton2.Text = "poisonButton2";
            this.poisonButton2.UseSelectable = true;
            // 
            // Poison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(488, 321);
            this.Controls.Add(this.poisonButton2);
            this.Controls.Add(this.poisonButton1);
            this.Icon = global::ReaLTaiizor_CR.Properties.Resources.ICO;
            this.Name = "Poison";
            this.ShadowType = ReaLTaiizor.Enum.Poison.FormShadowType.AeroShadow;
            this.Text = "Poison";
            this.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.poisonStyleManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.PoisonStyleExtender poisonStyleExtender1;
        private ReaLTaiizor.Controls.PoisonToolTip poisonToolTip1;
        private ReaLTaiizor.Manager.PoisonStyleManager poisonStyleManager1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton2;
    }
}