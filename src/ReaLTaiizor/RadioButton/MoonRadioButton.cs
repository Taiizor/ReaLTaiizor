#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region MoonRadioButton

    [DefaultEvent("CheckedChanged")]
    public class MoonRadiobutton : MoonControl
    {

        Color BG;
        Color FC;

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

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is MoonRadiobutton)
                    ((MoonRadiobutton)C).Checked = false;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_Checked)
                Checked = true;
            base.OnMouseDown(e);
        }

        public MoonRadiobutton()
        {
            LockHeight = 22;
            Width = 130;
            SetColor("BG", Color.FromArgb(240, 240, 240));
            SetColor("FC", Color.Gray);
            Cursor = Cursors.Hand;
        }

        protected override void ColorHook()
        {
            BG = GetColor("BG");
            FC = GetColor("FC");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            G.SmoothingMode = SmoothingMode.HighQuality;

            if (_Checked)
                G.FillEllipse(Brushes.Gray, new Rectangle(new Point(7, 7), new Size(8, 8)));

            if (State == MouseStateMoon.Over)
            {
                G.FillEllipse(Brushes.White, new Rectangle(new Point(4, 4), new Size(14, 14)));
                if (_Checked)
                    G.FillEllipse(Brushes.Gray, new Rectangle(new Point(7, 7), new Size(8, 8)));
            }

            G.DrawEllipse(Pens.White, new Rectangle(new Point(3, 3), new Size(16, 16)));
            G.DrawEllipse(Pens.LightGray, new Rectangle(new Point(2, 2), new Size(18, 18)));
            G.DrawEllipse(Pens.LightGray, new Rectangle(new Point(4, 4), new Size(14, 14)));

            G.DrawString(Text, new Font("Segoe UI", 9), new SolidBrush(FC), 23, 3);
        }
    }

    #endregion
}