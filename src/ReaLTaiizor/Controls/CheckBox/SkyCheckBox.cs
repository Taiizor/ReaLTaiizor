#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyCheckBox

    [DefaultEvent("CheckedChanged")]
    public class SkyCheckBox : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new Point(0, 0);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            mouse = e.Location;
        }
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
        }
        protected override void OnClick(EventArgs e)
        {
            if (mouse.X <= Height - 1 || mouse.Y <= Width - 1)
            {
                _Checked = !_Checked;
                Invalidate();
                CheckedChanged?.Invoke(this);
            }

            base.OnClick(e);
        }
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);
        #endregion

        public SkyCheckBox() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Size = new Size(90, 16);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            Rectangle checkBoxRectangle = new Rectangle(0, 0, Height - 1, Height - 1);
            G.SmoothingMode = SmoothingMode.HighQuality;

            G.Clear(Parent.FindForm().BackColor);

            LinearGradientBrush bodyGrad = new LinearGradientBrush(checkBoxRectangle, Color.FromArgb(245, 245, 245), Color.FromArgb(231, 231, 231), 90);
            G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
            G.DrawRectangle(new Pen(Color.FromArgb(189, 189, 189)), new Rectangle(0, 0, Height - 1, Height - 2));
            G.DrawRectangle(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(1, 1, Height - 3, Height - 4));
            G.DrawLine(new Pen(Color.FromArgb(168, 168, 168)), new Point(1, Height - 1), new Point(Height - 2, Height - 1));

            if (Checked)
            {
                Rectangle chkPoly = new Rectangle(checkBoxRectangle.X + checkBoxRectangle.Width / 4, checkBoxRectangle.Y + checkBoxRectangle.Height / 4, checkBoxRectangle.Width / 2, checkBoxRectangle.Height / 2);
                Point[] Poly =
                {
                    new Point(chkPoly.X, chkPoly.Y + chkPoly.Height / 2),
                    new Point(chkPoly.X + chkPoly.Width / 2, chkPoly.Y + chkPoly.Height),
                    new Point(chkPoly.X + chkPoly.Width, chkPoly.Y)
                };
                G.SmoothingMode = SmoothingMode.HighQuality;
                Pen P1 = new Pen(Color.FromArgb(27, 94, 137), 2);
                LinearGradientBrush chkGrad = new LinearGradientBrush(chkPoly, Color.FromArgb(200, 200, 200), Color.FromArgb(255, 255, 255), 0f);

                G.DrawString("a", new Font("Marlett", 10.75f), new SolidBrush(Color.FromArgb(220, ForeColor)), new Rectangle(-2, -1, Width - 1, Height - 1), new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                });
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(16, 0), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();

        }

    }

    #endregion
}