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
            poisonTile1 = new ReaLTaiizor.Controls.PoisonTile();
            SuspendLayout();
            // 
            // poisonTile1
            // 
            poisonTile1.ActiveControl = null;
            poisonTile1.Location = new System.Drawing.Point(3, 3);
            poisonTile1.Name = "poisonTile1";
            poisonTile1.Size = new System.Drawing.Size(130, 83);
            poisonTile1.TabIndex = 0;
            poisonTile1.Text = "poisonTile1";
            poisonTile1.UseSelectable = true;
            // 
            // TaskWindowControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(poisonTile1);
            Name = "TaskWindowControl";
            Size = new System.Drawing.Size(227, 136);
            ResumeLayout(false);

        }

        #endregion

        private Controls.PoisonTile poisonTile1;
    }
}
