using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Docking
{
    partial class DockConsole
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
            this.lstConsole = new ReaLTaiizor.Controls.CrownListView();
            this.SuspendLayout();
            // 
            // lstConsole
            // 
            this.lstConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConsole.Location = new System.Drawing.Point(0, 25);
            this.lstConsole.MultiSelect = true;
            this.lstConsole.Name = "lstConsole";
            this.lstConsole.Size = new System.Drawing.Size(500, 175);
            this.lstConsole.TabIndex = 0;
            this.lstConsole.Text = "crownListView1";
            // 
            // DockConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lstConsole);
            this.DefaultDockArea = ReaLTaiizor.Enum.Crown.DockArea.Bottom;
            this.DockText = "Console";
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = global::ReaLTaiizor.UI.Properties.Resources.Console;
            this.Name = "DockConsole";
            this.SerializationKey = "DockConsole";
            this.Size = new System.Drawing.Size(500, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private CrownListView lstConsole;
    }
}
