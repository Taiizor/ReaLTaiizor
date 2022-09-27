#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Forms
{
    #region DreamForm

    public class DreamForm : GroupBox
    {
        private int _TitleHeight = 25;
        public int TitleHeight
        {
            get => _TitleHeight;
            set
            {
                if (value > Height)
                {
                    value = Height;
                }

                if (value < 2)
                {
                    Height = 1;
                }

                _TitleHeight = value;
                Invalidate();
            }
        }

        private HorizontalAlignment _TitleAlign = (HorizontalAlignment)2;
        public HorizontalAlignment TitleAlign
        {
            get => _TitleAlign;
            set
            {
                _TitleAlign = value;
                Invalidate();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            Dock = (DockStyle)5;
            if (Parent.GetType() == typeof(Form))
            {//.FormBorderStyle = 0;
             //(ParentForm)Parent;
                FindForm().FormBorderStyle = 0;
                //Convert.ChangeType(Parent, typeof(Form));
            }

            base.OnHandleCreated(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (new Rectangle(Parent.Location.X, Parent.Location.Y, Width - 1, _TitleHeight - 1).IntersectsWith(new Rectangle(MousePosition.X, MousePosition.Y, 1, 1)))
            {
                Capture = false;
                //Message  M = Message.Create(Parent.Handle, 161, 2, 0);
                Message M = Message.Create(Parent.Handle, 161, (IntPtr)2, (IntPtr)0);
                //DefWndProc(M);
                DefWndProc(ref M);
            }
            base.OnMouseDown(e);
        }

        public Color ColorA { get; set; } = Color.FromArgb(40, 218, 255);
        public Color ColorB { get; set; } = Color.FromArgb(63, 63, 63);
        public Color ColorC { get; set; } = Color.FromArgb(41, 41, 41);
        public Color ColorD { get; set; } = Color.FromArgb(27, 27, 27);
        public Color ColorE { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color ColorF { get; set; } = Color.FromArgb(25, 255, 255, 255);


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using Bitmap B = new(Width, Height);
            using Graphics G = Graphics.FromImage(B);
            G.Clear(ColorC);

            //Draw.Gradient(G, _ColorD, _ColorC, 0, 0, Width, _TitleHeight)

            SizeF S = G.MeasureString(Text + "1", Font);
            float O = 6;
            if (_TitleAlign == (HorizontalAlignment)2)
            {
                O = (Width / 2) - (S.Width / 2);
            }

            if (_TitleAlign == (HorizontalAlignment)1)
            {
                O = Width - S.Width - 6;
            }

            Rectangle R = new((int)O, ((_TitleHeight + 2) / 2) - ((int)S.Height / 2), (int)S.Width, (int)S.Height);
            using (Brush T = new LinearGradientBrush(R, ColorA, ColorC, LinearGradientMode.Vertical))
            {
                G.DrawString(Text, Font, T, R);
            }

            G.DrawLine(new(ColorC), 0, 1, Width, 1);

            // Draw.Blend(G, _ColorE, _ColorF, _ColorE, 0.5, 0, 0, _TitleHeight + 1, Width, 1)
            ColorBlend x = new();
            Color[] temp = { ColorE, ColorF, ColorE };
            x.Colors = temp;
            float[] temp2 = { 0.5F, 0, 0, _TitleHeight + 1, Width, 1 };
            x.Positions = temp2;

            /*
                LinearGradientBrush B = new(new Point(10, 110), new Point(140, 110), Color.White, Color.Black);
                B.InterpolationColors = C_Blend;
            */

            G.DrawLine(new(ColorD), 0, _TitleHeight, Width, _TitleHeight);
            G.DrawRectangle(new(ColorD), 0, 0, Width - 1, Height - 1);
            Bitmap B1 = B;
            e.Graphics.DrawImage(B, 0, 0);
        }
    }

    #endregion
}