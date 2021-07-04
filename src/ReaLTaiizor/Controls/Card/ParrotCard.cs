﻿#region Imports

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
            base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            base.Size = new Size(320, 170);
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
                base.Invalidate();
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
                base.Invalidate();
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
                base.Invalidate();
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
                base.Invalidate();
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
                base.Invalidate();
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
            if (base.Width > base.Height)
            {
                int width = base.Width;
            }
            else
            {
                int height = base.Height;
            }
            Brush brush = new LinearGradientBrush(base.ClientRectangle, color1, color2, 135f);
            using (GraphicsPath graphicsPath = new())
            {
                graphicsPath.AddArc(base.Width - 10 - 2, 0, 10, 10, 250f, 90f);
                graphicsPath.AddArc(base.Width - 10 - 2, base.Height - 10, 10, 8, 0f, 90f);
                graphicsPath.AddArc(0, base.Height - 10 - 2, 8, 10, 90f, 90f);
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
            Rectangle r = new Rectangle(2, 6, base.Width - 4, 26);
            e.Graphics.DrawString(text1, new Font(Font.FontFamily, Font.Size + 4f), new SolidBrush(ForeColor), r, stringFormat);
            stringFormat.Alignment = StringAlignment.Near;
            r = new Rectangle(2, base.Height / 2, base.Width - 4, base.Height / 4);
            e.Graphics.DrawString(text2, new Font(Font.FontFamily, Font.Size * 2f + 2f), new SolidBrush(ForeColor), r, stringFormat);
            stringFormat.Alignment = StringAlignment.Near;
            r = new Rectangle(2, base.Height / 2 + base.Height / 4, base.Width - 4, base.Height / 4);
            e.Graphics.DrawString(text3, new Font(Font.FontFamily, Font.Size + 2f), new SolidBrush(ForeColor), r, stringFormat);
        }

        private Color color1 = Color.DodgerBlue;

        private Color color2 = Color.LimeGreen;

        private string text1 = "Savings Card";

        private string text2 = "1234 5678 9101 1121";

        private string text3 = "Exp: 01/02 - 03/04";
    }

    #endregion
}