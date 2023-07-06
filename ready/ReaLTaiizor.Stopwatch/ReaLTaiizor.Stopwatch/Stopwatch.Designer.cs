
namespace ReaLTaiizor.Stopwatch
{
    partial class Stopwatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stopwatch));
            this.parrotForm1 = new ReaLTaiizor.Forms.ParrotForm();
            this.materialTabControl1 = new ReaLTaiizor.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.parrotSegment1 = new ReaLTaiizor.Controls.ParrotSegment();
            this.parrotObjectEllipse1 = new ReaLTaiizor.Controls.ParrotObjectEllipse();
            this.parrotSplashScreen1 = new ReaLTaiizor.Controls.ParrotSplashScreen();
            this.parrotForm1.WorkingArea.SuspendLayout();
            this.parrotForm1.SuspendLayout();
            this.materialTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // parrotForm1
            // 
            this.parrotForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.ExitApplication = true;
            this.parrotForm1.FormStyle = ReaLTaiizor.Forms.ParrotForm.Style.MacOS;
            this.parrotForm1.Location = new System.Drawing.Point(0, 0);
            this.parrotForm1.MacOSForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.parrotForm1.MacOSLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.parrotForm1.MacOSRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.parrotForm1.MacOSSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(173)))), ((int)(((byte)(173)))));
            this.parrotForm1.MaterialBackColor = System.Drawing.Color.DodgerBlue;
            this.parrotForm1.MaterialForeColor = System.Drawing.Color.White;
            this.parrotForm1.Name = "parrotForm1";
            this.parrotForm1.ShowMaximize = false;
            this.parrotForm1.ShowMinimize = true;
            this.parrotForm1.Size = new System.Drawing.Size(272, 450);
            this.parrotForm1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotForm1.TabIndex = 0;
            this.parrotForm1.TitleText = "Stopwatch";
            this.parrotForm1.UbuntuForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(210)))));
            this.parrotForm1.UbuntuLeftBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(85)))), ((int)(((byte)(80)))));
            this.parrotForm1.UbuntuRightBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(60)))));
            // 
            // parrotForm1.WorkingArea
            // 
            this.parrotForm1.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotForm1.WorkingArea.Controls.Add(this.materialTabControl1);
            this.parrotForm1.WorkingArea.Controls.Add(this.parrotSegment1);
            this.parrotForm1.WorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parrotForm1.WorkingArea.Location = new System.Drawing.Point(0, 39);
            this.parrotForm1.WorkingArea.Name = "WorkingArea";
            this.parrotForm1.WorkingArea.Size = new System.Drawing.Size(272, 411);
            this.parrotForm1.WorkingArea.TabIndex = 0;
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(3, 36);
            this.materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.Padding = new System.Drawing.Point(0, 0);
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(265, 371);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(257, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // parrotSegment1
            // 
            this.parrotSegment1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotSegment1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.parrotSegment1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.parrotSegment1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.parrotSegment1.Items = "Tab 1,Add Tab";
            this.parrotSegment1.Location = new System.Drawing.Point(3, 3);
            this.parrotSegment1.Name = "parrotSegment1";
            this.parrotSegment1.SegmentActiveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.parrotSegment1.SegmentActiveFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(130)))), ((int)(((byte)(205)))));
            this.parrotSegment1.SegmentActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(130)))), ((int)(((byte)(205)))));
            this.parrotSegment1.SegmentActiveTextColor = System.Drawing.Color.White;
            this.parrotSegment1.SegmentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(135)))));
            this.parrotSegment1.SegmentColor = System.Drawing.Color.White;
            this.parrotSegment1.SegmentInactiveBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(255)))));
            this.parrotSegment1.SegmentInactiveFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.parrotSegment1.SegmentInactiveTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.parrotSegment1.SegmentNormalBackColor = System.Drawing.Color.Transparent;
            this.parrotSegment1.SegmentStyle = ReaLTaiizor.Controls.ParrotSegment.Style.Android;
            this.parrotSegment1.SelectedIndex = 0;
            this.parrotSegment1.Size = new System.Drawing.Size(266, 30);
            this.parrotSegment1.TabIndex = 0;
            this.parrotSegment1.Text = "parrotSegment1";
            this.parrotSegment1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.parrotSegment1.IndexChanged += new System.EventHandler(this.ParrotSegment1_IndexChanged);
            // 
            // parrotObjectEllipse1
            // 
            this.parrotObjectEllipse1.CornerRadius = 10;
            this.parrotObjectEllipse1.EffectedControl = null;
            this.parrotObjectEllipse1.EffectedForm = this;
            // 
            // parrotSplashScreen1
            // 
            this.parrotSplashScreen1.AllowDragging = true;
            this.parrotSplashScreen1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.parrotSplashScreen1.BottomText = "ReaLTaizor Stopwatch Edition";
            this.parrotSplashScreen1.BottomTextColor = System.Drawing.Color.Black;
            this.parrotSplashScreen1.BottomTextLocation = new System.Drawing.Point(125, 150);
            this.parrotSplashScreen1.BottomTextSize = 10;
            this.parrotSplashScreen1.EllipseCornerRadius = 15;
            this.parrotSplashScreen1.IsEllipse = true;
            this.parrotSplashScreen1.LoadedColor = System.Drawing.Color.DodgerBlue;
            this.parrotSplashScreen1.ProgressBarBorder = false;
            this.parrotSplashScreen1.ProgressBarLocation = new System.Drawing.Point(0, 224);
            this.parrotSplashScreen1.ProgressBarStyle = ReaLTaiizor.Controls.ParrotFlatProgressBar.Style.Material;
            this.parrotSplashScreen1.SecondsDisplayed = 1500;
            this.parrotSplashScreen1.ShowProgressBar = true;
            this.parrotSplashScreen1.SplashIcon = ((System.Drawing.Icon)(resources.GetObject("parrotSplashScreen1.SplashIcon")));
            this.parrotSplashScreen1.SplashSize = new System.Drawing.Size(450, 280);
            this.parrotSplashScreen1.TopText = "ReaLTaiizor Stopwatch";
            this.parrotSplashScreen1.TopTextColor = System.Drawing.Color.Black;
            this.parrotSplashScreen1.TopTextLocation = new System.Drawing.Point(5, 70);
            this.parrotSplashScreen1.TopTextSize = 31;
            this.parrotSplashScreen1.UnloadedColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            // 
            // Stopwatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(272, 450);
            this.Controls.Add(this.parrotForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Stopwatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopwatch";
            this.Load += new System.EventHandler(this.Stopwatch_Load);
            this.parrotForm1.WorkingArea.ResumeLayout(false);
            this.parrotForm1.ResumeLayout(false);
            this.materialTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Forms.ParrotForm parrotForm1;
        private Controls.ParrotObjectEllipse parrotObjectEllipse1;
        private Controls.ParrotSegment parrotSegment1;
        private Controls.ParrotSplashScreen parrotSplashScreen1;
        private Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
    }
}