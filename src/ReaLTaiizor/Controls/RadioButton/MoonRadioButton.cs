#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MoonRadioButton

    [DefaultEvent("CheckedChanged")]
    public class MoonRadioButton : MoonControl
    {
        private Color BG;
        private Color FC;

        public bool Checked
        {
            get;
            set
            {
                field = value;
                InvalidateControls();
                CheckedChanged?.Invoke(this);
                Invalidate();
            }
        }

        public Color CheckedColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        public Color HoverColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color HoverBackColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.Gray;

        public Color CircleColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        public Color CircleColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LightGray;

        public Color CircleColorC
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.LightGray;

        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !Checked)
            {
                return;
            }

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is MoonRadioButton button)
                {
                    button.Checked = false;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!Checked)
            {
                Checked = true;
            }

            base.OnMouseDown(e);
        }

        public MoonRadioButton()
        {
            LockHeight = 22;
            Width = 130;
            SetColor("BG", Color.FromArgb(240, 240, 240));
            SetColor("FC", Color.Gray);
            Cursor = Cursors.Hand;
            Font = new("Segoe UI", 9);
        }

        protected override void ColorHook()
        {
            BG = GetColor("BG");
            FC = GetColor("FC");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            G.SmoothingMode = SmoothingType;

            if (Checked)
            {
                G.FillEllipse(new SolidBrush(CheckedColor), new Rectangle(new Point(7, 7), new Size(8, 8)));
            }

            if (State == MouseStateMoon.Over)
            {
                G.FillEllipse(new SolidBrush(HoverColor), new Rectangle(new Point(4, 4), new Size(14, 14)));
                if (Checked)
                {
                    G.FillEllipse(new SolidBrush(HoverBackColor), new Rectangle(new Point(7, 7), new Size(8, 8)));
                }
            }

            G.DrawEllipse(new(new SolidBrush(CircleColorA)), new Rectangle(new Point(3, 3), new Size(16, 16)));
            G.DrawEllipse(new(new SolidBrush(CircleColorB)), new Rectangle(new Point(2, 2), new Size(18, 18)));
            G.DrawEllipse(new(new SolidBrush(CircleColorC)), new Rectangle(new Point(4, 4), new Size(14, 14)));

            G.DrawString(Text, Font, new SolidBrush(FC), 23, 3);
        }
    }

    #endregion
}