#region Imports

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Badge

    public class Badge : Control
    {
        #region Properties

        public int Value
        {
            get;
            set { field = value; Invalidate(); }
        } = 0;

        public int Maximum
        {
            get;
            set { field = value; Invalidate(); }
        } = 9;

        public Color BorderColor
        {
            get;
            set { field = value; Invalidate(); }
        } = Color.FromArgb(205, 70, 66);

        public Color BGColorA
        {
            get;
            set { field = value; Invalidate(); }
        } = Color.FromArgb(197, 69, 68);

        public Color BGColorB
        {
            get;
            set { field = value; Invalidate(); }
        } = Color.FromArgb(176, 52, 52);

        private string Texting
        {
            get
            {
                if (Value > Maximum)
                {
                    return $"{Maximum}+";
                }
                else
                {
                    return $"{Value}";
                }
            }
        }

        #endregion

        public Badge()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            Text = null;
            Size = new(20, 20);
            DoubleBuffered = true;
            ForeColor = Color.FromArgb(255, 255, 253);
            Font = new("Segoe UI", 8, FontStyle.Bold);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics _G = e.Graphics;

            _G.Clear(BackColor);
            _G.SmoothingMode = SmoothingMode.AntiAlias;

            LinearGradientBrush LGB = new(new Rectangle(new Point(0, 0), new Size(Width - 2, Height)), BGColorA, BGColorB, 90f);

            // Fills the body with LGB gradient
            _G.FillEllipse(LGB, new(new Point(0, 0), new Size(Width - 2, Height - 2)));

            // Draw border
            _G.DrawEllipse(new(BorderColor), new(new Point(0, 0), new Size(Width - 2, Height - 2)));

            _G.DrawString(Texting, Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 2, Height), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });

            e.Dispose();
        }

    }

    #endregion
}