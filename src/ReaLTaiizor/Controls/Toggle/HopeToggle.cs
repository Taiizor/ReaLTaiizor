#region Imports

using ReaLTaiizor.Colors;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace ReaLTaiizor.Controls
{
    #region HopeToggle

    public class HopeToggle : System.Windows.Forms.CheckBox
    {
        #region Variables

        private readonly Timer AnimationTimer = new() { Interval = 1 };
        private int PointAnimationNum = 4;

        #endregion

        #region Settings
        public Color BaseColor { get; set; } = Color.FromArgb(44, 55, 66);
        public Color BaseColorA { get; set; } = HopeColors.OneLevelBorder;
        public Color BaseColorB { get; set; } = Color.FromArgb(100, HopeColors.PrimaryColor);
        public Color HeadColorA { get; set; } = HopeColors.OneLevelBorder;
        public Color HeadColorB { get; set; } = Color.White;
        public Color HeadColorC { get; set; } = HopeColors.PrimaryColor;
        public Color HeadColorD { get; set; } = HopeColors.PrimaryColor;
        #endregion

        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 20; Width = 48;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(BaseColor);

            GraphicsPath roundRectangle = new();
            int radius = 9;
            roundRectangle.AddArc(11, 5, radius - 1, radius, 180, 90);
            roundRectangle.AddArc(Width - 21, 5, radius - 1, radius, -90, 90);
            roundRectangle.AddArc(Width - 21, Height - 14, radius - 1, radius, 0, 90);
            roundRectangle.AddArc(11, Height - 14, radius - 1, radius, 90, 90);
            roundRectangle.CloseAllFigures();

            graphics.FillPath(new SolidBrush(Checked ? BaseColorB : BaseColorA), roundRectangle);

            graphics.FillEllipse(new SolidBrush(Checked ? HeadColorC : HeadColorA), new RectangleF(PointAnimationNum, 1, 18, 18));
            graphics.FillEllipse(new SolidBrush(Checked ? HeadColorD : HeadColorB), new RectangleF(PointAnimationNum + 2, 3, 14, 14));
        }

        public HopeToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 20; Width = 47;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            Cursor = Cursors.Hand;
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 24)
                {
                    PointAnimationNum += 2;
                    Invalidate();
                }
            }
            else if (PointAnimationNum > 4)
            {
                PointAnimationNum -= 2;
                Invalidate();
            }
        }
    }

    #endregion
}