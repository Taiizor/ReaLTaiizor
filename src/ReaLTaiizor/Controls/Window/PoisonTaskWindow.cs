#region Imports

using ReaLTaiizor.Animate.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using System;
using System.Drawing;
using System.Windows.Forms;

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

            singletonWindow = new PoisonTaskWindow(secToClose, userControl)
            {
                Text = title,
                Resizable = false,
                Movable = true,
                StartPosition = FormStartPosition.Manual
            };

            if (parent is not null and IPoisonForm form)
            {
                singletonWindow.Theme = form.Theme;
                singletonWindow.Style = form.Style;
                singletonWindow.StyleManager = form.StyleManager.Clone(singletonWindow) as PoisonStyleManager;
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
            {
                singletonWindow.CancelTimer = true;
            }
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

        public bool CancelTimer { get; set; } = false;

        private readonly int closeTime = 0;
        private int elapsedTime = 0;
        private int progressWidth = 0;
        private DelayedCall timer;

        public bool StartLocation { get; set; } = true;

        public bool CustomSize { get; set; } = false;

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
            {
                timer = DelayedCall.Start(UpdateProgress, 5);
            }
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

                if (!CustomSize)
                {
                    Size = new(400, 200);
                }

                if (!StartLocation)
                {
                    TaskBar myTaskbar = new();
                    Location = myTaskbar.Position switch
                    {
                        TaskBarPosition.Left => new Point(myTaskbar.Bounds.Width + 5, myTaskbar.Bounds.Height - Height - 5),
                        TaskBarPosition.Top => new Point(myTaskbar.Bounds.Width - Width - 5, myTaskbar.Bounds.Height + 5),
                        TaskBarPosition.Right => new Point(myTaskbar.Bounds.X - Width - 5, myTaskbar.Bounds.Height - Height - 5),
                        TaskBarPosition.Bottom => new Point(myTaskbar.Bounds.Width - Width - 5, myTaskbar.Bounds.Y - Height - 5),
                        _ => new Point(Screen.PrimaryScreen.Bounds.Width - Width - 5, Screen.PrimaryScreen.Bounds.Height - Height - 5),
                    };
                }

                controlContainer.Location = new(0, 60);
                controlContainer.Size = new(Width - 40, Height - 80);
                controlContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;

                controlContainer.AutoScroll = false;
                controlContainer.HorizontalScrollbar = false;
                controlContainer.VerticalScrollbar = false;
                controlContainer.Refresh();

                if (StyleManager != null)
                {
                    StyleManager.Update();
                }

                isInitialized = true;

                MoveAnimation myMoveAnim = new();
                myMoveAnim.Start(controlContainer, new Point(20, 60), TransitionType.EaseInOutCubic, 15);
            }

            base.OnActivated(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using SolidBrush b = new(PoisonPaint.BackColor.Form(Theme));
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

            if (CancelTimer)
            {
                elapsedTime = 0;
            }

            double perc = elapsedTime / ((double)closeTime / 100);
            progressWidth = (int)(Width * (perc / 100));
            Invalidate(new Rectangle(0, 0, Width, 5));

            if (!CancelTimer)
            {
                timer.Reset();
            }
        }
    }

    #endregion
}