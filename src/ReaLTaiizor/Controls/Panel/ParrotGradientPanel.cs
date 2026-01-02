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
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top left color")]
        public Color TopLeft
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.DeepSkyBlue;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The top right color")]
        public Color TopRight
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Fuchsia;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom left color")]
        public Color BottomLeft
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Black;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The bottom right color")]
        public Color BottomRight
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Fuchsia;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The gradient orientation")]
        public GradientStyle Style
        {
            get;
            set
            {
                field = value;
                Refresh();
            }
        } = GradientStyle.Corners;

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
        public InterpolationMode InterpolationType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = InterpolationMode.HighQualityBilinear;

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
        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.ClearTypeGridFit;

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
            bufferedGraphics.Graphics.Clear(PrimerColor);
            if (Style == GradientStyle.Corners)
            {
                LinearGradientBrush linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), TopLeft, Color.Transparent, 45f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), TopRight, Color.Transparent, 135f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), BottomRight, Color.Transparent, 225f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush = new(new Rectangle(0, 0, base.Width, base.Height), BottomLeft, Color.Transparent, 315f);
                bufferedGraphics.Graphics.FillRectangle(linearGradientBrush, base.ClientRectangle);
                linearGradientBrush.Dispose();
            }
            else
            {
                Brush brush;
                if (Style == GradientStyle.Vertical)
                {
                    brush = new LinearGradientBrush(base.ClientRectangle, TopLeft, TopRight, 720f);
                }
                else
                {
                    brush = new LinearGradientBrush(base.ClientRectangle, TopLeft, TopRight, 90f);
                }
                bufferedGraphics.Graphics.FillRectangle(brush, base.ClientRectangle);
                brush.Dispose();
            }
            bufferedGraphics.Render(e.Graphics);
        }

        private BufferedGraphics bufferedGraphics;

        public enum GradientStyle
        {
            Horizontal,
            Vertical,
            Corners
        }
    }

    #endregion
}