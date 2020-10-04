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
            this.poisonToggle1 = new ReaLTaiizor.Controls.PoisonToggle();
            this.poisonButton1 = new ReaLTaiizor.Controls.PoisonButton();
            this.SuspendLayout();
            // 
            // poisonToggle1
            // 
            this.poisonToggle1.AutoSize = true;
            this.poisonToggle1.Checked = true;
            this.poisonToggle1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.poisonToggle1.Location = new System.Drawing.Point(315, 214);
            this.poisonToggle1.Name = "poisonToggle1";
            this.poisonToggle1.Size = new System.Drawing.Size(80, 17);
            this.poisonToggle1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Yellow;
            this.poisonToggle1.TabIndex = 0;
            this.poisonToggle1.Text = "On";
            this.poisonToggle1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            this.poisonToggle1.UseSelectable = true;
            // 
            // poisonButton1
            // 
            this.poisonButton1.Location = new System.Drawing.Point(398, 114);
            this.poisonButton1.Name = "poisonButton1";
            this.poisonButton1.Size = new System.Drawing.Size(180, 67);
            this.poisonButton1.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Green;
            this.poisonButton1.TabIndex = 1;
            this.poisonButton1.Text = "poisonButton1";
            this.poisonButton1.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Dark;
            this.poisonButton1.UseSelectable = true;
            this.poisonButton1.Click += new System.EventHandler(this.poisonButton1_Click);
            // 
            // Poison
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.poisonButton1);
            this.Controls.Add(this.poisonToggle1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Poison";
            this.ShadowType = ReaLTaiizor.Enum.Poison.FormShadowType.AeroShadow;
            this.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Yellow;
            this.Text = "Poison";
            this.TextAlign = ReaLTaiizor.Enum.Poison.FormTextAlignType.Center;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.PoisonToggle poisonToggle1;
        private ReaLTaiizor.Controls.PoisonButton poisonButton1;
    }
}