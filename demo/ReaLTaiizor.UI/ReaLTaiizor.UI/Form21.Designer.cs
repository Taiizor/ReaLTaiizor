using ReaLTaiizor.Controls;
using ReaLTaiizor.Docking.Crown;

namespace ReaLTaiizor.UI
{
    partial class Form21
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form21));
            mnuMain = new ReaLTaiizor.Controls.CrownMenuStrip();
            mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            mnuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            mnuView = new System.Windows.Forms.ToolStripMenuItem();
            mnuDialog = new System.Windows.Forms.ToolStripMenuItem();
            mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            checkableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkableWithIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            checkedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkedWithIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            mnuProperties = new System.Windows.Forms.ToolStripMenuItem();
            mnuConsole = new System.Windows.Forms.ToolStripMenuItem();
            mnuLayers = new System.Windows.Forms.ToolStripMenuItem();
            mnuHistory = new System.Windows.Forms.ToolStripMenuItem();
            toolMain = new ReaLTaiizor.Controls.CrownToolStrip();
            btnNewFile = new System.Windows.Forms.ToolStripButton();
            stripMain = new ReaLTaiizor.Controls.CrownStatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            DockPanel = new ReaLTaiizor.Docking.Crown.CrownDockPanel();
            CrownSeparator1 = new ReaLTaiizor.Controls.CrownSeparator();
            themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mnuMain.SuspendLayout();
            toolMain.SuspendLayout();
            stripMain.SuspendLayout();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            mnuMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuFile,
            mnuView,
            mnuTools,
            mnuWindow});
            mnuMain.Location = new System.Drawing.Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            mnuMain.Size = new System.Drawing.Size(944, 24);
            mnuMain.TabIndex = 0;
            mnuMain.Text = "CrownMenuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuNewFile,
            toolStripSeparator1,
            mnuClose});
            mnuFile.BackColor = System.Drawing.Color.Transparent;
            mnuFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new System.Drawing.Size(37, 20);
            mnuFile.Text = "&File";
            // 
            // mnuNewFile
            // 
            mnuNewFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuNewFile.Image = global::ReaLTaiizor.UI.Properties.Resources.NewFile_6276;
            mnuNewFile.Name = "mnuNewFile";
            mnuNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            mnuNewFile.Size = new System.Drawing.Size(160, 22);
            mnuNewFile.Text = "&New file";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.BackColor = System.Drawing.Color.Transparent;
            toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            toolStripSeparator1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // mnuClose
            // 
            mnuClose.BackColor = System.Drawing.Color.Transparent;
            mnuClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuClose.Image = global::ReaLTaiizor.UI.Properties.Resources.Close_16xLG;
            mnuClose.Name = "mnuClose";
            mnuClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            mnuClose.Size = new System.Drawing.Size(160, 22);
            mnuClose.Text = "&Close";
            // 
            // mnuView
            // 
            mnuView.BackColor = System.Drawing.Color.Transparent;
            mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuDialog,
            themeToolStripMenuItem});
            mnuView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            mnuView.Name = "mnuView";
            mnuView.Size = new System.Drawing.Size(44, 20);
            mnuView.Text = "&View";
            // 
            // mnuDialog
            // 
            mnuDialog.BackColor = System.Drawing.Color.Transparent;
            mnuDialog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuDialog.Image = global::ReaLTaiizor.UI.Properties.Resources.properties_16xLG;
            mnuDialog.Name = "mnuDialog";
            mnuDialog.Size = new System.Drawing.Size(180, 22);
            mnuDialog.Text = "&Dialog test";
            // 
            // mnuTools
            // 
            mnuTools.BackColor = System.Drawing.Color.Transparent;
            mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            checkableToolStripMenuItem,
            checkableWithIconToolStripMenuItem,
            toolStripSeparator2,
            checkedToolStripMenuItem,
            checkedWithIconToolStripMenuItem});
            mnuTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuTools.Name = "mnuTools";
            mnuTools.Size = new System.Drawing.Size(46, 20);
            mnuTools.Text = "&Tools";
            // 
            // checkableToolStripMenuItem
            // 
            checkableToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            checkableToolStripMenuItem.CheckOnClick = true;
            checkableToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            checkableToolStripMenuItem.Name = "checkableToolStripMenuItem";
            checkableToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            checkableToolStripMenuItem.Text = "Checkable";
            // 
            // checkableWithIconToolStripMenuItem
            // 
            checkableWithIconToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            checkableWithIconToolStripMenuItem.CheckOnClick = true;
            checkableWithIconToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            checkableWithIconToolStripMenuItem.Image = global::ReaLTaiizor.UI.Properties.Resources.properties_16xLG;
            checkableWithIconToolStripMenuItem.Name = "checkableWithIconToolStripMenuItem";
            checkableWithIconToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            checkableWithIconToolStripMenuItem.Text = "Checkable with icon";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.BackColor = System.Drawing.Color.Transparent;
            toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            toolStripSeparator2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(178, 6);
            // 
            // checkedToolStripMenuItem
            // 
            checkedToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            checkedToolStripMenuItem.Checked = true;
            checkedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            checkedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            checkedToolStripMenuItem.Name = "checkedToolStripMenuItem";
            checkedToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            checkedToolStripMenuItem.Text = "Checked";
            // 
            // checkedWithIconToolStripMenuItem
            // 
            checkedWithIconToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            checkedWithIconToolStripMenuItem.Checked = true;
            checkedWithIconToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            checkedWithIconToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            checkedWithIconToolStripMenuItem.Image = global::ReaLTaiizor.UI.Properties.Resources.properties_16xLG;
            checkedWithIconToolStripMenuItem.Name = "checkedWithIconToolStripMenuItem";
            checkedWithIconToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            checkedWithIconToolStripMenuItem.Text = "Checked with icon";
            // 
            // mnuWindow
            // 
            mnuWindow.BackColor = System.Drawing.Color.Transparent;
            mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuProject,
            mnuProperties,
            mnuConsole,
            mnuLayers,
            mnuHistory});
            mnuWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuWindow.Name = "mnuWindow";
            mnuWindow.Size = new System.Drawing.Size(63, 20);
            mnuWindow.Text = "&Window";
            // 
            // mnuProject
            // 
            mnuProject.BackColor = System.Drawing.Color.Transparent;
            mnuProject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuProject.Image = global::ReaLTaiizor.UI.Properties.Resources.application_16x;
            mnuProject.Name = "mnuProject";
            mnuProject.Size = new System.Drawing.Size(157, 22);
            mnuProject.Text = "&Project Explorer";
            // 
            // mnuProperties
            // 
            mnuProperties.BackColor = System.Drawing.Color.Transparent;
            mnuProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuProperties.Image = global::ReaLTaiizor.UI.Properties.Resources.properties_16xLG;
            mnuProperties.Name = "mnuProperties";
            mnuProperties.Size = new System.Drawing.Size(157, 22);
            mnuProperties.Text = "P&roperties";
            // 
            // mnuConsole
            // 
            mnuConsole.BackColor = System.Drawing.Color.Transparent;
            mnuConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuConsole.Image = global::ReaLTaiizor.UI.Properties.Resources.Console;
            mnuConsole.Name = "mnuConsole";
            mnuConsole.Size = new System.Drawing.Size(157, 22);
            mnuConsole.Text = "&Console";
            // 
            // mnuLayers
            // 
            mnuLayers.BackColor = System.Drawing.Color.Transparent;
            mnuLayers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuLayers.Image = global::ReaLTaiizor.UI.Properties.Resources.Collection_16xLG;
            mnuLayers.Name = "mnuLayers";
            mnuLayers.Size = new System.Drawing.Size(157, 22);
            mnuLayers.Text = "&Layers";
            // 
            // mnuHistory
            // 
            mnuHistory.BackColor = System.Drawing.Color.Transparent;
            mnuHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            mnuHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnuHistory.Image")));
            mnuHistory.Name = "mnuHistory";
            mnuHistory.Size = new System.Drawing.Size(157, 22);
            mnuHistory.Text = "&History";
            // 
            // toolMain
            // 
            toolMain.AutoSize = false;
            toolMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            toolMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            btnNewFile});
            toolMain.Location = new System.Drawing.Point(0, 26);
            toolMain.Name = "toolMain";
            toolMain.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            toolMain.Size = new System.Drawing.Size(944, 28);
            toolMain.TabIndex = 1;
            toolMain.Text = "CrownToolStrip1";
            // 
            // btnNewFile
            // 
            btnNewFile.AutoSize = false;
            btnNewFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            btnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            btnNewFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            btnNewFile.Image = global::ReaLTaiizor.UI.Properties.Resources.NewFile_6276;
            btnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            btnNewFile.Name = "btnNewFile";
            btnNewFile.Size = new System.Drawing.Size(24, 24);
            btnNewFile.Text = "New file";
            // 
            // stripMain
            // 
            stripMain.AutoSize = false;
            stripMain.BackColor = System.Drawing.Color.Transparent;
            stripMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            stripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripStatusLabel1,
            toolStripStatusLabel6,
            toolStripStatusLabel5});
            stripMain.Location = new System.Drawing.Point(0, 618);
            stripMain.Name = "stripMain";
            stripMain.Padding = new System.Windows.Forms.Padding(0, 5, 0, 3);
            stripMain.Size = new System.Drawing.Size(944, 24);
            stripMain.SizingGrip = false;
            stripMain.TabIndex = 2;
            stripMain.Text = "CrownStatusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.AutoSize = false;
            toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(1, 0, 50, 0);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(39, 16);
            toolStripStatusLabel1.Text = "Ready";
            toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel6
            // 
            toolStripStatusLabel6.Margin = new System.Windows.Forms.Padding(0, 0, 50, 2);
            toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            toolStripStatusLabel6.Size = new System.Drawing.Size(757, 14);
            toolStripStatusLabel6.Spring = true;
            toolStripStatusLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.Margin = new System.Windows.Forms.Padding(0, 0, 1, 0);
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new System.Drawing.Size(46, 16);
            toolStripStatusLabel5.Text = "120 MB";
            toolStripStatusLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DockPanel
            // 
            DockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            DockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            DockPanel.Location = new System.Drawing.Point(0, 54);
            DockPanel.Name = "DockPanel";
            DockPanel.Size = new System.Drawing.Size(944, 564);
            DockPanel.TabIndex = 3;
            // 
            // CrownSeparator1
            // 
            CrownSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            CrownSeparator1.Location = new System.Drawing.Point(0, 24);
            CrownSeparator1.Name = "CrownSeparator1";
            CrownSeparator1.Size = new System.Drawing.Size(944, 2);
            CrownSeparator1.TabIndex = 4;
            CrownSeparator1.Text = "CrownSeparator1";
            // 
            // themeToolStripMenuItem
            // 
            themeToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            darkToolStripMenuItem,
            lightToolStripMenuItem});
            themeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            themeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            themeToolStripMenuItem.Text = "Theme";
            // 
            // darkToolStripMenuItem
            // 
            darkToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            darkToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            darkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            darkToolStripMenuItem.Text = "Dark";
            darkToolStripMenuItem.Click += new System.EventHandler(DarkToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            lightToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            lightToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            lightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            lightToolStripMenuItem.Text = "Light";
            lightToolStripMenuItem.Click += new System.EventHandler(LightToolStripMenuItem_Click);
            // 
            // Form21
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(944, 642);
            Controls.Add(DockPanel);
            Controls.Add(stripMain);
            Controls.Add(toolMain);
            Controls.Add(CrownSeparator1);
            Controls.Add(mnuMain);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MainMenuStrip = mnuMain;
            MinimumSize = new System.Drawing.Size(640, 480);
            Name = "Form21";
            Text = "Crown Theme";
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            toolMain.ResumeLayout(false);
            toolMain.PerformLayout();
            stripMain.ResumeLayout(false);
            stripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private CrownMenuStrip mnuMain;
        private CrownToolStrip toolMain;
        private CrownStatusStrip stripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuDialog;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripButton btnNewFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private CrownDockPanel DockPanel;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuConsole;
        private System.Windows.Forms.ToolStripMenuItem mnuLayers;
        private System.Windows.Forms.ToolStripMenuItem mnuHistory;
        private CrownSeparator CrownSeparator1;
        private System.Windows.Forms.ToolStripMenuItem checkableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkableWithIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem checkedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkedWithIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
    }
}

