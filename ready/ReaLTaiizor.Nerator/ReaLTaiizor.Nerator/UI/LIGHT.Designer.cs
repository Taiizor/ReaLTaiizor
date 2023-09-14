namespace ReaLTaiizor.Nerator.UI
{
    partial class LIGHT
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LIGHT));
            TELE = new ReaLTaiizor.Controls.LabelEdit();
            LOPB = new System.Windows.Forms.PictureBox();
            PWDTB = new ReaLTaiizor.Controls.BigTextBox();
            PLPB = new ReaLTaiizor.Controls.PoisonProgressBar();
            SSBR = new ReaLTaiizor.Controls.ForeverStatusBar();
            CEB = new ReaLTaiizor.Controls.MaterialButton();
            CYB = new ReaLTaiizor.Controls.MaterialButton();
            MTC = new ReaLTaiizor.Controls.MaterialTabControl();
            Generate = new System.Windows.Forms.TabPage();
            HYS = new ReaLTaiizor.Controls.MaterialSwitch();
            WRPB = new System.Windows.Forms.PictureBox();
            History = new System.Windows.Forms.TabPage();
            HYP = new System.Windows.Forms.Panel();
            Setting = new System.Windows.Forms.TabPage();
            PWLN = new ReaLTaiizor.Controls.HopeTrackBar();
            DKUC = new UC.THEME.DK();
            LTUC = new UC.THEME.LT();
            TMCB = new ReaLTaiizor.Controls.MaterialCheckBox();
            SMCB = new ReaLTaiizor.Controls.MaterialComboBox();
            AMCB = new ReaLTaiizor.Controls.MaterialComboBox();
            RTPB = new System.Windows.Forms.PictureBox();
            MTS = new ReaLTaiizor.Controls.MaterialTabSelector();
            STATUST = new System.Windows.Forms.Timer(components);
            STATUSMT = new System.Windows.Forms.Timer(components);
            materialCheckBox1 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialCheckBox2 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialCheckBox3 = new ReaLTaiizor.Controls.MaterialCheckBox();
            TETT = new ReaLTaiizor.Controls.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(LOPB)).BeginInit();
            MTC.SuspendLayout();
            Generate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(WRPB)).BeginInit();
            History.SuspendLayout();
            Setting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(RTPB)).BeginInit();
            SuspendLayout();
            // 
            // TELE
            // 
            TELE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            TELE.BackColor = System.Drawing.Color.Transparent;
            TELE.Enabled = false;
            TELE.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            TELE.ForeColor = System.Drawing.Color.White;
            TELE.Location = new System.Drawing.Point(0, 0);
            TELE.Name = "TELE";
            TELE.Size = new System.Drawing.Size(359, 25);
            TELE.TabIndex = 1;
            TELE.Text = "Nerator";
            TELE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LOPB
            // 
            LOPB.BackColor = System.Drawing.Color.Transparent;
            LOPB.Enabled = false;
            LOPB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.ShowPassword;
            LOPB.Location = new System.Drawing.Point(0, 0);
            LOPB.Name = "LOPB";
            LOPB.Size = new System.Drawing.Size(25, 24);
            LOPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            LOPB.TabIndex = 3;
            LOPB.TabStop = false;
            // 
            // PWDTB
            // 
            PWDTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            PWDTB.BackColor = System.Drawing.Color.White;
            PWDTB.Font = new System.Drawing.Font("Tahoma", 11F);
            PWDTB.ForeColor = System.Drawing.Color.DimGray;
            PWDTB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.PasteSpecial;
            PWDTB.Location = new System.Drawing.Point(6, 106);
            PWDTB.MaxLength = 50;
            PWDTB.Multiline = false;
            PWDTB.Name = "PWDTB";
            PWDTB.ReadOnly = false;
            PWDTB.Size = new System.Drawing.Size(315, 41);
            PWDTB.TabIndex = 6;
            PWDTB.Text = "Nerator";
            PWDTB.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            PWDTB.UseSystemPasswordChar = false;
            // 
            // PLPB
            // 
            PLPB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            PLPB.Location = new System.Drawing.Point(10, 146);
            PLPB.Name = "PLPB";
            PLPB.Size = new System.Drawing.Size(307, 5);
            PLPB.Style = ReaLTaiizor.Enum.Poison.ColorStyle.Green;
            PLPB.TabIndex = 12;
            PLPB.Theme = ReaLTaiizor.Enum.Poison.ThemeStyle.Light;
            PLPB.Value = 50;
            // 
            // SSBR
            // 
            SSBR.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            SSBR.Dock = System.Windows.Forms.DockStyle.Bottom;
            SSBR.Font = new System.Drawing.Font("Segoe UI", 8F);
            SSBR.ForeColor = System.Drawing.Color.White;
            SSBR.Location = new System.Drawing.Point(0, 336);
            SSBR.Name = "SSBR";
            SSBR.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(63)))), ((int)(((byte)(159)))));
            SSBR.ShowTimeDate = true;
            SSBR.Size = new System.Drawing.Size(359, 22);
            SSBR.TabIndex = 13;
            SSBR.Text = "The application continues to run smoothly.";
            SSBR.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            SSBR.TimeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            SSBR.TimeFormat = "HH:mm:ss";
            // 
            // CEB
            // 
            CEB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            CEB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            CEB.Cursor = System.Windows.Forms.Cursors.Hand;
            CEB.Depth = 0;
            CEB.DrawShadows = true;
            CEB.HighEmphasis = true;
            CEB.Icon = global::ReaLTaiizor.Nerator.Properties.Resources.QuillInk;
            CEB.Location = new System.Drawing.Point(6, 156);
            CEB.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            CEB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            CEB.Name = "CEB";
            CEB.Size = new System.Drawing.Size(104, 36);
            CEB.TabIndex = 14;
            CEB.Text = "CREATE";
            CEB.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Default;
            CEB.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            CEB.UseAccentColor = false;
            CEB.UseVisualStyleBackColor = true;
            CEB.Click += new System.EventHandler(CEB_Click);
            // 
            // CYB
            // 
            CYB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            CYB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            CYB.Cursor = System.Windows.Forms.Cursors.Hand;
            CYB.Depth = 0;
            CYB.DrawShadows = true;
            CYB.HighEmphasis = true;
            CYB.Icon = global::ReaLTaiizor.Nerator.Properties.Resources.CopyClipboard;
            CYB.Location = new System.Drawing.Point(234, 156);
            CYB.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            CYB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            CYB.Name = "CYB";
            CYB.Size = new System.Drawing.Size(87, 36);
            CYB.TabIndex = 15;
            CYB.Text = "COPY";
            CYB.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Default;
            CYB.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            CYB.UseAccentColor = false;
            CYB.UseVisualStyleBackColor = true;
            CYB.Click += new System.EventHandler(CYB_Click);
            // 
            // MTC
            // 
            MTC.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            MTC.Controls.Add(Generate);
            MTC.Controls.Add(History);
            MTC.Controls.Add(Setting);
            MTC.Depth = 0;
            MTC.ItemSize = new System.Drawing.Size(44, 18);
            MTC.Location = new System.Drawing.Point(12, 98);
            MTC.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            MTC.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            MTC.Multiline = true;
            MTC.Name = "MTC";
            MTC.Padding = new System.Drawing.Point(0, 0);
            MTC.SelectedIndex = 0;
            MTC.Size = new System.Drawing.Size(335, 227);
            MTC.TabIndex = 16;
            // 
            // Generate
            // 
            Generate.BackColor = System.Drawing.SystemColors.Control;
            Generate.Controls.Add(HYS);
            Generate.Controls.Add(CEB);
            Generate.Controls.Add(CYB);
            Generate.Controls.Add(PWDTB);
            Generate.Controls.Add(PLPB);
            Generate.Controls.Add(WRPB);
            Generate.Location = new System.Drawing.Point(4, 4);
            Generate.Name = "Generate";
            Generate.Padding = new System.Windows.Forms.Padding(3);
            Generate.Size = new System.Drawing.Size(327, 201);
            Generate.TabIndex = 1;
            Generate.Text = "Generate";
            // 
            // HYS
            // 
            HYS.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            HYS.AutoSize = true;
            HYS.Checked = true;
            HYS.CheckState = System.Windows.Forms.CheckState.Checked;
            HYS.Depth = 0;
            HYS.Location = new System.Drawing.Point(117, 157);
            HYS.Margin = new System.Windows.Forms.Padding(0);
            HYS.MouseLocation = new System.Drawing.Point(-1, -1);
            HYS.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            HYS.Name = "HYS";
            HYS.Ripple = true;
            HYS.Size = new System.Drawing.Size(108, 37);
            HYS.TabIndex = 16;
            HYS.Text = "History";
            HYS.UseAccentColor = true;
            HYS.UseVisualStyleBackColor = true;
            HYS.CheckedChanged += new System.EventHandler(HYS_CheckedChanged);
            // 
            // WRPB
            // 
            WRPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            WRPB.BackColor = System.Drawing.Color.Transparent;
            WRPB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.WaitRabbit;
            WRPB.Location = new System.Drawing.Point(0, 0);
            WRPB.Margin = new System.Windows.Forms.Padding(0);
            WRPB.Name = "WRPB";
            WRPB.Size = new System.Drawing.Size(327, 103);
            WRPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            WRPB.TabIndex = 17;
            WRPB.TabStop = false;
            // 
            // History
            // 
            History.BackColor = System.Drawing.SystemColors.Control;
            History.Controls.Add(HYP);
            History.Location = new System.Drawing.Point(4, 4);
            History.Name = "History";
            History.Padding = new System.Windows.Forms.Padding(3);
            History.Size = new System.Drawing.Size(327, 201);
            History.TabIndex = 0;
            History.Text = "History";
            // 
            // HYP
            // 
            HYP.AutoScroll = true;
            HYP.BackColor = System.Drawing.Color.Transparent;
            HYP.Dock = System.Windows.Forms.DockStyle.Fill;
            HYP.Location = new System.Drawing.Point(3, 3);
            HYP.Margin = new System.Windows.Forms.Padding(0);
            HYP.Name = "HYP";
            HYP.Size = new System.Drawing.Size(321, 195);
            HYP.TabIndex = 0;
            // 
            // Setting
            // 
            Setting.BackColor = System.Drawing.SystemColors.Control;
            Setting.Controls.Add(PWLN);
            Setting.Controls.Add(DKUC);
            Setting.Controls.Add(LTUC);
            Setting.Controls.Add(TMCB);
            Setting.Controls.Add(SMCB);
            Setting.Controls.Add(AMCB);
            Setting.Controls.Add(RTPB);
            Setting.Location = new System.Drawing.Point(4, 4);
            Setting.Name = "Setting";
            Setting.Padding = new System.Windows.Forms.Padding(3);
            Setting.Size = new System.Drawing.Size(327, 201);
            Setting.TabIndex = 2;
            Setting.Text = "Setting";
            // 
            // PWLN
            // 
            PWLN.AlwaysValueVisible = true;
            PWLN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            PWLN.BallonArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            PWLN.BallonColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            PWLN.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(220)))), ((int)(((byte)(223)))));
            PWLN.BaseColor = System.Drawing.SystemColors.Control;
            PWLN.Cursor = System.Windows.Forms.Cursors.Hand;
            PWLN.FillBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            PWLN.Font = new System.Drawing.Font("Segoe UI", 8F);
            PWLN.ForeColor = System.Drawing.Color.White;
            PWLN.HeadBorderColor = System.Drawing.Color.DodgerBlue;
            PWLN.HeadColor = System.Drawing.Color.Black;
            PWLN.Location = new System.Drawing.Point(6, 57);
            PWLN.MaxValue = 50;
            PWLN.MinValue = 0;
            PWLN.Name = "PWLN";
            PWLN.ShowValue = true;
            PWLN.Size = new System.Drawing.Size(315, 45);
            PWLN.TabIndex = 17;
            PWLN.Text = "PWLN";
            PWLN.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            TETT.SetToolTip(PWLN, "Password Length");
            PWLN.UnknownColor = System.Drawing.Color.White;
            PWLN.Value = 15;
            // 
            // DKUC
            // 
            DKUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            DKUC.BackColor = System.Drawing.Color.Transparent;
            DKUC.Location = new System.Drawing.Point(218, 98);
            DKUC.Margin = new System.Windows.Forms.Padding(5);
            DKUC.MaximumSize = new System.Drawing.Size(103, 97);
            DKUC.MinimumSize = new System.Drawing.Size(103, 97);
            DKUC.Name = "DKUC";
            DKUC.Size = new System.Drawing.Size(103, 97);
            DKUC.TabIndex = 16;
            // 
            // LTUC
            // 
            LTUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            LTUC.BackColor = System.Drawing.Color.Transparent;
            LTUC.Location = new System.Drawing.Point(6, 98);
            LTUC.Margin = new System.Windows.Forms.Padding(5);
            LTUC.MaximumSize = new System.Drawing.Size(103, 97);
            LTUC.MinimumSize = new System.Drawing.Size(103, 97);
            LTUC.Name = "LTUC";
            LTUC.Size = new System.Drawing.Size(103, 97);
            LTUC.TabIndex = 15;
            // 
            // TMCB
            // 
            TMCB.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            TMCB.AutoSize = true;
            TMCB.Checked = true;
            TMCB.CheckState = System.Windows.Forms.CheckState.Checked;
            TMCB.Cursor = System.Windows.Forms.Cursors.Hand;
            TMCB.Depth = 0;
            TMCB.Location = new System.Drawing.Point(110, 164);
            TMCB.Margin = new System.Windows.Forms.Padding(0);
            TMCB.MouseLocation = new System.Drawing.Point(-1, -1);
            TMCB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            TMCB.Name = "TMCB";
            TMCB.Ripple = true;
            TMCB.Size = new System.Drawing.Size(103, 37);
            TMCB.TabIndex = 12;
            TMCB.Text = "Top Most";
            TMCB.UseVisualStyleBackColor = true;
            TMCB.CheckedChanged += new System.EventHandler(TMCB_CheckedChanged);
            // 
            // SMCB
            // 
            SMCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            SMCB.AutoResize = false;
            SMCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            SMCB.Cursor = System.Windows.Forms.Cursors.Hand;
            SMCB.Depth = 0;
            SMCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            SMCB.DropDownHeight = 174;
            SMCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            SMCB.DropDownWidth = 135;
            SMCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            SMCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            SMCB.FormattingEnabled = true;
            SMCB.Hint = "Special Mode";
            SMCB.IntegralHeight = false;
            SMCB.ItemHeight = 43;
            SMCB.Items.AddRange(new object[] {
            "Mixed",
            "Symbol",
            "Number"});
            SMCB.Location = new System.Drawing.Point(186, 6);
            SMCB.MaxDropDownItems = 4;
            SMCB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            SMCB.Name = "SMCB";
            SMCB.Size = new System.Drawing.Size(135, 49);
            SMCB.TabIndex = 14;
            SMCB.SelectedIndexChanged += new System.EventHandler(SMCB_SelectedIndexChanged);
            // 
            // AMCB
            // 
            AMCB.AutoResize = false;
            AMCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            AMCB.Cursor = System.Windows.Forms.Cursors.Hand;
            AMCB.Depth = 0;
            AMCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            AMCB.DropDownHeight = 174;
            AMCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            AMCB.DropDownWidth = 135;
            AMCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            AMCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            AMCB.FormattingEnabled = true;
            AMCB.Hint = "Alphabetic Mode";
            AMCB.IntegralHeight = false;
            AMCB.ItemHeight = 43;
            AMCB.Items.AddRange(new object[] {
            "Mixed",
            "Uppercase",
            "Lowercase"});
            AMCB.Location = new System.Drawing.Point(6, 6);
            AMCB.MaxDropDownItems = 4;
            AMCB.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            AMCB.Name = "AMCB";
            AMCB.Size = new System.Drawing.Size(135, 49);
            AMCB.TabIndex = 13;
            AMCB.SelectedIndexChanged += new System.EventHandler(AMCB_SelectedIndexChanged);
            // 
            // RTPB
            // 
            RTPB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            RTPB.BackColor = System.Drawing.Color.Transparent;
            RTPB.Image = global::ReaLTaiizor.Nerator.Properties.Resources.WhiteCat;
            RTPB.Location = new System.Drawing.Point(0, 71);
            RTPB.Margin = new System.Windows.Forms.Padding(0);
            RTPB.Name = "RTPB";
            RTPB.Size = new System.Drawing.Size(327, 93);
            RTPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            RTPB.TabIndex = 5;
            RTPB.TabStop = false;
            // 
            // MTS
            // 
            MTS.BaseTabControl = MTC;
            MTS.Cursor = System.Windows.Forms.Cursors.Default;
            MTS.Depth = 0;
            MTS.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            MTS.HeadAlignment = ReaLTaiizor.Controls.MaterialTabSelector.Alignment.Center;
            MTS.Location = new System.Drawing.Point(12, 75);
            MTS.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            MTS.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            MTS.Name = "MTS";
            MTS.Size = new System.Drawing.Size(335, 23);
            MTS.TabIndex = 17;
            MTS.Text = "materialTabSelector1";
            MTS.CharacterCasing = ReaLTaiizor.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            // 
            // STATUST
            // 
            STATUST.Enabled = true;
            STATUST.Interval = 1000;
            STATUST.Tick += new System.EventHandler(STATUST_Tick);
            // 
            // STATUSMT
            // 
            STATUSMT.Enabled = true;
            STATUSMT.Interval = 50;
            STATUSMT.Tick += new System.EventHandler(STATUSMT_Tick);
            // 
            // materialCheckBox1
            // 
            materialCheckBox1.Depth = 0;
            materialCheckBox1.Location = new System.Drawing.Point(0, 0);
            materialCheckBox1.Margin = new System.Windows.Forms.Padding(0);
            materialCheckBox1.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox1.Name = "materialCheckBox1";
            materialCheckBox1.Ripple = true;
            materialCheckBox1.Size = new System.Drawing.Size(104, 37);
            materialCheckBox1.TabIndex = 0;
            materialCheckBox1.Text = "materialCheckBox1";
            materialCheckBox1.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox2
            // 
            materialCheckBox2.Depth = 0;
            materialCheckBox2.Location = new System.Drawing.Point(0, 0);
            materialCheckBox2.Margin = new System.Windows.Forms.Padding(0);
            materialCheckBox2.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckBox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox2.Name = "materialCheckBox2";
            materialCheckBox2.Ripple = true;
            materialCheckBox2.Size = new System.Drawing.Size(104, 37);
            materialCheckBox2.TabIndex = 0;
            materialCheckBox2.Text = "materialCheckBox2";
            materialCheckBox2.UseVisualStyleBackColor = true;
            // 
            // materialCheckBox3
            // 
            materialCheckBox3.Depth = 0;
            materialCheckBox3.Location = new System.Drawing.Point(0, 0);
            materialCheckBox3.Margin = new System.Windows.Forms.Padding(0);
            materialCheckBox3.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckBox3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckBox3.Name = "materialCheckBox3";
            materialCheckBox3.Ripple = true;
            materialCheckBox3.Size = new System.Drawing.Size(104, 37);
            materialCheckBox3.TabIndex = 0;
            materialCheckBox3.Text = "materialCheckBox3";
            materialCheckBox3.UseVisualStyleBackColor = true;
            // 
            // TETT
            // 
            TETT.BackColor = System.Drawing.Color.White;
            TETT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            TETT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            TETT.IsDerivedStyle = true;
            TETT.OwnerDraw = true;
            TETT.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            TETT.StyleManager = null;
            TETT.ThemeAuthor = "Taiizor";
            TETT.ThemeName = "MetroLight";
            // 
            // LIGHT
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(359, 358);
            Controls.Add(SSBR);
            Controls.Add(MTS);
            Controls.Add(MTC);
            Controls.Add(LOPB);
            Controls.Add(TELE);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(359, 358);
            MinimumSize = new System.Drawing.Size(359, 358);
            Name = "LIGHT";
            Sizable = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "New Generation Password Generator";
            FormClosed += new System.Windows.Forms.FormClosedEventHandler(LIGHT_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(LOPB)).EndInit();
            MTC.ResumeLayout(false);
            Generate.ResumeLayout(false);
            Generate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(WRPB)).EndInit();
            History.ResumeLayout(false);
            Setting.ResumeLayout(false);
            Setting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(RTPB)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.LabelEdit TELE;
        private System.Windows.Forms.PictureBox LOPB;
        private ReaLTaiizor.Controls.BigTextBox PWDTB;
        private ReaLTaiizor.Controls.PoisonProgressBar PLPB;
        private ReaLTaiizor.Controls.ForeverStatusBar SSBR;
        private ReaLTaiizor.Controls.MaterialButton CEB;
        private ReaLTaiizor.Controls.MaterialButton CYB;
        private ReaLTaiizor.Controls.MaterialTabControl MTC;
        private System.Windows.Forms.TabPage History;
        private System.Windows.Forms.TabPage Generate;
        private ReaLTaiizor.Controls.MaterialTabSelector MTS;
        private System.Windows.Forms.TabPage Setting;
        private ReaLTaiizor.Controls.MaterialSwitch HYS;
        private System.Windows.Forms.Panel HYP;
        private System.Windows.Forms.Timer STATUST;
        private System.Windows.Forms.Timer STATUSMT;
        private System.Windows.Forms.PictureBox WRPB;
        private System.Windows.Forms.PictureBox RTPB;
        private ReaLTaiizor.Controls.MaterialCheckBox TMCB;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox1;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox2;
        private ReaLTaiizor.Controls.MaterialCheckBox materialCheckBox3;
        private ReaLTaiizor.Controls.MaterialComboBox AMCB;
        private ReaLTaiizor.Controls.MaterialComboBox SMCB;
        private UC.THEME.LT LTUC;
        private UC.THEME.DK DKUC;
        private ReaLTaiizor.Controls.HopeTrackBar PWLN;
        private ReaLTaiizor.Controls.MetroToolTip TETT;
    }
}