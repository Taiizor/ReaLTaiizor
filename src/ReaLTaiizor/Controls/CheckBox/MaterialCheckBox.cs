#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Helpers;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using static ReaLTaiizor.Utils.MaterialAnimations;
using static ReaLTaiizor.Helpers.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialCheckBox

    public class MaterialCheckBox : System.Windows.Forms.CheckBox, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private bool _ripple;

        [Category("Appearance")]
        public bool Ripple
        {
            get { return _ripple; }
            set
            {
                _ripple = value;
                AutoSize = AutoSize; //Make AutoSize directly set the bounds.

                if (value)
                {
                    Margin = new Padding(0);
                }

                Invalidate();
            }
        }

        private readonly AnimationManager _checkAM;
        private readonly AnimationManager _rippleAM;
        private readonly AnimationManager _hoverAM;
        private const int HEIGHT_RIPPLE = 37;
        private const int HEIGHT_NO_RIPPLE = 20;
        private const int TEXT_OFFSET = 26;
        private const int CHECKBOX_SIZE = 18;
        private const int CHECKBOX_SIZE_HALF = CHECKBOX_SIZE / 2;
        private int _boxOffset;

        public MaterialCheckBox()
        {
            _checkAM = new AnimationManager
            {
                AnimationType = AnimationType.EaseInOut,
                Increment = 0.05
            };
            _hoverAM = new AnimationManager(true)
            {
                AnimationType = AnimationType.Linear,
                Increment = 0.10
            };
            _rippleAM = new AnimationManager(false)
            {
                AnimationType = AnimationType.Linear,
                Increment = 0.10,
                SecondaryIncrement = 0.08
            };
            CheckedChanged += (sender, args) =>
            {
                if (Ripple)
                    _checkAM.StartNewAnimation(Checked ? AnimationDirection.In : AnimationDirection.Out);
            };
            _checkAM.OnAnimationProgress += sender => Invalidate();
            _hoverAM.OnAnimationProgress += sender => Invalidate();
            _rippleAM.OnAnimationProgress += sender => Invalidate();

            Ripple = true;
            Height = HEIGHT_RIPPLE;
            MouseLocation = new Point(-1, -1);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            _boxOffset = HEIGHT_RIPPLE / 2 - 9;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size strSize;

            using (MaterialNativeTextRenderer NativeText = new MaterialNativeTextRenderer(CreateGraphics()))
                strSize = NativeText.MeasureLogString(Text, SkinManager.getLogFontByType(MaterialManager.fontType.Body1));

            int w = _boxOffset + TEXT_OFFSET + strSize.Width;
            return Ripple ? new Size(w, HEIGHT_RIPPLE) : new Size(w, HEIGHT_NO_RIPPLE);
        }

        private static readonly Point[] CheckmarkLine = { new Point(3, 8), new Point(7, 12), new Point(14, 5) };

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // clear the control
            g.Clear(Parent.BackColor);

            int CHECKBOX_CENTER = _boxOffset + CHECKBOX_SIZE_HALF - 1;
            Point animationSource = new Point(CHECKBOX_CENTER, CHECKBOX_CENTER);
            double animationProgress = _checkAM.GetProgress();

            int colorAlpha = Enabled ? (int)(animationProgress * 255.0) : SkinManager.CheckBoxOffDisabledColor.A;
            int backgroundAlpha = Enabled ? (int)(SkinManager.CheckboxOffColor.A * (1.0 - animationProgress)) : SkinManager.CheckBoxOffDisabledColor.A;
            int rippleHeight = (HEIGHT_RIPPLE % 2 == 0) ? HEIGHT_RIPPLE - 3 : HEIGHT_RIPPLE - 2;

            SolidBrush brush = new SolidBrush(Color.FromArgb(colorAlpha, Enabled ? SkinManager.ColorScheme.AccentColor : SkinManager.CheckBoxOffDisabledColor));
            Pen pen = new Pen(brush.Color, 2);

            // draw hover animation
            if (Ripple)
            {
                double animationValue = _hoverAM.IsAnimating() ? _hoverAM.GetProgress() : hovered ? 1 : 0;
                int rippleSize = (int)(rippleHeight * (0.7 + (0.3 * animationValue)));

                using (SolidBrush rippleBrush = new SolidBrush(Color.FromArgb((int)(40 * animationValue), !Checked ? (SkinManager.Theme == MaterialManager.Themes.LIGHT ? Color.Black : Color.White) : brush.Color))) // no animation
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
            }

            // draw ripple animation
            if (Ripple && _rippleAM.IsAnimating())
            {
                for (int i = 0; i < _rippleAM.GetAnimationCount(); i++)
                {
                    double animationValue = _rippleAM.GetProgress(i);
                    int rippleSize = (_rippleAM.GetDirection(i) == AnimationDirection.InOutIn) ? (int)(rippleHeight * (0.7 + (0.3 * animationValue))) : rippleHeight;

                    using (SolidBrush rippleBrush = new SolidBrush(Color.FromArgb((int)((animationValue * 40)), !Checked ? (SkinManager.Theme == MaterialManager.Themes.LIGHT ? Color.Black : Color.White) : brush.Color)))
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                }
            }

            Rectangle checkMarkLineFill = new Rectangle(_boxOffset, _boxOffset, (int)(CHECKBOX_SIZE * animationProgress), CHECKBOX_SIZE);
            using (GraphicsPath checkmarkPath = MaterialDrawHelper.CreateRoundRect(_boxOffset - 0.5f, _boxOffset - 0.5f, CHECKBOX_SIZE, CHECKBOX_SIZE, 1))
            {
                if (Enabled)
                {
                    using (Pen pen2 = new Pen(MaterialDrawHelper.BlendColor(Parent.BackColor, Enabled ? SkinManager.CheckboxOffColor : SkinManager.CheckBoxOffDisabledColor, backgroundAlpha), 2))
                        g.DrawPath(pen2, checkmarkPath);

                    g.DrawPath(pen, checkmarkPath);
                    g.FillPath(brush, checkmarkPath);
                }
                else
                {
                    if (Checked)
                        g.FillPath(brush, checkmarkPath);
                    else
                        g.DrawPath(pen, checkmarkPath);
                }

                g.DrawImageUnscaledAndClipped(DrawCheckMarkBitmap(), checkMarkLineFill);
            }

            // draw checkbox text
            using (MaterialNativeTextRenderer NativeText = new MaterialNativeTextRenderer(g))
            {
                Rectangle textLocation = new Rectangle(_boxOffset + TEXT_OFFSET, 0, Width - (_boxOffset + TEXT_OFFSET), HEIGHT_RIPPLE);
                NativeText.DrawTransparentText(Text, SkinManager.getLogFontByType(MaterialManager.fontType.Body1),
                    Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                    textLocation.Location,
                    textLocation.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            // dispose used paint objects
            pen.Dispose();
            brush.Dispose();
        }

        private Bitmap DrawCheckMarkBitmap()
        {
            Bitmap checkMark = new Bitmap(CHECKBOX_SIZE, CHECKBOX_SIZE);
            Graphics g = Graphics.FromImage(checkMark);

            // clear everything, transparent
            g.Clear(Color.Transparent);

            // draw the checkmark lines
            using (Pen pen = new Pen(Parent.BackColor, 2))
                g.DrawLines(pen, CheckmarkLine);

            return checkMark;
        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                if (value)
                    Size = new Size(10, 10);
            }
        }

        private bool IsMouseInCheckArea()
        {
            return ClientRectangle.Contains(MouseLocation);
        }

        private bool hovered = false;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (DesignMode) return;

            MouseState = MaterialMouseState.OUT;

            GotFocus += (sender, AddingNewEventArgs) =>
            {
                if (Ripple && !hovered)
                {
                    _hoverAM.StartNewAnimation(AnimationDirection.In, new object[] { Checked });
                    hovered = true;
                }
            };

            LostFocus += (sender, args) =>
            {
                if (Ripple && hovered)
                {
                    _hoverAM.StartNewAnimation(AnimationDirection.Out, new object[] { Checked });
                    hovered = false;
                }
            };

            MouseEnter += (sender, args) =>
            {
                MouseState = MaterialMouseState.HOVER;
                //if (Ripple && !hovered)
                //{
                //    _hoverAM.StartNewAnimation(AnimationDirection.In, new object[] { Checked });
                //    hovered = true;
                //}
            };

            MouseLeave += (sender, args) =>
            {
                MouseLocation = new Point(-1, -1);
                MouseState = MaterialMouseState.OUT;
                //if (Ripple && hovered)
                //{
                //    _hoverAM.StartNewAnimation(AnimationDirection.Out, new object[] { Checked });
                //    hovered = false;
                //}
            };

            MouseDown += (sender, args) =>
            {
                MouseState = MaterialMouseState.DOWN;
                if (Ripple)
                {
                    _rippleAM.SecondaryIncrement = 0;
                    _rippleAM.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Checked });
                }
            };

            KeyDown += (sender, args) =>
            {
                if (Ripple && (args.KeyCode == Keys.Space) && _rippleAM.GetAnimationCount() == 0)
                {
                    _rippleAM.SecondaryIncrement = 0;
                    _rippleAM.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Checked });
                }
            };

            MouseUp += (sender, args) =>
            {
                if (Ripple)
                {
                    MouseState = MaterialMouseState.HOVER;
                    _rippleAM.SecondaryIncrement = 0.08;
                    _hoverAM.StartNewAnimation(AnimationDirection.Out, new object[] { Checked });
                    hovered = false;
                }
            };

            KeyUp += (sender, args) =>
            {
                if (Ripple && (args.KeyCode == Keys.Space))
                {
                    MouseState = MaterialMouseState.HOVER;
                    _rippleAM.SecondaryIncrement = 0.08;
                }
            };

            MouseMove += (sender, args) =>
            {
                MouseLocation = args.Location;
                Cursor = IsMouseInCheckArea() ? Cursors.Hand : Cursors.Default;
            };
        }
    }

    #endregion
}