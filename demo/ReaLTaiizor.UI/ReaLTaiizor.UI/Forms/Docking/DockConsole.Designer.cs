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
            lstConsole = new ReaLTaiizor.Controls.CrownListView();
            SuspendLayout();
            // 
            // lstConsole
            // 
            lstConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            lstConsole.Location = new System.Drawing.Point(0, 25);
            lstConsole.MultiSelect = true;
            lstConsole.Name = "lstConsole";
            lstConsole.Size = new System.Drawing.Size(500, 175);
            lstConsole.TabIndex = 0;
            lstConsole.Text = "crownListView1";
            // 
            // DockConsole
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(lstConsole);
            DefaultDockArea = Enum.Crown.DockArea.Bottom;
            DockText = "Console";
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = global::ReaLTaiizor.UI.Properties.Resources.Console;
            Name = "DockConsole";
            SerializationKey = "DockConsole";
            Size = new System.Drawing.Size(500, 200);
            ResumeLayout(false);

        }

        #endregion

        private CrownListView lstConsole;
    }
}
