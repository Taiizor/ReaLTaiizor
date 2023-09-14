#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Panel

    public class Panel : ContainerControl
    {
        private GraphicsPath Shape;

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private Color _EdgeColor = Color.FromArgb(32, 41, 50);
        public Color EdgeColor
        {
            get => _EdgeColor;
            set
            {
                _EdgeColor = value;
                Invalidate();
            }
        }

        public Panel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            BackColor = Color.FromArgb(39, 51, 63);
            Size = new(187, 117);
            Padding = new Padding(5, 5, 5, 5);
            DoubleBuffered = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Shape = new();
            Shape.AddArc(0, 0, 10, 10, 180, 90);
            Shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            Shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            Shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            Shape.CloseAllFigures();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new(Width, Height);
            Graphics G = Graphics.FromImage(B);

            G.SmoothingMode = SmoothingType;

            G.Clear(EdgeColor); // Set control background to transparent
            G.FillPath(new SolidBrush(BackColor), Shape); // Draw RTB background
            G.DrawPath(new(BackColor), Shape); // Draw border

            G.Dispose();
            e.Graphics.DrawImage((Image)B.Clone(), 0, 0);
            B.Dispose();
        }
    }

    #endregion
}