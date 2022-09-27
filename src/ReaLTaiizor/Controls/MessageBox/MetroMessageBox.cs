#region Imports

using ReaLTaiizor.Extension.Metro;
using ReaLTaiizor.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MetroMessageBox

    public class MetroMessageBox : MetroForm
    {
        #region Internal vars

        private Size _buttonSize;
        private MetroDefaultButton _okButton;
        private MetroDefaultButton _yesButton;
        private MetroDefaultButton _noButton;
        private MetroDefaultButton _cancelButton;
        private MetroDefaultButton _retryButton;
        private MetroDefaultButton _abortButton;
        private MetroDefaultButton _ignoreButton;

        #endregion

        #region Properties

        private Form OwnerForm { get; set; }

        public string Content { get; set; }

        public string Caption { get; set; }

        public MessageBoxButtons Buttons { get; set; }

        public new MessageBoxIcon Icon { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static new Color BackgroundColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static new Color BorderColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static Color ForegroundColor { get; set; }


        #endregion

        #region Constructor

        private MetroMessageBox()
        {
            base.Font = MetroFonts.Regular(9.5f);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            _buttonSize = new(95, 32);
            ApplyTheme();
            EvaluateControls();
            AddControls();
            //HideControls();
        }

        private void EvaluateControls()
        {
            EvaluateOkeyButton();
            EvaluateYesButton();
            EvaluateNoButton();
            EvaluateCancelButton();
            EvaluateRetryButton();
            EvaluateAbortButton();
            EvaluateIgnoreButton();
        }

        private void AddControls()
        {
            Controls.Add(_okButton);
            Controls.Add(_yesButton);
            Controls.Add(_noButton);
            Controls.Add(_cancelButton);
            Controls.Add(_retryButton);
            Controls.Add(_abortButton);
            Controls.Add(_ignoreButton);
        }

        private void EvaluateRetryButton()
        {
            _retryButton = new MetroDefaultButton
            {
                Text = @"Retry",
                Size = _buttonSize,
                Visible = false
            };
            _retryButton.Click += RetryButton_Click;
        }

        private void EvaluateCancelButton()
        {
            _cancelButton = new MetroDefaultButton
            {
                Text = @"Cancel",
                Size = _buttonSize,
                Visible = false
            };
            _cancelButton.Click += CancelButton_Click;
        }

        private void EvaluateNoButton()
        {
            _noButton = new MetroDefaultButton
            {
                Text = @"No",
                Size = _buttonSize,
                Visible = false
            };
            _noButton.Click += NoButton_Click;
        }

        private void EvaluateYesButton()
        {
            _yesButton = new MetroDefaultButton
            {
                Text = @"Yes",
                Size = _buttonSize,
                Visible = false
            };
            _yesButton.Click += YesButton_Click;
        }

        private void EvaluateOkeyButton()
        {
            _okButton = new MetroDefaultButton
            {
                Text = @"Ok",
                Size = _buttonSize,
                Visible = false
            };
            _okButton.Click += OkButton_Click;
        }

        private void EvaluateAbortButton()
        {
            _abortButton = new MetroDefaultButton
            {
                Text = @"Abort",
                Size = _buttonSize,
                Visible = false

            };
            _abortButton.Click += AbortButton_Click;
        }

        private void EvaluateIgnoreButton()
        {
            _ignoreButton = new MetroDefaultButton
            {
                Text = @"Ignore",
                Size = _buttonSize,
                Visible = false
            };
            _ignoreButton.Click += IgnoreButton_Click;
        }

        #endregion

        #region Events

        private void RetryButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void IgnoreButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }

        public static DialogResult Show(MetroForm form, string content)
        {
            return Show(form, content, form.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(MetroForm form, string content, string caption)
        {
            return Show(form, content, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(MetroForm form, string content, string caption, MessageBoxButtons buttons)
        {
            return Show(form, content, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(MetroForm form, string content, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            const string message = @"MetroMessageBox requires a form, use 'this' as the first parameter in the place you use MetroMessageBox.";
            MetroMessageBox msgBox = new()
            {
                OwnerForm = form ?? throw new ArgumentNullException(message),
                Content = content,
                Caption = caption,
                Buttons = buttons,
                Size = new(form.Width - 2, (form.Height / 3) - 1),
                Location = new(form.Location.X, (form.Height / 2) - 1)
            };
            if (icon is MessageBoxIcon.Error or MessageBoxIcon.Stop)
            {
                BackgroundColor = Color.FromArgb(210, 50, 45);
                BorderColor = Color.FromArgb(210, 50, 45);
                ForegroundColor = Color.White;
            }
            else if (icon == MessageBoxIcon.Information)
            {
                BackgroundColor = Color.FromArgb(60, 180, 218);
                BorderColor = Color.FromArgb(60, 180, 218);
                ForegroundColor = Color.White;
            }
            else if (icon == MessageBoxIcon.Question)
            {
                BackgroundColor = Color.FromArgb(70, 165, 70);
                BorderColor = Color.FromArgb(70, 165, 70);
                ForegroundColor = Color.White;
            }
            else if (icon is MessageBoxIcon.Exclamation or MessageBoxIcon.Warning)
            {
                BackgroundColor = Color.FromArgb(237, 156, 40);
                BorderColor = Color.FromArgb(237, 156, 40);
                ForegroundColor = Color.White;
            }
            else if (icon is MessageBoxIcon.None or MessageBoxIcon.Asterisk or MessageBoxIcon.Hand)
            {
                BackgroundColor = Color.White;
                BorderColor = Color.FromArgb(65, 177, 225);
                ForegroundColor = Color.Black;
            }

            return msgBox.ShowDialog();
        }

        protected new DialogResult ShowDialog()
        {

            int buttonHeight = Height - 45;
            int firstButton = Width - _buttonSize.Width - 10;
            int secondButoon = Width - (_buttonSize.Width * 2) - 20;
            switch (Buttons)
            {
                case MessageBoxButtons.OK:
                    _okButton.Location = new(firstButton, buttonHeight);
                    _okButton.Visible = true;
                    break;
                case MessageBoxButtons.OKCancel:
                    _okButton.Location = new(secondButoon, buttonHeight);
                    _okButton.Visible = true;
                    _cancelButton.Location = new(firstButton, buttonHeight);
                    _cancelButton.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    _yesButton.Location = new(secondButoon, buttonHeight);
                    _yesButton.Visible = true;
                    _noButton.Location = new(firstButton, buttonHeight);
                    _noButton.Visible = true;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    _yesButton.Location = new(Width - (_buttonSize.Width * 3) - 30, buttonHeight);
                    _yesButton.Visible = true;
                    _noButton.Location = new(secondButoon, buttonHeight);
                    _noButton.Visible = true;
                    _cancelButton.Location = new(firstButton, buttonHeight);
                    _cancelButton.Visible = true;
                    break;
                case MessageBoxButtons.RetryCancel:
                    _retryButton.Location = new(secondButoon, buttonHeight);
                    _retryButton.Visible = true;
                    _cancelButton.Location = new(firstButton, buttonHeight);
                    _cancelButton.Visible = true;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    _abortButton.Location = new(Width - (_buttonSize.Width * 3) - 30, buttonHeight);
                    _abortButton.Visible = true;
                    _retryButton.Location = new(secondButoon, buttonHeight);
                    _retryButton.Visible = true;
                    _ignoreButton.Location = new(firstButton, buttonHeight);
                    _ignoreButton.Visible = true;
                    break;
                default:
                    _okButton.Location = new(firstButton, buttonHeight);
                    _okButton.Visible = true;
                    break;
            }
            return base.ShowDialog();
        }

        #endregion

        #region Draw Dialog

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new(0, (OwnerForm.Height - (OwnerForm.Height / 2)) / 250, OwnerForm.Width - 3, (OwnerForm.Height / 3) - 3);

            using SolidBrush bg = new(BackgroundColor);
            using SolidBrush CTNT = new(ForegroundColor);
            using Pen p = new(BorderColor);
            G.FillRectangle(bg, rect);
            G.DrawString(Caption, Font, CTNT, new PointF(rect.X + 10, rect.Y + 10));
            G.DrawString(Content, Font, CTNT, new PointF(rect.X + 10, rect.Y + 50));
            G.DrawRectangle(p, rect);
        }

        #endregion

    }

    #endregion
}