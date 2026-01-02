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
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the image")]
        public Image Image
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Is the image paralax zoom")]
        public bool IsParallax
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Enable filters")]
        public bool FilterEnabled
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter color left")]
        public Color ColorLeft
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter color right")]
        public Color ColorRight
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DodgerBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Filter alpha")]
        public int FilterAlpha
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 200;

        [Category("Parrot")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.AntiAlias;

        [Category("Parrot")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = PixelOffsetMode.HighQuality;

        [Category("Parrot")]
        [Browsable(true)]
        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

        [Category("Parrot")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

        [Category("Parrot")]
        [Browsable(true)]
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

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
            if (Image != null)
            {
                if (!IsParallax)
                {
                    if (IsElipse)
                    {
                        Brush brush = new TextureBrush(new Bitmap(Image, base.Width, base.Height), new Rectangle(0, 0, base.Width, base.Height));
                        e.Graphics.FillEllipse(brush, 0, 0, base.Width, base.Height);
                        if (FilterEnabled)
                        {
                            Brush brush2 = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(FilterAlpha, ColorRight), Color.FromArgb(FilterAlpha, ColorLeft), 180f);
                            e.Graphics.FillEllipse(brush2, 0, 0, base.Width, base.Height);
                            return;
                        }
                    }
                    else
                    {
                        e.Graphics.DrawImage(new Bitmap(Image, base.Width, base.Height), 0, 0);
                        if (FilterEnabled)
                        {
                            Brush brush3 = new LinearGradientBrush(base.ClientRectangle, Color.FromArgb(FilterAlpha, ColorRight), Color.FromArgb(FilterAlpha, ColorLeft), 180f);
                            e.Graphics.FillRectangle(brush3, 0, 0, base.Width, base.Height);
                            return;
                        }
                    }
                }
                else if (IsParallax)
                {
                    try
                    {
                        bufferedGraphics.Graphics.DrawImage(new Bitmap(Image, base.Width * 2, base.Height * 2), x, y);
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
                bufferedGraphics.Graphics.DrawImage(new Bitmap(Image, base.Width * 2, base.Height * 2), x, y);
                bufferedGraphics.Render(base.CreateGraphics());
            }
            catch
            {
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsParallax)
            {
                x = e.X - base.Width;
                y = e.Y - base.Height;
                Invalidate();
            }
        }

        private int x;

        private int y;

        private BufferedGraphics bufferedGraphics;
    }

    #endregion
}