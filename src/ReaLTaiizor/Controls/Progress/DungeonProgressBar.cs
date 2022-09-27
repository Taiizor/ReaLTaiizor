#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DungeonProgressBar

    public class DungeonProgressBar : Control
    {

        #region Enums 

        public enum Alignment
        {
            Left,
            Right,
            Center
        }

        #endregion
        #region Variables 

        private int _Minimum;
        private int _Maximum = 100;
        private int _Value = 0;
        private Alignment ALN;
        private bool _DrawHatch;

        private bool _ShowPercentage;
        private GraphicsPath GP1;
        private GraphicsPath GP2;
        private GraphicsPath GP3;
        private Rectangle R1;
        private Rectangle R2;
        private readonly LinearGradientBrush GB1;
        private LinearGradientBrush GB2;
        private int I1;

        #endregion
        #region Properties

        public Color BorderColor { get; set; } = Color.FromArgb(180, 180, 180);

        public Color BackColorA { get; set; } = Color.FromArgb(244, 241, 243);

        public Color BackColorB { get; set; } = Color.FromArgb(244, 241, 243);

        public Color ProgressColorA { get; set; } = Color.FromArgb(214, 89, 37);

        public Color ProgressColorB { get; set; } = Color.FromArgb(223, 118, 75);

        public Color ProgressHatchColor { get; set; } = Color.FromArgb(25, 255, 255, 255);

        public int Maximum
        {
            get => _Maximum;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }

                if (value < _Value)
                {
                    _Value = value;
                }

                _Maximum = value;
                Invalidate();
            }
        }

        public int Minimum
        {
            get => _Minimum;
            set
            {
                _Minimum = value;

                if (value > _Maximum)
                {
                    _Maximum = value;
                }

                if (value > _Value)
                {
                    _Value = value;
                }

                Invalidate();
            }
        }

        public int Value
        {
            get => _Value;
            set
            {
                if (value > _Maximum)
                {
                    value = Maximum;
                }

                _Value = value;
                Invalidate();
            }
        }

        public Alignment ValueAlignment
        {
            get => ALN;
            set
            {
                ALN = value;
                Invalidate();
            }
        }

        public bool DrawHatch
        {
            get => _DrawHatch;
            set
            {
                _DrawHatch = value;
                Invalidate();
            }
        }

        public bool ShowPercentage
        {
            get => _ShowPercentage;
            set
            {
                _ShowPercentage = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 20;
            MinimumSize = new(58, 20);
        }

        #endregion

        public DungeonProgressBar()
        {
            _Maximum = 100;
            _ShowPercentage = true;
            _DrawHatch = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;
            DoubleBuffered = true;
        }

        public void Increment(int value)
        {
            _Value += value;
            Invalidate();
        }

        public void Deincrement(int value)
        {
            _Value -= value;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.Clear(Color.Transparent);
            G.SmoothingMode = SmoothingMode.HighQuality;

            GP1 = RoundRectangle.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 4);
            GP2 = RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, Height - 3), 4);

            R1 = new(0, 2, Width - 1, Height - 1);
            //GB1 = new(R1, Color.FromArgb(255, 255, 255), Color.FromArgb(230, 230, 230), 90f);

            // Draw inside background
            G.FillRectangle(new SolidBrush(BackColorA), R1);
            G.SetClip(GP1);
            G.FillPath(new SolidBrush(BackColorB), RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, (Height / 2) - 2), 4));


            I1 = (int)Math.Round((_Value - _Minimum) / (double)(_Maximum - _Minimum) * (Width - 3));
            if (I1 > 1)
            {
                GP3 = RoundRectangle.RoundRect(new Rectangle(1, 1, I1, Height - 3), 4);

                R2 = new(1, 1, I1, Height - 3);
                GB2 = new(R2, ProgressColorA, ProgressColorB, 90f);

                // Fill the value with its gradient
                G.FillPath(GB2, GP3);

                // Draw diagonal lines
                if (_DrawHatch == true)
                {
                    for (int i = 0; i <= (Width - 1) * _Maximum / _Value; i += 20)
                    {
                        G.DrawLine(new(new SolidBrush(ProgressHatchColor), 10.0F), new Point(Convert.ToInt32(i), 0), new Point(i - 10, Height));
                    }
                }

                G.SetClip(GP3);
                G.SmoothingMode = SmoothingMode.None;
                G.SmoothingMode = SmoothingMode.AntiAlias;
                G.ResetClip();
            }

            // Draw value as a string
            string DrawString = Convert.ToString(Convert.ToInt32(Value)) + "%";
            /*
                int textX = (int)(Width - G.MeasureString(DrawString, Font).Width - 1);
                int textY = (int)((Height / 2) - (Convert.ToInt32(G.MeasureString(DrawString, Font).Height / 2) - 2));
            */

            if (_ShowPercentage == true)
            {
                switch (ValueAlignment)
                {
                    case Alignment.Left:
                        G.DrawString(DrawString, new Font("Segoe UI", 8), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height + 2), new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Center
                        });
                        break;
                    case Alignment.Right:
                        G.DrawString(DrawString, new Font("Segoe UI", 8), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height + 2), new StringFormat
                        {
                            Alignment = StringAlignment.Far,
                            LineAlignment = StringAlignment.Center
                        });
                        break;
                    case Alignment.Center:
                        G.DrawString(DrawString, new Font("Segoe UI", 8), new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height + 2), new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                        break;
                }
            }

            // Draw border
            G.DrawPath(new(BorderColor), GP2);

            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}