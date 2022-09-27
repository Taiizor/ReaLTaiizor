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
    #region HopeSwitch

    public class HopeSwitch : System.Windows.Forms.CheckBox
    {
        #region Variables

        private readonly Timer AnimationTimer = new() { Interval = 1 };
        private int PointAnimationNum = 3;

        #endregion

        #region Settings

        public Color BaseColor { get; set; } = Color.White;

        public Color BaseOnColor { get; set; } = HopeColors.PrimaryColor;

        public Color BaseOffColor { get; set; } = HopeColors.OneLevelBorder;

        #endregion

        #region Events

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }

        protected override void OnResize(EventArgs e)
        {
            Height = 20; Width = 40;
            Invalidate();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.Clear(Parent.BackColor);

            GraphicsPath backRect = new();
            backRect.AddArc(new RectangleF(0.5f, 0.5f, Height - 1, Height - 1), 90, 180);
            backRect.AddArc(new RectangleF(Width - Height + 0.5f, 0.5f, Height - 1, Height - 1), 270, 180);
            backRect.CloseAllFigures();

            graphics.FillPath(new SolidBrush(Checked ? BaseOnColor : BaseOffColor), backRect);
            graphics.FillEllipse(new SolidBrush(BaseColor), new RectangleF(PointAnimationNum, 2, 16, 16));
        }

        public HopeSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            DoubleBuffered = true;
            Height = 20; Width = 42;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            Cursor = Cursors.Hand;
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (PointAnimationNum < 21)
                {
                    PointAnimationNum += 2;
                    Invalidate();
                }
            }
            else if (PointAnimationNum > 3)
            {
                PointAnimationNum -= 2;
                Invalidate();
            }
        }
    }

    #endregion
}