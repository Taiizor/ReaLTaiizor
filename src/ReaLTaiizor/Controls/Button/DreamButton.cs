#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region DreamButton

    public class DreamButton : System.Windows.Forms.Button
    {
        public DreamButton()
        {
            ForeColor = Color.FromArgb(40, 218, 255);
            Size = new Size(120, 40);
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
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    Rectangle R1 = new Rectangle(0, 0, Width, Height / 2);
                    Rectangle R2 = new Rectangle(0, Height / 2, Width, Height);
                    G.DrawRectangle(new Pen(_ColorA), 0, 0, Width - 1, Height - 1);

                    if (State == 2)
                    {
                        Brush GB1 = new LinearGradientBrush(R1, _ColorC, _ColorB, 40.0F);
                        Brush GB2 = new LinearGradientBrush(R2, _ColorB, _ColorC, 90.0F);
                        G.FillRectangle(GB1, R1);
                        G.FillRectangle(GB2, R2);
                        //Draw.Gradient(G, _ColorB, _ColorC, 1, 1, Width - 2, Height - 2);
                    }
                    else
                    {
                        Brush GB1 = new LinearGradientBrush(R1, _ColorB, _ColorC, 90.0F);
                        Brush GB2 = new LinearGradientBrush(R2, _ColorC, _ColorB, 90.0F);
                        G.FillRectangle(GB1, R1);
                        G.FillRectangle(GB2, R2);
                        // Draw.Gradient(G, _ColorC, _ColorB, 1, 1, Width - 2, Height - 2);
                    }
                    Pen P2 = new Pen(Color.Black, 2);
                    G.DrawRectangle(P2, 0, 0, Width, Height);
                    SizeF O = G.MeasureString(Text, Font);
                    G.DrawString(Text, Font, new SolidBrush(ForeColor), Width / 2 - O.Width / 2, Height / 2 - O.Height / 2);

                    //Draw.Blend(G, _ColorD, _ColorE, _ColorD, 0.5, 0, 1, 1, Width - 2, 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B1, 0, 0);
                }
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);
        }
    }

    #endregion
}