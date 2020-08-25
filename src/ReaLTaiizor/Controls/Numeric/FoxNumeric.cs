#region Imports

using System;
using System.Drawing;
using ReaLTaiizor.Utils;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region FoxNumeric

    public class FoxNumeric : Control
    {
        private Graphics G;

        private bool IsEnabled;

        private int _Value;
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value > _Max)
                {
                    _Value = _Max;
                    return;
                }
                if (value < Min)
                {
                    _Value = _Min;
                    return;
                }
                _Value = value;
                Invalidate();
            }
        }

        private int _Max = 100;
        public int Max
        {
            get { return _Max; }
            set
            {
                if (value == _Min || value < _Min)
                {
                    _Max = _Min + 1;
                    Invalidate();
                }
                else
                    _Max = value;
            }
        }

        private int _Min = 0;
        public int Min
        {
            get { return _Min; }
            set
            {
                if (value > _Max || value == _Max)
                {
                    _Min = _Max - 1;
                    Invalidate();
                }
                else
                    _Min = value;
            }
        }

        public new bool Enabled
        {
            get { return EnabledCalc; }
            set
            {
                IsEnabled = value;
                Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get { return IsEnabled; }
            set
            {
                Enabled = value;
                Invalidate();
            }
        }

        private Color _BorderColor = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        private Color _DisabledBorderColor = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color DisabledBorderColor
        {
            get { return _DisabledBorderColor; }
            set { _DisabledBorderColor = value; }
        }

        private Color _ButtonTextColor = FoxLibrary.ColorFromHex("#56626E");
        public Color ButtonTextColor
        {
            get { return _ButtonTextColor; }
            set { _ButtonTextColor = value; }
        }

        private Color _DisabledTextColor = FoxLibrary.ColorFromHex("#A6B2BE");
        public Color DisabledTextColor
        {
            get { return _DisabledTextColor; }
            set { _DisabledTextColor = value; }
        }

        private Color _DisabledButtonTextColor = FoxLibrary.ColorFromHex("#BAC6D2");
        public Color DisabledButtonTextColor
        {
            get { return _DisabledButtonTextColor; }
            set { _DisabledButtonTextColor = value; }
        }

        public FoxNumeric()
        {
            Enabled = true;
            DoubleBuffered = true;
            ForeColor = FoxLibrary.ColorFromHex("#424E5A");
            Font = new Font("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (Pen Border = new Pen(_BorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                    G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(Width - 20, 4, 15, 18), 2));
                }

                using (SolidBrush TextColor = new SolidBrush(ForeColor))
                    FoxLibrary.CenterString(G, Value.ToString(), Font, TextColor.Color, new Rectangle(-10, 0, Width, Height));

                using (SolidBrush SignColor = new SolidBrush(_ButtonTextColor))
                {
                    using (Font SignFont = new Font("Marlett", 10))
                    {
                        G.DrawString("t", SignFont, SignColor, new Point(Width - 20, 4));
                        G.DrawString("u", SignFont, SignColor, new Point(Width - 20, 10));
                    }
                }
            }
            else
            {
                using (Pen Border = new Pen(_DisabledBorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                    G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(Width - 20, 4, 15, 18), 2));
                }

                using (SolidBrush TextColor = new SolidBrush(_DisabledTextColor))
                    FoxLibrary.CenterString(G, Value.ToString(), Font, TextColor.Color, new Rectangle(-10, 0, Width, Height));

                using (SolidBrush SignColor = new SolidBrush(_DisabledButtonTextColor))
                {
                    using (Font SignFont = new Font("Marlett", 10))
                    {
                        G.DrawString("t", SignFont, SignColor, new Point(Width - 20, 4));
                        G.DrawString("u", SignFont, SignColor, new Point(Width - 20, 10));
                    }
                }
            }

            base.OnPaint(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (Enabled)
            {
                if (e.X > Width - 20 & e.Y < 10)
                    Value += 1;
                else if (e.X > Width - 20 & e.Y > 10)
                    Value -= 1;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Enabled)
            {
                if (e.X > Width - 20 & e.Y < 10)
                    Cursor = Cursors.Hand;
                else if (e.X > Width - 20 & e.Y > 10)
                    Cursor = Cursors.Hand;
                else
                    Cursor = Cursors.Default;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new Size(Width, 27);
        }
    }

    #endregion
}