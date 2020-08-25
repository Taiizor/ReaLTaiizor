#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyRadioButton

    [DefaultEvent("CheckedChanged")]
    public class SkyRadioButton : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new Point(0, 0);
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            mouse = e.Location;
            base.OnMouseMove(e);
        }

        protected override void OnTextChanged(EventArgs e)
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
                InvalidateControls();
                CheckedChanged?.Invoke(this);
                Invalidate();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (mouse.X <= Height - 1 || mouse.Y <= Width - 1)
            {
                if (!_Checked)
                    Checked = true;
                Invalidate();
            }
            base.OnClick(e);
        }

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InvalidateControls();
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is SkyRadioButton)
                    ((SkyRadioButton)C).Checked = false;
            }
        }
        #endregion

        public SkyRadioButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new Font("Verdana", 6.75f, FontStyle.Bold);
            Size = new Size(105, 14);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(Parent.FindForm().BackColor);

            G.DrawEllipse(new Pen(Color.FromArgb(168, 168, 168)), new Rectangle(0, 0, Height - 2, Height - 1));
            LinearGradientBrush bgGrad = new LinearGradientBrush(new Rectangle(0, 0, Height - 2, Height - 2), Color.FromArgb(245, 245, 245), Color.FromArgb(231, 231, 231), 90);
            G.FillEllipse(bgGrad, new Rectangle(0, 0, Height - 2, Height - 2));
            G.DrawEllipse(new Pen(Color.FromArgb(252, 252, 252)), new Rectangle(1, 1, Height - 4, Height - 4));

            if (Checked)
            {
                G.FillEllipse(new SolidBrush(Color.FromArgb(27, 94, 137)), new Rectangle(3, 3, Height - 8, Height - 8));
                G.FillEllipse(new SolidBrush(Color.FromArgb(150, 118, 177, 211)), new Rectangle(4, 4, Height - 10, Height - 10));
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(16, 1), new StringFormat
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