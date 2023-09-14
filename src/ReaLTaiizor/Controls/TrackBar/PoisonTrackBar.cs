#region Imports

using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Interface.Poison;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonTrackBar

    [ToolboxBitmap(typeof(System.Windows.Forms.TrackBar))]
    [DefaultEvent("Scroll")]
    public class PoisonTrackBar : Control, IPoisonControl
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
        [DefaultValue(true)]
        public bool UseSelectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }

        #endregion

        #region Events

        public event EventHandler ValueChanged;
        private void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public event ScrollEventHandler Scroll;
        private void OnScroll(ScrollEventType scrollType, int newValue)
        {
            Scroll?.Invoke(this, new ScrollEventArgs(scrollType, newValue));
        }


        #endregion

        #region Fields

        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool DisplayFocus { get; set; } = false;

        private int trackerValue = 50;
        [DefaultValue(50)]
        public int Value
        {
            get => trackerValue;
            set
            {
                if (value >= barMinimum & value <= barMaximum)
                {
                    trackerValue = value;
                    OnValueChanged();
                    Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
                }
            }
        }

        private int barMinimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get => barMinimum;
            set
            {
                if (value < barMaximum)
                {
                    barMinimum = value;
                    if (trackerValue < barMinimum)
                    {
                        trackerValue = barMinimum;
                        ValueChanged?.Invoke(this, new EventArgs());
                    }
                    Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
                }
            }
        }


        private int barMaximum = 100;
        [DefaultValue(100)]
        public int Maximum
        {
            get => barMaximum;
            set
            {
                if (value > barMinimum)
                {
                    barMaximum = value;
                    if (trackerValue > barMaximum)
                    {
                        trackerValue = barMaximum;
                        ValueChanged?.Invoke(this, new EventArgs());
                    }
                    Invalidate();
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
                }
            }
        }

        [DefaultValue(1)]
        public int SmallChange { get; set; } = 1;
        [DefaultValue(5)]
        public int LargeChange { get; set; } = 5;

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
                    throw new ArgumentOutOfRangeException("MouseWheelBarPartitions has to be greather than zero");
                }
            }
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public PoisonTrackBar(int min, int max, int value)
        {
            SetStyle
            (
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserMouse |
                ControlStyles.UserPaint,
                     true
            );

            BackColor = Color.Transparent;

            Minimum = min;
            Maximum = max;
            Value = value;
        }

        public PoisonTrackBar() : this(0, 100, 50) { }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!UseCustomBackColor)
                {
                    backColor = PoisonPaint.BackColor.Form(Theme);
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
            Color thumbColor, barColor;

            if (isHovered && !isPressed && Enabled)
            {
                thumbColor = PoisonPaint.BackColor.TrackBar.Thumb.Hover(Theme);
                barColor = PoisonPaint.BackColor.TrackBar.Bar.Hover(Theme);
            }
            else if (isHovered && isPressed && Enabled)
            {
                thumbColor = PoisonPaint.BackColor.TrackBar.Thumb.Press(Theme);
                barColor = PoisonPaint.BackColor.TrackBar.Bar.Press(Theme);
            }
            else if (!Enabled)
            {
                thumbColor = PoisonPaint.BackColor.TrackBar.Thumb.Disabled(Theme);
                barColor = PoisonPaint.BackColor.TrackBar.Bar.Disabled(Theme);
            }
            else
            {
                thumbColor = PoisonPaint.BackColor.TrackBar.Thumb.Normal(Theme);
                barColor = PoisonPaint.BackColor.TrackBar.Bar.Normal(Theme);
            }

            DrawTrackBar(e.Graphics, thumbColor, barColor);

            if (DisplayFocus && isFocused)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
            }
        }

        private void DrawTrackBar(Graphics g, Color thumbColor, Color barColor)
        {
            int TrackX = (trackerValue - barMinimum) * (Width - 6) / (barMaximum - barMinimum);

            using (SolidBrush b = new(thumbColor))
            {
                Rectangle barRect = new(0, (Height / 2) - 2, TrackX, 4);
                g.FillRectangle(b, barRect);

                Rectangle thumbRect = new(TrackX, (Height / 2) - 8, 6, 16);
                g.FillRectangle(b, thumbRect);
            }

            using (SolidBrush b = new(barColor))
            {
                Rectangle barRect = new(TrackX + 7, (Height / 2) - 2, Width - TrackX + 7, 4);
                g.FillRectangle(b, barRect);
            }
        }

        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            Invalidate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            Invalidate();

            base.OnLeave(e);
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

            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Left:
                    SetProperValue(Value - SmallChange);
                    OnScroll(ScrollEventType.SmallDecrement, Value);
                    break;
                case Keys.Up:
                case Keys.Right:
                    SetProperValue(Value + SmallChange);
                    OnScroll(ScrollEventType.SmallIncrement, Value);
                    break;
                case Keys.Home:
                    Value = barMinimum;
                    break;
                case Keys.End:
                    Value = barMaximum;
                    break;
                case Keys.PageDown:
                    SetProperValue(Value - LargeChange);
                    OnScroll(ScrollEventType.LargeDecrement, Value);
                    break;
                case Keys.PageUp:
                    SetProperValue(Value + LargeChange);
                    OnScroll(ScrollEventType.LargeIncrement, Value);
                    break;
            }

            if (Value == barMinimum)
            {
                OnScroll(ScrollEventType.First, Value);
            }

            if (Value == barMaximum)
            {
                OnScroll(ScrollEventType.Last, Value);
            }

            Point pt = PointToClient(Cursor.Position);
            OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab | ModifierKeys == Keys.Shift)
            {
                return base.ProcessDialogKey(keyData);
            }
            else
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                OnScroll(ScrollEventType.ThumbTrack, trackerValue);
                OnValueChanged();
                OnMouseMove(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Capture & e.Button == MouseButtons.Left)
            {
                ScrollEventType set = ScrollEventType.ThumbPosition;
                Point pt = e.Location;
                int p = pt.X;

                float coef = (barMaximum - barMinimum) / (float)(ClientSize.Width - 3);
                trackerValue = (int)((p * coef) + barMinimum);

                if (trackerValue <= barMinimum)
                {
                    trackerValue = barMinimum;
                    set = ScrollEventType.First;
                }
                else if (trackerValue >= barMaximum)
                {
                    trackerValue = barMaximum;
                    set = ScrollEventType.Last;
                }

                OnScroll(set, trackerValue);
                OnValueChanged();

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int v = e.Delta / 120 * (barMaximum - barMinimum) / mouseWheelBarPartitions;
            SetProperValue(Value + v);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion

        #region Helper Methods

        private void SetProperValue(int val)
        {
            if (val < barMinimum)
            {
                Value = barMinimum;
            }
            else if (val > barMaximum)
            {
                Value = barMaximum;
            }
            else
            {
                Value = val;
            }
        }

        #endregion
    }

    #endregion
}