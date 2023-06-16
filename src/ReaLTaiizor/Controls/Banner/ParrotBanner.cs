#region Imports

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotBanner

    public class ParrotBanner : Control
    {
        public ParrotBanner()
        {
            Size = new Size(100, 20);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Text = Name;
            ForeColor = Color.White;
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The banner border color")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the banner")]
        public Color BannerColor
        {
            get => bannerColor;
            set
            {
                bannerColor = value;
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
            List<Point> list = new();
            List<PointF> list2 = new();
            list.Add(new Point(0, base.Height / 10 * 5));
            list.Add(new Point(base.Height / 10, base.Height / 10 * 4));
            list.Add(new Point(base.Height / 10 * 2, base.Height / 10 * 3));
            list.Add(new Point(base.Height / 10 * 3, base.Height / 10 * 2));
            list.Add(new Point(base.Height / 10 * 4, base.Height / 10));
            list.Add(new Point(base.Height / 10 * 5, 0));
            list.Add(new Point(base.Width - (base.Height / 10 * 5), 0));
            list.Add(new Point(base.Width - (base.Height / 10 * 4), base.Height / 10));
            list.Add(new Point(base.Width - (base.Height / 10 * 3), base.Height / 10 * 2));
            list.Add(new Point(base.Width - (base.Height / 10 * 2), base.Height / 10 * 3));
            list.Add(new Point(base.Width - (base.Height / 10), base.Height / 10 * 4));
            list.Add(new Point(base.Width, base.Height / 10 * 5));
            list.Add(new Point(base.Width - (base.Height / 10), base.Height / 10 * 6));
            list.Add(new Point(base.Width - (base.Height / 10 * 2), base.Height / 10 * 7));
            list.Add(new Point(base.Width - (base.Height / 10 * 3), base.Height / 10 * 8));
            list.Add(new Point(base.Width - (base.Height / 10 * 4), base.Height / 10 * 9));
            list.Add(new Point(base.Width - (base.Height / 10 * 5), base.Height / 10 * 10));
            list.Add(new Point(base.Height / 10 * 5, base.Height / 10 * 10));
            list.Add(new Point(base.Height / 10 * 4, base.Height / 10 * 9));
            list.Add(new Point(base.Height / 10 * 3, base.Height / 10 * 8));
            list.Add(new Point(base.Height / 10 * 2, base.Height / 10 * 7));
            list.Add(new Point(base.Height / 10, base.Height / 10 * 6));
            list.Add(new Point(0, base.Height / 10 * 5));
            SolidBrush brush = new(bannerColor);
            e.Graphics.FillPolygon(brush, list.ToArray());
            list2.Add(new Point(0, base.Height / 10 * 5));
            list2.Add(new Point(base.Height / 10, base.Height / 10 * 4));
            list2.Add(new Point(base.Height / 10 * 2, base.Height / 10 * 3));
            list2.Add(new Point(base.Height / 10 * 3, base.Height / 10 * 2));
            list2.Add(new Point(base.Height / 10 * 4, base.Height / 10));
            list2.Add(new Point(base.Height / 10 * 5, 0));
            list2.Add(new Point(base.Width - (base.Height / 10 * 5) - 1, 0));
            list2.Add(new Point(base.Width - (base.Height / 10 * 4) - 1, base.Height / 10));
            list2.Add(new Point(base.Width - (base.Height / 10 * 3) - 1, base.Height / 10 * 2));
            list2.Add(new Point(base.Width - (base.Height / 10 * 2) - 1, base.Height / 10 * 3));
            list2.Add(new Point(base.Width - (base.Height / 10) - 1, base.Height / 10 * 4));
            list2.Add(new Point(base.Width - 1, base.Height / 10 * 5));
            list2.Add(new Point(base.Width - (base.Height / 10) - 1, base.Height / 10 * 6));
            list2.Add(new Point(base.Width - (base.Height / 10 * 2) - 1, base.Height / 10 * 7));
            list2.Add(new Point(base.Width - (base.Height / 10 * 3) - 1, base.Height / 10 * 8));
            list2.Add(new Point(base.Width - (base.Height / 10 * 4) - 1, base.Height / 10 * 9));
            list2.Add(new Point(base.Width - (base.Height / 10 * 5), (base.Height / 10 * 10) - 1));
            list2.Add(new Point((base.Height / 10 * 5) - 1, (base.Height / 10 * 10) - 1));
            list2.Add(new Point(base.Height / 10 * 4, base.Height / 10 * 9));
            list2.Add(new Point(base.Height / 10 * 3, base.Height / 10 * 8));
            list2.Add(new Point(base.Height / 10 * 2, base.Height / 10 * 7));
            list2.Add(new Point(base.Height / 10, base.Height / 10 * 6));
            list2.Add(new Point(0, base.Height / 10 * 5));
            Pen pen = new(borderColor, 1f);
            e.Graphics.DrawPolygon(pen, list2.ToArray());
            StringFormat stringFormat = new()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            SolidBrush brush2 = new(ForeColor);
            RectangleF layoutRectangle = new(0f, 0f, Width, Height);
            e.Graphics.PixelOffsetMode = PixelOffsetType;
            e.Graphics.TextRenderingHint = TextRenderingType;
            e.Graphics.DrawString(Text, Font, brush2, layoutRectangle, stringFormat);
            base.OnPaint(e);
        }

        private Color borderColor = Color.White;

        private Color bannerColor = Color.FromArgb(230, 71, 89);
    }

    #endregion
}