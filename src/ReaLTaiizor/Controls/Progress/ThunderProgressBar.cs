#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ThunderProgressBar

    public class ThunderProgressBar : Control
    {
        private int _Maximum = 100;

        public int Maximum
        {
            get => _Maximum;
            set
            {
                _Maximum = value;
                Invalidate();
            }
        }
        private int _Value = 0;
        public int Value
        {
            get
            {
                if (_Value == 0)
                {
                    return 0;
                }
                else
                {
                    return _Value;
                }
            }
            set
            {
                _Value = value;
                if (_Value > _Maximum)
                {
                    _Value = _Maximum;
                }

                Invalidate();
            }
        }
        private bool _ShowPercentage = false;
        public bool ShowPercentage
        {
            get => _ShowPercentage;
            set
            {
                _ShowPercentage = value;
                Invalidate();
            }
        }

        public ThunderProgressBar() : base()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.WhiteSmoke;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;

            double val = (double)_Value / _Maximum;
            int intValue = Convert.ToInt32(val * Width);
            G.Clear(BackColor);
            Color C1 = Color.FromArgb(174, 195, 30);
            Color C2 = Color.FromArgb(141, 153, 16);
            Rectangle R1 = new(0, 0, Width - 1, Height - 1);
            Rectangle R2 = new(0, 0, intValue - 1, Height - 1);
            Rectangle R3 = new(0, 0, intValue - 1, Height - 2);
            GraphicsPath GP1 = DrawThunder.RoundRect(R1, 1);
            GraphicsPath GP2 = DrawThunder.RoundRect(R2, 2);
            GraphicsPath GP3 = DrawThunder.RoundRect(R3, 1);
            LinearGradientBrush gB = new(R1, Color.FromArgb(26, 26, 26), Color.FromArgb(30, 30, 30), 90);
            LinearGradientBrush g1 = new(new Rectangle(2, 2, intValue - 1, Height - 2), C1, C2, 90);
            HatchBrush h1 = new(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(50, C1), Color.FromArgb(25, C2));
            Pen P1 = new(Color.Black);

            G.FillPath(gB, GP1);
            G.FillPath(g1, GP3);
            G.FillPath(h1, GP3);
            G.DrawPath(P1, GP1);
            G.DrawPath(new(Color.FromArgb(150, 97, 94, 90)), GP2);
            G.DrawPath(P1, GP2);

            if (_ShowPercentage)
            {
                G.DrawString(Convert.ToString(string.Concat(Value, "%")), Font, new SolidBrush(ForeColor), R1, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}