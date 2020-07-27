#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region RibbonRadioButton

    [DefaultEvent("CheckedChanged")]
    public class RibbonRadioButton : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 16;
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
            if (!_Checked)
                Checked = true;
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
                if (!object.ReferenceEquals(C, this) && C is RibbonRadioButton)
                    ((RibbonRadioButton)C).Checked = false;
            }
        }
        #endregion

        public RibbonRadioButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(40, 40, 40);
            Size = new Size(133, 16);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle radioBtnRectangle = new Rectangle(0, 0, Height, Height - 1);
            Rectangle Inner = new Rectangle(1, 1, Height - 2, Height - 3);

            G.SmoothingMode = SmoothingMode.HighQuality;
            G.CompositingQuality = CompositingQuality.HighQuality;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            G.Clear(BackColor);

            LinearGradientBrush bgGrad = new LinearGradientBrush(radioBtnRectangle, Color.FromArgb(203, 201, 205), Color.FromArgb(188, 186, 190), 90F);
            G.FillEllipse(bgGrad, radioBtnRectangle);

            G.DrawEllipse(new Pen(Color.FromArgb(117, 120, 117)), radioBtnRectangle);
            G.DrawEllipse(new Pen(Color.WhiteSmoke), Inner);

            if (Checked)
            {
                Font t = new Font("Marlett", 8, FontStyle.Bold);
                G.DrawString("n", t, new SolidBrush(Color.FromArgb(40, 40, 40)), 0, 3);
            }

            Font drawFont = new Font("Tahoma", 8, FontStyle.Bold);
            Brush nb = new SolidBrush(ForeColor);
            G.DrawString(Text, drawFont, nb, new Point(18, 7), new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            });

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }

    }

    #endregion
}