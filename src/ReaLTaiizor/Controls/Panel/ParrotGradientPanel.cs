#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotGradientPanel

    public class ParrotGradientPanel : System.Windows.Forms.Panel
    {
        public ParrotGradientPanel()
        {
            DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            BackColor = Color.White;
            base.Size = new Size(200, 200);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The primer color")]
        public Color PrimerColor
        {
            get => primerColor;
            set
            {
                primerColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top left color")]
        public Color TopLeft
        {
            get => topLeft;
            set
            {
                topLeft = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top right color")]
        public Color TopRight
        {
            get => topRight;
            set
            {
                topRight = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom left color")]
        public Color BottomLeft
        {
            get => bottomLeft;
            set
            {
                bottomLeft = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom right color")]
        public Color BottomRight
        {
            get => bottomRight;
            set
            {
                bottomRight = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gradient orientation")]
        public GradientStyle Style
        {
            get => style;
            set
            {
                style = value;
                Refresh();
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            BufferedGraphicsContext bufferedGraphicsContext = BufferedGraphicsManager.Current;
            bufferedGraphicsContext.MaximumBuffer = new Size(base.Width + 1, base.Height + 1);
            bufferedGraphics = bufferedGraphicsContext.Allocate(base.CreateGraphics(), base.ClientRectangle);
            bufferedGraphics.Graphics.SmoothingMode = SmoothingType;
            bufferedGraphics.Graphics.InterpolationMode = InterpolationType;
            bufferedGraphics.Graphics.CompositingQuality = CompositingQualityType;
            bufferedGraphics.Graphics.PixelOffsetMode = PixelOffsetType;
            bufferedGraphics.Graphics.TextRenderingHint = TextRenderingType;
            bufferedGraphics.Graphics.Clear(primerColor);
            if (style == GradientStyle.Corners)
            {
                LinearGradientBrush linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), TopLeft, Color.Transparent, 45f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), topRight, Color.Transparent, 135f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), bottomRight, Color.Transparent, 225f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), bottomLeft, Color.Transparent, 315f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush.Dispose();
            }
            else
            {
                Brush brush;
                if (style == GradientStyle.Vertical)
                {
                    brush = new LinearGradientBrush(base.ClientRectangle, topLeft, topRight, 720f);
                }
                else
                {
                    brush = new LinearGradientBrush(base.ClientRectangle, topLeft, topRight, 90f);
                }
                bufferedGraphics.Graphics.FillRectangle(brush, base.ClientRectangle);
                brush.Dispose();
            }
            bufferedGraphics.Render(e.Graphics);
        }

        private BufferedGraphics bufferedGraphics;

        private Color primerColor = Color.White;

        private Color topLeft = Color.DeepSkyBlue;

        private Color topRight = Color.Fuchsia;

        private Color bottomLeft = Color.Black;

        private Color bottomRight = Color.Fuchsia;

        private GradientStyle style = GradientStyle.Corners;

        public enum GradientStyle
        {
            Horizontal,
            Vertical,
            Corners
        }
    }

    #endregion
}