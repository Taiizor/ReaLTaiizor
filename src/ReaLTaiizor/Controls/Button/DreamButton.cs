#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DreamButton

    public class DreamButton : System.Windows.Forms.Button
    {
        public DreamButton()
        {
            ForeColor = Color.FromArgb(40, 218, 255);
            Size = new(120, 40);
            Cursor = Cursors.Hand;
        }

        private int State;
        protected override void OnMouseEnter(EventArgs e)
        {
            State = 1;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            State = 2;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            State = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            State = 1;
            Invalidate();
            base.OnMouseUp(e);
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
            using Bitmap B = new(Width, Height);
            using Graphics G = Graphics.FromImage(B);
            Rectangle R1 = new(0, 0, Width, Height / 2);
            Rectangle R2 = new(0, Height / 2, Width, Height);
            G.DrawRectangle(new(ColorA), 0, 0, Width - 1, Height - 1);

            if (State == 2)
            {
                Brush GB1 = new LinearGradientBrush(R1, ColorC, ColorB, 40.0F);
                Brush GB2 = new LinearGradientBrush(R2, ColorB, ColorC, 90.0F);
                G.FillRectangle(GB1, R1);
                G.FillRectangle(GB2, R2);
                //Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2);
            }
            else
            {
                Brush GB1 = new LinearGradientBrush(R1, ColorB, ColorC, 90.0F);
                Brush GB2 = new LinearGradientBrush(R2, ColorC, ColorB, 90.0F);
                G.FillRectangle(GB1, R1);
                G.FillRectangle(GB2, R2);
                // Draw.Gradient(G, _ColorC, _ColorB, 1, 1, Width - 2, Height - 2);
            }
            Pen P2 = new(Color.Black, 2);
            G.DrawRectangle(P2, 0, 0, Width, Height);
            SizeF O = G.MeasureString(Text, Font);
            G.DrawString(Text, Font, new SolidBrush(ForeColor), (Width / 2) - (O.Width / 2), (Height / 2) - (O.Height / 2));

            //Draw.Blend(G, _ColorD, _ColorE, _ColorD, 0.5, 0, 1, 1, Width - 2, 1);
            Bitmap B1 = B;
            e.Graphics.DrawImage(B1, 0, 0);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);
        }
    }

    #endregion
}