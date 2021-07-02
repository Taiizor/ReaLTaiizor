#region Imports

using ReaLTaiizor.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Child.Crown
{
    #region CrownScrollBaseChild

    public abstract class CrownScrollBase : Control
    {
        #region Event Region

        public event EventHandler ViewportChanged;
        public event EventHandler ContentSizeChanged;

        #endregion

        #region Field Region

        protected readonly CrownScrollBar _vScrollBar;
        protected readonly CrownScrollBar _hScrollBar;

        private Size _visibleSize;
        private Size _contentSize;

        private Rectangle _viewport;

        private Point _offsetMousePosition;

        private int _maxDragChange = 0;
        private readonly Timer _dragTimer;

        private bool _hideScrollBars = true;

        #endregion

        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle Viewport
        {
            get => _viewport;
            private set
            {
                _viewport = value;

                ViewportChanged?.Invoke(this, null);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size ContentSize
        {
            get => _contentSize;
            set
            {
                _contentSize = value;
                UpdateScrollBars();

                ContentSizeChanged?.Invoke(this, null);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point OffsetMousePosition => _offsetMousePosition;

        [Category("Behavior")]
        [Description("Determines the maximum scroll change when dragging.")]
        [DefaultValue(0)]
        public int MaxDragChange
        {
            get => _maxDragChange;
            set
            {
                _maxDragChange = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDragging { get; private set; }

        [Category("Behavior")]
        [Description("Determines whether scrollbars will remain visible when disabled.")]
        [DefaultValue(true)]
        public bool HideScrollBars
        {
            get => _hideScrollBars;
            set
            {
                _hideScrollBars = value;
                UpdateScrollBars();
            }
        }

        #endregion

        #region Constructor Region

        protected CrownScrollBase()
        {
            SetStyle
            (
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                    true
            );

            _vScrollBar = new CrownScrollBar { ScrollOrientation = Enum.Crown.ScrollOrientation.Vertical };
            _hScrollBar = new CrownScrollBar { ScrollOrientation = Enum.Crown.ScrollOrientation.Horizontal };

            Controls.Add(_vScrollBar);
            Controls.Add(_hScrollBar);

            _vScrollBar.ValueChanged += delegate { UpdateViewport(); };
            _hScrollBar.ValueChanged += delegate { UpdateViewport(); };

            _vScrollBar.MouseDown += delegate { Select(); };
            _hScrollBar.MouseDown += delegate { Select(); };

            _dragTimer = new Timer
            {
                Interval = 1
            };
            _dragTimer.Tick += DragTimer_Tick;
        }

        #endregion

        #region Method Region

        private void UpdateScrollBars()
        {
            if (_vScrollBar.Maximum != ContentSize.Height)
            {
                _vScrollBar.Maximum = ContentSize.Height;
            }

            if (_hScrollBar.Maximum != ContentSize.Width)
            {
                _hScrollBar.Maximum = ContentSize.Width;
            }

            int scrollSize = ThemeProvider.Theme.Sizes.ScrollBarSize;

            _vScrollBar.Location = new(ClientSize.Width - scrollSize, 0);
            _vScrollBar.Size = new(scrollSize, ClientSize.Height);

            _hScrollBar.Location = new(0, ClientSize.Height - scrollSize);
            _hScrollBar.Size = new(ClientSize.Width, scrollSize);

            if (DesignMode)
            {
                return;
            }

            // Do this twice in case changing the visibility of the scrollbars
            // causes the VisibleSize to change in such a way as to require a second scrollbar.
            // Probably a better way to detect that scenario...
            SetVisibleSize();
            SetScrollBarVisibility();
            SetVisibleSize();
            SetScrollBarVisibility();

            if (_vScrollBar.Visible)
            {
                _hScrollBar.Width -= scrollSize;
            }

            if (_hScrollBar.Visible)
            {
                _vScrollBar.Height -= scrollSize;
            }

            _vScrollBar.ViewSize = _visibleSize.Height;
            _hScrollBar.ViewSize = _visibleSize.Width;

            UpdateViewport();
        }

        private void SetScrollBarVisibility()
        {
            _vScrollBar.Enabled = _visibleSize.Height < ContentSize.Height;
            _hScrollBar.Enabled = _visibleSize.Width < ContentSize.Width;

            if (_hideScrollBars)
            {
                _vScrollBar.Visible = _vScrollBar.Enabled;
                _hScrollBar.Visible = _hScrollBar.Enabled;
            }
        }

        private void SetVisibleSize()
        {
            int scrollSize = ThemeProvider.Theme.Sizes.ScrollBarSize;

            _visibleSize = new(ClientSize.Width, ClientSize.Height);

            if (_vScrollBar.Visible)
            {
                _visibleSize.Width -= scrollSize;
            }

            if (_hScrollBar.Visible)
            {
                _visibleSize.Height -= scrollSize;
            }
        }

        private void UpdateViewport()
        {
            int left = 0;
            int top = 0;
            int width = ClientSize.Width;
            int height = ClientSize.Height;

            if (_hScrollBar.Visible)
            {
                left = _hScrollBar.Value;
                height -= _hScrollBar.Height;
            }

            if (_vScrollBar.Visible)
            {
                top = _vScrollBar.Value;
                width -= _vScrollBar.Width;
            }

            Viewport = new(left, top, width, height);

            Point pos = PointToClient(MousePosition);
            _offsetMousePosition = new(pos.X + Viewport.Left, pos.Y + Viewport.Top);

            Invalidate();
        }

        public void ScrollTo(Point point)
        {
            HScrollTo(point.X);
            VScrollTo(point.Y);
        }

        public void VScrollTo(int value)
        {
            if (_vScrollBar.Visible)
            {
                _vScrollBar.Value = value;
            }
        }

        public void HScrollTo(int value)
        {
            if (_hScrollBar.Visible)
            {
                _hScrollBar.Value = value;
            }
        }

        protected virtual void StartDrag()
        {
            IsDragging = true;
            _dragTimer.Start();
        }

        protected virtual void StopDrag()
        {
            IsDragging = false;
            _dragTimer.Stop();
        }

        public Point PointToView(Point point)
        {
            return new Point(point.X - Viewport.Left, point.Y - Viewport.Top);
        }

        public Rectangle RectangleToView(Rectangle rect)
        {
            return new Rectangle(new Point(rect.Left - Viewport.Left, rect.Top - Viewport.Top), rect.Size);
        }

        #endregion

        #region Event Handler Region

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            UpdateScrollBars();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateScrollBars();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _offsetMousePosition = new(e.X + Viewport.Left, e.Y + Viewport.Top);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Right)
            {
                Select();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            bool horizontal = false;

            if (_hScrollBar.Visible && ModifierKeys == Keys.Control)
            {
                horizontal = true;
            }

            if (_hScrollBar.Visible && !_vScrollBar.Visible)
            {
                horizontal = true;
            }

            if (!horizontal)
            {
                if (e.Delta > 0)
                {
                    _vScrollBar.ScrollByPhysical(3);
                }
                else if (e.Delta < 0)
                {
                    _vScrollBar.ScrollByPhysical(-3);
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    _hScrollBar.ScrollByPhysical(3);
                }
                else if (e.Delta < 0)
                {
                    _hScrollBar.ScrollByPhysical(-3);
                }
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // Allows arrow keys to trigger OnKeyPress
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void DragTimer_Tick(object sender, EventArgs e)
        {
            Point pos = PointToClient(MousePosition);

            int right = ClientRectangle.Right;
            int bottom = ClientRectangle.Bottom;

            if (_vScrollBar.Visible)
            {
                right = _vScrollBar.Left;
            }

            if (_hScrollBar.Visible)
            {
                bottom = _hScrollBar.Top;
            }

            if (_vScrollBar.Visible)
            {
                // Scroll up
                if (pos.Y < ClientRectangle.Top)
                {
                    int difference = (pos.Y - ClientRectangle.Top) * -1;

                    if (MaxDragChange > 0 && difference > MaxDragChange)
                    {
                        difference = MaxDragChange;
                    }

                    _vScrollBar.Value -= difference;
                }

                // Scroll down
                if (pos.Y > bottom)
                {
                    int difference = pos.Y - bottom;

                    if (MaxDragChange > 0 && difference > MaxDragChange)
                    {
                        difference = MaxDragChange;
                    }

                    _vScrollBar.Value += difference;
                }
            }

            if (_hScrollBar.Visible)
            {
                // Scroll left
                if (pos.X < ClientRectangle.Left)
                {
                    int difference = (pos.X - ClientRectangle.Left) * -1;

                    if (MaxDragChange > 0 && difference > MaxDragChange)
                    {
                        difference = MaxDragChange;
                    }

                    _hScrollBar.Value -= difference;
                }

                // Scroll right
                if (pos.X > right)
                {
                    int difference = pos.X - right;

                    if (MaxDragChange > 0 && difference > MaxDragChange)
                    {
                        difference = MaxDragChange;
                    }

                    _hScrollBar.Value += difference;
                }
            }
        }

        #endregion
    }

    #endregion
}