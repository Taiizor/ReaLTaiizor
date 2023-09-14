#region Imports

using ReaLTaiizor.Colors;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalMessageBox

    public class RoyalMessageBox : Forms.RoyalForm
    {
        private readonly RoyalButton okButton = new();
        private readonly RoyalButton yesButton = new();
        private readonly RoyalButton noButton = new();
        private readonly RoyalButton cancelButton = new();
        private readonly RoyalButton retryButton = new();

        public Form FormParent { get; set; }
        public string Content { get; set; }
        public string Caption { get; set; }
        public MessageBoxButtons Buttons { get; set; }
        public MessageBoxIcon Icon { get; set; }

        private static bool mode = true;
        public bool Mode
        {
            get => mode;
            set => mode = value;
        }

        public RoyalMessageBox() : base()
        {
            Font = new(new FontFamily("Segoe UI"), 9.75f, FontStyle.Regular);
            Moveable = false;
            ControlBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;

            okButton.Text = "Ok";
            okButton.Size = new(100, 35);
            okButton.Click += OkButton_Click;

            yesButton.Text = "Yes";
            yesButton.Size = new(100, 35);
            yesButton.Click += YesButton_Click;

            noButton.Text = "No";
            noButton.Size = new(100, 35);
            noButton.Click += NoButton_Click;

            cancelButton.Text = "Cancel";
            cancelButton.Size = new(100, 35);
            cancelButton.Click += CancelButton_Click;

            retryButton.Text = "Retry";
            retryButton.Size = new(100, 35);
            retryButton.Click += RetryButton_Click;

            Controls.Add(okButton);
            Controls.Add(yesButton);
            Controls.Add(noButton);
            Controls.Add(cancelButton);
            Controls.Add(retryButton);

            okButton.Hide();
            yesButton.Hide();
            noButton.Hide();
            cancelButton.Hide();
            retryButton.Hide();
        }

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

        public static DialogResult Show(Form form, string content)
        {
            return Show(form, content, "Royal Message Box Title", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(Form form, string content, string caption)
        {
            return Show(form, content, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public static DialogResult Show(Form form, string content, string caption, MessageBoxButtons buttons)
        {
            return Show(form, content, caption, buttons, MessageBoxIcon.None);
        }

        public static DialogResult Show(Form form, string content, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(form, content, caption, buttons, icon, mode);
        }

        public static DialogResult Show(Form form, string content, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool mode)
        {
            if (form == null)
            {
                throw new ArgumentNullException("RoyalMessageBox requires a valid form object in the first argument.");
            }

            RoyalMessageBox msgBox = new()
            {
                FormParent = form,
                Content = content,
                Caption = caption,
                Buttons = buttons,
                Icon = icon,
                Mode = mode,

                Size = form.Size,
                Location = form.Location
            };

            return msgBox.ShowDialog();
        }

        protected new DialogResult ShowDialog()
        {
            okButton.Hide();
            yesButton.Hide();
            noButton.Hide();
            cancelButton.Hide();
            retryButton.Hide();

            int messageBoxHeight = Height / 3;
            int buttonHeight = (messageBoxHeight * 2) - 45;
            int buttonWidth = okButton.Width;
            int oneButtonRight = Width - buttonWidth - 10;
            int twoButtonRight = Width - (buttonWidth * 2) - 20;
            int threeButtonRight = Width - (buttonWidth * 3) - 30;

            if (Buttons == MessageBoxButtons.OK)
            {
                okButton.Location = new(oneButtonRight, buttonHeight);
                okButton.Show();
            }
            else if (Buttons == MessageBoxButtons.OKCancel)
            {
                okButton.Location = new(twoButtonRight, buttonHeight);
                okButton.Show();

                cancelButton.Location = new(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else if (Buttons == MessageBoxButtons.YesNo)
            {
                yesButton.Location = new(twoButtonRight, buttonHeight);
                yesButton.Show();

                noButton.Location = new(oneButtonRight, buttonHeight);
                noButton.Show();
            }
            else if (Buttons == MessageBoxButtons.YesNoCancel)
            {
                yesButton.Location = new(threeButtonRight, buttonHeight);
                yesButton.Show();

                noButton.Location = new(twoButtonRight, buttonHeight);
                noButton.Show();

                cancelButton.Location = new(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else if (Buttons == MessageBoxButtons.RetryCancel)
            {
                retryButton.Location = new(twoButtonRight, buttonHeight);
                retryButton.Show();

                cancelButton.Location = new(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else
            {
                okButton.Location = new(oneButtonRight, buttonHeight);
                okButton.Show();
            }

            return base.ShowDialog();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.DarkGray)), e.ClipRectangle);

            Rectangle messageRect = new()
            {
                Size = new(Width, Height / 3),
                Location = new(0, (Height - (Height / 3)) / 2)
            };

            Font messageFont = new(Font.FontFamily, 12.75f, FontStyle.Regular);

            if (mode)
            {
                e.Graphics.FillRectangle(new SolidBrush(FormParent.BackColor), messageRect);
            }
            else
            {
                if (Icon is MessageBoxIcon.Warning or MessageBoxIcon.Exclamation)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 128, 0)), messageRect);
                }
                else if (Icon is MessageBoxIcon.Information or MessageBoxIcon.Asterisk)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), messageRect);
                }
                else if (Icon is MessageBoxIcon.Error or MessageBoxIcon.Hand or MessageBoxIcon.Stop)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), messageRect);
                }
                else if (Icon == MessageBoxIcon.Question)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DodgerBlue), messageRect);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(FormParent.BackColor), messageRect);
                }
            }

            SolidBrush textBrush = new(FormParent.ForeColor);
            //SolidBrush backBrush = new(parent.BackColor);

            if (!string.IsNullOrEmpty(Caption))
            {
                e.Graphics.DrawString(Caption, messageFont, new SolidBrush(RoyalColors.AccentColor), new PointF(messageRect.Left + 10, messageRect.Top + 10));
            }

            if (Icon != MessageBoxIcon.None)
            {
                if (mode)
                {
                    if (Icon is MessageBoxIcon.Warning or MessageBoxIcon.Exclamation)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Warning, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(Content))
                        {
                            e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                        }
                    }
                    else if (Icon is MessageBoxIcon.Information or MessageBoxIcon.Asterisk)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Information, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(Content))
                        {
                            e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                        }
                    }
                    else if (Icon is MessageBoxIcon.Error or MessageBoxIcon.Hand or MessageBoxIcon.Stop)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Error, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(Content))
                        {
                            e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                        }
                    }
                    else if (Icon == MessageBoxIcon.Question)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Question, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(Content))
                        {
                            e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Content))
                        {
                            e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Content))
                    {
                        e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));
                    }
                }

            }
            else if (!string.IsNullOrEmpty(Content))
            {
                e.Graphics.DrawString(Content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));
            }

            base.OnPaint(e);
        }
    }

    #endregion
}