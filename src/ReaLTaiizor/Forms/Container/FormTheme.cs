#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region FormTheme

    public class FormTheme : ContainerControl
    {
        #region Variables

        private Point MouseP = new(0, 0);
        private bool Cap = false;
        private readonly int MoveHeight;
        private readonly string _TextBottom = null;
        private const int BorderCurve = 7;
        protected MouseState State;
        private bool HasShown;
        private Rectangle HeaderRect;

        #endregion

        #region Enums

        public enum MouseState
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion

        #region Properties

        public bool Sizable { get; set; } = true;
        public bool SmartBounds { get; set; } = false;
        protected bool IsParentForm { get; private set; }

        protected bool IsParentMdi
        {
            get
            {
                if (Parent == null)
                {
                    return false;
                }

                return Parent.Parent != null;
            }
        }

        private bool _ControlMode;
        protected bool ControlMode
        {
            get => _ControlMode;
            set
            {
                _ControlMode = value;
                Invalidate();
            }
        }

        private FormStartPosition _StartPosition;
        public FormStartPosition StartPosition
        {
            get
            {
                if (IsParentForm && !_ControlMode)
                {
                    return ParentForm.StartPosition;
                }
                else
                {
                    return _StartPosition;
                }
            }
            set
            {
                _StartPosition = value;

                if (IsParentForm && !_ControlMode)
                {
                    ParentForm.StartPosition = value;
                }
            }
        }

        #endregion

        #region EventArgs

        protected sealed override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null)
            {
                return;
            }

            IsParentForm = Parent is System.Windows.Forms.Form;

            if (!_ControlMode)
            {
                InitializeMessages();

                if (IsParentForm)
                {
                    ParentForm.FormBorderStyle = FormBorderStyle.None;
                    ParentForm.TransparencyKey = Color.Fuchsia;

                    if (!DesignMode)
                    {
                        ParentForm.Shown += FormShown;
                    }
                }
                Parent.BackColor = BackColor;
                Parent.MinimumSize = new(126, 50);
            }
        }

        protected sealed override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_ControlMode)
            {
                HeaderRect = new(0, 0, Width - 14, MoveHeight - 7);
            }

            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                SetState(MouseState.Down);
            }

            if (!((IsParentForm && ParentForm.WindowState == FormWindowState.Maximized) || _ControlMode))
            {
                if (HeaderRect.Contains(e.Location))
                {
                    Capture = false;
                    WM_LMBUTTONDOWN = true;
                    DefWndProc(ref Messages[0]);
                }
                else if (Sizable && !(Previous == 0))
                {
                    Capture = false;
                    WM_LMBUTTONDOWN = true;
                    DefWndProc(ref Messages[Previous]);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cap = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (Sizable && !_ControlMode)
                {
                    InvalidateMouse();
                }
            }
            if (Cap)
            {
                Parent.Location = (Point)(object)(Convert.ToDouble(MousePosition) - Convert.ToDouble(MouseP));
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            ParentForm.Text = Text;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        private void FormShown(object sender, EventArgs e)
        {
            if (_ControlMode || HasShown)
            {
                return;
            }

            if (_StartPosition is FormStartPosition.CenterParent or FormStartPosition.CenterScreen)
            {
                Rectangle SB = Screen.PrimaryScreen.Bounds;
                Rectangle CB = ParentForm.Bounds;
                ParentForm.Location = new((SB.Width / 2) - (CB.Width / 2), (SB.Height / 2) - (CB.Height / 2));
            }
            HasShown = true;
        }

        #endregion

        #region Mouse & Size

        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        private Point GetIndexPoint;
        private bool B1x;
        private bool B2x;
        private bool B3;
        private bool B4;
        private int GetIndex()
        {
            GetIndexPoint = PointToClient(MousePosition);
            B1x = GetIndexPoint.X < 7;
            B2x = GetIndexPoint.X > Width - 7;
            B3 = GetIndexPoint.Y < 7;
            B4 = GetIndexPoint.Y > Height - 7;

            if (B1x && B3)
            {
                return 4;
            }

            if (B1x && B4)
            {
                return 7;
            }

            if (B2x && B3)
            {
                return 5;
            }

            if (B2x && B4)
            {
                return 8;
            }

            if (B1x)
            {
                return 1;
            }

            if (B2x)
            {
                return 2;
            }

            if (B3)
            {
                return 3;
            }

            if (B4)
            {
                return 6;
            }

            return 0;
        }

        private int Current;
        private int Previous;
        private void InvalidateMouse()
        {
            Current = GetIndex();
            if (Current == Previous)
            {
                return;
            }

            Previous = Current;
            switch (Previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 1:
                    Cursor = Cursors.SizeWE;
                    break;
                case 2:
                    Cursor = Cursors.SizeWE;
                    break;
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private readonly Message[] Messages = new Message[9];
        private void InitializeMessages()
        {
            Messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (int I = 1; I <= 8; I++)
            {
                Messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
            }
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent.Width > bounds.Width)
            {
                Parent.Width = bounds.Width;
            }

            if (Parent.Height > bounds.Height)
            {
                Parent.Height = bounds.Height;
            }

            int X = Parent.Location.X;
            int Y = Parent.Location.Y;

            if (X < bounds.X)
            {
                X = bounds.X;
            }

            if (Y < bounds.Y)
            {
                Y = bounds.Y;
            }

            int Width = bounds.X + bounds.Width;
            int Height = bounds.Y + bounds.Height;

            if (X + Parent.Width > Width)
            {
                X = Width - Parent.Width;
            }

            if (Y + Parent.Height > Height)
            {
                Y = Height - Parent.Height;
            }

            Parent.Location = new(X, Y);
        }

        private bool WM_LMBUTTONDOWN;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (WM_LMBUTTONDOWN && m.Msg == 513)
            {
                WM_LMBUTTONDOWN = false;

                SetState(MouseState.Over);
                if (!SmartBounds)
                {
                    return;
                }

                if (IsParentMdi)
                {
                    CorrectBounds(new Rectangle(Point.Empty, Parent.Parent.Size));
                }
                else
                {
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea);
                }
            }
        }

        #endregion

        //protected override void OnCreateControl()
        //{
        //    base.OnCreateControl();
        //    ParentForm.FormBorderStyle = FormBorderStyle.None;
        //    ParentForm.TransparencyKey = Color.Fuchsia;
        //}

        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        public FormTheme()
        {
            MoveHeight = 25;
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            Padding = new Padding(3, 28, 3, 28);
            SetStyle((ControlStyles)139270, true);
            ForeColor = Color.FromArgb(142, 142, 142);
            BackColor = Color.FromArgb(32, 41, 50);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new("Segoe UI", 8, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle ClientRectangle = new(0, 0, Width - 1, Height - 1);
            Color TransparencyKey = ParentForm.TransparencyKey;

            G.SmoothingMode = SmoothingMode.Default;
            G.Clear(TransparencyKey);

            // Draw the container borders
            G.FillPath(new SolidBrush(Color.FromArgb(52, 52, 52)), RoundRectangle.RoundRect(ClientRectangle, BorderCurve));
            // Draw a rectangle in which the controls should be added on
            G.FillPath(new SolidBrush(Color.FromArgb(32, 41, 50)), RoundRectangle.RoundRect(new Rectangle(2, 20, Width - 5, Height - 42), BorderCurve));

            // Patch the header with a rectangle that has a curve so its border will remain within container bounds
            G.FillPath(new SolidBrush(Color.FromArgb(52, 52, 52)), RoundRectangle.RoundRect(new Rectangle(2, 2, (Width / 2) + 2, 16), BorderCurve));
            G.FillPath(new SolidBrush(Color.FromArgb(52, 52, 52)), RoundRectangle.RoundRect(new Rectangle((Width / 2) - 3, 2, Width / 2, 16), BorderCurve));
            // Fill the header rectangle below the patch
            G.FillRectangle(new SolidBrush(Color.FromArgb(52, 52, 52)), new Rectangle(2, 15, Width - 5, 10));

            // Increase the thickness of the container borders
            G.DrawPath(new(Color.FromArgb(52, 52, 52)), RoundRectangle.RoundRect(new Rectangle(2, 2, Width - 5, Height - 5), BorderCurve));
            G.DrawPath(new(Color.FromArgb(52, 52, 52)), RoundRectangle.RoundRect(ClientRectangle, BorderCurve));

            // Draw the string from the specified 'Text' property on the header rectangle
            G.DrawString(Text, new Font("Trebuchet MS", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(221, 221, 221)), new Rectangle(BorderCurve, BorderCurve - 4, Width - 1, 22), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });

            // Draws a rectangle at the bottom of the container
            G.FillRectangle(new SolidBrush(Color.FromArgb(52, 52, 52)), 0, Height - 25, Width - 3, 22 - 2);
            G.DrawLine(new(Color.FromArgb(52, 52, 52)), 5, Height - 5, Width - 6, Height - 5);
            G.DrawLine(new(Color.FromArgb(52, 52, 52)), 7, Height - 4, Width - 7, Height - 4);

            G.DrawString(_TextBottom, new Font("Trebuchet MS", 10, FontStyle.Bold), new SolidBrush(Color.FromArgb(221, 221, 221)), 5, Height - 23);

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}