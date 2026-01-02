#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotCard

    public class ParrotCard : Control
    {
        public ParrotCard()
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            Size = new Size(320, 170);
            BackColor = Color.Transparent;
            ForeColor = Color.White;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 1st half color of he gradient")]
        public Color Color1
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
        [Description("The 2nd half color of he gradient")]
        public Color Color2
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LimeGreen;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 1st text")]
        public string Text1
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "Credit Card";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 2nd text")]
        public string Text2
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "1357 2468 9013 5724";

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 3rd text")]
        public string Text3
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "Exp: 01/02 - 03/04";

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

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Brush brush = new LinearGradientBrush(ClientRectangle, Color1, Color2, 135f);

            using (GraphicsPath graphicsPath = new())
            {
                graphicsPath.AddArc(Width - 10 - 2, 0, 10, 10, 250f, 90f);
                graphicsPath.AddArc(Width - 10 - 2, Height - 10, 10, 8, 0f, 90f);
                graphicsPath.AddArc(0, Height - 10 - 2, 8, 10, 90f, 90f);
                graphicsPath.AddArc(0, 0, 10, 10, 180f, 90f);
                graphicsPath.CloseFigure();
                e.Graphics.FillPath(brush, graphicsPath);
            }

            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            };

            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;

            Rectangle r = new(2, 6, Width - 4, 26);

            e.Graphics.DrawString(Text1, new Font(Font.FontFamily, Font.Size + 4f), new SolidBrush(ForeColor), r, stringFormat);

            stringFormat.Alignment = StringAlignment.Near;

            r = new Rectangle(2, Height / 2, Width - 4, Height / 4);
            e.Graphics.DrawString(Text2, new Font(Font.FontFamily, (Font.Size * 2f) + 2f), new SolidBrush(ForeColor), r, stringFormat);

            stringFormat.Alignment = StringAlignment.Near;

            r = new Rectangle(2, (Height / 2) + (Height / 4), Width - 4, Height / 4);
            e.Graphics.DrawString(Text3, new Font(Font.FontFamily, Font.Size + 2f), new SolidBrush(ForeColor), r, stringFormat);
        }
    }

    #endregion
}