#region Imports

using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Enum.Crown;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownMessageBox

    public partial class CrownMessageBox : CrownDialog
    {
        #region Field Region

        private readonly string _message;
        private int _maximumWidth = 350;

        #endregion

        #region Property Region

        [Description("Determines the maximum width of the message box when it autosizes around the displayed message.")]
        [DefaultValue(350)]
        public int MaximumWidth
        {
            get => _maximumWidth;
            set
            {
                _maximumWidth = value;
                CalculateSize();
            }
        }

        #endregion

        #region Constructor Region

        public CrownMessageBox()
        {
            InitializeComponent();

            ThemeProvider.Theme = ThemeProvider.Theme;
            BackColor = ThemeProvider.Theme.Colors.GreyBackground;
            lblText.ForeColor = ThemeProvider.Theme.Colors.LightText;
            Refresh();
        }

        public CrownMessageBox(string message, string title, DialogMessageBox icon, DialogButton buttons) : this()
        {
            Text = title;
            _message = message;

            DialogButtons = buttons;
            SetIcon(icon);
        }

        public CrownMessageBox(string message) : this(message, null, DialogMessageBox.None, DialogButton.Ok)
        { }

        public CrownMessageBox(string message, string title) : this(message, title, DialogMessageBox.None, DialogButton.Ok)
        { }

        public CrownMessageBox(string message, string title, DialogButton buttons) : this(message, title, DialogMessageBox.None, buttons)
        { }

        public CrownMessageBox(string message, string title, DialogMessageBox icon) : this(message, title, icon, DialogButton.Ok)
        { }

        #endregion

        #region Static Method Region

        public static DialogResult ShowInformation(string message, string caption, DialogButton buttons = DialogButton.Ok)
        {
            return ShowDialog(message, caption, DialogMessageBox.Information, buttons);
        }

        public static DialogResult ShowWarning(string message, string caption, DialogButton buttons = DialogButton.Ok)
        {
            return ShowDialog(message, caption, DialogMessageBox.Warning, buttons);
        }

        public static DialogResult ShowError(string message, string caption, DialogButton buttons = DialogButton.Ok)
        {
            return ShowDialog(message, caption, DialogMessageBox.Error, buttons);
        }

        private static DialogResult ShowDialog(string message, string caption, DialogMessageBox icon, DialogButton buttons)
        {
            using CrownMessageBox dlg = new(message, caption, icon, buttons);
            DialogResult result = dlg.ShowDialog();
            return result;
        }

        #endregion

        #region Method Region

        private void SetIcon(DialogMessageBox icon)
        {
            switch (icon)
            {
                case DialogMessageBox.None:
                    picIcon.Visible = false;
                    lblText.Left = 10;
                    break;
                case DialogMessageBox.Information:
                    picIcon.Image = Properties.Resources.info;
                    break;
                case DialogMessageBox.Warning:
                    picIcon.Image = Properties.Resources.warn;
                    break;
                case DialogMessageBox.Error:
                    picIcon.Image = Properties.Resources.err;
                    break;
            }
        }

        private void CalculateSize()
        {
            int width = 260; int height = 124;

            // Reset form back to original size
            Size = new(width, height);

            lblText.Text = string.Empty;
            lblText.AutoSize = true;
            lblText.Text = _message;

            // Set the minimum dialog size to whichever is bigger - the original size or the buttons.
            int minWidth = Math.Max(width, TotalButtonSize + 15);

            // Calculate the total size of the message
            int totalWidth = lblText.Right + 25;

            // Make sure we're not making the dialog bigger than the maximum size
            if (totalWidth < _maximumWidth)
            {
                // Width is smaller than the maximum width.
                // This means we can have a single-line message box.
                // Move the label to accomodate 
                width = totalWidth;
                lblText.Top = picIcon.Top + (picIcon.Height / 2) - (lblText.Height / 2);
            }
            else
            {
                // Width is larger than the maximum width.
                // Change the label size and wrap it.
                width = _maximumWidth;
                int offsetHeight = Height - picIcon.Height;
                lblText.AutoUpdateHeight = true;
                lblText.Width = width - lblText.Left - 25;
                height = offsetHeight + lblText.Height;
            }

            // Force the width to the minimum width
            if (width < minWidth)
            {
                width = minWidth;
            }

            // Set the new size of the dialog
            Size = new(width, height);
        }

        #endregion

        #region Event Handler Region

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CalculateSize();
        }

        #endregion
    }

    #endregion
}