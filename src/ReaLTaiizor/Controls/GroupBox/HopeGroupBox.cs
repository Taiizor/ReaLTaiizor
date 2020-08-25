#region Imports

using System.Drawing;
using ReaLTaiizor.Utils;
using ReaLTaiizor.Colors;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeGroupBox

    public class HopeGroupBox : System.Windows.Forms.GroupBox
    {
        #region Variables

        private Color _LineColor = HopeColors.OneLevelBorder;
        private Color _BorderColor = HopeColors.OneLevelBorder;

        #endregion

        #region Settings

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color ThemeColor { get; set; } = HopeColors.PrimaryColor;

        [RefreshProperties(RefreshProperties.Repaint)]
        public bool ShowText { get; set; } = false;

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Parent.BackColor);

            var BG = RoundRectangle.CreateRoundRect(1, 1, Width - 2, Height - 2, 3);
            graphics.FillPath(new SolidBrush(ThemeColor), BG);
            graphics.DrawPath(new Pen(_BorderColor), BG);

            if (ShowText)
            {
                graphics.DrawLine(new Pen(_LineColor, 1), 0, 38, Width, 38);
                graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new RectangleF(15, 0, Width - 50, 38), HopeStringAlign.Left);
            }
        }

        public HopeGroupBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            ForeColor = HopeColors.MainText;
        }
    }

    #endregion
}