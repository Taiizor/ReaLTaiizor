#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CrownScrollBar

    public class CrownScrollBar : Control
    {
        #region Event Region

        public event EventHandler<ScrollValueEventArgs> ValueChanged;

        #endregion

        #region Field Region

        private Enum.Crown.ScrollOrientation _scrollOrientation;

        private int _value;
        private int _minimum = 0;
        private int _maximum = 100;

        private int _viewSize;

        private Rectangle _trackArea;
        private float _viewContentRatio;

        private Rectangle _thumbArea;
        private Rectangle _upArrowArea;
        private Rectangle _downArrowArea;

        private bool _thumbHot;
        private bool _upArrowHot;
        private bool _downArrowHot;

        private bool _thumbClicked;
        private bool _upArrowClicked;
        private bool _downArrowClicked;

        private bool _isScrolling;
        private int _initialValue;
        private Point _initialContact;

        private readonly Timer _scrollTimer;

        #endregion

        #region Property Region

        [Category("Behavior")]
        [Description("The orientation type of the scrollbar.")]
        [DefaultValue(Enum.Crown.ScrollOrientation.Vertical)]
        public Enum.Crown.ScrollOrientation ScrollOrientation
        {
            get => _scrollOrientation;
            set
            {
                _scrollOrientation = value;
                UpdateScrollBar();
            }
        }

        [Category("Behavior")]
        [Description("The value that the scroll thumb position represents.")]
        [DefaultValue(0)]
        public int Value
        {
            get => _value;
            set
            {
                if (value < Minimum)
                {
                    value = Minimum;
                }

                int maximumValue = Maximum - ViewSize;
                if (value > maximumValue)
                {
                    value = maximumValue;
                }

                if (_value == value)
                {
                    return;
                }

                _value = value;

                UpdateThumb(true);

                ValueChanged?.Invoke(this, new ScrollValueEventArgs(Value));
            }
        }

        [Category("Behavior")]
        [Description("The lower limit value of the scrollable range.")]
        [DefaultValue(0)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                UpdateScrollBar();
            }
        }

        [Category("Behavior")]
        [Description("The upper limit value of the scrollable range.")]
        [DefaultValue(100)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                UpdateScrollBar();
            }
        }

        [Category("Behavior")]
        [Description("The view size for the scrollable area.")]
        [DefaultValue(0)]
        public int ViewSize
        {
            get => _viewSize;
            set
            {
                _viewSize = value;
                UpdateScrollBar();
            }
        }

        public new bool Visible
        {
            get => base.Visible;
            set
            {
                if (base.Visible == value)
                {
                    return;
                }

                base.Visible = value;
            }
        }

        #endregion

        #region Constructor Region

        public CrownScrollBar()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                    true
            );

            SetStyle(ControlStyles.Selectable, false);

            _scrollTimer = new Timer
            {
                Interval = 1
            };
            _scrollTimer.Tick += ScrollTimerTick;
        }

        #endregion

        #region Event Handler Region

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateScrollBar();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_thumbArea.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                _isScrolling = true;
                _initialContact = e.Location;

                if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
                {
                    _initialValue = _thumbArea.Top;
                }
                else
                {
                    _initialValue = _thumbArea.Left;
                }

                Invalidate();
                return;
            }

            if (_upArrowArea.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                _upArrowClicked = true;
                _scrollTimer.Enabled = true;

                Invalidate();
                return;
            }

            if (_downArrowArea.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                _downArrowClicked = true;
                _scrollTimer.Enabled = true;

                Invalidate();
                return;
            }

            if (_trackArea.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                // Step 1. Check if our input is at least aligned with the thumb
                if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
                {
                    Rectangle modRect = new(_thumbArea.Left, _trackArea.Top, _thumbArea.Width, _trackArea.Height);
                    if (!modRect.Contains(e.Location))
                    {
                        return;
                    }
                }
                else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
                {
                    Rectangle modRect = new(_trackArea.Left, _thumbArea.Top, _trackArea.Width, _thumbArea.Height);
                    if (!modRect.Contains(e.Location))
                    {
                        return;
                    }
                }

                // Step 2. Scroll to the area initially clicked.
                if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
                {
                    int loc = e.Location.Y;
                    loc -= _upArrowArea.Bottom - 1;
                    loc -= _thumbArea.Height / 2;
                    ScrollToPhysical(loc);
                }
                else
                {
                    int loc = e.Location.X;
                    loc -= _upArrowArea.Right - 1;
                    loc -= _thumbArea.Width / 2;
                    ScrollToPhysical(loc);
                }

                // Step 3. Initiate a thumb drag.
                _isScrolling = true;
                _initialContact = e.Location;
                _thumbHot = true;

                if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
                {
                    _initialValue = _thumbArea.Top;
                }
                else
                {
                    _initialValue = _thumbArea.Left;
                }

                Invalidate();
                return;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _isScrolling = false;

            _thumbClicked = false;
            _upArrowClicked = false;
            _downArrowClicked = false;

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_isScrolling)
            {
                bool thumbHot = _thumbArea.Contains(e.Location);
                if (_thumbHot != thumbHot)
                {
                    _thumbHot = thumbHot;
                    Invalidate();
                }

                bool upArrowHot = _upArrowArea.Contains(e.Location);
                if (_upArrowHot != upArrowHot)
                {
                    _upArrowHot = upArrowHot;
                    Invalidate();
                }

                bool downArrowHot = _downArrowArea.Contains(e.Location);
                if (_downArrowHot != downArrowHot)
                {
                    _downArrowHot = downArrowHot;
                    Invalidate();
                }
            }

            if (_isScrolling)
            {
                if (e.Button != MouseButtons.Left)
                {
                    OnMouseUp(null);
                    return;
                }

                Point difference = new(e.Location.X - _initialContact.X, e.Location.Y - _initialContact.Y);

                if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
                {
                    int thumbPos = _initialValue - _trackArea.Top;
                    int newPosition = thumbPos + difference.Y;

                    ScrollToPhysical(newPosition);
                }
                else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
                {
                    int thumbPos = _initialValue - _trackArea.Left;
                    int newPosition = thumbPos + difference.X;

                    ScrollToPhysical(newPosition);
                }

                UpdateScrollBar();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _thumbHot = false;
            _upArrowHot = false;
            _downArrowHot = false;

            Invalidate();
        }

        private void ScrollTimerTick(object sender, EventArgs e)
        {
            if (!_upArrowClicked && !_downArrowClicked)
            {
                _scrollTimer.Enabled = false;
                return;
            }

            if (_upArrowClicked)
            {
                ScrollBy(-1);
            }
            else if (_downArrowClicked)
            {
                ScrollBy(1);
            }
        }

        #endregion

        #region Method Region

        public void ScrollTo(int position)
        {
            Value = position;
        }

        public void ScrollToPhysical(int positionInPixels)
        {
            bool isVert = _scrollOrientation == Enum.Crown.ScrollOrientation.Vertical;

            int trackAreaSize = isVert ? _trackArea.Height - _thumbArea.Height : _trackArea.Width - _thumbArea.Width;

            float positionRatio = positionInPixels / (float)trackAreaSize;
            int viewScrollSize = Maximum - ViewSize;

            int newValue = (int)(positionRatio * viewScrollSize);
            Value = newValue;
        }

        public void ScrollBy(int offset)
        {
            int newValue = Value + offset;
            ScrollTo(newValue);
        }

        public void ScrollByPhysical(int offsetInPixels)
        {
            bool isVert = _scrollOrientation == Enum.Crown.ScrollOrientation.Vertical;

            int thumbPos = isVert ? (_thumbArea.Top - _trackArea.Top) : (_thumbArea.Left - _trackArea.Left);

            int newPosition = thumbPos - offsetInPixels;

            ScrollToPhysical(newPosition);
        }

        public void UpdateScrollBar()
        {
            Rectangle area = ClientRectangle;

            // Arrow buttons
            if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
            {
                _upArrowArea = new(area.Left, area.Top, ThemeProvider.Theme.Sizes.ArrowButtonSize, ThemeProvider.Theme.Sizes.ArrowButtonSize);
                _downArrowArea = new(area.Left, area.Bottom - ThemeProvider.Theme.Sizes.ArrowButtonSize, ThemeProvider.Theme.Sizes.ArrowButtonSize, ThemeProvider.Theme.Sizes.ArrowButtonSize);
            }
            else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
            {
                _upArrowArea = new(area.Left, area.Top, ThemeProvider.Theme.Sizes.ArrowButtonSize, ThemeProvider.Theme.Sizes.ArrowButtonSize);
                _downArrowArea = new(area.Right - ThemeProvider.Theme.Sizes.ArrowButtonSize, area.Top, ThemeProvider.Theme.Sizes.ArrowButtonSize, ThemeProvider.Theme.Sizes.ArrowButtonSize);
            }

            // Track
            if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
            {
                _trackArea = new(area.Left, area.Top + ThemeProvider.Theme.Sizes.ArrowButtonSize, area.Width, area.Height - (ThemeProvider.Theme.Sizes.ArrowButtonSize * 2));
            }
            else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
            {
                _trackArea = new(area.Left + ThemeProvider.Theme.Sizes.ArrowButtonSize, area.Top, area.Width - (ThemeProvider.Theme.Sizes.ArrowButtonSize * 2), area.Height);
            }

            // Thumb
            UpdateThumb();

            Invalidate();
        }

        private void UpdateThumb(bool forceRefresh = false)
        {
            if (ViewSize >= Maximum)
            {
                return;
            }

            // Cap to maximum value
            int maximumValue = Maximum - ViewSize;
            if (Value > maximumValue)
            {
                Value = maximumValue;
            }

            // Calculate size ratio
            _viewContentRatio = ViewSize / (float)Maximum;
            int viewAreaSize = Maximum - ViewSize;
            float positionRatio = Value / (float)viewAreaSize;

            // Update area
            if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
            {
                int thumbSize = (int)(_trackArea.Height * _viewContentRatio);

                if (thumbSize < ThemeProvider.Theme.Sizes.MinimumThumbSize)
                {
                    thumbSize = ThemeProvider.Theme.Sizes.MinimumThumbSize;
                }

                int trackAreaSize = _trackArea.Height - thumbSize;
                int thumbPosition = (int)(trackAreaSize * positionRatio);

                _thumbArea = new(_trackArea.Left + 3, _trackArea.Top + thumbPosition, ThemeProvider.Theme.Sizes.ScrollBarSize - 6, thumbSize);
            }
            else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
            {
                int thumbSize = (int)(_trackArea.Width * _viewContentRatio);

                if (thumbSize < ThemeProvider.Theme.Sizes.MinimumThumbSize)
                {
                    thumbSize = ThemeProvider.Theme.Sizes.MinimumThumbSize;
                }

                int trackAreaSize = _trackArea.Width - thumbSize;
                int thumbPosition = (int)(trackAreaSize * positionRatio);

                _thumbArea = new(_trackArea.Left + thumbPosition, _trackArea.Top + 3, thumbSize, ThemeProvider.Theme.Sizes.ScrollBarSize - 6);
            }

            if (forceRefresh)
            {
                Invalidate();
                Update();
            }
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // DEBUG: Scrollbar bg
            using (SolidBrush b = new(ThemeProvider.Theme.Colors.GreyBackground))
            {
                g.FillRectangle(b, ClientRectangle);
            }/**/

            // DEBUG: Arrow backgrounds
            /*using (var b = new(Color.White))
            {
                g.FillRectangle(b, _upArrowArea);
                g.FillRectangle(b, _downArrowArea);
            }*/

            // Up arrow
            Bitmap upIcon = _upArrowHot ? Properties.Resources.scrollbar_arrow_hot : Properties.Resources.scrollbar_arrow_standard;

            if (_upArrowClicked)
            {
                upIcon = Properties.Resources.scrollbar_arrow_clicked;
            }

            if (!Enabled)
            {
                upIcon = Properties.Resources.scrollbar_arrow_disabled;
            }

            if (_scrollOrientation == Enum.Crown.ScrollOrientation.Vertical)
            {
                upIcon.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            else if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
            {
                upIcon.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            g.DrawImageUnscaled(upIcon, _upArrowArea.Left + (_upArrowArea.Width / 2) - (upIcon.Width / 2), _upArrowArea.Top + (_upArrowArea.Height / 2) - (upIcon.Height / 2));

            // Down arrow
            Bitmap downIcon = _downArrowHot ? Properties.Resources.scrollbar_arrow_hot : Properties.Resources.scrollbar_arrow_standard;

            if (_downArrowClicked)
            {
                downIcon = Properties.Resources.scrollbar_arrow_clicked;
            }

            if (!Enabled)
            {
                downIcon = Properties.Resources.scrollbar_arrow_disabled;
            }

            if (_scrollOrientation == Enum.Crown.ScrollOrientation.Horizontal)
            {
                downIcon.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }

            g.DrawImageUnscaled(downIcon, _downArrowArea.Left + (_downArrowArea.Width / 2) - (downIcon.Width / 2), _downArrowArea.Top + (_downArrowArea.Height / 2) - (downIcon.Height / 2));

            // Draw thumb
            if (Enabled)
            {
                Color scrollColor = _thumbHot ? ThemeProvider.Theme.Colors.GreyHighlight : ThemeProvider.Theme.Colors.GreySelection;

                if (_isScrolling)
                {
                    scrollColor = ThemeProvider.Theme.Colors.ActiveControl;
                }

                using SolidBrush b = new(scrollColor);
                g.FillRectangle(b, _thumbArea);
            }
        }

        #endregion
    }

    #endregion
}