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
        public bool AllowDragging
        {
            get => allowDragging;
            set => allowDragging = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show progressbar")]
        public bool ShowProgressBar
        {
            get => showProgressBar;
            set => showProgressBar = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the splash elliptical")]
        public bool IsEllipse
        {
            get => isEllipse;
            set => isEllipse = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The corner radius if ellipse")]
        public int EllipseCornerRadius
        {
            get => ellipseCornerRadius;
            set => ellipseCornerRadius = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Progressbar style")]
        public ParrotFlatProgressBar.Style ProgressBarStyle
        {
            get => progressBarStyle;
            set => progressBarStyle = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Progressbar border")]
        public bool ProgressBarBorder
        {
            get => progressBarBorder;
            set => progressBarBorder = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progressbar loaded color")]
        public Color LoadedColor
        {
            get => loadedColor;
            set => loadedColor = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The progressbar unloaded color")]
        public Color UnloadedColor
        {
            get => unloadedColor;
            set => unloadedColor = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Amount of seconds splash is displayed for in milliseconds")]
        public int SecondsDisplayed
        {
            get => secondsDisplayed;
            set => secondsDisplayed = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The splash BackColor")]
        public Color BackColor
        {
            get => backColor;
            set => backColor = value;
        }

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
        public Icon SplashIcon
        {
            get => splashIcon;
            set => splashIcon = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text color")]
        public Color TopTextColor
        {
            get => topTextColor;
            set => topTextColor = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text")]
        public string TopText
        {
            get => topText;
            set => topText = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top text size")]
        public int TopTextSize
        {
            get => topTextSize;
            set => topTextSize = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text color")]
        public Color BottomTextColor
        {
            get => bottomTextColor;
            set => bottomTextColor = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text")]
        public string BottomText
        {
            get => bottomText;
            set => bottomText = value;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom text size")]
        public int BottomTextSize
        {
            get => bottomTextSize;
            set => bottomTextSize = value;
        }

        public void InitializeLoader(Control mainForm)
        {
            baseForm = mainForm;

            ((Form)baseForm).WindowState = FormWindowState.Minimized;
            ((Form)baseForm).ShowInTaskbar = false;

            splashForm.Icon = splashIcon;

            splashForm.BackColor = backColor;
            splashForm.FormBorderStyle = FormBorderStyle.None;
            splashForm.StartPosition = FormStartPosition.CenterScreen;
            splashForm.Size = splashSize;

            background.Dock = DockStyle.Fill;
            background.BackColor = backColor;

            splashForm.Controls.Add(background);

            progressBar.BarStyle = progressBarStyle;
            progressBar.ShowBorder = progressBarBorder;
            progressBar.InocmpletedColor = unloadedColor;
            progressBar.CompleteColor = loadedColor;
            progressBar.Value = 0;
            progressBar.Size = new Size(splashForm.Width, 10);
            progressBar.Location = new Point(0, splashForm.Height / 5 * 4);

            if (!showProgressBar)
            {
                progressBar.Visible = false;
            }

            background.Controls.Add(progressBar);
            updateProgress.Interval = secondsDisplayed / 100;
            updateProgress.Tick += UpdateLoader;
            text1.ForeColor = topTextColor;
            text1.Font = new Font("Ariel", (float)topTextSize);
            text1.Text = topText;
            text1.BackColor = backColor;
            text1.AutoSize = true;
            text1.Location = new Point(0, splashForm.Height / 4);
            background.Controls.Add(text1);
            text2.ForeColor = bottomTextColor;
            text2.Font = new Font("Ariel", (float)bottomTextSize);
            text2.Text = bottomText;
            text2.BackColor = backColor;
            text2.AutoSize = true;
            text2.Location = new Point(text1.Width / 2 - text2.Width, text1.Location.Y + text1.Height);
            background.Controls.Add(text2);
            handle.DockAtTop = false;

            if (allowDragging)
            {
                handle.HandleControl = background;
                handle2.HandleControl = text1;
                handle3.HandleControl = text2;
                handle4.HandleControl = progressBar;
            }
            if (isEllipse)
            {
                ellipse.CornerRadius = ellipseCornerRadius;
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

        private bool allowDragging = true;

        private bool showProgressBar = true;

        private bool isEllipse;

        private int ellipseCornerRadius = 15;

        private ParrotFlatProgressBar.Style progressBarStyle = ParrotFlatProgressBar.Style.Material;

        private bool progressBarBorder;

        private Color loadedColor = Color.DodgerBlue;

        private Color unloadedColor = Color.FromArgb(30, 30, 30);

        private int secondsDisplayed = 3000;

        private Color backColor = Color.FromArgb(30, 30, 30);

        private Size splashSize = new(450, 280);

        private Color topTextColor = Color.White;

        private string topText = "Visual Studio";

        private int topTextSize = 36;

        private Color bottomTextColor = Color.White;

        private string bottomText = "ReaLTaizor Special Edition";

        private int bottomTextSize = 16;

        private readonly Form splashForm = new();

        private Icon splashIcon = Properties.Resources.Taiizor1;

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