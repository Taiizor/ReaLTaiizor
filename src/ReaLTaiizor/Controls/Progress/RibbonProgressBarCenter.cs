#region Imports

using ReaLTaiizor.Util;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region RibbonProgressBarCenter

    public class RibbonProgressBarCenter : Control
    {

        #region " Control Help - Properties & Flicker Control "
        private int OFS = 0;
        private readonly int Speed = 50;

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
            get => _Value switch
            {
                0 => 0,
                _ => _Value,
            };
            set
            {
                _Value = value;
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

        private bool _ShowEdge = false;
        public bool ShowEdge
        {
            get => _ShowEdge;
            set
            {
                _ShowEdge = value;
                Invalidate();
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Thread T = new(Animate)
            {
                IsBackground = true
            };
        }

        public void Animate()
        {
            while (true)
            {
                if (OFS <= Width)
                {
                    OFS += 1;
                }
                else
                {
                    OFS = 0;
                }

                Invalidate();
                Thread.Sleep(Speed);
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private string _PercentageText = "%";
        public string PercentageText
        {
            get => _PercentageText;
            set
            {
                _PercentageText = value;
                Invalidate();
            }
        }

        private Color _ProgressBorderColorA = Color.FromArgb(150, 97, 94, 90);
        public Color ProgressBorderColorA
        {
            get => _ProgressBorderColorA;
            set
            {
                _ProgressBorderColorA = value;
                Invalidate();
            }
        }

        private Color _ProgressBorderColorB = Color.FromArgb(142, 107, 46);
        public Color ProgressBorderColorB
        {
            get => _ProgressBorderColorB;
            set
            {
                _ProgressBorderColorB = value;
                Invalidate();
            }
        }

        private Color _EdgeColor = Color.FromArgb(125, 97, 94, 90);
        public Color EdgeColor
        {
            get => _EdgeColor;
            set
            {
                _EdgeColor = value;
                Invalidate();
            }
        }

        private Color _BorderColor = Color.FromArgb(117, 120, 117);
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Invalidate();
            }
        }

        private Color _ColorA = Color.FromArgb(203, 201, 205);
        public Color ColorA
        {
            get => _ColorA;
            set
            {
                _ColorA = value;
                Invalidate();
            }
        }

        private Color _ColorB = Color.FromArgb(188, 186, 190);
        public Color ColorB
        {
            get => _ColorB;
            set
            {
                _ColorB = value;
                Invalidate();
            }
        }

        private Color _BaseColor = Color.FromArgb(75, Color.White);
        public Color BaseColor
        {
            get => _BaseColor;
            set
            {
                _BaseColor = value;
                Invalidate();
            }
        }

        private Color _ProgressColorA = Color.FromArgb(214, 162, 68);
        public Color ProgressColorA
        {
            get => _ProgressColorA;
            set
            {
                _ProgressColorA = value;
                Invalidate();
            }
        }

        private Color _ProgressColorB = Color.FromArgb(199, 147, 53);
        public Color ProgressColorB
        {
            get => _ProgressColorB;
            set
            {
                _ProgressColorB = value;
                Invalidate();
            }
        }

        private Color _ProgressLineColorA = Color.FromArgb(40, Color.White);
        public Color ProgressLineColorA
        {
            get => _ProgressLineColorA;
            set
            {
                _ProgressLineColorA = value;
                Invalidate();
            }
        }

        private Color _ProgressLineColorB = Color.FromArgb(20, Color.White);
        public Color ProgressLineColorB
        {
            get => _ProgressLineColorB;
            set
            {
                _ProgressLineColorB = value;
                Invalidate();
            }
        }

        private HatchStyle _HatchType = HatchStyle.DarkUpwardDiagonal;
        public HatchStyle HatchType
        {
            get => _HatchType;
            set
            {
                _HatchType = value;
                Invalidate();
            }
        }
        #endregion

        public RibbonProgressBarCenter() : base()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            ForeColor = Color.Black;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingType;

            int intValue = Convert.ToInt32(Convert.ToDouble(_Value) / Convert.ToDouble(_Maximum) * Width);
            G.Clear(BackColor);

            LinearGradientBrush gB = new(new Rectangle(0, 0, Width - 1, Height - 1), ColorA, ColorB, 90);
            G.FillPath(gB, DrawRibbon.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
            G.DrawPath(new(new SolidBrush(BaseColor)), DrawRibbon.RoundRect(new Rectangle(1, 1, Width - 3, Height - 3), 1));
            LinearGradientBrush g1 = new(new Rectangle(2, 2, intValue - 1, Height - 2), ProgressColorA, ProgressColorB, 90);
            G.FillPath(g1, DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 2), 1));
            HatchBrush h1 = new(HatchType, ProgressLineColorA, ProgressLineColorB);
            G.FillPath(h1, DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 2), 1));

            if (ShowEdge)
            {
                G.DrawPath(new(EdgeColor), DrawRibbon.RoundRect(new Rectangle(0, 1, Width - 1, Height - 3), 2));
            }

            G.DrawPath(new(BorderColor), DrawRibbon.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));

            G.DrawPath(new(ProgressBorderColorA), DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 1), 2));
            G.DrawPath(new(ProgressBorderColorB), DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 1), 2));

            if (_ShowPercentage)
            {
                G.DrawString(Convert.ToString(string.Concat(Value, PercentageText)), Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 1, Height - 1), new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                });
            }

            e.Graphics.DrawImage(B, 0, 0);
            G.Dispose();
            B.Dispose();
        }
    }

    #endregion
}