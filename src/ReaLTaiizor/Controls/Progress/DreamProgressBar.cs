#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DreamProgressBar

    public class DreamProgressBar : ProgressBar
    {
        private int _Value;
        public int Value
        {
            get => _Value;
            set
            {
                _Value = value;
                Invalidate();
            }
        }

        private int _Maximum = 100;
        public int Maximum
        {
            get => _Maximum;
            set
            {
                if (value == 0)
                {
                    value = 1;
                }

                _Maximum = value;
                Invalidate();
            }
        }

        public DreamProgressBar()
        {
            Value = 50;
        }

        public Color ColorA { get; set; } = Color.FromArgb(31, 31, 31);
        public Color ColorB { get; set; } = Color.FromArgb(41, 41, 41);
        public Color ColorC { get; set; } = Color.FromArgb(51, 51, 51);
        public Color ColorD { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color ColorE { get; set; } = Color.FromArgb(25, 255, 255, 255);


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int V = Width * _Value / _Maximum;
            using Bitmap B = new(Width, Height);
            using Graphics G = Graphics.FromImage(B);
            Rectangle R1 = new(1, 1, Width - 2, Height - 2);
            Rectangle R2 = new(2, 2, V - 4, Height - 4);
            Brush GB1 = new LinearGradientBrush(R1, ColorB, ColorC, 90.0F);
            Brush GB2 = new LinearGradientBrush(R2, ColorC, ColorB, 30.0F);
            G.FillRectangle(GB1, R1);
            G.FillRectangle(GB2, R2);
            // Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2)
            G.DrawRectangle(new(ColorB), 1, 1, V - 3, Height - 3);
            // Draw.Gradient(G, _ColorC, _ColorB, 2, 2, V - 4, Height - 4)

            G.DrawRectangle(new(ColorA), 0, 0, Width - 1, Height - 1);
            Bitmap B1 = B;
            e.Graphics.DrawImage(B1, 0, 0);
            /*
                Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2)
                G.DrawRectangle(new(_ColorB), 1, 1, V - 3, Height - 3)
                Draw.Gradient(G, _ColorC, _ColorB, 2, 2, V - 4, Height - 4)

                G.DrawRectangle(new(_ColorA), 0, 0, Width - 1, Height - 1)

                e.Graphics.DrawImage(B.Clone, 0, 0)
            */
        }
    }

    #endregion
}