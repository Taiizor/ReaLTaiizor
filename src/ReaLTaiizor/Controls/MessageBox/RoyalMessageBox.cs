#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Colors;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RoyalMessageBox

    public class RoyalMessageBox : Forms.RoyalForm
    {
        RoyalButton okButton = new RoyalButton();
        RoyalButton yesButton = new RoyalButton();
        RoyalButton noButton = new RoyalButton();
        RoyalButton cancelButton = new RoyalButton();
        RoyalButton retryButton = new RoyalButton();

        Form parent;
        public Form FormParent
        {
            get { return parent; }
            set { parent = value; }
        }

        string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        string caption;
        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        MessageBoxButtons buttons;
        public MessageBoxButtons Buttons
        {
            get { return buttons; }
            set { buttons = value; }
        }

        MessageBoxIcon icon;
        public MessageBoxIcon Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        static bool mode = true;
        public bool Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public RoyalMessageBox() : base()
        {
            Font = new Font(new FontFamily("Segoe UI"), 9.75f, FontStyle.Regular);
            Moveable = false;
            ControlBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;

            okButton.Text = "Ok";
            okButton.Size = new Size(100, 35);
            okButton.Click += OkButton_Click;

            yesButton.Text = "Yes";
            yesButton.Size = new Size(100, 35);
            yesButton.Click += YesButton_Click;

            noButton.Text = "No";
            noButton.Size = new Size(100, 35);
            noButton.Click += NoButton_Click;

            cancelButton.Text = "Cancel";
            cancelButton.Size = new Size(100, 35);
            cancelButton.Click += CancelButton_Click;

            retryButton.Text = "Retry";
            retryButton.Size = new Size(100, 35);
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

        void RetryButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        void OkButton_Click(object sender, EventArgs e)
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
                throw new ArgumentNullException("RoyalMessageBox requires a valid form object in the first argument.");

            RoyalMessageBox msgBox = new RoyalMessageBox();
            msgBox.parent = form;
            msgBox.content = content;
            msgBox.caption = caption;
            msgBox.buttons = buttons;
            msgBox.icon = icon;
            msgBox.Mode = mode;

            msgBox.Size = form.Size;
            msgBox.Location = form.Location;

            return msgBox.ShowDialog();
        }

        protected new DialogResult ShowDialog()
        {
            okButton.Hide();
            yesButton.Hide();
            noButton.Hide();
            cancelButton.Hide();
            retryButton.Hide();

            int messageBoxHeight = (Height / 3);
            int buttonHeight = (messageBoxHeight * 2) - 45;
            int buttonWidth = okButton.Width;
            int oneButtonRight = (Width - buttonWidth) - 10;
            int twoButtonRight = (Width - (buttonWidth * 2)) - 20;
            int threeButtonRight = (Width - (buttonWidth * 3)) - 30;

            if (buttons == MessageBoxButtons.OK)
            {
                okButton.Location = new Point(oneButtonRight, buttonHeight);
                okButton.Show();
            }
            else if (buttons == MessageBoxButtons.OKCancel)
            {
                okButton.Location = new Point(twoButtonRight, buttonHeight);
                okButton.Show();

                cancelButton.Location = new Point(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {
                yesButton.Location = new Point(twoButtonRight, buttonHeight);
                yesButton.Show();

                noButton.Location = new Point(oneButtonRight, buttonHeight);
                noButton.Show();
            }
            else if (buttons == MessageBoxButtons.YesNoCancel)
            {
                yesButton.Location = new Point(threeButtonRight, buttonHeight);
                yesButton.Show();

                noButton.Location = new Point(twoButtonRight, buttonHeight);
                noButton.Show();

                cancelButton.Location = new Point(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else if (buttons == MessageBoxButtons.RetryCancel)
            {
                retryButton.Location = new Point(twoButtonRight, buttonHeight);
                retryButton.Show();

                cancelButton.Location = new Point(oneButtonRight, buttonHeight);
                cancelButton.Show();
            }
            else
            {
                okButton.Location = new Point(oneButtonRight, buttonHeight);
                okButton.Show();
            }

            return base.ShowDialog();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.DarkGray)), e.ClipRectangle);

            Rectangle messageRect = new Rectangle();
            messageRect.Size = new Size(Width, (Height / 3));
            messageRect.Location = new Point(0, ((Height - (Height / 3)) / 2));

            Font messageFont = new Font(Font.FontFamily, 12.75f, FontStyle.Regular);

            if (mode)
                e.Graphics.FillRectangle(new SolidBrush(parent.BackColor), messageRect);
            else
            {
                if (icon == MessageBoxIcon.Warning || icon == MessageBoxIcon.Exclamation)
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 128, 0)), messageRect);
                else if (icon == MessageBoxIcon.Information || icon == MessageBoxIcon.Asterisk)
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), messageRect);
                else if (icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Hand || icon == MessageBoxIcon.Stop)
                    e.Graphics.FillRectangle(new SolidBrush(Color.Crimson), messageRect);
                else if (icon == MessageBoxIcon.Question)
                    e.Graphics.FillRectangle(new SolidBrush(Color.DodgerBlue), messageRect);
                else
                    e.Graphics.FillRectangle(new SolidBrush(parent.BackColor), messageRect);
            }

            SolidBrush textBrush = new SolidBrush(parent.ForeColor);
            SolidBrush backBrush = new SolidBrush(parent.BackColor);

            if (!string.IsNullOrEmpty(caption))
                e.Graphics.DrawString(caption, messageFont, new SolidBrush(RoyalColors.AccentColor), new PointF(messageRect.Left + 10, messageRect.Top + 10));
            
            if (icon != MessageBoxIcon.None)
            {
                if (mode)
                {
                    if (icon == MessageBoxIcon.Warning || icon == MessageBoxIcon.Exclamation)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Warning, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(content))
                            e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                    }
                    else if (icon == MessageBoxIcon.Information || icon == MessageBoxIcon.Asterisk)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Information, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(content))
                            e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                    }
                    else if (icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Hand || icon == MessageBoxIcon.Stop)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Error, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(content))
                            e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                    }
                    else if (icon == MessageBoxIcon.Question)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Question, new Rectangle(messageRect.Left + 10, messageRect.Top + 40, 64, 64));
                        if (!string.IsNullOrEmpty(content))
                            e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 64 + 10, messageRect.Top + 18 + 40));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(content))
                            e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(content))
                        e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));
                }

            }
            else if (!string.IsNullOrEmpty(content))
                e.Graphics.DrawString(content, messageFont, textBrush, new PointF(messageRect.Left + 10, messageRect.Top + 40));

            base.OnPaint(e);
        }
    }

    #endregion
}