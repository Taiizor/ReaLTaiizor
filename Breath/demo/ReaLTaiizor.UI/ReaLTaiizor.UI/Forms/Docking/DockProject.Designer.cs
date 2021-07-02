using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Docking
{
    partial class DockProject
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
            this.treeProject = new ReaLTaiizor.Controls.CrownTreeView();
            this.SuspendLayout();
            // 
            // treeProject
            // 
            this.treeProject.AllowMoveNodes = true;
            this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProject.Location = new System.Drawing.Point(0, 25);
            this.treeProject.MaxDragChange = 20;
            this.treeProject.MultiSelect = true;
            this.treeProject.Name = "treeProject";
            this.treeProject.ShowIcons = true;
            this.treeProject.Size = new System.Drawing.Size(280, 425);
            this.treeProject.TabIndex = 0;
            this.treeProject.Text = "crownTreeView1";
            // 
            // DockProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.treeProject);
            this.DefaultDockArea = ReaLTaiizor.Enum.Crown.DockArea.Left;
            this.DockText = "Project Explorer";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ReaLTaiizor.UI.Properties.Resources.application_16x;
            this.Name = "DockProject";
            this.SerializationKey = "DockProject";
            this.Size = new System.Drawing.Size(280, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private CrownTreeView treeProject;
    }
}