#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSplashScreen

    public class ParrotSplashScreen : Component
    {
        [Category("Parrot")]
        [Browsable(true)]
        [Description("Allow dragging the splash")]
        public bool AllowDragging { get; set; } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show progressbar")]
        public bool ShowProgressBar { get; set; } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the splash elliptical")]
        public bool IsEllipse { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The corner radius if ellipse")]
        public int EllipseCornerRadius { get; set; } = 15;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Progressbar location")]
        public Point ProgressBarLocation { get; set; } = new Point(0, 224);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Progressbar style")]
        public ParrotFlatProgressBar.Style ProgressBarStyle { get; set; } = ParrotFlatProgressBar.Style.Material;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Progressbar border")]
        public bool ProgressBarBorder { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progressbar loaded color")]
        public Color LoadedColor { get; set; } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progressbar unloaded color")]
        public Color UnloadedColor { get; set; } = Color.FromArgb(30, 30, 30);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Amount of seconds splash is displayed for in milliseconds")]
        public int SecondsDisplayed { get; set; } = 3000;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The splash BackColor")]
        public Color BackColor { get; set; } = Color.FromArgb(30, 30, 30);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The splash size")]
        public Size SplashSize
        {
            get => splashSize;
            set => splashSize = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The splash icon")]
        public Icon SplashIcon { get; set; } = Properties.Resources.Taiizor1;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text location")]
        public Point TopTextLocation { get; set; } = new Point(0, 70);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text color")]
        public Color TopTextColor { get; set; } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text")]
        public string TopText { get; set; } = "Visual Studio";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text font family")]
        public FontFamily TopTextFontFamily { get; set; } = new("Arial");

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text size")]
        public int TopTextSize { get; set; } = 36;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text location")]
        public Point BottomTextLocation { get; set; } = new Point(51, 125);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text color")]
        public Color BottomTextColor { get; set; } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text")]
        public string BottomText { get; set; } = "ReaLTaizor Special Edition";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text font family")]
        public FontFamily BottomTextFontFamily { get; set; } = new("Arial");

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text size")]
        public int BottomTextSize { get; set; } = 16;

        public void InitializeLoader(Control mainForm)
        {
            baseForm = mainForm;

            ((Form)baseForm).WindowState = FormWindowState.Minimized;
            ((Form)baseForm).ShowInTaskbar = false;

            splashForm.Icon = SplashIcon;

            splashForm.BackColor = BackColor;
            splashForm.FormBorderStyle = FormBorderStyle.None;
            splashForm.StartPosition = FormStartPosition.CenterScreen;
            splashForm.Size = splashSize;

            background.Dock = DockStyle.Fill;
            background.BackColor = BackColor;

            splashForm.Controls.Add(background);

            progressBar.BarStyle = ProgressBarStyle;
            progressBar.ShowBorder = ProgressBarBorder;
            progressBar.InocmpletedColor = UnloadedColor;
            progressBar.CompleteColor = LoadedColor;
            progressBar.Value = 0;
            progressBar.Size = new Size(splashForm.Width, 10);
            progressBar.Location = ProgressBarLocation;

            if (!ShowProgressBar)
            {
                progressBar.Visible = false;
            }

            background.Controls.Add(progressBar);
            updateProgress.Interval = SecondsDisplayed / 100;
            updateProgress.Tick += UpdateLoader;
            text1.ForeColor = TopTextColor;
            text1.Font = new Font(TopTextFontFamily, TopTextSize);
            text1.Text = TopText;
            text1.BackColor = BackColor;
            text1.AutoSize = true;
            text1.Location = TopTextLocation;
            background.Controls.Add(text1);
            text2.ForeColor = BottomTextColor;
            text2.Font = new Font(BottomTextFontFamily, BottomTextSize);
            text2.Text = BottomText;
            text2.BackColor = BackColor;
            text2.AutoSize = true;
            text2.Location = BottomTextLocation;
            background.Controls.Add(text2);
            handle.DockAtTop = false;

            if (AllowDragging)
            {
                handle.HandleControl = background;
                handle2.HandleControl = text1;
                handle3.HandleControl = text2;
                handle4.HandleControl = progressBar;
            }

            if (IsEllipse)
            {
                ellipse.CornerRadius = EllipseCornerRadius;
                ellipse.EffectedForm = splashForm;
            }

            splashForm.Show();
            splashForm.BringToFront();

            updateProgress.Enabled = true;
        }

        private void UpdateLoader(object sender, EventArgs e)
        {
            if (progressBar.Value < 100)
            {
                ParrotFlatProgressBar parrotFlatProgressBar = progressBar;
                int value = parrotFlatProgressBar.Value;
                parrotFlatProgressBar.Value = value + 1;
                return;
            }

            progressBar.Dispose();
            updateProgress.Dispose();
            background.Dispose();
            text1.Dispose();
            text2.Dispose();
            handle.Dispose();
            handle2.Dispose();
            handle3.Dispose();
            handle4.Dispose();
            ellipse.Dispose();

            splashForm.Dispose();

            ((Form)baseForm).ShowInTaskbar = true;
            ((Form)baseForm).WindowState = FormWindowState.Normal;
        }

        private Size splashSize = new(450, 280);
        private readonly Form splashForm = new();
        private readonly ParrotFlatProgressBar progressBar = new();

        private readonly Timer updateProgress = new();

        private readonly System.Windows.Forms.Panel background = new();

        private readonly Label text1 = new();

        private readonly ParrotFormHandle handle = new();

        private readonly ParrotFormHandle handle2 = new();

        private readonly ParrotFormHandle handle3 = new();

        private readonly ParrotFormHandle handle4 = new();

        private readonly ParrotObjectEllipse ellipse = new();

        private readonly Label text2 = new();

        private Control baseForm;
    }

    #endregion
}