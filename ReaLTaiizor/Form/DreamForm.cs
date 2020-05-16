#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor
{
    #region DreamForm

    public class DreamForm : GroupBox
    {
        private int _TitleHeight = 25;
        public int TitleHeight
        {
            get
            {
                return _TitleHeight;
            }
            set
            {
                if (value > Height)
                    value = Height;
                if (value < 2)
                    Height = 1;
                _TitleHeight = value;
                Invalidate();
            }
        }
        private HorizontalAlignment _TitleAlign = (HorizontalAlignment)2;
        public HorizontalAlignment TitleAlign
        {
            get
            {
                return _TitleAlign;
            }
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
                Message M = Message.Create((IntPtr)Parent.Handle, 161, (IntPtr)2, (IntPtr)0);
                //DefWndProc(M);
                DefWndProc(ref M);
            }
            base.OnMouseDown(e);
        }
        Color C1 = Color.FromArgb(40, 218, 255), C2 = Color.FromArgb(63, 63, 63), C3 = Color.FromArgb(41, 41, 41);//(74, 74, 74)
        Color C4 = Color.FromArgb(27, 27, 27), C5 = Color.FromArgb(0, 0, 0, 0), C6 = Color.FromArgb(25, 255, 255, 255);
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (Bitmap B = new Bitmap(Width, Height))
            {
                using (Graphics G = Graphics.FromImage(B))
                {
                    G.Clear(C3);

                    //Draw.Gradient(G, C4, C3, 0, 0, Width, _TitleHeight)

                    SizeF S = G.MeasureString(Text + "1", Font);
                    float O = 6;
                    if (_TitleAlign == (HorizontalAlignment)2)
                    {
                        O = Width / 2 - S.Width / 2;
                    }
                    if (_TitleAlign == (HorizontalAlignment)1)
                    {
                        O = Width - S.Width - 6;
                    }
                    Rectangle R = new Rectangle((int)O, (_TitleHeight + 2) / 2 - (int)S.Height / 2, (int)S.Width, (int)S.Height);
                    using (Brush T = new LinearGradientBrush(R, C1, C3, LinearGradientMode.Vertical))
                    {
                        G.DrawString(Text, Font, T, R);
                    }

                    G.DrawLine(new Pen(C3), 0, 1, Width, 1);

                    // Draw.Blend(G, C5, C6, C5, 0.5, 0, 0, _TitleHeight + 1, Width, 1)
                    ColorBlend x = new ColorBlend();
                    Color[] temp = { C5, C6, C5 };
                    x.Colors = temp;
                    float[] temp2 = { 0.5F, 0, 0, _TitleHeight + 1, Width, 1 };
                    x.Positions = temp2;
                    /*
                    LinearGradientBrush B = new LinearGradientBrush(new Point(10, 110),
                                                        new Point(140, 110),
                                                        Color.White,
                                                        Color.Black);
                    B.InterpolationColors = C_Blend;
                    */
                    G.DrawLine(new Pen(C4), 0, _TitleHeight, Width, _TitleHeight);
                    G.DrawRectangle(new Pen(C4), 0, 0, Width - 1, Height - 1);
                    Bitmap B1 = B;
                    e.Graphics.DrawImage(B, 0, 0);
                }
            }
        }
    }

    #endregion
}