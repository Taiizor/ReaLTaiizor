
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
            poisonStyleExtender1 = new ReaLTaiizor.Controls.PoisonStyleExtender(components);
            poisonToolTip1 = new ReaLTaiizor.Controls.PoisonToolTip();
            poisonStyleManager1 = new ReaLTaiizor.Manager.PoisonStyleManager(components);
            poisonButton1 = new ReaLTaiizor.Controls.PoisonButton();
            poisonButton2 = new ReaLTaiizor.Controls.PoisonButton();
            ((System.ComponentModel.ISupportInitialize)poisonStyleManager1).BeginInit();
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
            // poisonButton1
            // 
            poisonButton1.Location = new System.Drawing.Point(23, 87);
            poisonButton1.Name = "poisonButton1";
            poisonButton1.Size = new System.Drawing.Size(122, 34);
            poisonButton1.TabIndex = 0;
            poisonButton1.Text = "poisonButton1";
            poisonButton1.Click += poisonButton1_Click;
            // 
            // poisonButton2
            // 
            poisonButton2.Location = new System.Drawing.Point(23, 127);
            poisonButton2.Name = "poisonButton2";
            poisonButton2.Size = new System.Drawing.Size(122, 34);
            poisonButton2.TabIndex = 1;
            poisonButton2.Text = "poisonButton2";
            poisonButton2.Click += poisonButton2_Click;
            // 
            // Poison
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(488, 321);
            Controls.Add(poisonButton2);
            Controls.Add(poisonButton1);
            Icon = Properties.Resources.ICO;
            Name = "Poison";
            ShadowType = ReaLTaiizor.Enum.Poison.FormShadowType.AeroShadow;
            Text = "Poison";
            Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)poisonStyleManager1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private ReaLTaiizor.Controls.PoisonStyleExtender poisonStyleExtender1;
        private ReaLTaiizor.Controls.PoisonToolTip poisonToolTip1;
        private ReaLTaiizor.Manager.PoisonStyleManager poisonStyleManager1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton2;
    }
}