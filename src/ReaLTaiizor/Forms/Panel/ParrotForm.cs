#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Design.Parrot;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region ParrotForm

    [Designer(typeof(ParrotFormDesigner))]
    public class ParrotForm : System.Windows.Forms.Panel
    {
        public ParrotForm()
        {
            InitializeComponent();
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The working area")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public System.Windows.Forms.Panel WorkingArea { get; } = new();

        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }

                if (value.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
                {
                    IComponent rootComponent = designerHost.RootComponent;

                    if (rootComponent is ContainerControl)
                    {
                        DefaultForm = (Form)rootComponent;
                        DefaultStyle = ((Form)rootComponent).FormBorderStyle;
                        ((Form)rootComponent).FormBorderStyle = FormBorderStyle.None;
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                DefaultForm.FormBorderStyle = DefaultStyle;
            }
            catch
            {
                //throw;
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            SetBG = true;
            WorkingArea.BackColor = BackColor;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The titles text")]
        public string TitleText
        {
            get => titleText;
            set
            {
                titleText = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the maximize option")]
        public bool ShowMaximize
        {
            get => showMaximize;
            set
            {
                showMaximize = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Exit appliction? otherwise form will just be closed")]
        public bool ExitApplication
        {
            get => exitApplication;
            set
            {
                exitApplication = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show the minimize option")]
        public bool ShowMinimize
        {
            get => showMinimize;
            set
            {
                showMinimize = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The forecolor of the material titlebar")]
        public Color MaterialForeColor
        {
            get => materialForeColor;
            set
            {
                materialForeColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The backcolor of the material titlebar")]
        public Color MaterialBackColor
        {
            get => materialBackColor;
            set
            {
                materialBackColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The forecolor of the macos titlebar")]
        public Color MacOSForeColor
        {
            get => macOSForeColor;
            set
            {
                macOSForeColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The leftbackcolor of the macos titlebar")]
        public Color MacOSLeftBackColor
        {
            get => macOSLeftBackColor;
            set
            {
                macOSLeftBackColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The rightbackcolor of the macos titlebar")]
        public Color MacOSRightBackColor
        {
            get => macOSRightBackColor;
            set
            {
                macOSRightBackColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The separator color of the macos titlebar")]
        public Color MacOSSeparatorColor
        {
            get => macOSSeparatorColor;
            set
            {
                macOSSeparatorColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The forecolor of the ubuntu titlebar")]
        public Color UbuntuForeColor
        {
            get => ubuntuForeColor;
            set
            {
                ubuntuForeColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The leftbackcolor of the ubuntu titlebar")]
        public Color UbuntuLeftBackColor
        {
            get => ubuntuLeftBackColor;
            set
            {
                ubuntuLeftBackColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The rightbackcolor of the ubuntu titlebar")]
        public Color UbuntuRightBackColor
        {
            get => ubuntuRightBackColor;
            set
            {
                ubuntuRightBackColor = value;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The forms style")]
        public Style FormStyle
        {
            get => formStyle;
            set
            {
                formStyle = value;
                SetBG = false;
                base.Controls.Clear();
                RefreshUI();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            RefreshUI();
            base.ResumeLayout(true);
        }

        private void RefreshUI()
        {
            base.SuspendLayout();
            base.Controls.Clear();

            Dock = DockStyle.Fill;

            try
            {
                base.FindForm().FormBorderStyle = FormBorderStyle.None;
                base.FindForm().StartPosition = FormStartPosition.CenterScreen;
            }
            catch
            {
                //
            }

            Label label = new();

            ParrotFormHandle parrotFormHandle = new();

            _ = new ParrotFormHandle();

            switch (formStyle)
            {
                case Style.Material:
                    System.Windows.Forms.Panel panel = new();
                    System.Windows.Forms.Panel panel2 = new();

                    ParrotButton parrotButton = new();
                    ParrotButton parrotButton2 = new();
                    ParrotButton parrotButton3 = new();

                    _ = new ParrotFormHandle();

                    panel2.BackColor = MaterialBackColor;
                    panel2.Location = new Point(0, 0);
                    panel2.Size = new Size(base.Width, 50);
                    panel2.Dock = DockStyle.Top;
                    panel.BackColor = MaterialBackColor;
                    panel.Location = new Point(0, 0);
                    panel.Size = new Size(base.Width, 24);
                    panel.Dock = DockStyle.Top;

                    if (showMinimize)
                    {
                        parrotButton3.Size = new Size(24, 24);
                        parrotButton3.Dock = DockStyle.Right;
                        parrotButton3.ButtonText = "_";
                        parrotButton3.TextColor = MaterialForeColor;
                        parrotButton3.BackgroundColor = panel.BackColor;
                        parrotButton3.ClickBackColor = panel.BackColor;
                        parrotButton3.ButtonStyle = ParrotButton.Style.Material;
                        parrotButton3.Cursor = Cursors.Hand;
                        parrotButton3.ButtonImage = null;
                        parrotButton3.Click += Minimize_Click;

                        panel.Controls.Add(parrotButton3);
                    }

                    if (showMaximize)
                    {
                        parrotButton2.Size = new Size(24, 24);
                        parrotButton2.Dock = DockStyle.Right;
                        parrotButton2.ButtonText = "[  ]";
                        parrotButton2.TextColor = MaterialForeColor;
                        parrotButton2.BackgroundColor = panel.BackColor;
                        parrotButton2.ClickBackColor = panel.BackColor;
                        parrotButton2.ButtonStyle = ParrotButton.Style.Material;
                        parrotButton2.Cursor = Cursors.Hand;
                        parrotButton2.ButtonImage = null;
                        parrotButton2.Click += Maximize_Click;

                        panel.Controls.Add(parrotButton2);
                    }

                    parrotButton.Size = new Size(24, 24);
                    parrotButton.Dock = DockStyle.Right;
                    parrotButton.ButtonText = "X";
                    parrotButton.TextColor = MaterialForeColor;
                    parrotButton.BackgroundColor = panel.BackColor;
                    parrotButton.HoverBackgroundColor = Color.Red;
                    parrotButton.ClickBackColor = Color.Red;
                    parrotButton.ButtonText = "X";
                    parrotButton.ButtonStyle = ParrotButton.Style.Material;
                    parrotButton.Cursor = Cursors.Hand;
                    parrotButton.ButtonImage = null;

                    parrotButton.Click += Exit_Click;

                    panel.Controls.Add(parrotButton);

                    label.ForeColor = MaterialForeColor;
                    label.Font = new Font("Calibri", 12f);
                    label.TextAlign = ContentAlignment.MiddleLeft;
                    label.AutoSize = false;
                    label.Dock = DockStyle.Left;
                    label.Text = titleText;

                    panel.Controls.Add(label);

                    WorkingArea.Location = new Point(0, 50);

                    BackColor = Color.White;

                    WorkingArea.Dock = DockStyle.Fill;

                    parrotFormHandle.HandleControl = label;
                    parrotFormHandle.HandleControl = panel;
                    parrotFormHandle.HandleControl = panel2;

                    base.Controls.Add(WorkingArea);
                    base.Controls.Add(panel2);
                    base.Controls.Add(panel);
                    break;
                case Style.MacOS:
                    System.Windows.Forms.Panel panel3 = new()
                    {
                        BackColor = MacOSSeparatorColor,
                        Size = new Size(base.Width, 1),
                        Dock = DockStyle.Top
                    };

                    ParrotGradientPanel parrotGradientPanel = new()
                    {
                        Style = ParrotGradientPanel.GradientStyle.Horizontal,
                        TopLeft = MacOSLeftBackColor,
                        TopRight = MacOSRightBackColor,
                        Size = new Size(base.Width, 38),
                        Dock = DockStyle.Top
                    };

                    label.ForeColor = MacOSForeColor;
                    label.BackColor = Color.Transparent;
                    label.Parent = parrotGradientPanel;
                    label.Font = new Font("Microsoft Sans Serif", 10f);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.AutoSize = false;
                    label.Dock = DockStyle.Fill;
                    label.Text = titleText;

                    parrotGradientPanel.Controls.Add(label);

                    int x = 40;

                    if (!showMaximize)
                    {
                        x = 20;
                    }

                    if (showMinimize)
                    {
                        minimize.BackColor = Color.Transparent;
                        minimize.Parent = parrotGradientPanel;
                        minimize.Cursor = Cursors.Hand;
                        minimize.Size = new Size(20, 40);
                        minimize.Location = new Point(x, 0);

                        Bitmap image = new(17, 17);
                        Graphics graphics = Graphics.FromImage(image);

                        graphics.SmoothingMode = SmoothingType;

                        graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 205, 90)), new Rectangle(1, 1, 15, 15));

                        minimize.Image = image;

                        minimize.Click += Minimize_Click;
                        minimize.MouseEnter += Minimize_MouseEnter;
                        minimize.MouseLeave += Minimize_MouseLeave;

                        minimize.SizeMode = PictureBoxSizeMode.CenterImage;

                        parrotGradientPanel.Controls.Add(minimize);

                        minimize.BringToFront();
                    }

                    if (showMaximize)
                    {
                        maximize.BackColor = Color.Transparent;
                        maximize.Parent = parrotGradientPanel;
                        maximize.Cursor = Cursors.Hand;
                        maximize.Size = new Size(20, 40);
                        maximize.Location = new Point(20, 0);

                        Bitmap image2 = new(17, 17);
                        Graphics graphics2 = Graphics.FromImage(image2);

                        graphics2.SmoothingMode = SmoothingType;

                        graphics2.FillEllipse(new SolidBrush(Color.FromArgb(245, 190, 50)), new Rectangle(1, 1, 15, 15));

                        maximize.Image = image2;

                        maximize.Click += Maximize_Click;
                        maximize.MouseEnter += Maximize_MouseEnter;
                        maximize.MouseLeave += Maximize_MouseLeave;
                        maximize.SizeMode = PictureBoxSizeMode.CenterImage;

                        parrotGradientPanel.Controls.Add(maximize);

                        maximize.BringToFront();
                    }

                    exit.BackColor = Color.Transparent;
                    exit.Parent = parrotGradientPanel;
                    exit.Cursor = Cursors.Hand;
                    exit.Size = new Size(20, 40);
                    exit.Location = new Point(0, 0);

                    Bitmap image3 = new(17, 17);
                    Graphics graphics3 = Graphics.FromImage(image3);

                    graphics3.SmoothingMode = SmoothingType;

                    graphics3.FillEllipse(new SolidBrush(Color.FromArgb(235, 95, 80)), new Rectangle(1, 1, 15, 15));

                    exit.Image = image3;

                    exit.Click += Exit_Click;
                    exit.MouseEnter += Exit_MouseEnter;
                    exit.MouseLeave += Exit_MouseLeave;

                    exit.SizeMode = PictureBoxSizeMode.CenterImage;

                    parrotGradientPanel.Controls.Add(exit);

                    exit.BringToFront();

                    WorkingArea.Location = new Point(0, 50);

                    if (!SetBG)
                    {
                        BackColor = Color.FromArgb(236, 236, 236);
                        SetBG = true;
                    }

                    WorkingArea.BackColor = BackColor;
                    WorkingArea.Dock = DockStyle.Fill;

                    parrotFormHandle.HandleControl = label;
                    parrotFormHandle.HandleControl = parrotGradientPanel;

                    base.Controls.Add(WorkingArea);
                    base.Controls.Add(panel3);
                    base.Controls.Add(parrotGradientPanel);
                    break;
                case Style.Ubuntu:
                    ParrotGradientPanel parrotGradientPanel2 = new()
                    {
                        Style = ParrotGradientPanel.GradientStyle.Horizontal,
                        TopLeft = UbuntuLeftBackColor,
                        TopRight = UbuntuRightBackColor,
                        Size = new Size(base.Width, 30),
                        Dock = DockStyle.Top
                    };

                    label.ForeColor = UbuntuForeColor;
                    label.BackColor = Color.Transparent;
                    label.Parent = parrotGradientPanel2;
                    label.Size = new Size(base.Width - 50, 30);
                    label.Location = new Point(75, 0);
                    label.Font = new Font("Arial", 10f, FontStyle.Bold);
                    label.TextAlign = ContentAlignment.MiddleLeft;
                    label.AutoSize = false;
                    label.Text = titleText;

                    parrotGradientPanel2.Controls.Add(label);

                    int x2 = 50;

                    if (!showMinimize)
                    {
                        x2 = 25;
                    }

                    if (showMaximize)
                    {
                        maximize.BackColor = Color.Transparent;
                        maximize.Parent = parrotGradientPanel2;
                        maximize.Size = new Size(25, 30);
                        maximize.Location = new Point(x2, 0);

                        Bitmap image4 = new(17, 17);
                        Graphics graphics4 = Graphics.FromImage(image4);

                        graphics4.SmoothingMode = SmoothingType;

                        graphics4.FillEllipse(new SolidBrush(Color.FromArgb(120, 120, 110)), new Rectangle(1, 1, 15, 15));
                        graphics4.DrawEllipse(new Pen(Color.FromArgb(60, 60, 55), 1f), new Rectangle(1, 1, 15, 15));
                        graphics4.DrawRectangle(new Pen(Color.FromArgb(60, 60, 55), 1f), new Rectangle(6, 6, 5, 5));

                        maximize.Image = image4;
                        maximize.Click += Maximize_Click;
                        maximize.SizeMode = PictureBoxSizeMode.CenterImage;

                        parrotGradientPanel2.Controls.Add(maximize);

                        maximize.BringToFront();
                    }

                    if (showMaximize)
                    {
                        minimize.BackColor = Color.Transparent;
                        minimize.Parent = parrotGradientPanel2;
                        minimize.Size = new Size(25, 30);
                        minimize.Location = new Point(25, 0);

                        Bitmap image5 = new(17, 17);
                        Graphics graphics5 = Graphics.FromImage(image5);

                        graphics5.SmoothingMode = SmoothingType;

                        graphics5.FillEllipse(new SolidBrush(Color.FromArgb(120, 120, 110)), new Rectangle(1, 1, 15, 15));
                        graphics5.DrawEllipse(new Pen(Color.FromArgb(60, 60, 55), 1f), new Rectangle(1, 1, 15, 15));
                        graphics5.DrawLine(new Pen(Color.FromArgb(60, 60, 55), 1f), 6, 9, 11, 9);

                        minimize.Image = image5;
                        minimize.Click += Minimize_Click;
                        minimize.SizeMode = PictureBoxSizeMode.CenterImage;

                        parrotGradientPanel2.Controls.Add(minimize);

                        minimize.BringToFront();
                    }

                    exit.BackColor = Color.Transparent;
                    exit.Parent = parrotGradientPanel2;
                    exit.Size = new Size(25, 30);
                    exit.Location = new Point(0, 0);

                    Bitmap image6 = new(17, 17);
                    Graphics graphics6 = Graphics.FromImage(image6);

                    graphics6.SmoothingMode = SmoothingType;

                    graphics6.FillEllipse(new SolidBrush(Color.FromArgb(230, 95, 50)), new Rectangle(1, 1, 15, 15));
                    graphics6.DrawEllipse(new Pen(Color.FromArgb(60, 60, 55), 1f), new Rectangle(1, 1, 15, 15));

                    graphics6.DrawLine(new Pen(Color.FromArgb(60, 60, 55), 1f), 6, 6, 11, 11);
                    graphics6.DrawLine(new Pen(Color.FromArgb(60, 60, 55), 1f), 6, 11, 11, 6);

                    exit.Image = image6;
                    exit.Click += Exit_Click;
                    exit.SizeMode = PictureBoxSizeMode.CenterImage;

                    parrotGradientPanel2.Controls.Add(exit);
                    exit.BringToFront();

                    WorkingArea.Location = new Point(0, 50);

                    if (!SetBG)
                    {
                        BackColor = Color.FromArgb(240, 240, 240);
                        SetBG = true;
                    }

                    WorkingArea.BackColor = BackColor;

                    parrotFormHandle.HandleControl = label;
                    parrotFormHandle.HandleControl = parrotGradientPanel2;

                    WorkingArea.Dock = DockStyle.Fill;

                    base.Controls.Add(WorkingArea);
                    base.Controls.Add(parrotGradientPanel2);
                    break;
                default:
                    break;
            }

            base.ResumeLayout(true);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (exitApplication)
            {
                Environment.Exit(1);
                Application.Exit();
                return;
            }

            base.FindForm().Close();
            base.Dispose();
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            if (base.FindForm().WindowState == FormWindowState.Maximized)
            {
                base.FindForm().WindowState = FormWindowState.Normal;
                return;
            }

            base.FindForm().WindowState = FormWindowState.Maximized;
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            base.FindForm().WindowState = FormWindowState.Minimized;
        }

        private void Exit_MouseEnter(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(235, 95, 80)), new Rectangle(1, 1, 15, 15));
            graphics.DrawLine(new Pen(Color.Black, 1f), 6, 6, 11, 11);
            graphics.DrawLine(new Pen(Color.Black, 1f), 6, 11, 11, 6);

            exit.Image = image;
        }

        private void Exit_MouseLeave(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(235, 95, 80)), new Rectangle(1, 1, 15, 15));

            exit.Image = image;
        }

        private void Maximize_MouseEnter(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(245, 190, 50)), new Rectangle(1, 1, 15, 15));
            graphics.DrawRectangle(new Pen(Color.Black, 1f), new Rectangle(6, 6, 6, 6));

            maximize.Image = image;
        }

        private void Maximize_MouseLeave(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(245, 190, 50)), new Rectangle(1, 1, 15, 15));

            maximize.Image = image;
        }

        private void Minimize_MouseEnter(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 205, 90)), new Rectangle(1, 1, 15, 15));
            graphics.DrawLine(new Pen(Color.Black, 1f), 6, 9, 11, 9);

            minimize.Image = image;
        }

        private void Minimize_MouseLeave(object sender, EventArgs e)
        {
            Bitmap image = new(17, 17);
            Graphics graphics = Graphics.FromImage(image);

            graphics.SmoothingMode = SmoothingType;

            graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 205, 90)), new Rectangle(1, 1, 15, 15));

            minimize.Image = image;
        }

        private bool SetBG;
        private readonly PictureBox minimize = new();

        private readonly PictureBox exit = new();

        private readonly PictureBox maximize = new();

        private FormBorderStyle DefaultStyle;

        private Form DefaultForm;

        private string titleText = "Parrot Form";

        private bool showMaximize = true;

        private bool exitApplication = true;

        private bool showMinimize = true;

        private Color materialForeColor = Color.White;

        private Color materialBackColor = Color.DodgerBlue;

        private Color macOSForeColor = Color.FromArgb(40, 40, 40);

        private Color macOSLeftBackColor = Color.FromArgb(230, 230, 230);

        private Color macOSRightBackColor = Color.FromArgb(210, 210, 210);

        private Color macOSSeparatorColor = Color.FromArgb(173, 173, 173);

        private Color ubuntuForeColor = Color.FromArgb(220, 220, 210);

        private Color ubuntuLeftBackColor = Color.FromArgb(90, 85, 80);

        private Color ubuntuRightBackColor = Color.FromArgb(65, 65, 60);

        private Style formStyle = Style.MacOS;

        public enum Style
        {
            Material,
            MacOS,
            Ubuntu
        }
    }

    #endregion
}