#region Imports

using ReaLTaiizor.Child.Poison;
using System;
using System.Media;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonMessageBox

    public static class PoisonMessageBox
    {
        public static DialogResult Show(IWin32Window owner, string message)
        {
            return Show(owner, message, "Notification", 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, int height)
        {
            return Show(owner, message, "Notification", height);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title)
        {
            return Show(owner, message, title, MessageBoxButtons.OK, 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, int height)
        {
            return Show(owner, message, title, MessageBoxButtons.OK, height);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons)
        {
            return Show(owner, message, title, buttons, MessageBoxIcon.None, 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, int height)
        {
            return Show(owner, message, title, buttons, MessageBoxIcon.None, height);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, int height)
        {
            return Show(owner, message, title, buttons, icon, MessageBoxDefaultButton.Button1, height);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton)
        {
            return Show(owner, message, title, buttons, icon, defaultbutton, 211);
        }

        public static DialogResult Show(IWin32Window owner, string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultbutton, int height)
        {
            DialogResult _result = DialogResult.None;

            if (owner != null)
            {
                Form _owner = ((owner as Form) == null) ? ((UserControl)owner).ParentForm : (Form)owner;

                //int _minWidth = 500;
                //int _minHeight = 350;

                //if (_owner.Size.Width < _minWidth ||
                //    _owner.Size.Height < _minHeight)
                //{
                //    if (_owner.Size.Width < _minWidth && _owner.Size.Height < _minHeight) {
                //            _owner.Size = new(_minWidth, _minHeight);
                //    }
                //    else
                //    {
                //        if (_owner.Size.Width < _minWidth) _owner.Size = new(_minWidth, _owner.Size.Height);
                //        else _owner.Size = new(_owner.Size.Width, _minHeight);
                //    }

                //    int x = Convert.ToInt32(Math.Ceiling((decimal)(Screen.PrimaryScreen.WorkingArea.Size.Width / 2) - (_owner.Size.Width / 2)));
                //    int y = Convert.ToInt32(Math.Ceiling((decimal)(Screen.PrimaryScreen.WorkingArea.Size.Height / 2) - (_owner.Size.Height / 2)));
                //    _owner.Location = new(x, y);
                //}

                switch (icon)
                {
                    case MessageBoxIcon.Error:
                        SystemSounds.Hand.Play(); break;
                    case MessageBoxIcon.Exclamation:
                        SystemSounds.Exclamation.Play(); break;
                    case MessageBoxIcon.Question:
                        SystemSounds.Beep.Play(); break;
                    default:
                        SystemSounds.Asterisk.Play(); break;
                }

                PoisonMessageBoxControl _control = new()
                {
                    BackColor = _owner.BackColor
                };

                _control.Properties.Buttons = buttons;
                _control.Properties.DefaultButton = defaultbutton;
                _control.Properties.Icon = icon;
                _control.Properties.Message = message;
                _control.Properties.Title = title;
                _control.Padding = new Padding(0, 0, 0, 0);
                _control.ControlBox = false;
                _control.ShowInTaskbar = false;
                _control.TopMost = true;

                //_owner.Controls.Add(_control);
                //if (_owner is IPoisonForm)
                //{
                //    //if (((PoisonForm)_owner).DisplayHeader)
                //    //{
                //    //    _offset += 30;
                //    //}
                //    _control.Theme = ((PoisonForm)_owner).Theme;
                //    _control.Style = ((PoisonForm)_owner).Style;
                //}

                _control.Size = new(_owner.Size.Width, height);
                _control.Location = new(_owner.Location.X, _owner.Location.Y + ((_owner.Height - _control.Height) / 2));
                _control.ArrangeApperance();
                int _overlaySizes = Convert.ToInt32(Math.Floor(_control.Size.Height * 0.28));
                //_control.OverlayPanelTop.Size = new(_control.Size.Width, _overlaySizes - 30);
                //_control.OverlayPanelBottom.Size = new(_control.Size.Width, _overlaySizes);

                _control.ShowDialog();
                _control.BringToFront();
                _control.SetDefaultButton();

                Action<PoisonMessageBoxControl> _delegate = new(ModalState);

                IAsyncResult _asyncresult = null;

                try
                {
                    _asyncresult = _delegate.BeginInvoke(_control, null, _delegate);
                }
                catch { }

                bool _cancelled = false;

                try
                {
                    while (!_asyncresult.IsCompleted)
                    {
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                }
                catch
                {
                    _cancelled = true;

                    try
                    {
                        if (!_asyncresult.IsCompleted)
                        {
                            try { _asyncresult = null; }
                            catch { }
                        }
                    }
                    catch { _result = _control.Result; }

                    _delegate = null;
                }

                if (!_cancelled)
                {
                    _result = _control.Result;
                    //_owner.Controls.Remove(_control);
                    _control.Dispose(); _control = null;
                }

            }

            return _result;
        }

        private static void ModalState(PoisonMessageBoxControl control)
        {
            /*
            while (control.Visible)
            {

            }
            */
        }

    }

    #endregion
}