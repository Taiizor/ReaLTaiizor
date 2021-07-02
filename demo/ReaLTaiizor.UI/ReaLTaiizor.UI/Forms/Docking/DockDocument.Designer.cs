namespace ReaLTaiizor.UI.Forms.Docking
{
    partial class DockDocument
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.cmbOptions = new ReaLTaiizor.Controls.CrownDropDownList();
            this.SuspendLayout();
            // 
            // txtDocument
            // 
            this.txtDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txtDocument.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDocument.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtDocument.Location = new System.Drawing.Point(0, 0);
            this.txtDocument.Multiline = true;
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(175, 173);
            this.txtDocument.TabIndex = 1;
            this.txtDocument.Text = "This is some example text";
            // 
            // cmbOptions
            // 
            this.cmbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOptions.DropdownDirection = System.Windows.Forms.ToolStripDropDownDirection.AboveRight;
            this.cmbOptions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOptions.Location = new System.Drawing.Point(0, 158);
            this.cmbOptions.MaxHeight = 300;
            this.cmbOptions.Name = "cmbOptions";
            this.cmbOptions.ShowBorder = false;
            this.cmbOptions.Size = new System.Drawing.Size(65, 15);
            this.cmbOptions.TabIndex = 2;
            this.cmbOptions.Text = "crownComboBox1";
            // 
            // DockDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cmbOptions);
            this.Controls.Add(this.txtDocument);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockDocument";
            this.Size = new System.Drawing.Size(175, 173);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDocument;
        private ReaLTaiizor.Controls.CrownDropDownList cmbOptions;
    }
}