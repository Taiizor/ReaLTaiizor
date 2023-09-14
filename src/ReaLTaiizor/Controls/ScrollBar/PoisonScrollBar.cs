#region Imports

using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Native;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonScrollBar

    [Designer(typeof(PoisonScrollBarDesigner))]
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    public class PoisonScrollBar : Control, IPoisonControl
    {
        #region Interface

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                {
                    return poisonStyle;
                }

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                {
                    return StyleManager.Style;
                }

                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                {
                    return PoisonDefaults.Style;
                }

                return poisonStyle;
            }
            set => poisonStyle = value;
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                {
                    return poisonTheme;
                }

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }

                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                {
                    return PoisonDefaults.Theme;
                }

                return poisonTheme;
            }
            set => poisonTheme = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager { get; set; } = null;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor { get; set; } = false;
        [Browsable(false)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseCustomForeColor { get; set; } = false;
        [Browsable(false)]
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseStyleColors { get; set; } = false;

        [Browsable(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

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

        #region Fields

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

        private int trackPosition;

        private readonly Timer progressTimer = new();

        private int mouseWheelBarPartitions = 10;

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
        private bool isPressed;

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseBarColor { get; set; } = false;

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public int ScrollbarSize
        {
            get => Orientation == ScrollOrientationType.Vertical ? Width : Height;
            set
            {
                if (Orientation == ScrollOrientationType.Vertical)
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
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool HighlightOnWheel { get; set; } = false;

        private ScrollOrientationType poisonOrientation = ScrollOrientationType.Vertical;
        private ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;

        public ScrollOrientationType Orientation
        {
            get => poisonOrientation;

            set
            {
                if (value == poisonOrientation)
                {
                    return;
                }

                poisonOrientation = value;

                if (value == ScrollOrientationType.Vertical)
                {
                    scrollOrientation = ScrollOrientation.VerticalScroll;
                }
                else
                {
                    scrollOrientation = ScrollOrientation.HorizontalScroll;
                }

                Size = new(Height, Width);
                SetupScrollBar();
            }
        }

        private int minimum;
        private int maximum = 100;
        private int smallChange = 1;
        private int largeChange = 10;
        private int curValue;

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

        [DefaultValue(5)]
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
                        autoHoverTimer = new Timer
                        {
                            Interval = 1000
                        };
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

        #region Constructor

        public PoisonScrollBar()
        {
            SetStyle
            (
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                    true
            );

            Width = 10;
            Height = 200;

            SetupScrollBar();

            progressTimer.Interval = 20;
            progressTimer.Tick += ProgressTimerTick;
        }

        public PoisonScrollBar(ScrollOrientationType orientation) : this()
        {
            Orientation = orientation;
        }

        public PoisonScrollBar(ScrollOrientationType orientation, int width) : this(orientation)
        {
            Width = width;
        }

        public bool HitTest(Point point)
        {
            return thumbRectangle.Contains(point);
        }

        #endregion

        #region Update Methods

        [SecuritySafeCritical]
        public void BeginUpdate()
        {
            WinApi.SendMessage(Handle, (int)WinApi.Messages.WM_SETREDRAW, false, 0);
            inUpdate = true;
        }

        [SecuritySafeCritical]
        public void EndUpdate()
        {
            WinApi.SendMessage(Handle, (int)WinApi.Messages.WM_SETREDRAW, true, 0);
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
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    if (Parent != null)
                    {
                        if (Parent is IPoisonControl)
                        {
                            backColor = PoisonPaint.BackColor.Form(Theme);
                        }
                        else
                        {
                            backColor = Parent.BackColor;
                        }
                    }
                    else
                    {
                        backColor = PoisonPaint.BackColor.Form(Theme);
                    }
                }

                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new PoisonPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            Color backColor, thumbColor, barColor;

            if (UseCustomBackColor)
            {
                backColor = BackColor;
            }
            else
            {
                if (Parent != null)
                {
                    if (Parent is IPoisonControl)
                    {
                        backColor = PoisonPaint.BackColor.Form(Theme);
                    }
                    else
                    {
                        backColor = Parent.BackColor;
                    }
                }
                else
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
                }
            }

            if (isHovered && !isPressed && Enabled)
            {
                thumbColor = PoisonPaint.BackColor.ScrollBar.Thumb.Hover(Theme);
                barColor = PoisonPaint.BackColor.ScrollBar.Bar.Hover(Theme);
            }
            else if (isHovered && isPressed && Enabled)
            {
                thumbColor = PoisonPaint.BackColor.ScrollBar.Thumb.Press(Theme);
                barColor = PoisonPaint.BackColor.ScrollBar.Bar.Press(Theme);
            }
            else if (!Enabled)
            {
                thumbColor = PoisonPaint.BackColor.ScrollBar.Thumb.Disabled(Theme);
                barColor = PoisonPaint.BackColor.ScrollBar.Bar.Disabled(Theme);
            }
            else
            {
                thumbColor = PoisonPaint.BackColor.ScrollBar.Thumb.Normal(Theme);
                barColor = PoisonPaint.BackColor.ScrollBar.Bar.Normal(Theme);
            }

            DrawScrollBar(e.Graphics, backColor, thumbColor, barColor);

            OnCustomPaintForeground(new PoisonPaintEventArgs(backColor, thumbColor, e.Graphics));
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

            using (SolidBrush b = new(thumbColor))
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
            isPressed = false;
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
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int v = e.Delta / 120 * (maximum - minimum) / mouseWheelBarPartitions;

            if (Orientation == ScrollOrientationType.Vertical)
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
                isPressed = true;
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
                    thumbPosition = poisonOrientation == ScrollOrientationType.Vertical ? mouseLocation.Y - thumbRectangle.Y : mouseLocation.X - thumbRectangle.X;

                    Invalidate(thumbRectangle);
                }
                else
                {
                    trackPosition = poisonOrientation == ScrollOrientationType.Vertical ? mouseLocation.Y : mouseLocation.X;

                    if (trackPosition < (poisonOrientation == ScrollOrientationType.Vertical ? thumbRectangle.Y : thumbRectangle.X))
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
                trackPosition = poisonOrientation == ScrollOrientationType.Vertical ? e.Y : e.X;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;

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

                    int pos = poisonOrientation == ScrollOrientationType.Vertical ? e.Location.Y : e.Location.X;
                    int thumbSize = poisonOrientation == ScrollOrientationType.Vertical ? pos / Height / thumbHeight : pos / Width / thumbWidth;

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

                        if (Orientation == ScrollOrientationType.Vertical)
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
            isPressed = true;
            Invalidate();

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            isHovered = false;
            isPressed = false;
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

            if (Orientation == ScrollOrientationType.Horizontal)
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

            if (Orientation == ScrollOrientationType.Vertical)
            {
                thumbWidth = Width > 0 ? Width : 10;
                thumbHeight = GetThumbSize();

                clickedBarRectangle = ClientRectangle;
                clickedBarRectangle.Inflate(-1, -1);

                thumbRectangle = new(ClientRectangle.X, ClientRectangle.Y, thumbWidth, thumbHeight);

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

                thumbRectangle = new(ClientRectangle.X, ClientRectangle.Y, thumbWidth, thumbHeight);

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

            int thumbSize = poisonOrientation == ScrollOrientationType.Vertical ? thumbPosition / Height / thumbHeight : thumbPosition / Width / thumbWidth;

            if (Orientation == ScrollOrientationType.Vertical)
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
                poisonOrientation == ScrollOrientationType.Vertical ?
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
            if (Orientation == ScrollOrientationType.Vertical)
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

            if (Orientation == ScrollOrientationType.Vertical)
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