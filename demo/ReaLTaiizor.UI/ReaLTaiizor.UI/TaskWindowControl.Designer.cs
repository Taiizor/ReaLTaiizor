namespace ReaLTaiizor.UI
{
    partial class TaskWindowControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.poisonTile1 = new ReaLTaiizor.Controls.PoisonTile();
            this.SuspendLayout();
            // 
            // poisonTile1
            // 
            this.poisonTile1.ActiveControl = null;
            this.poisonTile1.Location = new System.Drawing.Point(3, 3);
            this.poisonTile1.Name = "poisonTile1";
            this.poisonTile1.Size = new System.Drawing.Size(130, 83);
            this.poisonTile1.TabIndex = 0;
            this.poisonTile1.Text = "poisonTile1";
            this.poisonTile1.UseSelectable = true;
            // 
            // TaskWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.poisonTile1);
            this.Name = "TaskWindowControl";
            this.Size = new System.Drawing.Size(227, 136);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PoisonTile poisonTile1;
    }
}
