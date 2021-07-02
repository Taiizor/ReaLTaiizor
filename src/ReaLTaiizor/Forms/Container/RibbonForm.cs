#region Imports

using ReaLTaiizor.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region RibbonForm

    public class RibbonForm : ContainerControl
    {
        private Color _BaseColor = Color.Fuchsia;
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
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

        private string _SubTitle;
        public string SubTitle
        {
            get => _SubTitle;
            set
            {
                _SubTitle = value;
                Invalidate();
            }
        }

        private Color _SubTitleColor = Color.WhiteSmoke;
        public Color SubTitleColor
        {
            get => _SubTitleColor;
            set
            {
                _SubTitleColor = value;
                Invalidate();
            }
        }

        private Font _SubTitleFont = new("Tahoma", 10, FontStyle.Bold);
        public Font SubTitleFont
        {
            get => _SubTitleFont;
            set
            {
                _SubTitleFont = value;
                Invalidate();
            }
        }

        private Color _HeaderLineColorA = Color.FromArgb(35, 35, 35);
        public Color HeaderLineColorA
        {
            get => _HeaderLineColorA;
            set
            {
                _HeaderLineColorA = value;
                Invalidate();
            }
        }

        private Color _HeaderLineColorB = Color.FromArgb(50, 50, 50);
        public Color HeaderLineColorB
        {
            get => _HeaderLineColorB;
            set
            {
                _HeaderLineColorB = value;
                Invalidate();
            }
        }

        private Color _HeaderLineColorC = Color.Black;
        public Color HeaderLineColorC
        {
            get => _HeaderLineColorC;
            set
            {
                _HeaderLineColorC = value;
                Invalidate();
            }
        }

        private Color _BottomLineColor = Color.FromArgb(99, 99, 99);
        public Color BottomLineColor
        {
            get => _BottomLineColor;
            set
            {
                _BottomLineColor = value;
                Invalidate();
            }
        }

        private Color _RibbonEdgeColorA = Color.Black;
        public Color RibbonEdgeColorA
        {
            get => _RibbonEdgeColorA;
            set
            {
                _RibbonEdgeColorA = value;
                Invalidate();
            }
        }

        private Color _RibbonEdgeColorB = Color.Black;
        public Color RibbonEdgeColorB
        {
            get => _RibbonEdgeColorB;
            set
            {
                _RibbonEdgeColorB = value;
                Invalidate();
            }
        }

        private Color _RibbonEdgeColorC = Color.Black;
        public Color RibbonEdgeColorC
        {
            get => _RibbonEdgeColorC;
            set
            {
                _RibbonEdgeColorC = value;
                Invalidate();
            }
        }

        private Color _RibbonEdgeColorD = Color.FromArgb(86, 86, 86);
        public Color RibbonEdgeColorD
        {
            get => _RibbonEdgeColorD;
            set
            {
                _RibbonEdgeColorD = value;
                Invalidate();
            }
        }

        private Color _RibbonEdgeColorE = Color.FromArgb(51, 51, 50);
        public Color RibbonEdgeColorE
        {
            get => _RibbonEdgeColorE;
            set
            {
                _RibbonEdgeColorE = value;
                Invalidate();
            }
        }

        private HatchStyle _HatchType = HatchStyle.SmallGrid;
        public HatchStyle HatchType
        {
            get => _HatchType;
            set
            {
                _HatchType = value;
                Invalidate();
            }
        }

        public RibbonForm()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.FromArgb(25, 25, 25);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle MainBox = new(0, 0, Width, 32);
            Rectangle SecondBox = new(0, 33, Width, Height);
            Rectangle BottomBox = new(-3, Height - 25, Width + 5, Height - 25);

            base.OnPaint(e);

            G.Clear(BaseColor);

            G.CompositingQuality = CompositingQualityType;
            ImageToCodeClassRibbon d = new();
            Image HATCHIMAGE = d.CodeToImage("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");
            Image BODYIMAGE = d.CodeToImage("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");
            TextureBrush TEXTUREIMAGE = new(HATCHIMAGE, WrapMode.TileFlipXY);
            TextureBrush BACKIMAGE = new(BODYIMAGE, WrapMode.TileFlipXY);
            G.FillRectangle(TEXTUREIMAGE, MainBox);

            Pen p1 = new(new SolidBrush(HeaderLineColorA));
            G.DrawLine(p1, 0, 31, Width, 31);
            Pen p2 = new(new SolidBrush(HeaderLineColorB));
            G.DrawLine(p2, 0, 30, Width, 30);
            G.DrawLine(new(new SolidBrush(HeaderLineColorC)), 0, 32, Width, 32);

            G.FillRectangle(BACKIMAGE, SecondBox);

            Point[] Pt =
            {
                new Point(10, 0),
                new Point(10, 66),
                new Point(36, 40),
                new Point(62, 66),
                new Point(62, 0)
            };

            Pen penCurrent = new(RibbonEdgeColorC);
            G.DrawPolygon(penCurrent, Pt);
            SolidBrush br = new(RibbonEdgeColorD);
            G.FillPolygon(br, Pt);

            Point[] Pt2 =
            {
                new Point(14, 0),
                new Point(14, 62),
                new Point(36, 40),
                new Point(58, 62),
                new Point(58, 0)
            };

            Pen penCurrent2 = new(RibbonEdgeColorE);
            G.DrawPolygon(penCurrent2, Pt2);
            //HatchBrush br2 = new(HatchType, Color.FromArgb(52, 52, 51), Color.FromArgb(48, 49, 48));
            //G.FillPolygon(br2, Pt2);
            G.FillPolygon(TEXTUREIMAGE, Pt2);

            G.DrawLine(new(new SolidBrush(RibbonEdgeColorA)), 10, 0, 10, 66);
            G.DrawLine(new(new SolidBrush(RibbonEdgeColorB)), 10, 66, 36, 40);

            G.DrawString(SubTitle, SubTitleFont, new SolidBrush(SubTitleColor), 68, 9);

            Rectangle IconRect = new(23, 8, 26, 26);
            G.DrawIcon(FindForm().Icon, IconRect);

            Pen BottomPen = new(new SolidBrush(BottomLineColor), 7);
            G.DrawRectangle(BottomPen, BottomBox);
            Image BOTTOMIMAGE = d.CodeToImage("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");
            TextureBrush BOTTOMTEXTURE = new(BOTTOMIMAGE, WrapMode.TileFlipXY);
            G.FillRectangle(BOTTOMTEXTURE, BottomBox);

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable;
            ParentForm.ShowIcon = false;
            ParentForm.TransparencyKey = Color.Fuchsia;
            Dock = DockStyle.Fill;
        }
    }

    #endregion
}