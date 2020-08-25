#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region Badge

    public class Badge : Control
    {
        #region Variables

        private int _Value = 0;
        private int _Maximum = 99;
        private Color _BorderColor = Color.FromArgb(205, 70, 66);
        private Color _BGColorA = Color.FromArgb(197, 69, 68);
        private Color _BGColorB = Color.FromArgb(176, 52, 52);

        #endregion

        #region Properties

        public int Value
        {
            get
            {
                if (_Value == 0)
                    return 0;
                return _Value;
            }
            set
            {
                if (value > _Maximum)
                    value = _Maximum;
                _Value = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (value < _Value)
                    _Value = value;
                _Maximum = value;
                Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; Invalidate(); }
        }

        public Color BGColorA
        {
            get { return _BGColorA; }
            set { _BGColorA = value; Invalidate(); }
        }

        public Color BGColorB
        {
            get { return _BGColorB; }
            set { _BGColorB = value; Invalidate(); }
        }

        #endregion

        public Badge()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            Text = null;
            DoubleBuffered = true;
            ForeColor = Color.FromArgb(255, 255, 253);
            Font = new Font("Segoe UI", 8, FontStyle.Bold);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 20;
            Width = 20;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var _G = e.Graphics;
            string myString = _Value.ToString();
            _G.Clear(BackColor);
            _G.SmoothingMode = SmoothingMode.AntiAlias;
            LinearGradientBrush LGB = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(18, 20)), _BGColorA, _BGColorB, 90f);

            // Fills the body with LGB gradient
            _G.FillEllipse(LGB, new Rectangle(new Point(0, 0), new Size(18, 18)));
            // Draw border
            _G.DrawEllipse(new Pen(_BorderColor), new Rectangle(new Point(0, 0), new Size(18, 18)));
            _G.DrawString(myString, Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 2, Height), new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
            e.Dispose();
        }

    }

    #endregion
}