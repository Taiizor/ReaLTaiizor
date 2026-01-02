#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region ParrotPalette

    public class ParrotPalette : Control
    {
        public ParrotPalette()
        {
            Size = new Size(175, 50);
            Increment = Width / 7;
            Cursor = Cursors.Hand;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor { get; set; }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The selected color")]
        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                selectedColor = value;
                OnColorChange();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("The color of the grid")]
        public Color GridColor
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.White;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show gridlines")]
        public bool ShowGrid
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = true;

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 1")]
        public Color Color1
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(30, 33, 38);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 2")]
        public Color Color2
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(37, 40, 49);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 3")]
        public Color Color3
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(24, 11, 56);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 4")]
        public Color Color4
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(48, 36, 76);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 5")]
        public Color Color5
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(1, 119, 215);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 6")]
        public Color Color6
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(26, 169, 219);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 7")]
        public Color Color7
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(24, 202, 142);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 8")]
        public Color Color8
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(102, 217, 174);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 9")]
        public Color Color9
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(230, 71, 89);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 10")]
        public Color Color10
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(234, 129, 136);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 11")]
        public Color Color11
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(159, 133, 255);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 12")]
        public Color Color12
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(188, 170, 252);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 13")]
        public Color Color13
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(228, 216, 54);

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 14")]
        public Color Color14
        {
            get;
            set
            {
                field = value;
                Invalidate();
            }
        } = Color.FromArgb(235, 227, 120);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.X > 0 && e.Y < Height)
            {
                selectedColor = Color2;
            }

            if (e.X > 0 && e.Y < Height / 2)
            {
                selectedColor = Color1;
            }

            if (e.X > Increment && e.Y < Height)
            {
                selectedColor = Color4;
            }

            if (e.X > Increment && e.Y < Height / 2)
            {
                selectedColor = Color3;
            }

            if (e.X > Increment * 2 && e.Y < Height)
            {
                selectedColor = Color6;
            }

            if (e.X > Increment * 2 && e.Y < Height / 2)
            {
                selectedColor = Color5;
            }

            if (e.X > Increment * 3 && e.Y < Height)
            {
                selectedColor = Color8;
            }

            if (e.X > Increment * 3 && e.Y < Height / 2)
            {
                selectedColor = Color7;
            }

            if (e.X > Increment * 4 && e.Y < Height)
            {
                selectedColor = Color10;
            }

            if (e.X > Increment * 4 && e.Y < Height / 2)
            {
                selectedColor = Color9;
            }

            if (e.X > Increment * 5 && e.Y < Height)
            {
                selectedColor = Color12;
            }

            if (e.X > Increment * 5 && e.Y < Height / 2)
            {
                selectedColor = Color11;
            }

            if (e.X > Increment * 6 && e.Y < Height)
            {
                selectedColor = Color14;
            }

            if (e.X > Increment * 6 && e.Y < Height / 2)
            {
                selectedColor = Color13;
            }

            OnColorChange();
        }

        public event EventHandler ColorChanged;

        protected virtual void OnColorChange()
        {
            ColorChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Increment = Width / 7;

            e.Graphics.FillRectangle(new SolidBrush(Color1), 0, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color2), 0, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color3), Increment, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color4), Increment, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color5), Increment * 2, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color6), Increment * 2, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color7), Increment * 3, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color8), Increment * 3, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color9), Increment * 4, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color10), Increment * 4, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color11), Increment * 5, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color12), Increment * 5, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color13), Increment * 6, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(Color14), Increment * 6, Height / 2, Increment, Height);

            if (ShowGrid)
            {
                e.Graphics.DrawRectangle(new Pen(GridColor, 1f), 0, 0, (Increment * 7) - 1, Height - 1);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment, 0, Increment, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment * 2, 0, Increment * 2, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment * 3, 0, Increment * 3, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment * 4, 0, Increment * 4, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment * 5, 0, Increment * 5, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), Increment * 6, 0, Increment * 6, Height);
                e.Graphics.DrawLine(new Pen(GridColor, 1f), 0, Height / 2, (Increment * 7) - 1, Height / 2);
            }
        }

        private int Increment;

        private Color selectedColor;
    }

    #endregion
}