#region Imports

using ReaLTaiizor.Enum.Material;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialSnackBar

    public class MaterialSnackBar : MaterialForm
    {
        private const int TOP_PADDING_SINGLE_LINE = 6;
        private const int LEFT_RIGHT_PADDING = 16;
        private const int BUTTON_PADDING = 8;
        private const int BUTTON_HEIGHT = 36;

        private MaterialButton _actionButton = new();
        private Timer _duration = new(); // Timer that checks when the drop down is fully visible

        private AnimationManager _AnimationManager;
        private bool _closingAnimationDone = false;
        private bool _useAccentColor;
        private bool CloseAnimation = false;

        #region "Events"

        [Category("Action")]
        [Description("Fires when Action button is clicked")]
        public event EventHandler ActionButtonClick;

        #endregion

        [Category("Material"), DefaultValue(false), DisplayName("Use Accent Color")]
        public bool UseAccentColor
        {
            get => _useAccentColor;
            set { _useAccentColor = value; Invalidate(); }
        }

        [Category("Material"), DefaultValue(2000)]
        public int Duration
        {
            get => _duration.Interval;
            set => _duration.Interval = value;
        }

        private string _text;
        [Category("Material"), DefaultValue("SnackBar text")]
        public new string Text
        {
            get => _text;
            set
            {
                _text = value;
                UpdateRects();
                Invalidate();
            }
        }

        private bool _showActionButton;
        [Category("Material"), DefaultValue(false), DisplayName("Show Action Button")]
        public bool ShowActionButton
        {
            get => _showActionButton;
            set { _showActionButton = value; UpdateRects(); Invalidate(); }
        }

        private string _actionButtonText;
        [Category("Material"), DefaultValue("OK")]
        public string ActionButtonText
        {
            get => _actionButtonText;
            set
            {
                _actionButtonText = value;
                Invalidate();
            }
        }

        //public ObservableCollection<MaterialButton> Buttons { get; set; }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public MaterialSnackBar(string Text, int Duration, bool ShowActionButton, string ActionButtonText, bool UseAccentColor)
        {
            this.Text = Text;
            this.Duration = Duration;
            TopMost = true;
            ShowInTaskbar = false;
            Sizable = false;

            BackColor = SkinManager.SnackBarBackgroundColor;
            FormStyle = FormStyles.StatusAndActionBar_None;

            this.ActionButtonText = ActionButtonText;
            this.UseAccentColor = UseAccentColor;
            Height = 48;
            MinimumSize = new Size(344, 48);
            MaximumSize = new Size(568, 48);

            this.ShowActionButton = ShowActionButton;

            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6));

            _AnimationManager = new AnimationManager();
            _AnimationManager.AnimationType = AnimationType.EaseOut;
            _AnimationManager.Increment = 0.03;
            _AnimationManager.OnAnimationProgress += _AnimationManager_OnAnimationProgress;

            _duration.Tick += new EventHandler(duration_Tick);

            _actionButton = new MaterialButton
            {
                AutoSize = false,
                NoAccentTextColor = SkinManager.SnackBarTextButtonNoAccentTextColor,
                DrawShadows = false,
                Type = MaterialButton.MaterialButtonType.Text,
                UseAccentColor = _useAccentColor,
                Visible = _showActionButton,
                Text = _actionButtonText
            };
            _actionButton.Click += (sender, e) =>
            {
                ActionButtonClick?.Invoke(this, new EventArgs());
                _closingAnimationDone = false;
                Close();
            };

            if (!Controls.Contains(_actionButton))
            {
                Controls.Add(_actionButton);
            }

            UpdateRects();

        }

        public MaterialSnackBar() : this("SnackBar Text", 3000, false, "OK", false)
        {
        }

        public MaterialSnackBar(string Text) : this(Text, 3000, false, "OK", false)
        {
        }

        public MaterialSnackBar(string Text, int Duration) : this(Text, Duration, false, "OK", false)
        {
        }

        public MaterialSnackBar(string Text, string ActionButtonText) : this(Text, 3000, true, ActionButtonText, false)
        {
        }

        public MaterialSnackBar(string Text, string ActionButtonText, bool UseAccentColor) : this(Text, 3000, true, ActionButtonText, UseAccentColor)
        {
        }

        public MaterialSnackBar(string Text, int Duration, string ActionButtonText) : this(Text, Duration, true, ActionButtonText, false)
        {
        }

        public MaterialSnackBar(string Text, int Duration, string ActionButtonText, bool UseAccentColor) : this(Text, Duration, true, ActionButtonText, UseAccentColor)
        {
        }

        private void UpdateRects()
        {
            if (_showActionButton == true)
            {
                int _buttonWidth = TextRenderer.MeasureText(ActionButtonText, SkinManager.GetFontByType(MaterialSkinManager.FontType.Button)).Width + 32;
                Rectangle _actionbuttonBounds = new(Width - BUTTON_PADDING - _buttonWidth, TOP_PADDING_SINGLE_LINE, _buttonWidth, BUTTON_HEIGHT);
                _actionButton.Width = _actionbuttonBounds.Width;
                _actionButton.Height = _actionbuttonBounds.Height;
                _actionButton.Text = _actionButtonText;
                _actionButton.Top = _actionbuttonBounds.Top;
                _actionButton.UseAccentColor = _useAccentColor;
            }
            else
            {
                _actionButton.Width = 0;
            }
            _actionButton.Left = Width - BUTTON_PADDING - _actionButton.Width;  //Button minimum width management
            _actionButton.Visible = _showActionButton;

            Width = TextRenderer.MeasureText(_text, SkinManager.GetFontByType(MaterialSkinManager.FontType.Body2)).Width + (2 * LEFT_RIGHT_PADDING) + _actionButton.Width + 48;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6));

        }
        private void duration_Tick(object sender, EventArgs e)
        {
            _duration.Stop();
            _closingAnimationDone = false;
            Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRects();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Location = new Point(Convert.ToInt32(Owner.Location.X + (Owner.Width / 2) - (Width / 2)), Convert.ToInt32(Owner.Location.Y + Owner.Height - 60));
            _AnimationManager.StartNewAnimation(AnimationDirection.In);
            _duration.Start();
        }

        void _AnimationManager_OnAnimationProgress(object sender)
        {
            if (CloseAnimation)
            {
                Opacity = _AnimationManager.GetProgress();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.Clear(BackColor);

            // Calc text Rect
            Rectangle textRect = new(
                LEFT_RIGHT_PADDING,
                0,
                Width - (2 * LEFT_RIGHT_PADDING) - _actionButton.Width,
                Height);

            //Draw  Text
            using MaterialNativeTextRenderer NativeText = new(g);
            // Draw header text
            NativeText.DrawTransparentText(
                _text,
                SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Body2),
                SkinManager.SnackBarTextHighEmphasisColor,
                textRect.Location,
                textRect.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = !_closingAnimationDone;
            if (!_closingAnimationDone)
            {
                CloseAnimation = true;
                _AnimationManager.Increment = 0.06;
                _AnimationManager.OnAnimationFinished += _AnimationManager_OnAnimationFinished;
                _AnimationManager.StartNewAnimation(AnimationDirection.Out);
            }
            base.OnClosing(e);
        }

        void _AnimationManager_OnAnimationFinished(object sender)
        {
            _closingAnimationDone = true;
            Close();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _closingAnimationDone = false;
            Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(344, 48);
            this.Name = "SnackBar";
            this.ResumeLayout(false);
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        return;
                    }

                    break;
            }

            base.WndProc(ref message);
        }

        public new void Show()
        {
            if (Owner == null)
            {
                throw new Exception("Owner is null. Set Owner first.");
            }
        }
    }

    #endregion
}