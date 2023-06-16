#region Imports

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotFormHandle

    public class ParrotFormHandle : Component
    {
        private Control _HandleControl;

        public const int WM_NCLBUTTONDOWN = 161;
        public const int HT_CAPTION = 2;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The HandleControl")]
        public Control HandleControl
        {
            get => _HandleControl;
            set
            {
                _HandleControl = value;
                _HandleControl.MouseDown += DragForm_MouseDown;
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Maximize when dragged to top")]
        public bool DockAtTop { get; set; } = true;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void DragForm_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(HandleControl.FindForm().Handle, 161, 2, 0);

                    if (DockAtTop && HandleControl.FindForm().FormBorderStyle == FormBorderStyle.None)
                    {
                        if (HandleControl.FindForm().WindowState != FormWindowState.Maximized && Cursor.Position.Y <= 3)
                        {
                            HandleControl.FindForm().WindowState = FormWindowState.Maximized;
                            return;
                        }

                        HandleControl.FindForm().WindowState = FormWindowState.Normal;
                    }
                }
            }
            catch { }
        }
    }

    #endregion
}