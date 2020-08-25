#region Imports

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RibbonCheckBox

    [DefaultEvent("CheckedChanged")]
    public class RibbonCheckBox : Control
    {

        #region " Control Help - MouseState & Flicker Control"
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        private bool _Checked = false;
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                Invalidate();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get { return _SmoothingType; }
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private CompositingQuality _CompositingQualityType = CompositingQuality.HighQuality;
        public CompositingQuality CompositingQualityType
        {
            get { return _CompositingQualityType; }
            set
            {
                _CompositingQualityType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.AntiAliasGridFit;
        public TextRenderingHint TextRenderingType
        {
            get { return _TextRenderingType; }
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.Transparent;
        public Color BaseColor
        {
            get { return _BaseColor; }
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _CheckedColor = Color.Black;
        public Color CheckedColor
        {
            get { return _CheckedColor; }
            set
            {
                _CheckedColor = value;
                Invalidate();
            }
        }

        private Color _CheckBorderColorA = Color.FromArgb(117, 120, 117);
        public Color CheckBorderColorA
        {
            get { return _CheckBorderColorA; }
            set
            {
                _CheckBorderColorA = value;
                Invalidate();
            }
        }

        private Color _CheckBorderColorB = Color.WhiteSmoke;
        public Color CheckBorderColorB
        {
            get { return _CheckBorderColorB; }
            set
            {
                _CheckBorderColorB = value;
                Invalidate();
            }
        }

        private Color _CheckBackColorA = Color.FromArgb(203, 201, 205);
        public Color CheckBackColorA
        {
            get { return _CheckBackColorA; }
            set
            {
                _CheckBackColorA = value;
                Invalidate();
            }
        }

        private Color _CheckBackColorB = Color.FromArgb(188, 186, 190);
        public Color CheckBackColorB
        {
            get { return _CheckBackColorB; }
            set
            {
                _CheckBackColorB = value;
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 14;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _Checked = !_Checked;
            Focus();
            CheckedChangedEvent?.Invoke(this);
            base.OnMouseDown(e);
        }

        public delegate void CheckedChangedEventHandler(object sender);
        private CheckedChangedEventHandler CheckedChangedEvent;

        public event CheckedChangedEventHandler CheckedChanged
        {
            add
            {
                CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
            }
            remove
            {
                CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
            }
        }
        #endregion

        public RibbonCheckBox() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            Size = new Size(120, 16);
            DoubleBuffered = true;
            Font = new Font("Tahoma", 8, FontStyle.Bold);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle checkBoxRectangle = new Rectangle(0, 0, Height, Height - 1);
            Rectangle Inner = new Rectangle(1, 1, Height - 2, Height - 3);

            G.SmoothingMode = SmoothingType;
            G.CompositingQuality = CompositingQualityType;
            G.TextRenderingHint = TextRenderingType;

            G.Clear(BaseColor);

            LinearGradientBrush bodyGrad = new LinearGradientBrush(checkBoxRectangle, CheckBackColorA, CheckBackColorB, 90);
            G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
            G.DrawRectangle(new Pen(CheckBorderColorA), checkBoxRectangle);
            G.DrawRectangle(new Pen(CheckBorderColorB), Inner);

            if (Checked)
            {
                Font t = new Font("Marlett", 10, FontStyle.Regular);
                G.DrawString("a", t, new SolidBrush(CheckedColor), -1.5F, 0F);
            }

            G.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(18, 7), new StringFormat
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