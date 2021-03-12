#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Forms
{
    #region MaterialFlexibleForm

    public class MaterialFlexibleForm : MaterialForm, MaterialControlI
    {
        private readonly MaterialManager MaterialManager;

        public static Font FONT;

        public static double MAX_WIDTH_FACTOR = 0.7;

        public static double MAX_HEIGHT_FACTOR = 0.9;

        private MaterialRichTextBox richTextBoxMessage;

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            leftButton = new MaterialButton();
            MaterialFlexibleFormBindingSource = new BindingSource(components);
            messageContainer = new System.Windows.Forms.Panel();
            pictureBoxForIcon = new PictureBox();
            richTextBoxMessage = new MaterialRichTextBox();
            middleButton = new MaterialButton();
            rightButton = new MaterialButton();
            ((ISupportInitialize)(MaterialFlexibleFormBindingSource)).BeginInit();
            messageContainer.SuspendLayout();
            ((ISupportInitialize)(pictureBoxForIcon)).BeginInit();
            SuspendLayout();
            //
            // leftButton
            //
            leftButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            leftButton.AutoSize = false;
            leftButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftButton.Depth = 0;
            leftButton.DialogResult = DialogResult.OK;
            leftButton.DrawShadows = true;
            leftButton.HighEmphasis = false;
            leftButton.Icon = null;
            leftButton.Location = new(44, 163);
            leftButton.Margin = new Padding(4, 6, 4, 6);
            leftButton.MinimumSize = new(0, 24);
            leftButton.MouseState = MaterialMouseState.HOVER;
            leftButton.Name = "leftButton";
            leftButton.Size = new(108, 36);
            leftButton.TabIndex = 2;
            leftButton.Text = "OK";
            leftButton.Type = MaterialButton.MaterialButtonType.Text;
            leftButton.UseAccentColor = false;
            leftButton.UseVisualStyleBackColor = true;
            leftButton.Visible = false;
            //
            // messageContainer
            //
            messageContainer.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            messageContainer.BackColor = Color.White;
            messageContainer.Controls.Add(pictureBoxForIcon);
            messageContainer.Controls.Add(richTextBoxMessage);
            messageContainer.Location = new(0, 65);
            messageContainer.Name = "messageContainer";
            messageContainer.Size = new(388, 81);
            messageContainer.TabIndex = 1;
            //
            // pictureBoxForIcon
            //
            pictureBoxForIcon.BackColor = Color.Transparent;
            pictureBoxForIcon.Location = new(15, 19);
            pictureBoxForIcon.Name = "pictureBoxForIcon";
            pictureBoxForIcon.Size = new(32, 32);
            pictureBoxForIcon.TabIndex = 8;
            pictureBoxForIcon.TabStop = false;
            //
            // richTextBoxMessage
            //
            richTextBoxMessage.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            richTextBoxMessage.BackColor = Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
            richTextBoxMessage.BorderStyle = BorderStyle.None;
            richTextBoxMessage.DataBindings.Add(new Binding("Text", MaterialFlexibleFormBindingSource, "MessageText", true, DataSourceUpdateMode.OnPropertyChanged));
            richTextBoxMessage.Depth = 0;
            richTextBoxMessage.Font = new("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            richTextBoxMessage.ForeColor = Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            richTextBoxMessage.Hint = "";
            richTextBoxMessage.Location = new(47, 2);
            richTextBoxMessage.Margin = new Padding(0);
            richTextBoxMessage.MouseState = MaterialMouseState.HOVER;
            richTextBoxMessage.Name = "richTextBoxMessage";
            richTextBoxMessage.ReadOnly = true;
            richTextBoxMessage.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxMessage.Size = new(338, 78);
            richTextBoxMessage.TabIndex = 0;
            richTextBoxMessage.TabStop = false;
            richTextBoxMessage.Text = "<Message>";
            richTextBoxMessage.LinkClicked += new LinkClickedEventHandler(RichTextBoxMessage_LinkClicked);
            //
            // middleButton
            //
            middleButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            middleButton.AutoSize = false;
            middleButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            middleButton.Depth = 0;
            middleButton.DialogResult = DialogResult.OK;
            middleButton.DrawShadows = true;
            middleButton.HighEmphasis = true;
            middleButton.Icon = null;
            middleButton.Location = new(160, 163);
            middleButton.Margin = new Padding(4, 6, 4, 6);
            middleButton.MinimumSize = new(0, 24);
            middleButton.MouseState = MaterialMouseState.HOVER;
            middleButton.Name = "middleButton";
            middleButton.Size = new(102, 36);
            middleButton.TabIndex = 3;
            middleButton.Text = "OK";
            middleButton.Type = MaterialButton.MaterialButtonType.Text;
            middleButton.UseAccentColor = false;
            middleButton.UseVisualStyleBackColor = true;
            middleButton.Visible = false;
            //
            // rightButton
            //
            rightButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            rightButton.AutoSize = false;
            rightButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightButton.Depth = 0;
            rightButton.DialogResult = DialogResult.OK;
            rightButton.DrawShadows = true;
            rightButton.HighEmphasis = true;
            rightButton.Icon = null;
            rightButton.Location = new(270, 163);
            rightButton.Margin = new Padding(4, 6, 4, 6);
            rightButton.MinimumSize = new(0, 24);
            rightButton.MouseState = MaterialMouseState.HOVER;
            rightButton.Name = "rightButton";
            rightButton.Size = new(106, 36);
            rightButton.TabIndex = 0;
            rightButton.Text = "OK";
            rightButton.Type = MaterialButton.MaterialButtonType.Contained;
            rightButton.UseAccentColor = false;
            rightButton.UseVisualStyleBackColor = true;
            rightButton.Visible = false;
            //
            // MaterialFlexibleForm
            //
            BackColor = Color.White;
            ClientSize = new(388, 208);
            Controls.Add(rightButton);
            Controls.Add(middleButton);
            Controls.Add(messageContainer);
            Controls.Add(leftButton);
            DataBindings.Add(new Binding("Text", MaterialFlexibleFormBindingSource, "CaptionText", true));
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new(276, 140);
            Name = "MaterialFlexibleForm";
            ShowIcon = false;
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterParent;
            Text = "<Caption>";
            Load += new EventHandler(MaterialFlexibleForm_Load);
            Shown += new EventHandler(MaterialFlexibleForm_Shown);
            ((ISupportInitialize)(MaterialFlexibleFormBindingSource)).EndInit();
            messageContainer.ResumeLayout(false);
            ((ISupportInitialize)(pictureBoxForIcon)).EndInit();
            ResumeLayout(false);
        }

        private MaterialButton leftButton;

        private BindingSource MaterialFlexibleFormBindingSource;

        private System.Windows.Forms.Panel messageContainer;

        private PictureBox pictureBoxForIcon;

        private MaterialButton middleButton;
        private MaterialButton rightButton;

        //These separators are used for the "copy to clipboard" standard operation, triggered by Ctrl + C (behavior and clipboard format is like in a standard MessageBox)
        private static readonly string STANDARD_MESSAGEBOX_SEPARATOR_LINES = "---------------------------\n";

        private static readonly string STANDARD_MESSAGEBOX_SEPARATOR_SPACES = "   ";

        //These are the possible buttons (in a standard MessageBox)
        private enum ButtonID
        {
            OK = 0,
            CANCEL,
            YES,
            NO,
            ABORT,
            RETRY,
            IGNORE
        };

        //These are the buttons texts for different languages.
        //If you want to add a new language, add it here and in the GetButtonText-Function
        private enum TwoLetterISOLanguageID
        {
            en,
            de,
            es,
            it,
            tr,
            zh
        };

        private static readonly string[] BUTTON_TEXTS_ENGLISH_EN = { "Ok", "Cancel", "Yes", "No", "Abort", "Retry", "Ignore" };

        private static readonly string[] BUTTON_TEXTS_GERMAN_DE = { "Ok", "Abbrechen", "Ja", "Nein", "Abbrechen", "Wiederholen", "Ignorieren" };

        private static readonly string[] BUTTON_TEXTS_SPANISH_ES = { "Aceptar", "Cancelar", "Sí", "No", "Abortar", "Reintentar", "Ignorar" };

        private static readonly string[] BUTTON_TEXTS_ITALIAN_IT = { "Ok", "Annulla", "Sì", "No", "Interrompi", "Riprova", "Ignora" };

        private static readonly string[] BUTTON_TEXTS_TURKISH_TR = { "Tamam", "İptal", "Evet", "Hayır", "Sonlandır", "Yeniden Dene", "Yoksay" };

        private static readonly string[] BUTTON_TEXTS_CHINA_ZH = { "确定", "取消", "是", "否", "终止", "重试", "忽略" };

        private MessageBoxDefaultButton defaultButton;

        private int visibleButtonsCount;

        private readonly TwoLetterISOLanguageID languageID = TwoLetterISOLanguageID.en;

        private MaterialFlexibleForm()
        {
            InitializeComponent();

            //Try to evaluate the language. If this fails, the fallback language English will be used
            _ = System.Enum.TryParse<TwoLetterISOLanguageID>(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, out languageID);

            KeyPreview = true;
            KeyUp += MaterialFlexibleForm_KeyUp;

            MaterialManager = MaterialManager.Instance;
            MaterialManager.AddFormToManage(this);
            FONT = MaterialManager.GetFontByType(MaterialManager.FontType.Body1);
        }

        private static string[] GetStringRows(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            string[] messageRows = message.Split(new char[] { '\n' }, StringSplitOptions.None);
            return messageRows;
        }

        private string GetButtonText(ButtonID buttonID)
        {
            int buttonTextArrayIndex = Convert.ToInt32(buttonID);

            return languageID switch
            {
                TwoLetterISOLanguageID.de => BUTTON_TEXTS_GERMAN_DE[buttonTextArrayIndex],
                TwoLetterISOLanguageID.es => BUTTON_TEXTS_SPANISH_ES[buttonTextArrayIndex],
                TwoLetterISOLanguageID.it => BUTTON_TEXTS_ITALIAN_IT[buttonTextArrayIndex],
                TwoLetterISOLanguageID.tr => BUTTON_TEXTS_TURKISH_TR[buttonTextArrayIndex],
                TwoLetterISOLanguageID.zh => BUTTON_TEXTS_CHINA_ZH[buttonTextArrayIndex],
                _ => BUTTON_TEXTS_ENGLISH_EN[buttonTextArrayIndex],
            };
        }

        private static double GetCorrectedWorkingAreaFactor(double workingAreaFactor)
        {
            const double MIN_FACTOR = 0.2;
            const double MAX_FACTOR = 1.0;

            if (workingAreaFactor < MIN_FACTOR)
            {
                return MIN_FACTOR;
            }

            if (workingAreaFactor > MAX_FACTOR)
            {
                return MAX_FACTOR;
            }

            return workingAreaFactor;
        }

        private static void SetDialogStartPosition(MaterialFlexibleForm MaterialFlexibleForm, IWin32Window owner)
        {
            //If no owner given: Center on current screen
            if (owner == null)
            {
                Screen screen = Screen.FromPoint(Cursor.Position);
                MaterialFlexibleForm.StartPosition = FormStartPosition.Manual;
                MaterialFlexibleForm.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - MaterialFlexibleForm.Width / 2;
                MaterialFlexibleForm.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - MaterialFlexibleForm.Height / 2;
            }
        }

        private static void SetDialogSizes(MaterialFlexibleForm MaterialFlexibleForm, string text, string caption)
        {
            //First set the bounds for the maximum dialog size
            MaterialFlexibleForm.MaximumSize = new(Convert.ToInt32(SystemInformation.WorkingArea.Width * GetCorrectedWorkingAreaFactor(MAX_WIDTH_FACTOR)), Convert.ToInt32(SystemInformation.WorkingArea.Height * GetCorrectedWorkingAreaFactor(MAX_HEIGHT_FACTOR)));

            //Get rows. Exit if there are no rows to render...
            string[] stringRows = GetStringRows(text);
            if (stringRows == null)
            {
                return;
            }

            //Calculate whole text height
            int textHeight = Math.Min(TextRenderer.MeasureText(text, FONT).Height, 600);

            //Calculate width for longest text line
            const int SCROLLBAR_WIDTH_OFFSET = 15;
            int longestTextRowWidth = stringRows.Max(textForRow => TextRenderer.MeasureText(textForRow, FONT).Width);
            int captionWidth = TextRenderer.MeasureText(caption, SystemFonts.CaptionFont).Width;
            int textWidth = Math.Max(longestTextRowWidth + SCROLLBAR_WIDTH_OFFSET, captionWidth);

            //Calculate margins
            int marginWidth = MaterialFlexibleForm.Width - MaterialFlexibleForm.richTextBoxMessage.Width;
            int marginHeight = MaterialFlexibleForm.Height - MaterialFlexibleForm.richTextBoxMessage.Height;

            //Set calculated dialog size (if the calculated values exceed the maximums, they were cut by windows forms automatically)
            MaterialFlexibleForm.Size = new(textWidth + marginWidth, textHeight + marginHeight);
        }

        private static void SetDialogIcon(MaterialFlexibleForm MaterialFlexibleForm, MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    MaterialFlexibleForm.pictureBoxForIcon.Image = SystemIcons.Information.ToBitmap();
                    break;
                case MessageBoxIcon.Warning:
                    MaterialFlexibleForm.pictureBoxForIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MessageBoxIcon.Error:
                    MaterialFlexibleForm.pictureBoxForIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    MaterialFlexibleForm.pictureBoxForIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                default:
                    //When no icon is used: Correct placement and width of rich text box.
                    MaterialFlexibleForm.pictureBoxForIcon.Visible = false;
                    MaterialFlexibleForm.richTextBoxMessage.Left -= MaterialFlexibleForm.pictureBoxForIcon.Width;
                    MaterialFlexibleForm.richTextBoxMessage.Width += MaterialFlexibleForm.pictureBoxForIcon.Width;
                    break;
            }
        }

        private static void SetDialogButtons(MaterialFlexibleForm MaterialFlexibleForm, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            //Set the buttons visibilities and texts
            switch (buttons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    MaterialFlexibleForm.visibleButtonsCount = 3;

                    MaterialFlexibleForm.leftButton.Visible = true;
                    MaterialFlexibleForm.leftButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.ABORT);
                    MaterialFlexibleForm.leftButton.DialogResult = DialogResult.Abort;

                    MaterialFlexibleForm.middleButton.Visible = true;
                    MaterialFlexibleForm.middleButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.RETRY);
                    MaterialFlexibleForm.middleButton.DialogResult = DialogResult.Retry;

                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.IGNORE);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.Ignore;

                    MaterialFlexibleForm.ControlBox = false;
                    break;
                case MessageBoxButtons.OKCancel:
                    MaterialFlexibleForm.visibleButtonsCount = 2;

                    MaterialFlexibleForm.middleButton.Visible = true;
                    MaterialFlexibleForm.middleButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.CANCEL);
                    MaterialFlexibleForm.middleButton.DialogResult = DialogResult.Cancel;

                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.OK);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.OK;

                    MaterialFlexibleForm.CancelButton = MaterialFlexibleForm.middleButton;
                    break;
                case MessageBoxButtons.RetryCancel:
                    MaterialFlexibleForm.visibleButtonsCount = 2;

                    MaterialFlexibleForm.middleButton.Visible = true;
                    MaterialFlexibleForm.middleButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.CANCEL);
                    MaterialFlexibleForm.middleButton.DialogResult = DialogResult.Cancel;

                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.RETRY);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.Retry;

                    MaterialFlexibleForm.CancelButton = MaterialFlexibleForm.middleButton;
                    break;
                case MessageBoxButtons.YesNo:
                    MaterialFlexibleForm.visibleButtonsCount = 2;

                    MaterialFlexibleForm.middleButton.Visible = true;
                    MaterialFlexibleForm.middleButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.NO);
                    MaterialFlexibleForm.middleButton.DialogResult = DialogResult.No;

                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.YES);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.Yes;

                    MaterialFlexibleForm.ControlBox = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    MaterialFlexibleForm.visibleButtonsCount = 3;

                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.YES);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.Yes;

                    MaterialFlexibleForm.middleButton.Visible = true;
                    MaterialFlexibleForm.middleButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.NO);
                    MaterialFlexibleForm.middleButton.DialogResult = DialogResult.No;

                    MaterialFlexibleForm.leftButton.Visible = true;
                    MaterialFlexibleForm.leftButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.CANCEL);
                    MaterialFlexibleForm.leftButton.DialogResult = DialogResult.Cancel;

                    MaterialFlexibleForm.CancelButton = MaterialFlexibleForm.leftButton;
                    break;
                case MessageBoxButtons.OK:
                default:
                    MaterialFlexibleForm.visibleButtonsCount = 1;
                    MaterialFlexibleForm.rightButton.Visible = true;
                    MaterialFlexibleForm.rightButton.Text = MaterialFlexibleForm.GetButtonText(ButtonID.OK);
                    MaterialFlexibleForm.rightButton.DialogResult = DialogResult.OK;

                    MaterialFlexibleForm.CancelButton = MaterialFlexibleForm.rightButton;
                    break;
            }

            //Set default button (used in MaterialFlexibleForm_Shown)
            MaterialFlexibleForm.defaultButton = defaultButton;
        }

        private void MaterialFlexibleForm_Shown(object sender, EventArgs e)
        {
            System.Windows.Forms.Button buttonToFocus;

            //Set the default button...
            int buttonIndexToFocus = defaultButton switch
            {
                MessageBoxDefaultButton.Button2 => 2,
                MessageBoxDefaultButton.Button3 => 3,
                _ => 1,
            };
            if (buttonIndexToFocus > visibleButtonsCount)
            {
                buttonIndexToFocus = visibleButtonsCount;
            }

            if (buttonIndexToFocus == 3)
            {
                buttonToFocus = rightButton;
            }
            else if (buttonIndexToFocus == 2)
            {
                buttonToFocus = middleButton;
            }
            else
            {
                buttonToFocus = leftButton;
            }

            buttonToFocus.Focus();
        }

        private void RichTextBoxMessage_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Process.Start(e.LinkText);
            }
            catch (Exception)
            {
                //Let the caller of MaterialFlexibleForm decide what to do with this exception...
                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        internal void MaterialFlexibleForm_KeyUp(object sender, KeyEventArgs e)
        {
            //Handle standard key strikes for clipboard copy: "Ctrl + C" and "Ctrl + Insert"
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert))
            {
                string buttonsTextLine = (leftButton.Visible ? leftButton.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                    + (middleButton.Visible ? middleButton.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                    + (rightButton.Visible ? rightButton.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty);

                //Build same clipboard text like the standard .Net MessageBox
                string textForClipboard = STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + Text + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + richTextBoxMessage.Text + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                     + buttonsTextLine.Replace("&", string.Empty) + Environment.NewLine
                                     + STANDARD_MESSAGEBOX_SEPARATOR_LINES;

                //Set text in clipboard
                Clipboard.SetText(textForClipboard);
            }
        }

        public string CaptionText { get; set; }

        public string MessageText { get; set; }

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            //Create a new instance of the FlexibleMessageBox form
            MaterialFlexibleForm MaterialFlexibleForm = new()
            {
                ShowInTaskbar = false,

                //Bind the caption and the message text
                CaptionText = caption,
                MessageText = text
            };
            MaterialFlexibleForm.MaterialFlexibleFormBindingSource.DataSource = MaterialFlexibleForm;

            //Set the buttons visibilities and texts. Also set a default button.
            SetDialogButtons(MaterialFlexibleForm, buttons, defaultButton);

            //Set the dialogs icon. When no icon is used: Correct placement and width of rich text box.
            SetDialogIcon(MaterialFlexibleForm, icon);

            //Set the font for all controls
            MaterialFlexibleForm.Font = FONT;
            MaterialFlexibleForm.richTextBoxMessage.Font = FONT;

            //Calculate the dialogs start size (Try to auto-size width to show longest text row). Also set the maximum dialog size.
            SetDialogSizes(MaterialFlexibleForm, text, caption);

            //Set the dialogs start position when given. Otherwise center the dialog on the current screen.
            SetDialogStartPosition(MaterialFlexibleForm, owner);

            //Show the dialog
            return MaterialFlexibleForm.ShowDialog(owner);
        }

        private void MaterialFlexibleForm_Load(object sender, EventArgs e)
        {
        }
    }

    #endregion
}