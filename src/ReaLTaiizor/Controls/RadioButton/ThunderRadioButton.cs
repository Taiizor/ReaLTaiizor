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
    #region ThunderRadioButton

    [DefaultEvent("CheckedChanged")]
    public class ThunderRadioButton : Control
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
            Checked = !Checked;
            base.OnClick(e);
        }

        private bool _Checked = false;
        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                InvalidateControls();
                CheckedChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InvalidateControls();
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
            {
                return;
            }

            foreach (Control C in Parent.Controls)
            {
                if (C is ThunderRadioButton button && C != this)
                {
                    button.Checked = false;
                }
            }
        }

        public ThunderRadioButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
            Size = new(160, 16);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.Clear(BackColor);
            Rectangle radioBtnRectangle = new(0, 0, Height - 1, Height - 1);
            Rectangle R1 = new(4, 4, Height - 9, Height - 9);
            StringFormat format = new() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
            LinearGradientBrush bgGrad = new(radioBtnRectangle, Color.FromArgb(174, 195, 30), Color.FromArgb(141, 153, 16), 90);
            Color C1 = Color.FromArgb(250, 15, 15, 15);
            SolidBrush nb = new(ForeColor);
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.CompositingQuality = CompositingQuality.HighQuality;
            Font drawFont = new("Tahoma", 10, FontStyle.Bold);

            G.FillEllipse(bgGrad, radioBtnRectangle);
            G.DrawEllipse(new(Color.Black), radioBtnRectangle);

            if (Checked)
            {
                LinearGradientBrush chkGrad = new(R1, C1, C1, 90);
                G.FillEllipse(chkGrad, R1);
            }

            G.DrawString(Text, drawFont, Brushes.Black, new Point(17, 2), format);
            G.DrawString(Text, drawFont, nb, new Point(16, 1), format);

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }

        public event EventHandler CheckedChanged;
    }

    #endregion
}