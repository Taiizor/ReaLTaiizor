using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Docking
{
    partial class DockHistory
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
            lstHistory = new ReaLTaiizor.Controls.CrownListView();
            SuspendLayout();
            // 
            // lstHistory
            // 
            lstHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            lstHistory.Location = new System.Drawing.Point(0, 25);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new System.Drawing.Size(280, 425);
            lstHistory.TabIndex = 0;
            lstHistory.Text = "CrownListView1";
            // 
            // DockHistory
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lstHistory);
            DefaultDockArea = Enum.Crown.DockArea.Right;
            DockText = "History";
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = global::ReaLTaiizor.UI.Properties.Resources.RefactoringLog_12810;
            Name = "DockHistory";
            SerializationKey = "DockHistory";
            Size = new System.Drawing.Size(280, 450);
            ResumeLayout(false);

        }

        #endregion

        private CrownListView lstHistory;
    }
}