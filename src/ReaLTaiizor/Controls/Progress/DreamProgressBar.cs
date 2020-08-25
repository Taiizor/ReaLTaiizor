#region Imports

using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DreamProgressBar

    public class DreamProgressBar : ProgressBar
    {
        private int _Value;
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

        private int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value == 0)
                    value = 1;
                _Maximum = value;
                Invalidate();
            }
        }

        public DreamProgressBar()
        {
            Value = 50;
        }

        Color _ColorA = Color.FromArgb(31, 31, 31);
        public Color ColorA
        {
            get { return _ColorA; }
            set { _ColorA = value; }
        }

        Color _ColorB = Color.FromArgb(41, 41, 41);
        public Color ColorB
        {
            get { return _ColorB; }
            set { _ColorB = value; }
        }

        Color _ColorC = Color.FromArgb(51, 51, 51);
        public Color ColorC
        {
            get { return _ColorC; }
            set { _ColorC = value; }
        }

        Color _ColorD = Color.FromArgb(0, 0, 0, 0);
        public Color ColorD
        {
            get { return _ColorD; }
            set { _ColorD = value; }
        }

        Color _ColorE = Color.FromArgb(25, 255, 255, 255);
        public Color ColorE
        {
            get { return _ColorE; }
            set { _ColorE = value; }
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int V = Width * _Value / _Maximum;
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(1, 1, Width - 2, Height - 2);
                    Rectangle R2 = new Rectangle(2, 2, V - 4, Height - 4);
                    Brush GB1 = new LinearGradientBrush(R1, _ColorB, _ColorC, 90.0F);
                    Brush GB2 = new LinearGradientBrush(R2, _ColorC, _ColorB, 30.0F);
                    G.FillRectangle(GB1, R1);
                    G.FillRectangle(GB2, R2);
                    // Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2)
                    G.DrawRectangle(new Pen(_ColorB), 1, 1, V - 3, Height - 3);
                    // Draw.Gradient(G, _ColorC, _ColorB, 2, 2, V - 4, Height - 4)

                    G.DrawRectangle(new Pen(_ColorA), 0, 0, Width - 1, Height - 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                    /*
                        Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2)
                        G.DrawRectangle(New Pen(_ColorB), 1, 1, V - 3, Height - 3)
                        Draw.Gradient(G, _ColorC, _ColorB, 2, 2, V - 4, Height - 4)

                        G.DrawRectangle(New Pen(_ColorA), 0, 0, Width - 1, Height - 1)

                        e.Graphics.DrawImage(B.Clone, 0, 0)
                    */
                }
            }
        }
    }

    #endregion
}