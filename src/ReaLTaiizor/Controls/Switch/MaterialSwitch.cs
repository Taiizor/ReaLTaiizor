#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialSwitch

    public class MaterialSwitch : System.Windows.Forms.CheckBox, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private bool useAccentColor;

        [Category("Material")]
        public bool UseAccentColor
        {
            get => useAccentColor;
            set
            {
                useAccentColor = value;
                Invalidate();
            }
        }

        private bool _ripple;

        [Category("Appearance")]
        public bool Ripple
        {
            get => _ripple;
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

        [Category("Appearance")]
        [Browsable(true), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Always)]
        public bool ReadOnly { get; set; }

        private readonly AnimationManager _checkAM;
        private readonly AnimationManager _hoverAM;
        private readonly AnimationManager _rippleAM;

        private const int THUMB_SIZE = 22;

        private const int THUMB_SIZE_HALF = THUMB_SIZE / 2;

        private const int TRACK_SIZE_HEIGHT = (int)14;
        private const int TRACK_SIZE_WIDTH = (int)36;
        private const int TRACK_RADIUS = (int)(TRACK_SIZE_HEIGHT / 2);

        private int TRACK_CENTER_Y;
        private int TRACK_CENTER_X_BEGIN;
        private int TRACK_CENTER_X_END;
        private int TRACK_CENTER_X_DELTA;

        private const int RIPPLE_DIAMETER = 37;

        private int _trackOffsetY;

        public MaterialSwitch()
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
            _checkAM.OnAnimationProgress += sender => Invalidate();
            _rippleAM.OnAnimationProgress += sender => Invalidate();
            _hoverAM.OnAnimationProgress += sender => Invalidate();

            CheckedChanged += (sender, args) =>
            {
                if (Ripple)
                {
                    _checkAM.StartNewAnimation(Checked ? AnimationDirection.In : AnimationDirection.Out);
                }
            };

            Ripple = true;
            MouseLocation = new Point(-1, -1);
            ReadOnly = false;
        }

        protected override void OnClick(EventArgs e)
        {
            if (!ReadOnly)
            {
                base.OnClick(e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            _trackOffsetY = (Height / 2) - THUMB_SIZE_HALF;

            TRACK_CENTER_Y = _trackOffsetY + THUMB_SIZE_HALF - 1;
            TRACK_CENTER_X_BEGIN = TRACK_CENTER_Y;
            TRACK_CENTER_X_END = TRACK_CENTER_X_BEGIN + TRACK_SIZE_WIDTH - (TRACK_RADIUS * 2);
            TRACK_CENTER_X_DELTA = TRACK_CENTER_X_END - TRACK_CENTER_X_BEGIN;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size strSize;
            using (MaterialNativeTextRenderer NativeText = new(CreateGraphics()))
            {
                strSize = NativeText.MeasureLogString(Text, SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Body1));
            }
            int w = TRACK_SIZE_WIDTH + THUMB_SIZE + strSize.Width;
            return Ripple ? new Size(w, RIPPLE_DIAMETER) : new Size(w, THUMB_SIZE);
        }

        private static readonly Point[] CheckmarkLine = { new Point(3, 8), new Point(7, 12), new Point(14, 5) };

        private const int TEXT_OFFSET = THUMB_SIZE;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);

            double animationProgress = _checkAM.GetProgress();

            // Draw Track
            Color thumbColor = BlendColor(
                        Enabled ? SkinManager.SwitchOffThumbColor : SkinManager.SwitchOffDisabledThumbColor, // Off color
                        Enabled ? UseAccentColor ? SkinManager.ColorScheme.AccentColor : SkinManager.ColorScheme.PrimaryColor : BlendColor(UseAccentColor ? SkinManager.ColorScheme.AccentColor : SkinManager.ColorScheme.PrimaryColor, SkinManager.SwitchOffDisabledThumbColor, 197), // On color
                        animationProgress * 255); // Blend amount

            using (GraphicsPath path = CreateRoundRect(new Rectangle(TRACK_CENTER_X_BEGIN - TRACK_RADIUS, TRACK_CENTER_Y - (TRACK_SIZE_HEIGHT / 2), TRACK_SIZE_WIDTH, TRACK_SIZE_HEIGHT), TRACK_RADIUS))
            {
                using SolidBrush trackBrush = new(
                    Color.FromArgb(Enabled ? SkinManager.SwitchOffTrackColor.A : SkinManager.BackgroundDisabledColor.A, // Track alpha
                    BlendColor( // animate color
                        Enabled ? SkinManager.SwitchOffTrackColor : SkinManager.BackgroundDisabledColor, // Off color
                        UseAccentColor ? SkinManager.ColorScheme.AccentColor : SkinManager.ColorScheme.PrimaryColor, // On color
                        animationProgress * 255) // Blend amount
                        .RemoveAlpha()));
                g.FillPath(trackBrush, path);
            }

            // Calculate animation movement X position
            int OffsetX = (int)(TRACK_CENTER_X_DELTA * animationProgress);

            // Ripple
            int rippleSize = (Height % 2 == 0) ? Height - 2 : Height - 3;

            Color rippleColor = Color.FromArgb(40, // color alpha
                Checked ? UseAccentColor ? SkinManager.ColorScheme.AccentColor : SkinManager.ColorScheme.PrimaryColor : // On color
                (SkinManager.Theme == MaterialSkinManager.Themes.LIGHT ? Color.Black : Color.White)); // Off color

            if (Ripple && _rippleAM.IsAnimating())
            {
                for (int i = 0; i < _rippleAM.GetAnimationCount(); i++)
                {
                    double rippleAnimProgress = _rippleAM.GetProgress(i);
                    int rippleAnimatedDiameter = (_rippleAM.GetDirection(i) == AnimationDirection.InOutIn) ? (int)(rippleSize * (0.7 + (0.3 * rippleAnimProgress))) : rippleSize;

                    using SolidBrush rippleBrush = new(Color.FromArgb((int)(40 * rippleAnimProgress), rippleColor.RemoveAlpha()));
                    g.FillEllipse(rippleBrush, new Rectangle(TRACK_CENTER_X_BEGIN + OffsetX - (rippleAnimatedDiameter / 2), TRACK_CENTER_Y - (rippleAnimatedDiameter / 2), rippleAnimatedDiameter, rippleAnimatedDiameter));
                }
            }

            // Hover
            if (Ripple)
            {
                double rippleAnimProgress = _hoverAM.GetProgress();
                int rippleAnimatedDiameter = (int)(rippleSize * (0.7 + (0.3 * rippleAnimProgress)));

                using SolidBrush rippleBrush = new(Color.FromArgb((int)(40 * rippleAnimProgress), rippleColor.RemoveAlpha()));
                g.FillEllipse(rippleBrush, new Rectangle(TRACK_CENTER_X_BEGIN + OffsetX - (rippleAnimatedDiameter / 2), TRACK_CENTER_Y - (rippleAnimatedDiameter / 2), rippleAnimatedDiameter, rippleAnimatedDiameter));
            }

            // draw Thumb Shadow
            RectangleF thumbBounds = new(TRACK_CENTER_X_BEGIN + OffsetX - THUMB_SIZE_HALF, TRACK_CENTER_Y - THUMB_SIZE_HALF, THUMB_SIZE, THUMB_SIZE);
            using (SolidBrush shadowBrush = new(Color.FromArgb(12, 0, 0, 0)))
            {
                g.FillEllipse(shadowBrush, new RectangleF(thumbBounds.X - 2, thumbBounds.Y - 1, thumbBounds.Width + 4, thumbBounds.Height + 6));
                g.FillEllipse(shadowBrush, new RectangleF(thumbBounds.X - 1, thumbBounds.Y - 1, thumbBounds.Width + 2, thumbBounds.Height + 4));
                g.FillEllipse(shadowBrush, new RectangleF(thumbBounds.X - 0, thumbBounds.Y - 0, thumbBounds.Width + 0, thumbBounds.Height + 2));
                g.FillEllipse(shadowBrush, new RectangleF(thumbBounds.X - 0, thumbBounds.Y + 2, thumbBounds.Width + 0, thumbBounds.Height + 0));
                g.FillEllipse(shadowBrush, new RectangleF(thumbBounds.X - 0, thumbBounds.Y + 1, thumbBounds.Width + 0, thumbBounds.Height + 0));
            }

            // draw Thumb
            using (SolidBrush thumbBrush = new(thumbColor))
            {
                g.FillEllipse(thumbBrush, thumbBounds);
            }

            // draw text
            using MaterialNativeTextRenderer NativeText = new(g);
            Rectangle textLocation = new(TEXT_OFFSET + TRACK_SIZE_WIDTH, 0, Width - (TEXT_OFFSET + TRACK_SIZE_WIDTH), Height);
            NativeText.DrawTransparentText(
                Text,
                SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Body1),
                Enabled ? SkinManager.TextHighEmphasisColor : SkinManager.TextDisabledOrHintColor,
                textLocation.Location,
                textLocation.Size,
                MaterialNativeTextRenderer.TextAlignFlags.Left | MaterialNativeTextRenderer.TextAlignFlags.Middle);
        }

        private Bitmap DrawCheckMarkBitmap()
        {
            Bitmap checkMark = new(THUMB_SIZE, THUMB_SIZE);
            Graphics g = Graphics.FromImage(checkMark);

            // clear everything, transparent
            g.Clear(Color.Transparent);

            // draw the checkmark lines
            using (Pen pen = new(Parent.BackColor, 2))
            {
                g.DrawLines(pen, CheckmarkLine);
            }

            return checkMark;
        }

        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                base.AutoSize = value;
                if (value)
                {
                    Size = new Size(10, 10);
                }
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

            if (DesignMode)
            {
                return;
            }

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