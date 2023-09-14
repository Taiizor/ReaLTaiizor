#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotPictureBox

    public class ParrotPictureBox : Control
    {
        public ParrotPictureBox()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.Size = new Size(150, 150);
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            x = 0 - (base.Width / 2);
            y = 0 - (base.Height / 2);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the image eliptical")]
        public bool IsElipse
        {
            get => isElipse;
            set
            {
                isElipse = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the image")]
        public Image Image
        {
            get => image;
            set
            {
                image = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the image paralax zoom")]
        public bool IsParallax
        {
            get => isParallax;
            set
            {
                isParallax = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Enable filters")]
        public bool FilterEnabled
        {
            get => filterEnabled;
            set
            {
                filterEnabled = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter color left")]
        public Color ColorLeft
        {
            get => colorLeft;
            set
            {
                colorLeft = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter color right")]
        public Color ColorRight
        {
            get => colorRight;
            set
            {
                colorRight = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter alpha")]
        public int FilterAlpha
        {
            get => filterAlpha;
            set
            {
                filterAlpha = value;
                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get => _CompositingQualityType;
            set
            {
                _CompositingQualityType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        private InterpolationMode _InterpolationType = InterpolationMode.HighQualityBilinear;
        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get => _InterpolationType;
            set
            {
                _InterpolationType = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width, base.Height);
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);
            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;
            bufferedGraphics.Graphics.TextRenderingHint = TextRenderingType;
            bufferedGraphics.Graphics.Clear(BackColor);
            e.Graphics.SmoothingMode = SmoothingType;
            e.Graphics.InterpolationMode = InterpolationType;
            e.Graphics.CompositingQuality = CompositingQualityType;
            e.Graphics.PixelOffsetMode = PixelOffsetType;
            if (image != null)
            {
                if (!isParallax)
                {
                    if (isElipse)
                    {
                        Brush brush = new TextureBrush(new Bitmap(image, base.Width, base.Height), new Rectangle(0, 0, base.Width, base.Height));
                        e.Graphics.FillEllipse(brush, 0, 0, base.Width, base.Height);
                        if (filterEnabled)
                        {
                            Brush brush2 = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(filterAlpha, colorRight), Color.FromArgb(filterAlpha, colorLeft), 180f);
                            e.Graphics.FillEllipse(brush2, 0, 0, base.Width, base.Height);
                            return;
                        }
                    }
                    else
                    {
                        e.Graphics.DrawImage(new Bitmap(image, base.Width, base.Height), 0, 0);
                        if (filterEnabled)
                        {
                            Brush brush3 = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(filterAlpha, colorRight), Color.FromArgb(filterAlpha, colorLeft), 180f);
                            e.Graphics.FillRectangle(brush3, 0, 0, base.Width, base.Height);
                            return;
                        }
                    }
                }
                else if (isParallax)
                {
                    try
                    {
                        bufferedGraphics.Graphics.DrawImage(new Bitmap(image, base.Width * 2, base.Height * 2), x, y);
                        bufferedGraphics.Render(e.Graphics);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void updateParallax()
        {
            try
            {
                bufferedGraphics.Graphics.Clear(BackColor);
                bufferedGraphics.Graphics.DrawImage(new Bitmap(image, base.Width * 2, base.Height * 2), x, y);
                bufferedGraphics.Render(base.CreateGraphics());
            }
            catch
            {
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isParallax)
            {
                x = e.X - base.Width;
                y = e.Y - base.Height;
                Invalidate();
            }
        }

        private int x;

        private int y;

        private BufferedGraphics bufferedGraphics;

        private bool isElipse;

        private Image image;

        private bool isParallax;

        private bool filterEnabled = true;

        private Color colorLeft = Color.DodgerBlue;

        private Color colorRight = Color.DodgerBlue;

        private int filterAlpha = 200;
    }

    #endregion
}