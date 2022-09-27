#region Imports

using ReaLTaiizor.Enum.Material;
using ReaLTaiizor.Manager;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialScrollBar

    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    public class MaterialScrollBar : Control, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        private bool useAccentColor;

        [Category("Material"), DefaultValue(false), DisplayName("Use Accent Color")]
        public bool UseAccentColor
        {
            get => useAccentColor;
            set { useAccentColor = value; Invalidate(); }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        internal const int SCROLLBAR_DEFAULT_SIZE = 10;

        #region Events

        public event ScrollEventHandler Scroll;

        private void OnScroll(ScrollEventType type, int oldValue, int newValue, ScrollOrientation orientation)
        {
            if (oldValue != newValue)
            {
                ValueChanged?.Invoke(this, curValue);
            }

            if (Scroll == null)
            {
                return;
            }

            if (orientation == ScrollOrientation.HorizontalScroll)
            {
                if (type != ScrollEventType.EndScroll && isFirstScrollEventHorizontal)
                {
                    type = ScrollEventType.First;
                }
                else if (!isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
                {
                    isFirstScrollEventHorizontal = true;
                }
            }
            else
            {
                if (type != ScrollEventType.EndScroll && isFirstScrollEventVertical)
                {
                    type = ScrollEventType.First;
                }
                else if (!isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
                {
                    isFirstScrollEventVertical = true;
                }
            }

            Scroll(this, new ScrollEventArgs(type, oldValue, newValue, orientation));
        }

        #endregion

        #region Properties

        private bool isFirstScrollEventVertical = true;
        private bool isFirstScrollEventHorizontal = true;

        private bool inUpdate;

        private Rectangle clickedBarRectangle;
        private Rectangle thumbRectangle;

        private bool topBarClicked;
        private bool bottomBarClicked;
        private bool thumbClicked;

        private int thumbWidth = 6;
        private int thumbHeight;

        private int thumbBottomLimitBottom;
        private int thumbBottomLimitTop;
        private int thumbTopLimit;
        private int thumbPosition;

        public const int WM_SETREDRAW = 0xb;

        private int trackPosition;

        private readonly Timer progressTimer = new();

        private int mouseWheelBarPartitions = 10;
        [DefaultValue(10)]
        public int MouseWheelBarPartitions
        {
            get => mouseWheelBarPartitions;
            set
            {
                if (value > 0)
                {
                    mouseWheelBarPartitions = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("value", "MouseWheelBarPartitions has to be greather than zero");
                }
            }
        }

        private bool isHovered;

        [DefaultValue(false)]
        public bool UseBarColor { get; set; } = false;

        [DefaultValue(SCROLLBAR_DEFAULT_SIZE)]
        public int ScrollbarSize
        {
            get => Orientation == MateScrollOrientation.Vertical ? Width : Height;
            set
            {
                if (Orientation == MateScrollOrientation.Vertical)
                {
                    Width = value;
                }
                else
                {
                    Height = value;
                }
            }
        }

        [DefaultValue(false)]
        public bool HighlightOnWheel { get; set; } = false;

        private MateScrollOrientation MaterialOrientation = MateScrollOrientation.Vertical;
        private ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;

        public MateScrollOrientation Orientation
        {
            get => MaterialOrientation;
            set
            {
                if (value == MaterialOrientation)
                {
                    return;
                }

                MaterialOrientation = value;
                scrollOrientation = value == MateScrollOrientation.Vertical ? ScrollOrientation.VerticalScroll : ScrollOrientation.HorizontalScroll;
                Size = new Size(Height, Width);
                SetupScrollBar();
            }
        }

        private int minimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get => minimum;
            set
            {
                if (minimum == value || value < 0 || value >= maximum)
                {
                    return;
                }

                minimum = value;
                if (curValue < value)
                {
                    curValue = value;
                }

                if (largeChange > (maximum - minimum))
                {
                    largeChange = maximum - minimum;
                }

                SetupScrollBar();

                if (curValue < value)
                {
                    dontUpdateColor = true;
                    Value = value;
                }
                else
                {
                    ChangeThumbPosition(GetThumbPosition());
                    Refresh();
                }
            }
        }

        private int maximum = 100;
        [DefaultValue(100)]
        public int Maximum
        {
            get => maximum;
            set
            {
                if (value == maximum || value < 1 || value <= minimum)
                {
                    return;
                }

                maximum = value;
                if (largeChange > (maximum - minimum))
                {
                    largeChange = maximum - minimum;
                }

                SetupScrollBar();

                if (curValue > value)
                {
                    dontUpdateColor = true;
                    Value = maximum;
                }
                else
                {
                    ChangeThumbPosition(GetThumbPosition());
                    Refresh();
                }
            }
        }

        private int smallChange = 1;
        [DefaultValue(1)]
        public int SmallChange
        {
            get => smallChange;
            set
            {
                if (value == smallChange || value < 1 || value >= largeChange)
                {
                    return;
                }

                smallChange = value;
                SetupScrollBar();
            }
        }

        private int largeChange = 10;
        [DefaultValue(10)]
        public int LargeChange
        {
            get => largeChange;
            set
            {
                if (value == largeChange || value < smallChange || value < 2)
                {
                    return;
                }

                if (value > (maximum - minimum))
                {
                    largeChange = maximum - minimum;
                }
                else
                {
                    largeChange = value;
                }

                SetupScrollBar();
            }
        }

        #region ValueChangeEvent
        // Declare a delegate
        public delegate void ScrollValueChangedDelegate(object sender, int newValue);

        public event ScrollValueChangedDelegate ValueChanged;
        #endregion

        private bool dontUpdateColor = false;

        private int curValue = 0;
        [DefaultValue(0)]
        [Browsable(false)]
        public int Value
        {
            get => curValue;

            set
            {
                if (curValue == value || value < minimum || value > maximum)
                {
                    return;
                }

                curValue = value;

                ChangeThumbPosition(GetThumbPosition());

                OnScroll(ScrollEventType.ThumbPosition, -1, value, scrollOrientation);

                if (!dontUpdateColor && HighlightOnWheel)
                {
                    if (!isHovered)
                    {
                        isHovered = true;
                    }

                    if (autoHoverTimer == null)
                    {
                        autoHoverTimer = new Timer();
                        autoHoverTimer.Interval = 1000;
                        autoHoverTimer.Tick += new EventHandler(autoHoverTimer_Tick);
                        autoHoverTimer.Start();
                    }
                    else
                    {
                        autoHoverTimer.Stop();
                        autoHoverTimer.Start();
                    }
                }
                else
                {
                    dontUpdateColor = false;
                }

                Refresh();
            }
        }

        private void autoHoverTimer_Tick(object sender, EventArgs e)
        {
            isHovered = false;
            Invalidate();
            autoHoverTimer.Stop();
        }

        private Timer autoHoverTimer = null;

        #endregion

        public MaterialScrollBar()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable |
                     //                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint, true);

            Width = SCROLLBAR_DEFAULT_SIZE;
            Height = 200;

            UseAccentColor = false;

            SetupScrollBar();

            progressTimer.Interval = 20;
            progressTimer.Tick += ProgressTimerTick;
        }

        public MaterialScrollBar(MateScrollOrientation orientation) : this()
        {
            Orientation = orientation;
        }

        public MaterialScrollBar(MateScrollOrientation orientation, int width) : this(orientation)
        {
            Width = width;
        }

        public bool HitTest(Point point)
        {
            return thumbRectangle.Contains(point);
        }

        #region Update Methods

        [SecuritySafeCritical]
        public void BeginUpdate()
        {
            SendMessage(Handle, WM_SETREDRAW, 0, 0);
            inUpdate = true;
        }

        [SecuritySafeCritical]
        public void EndUpdate()
        {
            SendMessage(Handle, WM_SETREDRAW, 1, 0);
            inUpdate = false;
            SetupScrollBar();
            Refresh();
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawScrollBar(e.Graphics, MaterialSkinManager.Instance.CardsColor, SkinManager.SwitchOffTrackColor, useAccentColor ? MaterialSkinManager.Instance.ColorScheme.AccentColor : MaterialSkinManager.Instance.ColorScheme.PrimaryColor);
        }

        private void DrawScrollBar(Graphics g, Color backColor, Color thumbColor, Color barColor)
        {
            if (UseBarColor)
            {
                using SolidBrush b = new(barColor);
                g.FillRectangle(b, ClientRectangle);
            }

            using (SolidBrush b = new(backColor))
            {
                Rectangle thumbRect = new(thumbRectangle.X - 1, thumbRectangle.Y - 1, thumbRectangle.Width + 2, thumbRectangle.Height + 2);
                g.FillRectangle(b, thumbRect);
            }

            using (SolidBrush b = new(isHovered ? barColor : thumbColor))
            {
                g.FillRectangle(b, thumbRectangle);
            }
        }

        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isHovered = false;
            //isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isHovered = false;
            //isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int v = e.Delta / 120 * (maximum - minimum) / mouseWheelBarPartitions;

            if (Orientation == MateScrollOrientation.Vertical)
            {
                Value -= v;
            }
            else
            {
                Value += v;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);

            Focus();

            if (e.Button == MouseButtons.Left)
            {

                Point mouseLocation = e.Location;

                if (thumbRectangle.Contains(mouseLocation))
                {
                    thumbClicked = true;
                    thumbPosition = MaterialOrientation == MateScrollOrientation.Vertical ? mouseLocation.Y - thumbRectangle.Y : mouseLocation.X - thumbRectangle.X;

                    Invalidate(thumbRectangle);
                }
                else
                {
                    trackPosition = MaterialOrientation == MateScrollOrientation.Vertical ? mouseLocation.Y : mouseLocation.X;

                    if (trackPosition < (MaterialOrientation == MateScrollOrientation.Vertical ? thumbRectangle.Y : thumbRectangle.X))
                    {
                        topBarClicked = true;
                    }
                    else
                    {
                        bottomBarClicked = true;
                    }

                    ProgressThumb(true);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                trackPosition = MaterialOrientation == MateScrollOrientation.Vertical ? e.Y : e.X;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            //isPressed = false;

            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                if (thumbClicked)
                {
                    thumbClicked = false;
                    OnScroll(ScrollEventType.EndScroll, -1, curValue, scrollOrientation);
                }
                else if (topBarClicked)
                {
                    topBarClicked = false;
                    StopTimer();
                }
                else if (bottomBarClicked)
                {
                    bottomBarClicked = false;
                    StopTimer();
                }

                Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);

            ResetScrollStatus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                if (thumbClicked)
                {
                    int oldScrollValue = curValue;

                    int pos = MaterialOrientation == MateScrollOrientation.Vertical ? e.Location.Y : e.Location.X;
                    int thumbSize = MaterialOrientation == MateScrollOrientation.Vertical ? pos / Height / thumbHeight : pos / Width / thumbWidth;

                    if (pos <= (thumbTopLimit + thumbPosition))
                    {
                        ChangeThumbPosition(thumbTopLimit);
                        curValue = minimum;
                        Invalidate();
                    }
                    else if (pos >= (thumbBottomLimitTop + thumbPosition))
                    {
                        ChangeThumbPosition(thumbBottomLimitTop);
                        curValue = maximum;
                        Invalidate();
                    }
                    else
                    {
                        ChangeThumbPosition(pos - thumbPosition);

                        int pixelRange, thumbPos;

                        if (Orientation == MateScrollOrientation.Vertical)
                        {
                            pixelRange = Height - thumbSize;
                            thumbPos = thumbRectangle.Y;
                        }
                        else
                        {
                            pixelRange = Width - thumbSize;
                            thumbPos = thumbRectangle.X;
                        }

                        float perc = 0f;

                        if (pixelRange != 0)
                        {
                            perc = thumbPos / (float)pixelRange;
                        }

                        curValue = Convert.ToInt32((perc * (maximum - minimum)) + minimum);
                    }

                    if (oldScrollValue != curValue)
                    {
                        OnScroll(ScrollEventType.ThumbTrack, oldScrollValue, curValue, scrollOrientation);
                        Refresh();
                    }
                }
            }
            else if (!ClientRectangle.Contains(e.Location))
            {
                ResetScrollStatus();
            }
            else if (e.Button == MouseButtons.None)
            {
                if (thumbRectangle.Contains(e.Location))
                {
                    Invalidate(thumbRectangle);
                }
                else if (ClientRectangle.Contains(e.Location))
                {
                    Invalidate();
                }
            }
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            isHovered = true;
            //isPressed = true;
            Invalidate();

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            isHovered = false;
            //isPressed = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        #endregion

        #region Management Methods

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);

            if (DesignMode)
            {
                SetupScrollBar();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetupScrollBar();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys keyUp = Keys.Up;
            Keys keyDown = Keys.Down;

            if (Orientation == MateScrollOrientation.Horizontal)
            {
                keyUp = Keys.Left;
                keyDown = Keys.Right;
            }

            if (keyData == keyUp)
            {
                Value -= smallChange;

                return true;
            }

            if (keyData == keyDown)
            {
                Value += smallChange;

                return true;
            }

            if (keyData == Keys.PageUp)
            {
                Value = GetValue(false, true);

                return true;
            }

            if (keyData == Keys.PageDown)
            {
                if (curValue + largeChange > maximum)
                {
                    Value = maximum;
                }
                else
                {
                    Value += largeChange;
                }

                return true;
            }

            if (keyData == Keys.Home)
            {
                Value = minimum;

                return true;
            }

            if (keyData == Keys.End)
            {
                Value = maximum;

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private void SetupScrollBar()
        {
            if (inUpdate)
            {
                return;
            }

            if (Orientation == MateScrollOrientation.Vertical)
            {
                thumbWidth = Width > 0 ? Width : 10;
                thumbHeight = GetThumbSize();

                clickedBarRectangle = ClientRectangle;
                clickedBarRectangle.Inflate(-1, -1);

                thumbRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, thumbWidth, thumbHeight);

                thumbPosition = thumbRectangle.Height / 2;
                thumbBottomLimitBottom = ClientRectangle.Bottom;
                thumbBottomLimitTop = thumbBottomLimitBottom - thumbRectangle.Height;
                thumbTopLimit = ClientRectangle.Y;
            }
            else
            {
                thumbHeight = Height > 0 ? Height : 10;
                thumbWidth = GetThumbSize();

                clickedBarRectangle = ClientRectangle;
                clickedBarRectangle.Inflate(-1, -1);

                thumbRectangle = new Rectangle(ClientRectangle.X, ClientRectangle.Y, thumbWidth, thumbHeight);

                thumbPosition = thumbRectangle.Width / 2;
                thumbBottomLimitBottom = ClientRectangle.Right;
                thumbBottomLimitTop = thumbBottomLimitBottom - thumbRectangle.Width;
                thumbTopLimit = ClientRectangle.X;
            }

            ChangeThumbPosition(GetThumbPosition());

            Refresh();
        }

        private void ResetScrollStatus()
        {
            bottomBarClicked = topBarClicked = false;

            StopTimer();
            Refresh();
        }

        private void ProgressTimerTick(object sender, EventArgs e)
        {
            ProgressThumb(true);
        }

        private int GetValue(bool smallIncrement, bool up)
        {
            int newValue;

            if (up)
            {
                newValue = curValue - (smallIncrement ? smallChange : largeChange);

                if (newValue < minimum)
                {
                    newValue = minimum;
                }
            }
            else
            {
                newValue = curValue + (smallIncrement ? smallChange : largeChange);

                if (newValue > maximum)
                {
                    newValue = maximum;
                }
            }

            return newValue;
        }

        private int GetThumbPosition()
        {
            int pixelRange;

            if (thumbHeight == 0 || thumbWidth == 0)
            {
                return 0;
            }

            int thumbSize = MaterialOrientation == MateScrollOrientation.Vertical ? thumbPosition / Height / thumbHeight : thumbPosition / Width / thumbWidth;

            if (Orientation == MateScrollOrientation.Vertical)
            {
                pixelRange = Height - thumbSize;
            }
            else
            {
                pixelRange = Width - thumbSize;
            }

            int realRange = maximum - minimum;
            float perc = 0f;

            if (realRange != 0)
            {
                perc = (curValue - (float)minimum) / realRange;
            }

            return Math.Max(thumbTopLimit, Math.Min(thumbBottomLimitTop, Convert.ToInt32(perc * pixelRange)));
        }

        private int GetThumbSize()
        {
            int trackSize =
                MaterialOrientation == MateScrollOrientation.Vertical ?
                    Height : Width;

            if (maximum == 0 || largeChange == 0)
            {
                return trackSize;
            }

            float newThumbSize = largeChange * (float)trackSize / maximum;

            return Convert.ToInt32(Math.Min(trackSize, Math.Max(newThumbSize, 10f)));
        }

        private void EnableTimer()
        {
            if (!progressTimer.Enabled)
            {
                progressTimer.Interval = 600;
                progressTimer.Start();
            }
            else
            {
                progressTimer.Interval = 10;
            }
        }

        private void StopTimer()
        {
            progressTimer.Stop();
        }

        private void ChangeThumbPosition(int position)
        {
            if (Orientation == MateScrollOrientation.Vertical)
            {
                thumbRectangle.Y = position;
            }
            else
            {
                thumbRectangle.X = position;
            }
        }

        private void ProgressThumb(bool enableTimer)
        {
            int scrollOldValue = curValue;
            ScrollEventType type = ScrollEventType.First;
            int thumbSize, thumbPos;

            if (Orientation == MateScrollOrientation.Vertical)
            {
                thumbPos = thumbRectangle.Y;
                thumbSize = thumbRectangle.Height;
            }
            else
            {
                thumbPos = thumbRectangle.X;
                thumbSize = thumbRectangle.Width;
            }

            if (bottomBarClicked && (thumbPos + thumbSize) < trackPosition)
            {
                type = ScrollEventType.LargeIncrement;

                curValue = GetValue(false, false);

                if (curValue == maximum)
                {
                    ChangeThumbPosition(thumbBottomLimitTop);

                    type = ScrollEventType.Last;
                }
                else
                {
                    ChangeThumbPosition(Math.Min(thumbBottomLimitTop, GetThumbPosition()));
                }
            }
            else if (topBarClicked && thumbPos > trackPosition)
            {
                type = ScrollEventType.LargeDecrement;

                curValue = GetValue(false, true);

                if (curValue == minimum)
                {
                    ChangeThumbPosition(thumbTopLimit);

                    type = ScrollEventType.First;
                }
                else
                {
                    ChangeThumbPosition(Math.Max(thumbTopLimit, GetThumbPosition()));
                }
            }

            if (scrollOldValue != curValue)
            {
                OnScroll(type, scrollOldValue, curValue, scrollOrientation);

                Invalidate();

                if (enableTimer)
                {
                    EnableTimer();
                }
            }
        }

        #endregion
    }

    #endregion
}