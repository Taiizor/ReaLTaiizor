#region Imports

using ReaLTaiizor.Util;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region MoonCheckBox

    [DefaultEvent("CheckedChanged")]
    public class MoonCheckBox : MoonControl
    {
        private Color BG;
        private Color FC;
        private Color TCN;
        private Color TCH;

        private bool _Checked;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        public bool Checked
        {
            get => _Checked;
            set
            {
                _Checked = value;
                CheckedChanged?.Invoke(this);
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Checked = !Checked;
        }

        public MoonCheckBox()
        {
            LockHeight = 22;
            SetColor("G1", Color.White);
            SetColor("G2", Color.LightGray);
            SetColor("BG", Color.FromArgb(240, 240, 240));
            SetColor("FC", Color.Gray);
            SetColor("TCN", Color.Gray);
            SetColor("TCH", Color.Gray);
            Cursor = Cursors.Hand;
            Font = new("Segoe UI", 9);
            Size = new(118, Height);
        }

        protected override void ColorHook()
        {
            BG = GetColor("BG");
            FC = GetColor("FC");
            TCN = GetColor("TCN");
            TCH = GetColor("TCH");
        }

        protected override void PaintHook()
        {
            G.Clear(BG);

            if (_Checked)
            {
                G.DrawString("a", new Font("Marlett", 14), new SolidBrush(TCN), new Point(0, 1));
            }

            if (State == MouseStateMoon.Over)
            {
                G.FillRectangle(Brushes.White, new Rectangle(new Point(3, 3), new Size(15, 15)));
                if (_Checked)
                {
                    G.DrawString("a", new Font("Marlett", 14), new SolidBrush(TCH), new Point(0, 1));
                }
            }

            G.DrawRectangle(Pens.White, 2, 2, 17, 17);
            G.DrawRectangle(Pens.LightGray, 3, 3, 15, 15);
            G.DrawRectangle(Pens.LightGray, 1, 1, 19, 19);

            G.DrawString(Text, Font, new SolidBrush(FC), 22, 3);
        }
    }

    #endregion
}