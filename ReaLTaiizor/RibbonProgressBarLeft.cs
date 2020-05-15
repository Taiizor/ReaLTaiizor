#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region RibbonProgressBarLeft

    public class RibbonProgressBarLeft : Control
    {

        #region " Control Help - Properties & Flicker Control "
        private int OFS = 0;
        private int Speed = 50;

        private int _Maximum = 100;
        public int Maximum
        {
            get { return _Maximum; }
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
                return _Value;
            }
            set
            {
                _Value = value;
                Invalidate();
            }
        }
        private bool _ShowPercentage = false;
        public bool ShowPercentage
        {
            get { return _ShowPercentage; }
            set
            {
                _ShowPercentage = value;
                Invalidate();
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            System.Threading.Thread T = new System.Threading.Thread(Animate);
            T.IsBackground = true;
        }
        public void Animate()
        {
            while (true)
            {
                if (OFS <= Width)
                    OFS += 1;
                else
                    OFS = 0;
                Invalidate();
                System.Threading.Thread.Sleep(Speed);
            }
        }
        #endregion

        public RibbonProgressBarLeft() : base()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            ForeColor = Color.Black;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingMode.HighQuality;

            int intValue = Convert.ToInt32((Convert.ToDouble(_Value) / Convert.ToDouble(_Maximum)) * Width);
            G.Clear(BackColor);

            LinearGradientBrush gB = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 1), Color.FromArgb(203, 201, 205), Color.FromArgb(188, 186, 190), 90);
            G.FillPath(gB, DrawRibbon.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
            G.DrawPath(new Pen(new SolidBrush(Color.FromArgb(75, Color.White))), DrawRibbon.RoundRect(new Rectangle(1, 1, Width - 3, Height - 3), 1));

            LinearGradientBrush g1 = new LinearGradientBrush(new Rectangle(2, 2, intValue - 1, Height - 2), Color.FromArgb(203, 201, 205), Color.FromArgb(188, 186, 190), 90);
            G.FillPath(g1, DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 2), 1));
            HatchBrush h1 = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.FromArgb(40, Color.White), Color.FromArgb(20, Color.White));
            G.FillPath(h1, DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 2), 1));

            //G.DrawPath(New Pen(Color.FromArgb(125, 97, 94, 90)), Draw.RoundRect(New Rectangle(0, 1, Width - 1, Height - 3), 2))
            G.DrawPath(new Pen(Color.FromArgb(117, 120, 117)), DrawRibbon.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));

            G.DrawPath(new Pen(Color.FromArgb(150, 97, 94, 90)), DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 1), 2));
            G.DrawPath(new Pen(Color.FromArgb(60, 113, 132)), DrawRibbon.RoundRect(new Rectangle(0, 0, intValue - 1, Height - 1), 2));
            //colored bar outline

            if (_ShowPercentage)
            {
                G.DrawString(Convert.ToString(string.Concat(Value, "%")), Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 1, Height - 1), new StringFormat
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