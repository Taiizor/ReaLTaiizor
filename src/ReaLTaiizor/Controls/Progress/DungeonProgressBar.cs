#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

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
        private LinearGradientBrush GB1;
        private LinearGradientBrush GB2;
        private int I1;

        private Color _BorderColor = Color.FromArgb(180, 180, 180);
        private Color _BackColorA = Color.FromArgb(244, 241, 243);
        private Color _BackColorB = Color.FromArgb(244, 241, 243);
        private Color _ProgressColorA = Color.FromArgb(214, 89, 37);
        private Color _ProgressColorB = Color.FromArgb(223, 118, 75);
        private Color _ProgressHatchColor = Color.FromArgb(25, 255, 255, 255);

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

        public Color ProgressColorA
        {
            get { return _ProgressColorA; }
            set { _ProgressColorA = value; }
        }

        public Color ProgressColorB
        {
            get { return _ProgressColorB; }
            set { _ProgressColorB = value; }
        }

        public Color ProgressHatchColor
        {
            get { return _ProgressHatchColor; }
            set { _ProgressHatchColor = value; }
        }

        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value < 1)
                    value = 1;
                if (value < _Value)
                    _Value = value;
                _Maximum = value;
                Invalidate();
            }
        }

        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                _Minimum = value;

                if (value > _Maximum)
                    _Maximum = value;
                if (value > _Value)
                    _Value = value;

                Invalidate();
            }
        }

        public int Value
        {
            get { return _Value; }
            set
            {
                if (value > _Maximum)
                    value = Maximum;
                _Value = value;
                Invalidate();
            }
        }

        public Alignment ValueAlignment
        {
            get { return ALN; }
            set
            {
                ALN = value;
                Invalidate();
            }
        }

        public bool DrawHatch
        {
            get { return _DrawHatch; }
            set
            {
                _DrawHatch = value;
                Invalidate();
            }
        }

        public bool ShowPercentage
        {
            get { return _ShowPercentage; }
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
            MinimumSize = new Size(58, 20);
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
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.Clear(Color.Transparent);
            G.SmoothingMode = SmoothingMode.HighQuality;

            GP1 = RoundRectangle.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 4);
            GP2 = RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, Height - 3), 4);

            R1 = new Rectangle(0, 2, Width - 1, Height - 1);
            //GB1 = new LinearGradientBrush(R1, Color.FromArgb(255, 255, 255), Color.FromArgb(230, 230, 230), 90f);

            // Draw inside background
            G.FillRectangle(new SolidBrush(_BackColorA), R1);
            G.SetClip(GP1);
            G.FillPath(new SolidBrush(_BackColorB), RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, Height / 2 - 2), 4));


            I1 = (int)Math.Round(((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum)) * (double)(Width - 3));
            if (I1 > 1)
            {
                GP3 = RoundRectangle.RoundRect(new Rectangle(1, 1, I1, Height - 3), 4);

                R2 = new Rectangle(1, 1, I1, Height - 3);
                GB2 = new LinearGradientBrush(R2, _ProgressColorA, _ProgressColorB, 90f);

                // Fill the value with its gradient
                G.FillPath(GB2, GP3);

                // Draw diagonal lines
                if (_DrawHatch == true)
                {
                    for (var i = 0; i <= (Width - 1) * _Maximum / _Value; i += 20)
                        G.DrawLine(new Pen(new SolidBrush(_ProgressHatchColor), 10.0F), new Point(Convert.ToInt32(i), 0), new Point((int)(i - 10), Height));
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
            G.DrawPath(new Pen(_BorderColor), GP2);

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}