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
            get => gridColor;
            set
            {
                gridColor = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Show gridlines")]
        public bool ShowGrid
        {
            get => showGrid;
            set
            {
                showGrid = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 1")]
        public Color Color1
        {
            get => color1;
            set
            {
                color1 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 2")]
        public Color Color2
        {
            get => color2;
            set
            {
                color2 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 3")]
        public Color Color3
        {
            get => color3;
            set
            {
                color3 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 4")]
        public Color Color4
        {
            get => color4;
            set
            {
                color4 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 5")]
        public Color Color5
        {
            get => color5;
            set
            {
                color5 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 6")]
        public Color Color6
        {
            get => color6;
            set
            {
                color6 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 7")]
        public Color Color7
        {
            get => color7;
            set
            {
                color7 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 8")]
        public Color Color8
        {
            get => color8;
            set
            {
                color8 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 9")]
        public Color Color9
        {
            get => color9;
            set
            {
                color9 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 10")]
        public Color Color10
        {
            get => color10;
            set
            {
                color10 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 11")]
        public Color Color11
        {
            get => color11;
            set
            {
                color11 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 12")]
        public Color Color12
        {
            get => color12;
            set
            {
                color12 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 13")]
        public Color Color13
        {
            get => color13;
            set
            {
                color13 = value;
                Invalidate();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Color 14")]
        public Color Color14
        {
            get => color14;
            set
            {
                color14 = value;
                Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.X > 0 && e.Y < Height)
            {
                selectedColor = color2;
            }

            if (e.X > 0 && e.Y < Height / 2)
            {
                selectedColor = color1;
            }

            if (e.X > Increment && e.Y < Height)
            {
                selectedColor = color4;
            }

            if (e.X > Increment && e.Y < Height / 2)
            {
                selectedColor = color3;
            }

            if (e.X > Increment * 2 && e.Y < Height)
            {
                selectedColor = color6;
            }

            if (e.X > Increment * 2 && e.Y < Height / 2)
            {
                selectedColor = color5;
            }

            if (e.X > Increment * 3 && e.Y < Height)
            {
                selectedColor = color8;
            }

            if (e.X > Increment * 3 && e.Y < Height / 2)
            {
                selectedColor = color7;
            }

            if (e.X > Increment * 4 && e.Y < Height)
            {
                selectedColor = color10;
            }

            if (e.X > Increment * 4 && e.Y < Height / 2)
            {
                selectedColor = color9;
            }

            if (e.X > Increment * 5 && e.Y < Height)
            {
                selectedColor = color12;
            }

            if (e.X > Increment * 5 && e.Y < Height / 2)
            {
                selectedColor = color11;
            }

            if (e.X > Increment * 6 && e.Y < Height)
            {
                selectedColor = color14;
            }

            if (e.X > Increment * 6 && e.Y < Height / 2)
            {
                selectedColor = color13;
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

            e.Graphics.FillRectangle(new SolidBrush(color1), 0, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color2), 0, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color3), Increment, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color4), Increment, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color5), Increment * 2, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color6), Increment * 2, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color7), Increment * 3, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color8), Increment * 3, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color9), Increment * 4, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color10), Increment * 4, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color11), Increment * 5, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color12), Increment * 5, Height / 2, Increment, Height);
            e.Graphics.FillRectangle(new SolidBrush(color13), Increment * 6, 0, Increment, Height / 2);
            e.Graphics.FillRectangle(new SolidBrush(color14), Increment * 6, Height / 2, Increment, Height);

            if (showGrid)
            {
                e.Graphics.DrawRectangle(new Pen(gridColor, 1f), 0, 0, (Increment * 7) - 1, Height - 1);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment, 0, Increment, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment * 2, 0, Increment * 2, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment * 3, 0, Increment * 3, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment * 4, 0, Increment * 4, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment * 5, 0, Increment * 5, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), Increment * 6, 0, Increment * 6, Height);
                e.Graphics.DrawLine(new Pen(gridColor, 1f), 0, Height / 2, (Increment * 7) - 1, Height / 2);
            }
        }

        private int Increment;

        private Color selectedColor;

        private Color gridColor = Color.White;

        private bool showGrid = true;

        private Color color1 = Color.FromArgb(30, 33, 38);

        private Color color2 = Color.FromArgb(37, 40, 49);

        private Color color3 = Color.FromArgb(24, 11, 56);

        private Color color4 = Color.FromArgb(48, 36, 76);

        private Color color5 = Color.FromArgb(1, 119, 215);

        private Color color6 = Color.FromArgb(26, 169, 219);

        private Color color7 = Color.FromArgb(24, 202, 142);

        private Color color8 = Color.FromArgb(102, 217, 174);

        private Color color9 = Color.FromArgb(230, 71, 89);

        private Color color10 = Color.FromArgb(234, 129, 136);

        private Color color11 = Color.FromArgb(159, 133, 255);

        private Color color12 = Color.FromArgb(188, 170, 252);

        private Color color13 = Color.FromArgb(228, 216, 54);

        private Color color14 = Color.FromArgb(235, 227, 120);
    }

    #endregion
}