#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotSwitch

    public class ParrotSwitch : Control
    {
        public ParrotSwitch()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            base.Size = new Size(60, 30);
            Cursor = Cursors.Hand;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The text of the button")]
        public Style SwitchStyle
        {
            get => switchStyle;
            set
            {
                switchStyle = value;
                SetSwitchColor = true;
                if (value == Style.iOS)
                {
                    base.Size = new Size(60, 30);
                }
                if (value == Style.Android)
                {
                    base.Size = new Size(58, 30);
                }
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The smoothing mode of the graphics")]
        public State SwitchState
        {
            get => switchState;
            set
            {
                switchState = value;
                OnSwitchStateChanged();
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The button on color")]
        public Color OnColor
        {
            get => onColor;
            set
            {
                onColor = value;
                SetSwitchColor = false;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The button off color")]
        public Color OffColor
        {
            get => offColor;
            set
            {
                offColor = value;
                SetSwitchColor = false;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The button on color")]
        public Color HandleOnColor
        {
            get => handleOnColor;
            set
            {
                handleOnColor = value;
                SetSwitchColor = false;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The button off color")]
        public Color HandleOffColor
        {
            get => handleOffColor;
            set
            {
                handleOffColor = value;
                SetSwitchColor = false;
                Invalidate();
            }
        }

        public event EventHandler SwitchStateChanged;

        protected virtual void OnSwitchStateChanged()
        {
            SwitchStateChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (SwitchState == State.On)
            {
                SwitchState = State.Off;
                return;
            }
            SwitchState = State.On;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            if (switchStyle == Style.iOS)
            {
                if (SetSwitchColor)
                {
                    onColor = Color.FromArgb(76, 217, 100);
                    handleOnColor = Color.FromArgb(255, 255, 255);
                    offColor = Color.FromArgb(255, 255, 255);
                    handleOffColor = Color.FromArgb(255, 255, 255);
                }
                if (switchState == State.On)
                {
                    e.Graphics.FillRectangle(new SolidBrush(onColor), 15, 0, 30, 29);
                    e.Graphics.FillPie(new SolidBrush(onColor), new Rectangle(1, 0, 30, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(onColor), new Rectangle(30, 0, 29, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOnColor), new Rectangle(31, 1, 27, 27), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOnColor), new Rectangle(32, 2, 25, 25), 0f, 360f);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(offColor), 15, 0, 30, 29);
                    e.Graphics.FillPie(new SolidBrush(offColor), new Rectangle(1, 0, 30, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(offColor), new Rectangle(30, 0, 29, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(2, 1, 29, 27), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOffColor), new Rectangle(3, 2, 27, 25), 0f, 360f);
                }
            }
            if (switchStyle == Style.Android)
            {
                if (SetSwitchColor)
                {
                    onColor = Color.FromArgb(217, 239, 237);
                    handleOnColor = Color.FromArgb(126, 199, 192);
                    offColor = Color.FromArgb(77, 77, 77);
                    handleOffColor = Color.FromArgb(185, 185, 185);
                }
                if (switchState == State.On)
                {
                    e.Graphics.FillRectangle(new SolidBrush(onColor), 10, 5, 30, 20);
                    e.Graphics.FillPie(new SolidBrush(onColor), new Rectangle(3, 5, 20, 20), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOnColor), new Rectangle(25, 0, 29, 29), 0f, 360f);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(offColor), 10, 5, 30, 20);
                    e.Graphics.FillPie(new SolidBrush(offColor), new Rectangle(28, 5, 20, 20), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOffColor), new Rectangle(0, 0, 29, 29), 0f, 360f);
                }
            }
            if (switchStyle == Style.Horizontal)
            {
                if (switchState == State.On)
                {
                    e.Graphics.FillRectangle(new SolidBrush(onColor), 0, 5, base.Width, base.Height - 10);
                    e.Graphics.FillRectangle(new SolidBrush(handleOnColor), (base.Width / 2) + 2, 7, (base.Width / 2) - 5, base.Height - 14);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(offColor), 0, 5, base.Width, base.Height - 10);
                    e.Graphics.FillRectangle(new SolidBrush(handleOffColor), 2, 7, (base.Width / 2) - 5, base.Height - 14);
                }
            }
            if (switchStyle == Style.Vertical)
            {
                if (switchState == State.On)
                {
                    e.Graphics.FillRectangle(new SolidBrush(onColor), 5, 0, base.Width - 10, base.Height);
                    e.Graphics.FillRectangle(new SolidBrush(handleOnColor), 7, (base.Height / 2) + 2, base.Width - 14, (base.Height / 2) - 5);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(offColor), 5, 0, base.Width - 10, base.Height);
                    e.Graphics.FillRectangle(new SolidBrush(handleOffColor), 7, 2, base.Width - 14, (base.Height / 2) - 5);
                }
            }
            if (switchStyle == Style.Dark)
            {
                if (SetSwitchColor)
                {
                    onColor = Color.FromArgb(40, 40, 40);
                    handleOnColor = Color.FromArgb(255, 255, 255);
                    offColor = Color.FromArgb(75, 75, 75);
                    handleOffColor = Color.FromArgb(255, 255, 255);
                }
                if (switchState == State.On)
                {
                    e.Graphics.FillRectangle(new SolidBrush(onColor), 15, 0, 30, 29);
                    e.Graphics.FillPie(new SolidBrush(onColor), new Rectangle(1, 0, 30, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(onColor), new Rectangle(30, 0, 29, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(31, 1, 27, 27), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOnColor), new Rectangle(32, 2, 25, 25), 0f, 360f);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(offColor), 15, 0, 30, 29);
                    e.Graphics.FillPie(new SolidBrush(offColor), new Rectangle(1, 0, 30, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(offColor), new Rectangle(30, 0, 29, 29), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(Color.FromArgb(200, 200, 200)), new Rectangle(2, 1, 29, 27), 0f, 360f);
                    e.Graphics.FillPie(new SolidBrush(handleOffColor), new Rectangle(3, 2, 27, 25), 0f, 360f);
                }
            }
            base.OnPaint(e);
        }

        private bool SetSwitchColor = true;

        private Style switchStyle = Style.Horizontal;

        private State switchState;

        private Color onColor = Color.FromArgb(102, 217, 174);

        private Color offColor = Color.FromArgb(234, 129, 136);

        private Color handleOnColor = Color.FromArgb(1, 180, 120);

        private Color handleOffColor = Color.FromArgb(230, 71, 89);

        public enum Style
        {
            Vertical,
            Horizontal,
            iOS,
            Android,
            Dark
        }

        public enum State
        {
            On,
            Off
        }
    }

    #endregion
}