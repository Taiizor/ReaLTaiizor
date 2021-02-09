using ReaLTaiizor.Controls;

namespace ReaLTaiizor.UI.Forms.Dialogs
{
    partial class DialogControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogControls));
            pnlMain = new System.Windows.Forms.Panel();
            tblMain = new System.Windows.Forms.TableLayoutPanel();
            pnlTreeView = new ReaLTaiizor.Controls.CrownSectionPanel();
            treeTest = new ReaLTaiizor.Controls.CrownTreeView();
            pnlListView = new ReaLTaiizor.Controls.CrownSectionPanel();
            lstTest = new ReaLTaiizor.Controls.CrownListView();
            pnlMessageBox = new ReaLTaiizor.Controls.CrownSectionPanel();
            panel1 = new System.Windows.Forms.Panel();
            crownGroupBox1 = new ReaLTaiizor.Controls.CrownGroupBox();
            crownRadioButton4 = new ReaLTaiizor.Controls.CrownRadioButton();
            crownCheckBox3 = new ReaLTaiizor.Controls.CrownCheckBox();
            panel7 = new System.Windows.Forms.Panel();
            crownComboBox1 = new ReaLTaiizor.Controls.CrownComboBox();
            crownTitle4 = new ReaLTaiizor.Controls.CrownTitle();
            panel6 = new System.Windows.Forms.Panel();
            crownNumeric1 = new ReaLTaiizor.Controls.CrownNumeric();
            crownTitle5 = new ReaLTaiizor.Controls.CrownTitle();
            panel5 = new System.Windows.Forms.Panel();
            crownRadioButton3 = new ReaLTaiizor.Controls.CrownRadioButton();
            crownRadioButton2 = new ReaLTaiizor.Controls.CrownRadioButton();
            crownRadioButton1 = new ReaLTaiizor.Controls.CrownRadioButton();
            crownTitle3 = new ReaLTaiizor.Controls.CrownTitle();
            panel4 = new System.Windows.Forms.Panel();
            crownCheckBox2 = new ReaLTaiizor.Controls.CrownCheckBox();
            crownCheckBox1 = new ReaLTaiizor.Controls.CrownCheckBox();
            crownTitle2 = new ReaLTaiizor.Controls.CrownTitle();
            panel3 = new System.Windows.Forms.Panel();
            btnMessageBox = new ReaLTaiizor.Controls.CrownButton();
            panel2 = new System.Windows.Forms.Panel();
            btnDialog = new ReaLTaiizor.Controls.CrownButton();
            crownTitle1 = new ReaLTaiizor.Controls.CrownTitle();
            pnlMain.SuspendLayout();
            tblMain.SuspendLayout();
            pnlTreeView.SuspendLayout();
            pnlListView.SuspendLayout();
            pnlMessageBox.SuspendLayout();
            panel1.SuspendLayout();
            crownGroupBox1.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(crownNumeric1)).BeginInit();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(tblMain);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(5, 10, 5, 0);
            pnlMain.Size = new System.Drawing.Size(708, 528);
            pnlMain.TabIndex = 2;
            // 
            // tblMain
            // 
            tblMain.ColumnCount = 3;
            tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tblMain.Controls.Add(pnlTreeView, 0, 0);
            tblMain.Controls.Add(pnlListView, 0, 0);
            tblMain.Controls.Add(pnlMessageBox, 0, 0);
            tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tblMain.Location = new System.Drawing.Point(5, 10);
            tblMain.Name = "tblMain";
            tblMain.RowCount = 1;
            tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tblMain.Size = new System.Drawing.Size(698, 518);
            tblMain.TabIndex = 0;
            // 
            // pnlTreeView
            // 
            pnlTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(51)))), ((int)(((byte)(63)))));
            pnlTreeView.Controls.Add(treeTest);
            pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlTreeView.Location = new System.Drawing.Point(469, 0);
            pnlTreeView.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            pnlTreeView.Name = "pnlTreeView";
            pnlTreeView.SectionHeader = "Tree view test";
            pnlTreeView.Size = new System.Drawing.Size(224, 518);
            pnlTreeView.TabIndex = 14;
            // 
            // treeTest
            // 
            treeTest.AllowMoveNodes = true;
            treeTest.Dock = System.Windows.Forms.DockStyle.Fill;
            treeTest.Location = new System.Drawing.Point(1, 25);
            treeTest.MaxDragChange = 20;
            treeTest.MultiSelect = true;
            treeTest.Name = "treeTest";
            treeTest.ShowIcons = true;
            treeTest.Size = new System.Drawing.Size(222, 492);
            treeTest.TabIndex = 0;
            treeTest.Text = "CrownTreeView1";
            // 
            // pnlListView
            // 
            pnlListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(51)))), ((int)(((byte)(63)))));
            pnlListView.Controls.Add(lstTest);
            pnlListView.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlListView.Location = new System.Drawing.Point(237, 0);
            pnlListView.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            pnlListView.Name = "pnlListView";
            pnlListView.SectionHeader = "List view test";
            pnlListView.Size = new System.Drawing.Size(222, 518);
            pnlListView.TabIndex = 13;
            // 
            // lstTest
            // 
            lstTest.Dock = System.Windows.Forms.DockStyle.Fill;
            lstTest.Location = new System.Drawing.Point(1, 25);
            lstTest.MultiSelect = true;
            lstTest.Name = "lstTest";
            lstTest.Size = new System.Drawing.Size(220, 492);
            lstTest.TabIndex = 7;
            lstTest.Text = "CrownListView1";
            // 
            // pnlMessageBox
            // 
            pnlMessageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(51)))), ((int)(((byte)(63)))));
            pnlMessageBox.Controls.Add(panel1);
            pnlMessageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMessageBox.Location = new System.Drawing.Point(5, 0);
            pnlMessageBox.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            pnlMessageBox.Name = "pnlMessageBox";
            pnlMessageBox.SectionHeader = "Controls test";
            pnlMessageBox.Size = new System.Drawing.Size(222, 518);
            pnlMessageBox.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Transparent;
            panel1.Controls.Add(crownGroupBox1);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(1, 25);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(10);
            panel1.Size = new System.Drawing.Size(220, 492);
            panel1.TabIndex = 0;
            // 
            // crownGroupBox1
            // 
            crownGroupBox1.AutoSize = true;
            crownGroupBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            crownGroupBox1.Controls.Add(crownRadioButton4);
            crownGroupBox1.Controls.Add(crownCheckBox3);
            crownGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            crownGroupBox1.Location = new System.Drawing.Point(10, 412);
            crownGroupBox1.Name = "crownGroupBox1";
            crownGroupBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 10);
            crownGroupBox1.Size = new System.Drawing.Size(200, 69);
            crownGroupBox1.TabIndex = 24;
            crownGroupBox1.TabStop = false;
            crownGroupBox1.Text = "GroupBox";
            // 
            // crownRadioButton4
            // 
            crownRadioButton4.AutoSize = true;
            crownRadioButton4.Dock = System.Windows.Forms.DockStyle.Top;
            crownRadioButton4.Location = new System.Drawing.Point(10, 40);
            crownRadioButton4.Name = "crownRadioButton4";
            crownRadioButton4.Size = new System.Drawing.Size(180, 19);
            crownRadioButton4.TabIndex = 1;
            crownRadioButton4.TabStop = true;
            crownRadioButton4.Text = "Radio button";
            // 
            // crownCheckBox3
            // 
            crownCheckBox3.AutoSize = true;
            crownCheckBox3.Dock = System.Windows.Forms.DockStyle.Top;
            crownCheckBox3.Location = new System.Drawing.Point(10, 21);
            crownCheckBox3.Name = "crownCheckBox3";
            crownCheckBox3.Size = new System.Drawing.Size(180, 19);
            crownCheckBox3.TabIndex = 0;
            crownCheckBox3.Text = "Checkbox";
            // 
            // panel7
            // 
            panel7.Controls.Add(crownComboBox1);
            panel7.Controls.Add(crownTitle4);
            panel7.Dock = System.Windows.Forms.DockStyle.Top;
            panel7.Location = new System.Drawing.Point(10, 349);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(200, 63);
            panel7.TabIndex = 23;
            // 
            // crownComboBox1
            // 
            crownComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            crownComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            crownComboBox1.FormattingEnabled = true;
            crownComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "This is a really long item in the collection to check out how text is clipped",
            "Item 4"});
            crownComboBox1.Location = new System.Drawing.Point(0, 26);
            crownComboBox1.Name = "crownComboBox1";
            crownComboBox1.Size = new System.Drawing.Size(200, 24);
            crownComboBox1.TabIndex = 20;
            // 
            // crownTitle4
            // 
            crownTitle4.Dock = System.Windows.Forms.DockStyle.Top;
            crownTitle4.Location = new System.Drawing.Point(0, 0);
            crownTitle4.Name = "crownTitle4";
            crownTitle4.Size = new System.Drawing.Size(200, 26);
            crownTitle4.TabIndex = 21;
            crownTitle4.Text = "ComboBox";
            // 
            // panel6
            // 
            panel6.Controls.Add(crownNumeric1);
            panel6.Controls.Add(crownTitle5);
            panel6.Dock = System.Windows.Forms.DockStyle.Top;
            panel6.Location = new System.Drawing.Point(10, 285);
            panel6.Margin = new System.Windows.Forms.Padding(0);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(200, 64);
            panel6.TabIndex = 22;
            // 
            // crownNumeric1
            // 
            crownNumeric1.Dock = System.Windows.Forms.DockStyle.Top;
            crownNumeric1.Location = new System.Drawing.Point(0, 26);
            crownNumeric1.Name = "crownNumeric1";
            crownNumeric1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            crownNumeric1.Size = new System.Drawing.Size(200, 23);
            crownNumeric1.TabIndex = 24;
            // 
            // crownTitle5
            // 
            crownTitle5.Dock = System.Windows.Forms.DockStyle.Top;
            crownTitle5.Location = new System.Drawing.Point(0, 0);
            crownTitle5.Name = "crownTitle5";
            crownTitle5.Size = new System.Drawing.Size(200, 26);
            crownTitle5.TabIndex = 23;
            crownTitle5.Text = "Numeric Up/Down";
            // 
            // panel5
            // 
            panel5.Controls.Add(crownRadioButton3);
            panel5.Controls.Add(crownRadioButton2);
            panel5.Controls.Add(crownRadioButton1);
            panel5.Controls.Add(crownTitle3);
            panel5.Dock = System.Windows.Forms.DockStyle.Top;
            panel5.Location = new System.Drawing.Point(10, 185);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(200, 100);
            panel5.TabIndex = 12;
            // 
            // crownRadioButton3
            // 
            crownRadioButton3.AutoSize = true;
            crownRadioButton3.Checked = true;
            crownRadioButton3.Dock = System.Windows.Forms.DockStyle.Top;
            crownRadioButton3.Enabled = false;
            crownRadioButton3.Location = new System.Drawing.Point(0, 64);
            crownRadioButton3.Name = "crownRadioButton3";
            crownRadioButton3.Size = new System.Drawing.Size(200, 19);
            crownRadioButton3.TabIndex = 4;
            crownRadioButton3.TabStop = true;
            crownRadioButton3.Text = "Disabled radio button";
            // 
            // crownRadioButton2
            // 
            crownRadioButton2.AutoSize = true;
            crownRadioButton2.Dock = System.Windows.Forms.DockStyle.Top;
            crownRadioButton2.Location = new System.Drawing.Point(0, 45);
            crownRadioButton2.Name = "crownRadioButton2";
            crownRadioButton2.Size = new System.Drawing.Size(200, 19);
            crownRadioButton2.TabIndex = 3;
            crownRadioButton2.Text = "Radio button";
            // 
            // crownRadioButton1
            // 
            crownRadioButton1.AutoSize = true;
            crownRadioButton1.Dock = System.Windows.Forms.DockStyle.Top;
            crownRadioButton1.Location = new System.Drawing.Point(0, 26);
            crownRadioButton1.Name = "crownRadioButton1";
            crownRadioButton1.Size = new System.Drawing.Size(200, 19);
            crownRadioButton1.TabIndex = 2;
            crownRadioButton1.Text = "Radio button";
            // 
            // crownTitle3
            // 
            crownTitle3.Dock = System.Windows.Forms.DockStyle.Top;
            crownTitle3.Location = new System.Drawing.Point(0, 0);
            crownTitle3.Name = "crownTitle3";
            crownTitle3.Size = new System.Drawing.Size(200, 26);
            crownTitle3.TabIndex = 16;
            crownTitle3.Text = "Radio buttons";
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            panel4.Controls.Add(crownCheckBox2);
            panel4.Controls.Add(crownCheckBox1);
            panel4.Controls.Add(crownTitle2);
            panel4.Dock = System.Windows.Forms.DockStyle.Top;
            panel4.Location = new System.Drawing.Point(10, 111);
            panel4.Name = "panel4";
            panel4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            panel4.Size = new System.Drawing.Size(200, 74);
            panel4.TabIndex = 11;
            // 
            // crownCheckBox2
            // 
            crownCheckBox2.AutoSize = true;
            crownCheckBox2.Checked = true;
            crownCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            crownCheckBox2.Dock = System.Windows.Forms.DockStyle.Top;
            crownCheckBox2.Enabled = false;
            crownCheckBox2.Location = new System.Drawing.Point(0, 45);
            crownCheckBox2.Name = "crownCheckBox2";
            crownCheckBox2.Size = new System.Drawing.Size(200, 19);
            crownCheckBox2.TabIndex = 13;
            crownCheckBox2.Text = "Disabled checkbox";
            // 
            // crownCheckBox1
            // 
            crownCheckBox1.AutoSize = true;
            crownCheckBox1.Dock = System.Windows.Forms.DockStyle.Top;
            crownCheckBox1.Location = new System.Drawing.Point(0, 26);
            crownCheckBox1.Name = "crownCheckBox1";
            crownCheckBox1.Size = new System.Drawing.Size(200, 19);
            crownCheckBox1.TabIndex = 12;
            crownCheckBox1.Text = "Enabled checkbox";
            // 
            // crownTitle2
            // 
            crownTitle2.Dock = System.Windows.Forms.DockStyle.Top;
            crownTitle2.Location = new System.Drawing.Point(0, 0);
            crownTitle2.Name = "crownTitle2";
            crownTitle2.Size = new System.Drawing.Size(200, 26);
            crownTitle2.TabIndex = 15;
            crownTitle2.Text = "Check boxes";
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.Controls.Add(btnMessageBox);
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(10, 71);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            panel3.Size = new System.Drawing.Size(200, 40);
            panel3.TabIndex = 10;
            // 
            // btnMessageBox
            // 
            btnMessageBox.Dock = System.Windows.Forms.DockStyle.Top;
            btnMessageBox.Location = new System.Drawing.Point(0, 0);
            btnMessageBox.Name = "btnMessageBox";
            btnMessageBox.Padding = new System.Windows.Forms.Padding(5);
            btnMessageBox.Size = new System.Drawing.Size(200, 30);
            btnMessageBox.TabIndex = 12;
            btnMessageBox.Text = "Message Box";
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(btnDialog);
            panel2.Controls.Add(crownTitle1);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(10, 10);
            panel2.Name = "panel2";
            panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            panel2.Size = new System.Drawing.Size(200, 61);
            panel2.TabIndex = 5;
            // 
            // btnDialog
            // 
            btnDialog.Dock = System.Windows.Forms.DockStyle.Top;
            btnDialog.Location = new System.Drawing.Point(0, 26);
            btnDialog.Name = "btnDialog";
            btnDialog.Padding = new System.Windows.Forms.Padding(5);
            btnDialog.Size = new System.Drawing.Size(200, 30);
            btnDialog.TabIndex = 4;
            btnDialog.Text = "Dialog";
            // 
            // crownTitle1
            // 
            crownTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            crownTitle1.Location = new System.Drawing.Point(0, 0);
            crownTitle1.Name = "crownTitle1";
            crownTitle1.Size = new System.Drawing.Size(200, 26);
            crownTitle1.TabIndex = 14;
            crownTitle1.Text = "Dialogs";
            // 
            // DialogControls
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(708, 573);
            Controls.Add(pnlMain);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "DialogControls";
            Text = "Controls";
            Controls.SetChildIndex(pnlMain, 0);
            pnlMain.ResumeLayout(false);
            tblMain.ResumeLayout(false);
            pnlTreeView.ResumeLayout(false);
            pnlListView.ResumeLayout(false);
            pnlMessageBox.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            crownGroupBox1.ResumeLayout(false);
            crownGroupBox1.PerformLayout();
            panel7.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(crownNumeric1)).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private CrownSectionPanel pnlTreeView;
        private CrownTreeView treeTest;
        private CrownSectionPanel pnlListView;
        private CrownListView lstTest;
        private CrownSectionPanel pnlMessageBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private CrownButton btnDialog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private CrownButton btnMessageBox;
        private CrownCheckBox crownCheckBox2;
        private CrownCheckBox crownCheckBox1;
        private System.Windows.Forms.Panel panel5;
        private CrownRadioButton crownRadioButton2;
        private CrownRadioButton crownRadioButton1;
        private CrownRadioButton crownRadioButton3;
        private CrownTitle crownTitle1;
        private CrownTitle crownTitle2;
        private CrownTitle crownTitle3;
        private System.Windows.Forms.Panel panel7;
        private CrownComboBox crownComboBox1;
        private CrownTitle crownTitle4;
        private System.Windows.Forms.Panel panel6;
        private CrownNumeric crownNumeric1;
        private CrownTitle crownTitle5;
        private CrownGroupBox crownGroupBox1;
        private CrownRadioButton crownRadioButton4;
        private CrownCheckBox crownCheckBox3;
    }
}