#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region AirRadioButton

    [DefaultEventAttribute("CheckedChanged")]
    public class AirRadioButton : AirControl
    {

        public AirRadioButton()
        {
            Font = new Font("Segoe UI", 9);
            LockHeight = 17;
            SetColor("Text", 60, 60, 60);
            SetColor("Gradient top", 237, 237, 237);
            SetColor("Gradient bottom", 230, 230, 230);
            SetColor("Borders", 167, 167, 167);
            SetColor("Bullet", 100, 100, 100);
            Width = 110;
            Cursor = Cursors.Hand;
        }

        private int X;
        private Color TextColor;
        private Color G1;
        private Color G2;
        private Color Bo;

        private Color Bb;
        protected override void ColorHook()
        {
            TextColor = GetColor("Text");
            G1 = GetColor("Gradient top");
            G2 = GetColor("Gradient bottom");
            Bb = GetColor("Bullet");
            Bo = GetColor("Borders");
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            X = e.Location.X;
            Invalidate();
        }

        protected override void PaintHook()
        {
            G.Clear(BackColor);
            G.SmoothingMode = SmoothingMode.HighQuality;
            if (_Checked)
            {
                LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(14, 14)), G1, G2, 90f);
                G.FillEllipse(LGB, new Rectangle(new Point(0, 0), new Size(14, 14)));
            }
            else
            {
                LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(14, 16)), G1, G2, 90f);
                G.FillEllipse(LGB, new Rectangle(new Point(0, 0), new Size(14, 14)));
            }

            if (State == MouseStateAir.Over & X < 15)
            {
                SolidBrush SB = new SolidBrush(Color.FromArgb(10, Color.Black));
                G.FillEllipse(SB, new Rectangle(new Point(0, 0), new Size(14, 14)));
            }
            else if (State == MouseStateAir.Down & X < 15)
            {
                SolidBrush SB = new SolidBrush(Color.FromArgb(20, Color.Black));
                G.FillEllipse(SB, new Rectangle(new Point(0, 0), new Size(14, 14)));
            }

            GraphicsPath P = new GraphicsPath();
            P.AddEllipse(new Rectangle(0, 0, 14, 14));
            G.SetClip(P);

            LinearGradientBrush LLGGBB = new LinearGradientBrush(new Rectangle(0, 0, 14, 5), Color.FromArgb(150, Color.White), Color.Transparent, 90f);
            G.FillRectangle(LLGGBB, LLGGBB.Rectangle);

            G.ResetClip();

            G.DrawEllipse(new Pen(Bo), new Rectangle(new Point(0, 0), new Size(14, 14)));

            if (_Checked)
            {
                SolidBrush LGB = new SolidBrush(Bb);
                G.FillEllipse(LGB, new Rectangle(new Point(4, 4), new Size(6, 6)));
            }

            DrawText(new SolidBrush(TextColor), HorizontalAlignment.Left, 17, -2);
        }

        private int _Field = 16;
        public int Field
        {
            get { return _Field; }
            set
            {
                if (value < 4)
                    return;
                _Field = value;
                LockHeight = value;
                Invalidate();
            }
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_Checked)
                Checked = true;
            base.OnMouseDown(e);
        }

        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        protected override void OnCreation()
        {
            InvalidateControls();
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_Checked)
                return;

            foreach (Control C in Parent.Controls)
            {
                if (!object.ReferenceEquals(C, this) && C is AirRadioButton)
                    ((AirRadioButton)C).Checked = false;
            }
        }

    }

    #endregion
}