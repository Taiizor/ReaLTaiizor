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

        public int Maximum
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = 100;

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

        public bool ShowPercentage
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

        public bool ShowEdge
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = false;

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

        public SmoothingMode SmoothingType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = SmoothingMode.HighQuality;

        public string PercentageText
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = "%";

        public Color ProgressBorderColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(150, 97, 94, 90);

        public Color ProgressBorderColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(142, 107, 46);

        public Color EdgeColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(125, 97, 94, 90);

        public Color BorderColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(117, 120, 117);

        public Color ColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(203, 201, 205);

        public Color ColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(188, 186, 190);

        public Color BaseColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(75, Color.White);

        public Color ProgressColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(214, 162, 68);

        public Color ProgressColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(199, 147, 53);

        public Color ProgressLineColorA
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(40, Color.White);

        public Color ProgressLineColorB
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(20, Color.White);

        public HatchStyle HatchType
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = HatchStyle.DarkUpwardDiagonal;
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

            int intValue = Convert.ToInt32(Convert.ToDouble(_Value) / Convert.ToDouble(Maximum) * Width);
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

            if (ShowPercentage)
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