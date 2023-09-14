#region Imports

using ReaLTaiizor.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

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
            get => _Value;
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
            get => _Max;
            set
            {
                if (value == _Min || value < _Min)
                {
                    _Max = _Min + 1;
                    Invalidate();
                }
                else
                {
                    _Max = value;
                }
            }
        }

        private int _Min = 0;
        public int Min
        {
            get => _Min;
            set
            {
                if (value > _Max || value == _Max)
                {
                    _Min = _Max - 1;
                    Invalidate();
                }
                else
                {
                    _Min = value;
                }
            }
        }

        public new bool Enabled
        {
            get => EnabledCalc;
            set
            {
                IsEnabled = value;
                Invalidate();
            }
        }

        [DisplayName("Enabled")]
        public bool EnabledCalc
        {
            get => IsEnabled;
            set
            {
                Enabled = value;
                Invalidate();
            }
        }

        public Color BorderColor { get; set; } = FoxLibrary.ColorFromHex("#C8C8C8");
        public Color DisabledBorderColor { get; set; } = FoxLibrary.ColorFromHex("#E6E6E6");
        public Color ButtonTextColor { get; set; } = FoxLibrary.ColorFromHex("#56626E");
        public Color DisabledTextColor { get; set; } = FoxLibrary.ColorFromHex("#A6B2BE");
        public Color DisabledButtonTextColor { get; set; } = FoxLibrary.ColorFromHex("#BAC6D2");

        public FoxNumeric()
        {
            Enabled = true;
            DoubleBuffered = true;
            ForeColor = FoxLibrary.ColorFromHex("#424E5A");
            Font = new("Segoe UI", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Enabled)
            {
                using (Pen Border = new(BorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                    G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(Width - 20, 4, 15, 18), 2));
                }

                using (SolidBrush TextColor = new(ForeColor))
                {
                    FoxLibrary.CenterString(G, Value.ToString(), Font, TextColor.Color, new Rectangle(-10, 0, Width, Height));
                }

                using SolidBrush SignColor = new(ButtonTextColor);
                using Font SignFont = new("Marlett", 10);
                G.DrawString("t", SignFont, SignColor, new Point(Width - 20, 4));
                G.DrawString("u", SignFont, SignColor, new Point(Width - 20, 10));
            }
            else
            {
                using (Pen Border = new(DisabledBorderColor))
                {
                    G.DrawPath(Border, FoxLibrary.RoundRect(FoxLibrary.FullRectangle(Size, true), 2));
                    G.DrawPath(Border, FoxLibrary.RoundRect(new Rectangle(Width - 20, 4, 15, 18), 2));
                }

                using (SolidBrush TextColor = new(DisabledTextColor))
                {
                    FoxLibrary.CenterString(G, Value.ToString(), Font, TextColor.Color, new Rectangle(-10, 0, Width, Height));
                }

                using SolidBrush SignColor = new(DisabledButtonTextColor);
                using Font SignFont = new("Marlett", 10);
                G.DrawString("t", SignFont, SignColor, new Point(Width - 20, 4));
                G.DrawString("u", SignFont, SignColor, new Point(Width - 20, 10));
            }

            base.OnPaint(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (Enabled)
            {
                if (e.X > Width - 20 & e.Y < 10)
                {
                    Value += 1;
                }
                else if (e.X > Width - 20 & e.Y > 10)
                {
                    Value -= 1;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Enabled)
            {
                if (e.X > Width - 20 & e.Y < 10)
                {
                    Cursor = Cursors.Hand;
                }
                else if (e.X > Width - 20 & e.Y > 10)
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Size = new(Width, 27);
        }
    }

    #endregion
}