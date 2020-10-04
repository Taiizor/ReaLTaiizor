#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Util;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using System.ComponentModel;
using ReaLTaiizor.Enum.Poison;
using ReaLTaiizor.Design.Poison;
using ReaLTaiizor.Drawing.Poison;
using ReaLTaiizor.Extension.Poison;
using ReaLTaiizor.Interface.Poison;

#endregion

namespace ReaLTaiizor.Controls
{
    #region PoisonLinkLabel

    [Designer(typeof(PoisonLinkLabelDesigner))]
    [ToolboxBitmap(typeof(LinkLabel))]
    [DefaultEvent("Click")]
    public class PoisonLinkLabel : System.Windows.Forms.Button, IPoisonControl
    {
        #region Interface

        private bool displayFocusRectangle = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool DisplayFocus
        {
            get { return displayFocusRectangle; }
            set { displayFocusRectangle = value; }
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
                CustomPaintBackground(this, e);
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
                CustomPaint(this, e);
        }

        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public event EventHandler<PoisonPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(PoisonPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
                CustomPaintForeground(this, e);
        }

        private ColorStyle poisonStyle = ColorStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ColorStyle.Default)]
        public ColorStyle Style
        {
            get
            {
                if (DesignMode || poisonStyle != ColorStyle.Default)
                    return poisonStyle;

                if (StyleManager != null && poisonStyle == ColorStyle.Default)
                    return StyleManager.Style;
                if (StyleManager == null && poisonStyle == ColorStyle.Default)
                    return PoisonDefaults.Style;

                return poisonStyle;
            }
            set
            {
                poisonStyle = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        private ThemeStyle poisonTheme = ThemeStyle.Default;
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        [DefaultValue(ThemeStyle.Default)]
        public ThemeStyle Theme
        {
            get
            {
                if (DesignMode || poisonTheme != ThemeStyle.Default)
                    return poisonTheme;

                if (StyleManager != null && poisonTheme == ThemeStyle.Default)
                    return StyleManager.Theme;
                if (StyleManager == null && poisonTheme == ThemeStyle.Default)
                    return PoisonDefaults.Theme;

                return poisonTheme;
            }
            set
            {
                poisonTheme = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        private PoisonStyleManager poisonStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoisonStyleManager StyleManager
        {
            get { return poisonStyleManager; }
            set { poisonStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set
            {
                useStyleColors = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [Browsable(false)]
        [Category(PoisonDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        private Image _image = null;
        [DefaultValue(null)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public virtual new Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                createimages();
            }
        }


        private Image _nofocus = null;
        [DefaultValue(null)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public Image NoFocusImage
        {
            get { return _nofocus; }
            set { _nofocus = value; }
        }


        Int32 _imagesize = 16;

        [DefaultValue(16)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public Int32 ImageSize
        {
            get { return _imagesize; }
            set
            {
                _imagesize = value;
                Invalidate();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                if (AutoSize && _image != null)
                {
                    base.Width = TextRenderer.MeasureText(value, PoisonFonts.LinkLabel(poisonLinkSize, poisonLinkWeight)).Width;
                    base.Width += _imagesize + 2;
                }
            }
        }

        #endregion

        #region Fields

        private PoisonLinkLabelSize poisonLinkSize = PoisonLinkLabelSize.Small;
        [DefaultValue(PoisonLinkLabelSize.Small)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonLinkLabelSize FontSize
        {
            get { return poisonLinkSize; }
            set
            {
                poisonLinkSize = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        private PoisonLinkLabelWeight poisonLinkWeight = PoisonLinkLabelWeight.Bold;
        [DefaultValue(PoisonLinkLabelWeight.Bold)]
        [Category(PoisonDefaults.PropertyCategory.Appearance)]
        public PoisonLinkLabelWeight FontWeight
        {
            get { return poisonLinkWeight; }
            set
            {
                poisonLinkWeight = value;
                if (DesignMode)
                    Invalidate();
            }
        }

        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public PoisonLinkLabel()
        {
            SetStyle
            (
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                     true
            );
        }
        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!useCustomBackColor)
                    backColor = PoisonPaint.BackColor.Form(Theme);

                if (backColor.A == 255 && BackgroundImage == null)
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
                    OnPaintBackground(e);

                OnCustomPaint(new PoisonPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        Color foreColor;

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            if (useCustomForeColor)
                foreColor = ForeColor;
            else
            {
                if (isHovered && !isPressed && Enabled)
                {
                    //foreColor = PoisonPaint.ForeColor.Link.Hover(Theme);
                    foreColor = PoisonPaint.ForeColor.Link.Normal(Theme);
                }
                else if (isHovered && isPressed && Enabled)
                    foreColor = PoisonPaint.ForeColor.Link.Press(Theme);
                else if (!Enabled)
                    foreColor = PoisonPaint.ForeColor.Link.Disabled(Theme);
                else
                    foreColor = !useStyleColors ? PoisonPaint.ForeColor.Link.Hover(Theme) : PoisonPaint.GetStyleColor(Style);
            }

            TextRenderer.DrawText(e.Graphics, Text, PoisonFonts.LinkLabel(poisonLinkSize, poisonLinkWeight), ClientRectangle, foreColor, PoisonPaint.GetTextFormatFlags(TextAlign, !this.AutoSize));

            OnCustomPaintForeground(new PoisonPaintEventArgs(Color.Empty, foreColor, e.Graphics));

            if (displayFocusRectangle && isFocused)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);

            if (_image != null) DrawIcon(e.Graphics);
        }

        private void DrawIcon(Graphics g)
        {
            if (Image != null)
            {
                int _imgW = _imagesize;
                int _imgH = _imagesize;

                if (_imagesize == 0)
                {
                    _imgW = _image.Width;
                    _imgH = _image.Height;
                }

                Point iconLocation = new Point(2, (ClientRectangle.Height - _imagesize) / 2);
                int _filler = 0;

                switch (ImageAlign)
                {
                    case ContentAlignment.BottomCenter:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) / 2, (ClientRectangle.Height - _imgH) - _filler);
                        break;
                    case ContentAlignment.BottomLeft:
                        iconLocation = new Point(_filler, (ClientRectangle.Height - _imgH) - _filler);
                        break;
                    case ContentAlignment.BottomRight:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) - _filler, (ClientRectangle.Height - _imgH) - _filler);
                        break;
                    case ContentAlignment.MiddleCenter:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) / 2, (ClientRectangle.Height - _imgH) / 2);
                        break;
                    case ContentAlignment.MiddleLeft:
                        iconLocation = new Point(_filler, (ClientRectangle.Height - _imgH) / 2);
                        break;
                    case ContentAlignment.MiddleRight:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) - _filler, (ClientRectangle.Height - _imgH) / 2);
                        break;
                    case ContentAlignment.TopCenter:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) / 2, _filler);
                        break;
                    case ContentAlignment.TopLeft:
                        iconLocation = new Point(_filler, _filler);
                        break;
                    case ContentAlignment.TopRight:
                        iconLocation = new Point((ClientRectangle.Width - _imgW) - _filler, _filler);
                        break;
                }

                iconLocation.Y += 1;

                if (_nofocus == null)
                {
                    if (Theme == ThemeStyle.Dark)
                        g.DrawImage((isHovered && !isPressed) ? _darkimg : _darklightimg, new Rectangle(iconLocation, new Size(_imgW, _imgH)));
                    else
                        g.DrawImage((isHovered && !isPressed) ? _lightimg : _lightlightimg, new Rectangle(iconLocation, new Size(_imgW, _imgH)));
                }
                else
                {
                    if (Theme == ThemeStyle.Dark)
                        g.DrawImage((isHovered && !isPressed) ? _darkimg : _nofocus, new Rectangle(iconLocation, new Size(_imgW, _imgH)));
                    else
                        g.DrawImage((isHovered && !isPressed) ? _image : _nofocus, new Rectangle(iconLocation, new Size(_imgW, _imgH)));
                }
            }
        }

        Image _lightlightimg = null;
        Image _darklightimg = null;
        Image _lightimg = null;
        Image _darkimg = null;

        private void createimages()
        {
            if (_image != null)
            {
                _lightimg = _image;
                _darkimg = ApplyInvert(new Bitmap(_image));

                _darklightimg = ApplyLight(new Bitmap(_darkimg));
                _lightlightimg = ApplyLight(new Bitmap(_lightimg));
            }
        }
        public Bitmap ApplyInvert(Bitmap bitmapImage)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

            return bitmapImage;
        }

        public Bitmap ApplyLight(Bitmap bitmapImage)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);

                    A = pixelColor.A;
                    if (pixelColor.A <= 255 && pixelColor.A >= 100)
                        A = 90;

                    R = (byte)(pixelColor.R);
                    G = (byte)(pixelColor.G);
                    B = (byte)(pixelColor.B);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

            return bitmapImage;
        }
        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            // isHovered = true;
            isPressed = false;
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
            //isHovered = true;
            isPressed = true;
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
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!isFocused)
            {
                isHovered = false;
                isPressed = false;
            }
            Invalidate();

            base.OnKeyUp(e);
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

                if (Name == "lnkClear" && Parent.GetType().Name == "PoisonTextBox") this.PerformClick();
                if (Name == "lnkClear" && Parent.GetType().Name == "SearchControl") this.PerformClick();
            }

            base.OnMouseDown(e);
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
            isPressed = false;

            Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion

        #region Overridden Methods

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        #endregion
    }

    #endregion
}