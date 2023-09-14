#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region SkyRadioButton

    [DefaultEvent("CheckedChanged")]
    public class SkyRadioButton : Control
    {
        #region " Control Help - MouseState & Flicker Control"
        private Point mouse = new(0, 0);
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
            get => _Checked;
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
                {
                    Checked = true;
                }

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
            {
                return;
            }

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is SkyRadioButton button)
                {
                    button.Checked = false;
                }
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

        public Color EllipseBorderColorA { get; set; } = Color.FromArgb(168, 168, 168);

        public Color EllipseBorderColorB { get; set; } = Color.FromArgb(252, 252, 252);

        public Color EllipseBackColorA { get; set; } = Color.FromArgb(245, 245, 245);

        public Color EllipseBackColorB { get; set; } = Color.FromArgb(231, 231, 231);

        public Color CheckedColorA { get; set; } = Color.FromArgb(27, 94, 137);

        public Color CheckedColorB { get; set; } = Color.FromArgb(150, 118, 177, 211);
        #endregion

        public SkyRadioButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(27, 94, 137);
            Font = new("Verdana", 6.75f, FontStyle.Bold);
            Size = new(105, 14);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingType;
            G.Clear(Parent.FindForm().BackColor);

            G.DrawEllipse(new(EllipseBorderColorA), new Rectangle(0, 0, Height - 2, Height - 1));
            LinearGradientBrush bgGrad = new(new Rectangle(0, 0, Height - 2, Height - 2), EllipseBackColorA, EllipseBackColorB, 90);
            G.FillEllipse(bgGrad, new Rectangle(0, 0, Height - 2, Height - 2));
            G.DrawEllipse(new(EllipseBorderColorB), new Rectangle(1, 1, Height - 4, Height - 4));

            if (Checked)
            {
                G.FillEllipse(new SolidBrush(CheckedColorA), new Rectangle(3, 3, Height - 8, Height - 8));
                G.FillEllipse(new SolidBrush(CheckedColorB), new Rectangle(4, 4, Height - 10, Height - 10));
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