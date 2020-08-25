#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyComboBox

    public class SkyComboBox : System.Windows.Forms.ComboBox
    {
        #region " Control Help - Properties & Flicker Control "

        private int _StartIndex = 0;
        public int StartIndex
        {
            get { return _StartIndex; }
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
                    LinearGradientBrush gloss = new LinearGradientBrush(e.Bounds, Color.FromArgb(15, Color.White), Color.FromArgb(0, Color.White), 90);
                    e.Graphics.FillRectangle(gloss, new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height)));
                    e.Graphics.DrawRectangle(new Pen(Color.FromArgb(50, Color.Black)) { DashStyle = DashStyle.Dot }, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1));
                }
                else
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), e.Bounds);
                using (SolidBrush b = new SolidBrush(Color.Black))
                    e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, b, new Rectangle(e.Bounds.X + 2, e.Bounds.Y, e.Bounds.Width - 4, e.Bounds.Height));
            }
            catch
            {
            }
            e.DrawFocusRectangle();
        }

        protected void DrawTriangle(Color Clr, Point FirstPoint, Point SecondPoint, Point ThirdPoint, Graphics G)
        {
            List<Point> points = new List<Point>();
            points.Add(FirstPoint);
            points.Add(SecondPoint);
            points.Add(ThirdPoint);
            G.FillPolygon(new SolidBrush(Clr), points.ToArray());
        }

        private Color _highlightColor = Color.FromArgb(121, 176, 214);
        public Color ItemHighlightColor
        {
            get { return _highlightColor; }
            set
            {
                _highlightColor = value;
                Invalidate();
            }
        }
        #endregion

        public SkyComboBox() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            DropDownStyle = ComboBoxStyle.DropDownList;
            DoubleBuffered = true;
            Size = new Size(75, 21);
            ItemHeight = 16;
            DrawItem += new DrawItemEventHandler(ReplaceItem);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.Clear(BackColor);
            LinearGradientBrush bodyGradNone = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(230, 230, 230), 90);
            G.FillRectangle(bodyGradNone, bodyGradNone.Rectangle);
            LinearGradientBrush bodyInBorderNone = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 3), Color.FromArgb(252, 252, 252), Color.FromArgb(249, 249, 249), 90);
            G.DrawRectangle(new Pen(bodyInBorderNone), new Rectangle(1, 1, Width - 3, Height - 4));
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Width - 1, Height - 2));
            G.DrawLine(new Pen(Color.FromArgb(200, 168, 168, 168)), new Point(1, Height - 1), new Point(Width - 2, Height - 1));
            DrawTriangle(Color.FromArgb(121, 176, 214), new Point(Width - 14, 8), new Point(Width - 7, 8), new Point(Width - 11, 12), G);
            G.DrawLine(new Pen(Color.FromArgb(27, 94, 137)), new Point(Width - 14, 8), new Point(Width - 8, 8));

            //Draw Separator line
            G.DrawLine(new Pen(Color.White), new Point(Width - 22, 1), new Point(Width - 22, Height - 3));
            G.DrawLine(new Pen(Color.FromArgb(189, 189, 189)), new Point(Width - 21, 1), new Point(Width - 21, Height - 3));
            G.DrawLine(new Pen(Color.White), new Point(Width - 20, 1), new Point(Width - 20, Height - 3));
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