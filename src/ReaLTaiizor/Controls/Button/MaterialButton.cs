#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using ReaLTaiizor.Helpers;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Extensions;
using System.Drawing.Drawing2D;
using static ReaLTaiizor.Utils.MaterialAnimations;
using static ReaLTaiizor.Helpers.MaterialDrawHelper;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialButton

    public class MaterialButton : System.Windows.Forms.Button, MaterialControlI
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialManager SkinManager => MaterialManager.Instance;

        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public enum MaterialButtonType
        {
            Text,
            Outlined,
            Contained
        }

        public bool UseAccentColor
        {
            get { return useAccentColor; }
            set { useAccentColor = value; Invalidate(); }
        }

        public bool HighEmphasis
        {
            get { return highEmphasis; }
            set { highEmphasis = value; Invalidate(); }
        }

        public bool DrawShadows
        {
            get { return drawShadows; }
            set { drawShadows = value; Invalidate(); }
        }

        public MaterialButtonType Type
        {
            get { return type; }
            set { type = value; Invalidate(); }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            Invalidate();
            LocationChanged += (sender, e) => { if (DrawShadows) Parent?.Invalidate(); };
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (drawShadows && Parent != null) AddShadowPaintEvent(Parent, drawShadowOnParent);
            if (_oldParent != null) RemoveShadowPaintEvent(_oldParent, drawShadowOnParent);
            _oldParent = Parent;
        }

        private Control _oldParent;

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Parent == null) return;
            if (Visible)
                AddShadowPaintEvent(Parent, drawShadowOnParent);
            else
                RemoveShadowPaintEvent(Parent, drawShadowOnParent);
        }

        private bool _shadowDrawEventSubscribed = false;

        private void AddShadowPaintEvent(Control control, PaintEventHandler shadowPaintEvent)
        {
            if (_shadowDrawEventSubscribed) return;
            control.Paint += shadowPaintEvent;
            control.Invalidate();
            _shadowDrawEventSubscribed = true;
        }

        private void RemoveShadowPaintEvent(Control control, PaintEventHandler shadowPaintEvent)
        {
            if (!_shadowDrawEventSubscribed) return;
            control.Paint -= shadowPaintEvent;
            control.Invalidate();
            _shadowDrawEventSubscribed = false;
        }

        private readonly AnimationManager _hoverAnimationManager = null;
        private readonly AnimationManager _animationManager = null;

        private SizeF _textSize;

        private Image _icon;

        private bool drawShadows;
        private bool highEmphasis;
        private bool useAccentColor;
        private MaterialButtonType type;

        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                {
                    Refresh();
                }

                Invalidate();
            }
        }

        [DefaultValue(true)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }

        public MaterialButton()
        {
            DrawShadows = true;
            HighEmphasis = true;
            UseAccentColor = false;
            Type = MaterialButtonType.Contained;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _hoverAnimationManager = new AnimationManager
            {
                Increment = 0.12,
                AnimationType = AnimationType.Linear
            };

            _hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            _animationManager.OnAnimationProgress += sender => Invalidate();

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);
        }

        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                _textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.getFontByType(MaterialManager.fontType.Button));
                if (AutoSize)
                    Refresh();

                Invalidate();
            }
        }

        private void drawShadowOnParent(object sender, PaintEventArgs e)
        {
            if (Parent == null)
            {
                RemoveShadowPaintEvent((Control)sender, drawShadowOnParent);
                return;
            }

            if (!DrawShadows || Type != MaterialButtonType.Contained || Parent == null) return;

            // paint shadow on parent
            Graphics gp = e.Graphics;
            Rectangle rect = new Rectangle(Location, ClientRectangle.Size);
            gp.SmoothingMode = SmoothingMode.AntiAlias;
            DrawSquareShadow(gp, rect);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            double hoverAnimProgress = _hoverAnimationManager.GetProgress();

            g.Clear(Parent.BackColor);

            // button rectand path
            RectangleF buttonRectF = new RectangleF(ClientRectangle.Location, ClientRectangle.Size);
            buttonRectF.X -= 0.5f;
            buttonRectF.Y -= 0.5f;
            GraphicsPath buttonPath = CreateRoundRect(buttonRectF, 4);

            // button shadow (blend with form shadow)
            DrawSquareShadow(g, ClientRectangle);

            if (Type == MaterialButtonType.Contained)
            {
                // draw button rect
                // Disabled
                if (!Enabled)
                {
                    using (SolidBrush disabledBrush = new SolidBrush(BlendColor(Parent.BackColor, SkinManager.BackgroundDisabledColor, SkinManager.BackgroundDisabledColor.A)))
                        g.FillPath(disabledBrush, buttonPath);
                }
                // High emphasis
                else if (HighEmphasis)
                    g.FillPath(UseAccentColor ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, buttonPath);
                // Mormal
                else
                {
                    using (SolidBrush normalBrush = new SolidBrush(SkinManager.BackgroundColor))
                        g.FillPath(normalBrush, buttonPath);
                }
            }
            else
                g.Clear(Parent.BackColor);

            //Hover
            using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(
                (int)(hoverAnimProgress * SkinManager.BackgroundFocusColor.A), (UseAccentColor ? (HighEmphasis && Type == MaterialButtonType.Contained ?
                SkinManager.ColorScheme.AccentColor.Lighten(0.5f) : // Contained with Emphasis - with accent
                SkinManager.ColorScheme.AccentColor) : // Not Contained Or Low Emphasis - with accent
                (Type == MaterialButtonType.Contained && HighEmphasis ? SkinManager.ColorScheme.LightPrimaryColor : // Contained with Emphasis without accent
                SkinManager.ColorScheme.PrimaryColor)).RemoveAlpha()))) // Normal or Emphasis without accent
            {
                g.FillPath(hoverBrush, buttonPath);
            }

            if (Type == MaterialButtonType.Outlined)
            {
                using (Pen outlinePen = new Pen(Enabled ? SkinManager.DividersAlternativeColor : SkinManager.DividersColor, 1))
                {
                    buttonRectF.X += 0.5f;
                    buttonRectF.Y += 0.5f;
                    g.DrawPath(outlinePen, buttonPath);
                }
            }

            //Ripple
            if (_animationManager.IsAnimating())
            {
                g.Clip = new Region(buttonRectF);
                for (var i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(
                        Color.FromArgb((int)(100 - (animationValue * 100)), // Alpha animation
                        (Type == MaterialButtonType.Contained && HighEmphasis ? (UseAccentColor ?
                            SkinManager.ColorScheme.AccentColor.Lighten(0.5f) : // Emphasis with accent
                            SkinManager.ColorScheme.LightPrimaryColor) : // Emphasis
                            (UseAccentColor ? SkinManager.ColorScheme.AccentColor : // Normal with accent
                            SkinManager.Theme == MaterialManager.Themes.LIGHT ? SkinManager.ColorScheme.PrimaryColor : SkinManager.ColorScheme.LightPrimaryColor))))) // Normal
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.ResetClip();
            }

            //Icon
            var iconRect = new Rectangle(8, 6, 24, 24);

            if (string.IsNullOrEmpty(Text))
            {
                // Center Icon
                iconRect.X += 2;
            }

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;
            if (Icon != null)
            {
                textRect.Width -= 8 + 24 + 4 + 8; // left padding + icon width + space between Icon and Text + right padding
                textRect.X += 8 + 24 + 4; // left padding + icon width + space between Icon and Text
            }

            Color textColor = Enabled ? (HighEmphasis ? (Type == MaterialButtonType.Text || Type == MaterialButtonType.Outlined) ?
                (UseAccentColor ? SkinManager.ColorScheme.AccentColor : // Outline or Text and accent and emphasis
                SkinManager.ColorScheme.PrimaryColor) : // Outline or Text and emphasis
                SkinManager.ColorScheme.TextColor : // Contained and Emphasis
                SkinManager.TextHighEmphasisColor) : // Cointained and accent
                SkinManager.TextDisabledOrHintColor; // Disabled

            using (MaterialNativeTextRenderer NativeText = new MaterialNativeTextRenderer(g))
            {
                NativeText.DrawTransparentText(Text.ToUpper(), SkinManager.getLogFontByType(MaterialManager.fontType.Button),
                    textColor,
                    textRect.Location,
                    textRect.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Center | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }
        }

        private Size GetPreferredSize()
        {
            return GetPreferredSize(Size);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size s = base.GetPreferredSize(proposedSize);

            // Provides extra space for proper padding for content
            var extra = 16;

            if (Icon != null)
            {
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;
            }

            if (AutoSize)
            {
                s.Width = (int)Math.Ceiling(_textSize.Width);
                s.Width += extra;
                s.Height = 36;
            }
            else
            {
                s.Width += extra;
                s.Height = 36;
            }

            return s;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode)
            {
                return;
            }

            MouseState = MaterialMouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MaterialMouseState.HOVER;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MaterialMouseState.OUT;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MaterialMouseState.DOWN;

                    _animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MaterialMouseState.HOVER;

                Invalidate();
            };

            GotFocus += (sender, args) =>
            {
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            LostFocus += (sender, args) =>
            {
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };

            KeyDown += (object sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.In, new Point(ClientRectangle.Width >> 1, ClientRectangle.Height >> 1));
                    Invalidate();
                }
            };
        }
    }

    #endregion
}