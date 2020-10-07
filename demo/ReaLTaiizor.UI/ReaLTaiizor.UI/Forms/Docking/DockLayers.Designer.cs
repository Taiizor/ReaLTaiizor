using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Docking
{
    partial class DockLayers
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
            lstLayers = new ReaLTaiizor.Controls.CrownListView();
            cmbList = new ReaLTaiizor.Controls.CrownDropDownList();
            SuspendLayout();
            // 
            // lstLayers
            // 
            lstLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            lstLayers.HideScrollBars = false;
            lstLayers.Location = new System.Drawing.Point(0, 51);
            lstLayers.Name = "lstLayers";
            lstLayers.ShowIcons = true;
            lstLayers.Size = new System.Drawing.Size(280, 399);
            lstLayers.TabIndex = 0;
            lstLayers.Text = "crownListView1";
            // 
            // cmbList
            // 
            cmbList.Dock = System.Windows.Forms.DockStyle.Top;
            cmbList.Location = new System.Drawing.Point(0, 25);
            cmbList.Name = "cmbList";
            cmbList.ShowBorder = false;
            cmbList.Size = new System.Drawing.Size(280, 26);
            cmbList.TabIndex = 1;
            cmbList.Text = "crownDropdownList1";
            // 
            // DockLayers
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lstLayers);
            Controls.Add(cmbList);
            DefaultDockArea = Enum.Crown.DockArea.Right;
            DockText = "Layers";
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = global::ReaLTaiizor.UI.Properties.Resources.Collection_16xLG;
            Name = "DockLayers";
            SerializationKey = "DockLayers";
            Size = new System.Drawing.Size(280, 450);
            ResumeLayout(false);

        }

        #endregion

        private CrownListView lstLayers;
        private CrownDropDownList cmbList;
    }
}
