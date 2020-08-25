#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonNumeric

    public class DungeonNumeric : Control
    {

        #region Enums

        public enum _TextAlignment
        {
            Near,
            Far
        }

        #endregion
        #region Variables

        private GraphicsPath Shape;
        private Pen P1;

        private long _Value;
        private long _Minimum;
        private long _Maximum;
        private int Xval;
        private bool KeyboardNum;
        private _TextAlignment MyStringAlignment;

        private Timer LongPressTimer = new Timer();

        private Color _BorderColor = Color.FromArgb(180, 180, 180);
        private Color _BackColorA = Color.FromArgb(246, 246, 246);
        private Color _BackColorB = Color.FromArgb(254, 254, 254);
        private Color _ButtonForeColorA = Color.FromArgb(75, 75, 75);
        private Color _ButtonForeColorB = Color.FromArgb(75, 75, 75);

        #endregion
        #region Properties

        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        public Color BackColorA
        {
            get { return _BackColorA; }
            set { _BackColorA = value; }
        }

        public Color BackColorB
        {
            get { return _BackColorB; }
            set { _BackColorB = value; }
        }

        public Color ButtonForeColorA
        {
            get { return _ButtonForeColorA; }
            set { _ButtonForeColorA = value; }
        }

        public Color ButtonForeColorB
        {
            get { return _ButtonForeColorB; }
            set { _ButtonForeColorB = value; }
        }

        public long Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value <= _Maximum & value >= _Minimum)
                    _Value = value;
                Invalidate();
            }
        }

        public long Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                if (value < _Maximum)
                    _Minimum = value;
                if (_Value < _Minimum)
                    _Value = Minimum;
                Invalidate();
            }
        }

        public long Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value > _Minimum)
                    _Maximum = value;
                if (_Value > _Maximum)
                    _Value = _Maximum;
                Invalidate();
            }
        }

        public _TextAlignment TextAlignment
        {
            get
            {
                return MyStringAlignment;
            }
            set
            {
                MyStringAlignment = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 28;
            MinimumSize = new Size(93, 28);
            Shape = new GraphicsPath();
            Shape.AddArc(0, 0, 10, 10, 180, 90);
            Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            Shape.CloseAllFigures();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Xval = e.Location.X;
            Invalidate();

            if (e.X < Width - 50)
                Cursor = Cursors.Default; //Cursors.IBeam
            else
                Cursor = Cursors.Default;
            if (e.X > Width - 25 && e.X < Width - 10)
                Cursor = Cursors.Hand;
            if (e.X > Width - 44 && e.X < Width - 33)
                Cursor = Cursors.Hand;
        }

        private void ClickButton()
        {
            if (Xval > Width - 25 && Xval < Width - 10)
            {
                if ((Value + 1) <= _Maximum)
                    _Value++;
            }
            else
            {
                if (Xval > Width - 44 && Xval <Width - 33)
                {
                    if ((Value - 1) >= _Minimum)
                        _Value--;
                }
                KeyboardNum = !KeyboardNum;
            }
            Focus();
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ClickButton();
            LongPressTimer.Start();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            LongPressTimer.Stop();
        }

        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            ClickButton();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (KeyboardNum == true)
                    _Value = long.Parse((_Value).ToString() + e.KeyChar.ToString().ToString());
                if (_Value > _Maximum)
                    _Value = _Maximum;
            }
            catch (Exception)
            {
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Back)
            {
                string TemporaryValue = _Value.ToString();
                TemporaryValue = TemporaryValue.Remove(Convert.ToInt32(TemporaryValue.Length - 1));
                if (TemporaryValue.Length == 0)
                    TemporaryValue = "0";
                _Value = Convert.ToInt32(TemporaryValue);
            }
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                if ((Value + 1) <= _Maximum)
                    _Value++;
                Invalidate();
            }
            else
            {
                if ((Value - 1) >= _Minimum)
                    _Value--;
                Invalidate();
            }
        }

        #endregion

        public DungeonNumeric()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            P1 = new Pen(_BorderColor);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(76, 76, 76);
            _Minimum = 0;
            _Maximum = 100;
            Font = new Font("Tahoma", 11);
            Size = new Size(70, 28);
            MinimumSize = new Size(62, 28);
            DoubleBuffered = true;

            LongPressTimer.Tick += LongPressTimer_Tick;
            LongPressTimer.Interval = 300;
        }

        public void Increment(int Value)
        {
            _Value += Value;
            Invalidate();
        }

        public void Decrement(int Value)
        {
            _Value -= Value;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            LinearGradientBrush BackgroundLGB = default(LinearGradientBrush);

            BackgroundLGB = new LinearGradientBrush(ClientRectangle, _BackColorA, _BackColorB, 90.0F);

            G.SmoothingMode = SmoothingMode.AntiAlias;

            G.Clear(Color.Transparent); // Set control background color
            G.FillPath(BackgroundLGB, Shape); // Draw background
            G.DrawPath(P1, Shape); // Draw border

            G.DrawString("+", new Font("Tahoma", 14), new SolidBrush(_ButtonForeColorA), new Rectangle(Width - 25, 1, 19, 30));
            G.DrawLine(new Pen(_BorderColor), Width - 28, 1, Width - 28, Height - 2);
            G.DrawString("-", new Font("Tahoma", 14), new SolidBrush(_ButtonForeColorB), new Rectangle(Width - 44, 1, 19, 30));
            G.DrawLine(new Pen(_BorderColor), Width - 48, 1, Width - 48, Height - 2);

            switch (MyStringAlignment)
            {
                case _TextAlignment.Near:
                    G.DrawString(Convert.ToString(Value), Font, new SolidBrush(ForeColor), new Rectangle(5, 0, Width - 1, Height - 1), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                    break;
                case _TextAlignment.Far:
                    G.DrawString(Convert.ToString(Value), Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 15, Height - 1), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
            }
            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}