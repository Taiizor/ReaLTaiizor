#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Native;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Animate.Poison;
using ReaLTaiizor.Interface.Poison;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonTaskWindow

    public sealed class PoisonTaskWindow : PoisonForm
    {
        private static PoisonTaskWindow singletonWindow;
        public static void ShowTaskWindow(IWin32Window parent, string title, Control userControl, int secToClose)
        {
            if (singletonWindow != null)
            {
                singletonWindow.Close();
                singletonWindow.Dispose();
                singletonWindow = null;
            }

            singletonWindow = new PoisonTaskWindow(secToClose, userControl);
            singletonWindow.Text = title;
            singletonWindow.Resizable = false;
            singletonWindow.Movable = true;
            singletonWindow.StartPosition = FormStartPosition.Manual;

            if (parent != null && parent is IPoisonForm)
            {
                singletonWindow.Theme = ((IPoisonForm)parent).Theme;
                singletonWindow.Style = ((IPoisonForm)parent).Style;
                singletonWindow.StyleManager = ((IPoisonForm)parent).StyleManager.Clone(singletonWindow) as PoisonStyleManager;
            }

            singletonWindow.Show();
        }

        public static bool IsVisible()
        {
            return singletonWindow != null && singletonWindow.Visible;
        }

        public static void ShowTaskWindow(IWin32Window parent, string text, Control userControl)
        {
            ShowTaskWindow(parent, text, userControl, 0);
        }

        public static void ShowTaskWindow(string text, Control userControl, int secToClose)
        {
            ShowTaskWindow(null, text, userControl, secToClose);
        }

        public static void ShowTaskWindow(string text, Control userControl)
        {
            ShowTaskWindow(null, text, userControl);
        }

        public static void CancelAutoClose()
        {
            if (singletonWindow != null)
                singletonWindow.CancelTimer = true;
        }

        public static void ForceClose()
        {
            if (singletonWindow != null)
            {
                CancelAutoClose();
                singletonWindow.Close();
                singletonWindow.Dispose();
                singletonWindow = null;
            }
        }

        private bool cancelTimer = false;
        public bool CancelTimer
        {
            get { return cancelTimer; }
            set { cancelTimer = value; }
        }

        private readonly int closeTime = 0;
        private int elapsedTime = 0;
        private int progressWidth = 0;
        private DelayedCall timer;

        private readonly PoisonPanel controlContainer;

        public PoisonTaskWindow()
        {
            controlContainer = new PoisonPanel();
            Controls.Add(controlContainer);
        }

        public PoisonTaskWindow(int duration, Control userControl) : this()
        {
            controlContainer.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
            closeTime = duration * 500;

            if (closeTime > 0)
                timer = DelayedCall.Start(UpdateProgress, 5);
        }


        private bool isInitialized = false;
        protected override void OnActivated(EventArgs e)
        {
            if (!isInitialized)
            {
                controlContainer.Theme = Theme;
                controlContainer.Style = Style;
                controlContainer.StyleManager = StyleManager;

                MaximizeBox = false;
                MinimizeBox = false;
                Movable = true;

                TopMost = true;

                Size = new Size(400, 200);

                TaskBar myTaskbar = new TaskBar();
                switch (myTaskbar.Position)
                {
                    case TaskBarPosition.Left:
                        Location = new Point(myTaskbar.Bounds.Width + 5, myTaskbar.Bounds.Height - Height - 5);
                        break;
                    case TaskBarPosition.Top:
                        Location = new Point(myTaskbar.Bounds.Width - Width - 5, myTaskbar.Bounds.Height + 5);
                        break;
                    case TaskBarPosition.Right:
                        Location = new Point(myTaskbar.Bounds.X - Width - 5, myTaskbar.Bounds.Height - Height - 5);
                        break;
                    case TaskBarPosition.Bottom:
                        Location = new Point(myTaskbar.Bounds.Width - Width - 5, myTaskbar.Bounds.Y - Height - 5);
                        break;
                    case TaskBarPosition.Unknown:
                    default:
                        Location = new Point(Screen.PrimaryScreen.Bounds.Width - Width - 5, Screen.PrimaryScreen.Bounds.Height - Height - 5);
                        break;
                }

                controlContainer.Location = new Point(0, 60);
                controlContainer.Size = new Size(Width - 40, Height - 80);
                controlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;

                controlContainer.AutoScroll = false;
                controlContainer.HorizontalScrollbar = false;
                controlContainer.VerticalScrollbar = false;
                controlContainer.Refresh();

                if (StyleManager != null)
                    StyleManager.Update();

                isInitialized = true;

                MoveAnimation myMoveAnim = new MoveAnimation();
                myMoveAnim.Start(controlContainer, new Point(20, 60), TransitionType.EaseInOutCubic, 15);
            }

            base.OnActivated(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (SolidBrush b = new SolidBrush(PoisonPaint.BackColor.Form(Theme)))
                e.Graphics.FillRectangle(b, new Rectangle(Width - progressWidth, 0, progressWidth, 5));
        }

        private void UpdateProgress()
        {
            if (elapsedTime == closeTime)
            {
                timer.Dispose();
                timer = null;
                Close();
                return;
            }

            elapsedTime += 5;

            if (cancelTimer)
                elapsedTime = 0;

            double perc = (double)elapsedTime / ((double)closeTime / 100);
            progressWidth = (int)((double)Width * (perc / 100));
            Invalidate(new Rectangle(0, 0, Width, 5));

            if (!cancelTimer)
                timer.Reset();
        }
    }

    #endregion
}