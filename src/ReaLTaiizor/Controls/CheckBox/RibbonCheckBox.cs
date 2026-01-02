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

        public bool Checked
        {
            get;
            set
            {
                field = value;
                CheckedChangedEvent?.Invoke(this);
                Invalidate();
            }
        } = false;

        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public CompositingQuality CompositingQualityType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = CompositingQuality.HighQuality;

        public TextRenderingHint TextRenderingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = TextRenderingHint.AntiAliasGridFit;

        public Color BaseColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Transparent;

        public Color CheckedColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Black;

        public Color CheckBorderColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(117, 120, 117);

        public Color CheckBorderColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.WhiteSmoke;

        public Color CheckBackColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(203, 201, 205);

        public Color CheckBackColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(188, 186, 190);

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
            Checked = !Checked;
            Focus();
            base.OnMouseDown(e);
        }

        public delegate void CheckedChangedEventHandler(object sender);
        private CheckedChangedEventHandler CheckedChangedEvent;

        public event CheckedChangedEventHandler CheckedChanged
        {
            add => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
            remove => CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
        }
        #endregion

        public RibbonCheckBox() : base()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            Size = new(120, 16);
            DoubleBuffered = true;
            Font = new("Tahoma", 8, FontStyle.Bold);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);
            Rectangle checkBoxRectangle = new(0, 0, Height, Height - 1);
            Rectangle Inner = new(1, 1, Height - 2, Height - 3);

            G.SmoothingMode = SmoothingType;
            G.CompositingQuality = CompositingQualityType;
            G.TextRenderingHint = TextRenderingType;

            G.Clear(BaseColor);

            LinearGradientBrush bodyGrad = new(checkBoxRectangle, CheckBackColorA, CheckBackColorB, 90);
            G.FillRectangle(bodyGrad, bodyGrad.Rectangle);
            G.DrawRectangle(new(CheckBorderColorA), checkBoxRectangle);
            G.DrawRectangle(new(CheckBorderColorB), Inner);

            if (Checked)
            {
                Font t = new("Marlett", 10, FontStyle.Regular);
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