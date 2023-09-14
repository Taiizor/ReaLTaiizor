#region Imports

using ReaLTaiizor.Colors;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region RoyalForm

    public class RoyalForm : Form
    {
        private RoyalButton maximizeButton;
        private RoyalButton minimizeButton;
        private RoyalButton closeButton;
        private const int WM_NCHITTEST = 0x0084;
        private const int HTCLIENT = 0x01;
        private const int HTCAPTION = 0x02;
        private const int wmNcHitTest = 0x84;
        private const int htLeft = 10;
        private const int htRight = 11;
        private const int htTop = 12;
        private const int htTopLeft = 13;
        private const int htTopRight = 14;
        private const int htBottom = 15;
        private const int htBottomLeft = 16;
        private const int htBottomRight = 17;

        public bool DrawBorder { get; set; }
        public int BorderThickness { get; set; }
        public bool Moveable { get; set; } = true;
        public bool Sizable { get; set; } = true;

        public RoyalForm()
        {
            InitializeComponent();

            ForeColor = RoyalColors.ForeColor;
            BackColor = RoyalColors.BackColor;

            closeButton.BackColor = BackColor;
            closeButton.HotTrackColor = Color.Crimson;
            closeButton.PressedColor = Color.Firebrick;
            closeButton.Cursor = Cursors.Hand;

            maximizeButton.BackColor = BackColor;
            maximizeButton.HotTrackColor = RoyalColors.HotTrackColor;
            maximizeButton.PressedColor = RoyalColors.PressedBackColor;
            maximizeButton.Cursor = Cursors.Hand;

            minimizeButton.BackColor = BackColor;
            minimizeButton.HotTrackColor = RoyalColors.HotTrackColor;
            minimizeButton.PressedColor = RoyalColors.PressedBackColor;
            minimizeButton.Cursor = Cursors.Hand;

            DrawBorder = false;
            BorderThickness = 1;
            Moveable = true;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new(250, 250);
        }

        protected override void OnResize(EventArgs e)
        {
            closeButton.Location = new(Width - 34, 1);
            maximizeButton.Location = new(Width - 68, 1);
            if (MaximizeBox)
            {
                minimizeButton.Location = new(Width - 102, 1);
            }
            else
            {
                minimizeButton.Location = new(Width - 68, 1);
            }

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DrawBorder)
            {
                e.Graphics.DrawRectangle(new(RoyalColors.BorderColor, BorderThickness), new Rectangle(0, 0, Width - BorderThickness, Height - BorderThickness));
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (Sizable && m.Msg == wmNcHitTest && WindowState != FormWindowState.Maximized)
            {
                int gripDist = 10;
                //int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                //int x = Cursor.Position.X;
                // int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                //Console.WriteLine(x);
                Point pt = PointToClient(Cursor.Position);
                //Console.WriteLine(pt);
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= 2 && clientSize.Height >= 2)
                {
                    m.Result = (IntPtr)htTop;
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htBottom;
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htLeft;
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)htRight;
                    return;
                }
            }

            if (m.Msg == WM_NCHITTEST)
            {
                if (Moveable)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = new IntPtr(HTCAPTION);
                    }
                }
            }

            if (ControlBox == false)
            {
                closeButton.Hide();
                minimizeButton.Hide();
                maximizeButton.Hide();
            }
            else if (Visible && ControlBox == true && !closeButton.Visible)
            {
                closeButton.Show();

                if (MinimizeBox)
                {
                    minimizeButton.Show();
                }

                if (MaximizeBox)
                {
                    maximizeButton.Show();
                }
            }

            if (Visible && !MinimizeBox && minimizeButton.Visible)
            {
                minimizeButton.Hide();
            }
            else if (Visible && MinimizeBox && !minimizeButton.Visible && ControlBox)
            {
                minimizeButton.Show();
            }

            if (Visible && !MaximizeBox && maximizeButton.Visible)
            {
                maximizeButton.Hide();
            }
            else if (Visible && MaximizeBox && !maximizeButton.Visible && ControlBox)
            {
                maximizeButton.Show();
            }
        }

        private void InitializeComponent()
        {
            minimizeButton = new RoyalButton();
            maximizeButton = new RoyalButton();
            closeButton = new RoyalButton();
            SuspendLayout();
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.BackColor = BackColor;
            minimizeButton.BorderColor = Color.FromArgb(180, 180, 180);
            minimizeButton.BorderThickness = 3;
            minimizeButton.DrawBorder = false;
            minimizeButton.HotTrackColor = Color.Gainsboro;
            minimizeButton.Image = Properties.Resources.Minimize;
            minimizeButton.LayoutFlags = RoyalLayoutFlags.ImageOnly;
            minimizeButton.Location = new(900, 1);
            minimizeButton.Margin = new Padding(1);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.PressedColor = Color.CornflowerBlue;
            minimizeButton.PressedForeColor = Color.White;
            minimizeButton.Size = new(33, 30);
            minimizeButton.TabIndex = 2;
            minimizeButton.Text = "minimizeButton";
            minimizeButton.Click += new EventHandler(MinimizeButton_Click);
            // 
            // maximizeButton
            // 
            maximizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            maximizeButton.BackColor = BackColor;
            maximizeButton.BorderColor = Color.FromArgb(180, 180, 180);
            maximizeButton.BorderThickness = 3;
            maximizeButton.DrawBorder = false;
            maximizeButton.HotTrackColor = Color.Gainsboro;
            maximizeButton.Image = Properties.Resources.Maximize;
            maximizeButton.LayoutFlags = RoyalLayoutFlags.ImageOnly;
            maximizeButton.Location = new(935, 1);
            maximizeButton.Margin = new Padding(1);
            maximizeButton.Name = "maximizeButton";
            maximizeButton.PressedColor = Color.CornflowerBlue;
            maximizeButton.PressedForeColor = Color.White;
            maximizeButton.Size = new(33, 30);
            maximizeButton.TabIndex = 1;
            maximizeButton.Text = "maximizeButton";
            maximizeButton.Click += new EventHandler(MaximizeButton_Click);
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BackColor = BackColor;
            closeButton.BorderColor = Color.FromArgb(180, 180, 180);
            closeButton.BorderThickness = 3;
            closeButton.DrawBorder = false;
            closeButton.HotTrackColor = Color.Gainsboro;
            closeButton.Image = Properties.Resources.Close;
            closeButton.LayoutFlags = RoyalLayoutFlags.ImageOnly;
            closeButton.Location = new(970, 1);
            closeButton.Margin = new Padding(1);
            closeButton.Name = "closeButton";
            closeButton.PressedColor = Color.Crimson;
            maximizeButton.PressedForeColor = Color.White;
            closeButton.Size = new(33, 30);
            closeButton.TabIndex = 0;
            closeButton.Text = "closeButton";
            closeButton.Click += new EventHandler(CloseButton_Click);
            // 
            // RoyalForm
            // 
            FormBorderStyle = FormBorderStyle.None;
            BackColor = Color.WhiteSmoke;
            ClientSize = new(512, 512);
            Controls.Add(minimizeButton);
            Controls.Add(maximizeButton);
            Controls.Add(closeButton);
            Name = "RoyalForm";
            Text = "RoyalForm";
            ResumeLayout(false);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }

    #endregion
}