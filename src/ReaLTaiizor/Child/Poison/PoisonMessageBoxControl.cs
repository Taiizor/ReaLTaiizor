#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Localization.Poison;
using ReaLTaiizor.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Child.Poison
{
    #region PoisonMessageBoxControlChild

    public partial class PoisonMessageBoxControl : Form
    {
        private readonly PoisonLocalize poisonLocalize = null;

        public PoisonMessageBoxControl()
        {
            InitializeComponent();

            _properties = new PoisonMessageBoxProperties(this);

            StylizeButton(poisonButton1);
            StylizeButton(poisonButton2);
            StylizeButton(poisonButton3);

            poisonButton1.Click += new EventHandler(Button_Click);
            poisonButton2.Click += new EventHandler(Button_Click);
            poisonButton3.Click += new EventHandler(Button_Click);

            poisonLocalize = new PoisonLocalize(this);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _defaultColor = Color.FromArgb(57, 179, 215);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _errorColor = Color.FromArgb(210, 50, 45);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _warningColor = Color.FromArgb(237, 156, 40);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _success = Color.FromArgb(71, 164, 71);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Color _question = Color.FromArgb(71, 164, 71);

        public System.Windows.Forms.Panel Body { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly PoisonMessageBoxProperties _properties = null;

        public PoisonMessageBoxProperties Properties => _properties;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DialogResult _result = DialogResult.None;

        public DialogResult Result => _result;

        public void ArrangeApperance()
        {
            titleLabel.Text = _properties.Title;
            messageLabel.Text = _properties.Message;

            switch (_properties.Icon)
            {
                case MessageBoxIcon.Exclamation:
                    Body.BackColor = _warningColor;
                    break;
                case MessageBoxIcon.Error:
                    Body.BackColor = _errorColor;
                    break;
                default: break;
            }

            switch (_properties.Buttons)
            {
                case MessageBoxButtons.OK:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Ok");
                    poisonButton1.Location = poisonButton3.Location;
                    poisonButton1.Tag = DialogResult.OK;

                    EnableButton(poisonButton2, false);
                    EnableButton(poisonButton3, false);
                    break;
                case MessageBoxButtons.OKCancel:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Ok");
                    poisonButton1.Location = poisonButton2.Location;
                    poisonButton1.Tag = DialogResult.OK;

                    EnableButton(poisonButton2);

                    poisonButton2.Text = poisonLocalize.Translate("Cancel");
                    poisonButton2.Location = poisonButton3.Location;
                    poisonButton2.Tag = DialogResult.Cancel;

                    EnableButton(poisonButton3, false);
                    break;
                case MessageBoxButtons.RetryCancel:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Retry");
                    poisonButton1.Location = poisonButton2.Location;
                    poisonButton1.Tag = DialogResult.Retry;

                    EnableButton(poisonButton2);

                    poisonButton2.Text = poisonLocalize.Translate("Cancel");
                    poisonButton2.Location = poisonButton3.Location;
                    poisonButton2.Tag = DialogResult.Cancel;

                    EnableButton(poisonButton3, false);
                    break;
                case MessageBoxButtons.YesNo:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Yes");
                    poisonButton1.Location = poisonButton2.Location;
                    poisonButton1.Tag = DialogResult.Yes;

                    EnableButton(poisonButton2);

                    poisonButton2.Text = poisonLocalize.Translate("No");
                    poisonButton2.Location = poisonButton3.Location;
                    poisonButton2.Tag = DialogResult.No;

                    EnableButton(poisonButton3, false);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Yes");
                    poisonButton1.Tag = DialogResult.Yes;

                    EnableButton(poisonButton2);

                    poisonButton2.Text = poisonLocalize.Translate("No");
                    poisonButton2.Tag = DialogResult.No;

                    EnableButton(poisonButton3);

                    poisonButton3.Text = poisonLocalize.Translate("Cancel");
                    poisonButton3.Tag = DialogResult.Cancel;

                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    EnableButton(poisonButton1);

                    poisonButton1.Text = poisonLocalize.Translate("Abort");
                    poisonButton1.Tag = DialogResult.Abort;

                    EnableButton(poisonButton2);

                    poisonButton2.Text = poisonLocalize.Translate("Retry");
                    poisonButton2.Tag = DialogResult.Retry;

                    EnableButton(poisonButton3);

                    poisonButton3.Text = poisonLocalize.Translate("Ignore");
                    poisonButton3.Tag = DialogResult.Ignore;

                    break;
                default: break;
            }

            Body.BackColor = _properties.Icon switch
            {
                MessageBoxIcon.Error => _errorColor,
                MessageBoxIcon.Warning => _warningColor,
                MessageBoxIcon.Information => _defaultColor,
                MessageBoxIcon.Question => _question,
                _ => Color.DarkGray,
            };
        }

        private static void EnableButton(PoisonButton button)
        {
            EnableButton(button, true);
        }

        private static void EnableButton(PoisonButton button, bool enabled)
        {
            button.Enabled = enabled; button.Visible = enabled;
        }

        public void SetDefaultButton()
        {
            switch (_properties.DefaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    if (poisonButton1 != null)
                    {
                        if (poisonButton1.Enabled)
                        {
                            poisonButton1.Focus();
                        }
                    }

                    break;
                case MessageBoxDefaultButton.Button2:
                    if (poisonButton2 != null)
                    {
                        if (poisonButton2.Enabled)
                        {
                            poisonButton2.Focus();
                        }
                    }

                    break;
                case MessageBoxDefaultButton.Button3:
                    if (poisonButton3 != null)
                    {
                        if (poisonButton3.Enabled)
                        {
                            poisonButton3.Focus();
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            //PoisonButton button = (PoisonButton)sender;
            //button.BackColor = PoisonPaint.BackColor.Button.Press(ThemeStyle.Light);
            //button.FlatAppearance.BorderColor = PoisonPaint.BorderColor.Button.Press(ThemeStyle.Light);
            //button.ForeColor = PoisonPaint.ForeColor.Button.Press(ThemeStyle.Light);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            StylizeButton((PoisonButton)sender, true);
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            StylizeButton((PoisonButton)sender);
        }

        private void StylizeButton(PoisonButton button)
        {
            StylizeButton(button, false);
        }

        private void StylizeButton(PoisonButton button, bool hovered)
        {
            button.Cursor = Cursors.Hand;

            button.MouseClick -= Button_MouseClick;
            button.MouseClick += Button_MouseClick;

            button.MouseEnter -= Button_MouseEnter;
            button.MouseEnter += Button_MouseEnter;

            button.MouseLeave -= Button_MouseLeave;
            button.MouseLeave += Button_MouseLeave;

            //if (hovered)
            //{
            //    button.FlatAppearance.BorderColor = PoisonPaint.BorderColor.Button.Hover(ThemeStyle.Light);
            //    button.ForeColor = PoisonPaint.ForeColor.Button.Hover(ThemeStyle.Light);
            //}
            //else
            //{
            //    button.BackColor = PoisonPaint.BackColor.Button.Normal(ThemeStyle.Light);
            //    button.FlatAppearance.BorderColor = Color.SlateGray;
            //    button.FlatAppearance.MouseOverBackColor = PoisonPaint.BorderColor.Button.Hover(ThemeStyle.Light);
            //    button.ForeColor = PoisonPaint.ForeColor.Button.Normal(ThemeStyle.Light);
            //    button.FlatAppearance.BorderSize = 1;
            //}
        }

        private void Button_Click(object sender, EventArgs e)
        {
            PoisonButton button = (PoisonButton)sender;
            if (!button.Enabled)
            {
                return;
            }

            _result = (DialogResult)button.Tag;
            Hide();
        }
    }

    #endregion
}