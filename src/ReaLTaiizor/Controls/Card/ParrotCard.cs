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
            get => color1;
            set
            {
                color1 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 2nd half color of he gradient")]
        public Color Color2
        {
            get => color2;
            set
            {
                color2 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 1st text")]
        public string Text1
        {
            get => text1;
            set
            {
                text1 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 2nd text")]
        public string Text2
        {
            get => text2;
            set
            {
                text2 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The 3rd text")]
        public string Text3
        {
            get => text3;
            set
            {
                text3 = value;
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

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            Brush brush = new LinearGradientBrush(ClientRectangle, color1, color2, 135f);

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

            e.Graphics.DrawString(text1, new Font(Font.FontFamily, Font.Size + 4f), new SolidBrush(ForeColor), r, stringFormat);

            stringFormat.Alignment = StringAlignment.Near;

            r = new Rectangle(2, Height / 2, Width - 4, Height / 4);
            e.Graphics.DrawString(text2, new Font(Font.FontFamily, (Font.Size * 2f) + 2f), new SolidBrush(ForeColor), r, stringFormat);

            stringFormat.Alignment = StringAlignment.Near;

            r = new Rectangle(2, (Height / 2) + (Height / 4), Width - 4, Height / 4);
            e.Graphics.DrawString(text3, new Font(Font.FontFamily, Font.Size + 2f), new SolidBrush(ForeColor), r, stringFormat);
        }

        private Color color1 = Color.DodgerBlue;
        private Color color2 = Color.LimeGreen;

        private string text1 = "Credit Card";
        private string text2 = "1357 2468 9013 5724";
        private string text3 = "Exp: 01/02 - 03/04";
    }

    #endregion
}