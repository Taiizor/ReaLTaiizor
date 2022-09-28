#region Imports

using ReaLTaiizor.Extension;
using ReaLTaiizor.Helper;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.MaterialDrawHelper;
using static ReaLTaiizor.Util.MaterialAnimations;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MaterialButton

    public class MaterialButton : System.Windows.Forms.Button, MaterialControlI
    {
        private const int ICON_SIZE = 24;
        private const int MINIMUMWIDTH = 64;
        private const int MINIMUMWIDTHICONONLY = 36; //64;
        private const int HEIGHTDEFAULT = 36;
        private const int HEIGHTDENSE = 32;

        // icons
        private TextureBrush iconsBrushes;

        /// <summary>
        /// Gets or sets the Depth
        /// </summary>
        [Browsable(false)]
        public int Depth { get; set; }

        /// <summary>
        /// Gets the SkinManager
        /// </summary>
        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the MouseState
        /// </summary>
        [Browsable(false)]
        public MaterialMouseState MouseState { get; set; }

        public enum MaterialButtonType
        {
            Text,
            Outlined,
            Contained
        }

        public enum MaterialButtonDensity
        {
            Default,
            Dense
        }

        public enum MaterialIconType
        {
            Rebase,
            Default
        }

        [Category("Material")]
        public MaterialIconType IconType { get; set; }

        [Browsable(false)]
        public Color NoAccentTextColor { get; set; }

        [Category("Material")]
        public bool UseAccentColor
        {
            get => useAccentColor;
            set { useAccentColor = value; Invalidate(); }
        }

        [Category("Material")]
        /// <summary>
        /// Gets or sets a value indicating whether HighEmphasis
        /// </summary>
        public bool HighEmphasis
        {
            get => highEmphasis;
            set { highEmphasis = value; Invalidate(); }
        }

        [DefaultValue(true)]
        [Category("Material")]
        [Description("Draw Shadows around control")]
        public bool DrawShadows
        {
            get => drawShadows;
            set { drawShadows = value; Invalidate(); }
        }

        [Category("Material")]
        public MaterialButtonType Type
        {
            get => type;
            set { type = value; preProcessIcons(); Invalidate(); }
        }

        [Category("Material")]
        /// <summary>
        /// Gets or sets a value indicating button density
        /// </summary>
        public MaterialButtonDensity Density
        {
            get => _density;
            set
            {
                _density = value;
                if (_density == MaterialButtonDensity.Dense)
                {
                    Size = new Size(Size.Width, HEIGHTDENSE);
                }
                else
                {
                    Size = new Size(Size.Width, HEIGHTDEFAULT);
                }

                Invalidate();
            }
        }

        public enum CharacterCasingEnum
        {
            Normal,
            Lower,
            Upper,
            Title
        }

        public CharacterCasingEnum _cc;
        [Category("Behavior"), DefaultValue(CharacterCasingEnum.Upper), Description("Change capitalization of Text property")]
        public CharacterCasingEnum CharacterCasing
        {
            get => _cc;
            set
            {
                _cc = value;
                Invalidate();
            }
        }
        protected override void InitLayout()
        {
            base.InitLayout();
            Invalidate();
            LocationChanged += (sender, e) => { if (DrawShadows) { Parent?.Invalidate(); } };
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (drawShadows && Parent != null)
            {
                AddShadowPaintEvent(Parent, drawShadowOnParent);
            }

            if (_oldParent != null)
            {
                RemoveShadowPaintEvent(_oldParent, drawShadowOnParent);
            }

            _oldParent = Parent;
        }

        private Control _oldParent;

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Parent == null)
            {
                return;
            }

            if (Visible)
            {
                AddShadowPaintEvent(Parent, drawShadowOnParent);
            }
            else
            {
                RemoveShadowPaintEvent(Parent, drawShadowOnParent);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private bool _shadowDrawEventSubscribed = false;

        private void AddShadowPaintEvent(Control control, PaintEventHandler shadowPaintEvent)
        {
            if (_shadowDrawEventSubscribed)
            {
                return;
            }

            control.Paint += shadowPaintEvent;
            control.Invalidate();
            _shadowDrawEventSubscribed = true;
        }

        private void RemoveShadowPaintEvent(Control control, PaintEventHandler shadowPaintEvent)
        {
            if (!_shadowDrawEventSubscribed)
            {
                return;
            }

            control.Paint -= shadowPaintEvent;
            control.Invalidate();
            _shadowDrawEventSubscribed = false;
        }

        private readonly AnimationManager _hoverAnimationManager = null;
        private readonly AnimationManager _focusAnimationManager = null;
        private readonly AnimationManager _animationManager = null;

        /// <summary>
        /// Defines the _textSize
        /// </summary>
        private SizeF _textSize;

        /// <summary>
        /// Defines the _icon
        /// </summary>
        private Image _icon;

        private bool drawShadows;
        private bool highEmphasis;
        private bool useAccentColor;
        private MaterialButtonType type;
        private MaterialButtonDensity _density;

        [Category("Material")]
        /// <summary>
        /// Gets or sets the Icon
        /// </summary>
        public Image Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                preProcessIcons();

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

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialButton"/> class.
        /// </summary>
        public MaterialButton()
        {
            DrawShadows = true;
            HighEmphasis = true;
            UseAccentColor = false;
            Type = MaterialButtonType.Contained;
            Density = MaterialButtonDensity.Default;
            NoAccentTextColor = Color.Empty;
            CharacterCasing = CharacterCasingEnum.Upper;

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
            _focusAnimationManager = new AnimationManager
            {
                Increment = 0.12,
                AnimationType = AnimationType.Linear
            };
            SkinManager.ColorSchemeChanged += sender =>
            {
                preProcessIcons();
            };

            SkinManager.ThemeChanged += sender =>
            {
                preProcessIcons();
            };

            _hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            _focusAnimationManager.OnAnimationProgress += sender => Invalidate();
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
            get => base.Text;
            set
            {
                base.Text = value;

                if (!string.IsNullOrEmpty(value))
                {
                    _textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.GetFontByType(MaterialSkinManager.FontType.Button));
                }
                else
                {
                    _textSize.Width = 0;
                    _textSize.Height = 0;
                }

                if (AutoSize)
                {
                    Refresh();
                }

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

            if (!DrawShadows || Type != MaterialButtonType.Contained || Parent == null)
            {
                return;
            }

            // paint shadow on parent
            Graphics gp = e.Graphics;
            Rectangle rect = new(Location, ClientRectangle.Size);
            gp.SmoothingMode = SmoothingMode.AntiAlias;
            MaterialDrawHelper.DrawSquareShadow(gp, rect);
        }

        private void preProcessIcons()
        {
            if (Icon == null)
            {
                return;
            }

            int newWidth, newHeight;
            //Resize icon if greater than ICON_SIZE
            if (Icon.Width > ICON_SIZE || Icon.Height > ICON_SIZE)
            {
                //calculate aspect ratio
                float aspect = Icon.Width / (float)Icon.Height;

                //calculate new dimensions based on aspect ratio
                newWidth = (int)(ICON_SIZE * aspect);
                newHeight = (int)(newWidth / aspect);

                //if one of the two dimensions exceed the box dimensions
                if (newWidth > ICON_SIZE || newHeight > ICON_SIZE)
                {
                    //depending on which of the two exceeds the box dimensions set it as the box dimension and calculate the other one based on the aspect ratio
                    if (newWidth > newHeight)
                    {
                        newWidth = ICON_SIZE;
                        newHeight = (int)(newWidth / aspect);
                    }
                    else
                    {
                        newHeight = ICON_SIZE;
                        newWidth = (int)(newHeight * aspect);
                    }
                }
            }
            else
            {
                newWidth = Icon.Width;
                newHeight = Icon.Height;
            }

            Bitmap IconResized = new(Icon, newWidth, newHeight);

            // Calculate lightness and color
            float l = (SkinManager.Theme == MaterialSkinManager.Themes.LIGHT & (highEmphasis == false | Enabled == false | Type != MaterialButtonType.Contained)) ? 0f : 1.5f;

            // Create matrices
            float[][] matrixGray = {
                    new float[] {   0,   0,   0,   0,  0}, // Red scale factor
                    new float[] {   0,   0,   0,   0,  0}, // Green scale factor
                    new float[] {   0,   0,   0,   0,  0}, // Blue scale factor
                    new float[] {   0,   0,   0, Enabled ? .7f : .3f,  0}, // alpha scale factor
                    new float[] {   l,   l,   l,   0,  1}};// offset


            ColorMatrix colorMatrixGray = new(matrixGray);

            ImageAttributes grayImageAttributes = new();

            // Set color matrices
            grayImageAttributes.SetColorMatrix(colorMatrixGray, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            // Image Rect
            Rectangle destRect = new(0, 0, ICON_SIZE, ICON_SIZE);

            // Create a pre-processed copy of the image (GRAY)
            Bitmap bgray = new(destRect.Width, destRect.Height);
            using (Graphics gGray = Graphics.FromImage(bgray))
            {
                gGray.DrawImage(IconResized,
                    new Point[] {
                                new Point(0, 0),
                                new Point(destRect.Width, 0),
                                new Point(0, destRect.Height),
                    },
                    destRect, GraphicsUnit.Pixel, grayImageAttributes);
            }

            // added processed image to brush for drawing
            TextureBrush textureBrushGray = new(bgray);

            textureBrushGray.WrapMode = WrapMode.Clamp;

            // Translate the brushes to the correct positions
            Rectangle iconRect = new(8, (Height / 2) - (ICON_SIZE / 2), ICON_SIZE, ICON_SIZE);

            textureBrushGray.TranslateTransform(iconRect.X + (iconRect.Width / 2) - (IconResized.Width / 2),
                                                iconRect.Y + (iconRect.Height / 2) - (IconResized.Height / 2));

            iconsBrushes = textureBrushGray;
        }

        /// <summary>
        /// The OnPaint
        /// </summary>
        /// <param name="pevent">The pevent<see cref="PaintEventArgs"/></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            double hoverAnimProgress = _hoverAnimationManager.GetProgress();
            double focusAnimProgress = _focusAnimationManager.GetProgress();

            g.Clear(Parent.BackColor == Color.Transparent ? ((Parent.Parent == null || (Parent.Parent != null && Parent.Parent.BackColor == Color.Transparent)) ? SkinManager.BackgroundColor : Parent.Parent.BackColor) : Parent.BackColor);

            // button rectand path
            RectangleF buttonRectF = new(ClientRectangle.Location, ClientRectangle.Size);
            buttonRectF.X -= 0.5f;
            buttonRectF.Y -= 0.5f;
            GraphicsPath buttonPath = MaterialDrawHelper.CreateRoundRect(buttonRectF, 4);

            // button shadow (blend with form shadow)
            MaterialDrawHelper.DrawSquareShadow(g, ClientRectangle);

            if (Type == MaterialButtonType.Contained)
            {
                // draw button rect
                // Disabled
                if (!Enabled)
                {
                    using SolidBrush disabledBrush = new(MaterialDrawHelper.BlendColor(Parent.BackColor, SkinManager.BackgroundDisabledColor, SkinManager.BackgroundDisabledColor.A));
                    g.FillPath(disabledBrush, buttonPath);
                }
                // High emphasis
                else if (HighEmphasis)
                {
                    g.FillPath(UseAccentColor ? SkinManager.ColorScheme.AccentBrush : SkinManager.ColorScheme.PrimaryBrush, buttonPath);
                }
                // Mormal
                else
                {
                    using SolidBrush normalBrush = new(SkinManager.BackgroundColor);
                    g.FillPath(normalBrush, buttonPath);
                }
            }
            else
            {
                g.Clear(Parent.BackColor);
            }

            //Hover
            if (hoverAnimProgress > 0)
            {
                using SolidBrush hoverBrush = new(Color.FromArgb(
                    (int)(HighEmphasis && Type == MaterialButtonType.Contained ? hoverAnimProgress * 80 : hoverAnimProgress * SkinManager.BackgroundHoverColor.A), (UseAccentColor ? (HighEmphasis && Type == MaterialButtonType.Contained ?
                    SkinManager.ColorScheme.AccentColor.Lighten(0.5f) : // Contained with Emphasis - with accent
                    SkinManager.ColorScheme.AccentColor) : // Not Contained Or Low Emphasis - with accent
                    (Type == MaterialButtonType.Contained && HighEmphasis ? SkinManager.ColorScheme.LightPrimaryColor : // Contained with Emphasis without accent
                    SkinManager.ColorScheme.PrimaryColor)).RemoveAlpha())); // Normal or Emphasis without accent
                g.FillPath(hoverBrush, buttonPath);
            }

            //Focus
            if (focusAnimProgress > 0)
            {
                using SolidBrush focusBrush = new(Color.FromArgb(
                    (int)(HighEmphasis && Type == MaterialButtonType.Contained ? focusAnimProgress * 80 : focusAnimProgress * SkinManager.BackgroundFocusColor.A), (UseAccentColor ? (HighEmphasis && Type == MaterialButtonType.Contained ?
                    SkinManager.ColorScheme.AccentColor.Lighten(0.5f) : // Contained with Emphasis - with accent
                    SkinManager.ColorScheme.AccentColor) : // Not Contained Or Low Emphasis - with accent
                    (Type == MaterialButtonType.Contained && HighEmphasis ? SkinManager.ColorScheme.LightPrimaryColor : // Contained with Emphasis without accent
                    SkinManager.ColorScheme.PrimaryColor)).RemoveAlpha())); // Normal or Emphasis without accent
                g.FillPath(focusBrush, buttonPath);
            }

            if (Type == MaterialButtonType.Outlined)
            {
                using Pen outlinePen = new(Enabled ? SkinManager.DividersAlternativeColor : SkinManager.DividersColor, 1);
                buttonRectF.X += 0.5f;
                buttonRectF.Y += 0.5f;
                g.DrawPath(outlinePen, buttonPath);
            }

            //Ripple
            if (_animationManager.IsAnimating())
            {
                g.Clip = new Region(buttonRectF);
                for (int i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    double animationValue = _animationManager.GetProgress(i);
                    Point animationSource = _animationManager.GetSource(i);

                    using Brush rippleBrush = new SolidBrush(
                        Color.FromArgb((int)(100 - (animationValue * 100)), // Alpha animation
                        Type == MaterialButtonType.Contained && HighEmphasis ? (UseAccentColor ?
                            SkinManager.ColorScheme.AccentColor.Lighten(0.5f) : // Emphasis with accent
                            SkinManager.ColorScheme.LightPrimaryColor) : // Emphasis
                            (UseAccentColor ? SkinManager.ColorScheme.AccentColor : // Normal with accent
                            SkinManager.Theme == MaterialSkinManager.Themes.LIGHT ? SkinManager.ColorScheme.PrimaryColor : SkinManager.ColorScheme.LightPrimaryColor))); // Normal
                    int rippleSize = (int)(animationValue * Width * 2);
                    g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - (rippleSize / 2), animationSource.Y - (rippleSize / 2), rippleSize, rippleSize));
                }
                g.ResetClip();
            }

            //Text
            Rectangle textRect = ClientRectangle;
            if (Icon != null)
            {
                textRect.Width -= 8 + ICON_SIZE + 4 + 8; // left padding + icon width + space between Icon and Text + right padding
                textRect.X += 8 + ICON_SIZE + 4; // left padding + icon width + space between Icon and Text
            }

            Color textColor = Enabled ? (HighEmphasis ? (Type is MaterialButtonType.Text or MaterialButtonType.Outlined) ?
                UseAccentColor ? SkinManager.ColorScheme.AccentColor : // Outline or Text and accent and emphasis
                NoAccentTextColor == Color.Empty ?
                SkinManager.ColorScheme.PrimaryColor :  // Outline or Text and emphasis
                NoAccentTextColor : // User defined Outline or Text and emphasis
                SkinManager.ColorScheme.TextColor : // Contained and Emphasis
                SkinManager.TextHighEmphasisColor) : // Cointained and accent
                SkinManager.TextDisabledOrHintColor; // Disabled

            using (MaterialNativeTextRenderer NativeText = new(g))
            {
                NativeText.DrawMultilineTransparentText(
                    CharacterCasing == CharacterCasingEnum.Upper ? base.Text.ToUpper() : CharacterCasing == CharacterCasingEnum.Lower ? base.Text.ToLower() :
                        CharacterCasing == CharacterCasingEnum.Title ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(base.Text.ToLower()) : base.Text,
                    SkinManager.GetLogFontByType(MaterialSkinManager.FontType.Button),
                    textColor,
                    textRect.Location,
                    textRect.Size,
                    MaterialNativeTextRenderer.TextAlignFlags.Center | MaterialNativeTextRenderer.TextAlignFlags.Middle);
            }

            //Icon
            Rectangle iconRect = new(8, (Height / 2) - (ICON_SIZE / 2), ICON_SIZE, ICON_SIZE);

            if (string.IsNullOrEmpty(Text))
            {
                // Center Icon
                iconRect.X += 2;
            }

            if (Icon != null)
            {
                if (IconType == MaterialIconType.Rebase)
                {
                    g.FillRectangle(iconsBrushes, iconRect);
                }
                else
                {
                    g.DrawImage(Icon, iconRect);
                }
            }
        }

        /// <summary>
        /// The GetPreferredSize
        /// </summary>
        /// <returns>The <see cref="Size"/></returns>
        private Size GetPreferredSize()
        {
            return GetPreferredSize(Size);
        }

        /// <summary>
        /// The GetPreferredSize
        /// </summary>
        /// <param name="proposedSize">The proposedSize<see cref="Size"/></param>
        /// <returns>The <see cref="Size"/></returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            Size s = base.GetPreferredSize(proposedSize);

            // Provides extra space for proper padding for content
            int extra = 16;

            if (Icon != null)
            {
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += ICON_SIZE + 4;
            }

            if (AutoSize)
            {
                s.Width = (int)Math.Ceiling(_textSize.Width);
                s.Width += extra;
                s.Height = HEIGHTDEFAULT;
            }
            else
            {
                s.Width += extra;
                s.Height = HEIGHTDEFAULT;
            }
            if (Icon != null && Text.Length == 0 && s.Width < MINIMUMWIDTHICONONLY)
            {
                s.Width = MINIMUMWIDTHICONONLY;
            }
            else if (s.Width < MINIMUMWIDTH)
            {
                s.Width = MINIMUMWIDTH;
            }

            return s;
        }

        /// <summary>
        /// The OnCreateControl
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            // before checking DesignMode property, as long as we need see Icon in proper position
            Resize += (sender, args) => { preProcessIcons(); Invalidate(); };

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
                _focusAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            LostFocus += (sender, args) =>
            {
                MouseState = MaterialMouseState.OUT;
                _focusAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };

            PreviewKeyDown += (object sender, PreviewKeyDownEventArgs e) =>
            {
                if (e.KeyCode is Keys.Enter or Keys.Space)
                {
                    _animationManager.StartNewAnimation(AnimationDirection.In, new Point(ClientRectangle.Width >> 1, ClientRectangle.Height >> 1));
                    Invalidate();
                }
            };
        }
    }

    #endregion
}