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
        #region Variables

        private int _Value = 0;
        private int _Maximum = 9;
        private Color _BorderColor = Color.FromArgb(205, 70, 66);
        private Color _BGColorA = Color.FromArgb(197, 69, 68);
        private Color _BGColorB = Color.FromArgb(176, 52, 52);

        #endregion

        #region Properties

        public int Value
        {
            get => _Value;
            set { _Value = value; Invalidate(); }
        }

        public int Maximum
        {
            get => _Maximum;
            set { _Maximum = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get => _BorderColor;
            set { _BorderColor = value; Invalidate(); }
        }

        public Color BGColorA
        {
            get => _BGColorA;
            set { _BGColorA = value; Invalidate(); }
        }

        public Color BGColorB
        {
            get => _BGColorB;
            set { _BGColorB = value; Invalidate(); }
        }

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
            LinearGradientBrush LGB = new(new Rectangle(new Point(0, 0), new Size(Width - 2, Height)), _BGColorA, _BGColorB, 90f);

            // Fills the body with LGB gradient
            _G.FillEllipse(LGB, new(new Point(0, 0), new Size(Width - 2, Height - 2)));
            // Draw border
            _G.DrawEllipse(new(_BorderColor), new(new Point(0, 0), new Size(Width - 2, Height - 2)));
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