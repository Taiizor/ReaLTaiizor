#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RibbonGroupBox

    public class RibbonGroupBox : ContainerControl
    {

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        public CompositingQuality CompositingQualityType
        {
            get => _CompositingQualityType;
            set
            {
                _CompositingQualityType = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.Transparent;
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _LineColorA = Color.FromArgb(126, 126, 126);
        public Color LineColorA
        {
            get => _LineColorA;
            set
            {
                _LineColorA = value;
                Invalidate();
            }
        }

        private Color _LineColorB = Color.FromArgb(126, 126, 126);
        public Color LineColorB
        {
            get => _LineColorB;
            set
            {
                _LineColorB = value;
                Invalidate();
            }
        }

        private Color _BorderColorA = Color.FromArgb(143, 143, 143);
        public Color BorderColorA
        {
            get => _BorderColorA;
            set
            {
                _BorderColorA = value;
                Invalidate();
            }
        }

        private Color _BorderColorB = Color.FromArgb(174, 178, 172);
        public Color BorderColorB
        {
            get => _BorderColorB;
            set
            {
                _BorderColorB = value;
                Invalidate();
            }
        }

        private Color _BorderColorC = Color.FromArgb(194, 192, 200);
        public Color BorderColorC
        {
            get => _BorderColorC;
            set
            {
                _BorderColorC = value;
                Invalidate();
            }
        }

        public RibbonGroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            ForeColor = Color.Black;
            Font = new("Tahoma", 10, FontStyle.Bold);
            Size = new(170, 90);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle Body = new(0, 0, Width - 1, Height - 1);

            base.OnPaint(e);

            G.Clear(BaseColor);

            G.SmoothingMode = SmoothingType;
            G.CompositingQuality = CompositingQualityType;

            ImageToCodeClassRibbon ICT = new();
            Image GROUPHATCH = ICT.CodeToImage("/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAIBAQIBAQICAgICAgICAwUDAwMDAwYEBAMFBwYHBwcGBwcICQsJCAgKCAcHCg0KCgsMDAwMBwkODw0MDgsMDAz/2wBDAQICAgMDAwYDAwYMCAcIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAz/wAARCAAcABwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD9OYVDLHsRNyYPTvgc/oO9NtljUO2FZQ2OmQPwz9PToOKfFGxKkyuVIzy3U8fXI4+nJpkEOQ6mWUlWOdx+h5656fqeKV+40hViC3AUIpKoQBnkenGR7du3XipF2R8BAQenJHGPaoo0xOql5AxUHGcBuR7+3XHc1LAGt02qGkA7nII9ulDb3JknsMhjkdUIKMCvygZwMgZ5+mf0ptuXlLAGMksSxyTzkdBz7/TipbRitojA8kA+v+elNtJT++YAApNtHfjAob6FJX1GozxTAERgkddxyPXgn0J7elOSd1HzFQT0xuNP4WUgZ4iLdT6mpEiUqGIJLc0k0Ckrn//Z");
            TextureBrush GROUPIMAGE = new(GROUPHATCH, WrapMode.TileFlipXY);

            G.FillRectangle(GROUPIMAGE, Body);
            Pen p = new(new SolidBrush(BorderColorA));
            G.DrawRectangle(p, Body);
            Pen p1 = new(new SolidBrush(BorderColorB));
            G.DrawRectangle(p1, 1, 1, Width - 3, Height - 3);
            Pen p2 = new(new SolidBrush(BorderColorC));
            G.DrawRectangle(p2, 2, 2, Width - 5, Height - 5);

            //G.DrawString(Text, t, New SolidBrush(Color.FromArgb(50, 50, 50)), New Rectangle(-1, 1, Width - 1, 25), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(0, 1, Width - 1, 25), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            Pen UnderlinePen = new(new SolidBrush(LineColorA), 3);
            G.DrawLine(UnderlinePen, 20, 24, Width - 20, 24);
            Pen UnderlinePen2 = new(new SolidBrush(LineColorB), 1);
            G.DrawLine(UnderlinePen2, 20, 27, Width - 20, 27);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}