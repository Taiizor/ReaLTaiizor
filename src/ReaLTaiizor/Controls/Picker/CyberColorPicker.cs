#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region CyberColorPicker

    public partial class CyberColorPicker : UserControl
    {
        #region Variables

        private class ValueBox
        {
            public float Value = 1F;
            public Color Color = Color.White;
        }

        private bool ValueBoxMD;
        private PointF CursorPos;
        private bool ColorPickerMD;

        #endregion

        #region Property Region

        private Color tmp_selectedcolor;
        [Category("Cyber")]
        [Description("Selected color")]
        public Color SelectedColor
        {
            get => tmp_selectedcolor;
            set
            {
                tmp_selectedcolor = value;
                ColorChanged(value);
            }
        }

        #endregion

        #region Constructor Region

        public CyberColorPicker()
        {
            InitializeComponent();

            Tag = "Cyber";
            pictureBox1.Tag = new PointF((float)pictureBox1.Width / 2, (float)pictureBox1.Height / 2);
            pictureBox3.Tag = new ValueBox();

            Bitmap Wheel = new(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(Wheel);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawWheel((float)Wheel.Width / 2, g);
            pictureBox1.Image = Wheel;
            CursorPos = new PointF((float)pictureBox1.Height / 2, (float)pictureBox1.Height / 2);
        }

        #endregion

        #region Event Region

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ColorPickerMD = true;
            PickColor(e.X, e.Y);
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (ColorPickerMD)
            {
                PickColor(e.X, e.Y);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ColorPickerMD = false;
            PickColor(e.X, e.Y);
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawEllipse(new Pen(Color.DarkGray, 1F), CursorPos.X - 4, CursorPos.Y - 4, 8, 8);
        }

        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush Brush = new(new Point(0, 0), new Point(0, pictureBox3.Height), ((ValueBox)pictureBox3.Tag).Color, Color.Black);
            e.Graphics.FillRectangle(Brush, pictureBox3.ClientRectangle);
            e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), 0, (pictureBox3.Height * (1 - ((ValueBox)pictureBox3.Tag).Value)) - 2, pictureBox3.Width, 4);
        }

        private void PictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ValueBoxMD = true;
            if (e.Y > pictureBox3.Height || e.Y < 0)
            {
                return;
            } ((ValueBox)pictureBox3.Tag).Value = (float)(pictureBox3.Height - e.Y) / pictureBox3.Height;
            PickColor(((PointF)pictureBox1.Tag).X, ((PointF)pictureBox1.Tag).Y);
        }

        private void PictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (ValueBoxMD)
            {
                if (e.Y > pictureBox3.Height || e.Y < 0)
                {
                    return;
                } ((ValueBox)pictureBox3.Tag).Value = (float)(pictureBox3.Height - e.Y) / pictureBox3.Height;
                PickColor(((PointF)pictureBox1.Tag).X, ((PointF)pictureBox1.Tag).Y);
            }
        }
        private void PictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            ValueBoxMD = false;
        }

        #endregion

        #region Event Handler Region

        public delegate void EventHandler(Color color);
        [Category("Cyber")]
        [Description("The event is raised when the value of the Text property changes in Control.")]
        public event EventHandler ColorChanged = delegate { };

        #endregion

        #region Method Region

        private void DrawWheel(float radius, Graphics graphics)
        {
            GraphicsPath FillPath = new();
            FillPath.AddEllipse(0, 0, radius * 2, radius * 2);
            FillPath.Flatten();

            GraphicsPath BrushPath = new();
            BrushPath.AddEllipse(-1, -1, (radius * 2) + 2, (radius * 2) + 2);
            BrushPath.Flatten();

            graphics.FillPath(GetBrush(BrushPath), FillPath);
        }

        private Brush GetBrush(GraphicsPath graphicsPath)
        {
            PathGradientBrush Brush = new(graphicsPath) { CenterColor = Color.White };

            Color[] Colors = new Color[graphicsPath.PointCount];
            for (int Angle = 0; Angle < Colors.Length; Angle++)
            {
                Colors[Angle] = HSVtoRGB((float)Angle / Colors.Length, 1, 1);
            }
            Brush.SurroundColors = Colors;

            return Brush;
        }

        private Color GetPixelColor(float x, float y, float value, float radius)
        {
            float R = (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            float S = (float)(R / radius);
            if (S > 1)
            {
                return Color.Transparent;
            }

            double Angle;
            if (x > 0 && y > 0)
            {
                Angle = Math.Asin(y / R);
            }
            else if (x <= 0 && y > 0)
            {
                Angle = Math.Acos(y / R) + (Math.PI / 2);
            }
            else if (x <= 0 && y <= 0)
            {
                Angle = Math.Asin(-y / R) + Math.PI;
            }
            else
            {
                Angle = Math.Acos(-y / R) + (3 * Math.PI / 2);
            }

            float H = (float)(Angle / Math.PI / 2);

            return HSVtoRGB(H, S, value);
        }

        private Color PickColor(float x, float y)
        {
            float x1 = x - ((float)pictureBox1.Width / 2);
            float y1 = y - ((float)pictureBox1.Height / 2);
            float Radius = (float)Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(y1, 2));

            if (Radius > (float)pictureBox1.Width / 2)
            {
                float mult = (float)pictureBox1.Width / 2 / Radius;
                x1 *= mult;
                y1 *= mult;
            }

            Color Color = GetPixelColor(x1, y1, ((ValueBox)pictureBox3.Tag).Value, (float)pictureBox1.Width / 2);

            if (Color == Color.Transparent)
            {
                return Color.Empty;
            }

            label1.Text = $"RGB: {Color.R}, {Color.G}, {Color.B}";
            label2.Text = $"HEX: #{Color.ToArgb():X}";
            CursorPos = new PointF(x1 + ((float)pictureBox1.Width / 2), y1 + ((float)pictureBox1.Height / 2));
            pictureBox2.BackColor = Color;
            ((ValueBox)pictureBox3.Tag).Color = Color;
            pictureBox1.Tag = new PointF(x, y);
            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();

            SelectedColor = Color;
            return Color;
        }

        private Color HSVtoRGB(double H, double S, double V)
        {
            double R, G, B;

            if (S == 0)
            {
                R = V * 255;
                G = V * 255;
                B = V * 255;
            }
            else
            {
                double H1 = H * 6;
                if (H1 == 6)
                {
                    H1 = 0;
                }

                int i = (int)Math.Floor(H1);

                double i1 = V * (1 - S);
                double i2 = V * (1 - (S * (H1 - i)));
                double i3 = V * (1 - (S * (1 - (H1 - i))));

                switch (i)
                {
                    case 0:
                        R = V;
                        G = i3;
                        B = i1;
                        break;
                    case 1:
                        R = i2;
                        G = V;
                        B = i1;
                        break;
                    case 2:
                        R = i1;
                        G = V;
                        B = i3;
                        break;
                    case 3:
                        R = i1;
                        G = i2;
                        B = V;
                        break;
                    case 4:
                        R = i3;
                        G = i1;
                        B = V;
                        break;
                    default:
                        R = V;
                        G = i1;
                        B = i2;
                        break;
                }

                R *= 255;
                G *= 255;
                B *= 255;
            }

            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        #endregion
    }

    #endregion
}