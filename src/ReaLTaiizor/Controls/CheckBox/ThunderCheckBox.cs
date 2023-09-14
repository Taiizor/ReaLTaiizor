#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ThunderCheckBox

    [DefaultEvent("CheckedChanged")]
    public class ThunderCheckBox : Control
    {
        private MouseStateThunder State = MouseStateThunder.None;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseStateThunder.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseStateThunder.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseStateThunder.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseStateThunder.None;
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 16;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            _Checked = !_Checked;
            CheckedChanged?.Invoke(this, EventArgs.Empty);
            base.OnClick(e);
        }

        private bool _Checked = false;
        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        public ThunderCheckBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            Size = new(135, 16);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.CompositingQuality = CompositingQuality.HighQuality;
            Rectangle checkBoxRectangle = new(0, 0, Height - 1, Height - 1);
            LinearGradientBrush bodyGrad = new(checkBoxRectangle, Color.FromArgb(174, 195, 30), Color.FromArgb(141, 153, 16), 90);
            SolidBrush nb = new(ForeColor);
            StringFormat format = new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            Font drawFont = new("Tahoma", 9, FontStyle.Bold);
            G.Clear(BackColor);
            G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
            G.DrawRectangle(new(Color.Black), checkBoxRectangle);
            G.DrawString(Text, drawFont, Brushes.Black, new Point(17, 9), format);
            G.DrawString(Text, drawFont, nb, new Point(16, 8), format);

            if (_Checked)
            {
                Rectangle chkPoly = new(checkBoxRectangle.X + (checkBoxRectangle.Width / 4), checkBoxRectangle.Y + (checkBoxRectangle.Height / 4), checkBoxRectangle.Width / 2, checkBoxRectangle.Height / 2);
                Point[] p = new Point[]
                {
                    new Point(chkPoly.X, chkPoly.Y + (chkPoly.Height /2)),
                    new Point(chkPoly.X + (chkPoly.Width / 2), chkPoly.Y + chkPoly.Height),
                    new Point(chkPoly.X + chkPoly.Width, chkPoly.Y)
                };
                Pen P1 = new(Color.FromArgb(12, 12, 12), 2);
                for (int i = 0; i <= p.Length - 2; i++)
                {
                    G.DrawLine(P1, p[i], p[i + 1]);
                }
            }
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }

        public event EventHandler CheckedChanged;
    }

    #endregion
}