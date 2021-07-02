#region Imports

using ReaLTaiizor.Controls;
using ReaLTaiizor.Enum.Crown;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Child.Crown
{
    #region CrownDialogChild

    public partial class CrownDialog : CrownForm
    {
        #region Field Region

        private DialogButton _dialogButtons = DialogButton.Ok;
        private readonly List<CrownButton> _buttons;

        #endregion

        #region Button Region

        protected CrownButton btnOk;
        protected CrownButton btnCancel;
        protected CrownButton btnClose;
        protected CrownButton btnYes;
        protected CrownButton btnNo;
        protected CrownButton btnAbort;
        protected CrownButton btnRetry;
        protected CrownButton btnIgnore;

        #endregion

        #region Property Region

        [Description("Determines the type of the dialog window.")]
        [DefaultValue(DialogButton.Ok)]
        public DialogButton DialogButtons
        {
            get => _dialogButtons;
            set
            {
                if (_dialogButtons == value)
                {
                    return;
                }

                _dialogButtons = value;
                SetButtons();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalButtonSize { get; private set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IButtonControl AcceptButton
        {
            get => base.AcceptButton;
            private set => base.AcceptButton = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new IButtonControl CancelButton
        {
            get => base.CancelButton;
            private set => base.CancelButton = value;
        }

        #endregion

        #region Constructor Region

        public CrownDialog()
        {
            InitializeComponent();

            _buttons = new List<CrownButton>
            {
                btnAbort, btnRetry, btnIgnore, btnOk,
                btnCancel, btnClose, btnYes, btnNo
            };
        }

        #endregion

        #region Event Handler Region

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetButtons();
        }

        #endregion

        #region Method Region

        private void SetButtons()
        {
            foreach (CrownButton btn in _buttons)
            {
                btn.Visible = false;
            }

            switch (_dialogButtons)
            {
                case DialogButton.Ok:
                    ShowButton(btnOk, true);
                    AcceptButton = btnOk;
                    break;
                case DialogButton.Close:
                    ShowButton(btnClose, true);
                    AcceptButton = btnClose;
                    CancelButton = btnClose;
                    break;
                case DialogButton.OkCancel:
                    ShowButton(btnOk);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnOk;
                    CancelButton = btnCancel;
                    break;
                case DialogButton.AbortRetryIgnore:
                    ShowButton(btnAbort);
                    ShowButton(btnRetry);
                    ShowButton(btnIgnore, true);
                    AcceptButton = btnAbort;
                    CancelButton = btnIgnore;
                    break;
                case DialogButton.RetryCancel:
                    ShowButton(btnRetry);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnRetry;
                    CancelButton = btnCancel;
                    break;
                case DialogButton.YesNo:
                    ShowButton(btnYes);
                    ShowButton(btnNo, true);
                    AcceptButton = btnYes;
                    CancelButton = btnNo;
                    break;
                case DialogButton.YesNoCancel:
                    ShowButton(btnYes);
                    ShowButton(btnNo);
                    ShowButton(btnCancel, true);
                    AcceptButton = btnYes;
                    CancelButton = btnCancel;
                    break;
            }

            SetFlowSize();
        }

        private static void ShowButton(CrownButton button, bool isLast = false)
        {
            button.SendToBack();

            if (!isLast)
            {
                button.Margin = new Padding(0, 0, 10, 0);
            }

            button.Visible = true;
        }

        private void SetFlowSize()
        {
            int width = flowInner.Padding.Horizontal;

            foreach (CrownButton btn in _buttons)
            {
                if (btn.Visible)
                {
                    width += btn.Width + btn.Margin.Right;
                }
            }

            flowInner.Width = width;
            TotalButtonSize = width;
        }

        #endregion
    }

    #endregion
}