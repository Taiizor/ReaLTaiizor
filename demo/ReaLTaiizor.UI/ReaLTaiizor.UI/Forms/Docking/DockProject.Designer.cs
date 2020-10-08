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
            treeProject = new ReaLTaiizor.Controls.CrownTreeView();
            SuspendLayout();
            // 
            // treeProject
            // 
            treeProject.AllowMoveNodes = true;
            treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            treeProject.Location = new System.Drawing.Point(0, 25);
            treeProject.MaxDragChange = 20;
            treeProject.MultiSelect = true;
            treeProject.Name = "treeProject";
            treeProject.ShowIcons = true;
            treeProject.Size = new System.Drawing.Size(280, 425);
            treeProject.TabIndex = 0;
            treeProject.Text = "crownTreeView1";
            // 
            // DockProject
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(treeProject);
            DefaultDockArea = Enum.Crown.DockArea.Left;
            DockText = "Project Explorer";
			BackColor = System.Drawing.Color.Transparent;
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = global::ReaLTaiizor.UI.Properties.Resources.application_16x;
            Name = "DockProject";
            SerializationKey = "DockProject";
            Size = new System.Drawing.Size(280, 450);
            ResumeLayout(false);

        }

        #endregion

        private CrownTreeView treeProject;
    }
}