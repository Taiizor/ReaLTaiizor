#region Imports

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyComboBox

    public class SkyComboBox : ComboBox
    {
        #region " Control Help - Properties & Flicker Control "

        private int _StartIndex = 0;
        public int StartIndex
        {
            get => _StartIndex;
            set
            {
                _StartIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }

        public void ReplaceItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            try
            {
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(_highlightColor), e.Bounds);
                    LinearGradientBrush gloss = new(e.Bounds, ListSelectedBackColorA, ListSelectedBackColorB, 90);
                    e.Graphics.FillRectangle(gloss, new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height)));
                    e.Graphics.DrawRectangle(new(ListBorderColor) { DashStyle = ListDashType }, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(ListBackColor), e.Bounds);
                }

                using SolidBrush b = new(ListForeColor);
                e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, b, new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height));
            }
            catch
            {
            }
            e.DrawFocusRectangle();
        }

        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new()
            {
                FirstPoint,
                SecondPoint,
                ThirdPoint
            };
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }

        private Color _highlightColor = Color.FromArgb(121, 176, 214);
        public Color ItemHighlightColor
        {
            get => _highlightColor;
            set
            {
                _highlightColor = value;
                Invalidate();
            }
        }
        #endregion

        #region Variables
        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        #endregion

        #region Settings
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        public Color BGColorA { get; set; } = Color.FromArgb(245, 245, 245);

        public Color BGColorB { get; set; } = Color.FromArgb(230, 230, 230);

        public Color BorderColorA { get; set; } = Color.FromArgb(252, 252, 252);

        public Color BorderColorB { get; set; } = Color.FromArgb(249, 249, 249);

        public Color BorderColorC { get; set; } = Color.FromArgb(189, 189, 189);

        public Color BorderColorD { get; set; } = Color.FromArgb(200, 168, 168, 168);

        public Color TriangleColorA { get; set; } = Color.FromArgb(121, 176, 214);

        public Color TriangleColorB { get; set; } = Color.FromArgb(27, 94, 137);

        public Color LineColorA { get; set; } = Color.White;

        public Color LineColorB { get; set; } = Color.FromArgb(189, 189, 189);

        public Color LineColorC { get; set; } = Color.White;

        public Color ListForeColor { get; set; } = Color.Black;

        public Color ListBackColor { get; set; } = Color.FromArgb(255, 255, 255, 255);

        public Color ListBorderColor { get; set; } = Color.FromArgb(50, Color.Black);

        public DashStyle ListDashType { get; set; } = DashStyle.Dot;

        public Color ListSelectedBackColorA { get; set; } = Color.FromArgb(15, Color.White);

        public Color ListSelectedBackColorB { get; set; } = Color.FromArgb(0, Color.White);
        #endregion

        public SkyComboBox() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            DropDownStyle = ComboBoxStyle.DropDownList;
            DoubleBuffered = true;
            Size = new(75, 21);
            ItemHeight = 16;
            DrawItem += new DrawItemEventHandler(ReplaceItem);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingType;

            G.Clear(BackColor);
            LinearGradientBrush bodyGradNone = new(new Rectangle(0, 0, Width - 1, Height - 2), BGColorA, BGColorB, 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new(new Rectangle(0, 0, Width - 1, Height - 3), BorderColorA, BorderColorB, 90);
            G.DrawRectangle(new(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 4));
            G.DrawRectangle(new(BorderColorC), new Rectangle(0, 0, Width - 1, Height - 2));
            G.DrawLine(new(BorderColorD), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
            DrawTriangle(TriangleColorA, new Point(Width - 14, 8), new Point(Width - 7, 8), new Point(Width - 11, 12), G);
            G.DrawLine(new(TriangleColorB), new Point(Width - 14, 8), new Point(Width - 8, 8));

            //Draw Separator line
            G.DrawLine(new(LineColorA), new Point(Width - 22, 1), new Point(Width - 22, Height - 3));
            G.DrawLine(new(LineColorB), new Point(Width - 21, 1), new Point(Width - 21, Height - 3));
            G.DrawLine(new(LineColorC), new Point(Width - 20, 1), new Point(Width - 20, Height - 3));

            try
            {
                G.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(5, -1, Width - 20, Height), new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                });
            }
            catch
            {
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}