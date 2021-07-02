#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
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
            if (!_Checked)
            {
                @Checked = true;
            }
            else
            {
                @Checked = false;
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
                if (!object.ReferenceEquals(C, this) && C is RibbonRadioButton button)
                {
                    button.Checked = false;
                }
            }
        }

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

        private TextRenderingHint _TextRenderingType = TextRenderingHint.AntiAliasGridFit;
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        private Color _CheckedColor = Color.FromArgb(40, 40, 40);
        public Color CheckedColor
        {
            get => _CheckedColor;
            set
            {
                _CheckedColor = value;
                Invalidate();
            }
        }

        private Color _CircleBorderColor = Color.FromArgb(117, 120, 117);
        public Color CircleBorderColor
        {
            get => _CircleBorderColor;
            set
            {
                _CircleBorderColor = value;
                Invalidate();
            }
        }

        private Color _CircleEdgeColor = Color.WhiteSmoke;
        public Color CircleEdgeColor
        {
            get => _CircleEdgeColor;
            set
            {
                _CircleEdgeColor = value;
                Invalidate();
            }
        }

        private Color _CheckedBorderColorA = Color.FromArgb(203, 201, 205);
        public Color CheckedBorderColorA
        {
            get => _CheckedBorderColorA;
            set
            {
                _CheckedBorderColorA = value;
                Invalidate();
            }
        }

        private Color _CheckedBorderColorB = Color.FromArgb(188, 186, 190);
        public Color CheckedBorderColorB
        {
            get => _CheckedBorderColorB;
            set
            {
                _CheckedBorderColorB = value;
                Invalidate();
            }
        }
        #endregion

        public RibbonRadioButton() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(40, 40, 40);
            Size = new(133, 16);
            DoubleBuffered = true;
            Cursor = Cursors.Hand;
            Font = new("Tahoma", 8, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle radioBtnRectangle = new(0, 0, Height, Height - 1);
            Rectangle Inner = new(1, 1, Height - 2, Height - 3);

            G.SmoothingMode = SmoothingType;
            G.CompositingQuality = CompositingQualityType;
            G.TextRenderingHint = TextRenderingType;

            G.Clear(BackColor);

            LinearGradientBrush bgGrad = new(radioBtnRectangle, CheckedBorderColorA, CheckedBorderColorB, 90F);
            G.FillEllipse(bgGrad, radioBtnRectangle);

            G.DrawEllipse(new(CircleBorderColor), radioBtnRectangle);
            G.DrawEllipse(new(CircleEdgeColor), Inner);

            if (Checked)
            {
                G.DrawString("n", new Font("Marlett", 8, FontStyle.Bold), new SolidBrush(CheckedColor), 0, 3);
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(18, 8), new StringFormat
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